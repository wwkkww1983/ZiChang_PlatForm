/***********************************************
 * ��Ԫ���ƣ����ŷ���ʧ��
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
	/// ҵ���߼���SYS_FailerSendMessage ��ժҪ˵����
	/// </summary>
	public class SYS_FailerSendMessageBLL
	{
		private readonly ISYS_FailerSendMessage dal=DataAccess.CreateSYS_FailerSendMessage();
		public SYS_FailerSendMessageBLL()
		{}


        public void GridViewPagerBindbyRowNumber(Wuqi.Webdiyer.AspNetPager anpager, string strWhere, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl, int startyear, int endyear)
        {
            dal.GridViewPagerBindbyRowNumber(anpager,strWhere,strOrderCondition,grvControl,startyear,endyear);
        }

         /// <summary>
        /// �������Ͷ���
        /// </summary>
        /// <param name="FailerID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        #region �������Ͷ���
        public int InsertIntoReadySendMessage(string FailerID, int year)
        {
            return dal.InsertIntoReadySendMessage(FailerID,year);
        }
        #endregion
    }
}

