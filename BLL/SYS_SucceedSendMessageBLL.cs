/***********************************************
 * ��Ԫ���ƣ����ŷ��ͳɹ�
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
    /// ҵ���߼���SYS_SucceedSendMessage ��ժҪ˵����
    /// </summary>
    public class SYS_SucceedSendMessageBLL
    {
        private readonly ISYS_SucceedSendMessage dal = DataAccess.CreateSYS_SucceedSendMessage();
        public SYS_SucceedSendMessageBLL()
        { }

         #region ��ȡ���ŷ�������ϵͳ��
        public DataSet GetOrganizationInfo()
        {
            return dal.GetOrganizationInfo();
        }
        #endregion

        #region ��ȡ���ŷ�������ϵͳ��
        public DataSet GetSysType()
        {
            return dal.GetSysType();
        }
        #endregion

        public void GridViewPagerBindbyRowNumber(Wuqi.Webdiyer.AspNetPager anpager, string strWhere, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl, int startyear, int endyear)
        {
            dal.GridViewPagerBindbyRowNumber(anpager, strWhere, strOrderCondition, grvControl, startyear, endyear);
        }

    }
}

