/***********************************************
 * ��Ԫ���ƣ����ŷ��ͳɹ��ӿ�
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
	/// �ӿڲ�ISYS_SucceedSendMessage ��ժҪ˵����
	/// </summary>
	public interface ISYS_SucceedSendMessage
	{

       DataSet GetOrganizationInfo();
        
        DataSet GetSysType();
        
        void GridViewPagerBindbyRowNumber(Wuqi.Webdiyer.AspNetPager anpager, string strWhere, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl, int startyear, int endyear);
       
	}
}
