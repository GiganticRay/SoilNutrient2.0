using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// LoadProvince 的摘要说明
    /// </summary>
    public class LoadProvince : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //string text = "[{\"Name\":\"leichao\",\"Age\":\"20\"},";
            //text += "{\"Name\":\"Ray\",\"Age\":\"19\"}]";
            //context.Response.Write(text);
            BLL.Areas AreasServer = new BLL.Areas();
            //select Id,AreaID,AreaName,AreaPID
            List<Model.Areas> ModelListAreas = AreasServer.GetModelList("AreaPID=0");
            JavaScriptSerializer myjsSerializer = new JavaScriptSerializer();
            var responseJson = myjsSerializer.Serialize(ModelListAreas);


            context.Response.Write(responseJson);
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