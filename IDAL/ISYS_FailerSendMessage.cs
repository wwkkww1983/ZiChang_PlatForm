/***********************************************
 * ��Ԫ���ƣ����ŷ���ʧ�ܽӿ�
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/

using System;
using System.Data;
namespace IndustryPlatform.IDAL
{
	/// <summary>
	/// �ӿڲ�ISYS_FailerSendMessage ��ժҪ˵����
	/// </summary>
	public interface ISYS_FailerSendMessage
	{

        void GridViewPagerBindbyRowNumber(Wuqi.Webdiyer.AspNetPager anpager, string strWhere, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl, int startyear, int endyear);
        

         /// <summary>
        /// �������Ͷ���
        /// </summary>
        /// <param name="FailerID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        int InsertIntoReadySendMessage(string FailerID, int year);
	}
}
