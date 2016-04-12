﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RoadFlow.Data.MSSQL
{
    public class WorkFlowTask : RoadFlow.Data.Interface.IWorkFlowTask
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowTask()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.WorkFlowTask实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.WorkFlowTask model)
        {
            string sql = @"INSERT INTO WorkFlowTask
				(ID,PrevID,PrevStepID,FlowID,StepID,StepName,InstanceID,GroupID,Type,Title,SenderID,SenderName,SenderTime,ReceiveID,ReceiveName,ReceiveTime,OpenTime,CompletedTime,CompletedTime1,Comment,IsSign,Status,Note,Sort,SubFlowGroupID) 
				VALUES(@ID,@PrevID,@PrevStepID,@FlowID,@StepID,@StepName,@InstanceID,@GroupID,@Type,@Title,@SenderID,@SenderName,@SenderTime,@ReceiveID,@ReceiveName,@ReceiveTime,@OpenTime,@CompletedTime,@CompletedTime1,@Comment,@IsSign,@Status,@Note,@Sort,@SubFlowGroupID)";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1){ Value = model.ID },
				new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier, -1){ Value = model.PrevID },
				new SqlParameter("@PrevStepID", SqlDbType.UniqueIdentifier, -1){ Value = model.PrevStepID },
				new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1){ Value = model.FlowID },
				new SqlParameter("@StepID", SqlDbType.UniqueIdentifier, -1){ Value = model.StepID },
				new SqlParameter("@StepName", SqlDbType.NVarChar, 1000){ Value = model.StepName },
				new SqlParameter("@InstanceID", SqlDbType.VarChar, 50){ Value = model.InstanceID },
				new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, -1){ Value = model.GroupID },
				new SqlParameter("@Type", SqlDbType.Int, -1){ Value = model.Type },
				new SqlParameter("@Title", SqlDbType.NVarChar, 4000){ Value = model.Title },
				new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier, -1){ Value = model.SenderID },
				new SqlParameter("@SenderName", SqlDbType.NVarChar, 100){ Value = model.SenderName },
				new SqlParameter("@SenderTime", SqlDbType.DateTime, 8){ Value = model.SenderTime },
				new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier, -1){ Value = model.ReceiveID },
				new SqlParameter("@ReceiveName", SqlDbType.NVarChar, 100){ Value = model.ReceiveName },
				new SqlParameter("@ReceiveTime", SqlDbType.DateTime, 8){ Value = model.ReceiveTime },
				model.OpenTime == null ? new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = model.OpenTime },
				model.CompletedTime == null ? new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = model.CompletedTime },
				model.CompletedTime1 == null ? new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = model.CompletedTime1 },
				model.Comment == null ? new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = model.Comment },
				model.IsSign == null ? new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = model.IsSign },
				new SqlParameter("@Status", SqlDbType.Int, -1){ Value = model.Status },
				model.Note == null ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note },
				new SqlParameter("@Sort", SqlDbType.Int, -1){ Value = model.Sort },
				model.SubFlowGroupID == null ? new SqlParameter("@SubFlowGroupID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@SubFlowGroupID", SqlDbType.UniqueIdentifier, -1) { Value = model.SubFlowGroupID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.WorkFlowTask实体类</param>
        public int Update(RoadFlow.Data.Model.WorkFlowTask model)
        {
            string sql = @"UPDATE WorkFlowTask SET 
				PrevID=@PrevID,PrevStepID=@PrevStepID,FlowID=@FlowID,StepID=@StepID,StepName=@StepName,InstanceID=@InstanceID,GroupID=@GroupID,Type=@Type,Title=@Title,SenderID=@SenderID,SenderName=@SenderName,SenderTime=@SenderTime,ReceiveID=@ReceiveID,ReceiveName=@ReceiveName,ReceiveTime=@ReceiveTime,OpenTime=@OpenTime,CompletedTime=@CompletedTime,CompletedTime1=@CompletedTime1,Comment=@Comment,IsSign=@IsSign,Status=@Status,Note=@Note,Sort=@Sort,SubFlowGroupID=@SubFlowGroupID
				WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier, -1){ Value = model.PrevID },
				new SqlParameter("@PrevStepID", SqlDbType.UniqueIdentifier, -1){ Value = model.PrevStepID },
				new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1){ Value = model.FlowID },
				new SqlParameter("@StepID", SqlDbType.UniqueIdentifier, -1){ Value = model.StepID },
				new SqlParameter("@StepName", SqlDbType.NVarChar, 1000){ Value = model.StepName },
				new SqlParameter("@InstanceID", SqlDbType.VarChar, 50){ Value = model.InstanceID },
				new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, -1){ Value = model.GroupID },
				new SqlParameter("@Type", SqlDbType.Int, -1){ Value = model.Type },
				new SqlParameter("@Title", SqlDbType.NVarChar, 4000){ Value = model.Title },
				new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier, -1){ Value = model.SenderID },
				new SqlParameter("@SenderName", SqlDbType.NVarChar, 100){ Value = model.SenderName },
				new SqlParameter("@SenderTime", SqlDbType.DateTime, 8){ Value = model.SenderTime },
				new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier, -1){ Value = model.ReceiveID },
				new SqlParameter("@ReceiveName", SqlDbType.NVarChar, 100){ Value = model.ReceiveName },
				new SqlParameter("@ReceiveTime", SqlDbType.DateTime, 8){ Value = model.ReceiveTime },
				model.OpenTime == null ? new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = model.OpenTime },
				model.CompletedTime == null ? new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = model.CompletedTime },
				model.CompletedTime1 == null ? new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = model.CompletedTime1 },
				model.Comment == null ? new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = model.Comment },
				model.IsSign == null ? new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = model.IsSign },
				new SqlParameter("@Status", SqlDbType.Int, -1){ Value = model.Status },
				model.Note == null ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note },
				new SqlParameter("@Sort", SqlDbType.Int, -1){ Value = model.Sort },
				model.SubFlowGroupID == null ? new SqlParameter("@SubFlowGroupID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@SubFlowGroupID", SqlDbType.UniqueIdentifier, -1) { Value = model.SubFlowGroupID },
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.WorkFlowTask> DataReaderToList(SqlDataReader dataReader)
        {
            List<RoadFlow.Data.Model.WorkFlowTask> List = new List<RoadFlow.Data.Model.WorkFlowTask>();
            RoadFlow.Data.Model.WorkFlowTask model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.WorkFlowTask();
                model.ID = dataReader.GetGuid(0);
                model.PrevID = dataReader.GetGuid(1);
                model.PrevStepID = dataReader.GetGuid(2);
                model.FlowID = dataReader.GetGuid(3);
                model.StepID = dataReader.GetGuid(4);
                model.StepName = dataReader.GetString(5);
                model.InstanceID = dataReader.GetString(6);
                model.GroupID = dataReader.GetGuid(7);
                model.Type = dataReader.GetInt32(8);
                model.Title = dataReader.GetString(9);
                model.SenderID = dataReader.GetGuid(10);
                model.SenderName = dataReader.GetString(11);
                model.SenderTime = dataReader.GetDateTime(12);
                model.ReceiveID = dataReader.GetGuid(13);
                model.ReceiveName = dataReader.GetString(14);
                model.ReceiveTime = dataReader.GetDateTime(15);
                if (!dataReader.IsDBNull(16))
                    model.OpenTime = dataReader.GetDateTime(16);
                if (!dataReader.IsDBNull(17))
                    model.CompletedTime = dataReader.GetDateTime(17);
                if (!dataReader.IsDBNull(18))
                    model.CompletedTime1 = dataReader.GetDateTime(18);
                if (!dataReader.IsDBNull(19))
                    model.Comment = dataReader.GetString(19);
                if (!dataReader.IsDBNull(20))
                    model.IsSign = dataReader.GetInt32(20);
                model.Status = dataReader.GetInt32(21);
                if (!dataReader.IsDBNull(22))
                    model.Note = dataReader.GetString(22);
                model.Sort = dataReader.GetInt32(23);
                if (!dataReader.IsDBNull(24))
                    model.SubFlowGroupID = dataReader.GetGuid(24);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowTask";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowTask";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.WorkFlowTask Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 删除一组实例
        /// </summary>
        public int Delete(Guid flowID, Guid groupID)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE GroupID=@GroupID";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			};
            if (!flowID.IsEmptyGuid())
            {
                sql += " AND FlowID=@FlowID";
                parameters.Add(new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) { Value = flowID });
            }
            return dbHelper.Execute(sql, parameters.ToArray());
        }
       
        /// <summary>
        /// 更新打开时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="openTime"></param>
        /// <param name="isStatus">是否将状态更新为1</param>
        public void UpdateOpenTime(Guid id, DateTime openTime, bool isStatus = false)
        {
            string sql = "UPDATE WorkFlowTask SET OpenTime=@OpenTime " + (isStatus ? ", Status=1" : "") + " WHERE ID=@ID AND OpenTime IS NULL";
            
            SqlParameter[] parameters = new SqlParameter[]{
                openTime==DateTime.MinValue? new SqlParameter("@OpenTime", SqlDbType.DateTime){ Value = DBNull.Value} :
                    new SqlParameter("@OpenTime", SqlDbType.DateTime){ Value = openTime },
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            dbHelper.Execute(sql, parameters);
        }


        /// <summary>
        /// 查询待办任务
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="type">0待办 1已完成</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, string query="", string title="", string flowid="", string sender="", string date1="", string date2="", int type=0)
        {
            List<SqlParameter> parList = new List<SqlParameter>();
            StringBuilder sql = new StringBuilder("SELECT *,ROW_NUMBER() OVER(ORDER BY " + (type == 0 ? "ReceiveTime DESC" : "CompletedTime1 DESC") + ") AS PagerAutoRowNumber FROM WorkFlowTask WHERE ReceiveID=@ReceiveID");
            sql.Append(type == 0 ? " AND Status IN(0,1)" : " AND Status IN(2,3)");
            parList.Add(new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) { Value = userID });
            if (!title.IsNullOrEmpty())
            {
                sql.Append(" AND CHARINDEX(@Title,Title)>0");
                parList.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 2000) { Value = title });
            }
            if (flowid.IsGuid())
            {
                sql.Append(" AND FlowID=@FlowID");
                parList.Add(new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) { Value = flowid.ToGuid() });
            }
            else if (!flowid.IsNullOrEmpty() && flowid.IndexOf(',') >= 0)
            {
                sql.Append(" AND FlowID IN(" + RoadFlow.Utility.Tools.GetSqlInString(flowid) + ")");
            }
            if (sender.IsGuid())
            {
                sql.Append(" AND SenderID=@SenderID");
                parList.Add(new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) { Value = sender.ToGuid() });
            }
            if (date1.IsDateTime())
            {
                sql.Append(" AND ReceiveTime>=@ReceiveTime");
                parList.Add(new SqlParameter("@ReceiveTime", SqlDbType.DateTime) { Value = date1.ToDateTime().Date });
            }
            if (date2.IsDateTime())
            {
                sql.Append(" AND ReceiveTime<=@ReceiveTime1");
                parList.Add(new SqlParameter("@ReceiveTime1", SqlDbType.DateTime) { Value = date2.ToDateTime().AddDays(1).Date });
            }
            
            long count;
            int size = RoadFlow.Utility.Tools.GetPageSize();
            int number = RoadFlow.Utility.Tools.GetPageNumber();
            string sql1 = dbHelper.GetPaerSql(sql.ToString(), size, number, out count, parList.ToArray());
            pager = RoadFlow.Utility.Tools.GetPagerHtml(count, size, number, query);

            
            SqlDataReader dataReader = dbHelper.GetDataReader(sql1, parList.ToArray());
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到流程实例列表
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="senderID"></param>
        /// <param name="receiveID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="status">是否完成 0:全部 1:未完成 2:已完成</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, string query = "", string title = "", string flowid = "", string date1 = "", string date2 = "", int status = 0)
        {
            List<SqlParameter> parList = new List<SqlParameter>();
            StringBuilder sql = new StringBuilder(@"SELECT a.*,ROW_NUMBER() OVER(ORDER BY a.SenderTime DESC) AS PagerAutoRowNumber FROM WorkFlowTask a
                WHERE a.ID=(SELECT TOP 1 ID FROM WorkFlowTask WHERE FlowID=a.FlowID AND GroupID=a.GroupID ORDER BY Sort DESC)");

            if (status != 0)
            {
                if (status == 1)
                {
                    sql.Append(" AND a.Status IN(0,1,5)");
                }
                else if (status == 2)
                {
                    sql.Append(" AND a.Status IN(2,3,4)");
                }
            }
            
            if (flowID != null && flowID.Length > 0)
            {
                sql.Append(string.Format(" AND a.FlowID IN({0})", RoadFlow.Utility.Tools.GetSqlInString(flowID)));
            }
            if (senderID != null && senderID.Length > 0)
            {
                if (senderID.Length == 1)
                {
                    sql.Append(" AND a.SenderID=@SenderID");
                    parList.Add(new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) { Value = senderID[0] });
                }
                else
                {
                    sql.Append(string.Format(" AND a.SenderID IN({0})", RoadFlow.Utility.Tools.GetSqlInString(senderID)));
                }
            }
            if (receiveID != null && receiveID.Length > 0)
            {
                if (senderID.Length == 1)
                {
                    sql.Append(" AND a.ReceiveID=@ReceiveID");
                    parList.Add(new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) { Value = receiveID[0] });
                }
                else
                {
                    sql.Append(string.Format(" AND a.ReceiveID IN({0})", RoadFlow.Utility.Tools.GetSqlInString(receiveID)));
                }
            }
            if (!title.IsNullOrEmpty())
            {
                sql.Append(" AND CHARINDEX(@Title,a.Title)>0");
                parList.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 2000) { Value = title });
            }
            if (flowid.IsGuid())
            {
                sql.Append(" AND a.FlowID=@FlowID");
                parList.Add(new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) { Value = flowid.ToGuid() });
            }
            else if (!flowid.IsNullOrEmpty() && flowid.IndexOf(',') >= 0)
            {
                sql.Append(" AND a.FlowID IN(" + RoadFlow.Utility.Tools.GetSqlInString(flowid) + ")");
            }
            if (date1.IsDateTime())
            {
                sql.Append(" AND a.SenderTime>=@SenderTime");
                parList.Add(new SqlParameter("@SenderTime", SqlDbType.DateTime) { Value = date1.ToDateTime().Date });
            }
            if (date2.IsDateTime())
            {
                sql.Append(" AND a.SenderTime<=@SenderTime1");
                parList.Add(new SqlParameter("@SenderTime1", SqlDbType.DateTime) { Value = date1.ToDateTime().AddDays(1).Date });
            }

            long count;
            int size = RoadFlow.Utility.Tools.GetPageSize();
            int number = RoadFlow.Utility.Tools.GetPageNumber();
            string sql1 = dbHelper.GetPaerSql(sql.ToString(), size, number, out count, parList.ToArray());
            pager = RoadFlow.Utility.Tools.GetPagerHtml(count, size, number, query);
          
            SqlDataReader dataReader = dbHelper.GetDataReader(sql1, parList.ToArray());
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个流程实例的发起者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public Guid GetFirstSnderID(Guid flowID, Guid groupID)
        {
            string sql = "SELECT SenderID FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID AND PrevID=@PrevID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID },
                new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier){ Value = Guid.Empty }
			};
            string senderID = dbHelper.GetFieldValue(sql, parameters);
            return senderID.ToGuid();
        }

        /// <summary>
        /// 得到一个流程实例一个步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			};
            DataTable dt = dbHelper.GetDataTable(sql, parameters);
            List<Guid> senderList = new List<Guid>();
            foreach (DataRow dr in dt.Rows)
            {
                Guid senderID;
                if (Guid.TryParse(dr[0].ToString(), out senderID))
                {
                    senderList.Add(senderID);
                }
            }
            return senderList;
        }
        /// <summary>
        /// 得到一个流程实例前一步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID)";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			};
            DataTable dt = dbHelper.GetDataTable(sql, parameters);
            List<Guid> senderList = new List<Guid>();
            foreach (DataRow dr in dt.Rows)
            {
                Guid senderID;
                if (Guid.TryParse(dr[0].ToString(), out senderID))
                {
                    senderList.Add(senderID);
                }
            }
            return senderList;
        }

        /// <summary>
        /// 完成一个任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="comment"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        public int Completed(Guid taskID, string comment = "", bool isSign = false, int status = 2, string note="")
        {
            string sql = "UPDATE WorkFlowTask SET Comment=@Comment,CompletedTime1=@CompletedTime1,IsSign=@IsSign,Status=@Status" + (note.IsNullOrEmpty() ? "" : ",Note=@Note") + " WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				comment.IsNullOrEmpty() ? new SqlParameter("@Comment", SqlDbType.VarChar){ Value = DBNull.Value } : new SqlParameter("@Comment", SqlDbType.VarChar){ Value = comment },
                new SqlParameter("@CompletedTime1", SqlDbType.DateTime){ Value = RoadFlow.Utility.DateTimeNew.Now },
                new SqlParameter("@IsSign", SqlDbType.Int){ Value = isSign?1:0 },
                new SqlParameter("@Status", SqlDbType.Int){ Value = status },
                note.IsNullOrEmpty()?new SqlParameter("@Note", SqlDbType.NVarChar){ Value = DBNull.Value }:new SqlParameter("@Note", SqlDbType.NVarChar){ Value = note },
                new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = taskID }
			};         
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 更新一个任务后后续任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="comment"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        public int UpdateNextTaskStatus(Guid taskID, int status)
        {
            string sql = "UPDATE WorkFlowTask SET Status=@Status WHERE PrevID=@PrevID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Status", SqlDbType.Int){ Value = status },
                new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier){ Value = taskID }
			};
            return dbHelper.Execute(sql, parameters);
        }


        /// <summary>
        /// 得到一个流程实例一个步骤的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个流程实例一个步骤一个人员的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND ReceiveID=@ReceiveID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID },
                new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier){ Value = userID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }


        /// <summary>
        /// 得到一个实例的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        {
            string sql = string.Empty;
            SqlParameter[] parameters;
            if (flowID == null || flowID.IsEmptyGuid())
            {
                sql = "SELECT * FROM WorkFlowTask WHERE GroupID=@GroupID";
                parameters = new SqlParameter[]{
                    new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			    };
            }
            else
            {
                sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID";
                parameters = new SqlParameter[]{
				    new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                    new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			    };
            }
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到和一个任务同级的任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="isStepID">是否区分步骤ID，多步骤会签区分的是上一步骤ID</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskList(Guid taskID, bool isStepID = true)
        {
            var task = Get(taskID);
            if (task == null)
            {
                return new List<Model.WorkFlowTask>() { };
            }
            string sql = string.Format("SELECT * FROM WorkFlowTask WHERE PrevID=@PrevID AND {0}", isStepID ? "StepID=@StepID" : "PrevStepID=@StepID");
            SqlParameter[] parameters1 = new SqlParameter[]{
                new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier){ Value = task.PrevID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = isStepID ? task.StepID : task.PrevStepID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters1);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个任务的前一任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetPrevTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE ID=@ID)";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = taskID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个任务的后续任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetNextTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE PrevID=@PrevID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier){ Value = taskID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }


        /// <summary>
        /// 查询一个流程是否有任务数据
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasTasks(Guid flowID)
        {
            string sql = "SELECT TOP 1 ID FROM WorkFlowTask WHERE FlowID=@FlowID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            bool has = dataReader.HasRows;
            dataReader.Close();
            return has;
        }

        /// <summary>
        /// 查询一个用户在一个步骤是否有未完成任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT TOP 1 ID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND ReceiveID=@ReceiveID AND Status IN(-1,0,1)";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID },
                new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier){ Value = userID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            bool has = dataReader.HasRows;
            dataReader.Close();
            return has;
        }

        /// <summary>
        /// 激活临时任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <param name="completedTime">要求完成时间</param>
        /// <returns></returns>
        public int UpdateTempTasks(Guid flowID, Guid stepID, Guid groupID, DateTime? completedTime, DateTime receiveTime)
        {
            string sql = "UPDATE WorkFlowTask SET CompletedTime=@CompletedTime,ReceiveTime=@ReceiveTime,SenderTime=@SenderTime,Status=0 WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Status=-1";
            SqlParameter[] parameters = new SqlParameter[]{
                !completedTime.HasValue ? new SqlParameter("@CompletedTime", SqlDbType.DateTime) { Value = DBNull.Value } : 
                new SqlParameter("@CompletedTime", SqlDbType.DateTime) { Value = completedTime.Value },
                new SqlParameter("@ReceiveTime", SqlDbType.DateTime){ Value = receiveTime },
                new SqlParameter("@SenderTime", SqlDbType.DateTime){ Value = receiveTime },
                new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 删除临时任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <param name="prevStepID"></param>
        /// <returns></returns>
        public int DeleteTempTasks(Guid flowID, Guid stepID, Guid groupID, Guid prevStepID)
        {
            string sql = "DELETE WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Status=-1";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
                new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
                new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
			};
            if (!prevStepID.IsEmptyGuid())
            {
                sql += " AND PrevStepID=@PrevStepID";
                parameters.Add(new SqlParameter("@PrevStepID", SqlDbType.UniqueIdentifier) { Value = prevStepID });
            }
            return dbHelper.Execute(sql, parameters.ToArray());
        }

        /// <summary>
        /// 得到一个任务的状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int GetTaskStatus(Guid taskID)
        {
            string sql = "SELECT Status FROM WorkFlowTask WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = taskID }
			};
            string status = dbHelper.GetFieldValue(sql, parameters);
            int s;
            return status.IsInt(out s) ? s : -1;
        }

        /// <summary>
        /// 根据SubFlowID得到一个任务
        /// </summary>
        /// <param name="subflowGroupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE SubFlowGroupID=@SubFlowGroupID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@SubFlowGroupID", SqlDbType.UniqueIdentifier){ Value = subflowGroupID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
    }
}