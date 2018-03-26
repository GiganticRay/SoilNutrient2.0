using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace SoilNutrient.Web.Ashx
{
    /// <summary>
    /// UploadImgs 的摘要说明
    /// </summary>
    public class UploadImgs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string inputStr = context.Request["postData"];
            //将base64分割
            string[] a = inputStr.Split(',');
            //获取文件后缀名
            string ext = "." + a[0].Substring(a[0].IndexOf("/") + 1, a[0].IndexOf(";") - a[0].IndexOf("/") - 1);
            // 检查是否是图片  
            //*.jpg;*.png;*.jpeg;*.gif
            if (!(ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif"))
            {
                context.Response.Write("不是图片");
                context.Response.End();
            }

            //将base64转换成byte数组
            byte[] bytes = Convert.FromBase64String(a[1]);
            //将byte数组转换成Image对象
            if (bytes != null)
            {
                MemoryStream ms = new MemoryStream(bytes);
                Image myImage = Image.FromStream(ms);
                //文件相对路径           
                string savePath = "/UpImages/" + Guid.NewGuid().ToString() + ext;
                myImage.Save(context.Request.MapPath(savePath));
                context.Response.Write(savePath);
                context.Response.End();
            }
            else
            {
                context.Response.Write("转换图片失败");
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