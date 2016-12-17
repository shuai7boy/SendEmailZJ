using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 发送邮件ZJ.Helpers;

namespace 发送邮件ZJ.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendYanZhengMa(string recEmail)
        {         
            
            //调用模板文件 并进行占位符替换
            string templetpath = Server.MapPath("../mailtemplate/irupoint.txt");
            NameValueCollection myCol = new NameValueCollection();
            myCol.Add("ename", "一明");
            myCol.Add("from", "地狱之门");
            myCol.Add("link", "http://shuai7boy.cn/");
            string mailBody = TemplateHelper.BulidByFile(templetpath, myCol);
            string result= MailHelper.SendNetMail(recEmail, mailBody, true);        
            return Content(result);
        }
    }
}
