using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace 发送邮件ZJ.Helpers
{
    /// <summary>
    /// 发送邮件帮助类
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recEmail">收件地址</param>
        /// <param name="mailBody">发送内容：这里可以传递过来普通内容或模板内容</param>
        ///  <param name="IsBodyHtml">设置是否以html格式发送</param>
        /// <returns></returns>
        public static string SendNetMail(string recEmail, string mailBody, bool IsBodyHtml)
        {
            string result = "no";//返回结果
            string sendFrom = ConfigurationManager.AppSettings["sendFrom"]; //生成一个发送地址
            string sendUserName = ConfigurationManager.AppSettings["sendUserName"];//发送人的名字          
            string recUserName = ConfigurationManager.AppSettings["recUserName"];//收件人名字
            string sendTitle = ConfigurationManager.AppSettings["sendTitle"];//发送邮件标题
            string username = ConfigurationManager.AppSettings["username"];//发送邮箱用户名
            string passwd = ConfigurationManager.AppSettings["passwd"];//发送邮箱密码
            try { 
            //确定smtp服务器地址。实例化一个Smtp客户端
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.yeah.net");
           
          
            //构造一个发件人地址对象
            MailAddress from = new MailAddress(sendFrom, sendUserName, Encoding.UTF8);//发送地址，发送人的名字
            //构造一个收件人地址对象
            MailAddress to = new MailAddress(recEmail,recUserName, Encoding.UTF8);//收件地址，收件人的名字
            //构造一个Email的Message对象
            MailMessage message = new MailMessage(from, to);
            //添加邮件主题和内容
            message.Subject = sendTitle;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            //设置邮件的信息
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            message.IsBodyHtml = IsBodyHtml;//设置是否为html格式的值
            //如果服务器支持安全连接，则将安全连接设为true。
            //gmail支持，163不支持，如果是gmail则一定要将其设为true               
            client.EnableSsl = true;
            //设置用户名和密码。             
            client.UseDefaultCredentials = false;
            //用户登陆信息
            NetworkCredential myCredentials = new NetworkCredential(username, passwd);
            client.Credentials = myCredentials;
            //发送邮件
            client.Send(message);
            //提示发送成功
            result = "ok";
            }
            catch
            {
                result = "no";
            }
            return result;
        }
    }
}