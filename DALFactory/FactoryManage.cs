using System;
using System.Reflection;
using System.Configuration;
using IndustryPlatform.IDAL;

namespace IndustryPlatform.DALFactory
{
    /// <summary>
    /// ���󹤳�ģʽ����DAL��
    /// web.config ��Ҫ�������ã�(���ù���ģʽ+�������+�������,ʵ�ֶ�̬������ͬ�����ݲ����ӿ�)  
    /// DataCache���ڵ���������ļ�����
    /// ���԰�����DAL��Ĵ����������DataAccess����
    /// <appSettings>  
    /// <add key="DAL" value="LiTianPing.SQLServerDAL" /> (����������ռ����ʵ���������Ϊ�Լ���Ŀ�������ռ�)
    /// </appSettings> 
    /// </summary>
    public sealed class DataAccess
    {
        private static readonly string path = System.Configuration.ConfigurationSettings.AppSettings["DAL"];
        /// <summary>
        /// ���������ӻ����ȡ
        /// </summary>
        public static object CreateObject(string path, string CacheKey)
        {
            object objType = DataCache.GetCache(CacheKey);//�ӻ����ȡ
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(CacheKey);//���䴴��
                    DataCache.SetCache(CacheKey, objType);// д�뻺��
                }
                catch
                { }
            }
            return objType;
        }
        /// <summary>
        /// ������Ա���ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISYS_Operator CreateSYS_Operator()
        {
            string CacheKey = path + ".SYS_OperatorDao";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_Operator)objType;
        }


        /// <summary>
        /// ������֯�������ݲ�ӿ�
        /// </summary>
        /// <returns></returns>
        public static IndustryPlatform.IDAL.ISYS_Organization CreateSYS_Organization()
        {
            string CacheKey = path + ".SYS_Organization";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_Organization)objType;
        }

        /// <summary>
        /// �����˵����ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISYS_Menu CreateSYS_Menu()
        {
            string CacheKey = path + ".SYS_Menu";
            object objType = CreateObject(path,CacheKey);
            return (IndustryPlatform.IDAL.ISYS_Menu)objType;
        }

        /// <summary>
        /// ������λ���ݲ�ӿ�
        /// </summary>
        /// <returns></returns>
        public static IndustryPlatform.IDAL.ISYS_Position CreateSYS_Position()
        {
            string CacheKey = path + ".SYS_PositionDao";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_Position)objType;
        }

        /// <summary>
        /// �����������ݲ�ӿ�
        /// </summary>
        /// <returns></returns>
        public static IndustryPlatform.IDAL.ISYS_Leaveword CreateSYS_Leaveword()
        {
            string CacheKey = path + ".SYS_LeavewordDao";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_Leaveword)objType;
        }

        /// <summary>
        /// ����Sys_Colliery���ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISys_Colliery CreateSys_Colliery()
        {

            string CacheKey = path + ".Sys_Colliery";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISys_Colliery)objType;
        }

        /// <summary>
        /// ����SYS_Dictionary���ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISYS_Dictionary CreateSYS_Dictionary()
        {

            string CacheKey = path + ".SYS_DictionaryDao";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_Dictionary)objType;
        }


        /// <summary>
        /// ����SYS_FailerSendMessage���ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISYS_FailerSendMessage CreateSYS_FailerSendMessage()
        {

            string CacheKey = path + ".SYS_FailerSendMessageSQLDAL";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_FailerSendMessage)objType;
        }

        /// <summary>
        /// ����SYS_ReadySendMessage���ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISYS_ReadySendMessage CreateSYS_ReadySendMessage()
        {

            string CacheKey = path + ".SYS_ReadySendMessageSQLDAL";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_ReadySendMessage)objType;
        }


        /// <summary>
        /// ����SYS_ReceiveMessage���ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISYS_ReceiveMessage CreateSYS_ReceiveMessage()
        {

            string CacheKey = path + ".SYS_ReceiveMessageSQLDAL";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_ReceiveMessage)objType;
        }


        /// <summary>
        /// ����SYS_SucceedSendMessage���ݲ�ӿ�
        /// </summary>
        public static IndustryPlatform.IDAL.ISYS_SucceedSendMessage CreateSYS_SucceedSendMessage()
        {

            string CacheKey = path + ".SYS_SucceedSendMessageSQLDAL";
            object objType = CreateObject(path, CacheKey);
            return (IndustryPlatform.IDAL.ISYS_SucceedSendMessage)objType;
        }

    }
}
