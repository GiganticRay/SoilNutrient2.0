using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// LoadBriefInformation 的摘要说明
    /// </summary>
    public class LoadBriefInformation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            SoilNutrientSoft.BLL.FarmlandMeg newFarmlandMeg = new SoilNutrientSoft.BLL.FarmlandMeg();
            DataSet allInfor = newFarmlandMeg.GetAllList();
            //需要的数据集合
            List<InforData> newdata = new List<InforData>();
            //遍历每张表
            foreach (DataTable item in allInfor.Tables)
            {
                //将表转换成集合
                List<SoilNutrientSoft.Model.FarmlandMeg> newFarmlandMegModel = newFarmlandMeg.DataTableToList(item);
                //遍历集合
                foreach (var data in newFarmlandMegModel)
                {
                    newdata.Add(new InforData()
                    {
                        //数据
                        Lon = data.Lon,
                        Lat = data.Lat,
                        householaer_name = data.Name_of_householder,
                        Phone_number = data.Phone_number,
                        sample_name = data.Sample_name,
                        idNum = data.Id.ToString()
                    });
                }
            }
            //创建序列化对象
            JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
            //转换成JSON字符串
            var dataStr = JavaScriptSerializer.Serialize(newdata);

            context.Response.Write(dataStr);
        }

        //内部类
        public class InforData
        {
            public decimal Lon;
            public decimal Lat;
            public string householaer_name;
            public string Phone_number;
            public string sample_name;
            public string idNum;
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