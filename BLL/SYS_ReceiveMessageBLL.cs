/***********************************************
 * ��Ԫ���ƣ����ն���
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
	/// ҵ���߼���SYS_ReceiveMessage ��ժҪ˵����
	/// </summary>
	public class SYS_ReceiveMessageBLL
	{
		private readonly ISYS_ReceiveMessage dal=DataAccess.CreateSYS_ReceiveMessage();
		public SYS_ReceiveMessageBLL()
		{}
		
        /// <summary>
        /// GridView�ؼ���ҳ�ﶨ
        /// </summary>
        /// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        /// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        /// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        /// <param name="rptControl">GridView�ؼ�</param>
        public void GridViewPagerBindbyRowNumber(Wuqi.Webdiyer.AspNetPager anpager, string strWhere, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl, int startyear, int endyear)
        {
            dal.GridViewPagerBindbyRowNumber(anpager, strWhere, strOrderCondition, grvControl, startyear, endyear);
        }
    }
}

