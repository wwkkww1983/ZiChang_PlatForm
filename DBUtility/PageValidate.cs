using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace IndustryPlatform.Common
{
	/// <summary>
	/// ҳ������У����
	/// 
	/// 2007.8
	/// </summary>
	public class PageValidate
	{
		private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegLetter = new Regex("^[a-zA-Z]+$");
		private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
		private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
		private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //�ȼ���^[+-]?\d+[.]?\d+$
		private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
		private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        private static Regex RegDate = new Regex("^(\\d{4})(\\/|-)(\\d{1,2})(\\/|-)(\\d{1,2})$");

		public PageValidate()
		{
		}

		#region �����ַ������		
		
		/// <summary>
		/// ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶�����
		/// </summary>
		/// <param name="req">Request</param>
		/// <param name="inputKey">Request�ļ�ֵ</param>
		/// <param name="maxLen">��󳤶�</param>
		/// <returns>����Request��ѯ�ַ���</returns>
		public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
		{
			string retVal = string.Empty;
			if(inputKey != null && inputKey != string.Empty)
			{
				retVal = req.QueryString[inputKey];
				if(null == retVal)
					retVal = req.Form[inputKey];
				if(null != retVal)
				{
					retVal = SqlText(retVal, maxLen);
					if(!IsNumber(retVal))
						retVal = string.Empty;
				}
			}
			if(retVal == null)
				retVal = string.Empty;
			return retVal;
		}
        /// <summary>
        /// �ж��ַ����Ƿ����ָ������
        /// </summary>
        /// <param name="inputData">Ҫ�Ƚϵ��ַ���</param>
        /// <param name="imaxLen">ָ������</param>
        /// <returns></returns>
        public static bool IsLengther(string inputData, int imaxLen)
        {
            bool blFlag = false;

            int iLength = inputData.Length;

            if (iLength > imaxLen)
            {
                blFlag = true;
            }
            else
            {
                blFlag = false;
            }

            return blFlag;
        }
        /// <summary>
        /// �ж��ַ����Ƿ�С��ָ������
        /// </summary>
        /// <param name="inputData">Ҫ�Ƚϵ��ַ���</param>
        /// <param name="imaxLen">ָ������</param>
        /// <returns></returns>
        public static bool IsShorter(string inputData, int iminLen)
        {
            bool blFlag = false;

            int iLength = inputData.Length;

            if (iLength < iminLen)
            {
                blFlag = true;
            }
            else
            {
                blFlag = false;
            }

            return blFlag;
        }
		/// <summary>
		/// �Ƿ������ַ���
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsNumber(string inputData)
		{
			Match m = RegNumber.Match(inputData);
			return m.Success;
		}
        public static bool IsLetter(string inputData)
        {
            Match m = RegLetter.Match(inputData);
            return m.Success;
        }
		/// <summary>
		/// �Ƿ������ַ��� �ɴ�������
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsNumberSign(string inputData)
		{
			Match m = RegNumberSign.Match(inputData);
			return m.Success;
		}		
		/// <summary>
		/// �Ƿ��Ǹ�����
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsDecimal(string inputData)
		{
			Match m = RegDecimal.Match(inputData);
			return m.Success;
		}		
		/// <summary>
		/// �Ƿ��Ǹ����� �ɴ�������
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsDecimalSign(string inputData)
		{
			Match m = RegDecimalSign.Match(inputData);
			return m.Success;
		}		

		#endregion

		#region ���ļ��

		/// <summary>
		/// ����Ƿ��������ַ�
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsHasCHZN(string inputData)
		{
			Match m = RegCHZN.Match(inputData);
			return m.Success;
		}	

		#endregion

		#region �ʼ���ַ
		/// <summary>
		/// �Ƿ��Ǹ����� �ɴ�������
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsEmail(string inputData)
		{
			Match m = RegEmail.Match(inputData);
			return m.Success;
		}		

		#endregion

        #region ����
        /// <summary>
        /// �Ƿ������ڸ�ʽ
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsDateTime(string inputData)
        {
            bool flag = false;
        
            string regex = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))"; //���ڲ���
            regex += @"(\s(((0?[0-9])|([1][0-9])|([2][0-4]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$"; //ʱ�䲿��

            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            Regex reg = new Regex(regex, options);
            if (reg.IsMatch(inputData))
            {
                flag = true;
            }
            return flag;
        }
        public static bool IsDate(string inputData)
        {
            bool flag = false;

            string regex = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))"; //���ڲ���
            

            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            Regex reg = new Regex(regex, options);
            if (reg.IsMatch(inputData))
            {
                flag = true;
            }
            return flag;
        }

        #endregion

        #region ����

        /// <summary>
		/// ����ַ�����󳤶ȣ�����ָ�����ȵĴ�
		/// </summary>
		/// <param name="sqlInput">�����ַ���</param>
		/// <param name="maxLength">��󳤶�</param>
		/// <returns></returns>			
		public static string SqlText(string sqlInput, int maxLength)
		{			
			if(sqlInput != null && sqlInput != string.Empty)
			{
				sqlInput = sqlInput.Trim();							
				if(sqlInput.Length > maxLength)//����󳤶Ƚ�ȡ�ַ���
					sqlInput = sqlInput.Substring(0, maxLength);
			}
			return sqlInput;
		}		
		/// <summary>
		/// �ַ�������
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static string HtmlEncode(string inputData)
		{
			return HttpUtility.HtmlEncode(inputData);
		}
		/// <summary>
		/// ����Label��ʾEncode���ַ���
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="txtInput"></param>
		public static void SetLabel(Label lbl, string txtInput)
		{
			lbl.Text = HtmlEncode(txtInput);
		}
		public static void SetLabel(Label lbl, object inputObj)
		{
			SetLabel(lbl, inputObj.ToString());
		}		
		//�ַ�������
		public static string InputText(string inputString, int maxLength) 
		{			
			StringBuilder retVal = new StringBuilder();

			// ����Ƿ�Ϊ��
			if ((inputString != null) && (inputString != String.Empty)) 
			{
				inputString = inputString.Trim();
				
				//��鳤��
				if (inputString.Length > maxLength)
					inputString = inputString.Substring(0, maxLength);
				
				//�滻Σ���ַ�
				for (int i = 0; i < inputString.Length; i++) 
				{
					switch (inputString[i]) 
					{
						case '"':
							retVal.Append("&quot;");
							break;
						case '<':
							retVal.Append("&lt;");
							break;
						case '>':
							retVal.Append("&gt;");
							break;
						default:
							retVal.Append(inputString[i]);
							break;
					}
				}				
				retVal.Replace("'", " ");// �滻������
			}
			return retVal.ToString();
			
		}
		/// <summary>
		/// ת���� HTML code
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Encode(string str)
		{			
			str = str.Replace("&","&amp;");
			str = str.Replace("'","''");
			str = str.Replace("\"","&quot;");
			str = str.Replace(" ","&nbsp;");
			str = str.Replace("<","&lt;");
			str = str.Replace(">","&gt;");
			str = str.Replace("\n","<br>");
			return str;
		}
		/// <summary>
		///����html�� ��ͨ�ı�
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Decode(string str)
		{			
			str = str.Replace("<br>","\n");
			str = str.Replace("&gt;",">");
			str = str.Replace("&lt;","<");
			str = str.Replace("&nbsp;"," ");
			str = str.Replace("&quot;","\"");
			return str;
		}

		#endregion

		/// <summary>
		/// ������ʱ����ת�����ַ���
		/// </summary>
		/// <param name="datetime">ʱ���ַ���</param>
		/// <param name="IsDate">�Ƿ�ת������������</param>
		/// <returns></returns>
		#region ������ʱ����ת�����ַ���
		public static string GetDateorGetDateTime(string datetime, bool IsDate)
		{
			string str_date = datetime.Replace("/","-");
			string[] dtime = datetime.Split(' ');
			if (IsDate)
			{
				string[] arr = new string[3];
				arr = dtime[0].Split('-');

				if (arr[1].Length == 1)
				{
					arr[1] = arr[1].PadLeft(2, '0');
				}
				if (arr[2].Length == 1)
				{
					arr[2] = arr[2].PadLeft(2, '0');
				}
				str_date = arr[0] + "-" + arr[1] + "-" + arr[2];
			}
			else
			{
				#region ������
				string[] date = dtime[0].Split('-');
				string month = date[1].PadLeft(2, '0');
				string day = date[2].PadLeft(2, '0');
				#endregion

				#region ʱ����
				string[] datet = dtime[1].Split(':');
				string hour = datet[0].PadLeft(2, '0');
				string min = datet[1];
				string second = datet[2];
				#endregion


				str_date = date[0].ToString() + "-" + month + "-" + day + " " + hour + ":" + min + ":" + second;
			}
			return str_date;
		}
		#endregion

	}
}
