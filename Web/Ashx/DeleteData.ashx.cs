using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// DeleteData 的摘要说明
    /// </summary>
    public class DeleteData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            Dictionary<SqlConnection, SqlTransaction> MyDict = new Dictionary<SqlConnection, SqlTransaction>();
            try
            {
                context.Response.ContentType = "text/plain";
                //获取要删除的表的id
                int id = Convert.ToInt32(context.Request["Id"]);

                BLL.FarmlandMeg newFarmlandMegBll = new BLL.FarmlandMeg();

                //删除操作
                bool resultFarmlandMeg = newFarmlandMegBll.Delete(id, MyDict);

                BLL.SoilNutrientMeg newSoilNutrientMegBll = new BLL.SoilNutrientMeg();
                List<Model.SoilNutrientMeg> newSoilNutrientMegModelList = newSoilNutrientMegBll.GetModelList(" All_id = " + id);
                bool resultSoilNutrientMeg = newSoilNutrientMegBll.Delete(newSoilNutrientMegModelList[0].Id, MyDict);


                BLL.CropsMeg newCropsMegBll = new BLL.CropsMeg();
                List<Model.CropsMeg> newCropsMegModelList = newCropsMegBll.GetModelList(" All_id  = " + id);
                bool resultCropsMeg = newCropsMegBll.Delete(newCropsMegModelList[0].Id, MyDict);

                BLL.FarmlandMSug newFarmlandMSugBll = new BLL.FarmlandMSug();
                List<Model.FarmlandMSug> newFarmlandMSugModelList = newFarmlandMSugBll.GetModelList(" All_id  = " + id);
                bool resultFarmlandMSug = newFarmlandMSugBll.Delete(newFarmlandMSugModelList[0].Id, MyDict);


                BLL.Picture newPictureBll = new BLL.Picture();
                List<Model.Picture> newPictureModelList = newPictureBll.GetModelList(" All_id = " + id);
                List<bool> resultPictureList = new List<bool>();
                //判断是否存在图片
                if (newPictureModelList.Count > 0)
                {
                    //用于存储图片绝对路径的集合
                    List<String> filesPath = new List<string>();
                    //如果存在将其删除
                    foreach (Model.Picture Pic in newPictureModelList)
                    {
                        Model.Picture newPictureModel = newPictureBll.GetModel(Pic.Id);
                        //图片相对路径
                        string picPath = newPictureModel.picturePath;
                        //图片绝对路径
                        string absolutePath = context.Request.MapPath(picPath);
                        //将绝对路径添加到集合中
                        filesPath.Add(absolutePath);

                        //将删除结果累计添加到集合中
                        resultPictureList.Add(newPictureBll.Delete(Pic.Id, MyDict));
                    }

                    //删除图片操作
                    foreach (String path in filesPath)
                    {
                        if (File.Exists(path))
                        {
                            //如果文件存在，则删除
                            File.Delete(path);
                        }
                    }

                }

                if (resultFarmlandMeg && resultSoilNutrientMeg && resultFarmlandMSug && resultCropsMeg && !resultPictureList.Contains(false))
                {
                    context.Response.Write("ok");
                }
                else
                {
                    context.Response.Write("删除失败");
                }
                QuitConnTrans(MyDict);
            }
            catch (Exception E)
            {
                ExceptionQuitConnTrans(MyDict);
                //如有异常 将异常信息返回
                context.Response.Write(E.Message);
                //将信息返回给客户端，停止该页的执行  
                context.Response.End();
            }
        }

        /// <summary>
        /// 关闭事物和连接对象
        /// </summary>
        /// <param name="MyDict"></param>
        public void QuitConnTrans(Dictionary<SqlConnection, SqlTransaction> MyDict)
        {
            SqlConnection Myconn = null;
            SqlTransaction MyTrans = null;
            foreach (var item in MyDict)
            {
                Myconn = item.Key;
                MyTrans = item.Value;
            }
            MyTrans.Commit();
            Myconn.Close();
        }

        /// <summary>
        /// 异常关闭事物和连接对象
        /// </summary>
        /// <param name="MyDict"></param>
        public void ExceptionQuitConnTrans(Dictionary<SqlConnection, SqlTransaction> MyDict)
        {
            SqlConnection Myconn = null;
            SqlTransaction MyTrans = null;
            foreach (var item in MyDict)
            {
                Myconn = item.Key;
                MyTrans = item.Value;
            }
            MyTrans.Rollback();
            Myconn.Close();
            MyDict.Clear();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}