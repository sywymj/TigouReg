using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing;
using System.Threading;
using System.Security.Cryptography;
using System.Net;
using System.Data;
using System.IO;

namespace TigouRegApp
{
    public class UserInfor
    {
        public string Account;
        public string PassWord;
        public string Hr;
    }

    public class TigouLib:verifyCodeBaseClass
    {
        public static Encoding pageCode = Encoding.Default;
        public static Random myRand = new Random();
        public static int verifyCodeRetryMaxCount = 3;
        public static MD5 md = MD5.Create();
        public static bool isCancel = false;
        public static int cdsIndex = 0;
        public static string tip = "各位破解的老师，请高抬贵手，放小弟一马，小弟这厢有礼了。如果别人付费破解，请联系我。我给你支付一定的费用。如果想了解源码，请联系我，我可以和你交换代码。请联系QQ：4284607。谢谢啦";

        public static event Func<Image, string> OnVeifyImageOk;

        private static string GetTimeStamp()
        {
            double timestamp = (DateTime.Now.AddHours(-8) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            return ((long)timestamp).ToString();
        }

        HttpRequest.HttpRequest tgRequest = new HttpRequest.HttpRequest();
        public string RegAccount(string account, string password, string email, string psn)
        {
            string urlString = string.Empty;
            string refString = string.Empty;
            string htmlString = string.Empty;
            string postData = string.Empty;
            string hrString = string.Empty;
            string pattern = string.Empty;

            try
            {
                urlString = string.Format(@"http://www.tigou.net/register.aspx");
                refString = urlString;
                tgRequest.OpenRequest(urlString, refString);

                urlString = string.Format(@"http://www.tigou.net/chkusername.aspx?user={0}&t={1}",HttpUtility.UrlEncode(account,pageCode),
                    GetTimeStamp());
                tgRequest.OpenRequest(urlString, refString);
                htmlString = tgRequest.HtmlDocument;
                if (!htmlString.Contains("该帐户名可以使用"))
                {
                    throw new Exception("Already Register");
                }


                urlString = string.Format(@"http://www.tigou.net/verifycode2.aspx?temp={0}", GetTimeStamp());
                tgRequest.OpenRequest(urlString, refString);
                if (tgRequest.image==null)
                {
                    throw new Exception("get verify image error!");
                }
                if (OnVeifyImageOk!=null)
                {
                    this.m_verifyCode = OnVeifyImageOk(tgRequest.image);
                }
                else
                {
                    AutoResetEvent verifyEvent = new AutoResetEvent(false);
                    MTVerifyCodeClass.AddVerifyImage(verifyEvent, tgRequest.image, this);
                    verifyEvent.WaitOne();

                    verifyEvent.Close();
                }

                


                urlString = string.Format(@"http://www.tigou.net/reg.aspx");
                postData = string.Format(@"LoginName={0}&PassWord={1}&rePassWord={1}&Email={2}&CardID={3}&VerifyCode={4}&RecommendID=-1&URL_ref=",
                    HttpUtility.UrlEncode(account,pageCode),
                    HttpUtility.UrlEncode(password,pageCode),
                    HttpUtility.UrlEncode(email,pageCode),
                    psn,this.m_verifyCode
                    );
                tgRequest.OpenRequest(urlString, refString, postData);
                htmlString = tgRequest.HtmlDocument;
                if (!htmlString.Contains("注册成功"))
                {
                    throw new Exception(htmlString);
                }

            }
            catch (System.Exception ex)
            {
                hrString = ex.Message;
            }
            return hrString;
        }
    }
}
