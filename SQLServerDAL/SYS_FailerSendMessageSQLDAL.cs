/***********************************************
 * ��Ԫ���ƣ����ŷ���ʧ��
 * �� �� �ߣ���־��
 * ����ʱ�䣺2009-8-26
 * �޸�ʱ�䣺
 * �޸�ԭ��
 *********************************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using IndustryPlatform.IDAL;
using IndustryPlatform.DBUtility;

namespace IndustryPlatform.SQLServerDAL
{
    /// <summary>
    /// ���ݷ�����SYS_FailerSendMessage��
    /// </summary>
    public class SYS_FailerSendMessageSQLDAL : ISYS_FailerSendMessage
    {
        public SYS_FailerSendMessageSQLDAL()
        { }


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
                sbSucceedTable.Append("'Sys_FailerSendMessage" + i + "',");
            }
            sbSucceedTable.Remove(sbSucceedTable.Length - 1, 1);
            string strselectTable = "select name from sysobjects where type='u' and name in(" + sbSucceedTable + ")";

            DataSet dsSucceedTable = DbHelperSQL.DQuery(strselectTable);

            StringBuilder sbsql = new StringBuilder();

            sbsql.Append("select FSMID,PhoneNumber,MContent,FailerDate from (");

            for (int i = 0; i < dsSucceedTable.Tables[0].Rows.Count; i++)
            {
                sbsql.Append("select * from " + dsSucceedTable.Tables[0].Rows[i][0].ToString() + " union");
            }

            sbsql.Remove(sbsql.Length - 6, 6);
            sbsql.Append(") as FSM");
            #endregion

            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from (select row_number() over (order by " + strOrderCondition + ") as rowno,*  from (" + sbsql.ToString() + ") as VT_FailerMessage");
            strSql.Append(strWhere);
            strSql.Append(" ) as result Where (rowno Between " + ((anpager.CurrentPageIndex - 1) * anpager.PageSize + 1) + " and " + anpager.CurrentPageIndex * anpager.PageSize + ")");

            StringBuilder strb = new StringBuilder();

            strb.Append("Select Count(*) From (" + sbsql.ToString() + ") as VT_FailerMessage");
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

        /// <summary>
        /// �������Ͷ���
        /// </summary>
        /// <param name="FailerID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        #region �������Ͷ���
        public int InsertIntoReadySendMessage(string FailerID, int year)
        {
            int Irow = 0;
            List<string> Ltsql = new List<string>();

            string strsql = "select PhoneNumber,MContent,FailerDate from Sys_FailerSendMessage" + year + " where FSMID in(" + FailerID + ")";

            DataSet dsFailerMessage = DbHelperSQL.DQuery(strsql);

            if (null != dsFailerMessage)
            {
                if (dsFailerMessage.Tables[0].Rows.Count != 0)
                {
                    DataRowCollection drc = dsFailerMessage.Tables[0].Rows;

                    for (int i = 0; i < drc.Count; i++)
                    {

                        Ltsql.Add("insert into Sys_ReadySendMessage" + year + " (PhoneNumber,MContent,SendDate) values('" + drc[i]["PhoneNumber"].ToString() + "','" + drc[i]["MContent"].ToString() + "','" + drc[i]["FailerDate"].ToString() + "');");
                    }

                    Ltsql.Add("delete from Sys_ReadySendMessage" + year + " where FSMID in (" + FailerID + ");");

                    Irow = DbHelperSQL.ExecuteSqlTran(Ltsql);
                }
            }
            else
            {
                Irow = 0;
            }

            return Irow;
        }
        #endregion
    }
}

