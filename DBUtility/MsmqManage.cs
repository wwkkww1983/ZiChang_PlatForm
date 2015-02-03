using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Messaging;
using System.Data;
using System.Transactions;

namespace IndustryPlatform.DBUtility
{
    public class MsmqManage
    {
       

        public static string RoomCode="0";
        private static MsmqManage MQ=null;
        string _StrRoomIP = "";
        private MessageQueue _MqSqlClient;
        private  const string _ServiceMachinePrefix = @"FormatName:Direct=TCP:";

        // ͬ������(ȫ��0������վ1����Ʊվ2��������3)|����|��������(��0����1��ɾ2)|ʱ���|Sql���
        //�ָ����
        public  string Prefix = "|";
        //ͬ������
        public  int AllStation = 0;
        public  int BangStation = 1;
        public  int CheckStation = 2;
        public  int NotSend = 3;
        //��������
        public  int AddFlg = 0;
        public  int EditFlg = 1;
        public  int DelFlg = 2;
       

        private MsmqManage()
        {
            DataTable dt = DbHelperSQL.TQuery("select RoomIp,RoomName from TT_Room where RoomCode='0'");
            _StrRoomIP = dt.Rows[0][0].ToString();
           
        }

        //��ȡ��̬MsmqManage���� ����
        public static MsmqManage GetMsmq()
        {
            if (MQ == null)
            {
                MQ = new MsmqManage();
            }

            return MQ;
        }
        
        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="strSql"></param>
        public void AddMsmq(string strSql)
        {
           
            System.Threading.Thread thReciver = new System.Threading.Thread(new ParameterizedThreadStart(SendSqlMessageThread));
            thReciver.IsBackground = true; thReciver.SetApartmentState(System.Threading.ApartmentState.STA); thReciver.Start(strSql);
        }

        public void AddMsmq(string strSql, string strIP)
        {
            _MqSqlClient = new MessageQueue(_ServiceMachinePrefix + strIP + "\\private$\\SqlServer");
            
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();

            message.Label = RoomCode;
            message.Body = strSql;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _MqSqlClient.Send(message, MessageQueueTransactionType.Single);
                    scope.Complete();
                }
                catch (MessageQueueException exc)
                {
                }
            }
        }



        private void SendSqlMessageThread(object objSql)
        {
            
            //string strRoomName = dt.Rows[i][1].ToString();

            _MqSqlClient = new MessageQueue(_ServiceMachinePrefix + _StrRoomIP + "\\private$\\SqlServer");

            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();

            message.Label = RoomCode;
            message.Body = objSql;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _MqSqlClient.Send(message, MessageQueueTransactionType.Single);
                    scope.Complete();
                }
                catch (MessageQueueException exc)
                {
                }
            }
        }
        //private void SendSqlMessageThread(object objSql)
        //{
        //    //????????IP?????????
        //    DataTable dt = DbHelperSQL.TQuery("select RoomIp,RoomName from TT_Room where RoomCode<>'0'");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string strRoomIP = dt.Rows[i][0].ToString();
        //        string strRoomName = dt.Rows[i][1].ToString();

        //        _MqSqlClient = new MessageQueue(_ServiceMachinePrefix + strRoomIP + "\\private$\\SqlServer");

        //        Message message = new Message();
        //        message.Formatter = new BinaryMessageFormatter();

        //        message.Label = strRoomIP;
        //        message.Body = objSql;
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            try
        //            {
        //                _MqSqlClient.Send(message, MessageQueueTransactionType.Single);
        //                scope.Complete();
        //            }
        //            catch (MessageQueueException exc)
        //            {
        //            }
        //        }
        //    }
        //}
    }
}