using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilNutrientSoft.Web.Ashx
{
    /// <summary>
    /// VerifyIogin 的摘要说明
    /// </summary>
    public class VerifyIogin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userName = context.Request["userName"];
            string pwd = context.Request["password"];

            
            string logName = "cuit123";
            string logPwd = "cuit123";

            if (userName == logName && pwd == logPwd)
            {
                context.Response.Write("ok");
                context.Response.End();
            }
            else if (userName != logName && pwd != logPwd)
            {
                context.Response.Write("用户名及密码错误");
                context.Response.End();
            }
            else if (userName == logName && pwd != logPwd)
            {
                context.Response.Write("密码错误");
                context.Response.End();
            }
            else if (userName != logName && pwd == logPwd)
            {
                context.Response.Write("用户名错误");
                context.Response.End();
            }
            else
            {
                context.Response.Write("登录失败");
                context.Response.End();
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