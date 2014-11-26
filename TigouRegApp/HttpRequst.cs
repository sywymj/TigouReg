
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using ICSharpCode.SharpZipLib.GZip;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;

namespace HttpRequest
{
    /*
    * ������ƣ�����̫��
    * E-mail��stjzp@21cn.com
    * QQ:548317
    * */

    /// <summary>
    /// HTTP/HTTPS��Դ������
    /// </summary>
    public class HttpRequest
    {
        // ����CookieΪͬһCookieֵ��        
        public static System.Threading.ManualResetEvent AdslDialerEvent = new System.Threading.ManualResetEvent(true);



        public CookieContainer craboCookie = new CookieContainer();
        public CookieCollection responseCookies = null;
        protected string __Uri__ = null;         // ��ʶ Internet ��Դ�� URI

        protected string __Referer__ = null;         // ��ʶ Internet ��Դ����� Referer        

        protected string __Headers__ = null;         // ��ʶ Internet ��Դ����� Header

        // ��ʶ Internet ��Դ����� Accept
        protected string __Accept__ = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        //protected string __Accept__ = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";


        protected string __Method__ = null;         // ��ʶ Internet ��Դ����� Method

        protected string __Data__ = null;         // POST����ʱ������

        protected string __CharacterSet__ = null;         // ��Ӧ���ַ���

        protected HttpStatusCode __StatusCode__;         // ��Ӧ״̬

        protected StringBuilder __Html_Text__ = new StringBuilder();

        /// <summary>
        /// ////////////////////////////////////////
        /// </summary>
        protected MemoryStream __MemStream__ = null;
        protected string __Boundary__ = null;

        public Image image = null;
        public string responseUrl = string.Empty;

        protected WebProxy __webProxy = null;

        public WebProxy webProxy
        {
            get { return __webProxy; }
            set { this.__webProxy = value; }
        }


        public HttpRequest()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            __CharacterSet__ = "GB2312";
        }

        /// <summary>
        /// �� Internet ��Դ����GET����
        /// </summary>
        /// <param name="requestUriString">��ʶ Internet ��Դ�� URI</param>
        /// <param name="requestReferer">��ʶ Internet ��Դ����� Referer</param>
        public bool OpenRequest(string requestUriString, string requestReferer)
        {
            __Uri__ = requestUriString;
            __Referer__ = requestReferer;
            __Method__ = "GET";
            return OpenRequest();
        }

        /// <summary>
        /// �� Internet ��Դ����GET����
        /// </summary>
        /// <param name="requestUriString">��ʶ Internet ��Դ�� URI</param>
        /// <param name="requestReferer">��ʶ Internet ��Դ����� Referer</param>
        /// <param name="requestMethod">��ʶ Internet ��Դ����� Post ����</param>
        public bool OpenRequest(string requestUriString, string requestReferer, string requestData)
        {
            __Uri__ = requestUriString;
            __Referer__ = requestReferer;
            __Data__ = requestData;
            __Method__ = "POST";

            return OpenRequest();
        }


        public bool OpenRequest(string requestUriString, string requestReferer, MemoryStream memStream,string boundary)
        {
            __Uri__ = requestUriString;
            __Referer__ = requestReferer;
            __MemStream__ = memStream;
            __Method__ = "POST";
            __Boundary__ = boundary;
            
            return OpenRequest();
        }


        /// <summary>
        /// �� Internet ��Դ��������
        /// </summary>
        /// <returns></returns>
        private bool OpenRequest()
        {
            // ����ѱ�������
            __Html_Text__.Remove(0, __Html_Text__.Length);

            // ���� HttpWebRequest ʵ��
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(__Uri__);


            //���ô���
            if (this.webProxy!=null)
            {
                Request.Proxy = this.webProxy;
            }

            // ���ø����ض���
            Request.AllowAutoRedirect = true;

            #region �ж�Uri��Դ����
            {
                Regex __RegexUri_ = new Regex("^https://", RegexOptions.IgnoreCase);
                if (__RegexUri_.IsMatch(__Uri__))
                    ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            }
            #endregion

            Request.Timeout =200000;
            Request.Method = __Method__;
            Request.Accept = __Accept__;
            Request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; Maxthon; .NET CLR 1.1.4322); Http STdns";
            //Request.UserAgent = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16";
            
            
            Request.CookieContainer = craboCookie;
            Request.Referer = __Referer__;
            Request.Method = __Method__;

            //Request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate";
            
            
            if (__Method__ == "POST")
            {
                
                if (__MemStream__==null)
                {
                    #region ����ΪPOST
                    Request.ContentType = "application/x-www-form-urlencoded";
                    byte[] Bytes = Encoding.GetEncoding(CharacterSet).GetBytes(__Data__);
                    Request.ContentLength = Bytes.Length;
                    using (Stream writer = Request.GetRequestStream())
                    {
                        writer.Write(Bytes, 0, Bytes.Length);
                        writer.Close();
                    }
                    #endregion
                }
                else
                {
                    Request.ContentType = "multipart/form-data; boundary=" + __Boundary__;
                    Request.ContentLength = __MemStream__.Length;
                    using (Stream writer = Request.GetRequestStream())
                    {
                        __MemStream__.WriteTo(writer);
                        writer.Close();
                        __MemStream__.Close();
                        __MemStream__ = null;
                    }
                }


            }


            AdslDialerEvent.WaitOne();

            #region ��ȡ��������
            // ���÷���ֵ����
            bool bResult = true;

            this.image = null;
            try
            {
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                

                // ��ȡ״ֵ̬
                __StatusCode__ = Response.StatusCode;

                if (__StatusCode__ == HttpStatusCode.OK)
                {
                    // �ж�ҳ�����
                    string ContentEncoding = Response.ContentEncoding.ToLower();
                    responseUrl = Response.ResponseUri.ToString();

                    responseCookies = Response.Cookies;


                    switch (ContentEncoding)
                    {
                        case "gzip":
                            using (Stream reader = new GZipInputStream(Response.GetResponseStream()))
                            {
                                MemoryStream ms = new MemoryStream();
                                int nSize = 4096;
                                byte[] writeData = new byte[nSize];
                                while (true)
                                {
                                    try
                                    {
                                        nSize = reader.Read(writeData, 0, nSize);
                                        if (nSize > 0)
                                            ms.Write(writeData, 0, nSize);
                                        else
                                            break;
                                    }
                                    catch (GZipException)
                                    {
                                        break;
                                    }
                                }
                                reader.Close();
                                //__Html_Text__.Append(Encoding.GetEncoding(CharacterSet).GetString(ms.GetBuffer()));
                                __Html_Text__.Append(Encoding.GetEncoding(Response.CharacterSet).GetString(ms.GetBuffer()));
                                //__Html_Text__.Append(Encoding.Default.GetString(ms.GetBuffer()));
                            }
                            break;


                        default:



                            using (Stream reader = Response.GetResponseStream())
                            {

                                if (Response.ContentType.Contains("image"))
                                {
                                    image = Image.FromStream(reader);
                                }
                                else
                                {
                                    MemoryStream memory = new MemoryStream();
                                    int byteValue = -1;
                                    while ((byteValue = reader.ReadByte()) != -1)
                                    {
                                        memory.WriteByte((byte)byteValue);
                                    }
                                    byte[] myBuffer = memory.ToArray();
                                    string myCharset = GetCharSet(myBuffer);
                                    Encoding myEncode = Encoding.GetEncoding(myCharset);
                                    __Html_Text__.Append(myEncode.GetString(myBuffer));

                                    //StreamReader sr = new StreamReader(reader, Encoding.Default);

                                    //__Html_Text__.Append(sr.ReadToEnd());
                                    //sr.Close();

                                }
                                reader.Close();

                            }
                            break;
                    }
                }
                else
                {
                    bResult = false;
                    responseUrl = string.Empty;
                }
            }
            catch (Exception pEx)
            {
                __Html_Text__.Append(pEx.Message);
                return false;
            }

            #endregion

            return bResult;
        }
        private string GetCharSet(byte[] pBuffer)
        {
            Encoding encode = Encoding.ASCII;
            String _htmlTemp=encode.GetString(pBuffer);
            Match m = Regex.Match(_htmlTemp, @"<meta[\s\S]+?content=\s*""[\s\S]*?charset=(?<charset>\S+)\s*""",RegexOptions.IgnoreCase);
            if (m.Success)
            {
                if (m.Groups["charset"].Value.Length>3)
                {
                    //return "gb2312";
                    return "utf-8";
                }
                return m.Groups["charset"].Value;
            } 
            else
            {
                return "gb2312";
                //return "utf-8";
            }
        
        }
        /// <summary>
        /// ��ȡ������ Uri ��Դ�ַ���
        /// </summary>
        public string CharacterSet
        {
            get
            {
                return __CharacterSet__;
            }
            set
            {
                __CharacterSet__ = value;
            }
        }

        /// <summary>
        /// ��ȡ������ Uri ��Դ��ʶ
        /// </summary>
        public string RequestUriString
        {
            get
            {
                return __Uri__;
            }
            set
            {
                __Uri__ = value;
            }
        }

        /// <summary>
        /// ��ȡ������ Uri ��Դ���� Accept
        /// </summary>
        public string Accept
        {
            get
            {
                return __Accept__;
            }
            set
            {
                __Accept__ = value;
            }
        }

        public string HtmlDocument
        {
            get
            {
                return __Html_Text__.ToString();
            }
        }
    }

    /// <summary>
    /// �� Uri ��ԴΪ HTTPS ʱ������֤�顣
    /// </summary>
    public class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public bool CheckValidationResult(ServicePoint _ServicePoint_, X509Certificate _Cert_, WebRequest _WebRequest_, int _Problem_)
        {
            return true;
        }
    }
}
 


