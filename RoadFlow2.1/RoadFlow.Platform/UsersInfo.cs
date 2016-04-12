using System;
using System.Collections.Generic;
using System.Text;

namespace RoadFlow.Platform
{
    public class UsersInfo
    {
        private RoadFlow.Data.Interface.IUsersInfo dataUsersInfo;
        public UsersInfo()
        {
            this.dataUsersInfo = Data.Factory.Factory.GetUsersInfo();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(RoadFlow.Data.Model.UsersInfo model)
        {
            return dataUsersInfo.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(RoadFlow.Data.Model.UsersInfo model)
        {
            return dataUsersInfo.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.UsersInfo> GetAll()
        {
            return dataUsersInfo.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public RoadFlow.Data.Model.UsersInfo Get(Guid userid)
        {
            return dataUsersInfo.Get(userid);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid userid)
        {
            return dataUsersInfo.Delete(userid);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataUsersInfo.GetCount();
        }
    }
}
