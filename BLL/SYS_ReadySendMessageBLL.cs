/***********************************************
 * ��Ԫ���ƣ����ŵȴ�����
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/

using System;
using System.Data;
using IndustryPlatform.DALFactory;
using IndustryPlatform.Model;
using IndustryPlatform.IDAL;
namespace IndustryPlatform.BLL
{
	/// <summary>
	/// ҵ���߼���SYS_ReadySendMessage ��ժҪ˵����
	/// </summary>
	public class SYS_ReadySendMessageBLL
	{
		private readonly ISYS_ReadySendMessage dal=DataAccess.CreateSYS_ReadySendMessage();
		public SYS_ReadySendMessageBLL()
		{}

        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetReadySendMessageInfo()
        { 
            return dal.GetReadySendMessageInfo();
        }
		
    }
}

