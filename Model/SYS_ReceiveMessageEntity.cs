/***********************************************
 * ��Ԫ���ƣ����ն��ŵ�ʵ��
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/
using System;
namespace IndustryPlatform.Model
{
	/// <summary>
	/// ʵ����SYS_ReceiveMessage ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	public class SYS_ReceiveMessageEntity
	{
		public SYS_ReceiveMessageEntity()
		{}
		#region Model
		private int _rmid;
		private string _phonenumber;
		private string _mcontent;
		private DateTime _receivedate;
		/// <summary>
		/// ���Ž��յ�GUID
		/// </summary>
		public int RMID
		{
			set{ _rmid=value;}
			get{return _rmid;}
		}
		/// <summary>
		/// ���ŵ�PhoneNumber
		/// </summary>
		public string PhoneNumber
		{
			set{ _phonenumber=value;}
			get{return _phonenumber;}
		}
		/// <summary>
		/// ���ն��ŵ�����
		/// </summary>
		public string MContent
		{
			set{ _mcontent=value;}
			get{return _mcontent;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime ReceiveDate
		{
			set{ _receivedate=value;}
			get{return _receivedate;}
		}
		#endregion Model

	}
}

