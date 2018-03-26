using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// ProcessSeoSelect 的摘要说明
    /// </summary>
    public class ProcessSeoSelect : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //ProvinceDropdown:-1
            //CountryDropdown:-1
            //NutrientDivideDropdown:1

            //ProvinceDropdown:3
            //CountryDropdown:49
            //NutrientDivideDropdown:4
            int ProvinceNum = Convert.ToInt32(context.Request["ProvinceDropdown"]);
            int CountryNum = Convert.ToInt32(context.Request["CountryDropdown"]);
            int NutrientDivide = Convert.ToInt32(context.Request["NutrientDivideDropdown"]);

            BLL.FarmlandMeg newFarmlandMegBll = new BLL.FarmlandMeg();
            List<Model.FarmlandMeg> newFarmlandMegModel = new List<Model.FarmlandMeg>();
            string selectStr = "";
            //查询到满足条件的集合
            if (ProvinceNum == -1 && CountryNum == -1)
            {
                //表示所有都全选
                selectStr = " 0 = 0 ";
            }
            else if (ProvinceNum >= 0 && CountryNum == -1)
            {
                //表示只有“区县”全选
                selectStr = " City = " + ProvinceNum;
            }
            else
            {
                //
                selectStr = " City = " + ProvinceNum + " and County = " + CountryNum;

            }

            newFarmlandMegModel = newFarmlandMegBll.GetModelList(selectStr);
            //遍历List集合


            BLL.SoilNutrientMeg newSoilNutrientMegBll = new BLL.SoilNutrientMeg();
            Model.SoilNutrientMeg newSoilNutrientMegModel = new Model.SoilNutrientMeg();

            //获取最终数据结果
            List<SoilNutrientMegList> newSoilNutrientMegList = new List<SoilNutrientMegList>();
            foreach (var item in newFarmlandMegModel)
            {
                //获取id
                int id = item.Id;
                //获取土壤养分信息表的模板
                newSoilNutrientMegModel = newSoilNutrientMegBll.GetModelList(" All_id = " + id)[0];
                newSoilNutrientMegList.Add(new SoilNutrientMegList()
                {
                    sampleName = item.Sample_name,
                    N = Convert.ToDouble(newSoilNutrientMegModel.N),
                    P = Convert.ToDouble(newSoilNutrientMegModel.P),
                    K = Convert.ToDouble(newSoilNutrientMegModel.K),
                    HydrolyticN = Convert.ToDouble(newSoilNutrientMegModel.HydrolyticN),
                    QuickP = Convert.ToDouble(newSoilNutrientMegModel.QuickP),
                    QUicK = Convert.ToDouble(newSoilNutrientMegModel.QUicK),
                    OrganicMatter = Convert.ToDouble(newSoilNutrientMegModel.OrganicMatter),
                    PH = Convert.ToDouble(newSoilNutrientMegModel.PH)
                });
            }
            //创建序列化对象
            JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
            //转换成JSON字符串
            var dataStr = JavaScriptSerializer.Serialize(newSoilNutrientMegList);

            context.Response.Write(dataStr);
        }

        //内部类
        public class SoilNutrientMegList
        {
            public string sampleName = "";
            public double N = 0;
            public double P = 0;
            public double K = 0;
            public double HydrolyticN = 0;
            public double QuickP = 0;
            public double QUicK = 0;
            public double OrganicMatter = 0;
            public double PH = 0;
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