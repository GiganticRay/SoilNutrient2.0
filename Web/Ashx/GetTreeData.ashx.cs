using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// GetTreeData 的摘要说明
    /// </summary>
    public class GetTreeData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            StringBuilder newSb = new StringBuilder();
            newSb.Append("[");
            //农田信息对象
            BLL.FarmlandMeg newFarmlandMeg = new BLL.FarmlandMeg();
            //需要的数据集合
            DataSet st = newFarmlandMeg.GetAllList();
            //遍历每张表
            foreach (DataTable item in st.Tables)
            {
                //将表转换成集合
                List<Model.FarmlandMeg> FarmlandMegList = newFarmlandMeg.DataTableToList(item);

                //用于检测是否已经加载过该市的节点  防止多个相同的“市”节点出现
                List<int> existNum = new List<int>();

                //遍历表集合
                int idNum = 1;
                foreach (var data in FarmlandMegList)
                {
                    //地域对象
                    BLL.Areas newAreas = new BLL.Areas();
                    //获取城市对应的ID
                    int cityNum = int.Parse(data.City);
                    //判断该城市是否已经加载，如果已经加载则无需重复加载
                    if (!existNum.Contains(cityNum))
                    {
                        //将表中的城市加到集合中
                        existNum.Add(cityNum);
                        newSb.Append("{");
                        newSb.AppendFormat("\"tags\":[\"-1\"],", idNum);
                        //获取地域数据
                        DataTable d = newAreas.GetList(" AreaID = " + cityNum).Tables[0];
                        List<Model.Areas> newAerasModel = newAreas.DataTableToList(d);
                        string cityName = newAerasModel[0].AreaName;
                        //节点名称
                        newSb.AppendFormat("\"text\":\"{0}\",", cityName);
                        newSb.Append("\"state\":{\"backColor\": \"#428BCA\"},");
                        //子节点集合    
                        newSb.Append("\"nodes\":[");

                        //如果集合中不存在该数就继续加载数据

                        //获取同一个市下的县区
                        DataSet newSameCity = newFarmlandMeg.GetList("City=" + cityNum);
                        foreach (DataTable countryTable in newSameCity.Tables)
                        {
                            //  县/区  同一个城市下的县区集合
                            List<Model.FarmlandMeg> countriesList = newFarmlandMeg.DataTableToList(countryTable);
                            int countryIdNum = 1;
                            //用于检测是否已经加载过该市的节点  防止多个相同的“区/县”节点出现
                            List<int> exitCountry = new List<int>();
                            foreach (var country in countriesList)
                            {
                                //判断是否添加过该  县/区
                                if (!exitCountry.Contains(Convert.ToInt32(country.County)))
                                {
                                    exitCountry.Add(Convert.ToInt32(country.County));
                                    newSb.Append("{");
                                    //通过区县 获取地域数据
                                    DataTable areasCountry = newAreas.GetList(" AreaID = " + country.County).Tables[0];
                                    List<Model.Areas> areasCountryList = newAreas.DataTableToList(areasCountry);
                                    //区县名称
                                    string countryName = areasCountryList[0].AreaName;
                                    //newSb.Append("\"id\":" + idNum.ToString() + countryIdNum.ToString() + ",");
                                    newSb.AppendFormat("\"tags\":[\"-1\"],", idNum.ToString() + countryIdNum.ToString());

                                    //newSb.Append("\"state\":{\"expanded\": \"false\"},");
                                    newSb.AppendFormat("\"text\":\"{0}\",", countryName);
                                    newSb.Append("\"nodes\":[");


                                    //获取同一个市区下的集合
                                    DataTable townTable = newFarmlandMeg.GetList(" City= " + cityNum + " and County= " + country.County).Tables[0];
                                    List<Model.FarmlandMeg> townList = newFarmlandMeg.DataTableToList(townTable);
                                    //int townNum = 1;
                                    foreach (var key in townList)
                                    {
                                        newSb.Append("{");
                                        string sampleName = key.Sample_name;
                                        string townId = key.Id.ToString();
                                        //newSb.Append("\"id\":" + idNum.ToString() + countryIdNum.ToString() + townNum.ToString()+ ",");
                                        //newSb.AppendFormat("\"id\":\"{0}\",", idNum.ToString() + countryIdNum.ToString() + townNum.ToString());
                                        //将数据库中的id给终极子节点
                                        newSb.AppendFormat("\"tags\":[\"{0}\"],", townId);
                                        //添加采样点名称
                                        newSb.AppendFormat("\"text\":\"{0}\"", sampleName);

                                        newSb.Append("},");
                                        //townNum++;
                                    }
                                    //将最后一个“,”移除
                                    newSb.Remove(newSb.Length - 1, 1);
                                    newSb.Append("]");
                                    countryIdNum++;
                                    newSb.Append("},");
                                }
                            }
                            //将最后一个“,”移除
                            newSb.Remove(newSb.Length - 1, 1);
                        }
                        newSb.Append("]");
                        newSb.Append("},");
                        idNum++;
                    }
                }
            }
            if (!(newSb.ToString() == "["))
            {
                //如果StringBuilder中不止“[”，则移除，否则不移除
                //将最后一个“,”移除
                newSb.Remove(newSb.Length - 1, 1);
            }

            newSb.Append("]");
            //context.Response.Write(dataStr);
            context.Response.Write(newSb.ToString());
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