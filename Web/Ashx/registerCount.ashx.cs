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
            BLL.UserInfo UserInfoService = new BLL.UserInfo();

            context.Response.ContentType = "text/plain";
            string userName = context.Request["userName"];
            string pwd = context.Request["password"];
            string confirmPwd = context.Request["password_confirmation"];
            string Guid_Code = context.Request["Guid_Code"];

            if (UserInfoService.Exists(Guid_Code) == false)
            {
                context.Response.Write("邀请码不正确,请QQ联系 1443742816 , 2101300125 获取邀请码！");
                context.Response.End();
            }
            else if (pwd != confirmPwd)
            {
                context.Response.Write("输入的两次密码不一致！");
                context.Response.End();
            }
            else if (pwd == "" || userName == "")
            {
                context.Response.Write("账号或密码不能为空！");
                context.Response.End();
            }
            else
            {
                Model.UserInfo UserinfoModal = new Model.UserInfo();
                UserinfoModal.UserId = userName;
                UserinfoModal.UserPassword = pwd;
                if (UserInfoService.Add(UserinfoModal) == 0 || UserInfoService.Delete(Guid_Code))
                {
                    context.Response.Write("ok");
                }

                else
                {
                    context.Response.Write("注册失败!");
                }
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