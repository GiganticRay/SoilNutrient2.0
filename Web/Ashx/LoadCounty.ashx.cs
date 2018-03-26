using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// LoadCounty 的摘要说明
    /// </summary>
    public class LoadCounty : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BLL.Areas AreaAreasServer = new BLL.Areas();
            List<Model.Areas> ModelListAreas = AreaAreasServer.GetModelList(string.Format("AreaPID={0}", context.Request["PID"]));

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