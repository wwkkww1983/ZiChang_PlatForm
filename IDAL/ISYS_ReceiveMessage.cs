/***********************************************
 * ��Ԫ���ƣ����ն��Žӿ�
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
	/// �ӿڲ�ISYS_ReceiveMessage ��ժҪ˵����
	/// </summary>
	public interface ISYS_ReceiveMessage
	{
		///// <summary #region GridView�ؼ���ҳ�ﶨ
        /// <summary>
        /// GridView�ؼ���ҳ�ﶨ
        /// </summary>
        /// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        /// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        /// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        /// <param name="rptControl">GridView�ؼ�</param>
        void GridViewPagerBindbyRowNumber(Wuqi.Webdiyer.AspNetPager anpager, string strWhere, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl, int startyear, int endyear);
        
        
	}
}
