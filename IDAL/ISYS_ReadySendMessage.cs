/***********************************************
 * ��Ԫ���ƣ����ŵȴ����ͽӿ�
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/

using System;
using System.Data;
using IndustryPlatform.Model;

namespace IndustryPlatform.IDAL
{
	/// <summary>
	/// �ӿڲ�ISYS_ReadySendMessage ��ժҪ˵����
	/// </summary>
	public interface ISYS_ReadySendMessage
	{
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <returns></returns>
        DataTable GetReadySendMessageInfo();
        
	}
}
