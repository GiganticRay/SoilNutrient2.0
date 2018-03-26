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

            //从数据库读取用户密码验证
            BLL.UserInfo UserInfoObject = new BLL.UserInfo();


            if (UserInfoObject.Exists(userName, pwd) == true)
            {
                context.Response.Write("ok");
                context.Response.End();
            }
            else
            {
                context.Response.Write("登陆失败，请检查用户名或密码");
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