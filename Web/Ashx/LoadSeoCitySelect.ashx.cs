using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// LoadSeoCitySelect 的摘要说明
    /// </summary>
    public class LoadSeoCitySelect : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<option value='{0}'>{1}</option>", -1, "全选");

            BLL.FarmlandMeg newFarmlandMegBll = new BLL.FarmlandMeg();
            DataSet newFarmlandMegAllList = newFarmlandMegBll.GetAllList();
            //遍历DataSet
            foreach (DataTable dt in newFarmlandMegAllList.Tables)
            {
                List<Model.FarmlandMeg> FarmlandMegList = newFarmlandMegBll.DataTableToList(dt);
                List<int> existCity = new List<int>();
                //遍历List中的FarmlandMeg
                foreach (SoilNutrientSoft.Model.FarmlandMeg FM in FarmlandMegList)
                {
                    //判断是否存在该城市
                    if (!existCity.Contains(Convert.ToInt32(FM.City)))
                    {
                        //获取到城市ID
                        int CityNum = Convert.ToInt32(FM.City);
                        BLL.Areas newAreasBll = new BLL.Areas();
                        List<Model.Areas> newAreasModelList = newAreasBll.GetModelList("AreaID = " + CityNum);
                        foreach (SoilNutrientSoft.Model.Areas item in newAreasModelList)
                        {
                            //如果不存在该城市，就将其添加
                            sb.AppendFormat("<option value='{0}'>{1}</option>", item.AreaID, item.AreaName);
                        }
                        //将其添加至已存List中
                        existCity.Add(CityNum);
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