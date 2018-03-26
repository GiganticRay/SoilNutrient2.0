using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SoilNutrient.Web.Ashx
{
    /// <summary>
    /// ProcessUpdateAllData 的摘要说明
    /// </summary>
    public class ProcessUpdateAllData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Dictionary<SqlConnection, SqlTransaction> MyDict = new Dictionary<SqlConnection, SqlTransaction>();
            try
            {
                

                //获取当前数据在数据库中对应的id
                int FarmlandMegId = Convert.ToInt32(context.Request["hiddenID"]);

                //根据“农田信息”表中的id分别查出其他三个表中的id

                //土壤养分信息录入表
                SoilNutrientSoft.BLL.SoilNutrientMeg newSoilNutrientMegBll = new SoilNutrientSoft.BLL.SoilNutrientMeg();
                List<SoilNutrientSoft.Model.SoilNutrientMeg> newSoilNutrientMegList = newSoilNutrientMegBll.GetModelList(" All_id = " + FarmlandMegId);
                //获取其在土壤养分信息录入表中对应的id
                int SoilNutrientMegId = newSoilNutrientMegList[0].Id;


                //作物信息录入表
                SoilNutrientSoft.BLL.CropsMeg newCropsMegBll = new SoilNutrientSoft.BLL.CropsMeg();
                List<SoilNutrientSoft.Model.CropsMeg> newCropsMegList = newCropsMegBll.GetModelList(" All_id = " + FarmlandMegId);
                //获取其在作物信息录入表中对应的id
                int CropsMegId = newCropsMegList[0].Id;


                //农田管理建议表
                SoilNutrientSoft.BLL.FarmlandMSug newFarmlandMSugBll = new SoilNutrientSoft.BLL.FarmlandMSug();
                List<SoilNutrientSoft.Model.FarmlandMSug> newFarmlandMSugList = newFarmlandMSugBll.GetModelList(" All_id = " + FarmlandMegId);
                //获取其在农田管理建议表中对应的id
                int FarmlandMSugId = newFarmlandMSugList[0].Id;


                #region 第一张表单  农田信息

                SoilNutrientSoft.Model.FarmlandMeg newFarmlandMegModel = new SoilNutrientSoft.Model.FarmlandMeg();

                newFarmlandMegModel.Id = FarmlandMegId;
                newFarmlandMegModel.City = context.Request["city"];
                newFarmlandMegModel.County = context.Request["country"];
                newFarmlandMegModel.Town = context.Request["townName"];
                newFarmlandMegModel.Village = context.Request["villageName"];
                newFarmlandMegModel.Sample_name = context.Request["SampleName"];
                newFarmlandMegModel.Lon = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LongitudeDegree"]) ? "0" : context.Request["LongitudeDegree"]);
                newFarmlandMegModel.Lat = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["LatitudeDegree"]) ? "0" : context.Request["LatitudeDegree"]);
                newFarmlandMegModel.Name_of_householder = context.Request["NameOfHouseholder"];
                newFarmlandMegModel.Phone_number = context.Request["PhoneNumber"];
                newFarmlandMegModel.Irrigation_Conditions = Convert.ToInt32(String.IsNullOrEmpty(context.Request["IrrigationConditions"]) ? "1" : context.Request["IrrigationConditions"]);
                newFarmlandMegModel.Acreage = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Acreage"]) ? "0" : context.Request["Acreage"]);
                newFarmlandMegModel.Fertility = Convert.ToInt32(String.IsNullOrEmpty(context.Request["Fertility"]) ? "1" : context.Request["Fertility"]);
                newFarmlandMegModel.Weeds = context.Request["Weeds"];

                SoilNutrientSoft.BLL.FarmlandMeg newFarmlandMegBLL = new SoilNutrientSoft.BLL.FarmlandMeg();

                //更新数据，返回bool类型的值
                bool resultNumFLM = newFarmlandMegBLL.Update(newFarmlandMegModel, MyDict);

                #endregion

                #region 第二张表单 土壤养分信息

                SoilNutrientSoft.Model.SoilNutrientMeg newSoilNutrientMegModel = new SoilNutrientSoft.Model.SoilNutrientMeg();
                newSoilNutrientMegModel.Id = SoilNutrientMegId;
                newSoilNutrientMegModel.N = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["N_SoilNutrient"]) ? "0" : context.Request["N_SoilNutrient"]);
                newSoilNutrientMegModel.P = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["P_SoilNutrient"]) ? "0" : context.Request["P_SoilNutrient"]);
                newSoilNutrientMegModel.K = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["K_SoilNutrient"]) ? "0" : context.Request["K_SoilNutrient"]);
                newSoilNutrientMegModel.HydrolyticN = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["HydrolyticN_SoilNutrient"]) ? "0" : context.Request["HydrolyticN_SoilNutrient"]);
                newSoilNutrientMegModel.QuickP = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["QuickP_SoilNutrient"]) ? "0" : context.Request["QuickP_SoilNutrient"]);
                newSoilNutrientMegModel.QUicK = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["QuickK_SoilNutrient"]) ? "0" : context.Request["QuickK_SoilNutrient"]);
                newSoilNutrientMegModel.OrganicMatter = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["OrganicMatter_SoilNutrient"]) ? "0" : context.Request["OrganicMatter_SoilNutrient"]);
                newSoilNutrientMegModel.PH = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["PH_SoilNutrient"]) ? "0" : context.Request["PH_SoilNutrient"]);
                newSoilNutrientMegModel.All_id = FarmlandMegId;


                //更新数据，返回bool类型的值
                bool resultNumSoil = newSoilNutrientMegBll.Update(newSoilNutrientMegModel, MyDict);

                #endregion

                #region 第三张表  作物信息

                SoilNutrientSoft.Model.CropsMeg newCropsMegModel = new SoilNutrientSoft.Model.CropsMeg();
                newCropsMegModel.Id = CropsMegId;
                newCropsMegModel.CropType = context.Request["CropType_CropInfo"];
                newCropsMegModel.Varieties = context.Request["Varieties_CropInfo"];
                newCropsMegModel.Yield = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Yield_CropInfo"]) ? "0" : context.Request["Yield_CropInfo"]);
                newCropsMegModel.urea = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["urea_CropInfo"]) ? "0" : context.Request["urea_CropInfo"]);
                newCropsMegModel.An = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["An_CropInfo"]) ? "0" : context.Request["An_CropInfo"]);
                newCropsMegModel.K = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["K_CropInfo"]) ? "0" : context.Request["K_CropInfo"]);
                newCropsMegModel.Organic_manure = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Organic_manure_CropInfo"]) ? "0" : context.Request["Organic_manure_CropInfo"]);
                newCropsMegModel.Others = Convert.ToDecimal(String.IsNullOrEmpty(context.Request["Others_CropInfo"]) ? "0" : context.Request["Others_CropInfo"]);
                newCropsMegModel.Irrigation_times = Convert.ToInt32(String.IsNullOrEmpty(context.Request["Irrigation_times"]) ? "0" : context.Request["Irrigation_times_CropInfo"]);
                newCropsMegModel.All_id = FarmlandMegId;

                //更新数据，返回bool类型的值
                bool resultNumCrops = newCropsMegBll.Update(newCropsMegModel, MyDict);


                #endregion

                #region 第四张表  	农田管理建议

                SoilNutrientSoft.Model.FarmlandMSug newFarmlandMSugModel = new SoilNutrientSoft.Model.FarmlandMSug();
                newFarmlandMSugModel.Id = FarmlandMSugId;
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
                newFarmlandMSugModel.All_id = FarmlandMegId;

                //更新数据，返回bool类型的值
                bool resultFLMS = newFarmlandMSugBll.Update(newFarmlandMSugModel, MyDict);
                #endregion

                //判断四张表是否都更新
                if (resultFLMS && resultNumCrops && resultNumFLM && resultNumSoil)
                {
                    context.Response.Write("ok");
                }
                else
                {
                    context.Response.Write("信息录入失败");
                }
                QuitConnTrans(MyDict);
            }
            catch (Exception E)
            {
                ExceptionQuitConnTrans(MyDict);
                context.Response.Write("更新数据时发生异常："+E.Message);
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