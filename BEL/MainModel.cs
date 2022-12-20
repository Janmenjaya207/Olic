using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BEL
{
    public class MainModel
    {
        public static DateTime datetoserver()
        {
            string zoneId = "India Standard Time";
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime result = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            return result;
        }

        public static DateTime convertdateformat(string date)
        {
            try
            {
                string[] a = date.Split('/');
                string day = a[0];
                string month = a[1];
                string year = a[2];
                string fulldate = year + "-" + month + "-" + day;
                DateTime data = Convert.ToDateTime(fulldate);
                return data;
            }
            catch (Exception)
            {
                try
                {
                    string[] a = date.Split('-');
                    string day = a[0];
                    string month = a[1];
                    string year = a[2];
                    string fulldate = year + "-" + month + "-" + day;
                    DateTime data = Convert.ToDateTime(fulldate);
                    return data;
                }
                catch (Exception)
                {
                    try
                    {
                        string[] a = date.Split('-');
                        string day = a[0];
                        string month = a[1];
                        string year = a[2];
                        string fulldate = year + "-" + month + "-" + day;
                        DateTime data = Convert.ToDateTime(fulldate);
                        return data;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            string[] a = date.Split('-');
                            string day = a[0];
                            string month = a[1];
                            string year = a[2];
                            string fulldate = year + "-" + month + "-" + day;
                            DateTime data = Convert.ToDateTime(fulldate);
                            return data;
                        }
                        catch (Exception ex) { throw ex; }

                    }

                }
            }
        }

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string ReturnStrEncryptCode(string id)
        {
            string msg = string.Empty;
            try
            {
                msg = Encrypt(id, "sblw-3hn9-sqoy59");
            }
            catch (Exception)
            {

            }
            return msg;
        }

        public static string ReturnStrDecryptCode(string id)
        {
            string msg = string.Empty;
            try
            {
                string ids = id.Replace(" ", "+");
                msg = Decrypt(ids, "sblw-3hn9-sqoy59");
            }
            catch (Exception)
            {

            }
            return msg;
        }

        //------------------------------------------------------------------Base64 Encode & Decode-------------------------------------------------------------------------------------//
        public static string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}

