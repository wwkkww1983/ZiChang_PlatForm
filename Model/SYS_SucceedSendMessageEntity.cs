/***********************************************
 * ��Ԫ���ƣ����ŷ��ͳɹ���ʵ��
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/
using System;
namespace IndustryPlatform.Model
{
	/// <summary>
	/// ʵ����SYS_SucceedSendMessage ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	public class SYS_SucceedSendMessageEntity
	{
		public SYS_SucceedSendMessageEntity()
		{}
		#region Model
		private int _ssmid;
		private int _operatorid;
        private string _PhoneNum;
		private string _mcontent;
		private string _systype;
		private DateTime _succeeddate;
		/// <summary>
		/// ���ű��
		/// </summary>
		public int SSMID
		{
			set{ _ssmid=value;}
			get{return _ssmid;}
		}
		/// <summary>
		/// �ն�����ID
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
		/// ��������
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
		/// ���ͳɹ���ʱ��
		/// </summary>
		public DateTime SucceedDate
		{
			set{ _succeeddate=value;}
			get{return _succeeddate;}
		}
		#endregion Model

	}
}

