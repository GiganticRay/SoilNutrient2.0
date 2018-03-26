using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// PostEnteringFarmInfo 的摘要说明
    /// </summary>
    public class PostEnteringFarmInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Dictionary<SqlConnection, SqlTransaction> MyDict = new Dictionary<SqlConnection, SqlTransaction>();
            //用在处理存储表单的事物的时候判断是否完毕
            List<int> LFormTimes = new List<int>();
            LFormTimes.Add(0);
            
            //第一张表单  农田信息数据录入后返回的最大id
            int getMaxId = 0;
            //第一张表单  农田信息数据录入返回的结果值
            int resultNumFLM = 0;


            #region 第一张表单  农田信息

            Model.FarmlandMeg newFarmlandMegModel = new Model.FarmlandMeg();
            try
            {
                newFarmlandMegModel.City = context.Request["city"];
                newFarmlandMegModel.County = context.Request["country"];
                newFarmlandMegModel.Town = context.Request["townName"];
                newFarmlandMegModel.Village = context.Request["villageName"];
                newFarmlandMegModel.Sample_name = context.Request["SampleName"];
                newFarmlandMegModel.Lon = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LongitudeDegree"]) ? "0" : context.Request["LongitudeDegree"]) + Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LongitudeMinute"]) ? "0" : context.Request["LongitudeMinute"]) / (decimal)60.0 + Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LongitudeSecond"]) ? "0" : context.Request["LongitudeSecond"]) / (decimal)3600.0;
                newFarmlandMegModel.Lat = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LatitudeDegree"]) ? "0" : context.Request["LatitudeDegree"]) + Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LatitudeMinute"]) ? "0" : context.Request["LatitudeMinute"]) / (decimal)60.0 + Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LatitudeSecond"]) ? "0" : context.Request["LatitudeSecond"]) / (decimal)3600.0;
                newFarmlandMegModel.Name_of_householder = context.Request["NameOfHouseholder"];
                newFarmlandMegModel.Phone_number = context.Request["PhoneNumber"];
                newFarmlandMegModel.Irrigation_Conditions = Convert.ToInt32(String.IsNullOrEmpty(context.Request["IrrigationConditions"]) ? "1" : context.Request["IrrigationConditions"]);
                newFarmlandMegModel.Acreage = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Acreage"]) ? "0" : context.Request["Acreage"]);
                //int.Parse(context.Request["Acreage"]);
                newFarmlandMegModel.Fertility = Convert.ToInt32(String.IsNullOrEmpty(context.Request["Fertility"]) ? "1" : context.Request["Fertility"]);
                newFarmlandMegModel.Weeds = context.Request["Weeds"];

                //需要先将农田信息写入数据库 
                //然后返回农田信息表中的最大ID给其他三张表的All_id使用 
                //以建立四张表的联系

                BLL.FarmlandMeg newFarmlandMegBLL = new BLL.FarmlandMeg();

                //提交数据，返回插入的id
                resultNumFLM = newFarmlandMegBLL.Add(newFarmlandMegModel, MyDict);
                if (resultNumFLM > 0)
                {
                    //表示农田信息表单成功录入
                    //该表中的最大id
                    getMaxId = resultNumFLM;    // newFarmlandMegBLL.GetMaxId()-1;
                }
                else
                {
                    context.Response.Write("农田信息表单数据录入失败");
                    //将信息返回给客户端，停止该页的执行  
                    context.Response.End();
                }

            }
            catch (Exception E)
            {
                //如有异常 将异常信息返回
                context.Response.Write(E.Message);
                //将信息返回给客户端，停止该页的执行  
                context.Response.End();
            }
            #endregion
            if (getMaxId <= 0)
            {
                //如果getMaxId<=0表示第一张表插入失败
                context.Response.Write("信息录入失败");
                //将信息返回给客户端，停止该页的执行  
                context.Response.End();
            }
            else
            {
                try
                {
                    #region 第二张表单 土壤养分信息

                    Model.SoilNutrientMeg newSoilNutrientMegModel = new Model.SoilNutrientMeg();
                    newSoilNutrientMegModel.N = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["N_SoilNutrient"]) ? "0" : context.Request["N_SoilNutrient"]);
                    newSoilNutrientMegModel.P = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["P_SoilNutrient"]) ? "0" : context.Request["P_SoilNutrient"]);

                    newSoilNutrientMegModel.K = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["K_SoilNutrient"]) ? "0" : context.Request["K_SoilNutrient"]);
                    newSoilNutrientMegModel.HydrolyticN = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["HydrolyticN_SoilNutrient"]) ? "0" : context.Request["HydrolyticN_SoilNutrient"]);
                    newSoilNutrientMegModel.QuickP = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["QuickP_SoilNutrient"]) ? "0" : context.Request["QuickP_SoilNutrient"]);
                    newSoilNutrientMegModel.QUicK = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["QuickK_SoilNutrient"]) ? "0" : context.Request["QuickK_SoilNutrient"]);
                    newSoilNutrientMegModel.OrganicMatter = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["OrganicMatter_SoilNutrient"]) ? "0" : context.Request["OrganicMatter_SoilNutrient"]);
                    newSoilNutrientMegModel.PH = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["PH_SoilNutrient"]) ? "0" : context.Request["PH_SoilNutrient"]);
                    newSoilNutrientMegModel.All_id = getMaxId;

                    //土壤养分信息录入
                    BLL.SoilNutrientMeg newSoilNutrientMegBll = new BLL.SoilNutrientMeg();

                    int resultNumSoil = newSoilNutrientMegBll.Add(newSoilNutrientMegModel, MyDict);

                    #endregion

                    #region 第三张表  作物信息

                    Model.CropsMeg newCropsMegModel = new Model.CropsMeg();
                    newCropsMegModel.CropType = context.Request["CropType_CropInfo"];
                    newCropsMegModel.Varieties = context.Request["Varieties_CropInfo"];
                    newCropsMegModel.Yield = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Yield_CropInfo"]) ? "0" : context.Request["Yield_CropInfo"]);
                    newCropsMegModel.urea = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["urea_CropInfo"]) ? "0" : context.Request["urea_CropInfo"]);
                    newCropsMegModel.An = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["An_CropInfo"]) ? "0" : context.Request["An_CropInfo"]);
                    newCropsMegModel.K = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["K_CropInfo"]) ? "0" : context.Request["K_CropInfo"]);
                    newCropsMegModel.Organic_manure = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Organic_manure_CropInfo"]) ? "0" : context.Request["Organic_manure_CropInfo"]);
                    newCropsMegModel.Others = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Others_CropInfo"]) ? "0" : context.Request["Others_CropInfo"]);
                    newCropsMegModel.Irrigation_times = Convert.ToInt32(String.IsNullOrEmpty(context.Request["Irrigation_times"]) ? "0" : context.Request["Irrigation_times_CropInfo"]);
                    newCropsMegModel.All_id = getMaxId;

                    //作物信息录入
                    BLL.CropsMeg newCropsMegBll = new BLL.CropsMeg();
                    int resultNumCrops = newCropsMegBll.Add(newCropsMegModel, MyDict);
                    #endregion

                    #region 第四张表  	农田管理建议

                    Model.FarmlandMSug newFarmlandMSugModel = new Model.FarmlandMSug();
                    newFarmlandMSugModel.CropType = context.Request["CropType_ManaSug"];
                    newFarmlandMSugModel.Varieties = context.Request["Varieties_ManaSug"];
                    newFarmlandMSugModel.TargetYield = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["TargetYield_ManaSug"]) ? "0" : context.Request["TargetYield_ManaSug"]);
                    newFarmlandMSugModel.urea = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["urea_ManaSug"]) ? "0" : context.Request["urea_ManaSug"]);
                    newFarmlandMSugModel.An = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["An_ManaSug"]) ? "0" : context.Request["An_ManaSug"]);
                    newFarmlandMSugModel.K = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["K_ManaSug"]) ? "0" : context.Request["K_ManaSug"]);
                    newFarmlandMSugModel.OrganicManure = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Organic_manure_ManaSug"]) ? "0" : context.Request["Organic_manure_ManaSug"]);
                    newFarmlandMSugModel.Others = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Others_ManaSug"]) ? "0" : context.Request["Others_ManaSug"]);
                    newFarmlandMSugModel.IrrigationTimes = Convert.ToInt32(String.IsNullOrEmpty(context.Request["Irrigation_times_ManaSug"]) ? "0" : context.Request["Irrigation_times_ManaSug"]);
                    newFarmlandMSugModel.SowingAmount = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["SowingAmount_ManaSug"]) ? "0" : context.Request["SowingAmount_ManaSug"]);
                    newFarmlandMSugModel.SowingMethod = context.Request["SowingMethod_ManaSug"];
                    newFarmlandMSugModel.WeedControl = context.Request["WeedControl_ManaSug"];
                    newFarmlandMSugModel.PestControl = context.Request["PestControl_ManaSug"];
                    newFarmlandMSugModel.FieldManagement = context.Request["FieldManagement_ManaSug"];
                    newFarmlandMSugModel.Remarks = context.Request["Remarks_ManaSug"];
                    newFarmlandMSugModel.All_id = getMaxId;
                    //录入 农田管理建议
                    BLL.FarmlandMSug newFarmlandMSugBll = new BLL.FarmlandMSug();
                    int resultFLMS = newFarmlandMSugBll.Add(newFarmlandMSugModel, MyDict);
                    #endregion


                    //判断四张表是否都插入
                    if (resultFLMS > 0 && resultNumCrops > 0 && resultNumFLM > 0 && resultNumSoil > 0)
                    {
                        context.Response.Write("ok");
                    }
                    else
                    {
                        context.Response.Write("信息录入失败");
                    }
                }
                catch (Exception E)
                {
                    //如有异常 将异常信息返回
                    context.Response.Write(E.Message);
                    //将信息返回给客户端，停止该页的执行  
                    context.Response.End();
                }
            }



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