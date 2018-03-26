using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// registerCount 的摘要说明
    /// </summary>
    public class registerCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("alert('目暂时不支持注册功能')");
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