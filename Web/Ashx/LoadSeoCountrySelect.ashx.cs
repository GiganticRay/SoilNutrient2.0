using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// LoadSeoCountrySelect 的摘要说明
    /// </summary>
    public class LoadSeoCountrySelect : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<option value='{0}'>{1}</option>", -1, "全选");
            //获取到城市的id
            int cId = int.Parse(context.Request["cId"] ?? "1");

            BLL.FarmlandMeg newFarmlandMegBll = new BLL.FarmlandMeg();
            //获取表中该城市下存在的区/县
            DataSet newFarmlandMegAllList = newFarmlandMegBll.GetList("City = " + cId);
            //遍历DataSet
            foreach (DataTable dt in newFarmlandMegAllList.Tables)
            {
                List<Model.FarmlandMeg> FarmlandMegList = newFarmlandMegBll.DataTableToList(dt);
                List<int> existCountry = new List<int>();
                //遍历List中的FarmlandMeg
                foreach (SoilNutrientSoft.Model.FarmlandMeg FM in FarmlandMegList)
                {
                    //判断是否存在该区/县
                    if (!existCountry.Contains(Convert.ToInt32(FM.County)))
                    {
                        //获取到区/县ID
                        int CountryNum = Convert.ToInt32(FM.County);
                        BLL.Areas newAreasBll = new BLL.Areas();
                        List<Model.Areas> newAreasModelList = newAreasBll.GetModelList("AreaID = " + CountryNum);
                        foreach (SoilNutrientSoft.Model.Areas item in newAreasModelList)
                        {
                            //如果不存在该区/县，就将其添加
                            sb.AppendFormat("<option value='{0}'>{1}</option>", item.AreaID, item.AreaName);
                        }
                        //将其添加至已存List中
                        existCountry.Add(CountryNum);
                    }
                }
            }
            context.Response.Write(sb.ToString());
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