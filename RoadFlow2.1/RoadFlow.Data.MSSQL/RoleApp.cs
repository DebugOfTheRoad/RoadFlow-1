﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace RoadFlow.Data.MSSQL
{
    public class RoleApp : RoadFlow.Data.Interface.IRoleApp
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleApp()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.RoleApp实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.RoleApp model)
        {
            string sql = @"INSERT INTO RoleApp
				(ID,ParentID,RoleID,AppID,Title,Params,Sort,Ico,Type) 
				VALUES(@ID,@ParentID,@RoleID,@AppID,@Title,@Params,@Sort,@Ico,@Type)";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1){ Value = model.ID },
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1){ Value = model.ParentID },
				new SqlParameter("@RoleID", SqlDbType.UniqueIdentifier, -1){ Value = model.RoleID },
				model.AppID == null ? new SqlParameter("@AppID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@AppID", SqlDbType.UniqueIdentifier, -1) { Value = model.AppID },
				new SqlParameter("@Title", SqlDbType.NVarChar, 400){ Value = model.Title },
				model.Params == null ? new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = model.Params },
				new SqlParameter("@Sort", SqlDbType.Int, -1){ Value = model.Sort },
				model.Ico == null ? new SqlParameter("@Ico", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 200) { Value = model.Ico },
				new SqlParameter("@Type", SqlDbType.Int, -1){ Value = model.Type }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.RoleApp实体类</param>
        public int Update(RoadFlow.Data.Model.RoleApp model)
        {
            string sql = @"UPDATE RoleApp SET 
				ParentID=@ParentID,RoleID=@RoleID,AppID=@AppID,Title=@Title,Params=@Params,Sort=@Sort,Ico=@Ico,Type=@Type
				WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1){ Value = model.ParentID },
				new SqlParameter("@RoleID", SqlDbType.UniqueIdentifier, -1){ Value = model.RoleID },
				model.AppID == null ? new SqlParameter("@AppID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@AppID", SqlDbType.UniqueIdentifier, -1) { Value = model.AppID },
				new SqlParameter("@Title", SqlDbType.NVarChar, 400){ Value = model.Title },
				model.Params == null ? new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = model.Params },
				new SqlParameter("@Sort", SqlDbType.Int, -1){ Value = model.Sort },
				model.Ico == null ? new SqlParameter("@Ico", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 200) { Value = model.Ico },
				new SqlParameter("@Type", SqlDbType.Int, -1){ Value = model.Type },
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM RoleApp WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.RoleApp> DataReaderToList(SqlDataReader dataReader)
        {
            List<RoadFlow.Data.Model.RoleApp> List = new List<RoadFlow.Data.Model.RoleApp>();
            RoadFlow.Data.Model.RoleApp model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.RoleApp();
                model.ID = dataReader.GetGuid(0);
                model.ParentID = dataReader.GetGuid(1);
                model.RoleID = dataReader.GetGuid(2);
                if (!dataReader.IsDBNull(3))
                    model.AppID = dataReader.GetGuid(3);
                model.Title = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                    model.Params = dataReader.GetString(5);
                model.Sort = dataReader.GetInt32(6);
                if (!dataReader.IsDBNull(7))
                    model.Ico = dataReader.GetString(7);
                model.Type = dataReader.GetInt32(8);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.RoleApp> GetAll()
        {
            string sql = "SELECT * FROM RoleApp";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.RoleApp> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM RoleApp";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.RoleApp Get(Guid id)
        {
            string sql = "SELECT * FROM RoleApp WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.RoleApp> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询一个角色所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.RoleApp> GetAllByRoleID(Guid roleID)
        {
            string sql = "SELECT * FROM RoleApp WHERE RoleID=@RoleID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@RoleID", SqlDbType.UniqueIdentifier){ Value = roleID }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.RoleApp> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 查询一个角色所有记录
        /// </summary>
        public System.Data.DataTable GetAllDataTableByRoleID(Guid roleID)
        {
            string sql = "SELECT a.*,b.Address,b.OpenMode,b.Width,b.Height,b.Params AS Param1,b.Manager,b.UseMember FROM RoleApp a LEFT JOIN AppLibrary b ON a.AppID=b.ID WHERE a.RoleID=@RoleID ORDER BY a.Sort";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@RoleID", SqlDbType.UniqueIdentifier){ Value = roleID }
			};
            return dbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public System.Data.DataTable GetAllDataTable()
        {
            string sql = "SELECT a.*,b.Address,b.OpenMode,b.Width,b.Height,b.Params AS Params1,b.Manager,b.UseMember FROM RoleApp a LEFT JOIN AppLibrary b ON a.AppID=b.ID ORDER BY a.Sort";
            return dbHelper.GetDataTable(sql);
        }

        
        /// <summary>
        /// 查询所有下级记录
        /// </summary>
        public System.Data.DataTable GetChildsDataTable(Guid id)
        {
            string sql = "SELECT a.*,b.Address,b.OpenMode,b.Width,b.Height,b.Params AS Params1,b.Manager,b.UseMember FROM RoleApp a LEFT JOIN AppLibrary b ON a.AppID=b.ID WHERE a.ParentID=@ParentID ORDER BY a.Sort";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            return dbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<RoadFlow.Data.Model.RoleApp> GetChild(Guid id)
        {
            string sql = "SELECT * FROM RoleApp WHERE ParentID=@ParentID ORDER BY Sort";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.RoleApp> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 是否有下级记录
        /// </summary>
        public bool HasChild(Guid id)
        {
            string sql = "SELECT TOP 1 ID FROM RoleApp WHERE ParentID=@ParentID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            bool has = dataReader.HasRows;
            dataReader.Close();
            return has;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE RoleApp SET Sort=@Sort WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Sort", SqlDbType.Int){ Value = sort },
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 删除一个角色记录
        /// </summary>
        public int DeleteByRoleID(Guid roleID)
        {
            string sql = "DELETE FROM RoleApp WHERE RoleID=@RoleID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@RoleID", SqlDbType.UniqueIdentifier){ Value = roleID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除一个应用记录
        /// </summary>
        public int DeleteByAppID(Guid appID)
        {
            string sql = "DELETE FROM RoleApp WHERE AppID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = appID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 得到最大排序值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetMaxSort(Guid id)
        {
            string sql = "SELECT MAX(Sort)+1 FROM RoleApp WHERE ParentID=@ParentID";
            SqlParameter[] parameters = new SqlParameter[]{
				new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier){ Value = id }
			};
            string max = dbHelper.GetFieldValue(sql, parameters);
            int max1;
            return max.IsInt(out max1) ? max1 : 1;
        }
    }
}