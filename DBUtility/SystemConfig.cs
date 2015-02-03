using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace IndustryPlatform.Common
{
    public class SystemConfig
    {
        /// <summary>
        /// ȡ����ǰWebApplication�ĸ�Url
        /// </summary>
        /// <returns>��Http://��ͷ����/��β����ȫ�޶���</returns>
        public static string RootUrl
        {
            get
            {
                return GetRoot();
            }
        }


        /// <summary>
        /// ȡ��ϵͳ��ʼ�������Url
        /// </summary>
        /// <returns>��Http://��ͷ����/��β����ȫ�޶���</returns>
        /// 
        public static string StartUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["StartUrl"].ToString();
            }
        }


   /// <summary>
        /// ȡ��ϵͳ��ʼ�������SysID
        /// </summary>
        /// <returns>��Http://��ͷ����/��β����ȫ�޶���</returns>
        /// 
        public static string SysID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SysID"].ToString();
            }
        }

        public static string ServerPort
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            }
        }

        /// ȡ�ÿͻ�����ʵIP������д�����ȡ��һ����������ַ 
        /// </summary> 
        public static string IPAddress
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //�����д��� 
                    if (result.IndexOf(".") == -1)     //û�С�.���϶��Ƿ�IPv4��ʽ 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //�С�,�������ƶ������ȡ��һ������������IP�� 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];     //�ҵ����������ĵ�ַ 
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //������IP��ʽ 
                            return result;
                        else
                            result = null;     //�����е����� ��IP��ȡIP 
                    }

                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }
        /// �ж��Ƿ���IP��ַ��ʽ 0.0.0.0
        /// </summary>
        /// <param name="str1">���жϵ�IP��ַ</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// ����û��Ƿ��Ѿ�ʧȥ�˻Ự��������
        /// </summary>
        public static bool VertifyOnline()
        {
            HttpContext _context = HttpContext.Current;
            if (_context.Session["IsOnline"] != null) return true;
            return false;
        }

        /// <summary>
        /// ȡ����ǰWebApplication�ĸ�Url
        /// </summary>
        /// <returns>��Http://��ͷ����/��β����ȫ�޶���</returns>
        public static string GetRoot()
        {
            string rs;
            string crmAppPath = "";
            crmAppPath = (HttpContext.Current.Request.ApplicationPath == "/") ? HttpContext.Current.Request.ApplicationPath : HttpContext.Current.Request.ApplicationPath + "/";
            string sPort = SystemConfig.ServerPort;
            if (sPort == "80")
                rs = "Http://" + HttpContext.Current.Request.Url.Host + crmAppPath;
            else
                rs = "Http://" + HttpContext.Current.Request.Url.Host + ":" + sPort + crmAppPath;
            return rs;
        }

        /// <summary>
        /// �ӻ�ȡ������·�����ȡ�ļ���
        /// </summary>
        /// <param name="RequestPath">����·��</param>
        /// <returns>��ȡ���ļ���</returns>
        public static string GetRequestFileName(string RequestPath)
        {
            if (RequestPath == "")
                return string.Empty;
            string rs = "";
            int i = RequestPath.LastIndexOf(@"/");
            if (i != -1)
                rs = RequestPath.Substring(i + 1);
            return rs;
        }

        /// <summary>
        /// �Ƿ�Ϊ����
        /// </summary>
        public static bool IsDateTime(object obj)
        {
            if (obj.Equals(string.Empty)) return false;
            bool rs = false;
            try
            {
                DateTime ii = Convert.ToDateTime(obj);
                rs = true;
            }
            catch (InvalidCastException)
            {
                rs = false;
            }
            return rs;
        }
    }
}
