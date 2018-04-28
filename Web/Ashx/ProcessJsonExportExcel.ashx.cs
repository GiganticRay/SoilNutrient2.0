using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// ProcessJsonExportExcel 的摘要说明
    /// </summary>
    public class ProcessJsonExportExcel : IHttpHandler
    {
        //返回的列表
        private List<SoilAllInfo> ListSoilAllInfo = new List<SoilAllInfo>();

        public void ProcessRequest(HttpContext context)
        {
            


            context.Response.ContentType = "text/plain";

            //省Num
            int ProvinceNum = Convert.ToInt32(context.Request["ProvinceDropdown"]);
            //县Num
            int CountryNum = Convert.ToInt32(context.Request["CountryDropdown"]);
            //
            int NutrientDivide = Convert.ToInt32(context.Request["NutrientDivideDropdown"]);

            BLL.FarmlandMeg newFarmlandMegBll = new BLL.FarmlandMeg();
            List<Model.FarmlandMeg> newFarmlandMegModel = new List<Model.FarmlandMeg>();


            //获取到符合条件的集合并返回
            GetListSoilAllInfo(ProvinceNum, CountryNum);
            //GetListSoilAllInfo(21, 198);


            //创建序列化对象
            JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
            //转换成JSON字符串
            var dataStr = JavaScriptSerializer.Serialize(ListSoilAllInfo);

            context.Response.Write(dataStr);
        }

        public List<SoilAllInfo> GetListSoilAllInfo(int ProvinceNum, int CountryNum)
        {
            string strwhere = "";
            //查询到满足条件的集合
            if (ProvinceNum == -1 && CountryNum == -1)
            {
                //表示所有都全选
                strwhere = "1 > 0";
            }
            else if (ProvinceNum >= 0 && CountryNum == -1)
            {
                //表示只有“区县”全选
                strwhere = "City = " + ProvinceNum.ToString();
            }
            else
            {
                strwhere = "City = " + ProvinceNum.ToString() + " and County = " + CountryNum.ToString();
                //测试为全选
            }

            //农田基本数据对象
            SoilNutrientSoft.BLL.FarmlandMeg newFarmlandMegBll = new SoilNutrientSoft.BLL.FarmlandMeg();
            List<Model.FarmlandMeg> TmpFarmlanMeg = newFarmlandMegBll.GetModelList(strwhere);
            //作物数据
            SoilNutrientSoft.BLL.CropsMeg newCropsMegBll = new SoilNutrientSoft.BLL.CropsMeg();
            //农田建议
            SoilNutrientSoft.BLL.FarmlandMSug newFarmlandMSugBll = new SoilNutrientSoft.BLL.FarmlandMSug();
            //土壤养分信息    
            SoilNutrientSoft.BLL.SoilNutrientMeg newSoilNutrientMegBll = new SoilNutrientSoft.BLL.SoilNutrientMeg();

            //通过查询出来的id来获取其他三张表的信息
            foreach (var tmp in TmpFarmlanMeg)
            {
                var tmpInfo = new SoilAllInfo();

                tmpInfo.CropsMegCity = tmp.City;
                tmpInfo.CropsMegCounty = tmp.County;
                tmpInfo.CropsMegTown = tmp.Town;
                tmpInfo.CropsMegVillage = tmp.Village;
                tmpInfo.CropsMegSampleName = tmp.Sample_name;
                tmpInfo.CropsMegLon = tmp.Lon;
                tmpInfo.CropsMegLat = tmp.Lat;
                tmpInfo.CropsMegNameOfHouseholder = tmp.Name_of_householder;
                tmpInfo.CropsMegPhoneNumber = tmp.Phone_number;
                tmpInfo.CropsMegIrrigationConditions = tmp.Irrigation_Conditions;
                tmpInfo.CropsMegAcreage = tmp.Acreage;
                tmpInfo.CropsMegFertility = tmp.Fertility;
                tmpInfo.CropsMegWeeds = tmp.Weeds;

                //根据id分别获取  作物数据    农田建议    土壤养分信息

                Model.CropsMeg tmpCropsMegs = (newCropsMegBll.GetModelList("id=" + tmp.Id.ToString()))[0];
                tmpInfo.CropsMegCropType = tmpCropsMegs.CropType;
                tmpInfo.CropMegVarieties = tmpCropsMegs.Varieties;
                tmpInfo.CropMegYield = tmpCropsMegs.Yield;
                tmpInfo.CropMegUrea = tmpCropsMegs.urea;
                tmpInfo.CropMegAn = tmpCropsMegs.An;
                tmpInfo.CropMegK = tmpCropsMegs.K;
                tmpInfo.CropMegOrganicManure = tmpCropsMegs.Organic_manure;
                tmpInfo.CropMegOthers = tmpCropsMegs.Others;
                tmpInfo.CropMegIrrigationTimes = tmpCropsMegs.Irrigation_times;

                Model.FarmlandMSug tmpFarmlandMSug = (newFarmlandMSugBll.GetModelList("id=" + tmp.Id.ToString()))[0];
                tmpInfo.MSugCropType = tmpFarmlandMSug.CropType;
                tmpInfo.MSugVarieties = tmpFarmlandMSug.Varieties;
                tmpInfo.MSugTargetYield = tmpFarmlandMSug.TargetYield;
                tmpInfo.MSugUrea = tmpFarmlandMSug.urea;
                tmpInfo.MSugAn = tmpFarmlandMSug.An;
                tmpInfo.MSugK = tmpFarmlandMSug.K;
                tmpInfo.MSugOrganicManure = tmpFarmlandMSug.OrganicManure;
                tmpInfo.MSugOthers = tmpFarmlandMSug.Others;
                tmpInfo.MSugIrrigationTimes = tmpFarmlandMSug.IrrigationTimes;
                tmpInfo.MSugSowingAmout = tmpFarmlandMSug.SowingAmount;
                tmpInfo.MSugSowingMethod = tmpFarmlandMSug.SowingMethod;
                tmpInfo.MSugWeedControl = tmpFarmlandMSug.WeedControl;
                tmpInfo.MSugFieldManagement = tmpFarmlandMSug.FieldManagement;
                tmpInfo.MSugRemarkers = tmpFarmlandMSug.Remarks;

                Model.SoilNutrientMeg tmpSoilNutrientMeg = (newSoilNutrientMegBll.GetModelList("id=" + tmp.Id.ToString()))[0];
                tmpInfo.SoilNutrientN = tmpSoilNutrientMeg.N;
                tmpInfo.SoilNutrientP = tmpSoilNutrientMeg.P;
                tmpInfo.SoilNutrientK = tmpSoilNutrientMeg.K;
                tmpInfo.SoilNutrientHydrolyticN = tmpSoilNutrientMeg.HydrolyticN;
                tmpInfo.SoilNutrientQuickP = tmpSoilNutrientMeg.QUicK;
                tmpInfo.SoilNutrientQuickK = tmpSoilNutrientMeg.QUicK;
                tmpInfo.SoilNutrientOrganicMatter = tmpSoilNutrientMeg.OrganicMatter;
                tmpInfo.SoilNutrientPh = tmpSoilNutrientMeg.PH;

                ListSoilAllInfo.Add(tmpInfo);

            }
            return ListSoilAllInfo;
        }

        //内部类
        public class SoilAllInfo
        {
            //先来四个属性说明是哪一个
            public string FarmInfo = "农田信息:";
            public string SoilNutrientInfo = "土壤养分信息:";
            public string CropData = "作物数据:";
            public string FarmSug = "作物类型";
   

            //'农田信息',
            //'市级', '县级','乡/镇','村','采样点名称','经度','纬度','户主姓名','联系电话','灌溉条件','土地面积','土地肥力','杂草情况',
            public string CropsMegCity = "";
            public string CropsMegCounty = "";
            public string CropsMegTown = "";
            public string CropsMegVillage = "";
            public string CropsMegSampleName = "";
            public decimal? CropsMegLon = 0;
            public decimal? CropsMegLat = 0;
            public string CropsMegNameOfHouseholder = "";
            public string CropsMegPhoneNumber = "";
            public int? CropsMegIrrigationConditions = 0;
            public decimal? CropsMegAcreage = 0;
            public int? CropsMegFertility = 0;
            public string CropsMegWeeds = "";

            //'土壤养分信息',
            //'氮(mg/kg)', '磷(mg/kg)', '钾(mg/kg)', '水解性氮(mg/kg)', '速效磷(mg/kg)', '速效钾(mg/kg)', '有机质(mg/kg)', 'PH',
            public decimal? SoilNutrientN = 0;
            public decimal? SoilNutrientP = 0;
            public decimal? SoilNutrientK = 0;
            public decimal? SoilNutrientHydrolyticN = 0;
            public decimal? SoilNutrientQuickP = 0;
            public decimal? SoilNutrientQuickK = 0;
            public decimal? SoilNutrientOrganicMatter = 0;
            public decimal? SoilNutrientPh = 0;


            //'作物数据',
            //'作物类型', '品种', '产量(kg/亩)', '尿素(kg/亩)', '二胺(kg/亩)', '钾肥(kg/亩)', '有机肥(kg/亩)', '其他(kg/亩)', '灌溉次数(次)',
            public string CropsMegCropType = "";
            public string CropMegVarieties = "";
            public decimal? CropMegYield = 0;
            public decimal? CropMegUrea = 0;
            public decimal? CropMegAn = 0;
            public decimal? CropMegK = 0;
            public decimal? CropMegOrganicManure = 0;
            public decimal? CropMegOthers = 0;
            public int? CropMegIrrigationTimes = 0;

            //'农田建议',
            //'作物类型', '品种', '目标常量(kg/亩)', '尿素(kg/亩)', '二胺(kg/亩)', '钾肥(kg/亩)', '有机肥(kg/亩)', '其他(kg/亩)', '灌溉次数(次)', '播种量(kg/亩)', '播种方式', '杂草控制', '病虫害控制', '田间管理', '备注']
            public string MSugCropType = "";
            public string MSugVarieties = "";
            public decimal? MSugTargetYield = 0;
            public decimal? MSugUrea = 0;
            public decimal? MSugAn = 0;
            public decimal? MSugK = 0;
            public decimal? MSugOrganicManure = 0;
            public decimal? MSugOthers = 0;
            public decimal? MSugIrrigationTimes = 0;
            public decimal? MSugSowingAmout = 0;
            public string MSugSowingMethod = "";
            public string MSugWeedControl = "";
            public string MSugPestControl = "";
            public string MSugFieldManagement = "";
            public string MSugRemarkers = "";

        }


        public bool IsReusable
        {
            get { return false; }
        }
    }
}