using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// ClickMarkerLoadData 的摘要说明
    /// </summary>
    public class ClickMarkerLoadData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //获取到标记对应的id
            int markerID = Convert.ToInt32(context.Request["id"]);

            //农田基本数据对象
            SoilNutrientSoft.BLL.FarmlandMeg newFarmlandMegBll = new SoilNutrientSoft.BLL.FarmlandMeg();
            SoilNutrientSoft.Model.FarmlandMeg newFarmlandMegModel = new SoilNutrientSoft.Model.FarmlandMeg();
            //根据id查询数据
            newFarmlandMegModel = newFarmlandMegBll.GetModel(markerID);

            //创建序列化对象
            JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
            //转换成JSON字符串
            // var dataStr = JavaScriptSerializer.Serialize(newFarmlandMegModel);

            //**************************************************
            //土壤养分信息
            SoilNutrientSoft.BLL.SoilNutrientMeg newSoilNutrientMegBll = new SoilNutrientSoft.BLL.SoilNutrientMeg();
            List<SoilNutrientSoft.Model.SoilNutrientMeg> newSoilNutrientMegList = newSoilNutrientMegBll.GetModelList(" All_id = " + markerID.ToString());

            //转换成JSON字符串
            // var dataStr = JavaScriptSerializer.Serialize(newSoilNutrientMegList[0]);

            //**************************************************


            //作物数据
            SoilNutrientSoft.BLL.CropsMeg newCropsMegBll = new SoilNutrientSoft.BLL.CropsMeg();
            List<SoilNutrientSoft.Model.CropsMeg> newCropsMegList = newCropsMegBll.GetModelList(" All_id = " + markerID.ToString());

            //转换成JSON字符串
            //var dataStr = JavaScriptSerializer.Serialize(newCropsMegList[0]);

            //***************************************

            //农田建议
            SoilNutrientSoft.BLL.FarmlandMSug newFarmlandMSugBll = new SoilNutrientSoft.BLL.FarmlandMSug();
            List<SoilNutrientSoft.Model.FarmlandMSug> newFarmlandMSugList = newFarmlandMSugBll.GetModelList(" All_id = " + markerID.ToString());

            //转换成JSON字符串
            //var dataStr = JavaScriptSerializer.Serialize(newFarmlandMSugList[0]);


            //***************************************

            //图片
            SoilNutrientSoft.BLL.Picture newPictureBll = new SoilNutrientSoft.BLL.Picture();
            //得到数据集合
            List<SoilNutrientSoft.Model.Picture> newPictureModelList = newPictureBll.GetModelList(" All_id = " + markerID.ToString());

            //添加至内部类List中
            List<Pic> newPicList = new List<Pic>();
            foreach (var item in newPictureModelList)
            {
                newPicList.Add(new Pic()
                {
                    picPath = item.picturePath
                });
            }
            //转换成JSON字符串
            //var dataStr = JavaScriptSerializer.Serialize(newPicList);


            SerializeObject newSerializeObject = new SerializeObject()
            {
                FarmlandMegObject = newFarmlandMegModel,
                SoilNutrientMegObject = newSoilNutrientMegList[0],
                CropsMegObject = newCropsMegList[0],
                FarmlandMSugObject = newFarmlandMSugList[0],
                PicObject = newPicList
            };

            var dataStr = JavaScriptSerializer.Serialize(newSerializeObject);


            context.Response.Write(dataStr);
        }

        public class SerializeObject
        {
            public SoilNutrientSoft.Model.FarmlandMeg FarmlandMegObject;
            public SoilNutrientSoft.Model.SoilNutrientMeg SoilNutrientMegObject;
            public SoilNutrientSoft.Model.CropsMeg CropsMegObject;
            public SoilNutrientSoft.Model.FarmlandMSug FarmlandMSugObject;
            public List<Pic> PicObject;
        }

        public class Pic
        {
            public string picPath;
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