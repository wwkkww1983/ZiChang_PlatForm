/***********************************************
 * ��Ԫ���ƣ����ŷ���ʧ�ܵ�ʵ��
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/
using System;
namespace IndustryPlatform.Model
{
	/// <summary>
	/// ʵ����SYS_FailerSendMessage ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	public class SYS_FailerSendMessageEntity
	{
		public SYS_FailerSendMessageEntity()
		{}
		#region Model
		private int _fsmid;
		private int _operatorid;
        private string _PhoneNum;
		private string _mcontent;
		private string _systype;
		private DateTime _failerdate;
		/// <summary>
		/// ����ʧ�ܵ�Guid
		/// </summary>
		public int FSMID
		{
			set{ _fsmid=value;}
			get{return _fsmid;}
		}
		/// <summary>
		/// ���Ž�����ID
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
		/// ���Ͷ��ŵ�����
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
		/// ����ʧ��ʱ��
		/// </summary>
		public DateTime FailerDate
		{
			set{ _failerdate=value;}
			get{return _failerdate;}
		}
		#endregion Model

	}
}

