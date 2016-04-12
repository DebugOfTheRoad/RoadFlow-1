﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace RoadFlow.Data.MSSQL
{
    public class Organize : RoadFlow.Data.Interface.IOrganize
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Organize()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.Organize实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.Organize model)
        {
            string sql = @"INSERT INTO Organize
				(ID,Name,Number,Type,Status,ParentID,Sort,Depth,ChildsLength,ChargeLeader,Leader,Note) 
				VALUES(@ID,@Name,@Number,@Type,@Status,@ParentID,@Sort,@Depth,@ChildsLength,@ChargeLeader,@Leader,@Note)";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1){ Value = model.ID },
				new SqlParameter("@Name", SqlDbType.VarChar, 2000){ Value = model.Name },
				new SqlParameter("@Number", SqlDbType.VarChar, 900){ Value = model.Number },
				new SqlParameter("@Type", SqlDbType.Int, -1){ Value = model.Type },
				new SqlParameter("@Status", SqlDbType.Int, -1){ Value = model.Status },
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1){ Value = model.ParentID },
				new SqlParameter("@Sort", SqlDbType.Int, -1){ Value = model.Sort },
				new SqlParameter("@Depth", SqlDbType.Int, -1){ Value = model.Depth },
				new SqlParameter("@ChildsLength", SqlDbType.Int, -1){ Value = model.ChildsLength },
				model.ChargeLeader == null ? new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = model.ChargeLeader },
				model.Leader == null ? new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = model.Leader },
				model.Note == null ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.Organize实体类</param>
        public int Update(RoadFlow.Data.Model.Organize model)
        {
            string sql = @"UPDATE Organize SET 
				Name=@Name,Number=@Number,Type=@Type,Status=@Status,ParentID=@ParentID,Sort=@Sort,Depth=@Depth,ChildsLength=@ChildsLength,ChargeLeader=@ChargeLeader,Leader=@Leader,Note=@Note
				WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@Name", SqlDbType.VarChar, 2000){ Value = model.Name },
				new SqlParameter("@Number", SqlDbType.VarChar, 900){ Value = model.Number },
				new SqlParameter("@Type", SqlDbType.Int, -1){ Value = model.Type },
				new SqlParameter("@Status", SqlDbType.Int, -1){ Value = model.Status },
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1){ Value = model.ParentID },
				new SqlParameter("@Sort", SqlDbType.Int, -1){ Value = model.Sort },
				new SqlParameter("@Depth", SqlDbType.Int, -1){ Value = model.Depth },
				new SqlParameter("@ChildsLength", SqlDbType.Int, -1){ Value = model.ChildsLength },
				model.ChargeLeader == null ? new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = model.ChargeLeader },
				model.Leader == null ? new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = model.Leader },
				model.Note == null ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note },
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Organize WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.Organize> DataReaderToList(SqlDataReader dataReader)
        {
            List<RoadFlow.Data.Model.Organize> List = new List<RoadFlow.Data.Model.Organize>();
            RoadFlow.Data.Model.Organize model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.Organize();
                model.ID = dataReader.GetGuid(0);
                model.Name = dataReader.GetString(1);
                model.Number = dataReader.GetString(2);
                model.Type = dataReader.GetInt32(3);
                model.Status = dataReader.GetInt32(4);
                model.ParentID = dataReader.GetGuid(5);
                model.Sort = dataReader.GetInt32(6);
                model.Depth = dataReader.GetInt32(7);
                model.ChildsLength = dataReader.GetInt32(8);
                if (!dataReader.IsDBNull(9))
                    model.ChargeLeader = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                    model.Leader = dataReader.GetString(10);
                if (!dataReader.IsDBNull(11))
                    model.Note = dataReader.GetString(11);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.Organize> GetAll()
        {
            string sql = "SELECT * FROM Organize";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM Organize";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.Organize Get(Guid id)
        {
            string sql = "SELECT * FROM Organize WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 查询根记录
        /// </summary>
        public RoadFlow.Data.Model.Organize GetRoot()
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=@ParentID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier){ Value = Guid.Empty }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<RoadFlow.Data.Model.Organize> GetChilds(Guid ID)
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=@ParentID ORDER BY Sort";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier){ Value = ID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到最大排序值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort(Guid id)
        {
            string sql = "SELECT ISNULL(MAX(Sort),0)+1 FROM Organize WHERE ParentID=@ParentID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            string sort = dbHelper.GetFieldValue(sql, parameters);
            return sort.ToInt();
        }

        /// <summary>
        /// 更新下级数
        /// </summary>
        /// <returns></returns>
        public int UpdateChildsLength(Guid id, int length)
        {
            string sql = "UPDATE Organize SET ChildsLength=@ChildsLength WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ChildsLength", SqlDbType.Int){ Value = length },
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <returns></returns>
        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Organize SET Sort=@Sort WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Sort", SqlDbType.Int){ Value = sort },
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 查询一个组织的所有上级
        /// </summary>
        public List<RoadFlow.Data.Model.Organize> GetAllParent(string number)
        {
            string sql = "SELECT * FROM Organize WHERE ID IN(" + RoadFlow.Utility.Tools.GetSqlInString(number) + ") ORDER BY Depth";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 查询一个组织的所有下级
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.Organize> GetAllChild(string number)
        {
            string sql = "SELECT * FROM Organize WHERE Number LIKE '" + number.ReplaceSql() + "%' ORDER BY Sort";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

    }
}