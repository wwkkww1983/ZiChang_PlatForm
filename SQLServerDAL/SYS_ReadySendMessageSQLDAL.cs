/***********************************************
 * ��Ԫ���ƣ����ŵȴ�����
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
    /// ���ݷ�����SYS_ReadySendMessage��
    /// </summary>
    public class SYS_ReadySendMessageSQLDAL : ISYS_ReadySendMessage
    {
        public SYS_ReadySendMessageSQLDAL()
        { }

        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetReadySendMessageInfo()
        {
            DataTable dtReadySendMessageInfo = null;
            int iYear = DateTime.Now.Year;
            int ICount = 0;
            string strsql = "select count(*) from Sys_ReadySendMessage" + (iYear - 1) + "";
            try
            {
                ICount = Convert.ToInt32(DbHelperSQL.GetSingle(strsql));
            }
            catch
            {
                ICount = 0;
            }


            StringBuilder sbsql = new StringBuilder();

            #region ƴ�Ӳ�ѯ
            sbsql.Append("select top 20 RSMID,PhoneNumber,MContent,SendDate");

            if (ICount == 0)
            {
                sbsql.Append(" from Sys_ReadySendMessage" + iYear + " RSM");
                sbsql.Append(" order by SendDate desc");
            }
            else
            {
                sbsql.Append(" from (");
                sbsql.Append("select * from Sys_ReadySendMessage" + iYear + "");
                sbsql.Append(" union");
                sbsql.Append(" select * from Sys_ReadySendMessage" + (iYear - 1) + "");
                sbsql.Append(") as RSM");
                sbsql.Append(" order by SendDate desc");
            }
            #endregion

            DataSet ds = DbHelperSQL.DQuery(sbsql.ToString());

            dtReadySendMessageInfo = ds.Tables[0];

            return dtReadySendMessageInfo;
        }

    }
}

