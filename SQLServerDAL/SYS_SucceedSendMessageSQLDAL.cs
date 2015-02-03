/***********************************************
 * ��Ԫ���ƣ����ŷ��ͳɹ�
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using IndustryPlatform.IDAL;
using IndustryPlatform.DBUtility;

namespace IndustryPlatform.SQLServerDAL
{
    /// <summary>
    /// ���ݷ�����SYS_SucceedSendMessage��
    /// </summary>
    public class SYS_SucceedSendMessageSQLDAL : ISYS_SucceedSendMessage
    {
        public SYS_SucceedSendMessageSQLDAL()
        { }

        #region ��ȡ���ŷ�������ϵͳ��
        public DataSet GetOrganizationInfo()
        {
            StringBuilder sbql = new StringBuilder();
            sbql.Append("select name from sysobjects where type='u' and Name like 'SYS_SucceedSendMessage%'");

            DataSet dsTableName = DbHelperSQL.DQuery(sbql.ToString());

            StringBuilder sbselect = new StringBuilder();

            sbselect.Append("select distinct OZ.OrgID,OrgName from (");
            for (int i = 0; i < dsTableName.Tables[0].Rows.Count; i++)
            {
                sbselect.Append("select OperatorID from " + dsTableName.Tables[0].Rows[i][0].ToString() + " union");
            }

            sbselect.Remove(sbselect.Length - 6, 6);
            sbselect.Append(") as SSM");
            sbselect.Append(" inner join Sys_Operator O on O.OperatorID = SSM.OperatorID");
            sbselect.Append(" inner join Sys_Organization OZ on OZ.OrgID = O.OrgID");

            DataSet dsOrgInfo = DbHelperSQL.DQuery(sbselect.ToString());

            return dsOrgInfo;
        }
        #endregion

        #region ��ȡ���ŷ�������ϵͳ��
        public DataSet GetSysType()
        {
            StringBuilder sbql = new StringBuilder();
            sbql.Append("select name from sysobjects where type='u' and Name like 'SYS_SucceedSendMessage%'");

            DataSet dsTableName = DbHelperSQL.DQuery(sbql.ToString());

            StringBuilder sbselect = new StringBuilder();

            sbselect.Append("select distinct MenuID,MenuName from (");
            for (int i = 0; i < dsTableName.Tables[0].Rows.Count; i++)
            {
                sbselect.Append("select systype from " + dsTableName.Tables[0].Rows[i][0].ToString() + " union");
            }

            sbselect.Remove(sbselect.Length - 6, 6);
            sbselect.Append(") as SSM");
            sbselect.Append(" inner join Sys_Menu M on M.MenuID = SSM.SysType");

            DataSet dsSysType = DbHelperSQL.DQuery(sbselect.ToString());

            return dsSysType;
        }
        #endregion


        #region ʹ�÷�ҳ�ؼ���GridView(���ط���)
        //#region Repeater�ؼ���ҳ�ﶨ
        ///// <summary #region GridView�ؼ���ҳ�ﶨ
        /// <summary>
        /// GridView�ؼ���ҳ�ﶨ
        /// </summary>
        /// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        /// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        /// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        /// <param name="rptControl">GridView�ؼ�</param>
        public void GridViewPagerBindbyRowNumber(Wuqi.Webdiyer.AspNetPager anpager, string strWhere, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl, int startyear, int endyear)
        {
            StringBuilder sbSucceedTable = new StringBuilder();

            DataSet dstTemp = new DataSet();

            #region ����ʱ���ȡҪ��ѯ�Ķ��ű�
            for (int i = startyear; i <= endyear; i++)
            {
                sbSucceedTable.Append("'Sys_SucceedSendMessage" + i + "',");
            }
            sbSucceedTable.Remove(sbSucceedTable.Length - 1, 1);
            string strselectTable = "select name from sysobjects where type='u' and name in(" + sbSucceedTable + ")";

            DataSet dsSucceedTable = DbHelperSQL.DQuery(strselectTable);

            StringBuilder sbsql = new StringBuilder();

            sbsql.Append("select SSMID,PhoneNumber,MContent,SucceedDate from (");

            for (int i = 0; i < dsSucceedTable.Tables[0].Rows.Count; i++)
            {
                sbsql.Append("select * from " + dsSucceedTable.Tables[0].Rows[i][0].ToString() + " union");
            }

            sbsql.Remove(sbsql.Length - 6, 6);
            sbsql.Append(") as SSM");
            #endregion

            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from (select row_number() over (order by " + strOrderCondition + ") as rowno,*  from (" + sbsql.ToString() + ") as VT_SucceedMessage");
            strSql.Append(strWhere);
            strSql.Append(" ) as result Where (rowno Between " + ((anpager.CurrentPageIndex - 1) * anpager.PageSize + 1) + " and " + anpager.CurrentPageIndex * anpager.PageSize + ")");

            StringBuilder strb = new StringBuilder();

            strb.Append("Select Count(*) From (" + sbsql.ToString() + ") as VT_SucceedMessage");
            strb.Append(strWhere);

            anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle(strb.ToString()));
            dstTemp = DbHelperSQL.Query(strSql.ToString());



            if (dstTemp.Tables[0].Rows.Count == 0)
            {
                grvControl.DataSource = null;
                grvControl.DataBind();
            }
            else
            {
                grvControl.DataSource = dstTemp.Tables[0];
                grvControl.DataBind();
            }


            //��̬�����û��Զ����ı�����
            anpager.CustomInfoHTML = "����<font color=\"blue\"><b>" + anpager.RecordCount.ToString() + "</b></font>����¼";
            anpager.CustomInfoHTML += " ��ҳ����<font color=\"blue\"><b>" + anpager.PageCount.ToString() + "</b></font>ҳ";
            anpager.CustomInfoHTML += " ��ǰҳ����<font color=\"red\"><b>" + anpager.CurrentPageIndex.ToString() + "</b></font>ҳ";

        }
        #endregion

    }
}

