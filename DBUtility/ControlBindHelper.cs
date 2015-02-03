using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Collections;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace IndustryPlatform.DBUtility
{
    /// <summary>
    /// �ؼ��ﶨ���������
    /// </summary>
    public abstract class ControlBindHelper
    {
        public ControlBindHelper()
        {
        }


        /// <summary>
        /// ���ݷ�ҳ��������б�
        /// </summary>
        public static void GridViewPagerBindByRowNum(Wuqi.Webdiyer.AspNetPager anpager, string strTableName, string strWhere, string strOrder, System.Web.UI.WebControls.GridView grvControl)
        {
            int iCount = Convert.ToInt32(DbHelperSQL.GetSingle("select count(0) from " + strTableName + " where " + strWhere));
            anpager.RecordCount = iCount;
            int iRow1 = (anpager.CurrentPageIndex - 1) * anpager.PageSize + 1;
            int iRow2 = anpager.CurrentPageIndex * anpager.PageSize;
            string strSql = "select * from "
                                     + "(select row_number() over(order by " + strOrder + ") RowNo,* from " + strTableName
                                     + " where " + strWhere + ") as result where RowNo>=" + iRow1 + " and RowNo <=" + iRow2;
            DataSet ds = DbHelperSQL.Query(strSql);
            grvControl.DataSource = ds;
            grvControl.DataBind();
            //��̬�����û��Զ����ı�����
            anpager.CustomInfoHTML = "����<font color=\"blue\"><b>" + anpager.RecordCount.ToString() + "</b></font>����¼";
            anpager.CustomInfoHTML += " ��ҳ����<font color=\"blue\"><b>" + anpager.PageCount.ToString() + "</b></font>ҳ";
            anpager.CustomInfoHTML += " ��ǰҳ����<font color=\"red\"><b>" + anpager.CurrentPageIndex.ToString() + "</b></font>ҳ";
        }

        #region DropDownList�ﶨ
        /// <summary>
        /// �ﶨDropDownList
        /// </summary>
        /// <param name="ddlControl">DropDownList�ؼ�ID</param>
        /// <param name="strTableName">����</param>
        /// <param name="strText">��ʾ���ı��ֶ�</param>
        /// <param name="strValue">ֵ�ֶ�</param>
        /// <param name="strInitializeText">��ʼ���ı�</param>
        /// <param name="strInitializeValue">��ʼ��ֵ</param>
        public static void DropDownListBind(System.Web.UI.WebControls.DropDownList ddlControl, string strTableName, string strText, string strValue, string strInitializeText,string strInitializeValue)
        {
            string strSql = "SELECT " + strText + "," + strValue + " FROM " + strTableName + "";
            DataSet dstTemp = new DataSet();

            dstTemp = DbHelperSQL.Query(strSql);

            ddlControl.DataSource = dstTemp.Tables[0];
            ddlControl.DataTextField = strText;
            ddlControl.DataValueField = strValue;
            ddlControl.DataBind();

            ddlControl.Items.Insert(0, "" + strInitializeText + "");
            ddlControl.Items[0].Value = strInitializeValue;
        }
        /// <summary>
        /// �ﶨDropDownList
        /// </summary>
        /// <param name="ddlControl">DropDownList�ؼ�ID</param>
        /// <param name="strTableName">����</param>
        /// <param name="strText">��ʾ���ı��ֶ�</param>
        /// <param name="strValue">ֵ�ֶ�</param>
        /// <param name="strWhere">��ѯ������������Where</param>
        public static void DropDownListBind(System.Web.UI.WebControls.DropDownList ddlControl,string strTableName, string strText, string strValue, string strWhere)
        {

            string strSql = "SELECT " + strText + "," + strValue + " FROM " + strTableName + " Where " + strWhere;
            DataSet dstTemp = new DataSet();

            dstTemp = DbHelperSQL.Query(strSql);
            ddlControl.DataSource = dstTemp.Tables[0];
            ddlControl.DataTextField = strText;
            ddlControl.DataValueField = strValue;
            ddlControl.DataBind();
        }
        /// <summary>
        /// �ﶨDropDownList
        /// </summary>
        /// <param name="ddlControl">DropDownList�ؼ�ID</param>
        /// <param name="strTableName">����</param>
        /// <param name="strText">��ʾ���ı��ֶ�</param>
        /// <param name="strValue">ֵ�ֶ�</param>
        /// <param name="strWhere">��ѯ������������Where</param>
        /// <param name="strInitializeText">��ʼ���ı�</param>
        /// <param name="strInitializeValue">��ʼ��ֵ</param>
        public static void DropDownListBind(System.Web.UI.WebControls.DropDownList ddlControl, string strTableName, string strText, string strValue, string strWhere, string strInitializeText, string strInitializeValue)
        {
            string strSql = "SELECT " + strText + "," + strValue + " FROM " + strTableName + " Where " + strWhere;
            DataSet dstTemp = new DataSet();

            dstTemp = DbHelperSQL.Query(strSql);
            ddlControl.DataSource = dstTemp.Tables[0];
            ddlControl.DataTextField = strText;
            ddlControl.DataValueField = strValue;
            ddlControl.DataBind();
            ddlControl.Items.Insert(0, "" + strInitializeText + "");
            ddlControl.Items[0].Value = strInitializeValue;
        }
        /// <summary>
        /// �ﶨDropDownList
        /// </summary>
        /// <param name="ddlControl">DropDownList�ؼ�ID</param>
        /// <param name="strTableName">����</param>
        /// <param name="strText">��ʾ���ı��ֶ�</param>
        /// <param name="strValue">ֵ�ֶ�</param>
        /// <param name="strWhere">��ѯ������������Where</param>
        /// <param name="strOrder">�����ֶ�</param>
        /// <param name="strInitializeText">��ʼ���ı�</param>
        /// <param name="strInitializeValue">��ʼ��ֵ</param>
        public static void DropDownListBind(System.Web.UI.WebControls.DropDownList ddlControl, string strTableName, string strText, string strValue, string strWhere, string strOrder, string strInitializeText, string strInitializeValue)
        {

            string strSql = "SELECT " + strText + "," + strValue + " FROM " + strTableName + " Where " + strWhere + " Order By " + strOrder;
            DataSet dstTemp = new DataSet();

            dstTemp = DbHelperSQL.Query(strSql);
            ddlControl.DataSource = dstTemp.Tables[0];
            ddlControl.DataTextField = strText;
            ddlControl.DataValueField = strValue;
            ddlControl.DataBind();
            ddlControl.Items.Insert(0, "" + strInitializeText + "");
            ddlControl.Items[0].Value = strInitializeValue;
        }
        #endregion

        //#region Repeater�ؼ���ҳ�ﶨ
        ///// <summary #region GridView�ؼ���ҳ�ﶨ
        /// <summary>
        /// GridView�ؼ���ҳ�ﶨ
        /// </summary>
        /// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        /// <param name="strTableName">����</param>
        /// <param name="strPrimaryKey">���Ψһ������</param>
        /// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        /// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        /// <param name="rptControl">GridView�ؼ�</param>
        public static void GridViewPagerBind(Wuqi.Webdiyer.AspNetPager anpager, string strTableName, string strPrimaryKey, string strQuaryCondition, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl)
        {
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@pageindex",SqlDbType.Int),
                    new SqlParameter("@pagesize",SqlDbType.Int),
                    new SqlParameter("@docount",SqlDbType.Bit),
                    new SqlParameter("@strwhere",SqlDbType.NVarChar,1000),
                    new SqlParameter("@tablenm",SqlDbType.NVarChar,100),
                    new SqlParameter("@tbmainid",SqlDbType.NVarChar,100),
                    new SqlParameter("@strorder",SqlDbType.NVarChar,100),
                };

            parameters[0].Value = anpager.CurrentPageIndex;
            parameters[1].Value = anpager.PageSize;
            parameters[2].Value = false;
            parameters[3].Value = strQuaryCondition;
            parameters[4].Value = strTableName;
            parameters[5].Value = strPrimaryKey;
            parameters[6].Value = strOrderCondition;

            if (strQuaryCondition == "")
            {
                anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName));
            }
            else
            {
                anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName + " Where " + strQuaryCondition));
            }
            DataSet dstTemp = DbHelperSQL.DRunProcedure("P_ControlPager", parameters, "NewTableName");

            if (dstTemp.Tables[0].Rows.Count == 0)
            {
                //DataRow dr = dstTemp.Tables[0].NewRow();
                //dstTemp.Tables[0].Rows.Add(dr);
                grvControl.DataSource = null;
                grvControl.DataBind();
            }
            else
            {
                grvControl.DataSource = dstTemp.Tables["NewTableName"];
                grvControl.DataBind();
            }


            //��̬�����û��Զ����ı�����
            anpager.CustomInfoHTML = "����<font color=\"blue\"><b>" + anpager.RecordCount.ToString() + "</b></font>����¼";
            anpager.CustomInfoHTML += " ��ҳ����<font color=\"blue\"><b>" + anpager.PageCount.ToString() + "</b></font>ҳ";
            anpager.CustomInfoHTML += " ��ǰҳ����<font color=\"red\"><b>" + anpager.CurrentPageIndex.ToString() + "</b></font>ҳ";

        }

        ///// Repeater�ؼ���ҳ�ﶨ
        ///// </summary>
        ///// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        ///// <param name="strTableName">����</param>
        ///// <param name="strPrimaryKey">���Ψһ������</param>
        ///// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        ///// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        ///// <param name="rptControl">Repeater�ؼ�</param>
        //public static void RepeaterPagerBind(Wuqi.Webdiyer.AspNetPager anpager, string strTableName,string strPrimaryKey,string strQuaryCondition,string strOrderCondition, System.Web.UI.WebControls.Repeater rptControl)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //        {
        //            new SqlParameter("@pageindex",SqlDbType.Int),
        //            new SqlParameter("@pagesize",SqlDbType.Int),
        //            new SqlParameter("@docount",SqlDbType.Bit),
        //            new SqlParameter("@strwhere",SqlDbType.NVarChar,1000),
        //            new SqlParameter("@tablenm",SqlDbType.NVarChar,100),
        //            new SqlParameter("@tbmainid",SqlDbType.NVarChar,100),
        //            new SqlParameter("@strorder",SqlDbType.NVarChar,100),
        //        };

        //    parameters[0].Value = anpager.CurrentPageIndex;
        //    parameters[1].Value = anpager.PageSize;
        //    parameters[2].Value = false;
        //    parameters[3].Value = strQuaryCondition;
        //    parameters[4].Value = strTableName;
        //    parameters[5].Value = strPrimaryKey;
        //    parameters[6].Value = strOrderCondition;

        //    if (strQuaryCondition == "")
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName));
        //    }
        //    else
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName + " Where " + strQuaryCondition));
        //    }

        //    DataSet dstTemp = DbHelperSQL.DRunProcedure("P_ControlPager", parameters, "NewTableName");

        //    rptControl.DataSource = dstTemp.Tables["NewTableName"];
        //    rptControl.DataBind();

        //    //��̬�����û��Զ����ı�����
        //    anpager.CustomInfoHTML = "����<font color=\"blue\"><b>" + anpager.RecordCount.ToString() + "</b></font>����¼";
        //    anpager.CustomInfoHTML += " ��ҳ����<font color=\"blue\"><b>" + anpager.PageCount.ToString() + "</b></font>ҳ";
        //    anpager.CustomInfoHTML += " ��ǰҳ����<font color=\"red\"><b>" + anpager.CurrentPageIndex.ToString() + "</b></font>ҳ";
        //}
        //#endregion

        //#region GridView�ؼ���ҳ�ﶨ
        ///// <summary>
        ///// GridView�ؼ���ҳ�ﶨ
        ///// </summary>
        ///// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        ///// <param name="strTableName">����</param>
        ///// <param name="strPrimaryKey">���Ψһ������</param>
        ///// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        ///// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        ///// <param name="rptControl">GridView�ؼ�</param>
        //public static void GridViewPagerBind(Wuqi.Webdiyer.AspNetPager anpager, string strTableName, string strPrimaryKey, string strQuaryCondition, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //        {
        //            new SqlParameter("@pageindex",SqlDbType.Int),
        //            new SqlParameter("@pagesize",SqlDbType.Int),
        //            new SqlParameter("@docount",SqlDbType.Bit),
        //            new SqlParameter("@strwhere",SqlDbType.NVarChar,1000),
        //            new SqlParameter("@tablenm",SqlDbType.NVarChar,100),
        //            new SqlParameter("@tbmainid",SqlDbType.NVarChar,100),
        //            new SqlParameter("@strorder",SqlDbType.NVarChar,100),
        //        };

        //    parameters[0].Value = anpager.CurrentPageIndex;
        //    parameters[1].Value = anpager.PageSize;
        //    parameters[2].Value = false;
        //    parameters[3].Value = strQuaryCondition;
        //    parameters[4].Value = strTableName;
        //    parameters[5].Value = strPrimaryKey;
        //    parameters[6].Value = strOrderCondition;

        //    if (strQuaryCondition == "")
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName));
        //    }
        //    else
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName + " Where " + strQuaryCondition));
        //    }
        //    DataSet dstTemp = DbHelperSQL.DRunProcedure("P_ControlPager", parameters,"NewTableName");

        //    if (dstTemp.Tables[0].Rows.Count == 0)
        //    {
        //        //DataRow dr = dstTemp.Tables[0].NewRow();
        //        //dstTemp.Tables[0].Rows.Add(dr);
        //        grvControl.DataSource = null;
        //        grvControl.DataBind();
        //    }
        //    else
        //    {
        //        grvControl.DataSource = dstTemp.Tables["NewTableName"];
        //        grvControl.DataBind();
        //    }
            

        //    //��̬�����û��Զ����ı�����
        //    anpager.CustomInfoHTML = "����<font color=\"blue\"><b>" + anpager.RecordCount.ToString() + "</b></font>����¼";
        //    anpager.CustomInfoHTML += " ��ҳ����<font color=\"blue\"><b>" + anpager.PageCount.ToString() + "</b></font>ҳ";
        //    anpager.CustomInfoHTML += " ��ǰҳ����<font color=\"red\"><b>" + anpager.CurrentPageIndex.ToString() + "</b></font>ҳ";

        //}

        //#endregion

        //#region GridView�ؼ���ҳ�ﶨ
        ///// <summary>
        ///// GridView�ؼ���ҳ�ﶨ
        ///// </summary>
        ///// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        ///// <param name="strTableName">����</param>
        ///// <param name="strPrimaryKey">���Ψһ������</param>
        ///// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        ///// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        ///// <param name="rptControl">GridView�ؼ�</param>
        //public static void GridViewPagerBind(Wuqi.Webdiyer.AspNetPager anpager, string strTableName, string strPrimaryKey, string strQuaryCondition, string strOrderCondition, System.Web.UI.WebControls.GridView grvControl,string produceName)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //        {
        //            new SqlParameter("@pageindex",SqlDbType.Int),
        //            new SqlParameter("@pagesize",SqlDbType.Int),
        //            new SqlParameter("@docount",SqlDbType.Bit),
        //            new SqlParameter("@strwhere",SqlDbType.NVarChar,1000),
        //            new SqlParameter("@tablenm",SqlDbType.NVarChar,100),
        //            new SqlParameter("@tbmainid",SqlDbType.NVarChar,100),
        //            new SqlParameter("@strorder",SqlDbType.NVarChar,100),
        //        };

        //    parameters[0].Value = anpager.CurrentPageIndex;
        //    parameters[1].Value = anpager.PageSize;
        //    parameters[2].Value = false;
        //    parameters[3].Value = strQuaryCondition;
        //    parameters[4].Value = strTableName;
        //    parameters[5].Value = strPrimaryKey;
        //    parameters[6].Value = strOrderCondition;

        //    if (strQuaryCondition == "")
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName));
        //    }
        //    else
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName + " Where " + strQuaryCondition));
        //    }

        //    DataSet dstTemp = DbHelperSQL.DRunProcedure(produceName, parameters, "NewTableName");

        //    grvControl.DataSource = dstTemp.Tables["NewTableName"];
        //    grvControl.DataBind();

        //    //��̬�����û��Զ����ı�����
        //    anpager.CustomInfoHTML = "����<font color=\"blue\"><b>" + anpager.RecordCount.ToString() + "</b></font>����¼";
        //    anpager.CustomInfoHTML += " ��ҳ����<font color=\"blue\"><b>" + anpager.PageCount.ToString() + "</b></font>ҳ";
        //    anpager.CustomInfoHTML += " ��ǰҳ����<font color=\"red\"><b>" + anpager.CurrentPageIndex.ToString() + "</b></font>ҳ";
        //}

        //#endregion

        //#region DataList�ؼ���ҳ�ﶨ
        ///// <summary>
        ///// DataList�ؼ���ҳ�ﶨ
        ///// </summary>
        ///// <param name="anpager">AspNetPager��ҳ�ؼ�</param>
        ///// <param name="strTableName">����</param>
        ///// <param name="strPrimaryKey">���Ψһ������</param>
        ///// <param name="strQuaryCondition">��ѯWhere����������Where</param>
        ///// <param name="strOrderCondition">��Ҫ������ֶ���</param>
        ///// <param name="rptControl">DataList�ؼ�</param>
        //public static void DataListPagerBind(Wuqi.Webdiyer.AspNetPager anpager, string strTableName, string strPrimaryKey, string strQuaryCondition, string strOrderCondition, System.Web.UI.WebControls.DataList dlstControl)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //        {
        //            new SqlParameter("@pageindex",SqlDbType.Int),
        //            new SqlParameter("@pagesize",SqlDbType.Int),
        //            new SqlParameter("@docount",SqlDbType.Bit),
        //            new SqlParameter("@strwhere",SqlDbType.NVarChar,1000),
        //            new SqlParameter("@tablenm",SqlDbType.NVarChar,100),
        //            new SqlParameter("@tbmainid",SqlDbType.NVarChar,100),
        //            new SqlParameter("@strorder",SqlDbType.NVarChar,100),
        //        };

        //    parameters[0].Value = anpager.CurrentPageIndex;
        //    parameters[1].Value = anpager.PageSize;
        //    parameters[2].Value = false;
        //    parameters[3].Value = strQuaryCondition;
        //    parameters[4].Value = strTableName;
        //    parameters[5].Value = strPrimaryKey;
        //    parameters[6].Value = strOrderCondition;

        //    if (strQuaryCondition == "")
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName));
        //    }
        //    else
        //    {
        //        anpager.RecordCount = Convert.ToInt32(DbHelperSQL.GetSingle("Select Count(*) From " + strTableName + " Where " + strQuaryCondition));
        //    }

        //    DataSet dstTemp = DbHelperSQL.DRunProcedure("P_ControlPager", parameters, "NewTableName");

        //    dlstControl.DataSource = dstTemp.Tables["NewTableName"];
        //    dlstControl.DataBind();

        //    //��̬�����û��Զ����ı�����
        //    anpager.CustomInfoHTML = "����<font color=\"blue\"><b>" + anpager.RecordCount.ToString() + "</b></font>����¼";
        //    anpager.CustomInfoHTML += " ��ҳ����<font color=\"blue\"><b>" + anpager.PageCount.ToString() + "</b></font>ҳ";
        //    anpager.CustomInfoHTML += " ��ǰҳ����<font color=\"red\"><b>" + anpager.CurrentPageIndex.ToString() + "</b></font>ҳ";

        //}
        //#endregion

        #region CheckBoxListBind�
        /// <summary>
        /// CheckBoxListBind�
        /// </summary>
        /// <param name="chklControl">CheckBoxList�ؼ�</param>
        /// <param name="strTableName">����</param>
        /// <param name="strText">��ʾ���ֶ�</param>
        /// <param name="strValue">ֵ�ֶ�</param>
        public static void CheckBoxListBind(System.Web.UI.WebControls.CheckBoxList chklControl,string strTableName, string strText, string strValue)
        {
            string strSql = "SELECT * FROM " + strTableName + "";
            DataSet dstTemp = new DataSet();

            dstTemp = DbHelperSQL.Query(strSql);

            chklControl.DataSource = dstTemp.Tables[0];
            chklControl.DataTextField = strText;
            chklControl.DataValueField = strValue;
            chklControl.DataBind();
        }
        /// <summary>
        /// CheckBoxListBind�
        /// </summary>
        /// <param name="chklControl">CheckBoxList�ؼ�</param>
        /// <param name="strTableName">����</param>
        /// <param name="strText">��ʾ���ֶ�</param>
        /// <param name="strValue">ֵ�ֶ�</param>
         /// <param name="strWhere">��ѯ����</param>
        public static void CheckBoxListBind(System.Web.UI.WebControls.CheckBoxList chklControl, string strTableName, string strText, string strValue, string strWhere)
        {
            string strSql = "SELECT * FROM " + strTableName + " Where " + strWhere;
            DataSet dstTemp = new DataSet();

            dstTemp = DbHelperSQL.Query(strSql);

            chklControl.DataSource = dstTemp.Tables[0];
            chklControl.DataTextField = strText;
            chklControl.DataValueField = strValue;
            chklControl.DataBind();
        }

        #endregion 

        #region ��TextBox��ǰ�������ID+1
        /// <summary>
        /// ��TextBox��ǰ�������ID+1
        /// </summary>
        /// <param name="txtControl">TextBox�ؼ�ID</param>
        /// <param name="strTableName">����</param>
        /// <param name="strPrimaryKeyName">����</param>
        public static void TextBoxAutoIncreaseCodeBind(System.Web.UI.WebControls.TextBox txtControl,string strTableName,string strPrimaryKeyName)
        {
            string strMaxCode = string.Empty;

            try
            {
                string strSql = "Select EnumValue From dbo.T_EnumValue Where EnumValueName='" + strTableName + "'";
                string strDefaultValue = DbHelperSQL.GetSingle(strSql).ToString();

                strSql = "Select Top 1 " + strPrimaryKeyName + " From " + strTableName + " Order By " + strPrimaryKeyName + " desc";

                object objScalar = DbHelperSQL.GetSingle(strSql);

                if (objScalar != null && objScalar.ToString() != "")
                {
                    strMaxCode = objScalar.ToString();

                    if (strMaxCode.Length > 2)
                    {
                        string strMaxCodeBefore = strMaxCode.Substring(0, 1);
                        string strMaxCodeAfter = strMaxCode.Substring(1);
                        int iMaxCodeAfterLength = strMaxCodeAfter.Length;

                        strMaxCodeAfter = (Convert.ToInt32(strMaxCodeAfter) + 1).ToString();

                        if (strMaxCodeAfter.Length != iMaxCodeAfterLength)
                        {
                            strMaxCodeAfter = strMaxCodeAfter.PadLeft(iMaxCodeAfterLength, '0');
                        }

                        strMaxCode = strMaxCodeBefore + strMaxCodeAfter;
                    }
                }
                else
                {
                    strMaxCode = strDefaultValue;
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message.ToString();
            }

            txtControl.Text = strMaxCode;
            txtControl.ReadOnly = true;
            txtControl.BorderColor = Color.DarkGray;
            txtControl.BorderStyle = BorderStyle.None;
        }

        public static void TextBoxAutoIncreaseCodeBindNumber(System.Web.UI.WebControls.TextBox txtControl, string strTableName, string strPrimaryKeyName)
        {
            string strMaxCode = string.Empty;

            try
            {
                string strSql = "Select EnumValue From dbo.T_EnumValue Where EnumValueName='" + strTableName + "'";
                string strDefaultValue = DbHelperSQL.GetSingle(strSql).ToString();

                strSql = "Select Max(Convert(int," + strPrimaryKeyName + ")) From " + strTableName;// +" Order By " + strPrimaryKeyName + " desc";

                object objScalar = DbHelperSQL.GetSingle(strSql);

                if (objScalar != null && objScalar.ToString() != "")
                {
                    strMaxCode = objScalar.ToString();

                    //if (strMaxCode.Length > 0)
                    //{
                        //string strMaxCodeBefore = strMaxCode.Substring(0, 1);
                        string strMaxCodeAfter = strMaxCode;//.Substring(1);
                        int iMaxCodeAfterLength = strMaxCodeAfter.Length;

                        strMaxCodeAfter = (Convert.ToInt32(strMaxCodeAfter) + 1).ToString();

                        //if (strMaxCodeAfter.Length != iMaxCodeAfterLength)
                        //{
                        //    strMaxCodeAfter = strMaxCodeAfter.PadLeft(iMaxCodeAfterLength, '0');
                        //}

                        strMaxCode = strMaxCodeAfter;   //   strMaxCodeBefore +
                    //}
                }
                else
                {
                    strMaxCode = strDefaultValue;
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message.ToString();
            }

            txtControl.Text = strMaxCode;
            txtControl.ReadOnly = true;
            txtControl.BorderColor = Color.DarkGray;
            txtControl.BorderStyle = BorderStyle.None;
        }
        /// <summary>
        /// ��TextBox���
        /// </summary>
        /// <param name="txtControl">TextBox�ؼ�ID</param>
        /// <param name="strTableName">����</param>
        /// <param name="strPrimaryKeyName">����</param>
        /// <param name="strWhere">��ѯ����</param>
        public static void TextBoxAutoIncreaseCodeBind(System.Web.UI.WebControls.TextBox txtControl, string strTableName, string strPrimaryKeyName,string strWhere)
        {
            string strMaxCode = string.Empty;
            
            try
            {
                string strSql = "Select EnumValue From dbo.T_EnumValue Where EnumValueName='" + strTableName + "'";
                string strDefaultValue = DbHelperSQL.GetSingle(strSql).ToString();

                strSql = "Select Top 1 " + strPrimaryKeyName + " From " + strTableName + " Where " + strWhere + " Order By " + strPrimaryKeyName + " desc";

                object objScalar = DbHelperSQL.GetSingle(strSql);

                if (objScalar != null && objScalar.ToString() != "")
                {
                    strMaxCode = objScalar.ToString();

                    if (strMaxCode.Length > 2)
                    {
                        string strMaxCodeBefore = strMaxCode.Substring(0, 1);
                        string strMaxCodeAfter = strMaxCode.Substring(1);
                        int iMaxCodeAfterLength = strMaxCodeAfter.Length;

                        strMaxCodeAfter = (Convert.ToInt32(strMaxCodeAfter) + 1).ToString();

                        if (strMaxCodeAfter.Length != iMaxCodeAfterLength)
                        {
                            strMaxCodeAfter = strMaxCodeAfter.PadLeft(iMaxCodeAfterLength, '0');
                        }

                        strMaxCode = strMaxCodeBefore + strMaxCodeAfter;
                    }
                }
                else
                {
                    strMaxCode = strDefaultValue;
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message.ToString();
            }

            txtControl.Text = strMaxCode;
            txtControl.ReadOnly = true;
            txtControl.BorderColor = Color.DarkGray;
            txtControl.BorderStyle = BorderStyle.None;
        }
        /// <summary>
        /// ��TextBox���
        /// </summary>
        /// <param name="txtControl">TextBox�ؼ�ID</param>
        /// <param name="strTableName">����</param>
        /// <param name="strPrimaryKeyName">����</param>
        /// <param name="strWhere">��ѯ����</param>
        public static void TextBoxAutoIncreaseCodeBindNumber(System.Web.UI.WebControls.TextBox txtControl, string strTableName, string strPrimaryKeyName, string strWhere)
        {
            string strMaxCode = string.Empty;

            try
            {
                string strSql = "Select EnumValue From dbo.T_EnumValue Where EnumValueName='" + strTableName + "'";
                string strDefaultValue = DbHelperSQL.GetSingle(strSql).ToString();

                strSql = "Select Max(Convert(int," + strPrimaryKeyName + ")) From " + strTableName + " Where " + strWhere;// +" Order By " + strPrimaryKeyName + " desc";

                object objScalar = DbHelperSQL.GetSingle(strSql);

                if (objScalar != null && objScalar.ToString() != "")
                {
                    strMaxCode = objScalar.ToString();

                    if (strMaxCode.Length > 0)
                    {
                        //string strMaxCodeBefore = strMaxCode.Substring(0, 1);
                        string strMaxCodeAfter = strMaxCode;//.Substring(1);
                        int iMaxCodeAfterLength = strMaxCodeAfter.Length;

                        strMaxCodeAfter = (Convert.ToInt32(strMaxCodeAfter) + 1).ToString();

                        if (strMaxCodeAfter.Length != iMaxCodeAfterLength)
                        {
                            strMaxCodeAfter = strMaxCodeAfter.PadLeft(iMaxCodeAfterLength, '0');
                        }

                        strMaxCode = strMaxCodeAfter;   //  strMaxCodeBefore + 
                    }
                }
                else
                {
                    strMaxCode = strDefaultValue;
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message.ToString();
            }

            txtControl.Text = strMaxCode;
            txtControl.ReadOnly = true;
            txtControl.BorderColor = Color.DarkGray;
            txtControl.BorderStyle = BorderStyle.None;
        }
        #endregion

        #region �޿ؼ�������ID���ֵ
        /// <summary>
        /// �޿ؼ���
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strPrimaryKeyName">����</param>
        /// <returns>����ID���ֵ</returns>
        public static string UnControlAutoIncreaseCode(string strTableName, string strPrimaryKeyName)
        {
            string strMaxCode = string.Empty;

            try
            {
                string strSql = "Select EnumValue From dbo.T_EnumValue Where EnumValueName='" + strTableName + "'";
                string strDefaultValue = DbHelperSQL.GetSingle(strSql).ToString();

                strSql = "Select Top 1 " + strPrimaryKeyName + " From " + strTableName + " Order By " + strPrimaryKeyName + " desc";

                object objScalar = DbHelperSQL.GetSingle(strSql);

                if (objScalar != null && objScalar.ToString() != "")
                {
                    strMaxCode = objScalar.ToString();

                    if (strMaxCode.Length > 2)
                    {
                        string strMaxCodeBefore = strMaxCode.Substring(0, 1);
                        string strMaxCodeAfter = strMaxCode.Substring(1);
                        int iMaxCodeAfterLength = strMaxCodeAfter.Length;

                        strMaxCodeAfter = (Convert.ToInt32(strMaxCodeAfter) + 1).ToString();

                        if (strMaxCodeAfter.Length != iMaxCodeAfterLength)
                        {
                            strMaxCodeAfter = strMaxCodeAfter.PadLeft(iMaxCodeAfterLength, '0');
                        }

                        strMaxCode = strMaxCodeBefore + strMaxCodeAfter;
                    }
                }
                else
                {
                    strMaxCode = strDefaultValue;
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message.ToString();
            }

            return strMaxCode;
        }

        /// <summary>
        /// �޿ؼ���
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strPrimaryKeyName">����</param>
        /// <returns>����ID���ֵ</returns>
        public static string UnControlAutoIncreaseCodeNumber(string strTableName, string strPrimaryKeyName)
        {
            string strMaxCode = string.Empty;

            try
            {
                string strSql = "Select EnumValue From dbo.T_EnumValue Where EnumValueName='" + strTableName + "'";
                string strDefaultValue = DbHelperSQL.GetSingle(strSql).ToString();

                strSql = "Select Max(Convert(int," + strPrimaryKeyName + ")) From " + strTableName;// +" Order By " + strPrimaryKeyName + " desc";

                object objScalar = DbHelperSQL.GetSingle(strSql);

                if (objScalar != null && objScalar.ToString() != "")
                {
                    strMaxCode = objScalar.ToString();

                    //if (strMaxCode.Length > 2)
                    //{
                        //string strMaxCodeBefore = strMaxCode.Substring(0, 1);
                        string strMaxCodeAfter = strMaxCode;//.Substring(1);
                        //int iMaxCodeAfterLength = strMaxCodeAfter.Length;

                        strMaxCodeAfter = (Convert.ToInt32(strMaxCodeAfter) + 1).ToString();

                        //if (strMaxCodeAfter.Length != iMaxCodeAfterLength)
                        //{
                        //    strMaxCodeAfter = strMaxCodeAfter.PadLeft(iMaxCodeAfterLength, '0');
                        //}

                        strMaxCode = strMaxCodeAfter;//strMaxCodeBefore + 
                    //}
                }
                else
                {
                    strMaxCode = strDefaultValue;
                    //strMaxCode = "1";
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message.ToString();
            }

            return strMaxCode;
        }
        /// <summary>
        /// �޿ؼ���
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strPrimaryKeyName">����</param>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns>����ID���ֵ</returns>
        public static string UnControlAutoIncreaseCode(string strTableName, string strPrimaryKeyName,string strWhere)
        {
            string strMaxCode = string.Empty;
            
            try
            {
                string strSql = "Select EnumValue From dbo.T_EnumValue Where EnumValueName='" + strTableName + "'";
                string strDefaultValue = DbHelperSQL.GetSingle(strSql).ToString();

                strSql = "Select Top 1 " + strPrimaryKeyName + " From " + strTableName + " Where" + strWhere + " Order By " + strPrimaryKeyName + " desc";

                object objScalar = DbHelperSQL.GetSingle(strSql);

                if (objScalar != null && objScalar.ToString() != "")
                {
                    strMaxCode = objScalar.ToString();

                    if (strMaxCode.Length > 2)
                    {
                        string strMaxCodeBefore = strMaxCode.Substring(0, 1);
                        string strMaxCodeAfter = strMaxCode.Substring(1);
                        int iMaxCodeAfterLength = strMaxCodeAfter.Length;

                        strMaxCodeAfter = (Convert.ToInt32(strMaxCodeAfter) + 1).ToString();

                        if (strMaxCodeAfter.Length != iMaxCodeAfterLength)
                        {
                            strMaxCodeAfter = strMaxCodeAfter.PadLeft(iMaxCodeAfterLength, '0');
                        }

                        strMaxCode = strMaxCodeBefore + strMaxCodeAfter;
                    }
                }
                else
                {
                    strMaxCode = strDefaultValue;
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message.ToString();
            }

            return strMaxCode;
        }
        
        #endregion

        #region �����ؼ�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeview">���ؼ���ID</param>
        /// <param name="tablename">������</param>
        /// <param name="fieldText">��������</param>
        /// <param name="fieldValue">���ݱ��</param>
        /// <param name="FatherCode">�������</param>
        /// <param name="FatherValue">������������</param>
        /// <param name="condition">�������</param>
        public static void BindTreeview(TreeView treeview, string tablename, string fieldText, string fieldValue, string FatherCode, string FatherValue, string condition)
        {
            string str = "select " + fieldText + "," + fieldValue + "," + FatherCode + " from " + tablename + "  where " + condition;
            DataTable dt = DbHelperSQL.Query(str).Tables[0];
            DataRow[] Arr_datarow;
            //��������Ҫ�������Ա��Ȩ�������û�������
            if (FatherValue != "0")
            {
                Arr_datarow = dt.Select(fieldValue + "='" + FatherValue + "'");
            }
            else
            {
                Arr_datarow = dt.Select(FatherCode + "='" + FatherValue + "'");
            }
            treeview.Nodes.Clear();
            if (Arr_datarow.Length <= 0) return;
            foreach (DataRow dr in Arr_datarow)
            {
                TreeNode rootnode = new TreeNode();
                rootnode.Text = dr[fieldText].ToString().Trim();
                rootnode.Value = dr[fieldValue].ToString().Trim();
                //rootnode.SelectAction = TreeNodeSelectAction.Expand;
                treeview.Nodes.Add(rootnode);
                BindSubNode(dt, rootnode, fieldText, fieldValue, FatherCode);
            }
        }
        private static void BindSubNode(DataTable dtTable, TreeNode fatherNode, string fieldText, string fieldValue, string fathercode)
        {
            DataRow[] arr_datarow = dtTable.Select(fathercode + " = '" + fatherNode.Value.ToString().Trim() + "'");
            if (arr_datarow.Length <= 0) return;
            foreach (DataRow dr in arr_datarow)
            {
                TreeNode node = new TreeNode();
                node.Text = dr[fieldText].ToString().Trim();
                node.Value = dr[fieldValue].ToString().Trim();
                fatherNode.ChildNodes.Add(node);
                BindSubNode(dtTable, node, fieldText, fieldValue, fathercode);
            }

        }
        #endregion


        /// <summary>
        ///��ȡ����δ���ð�����IP
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllRoomIP()
        {
            string strSendDataObject = System.Configuration.ConfigurationManager.AppSettings["SendDataObject"].ToString();
            List<string> listIp = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct RoomIp from TT_ROOM  where IsForbid='0'");
            if (strSendDataObject != "")
                strSql.Append(" and RoomType='" + strSendDataObject + "'");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    listIp.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
                }
            }
            return listIp;
        }



        #region ���ݱ�ID�õ����е�ĳһ�ֶ�
        /// <summary>
        /// ���ݱ�ID�õ����е�ĳһ�ֶ�
        /// </summary>
        /// <param name="strTable">����/��ͼ��</param>
        /// <param name="strFieldName">��Ҫ�õ����ֶ���</param>
        /// <param name="where">��������</param>
        /// <returns>���ز�ѯ�ֶ�ֵ</returns>
        public static string GetFiledValue(string strTable, string strFieldName,string where)
        {

            string strsel = "SELECT " + strFieldName + " FROM " + strTable + " where 1=1";
            if (where != "")
                strsel += " and " + where + " ";
            try
            {
                SqlDataReader sdr = DbHelperSQL.ExecuteReader(strsel);
                if (sdr.Read())
                {
                    return sdr[0].ToString();
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }
        #endregion
        
      
    }
       
}
