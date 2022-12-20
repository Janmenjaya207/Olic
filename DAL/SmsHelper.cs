using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace DAL
{
    public static class SmsHelper
    {
        public const string UserId = "1048";
        public const string Secretcode = "4ZDY2dGpNWvkjeui";
        public const string Sender = "OTBIMS";
        public const string REQ_ID = "1";
        public const string FORMAT = "text";
        public const string ROUTE_ID = "3";
        public const string URL_ID = "";
        public const string UNIQUE = "0";
        public const string FLASH = "0";

        public static bool SendSms(string mobile, string sms)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://apps.sandeshlive.com/API/WebSMS/Http/v1.0a/index.php?");
            sb.Append("userid=" + UserId + "&password=4ZDY2dGpNWvkjeui&sender=" + Sender);
            sb.Append("&to=" + mobile + "&message=" + HttpUtility.UrlEncode(sms));
            sb.Append("&reqid=" + REQ_ID + "&format=" + FORMAT + "&route_id=" + ROUTE_ID);
            sb.Append("&url_id=" + URL_ID + "&unique=" + UNIQUE + "&sendondate=" + DateTime.Now.ToString() + "&flash=" + FLASH);

            string sendSMSUri = sb.ToString();
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sendSMSUri);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseString = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}