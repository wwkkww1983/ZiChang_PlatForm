/***********************************************
 * ��Ԫ���ƣ��������ŵ�ʵ��
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/
using System;
namespace IndustryPlatform.Model
{
	/// <summary>
	/// ʵ����SYS_ReadySendMessage ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	public class SYS_ReadySendMessageEntity
	{
		public SYS_ReadySendMessageEntity()
		{}
		#region Model
		private int _rsmid;
		private int _operatorid;
        private string _PhoneNum;
		private string _mcontent;
		private string _systype;
        private DateTime _senddate;
		private string _sendstate;
		private int _failernumber;
		/// <summary>
		/// ���Ͷ��ŵ�GUID
		/// </summary>
		public int RSMID
		{
			set{ _rsmid=value;}
			get{return _rsmid;}
		}
		/// <summary>
		/// ���Ž����˵�ID
		/// </summary>
		public int OperatorID
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
        /// <summary>
        /// �ֻ�����
        /// </summary>
        public string PhoneNum
        {
            get { return _PhoneNum; }
            set { _PhoneNum = value; }
        }
		/// <summary>
		/// ���ŵ�����
		/// </summary>
		public string MContent
		{
			set{ _mcontent=value;}
			get{return _mcontent;}
		}
		/// <summary>
		/// ��������ϵͳ
		/// </summary>
		public string SysType
		{
			set{ _systype=value;}
			get{return _systype;}
		}
        /// <summary>
        /// �������ŵ�����ʱ��
        /// </summary>
        public DateTime SendDate
        {
            get { return _senddate; }
            set { _senddate = value; }
        }
		/// <summary>
		/// ���ŵķ���״̬
		/// </summary>
		public string SendState
		{
			set{ _sendstate=value;}
			get{return _sendstate;}
		}
		/// <summary>
		/// ���ŷ���ʧ�ܴ���
		/// </summary>
		public int FailerNumber
		{
			set{ _failernumber=value;}
			get{return _failernumber;}
		}
		#endregion Model

	}
}

