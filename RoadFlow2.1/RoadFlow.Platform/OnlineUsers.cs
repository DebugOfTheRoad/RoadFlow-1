using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadFlow.Platform
{
    public class OnlineUsers
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        private string key = RoadFlow.Utility.Keys.CacheKeys.OnlineUsers.ToString();

        /// <summary>
        /// 得到所有在线用户表
        /// </summary>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.OnlineUsers> GetAll()
        {
            object obj = RoadFlow.Cache.IO.Opation.Get(key);
            return obj != null && obj is List<RoadFlow.Data.Model.OnlineUsers> ? obj as List<RoadFlow.Data.Model.OnlineUsers> : new List<RoadFlow.Data.Model.OnlineUsers>();
        }

        private void set(List<RoadFlow.Data.Model.OnlineUsers> list)
        {
            if (list == null) return;
            RoadFlow.Cache.IO.Opation.Set(key, list);
        }

        /// <summary>
        /// 添加一个用户到在线用户表
        /// </summary>
        public bool Add(RoadFlow.Data.Model.Users user, Guid uniqueID)
        {
            if (user == null) return false;
            var onList = GetAll();
            bool isadd = false;
            var onUser = onList.Find(p => p.ID == user.ID);
            if (onUser == null)
            {
                isadd = true;
                onUser = new RoadFlow.Data.Model.OnlineUsers();
                var station = new UsersRelation().GetMainByUserID(user.ID);
                if (station != null)
                {
                    onUser.OrgName = new Organize().GetAllParentNames(station.OrganizeID);
                }
            }
            onUser.ID = user.ID;
            onUser.ClientInfo = string.Concat("操作系统：", RoadFlow.Utility.Tools.GetOSName(), "  浏览器：", RoadFlow.Utility.Tools.GetBrowse());
            onUser.IP = RoadFlow.Utility.Tools.GetIPAddress();
            onUser.LastPage = "";
            onUser.LoginTime = RoadFlow.Utility.DateTimeNew.Now;
            onUser.UniqueID = uniqueID;
            onUser.UserName = user.Name;
            if (isadd)
            {
                onList.Add(onUser);
            }
            set(onList);
            return true;
        }

        /// <summary>
        /// 删除一个在线用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool Remove(Guid userID)
        {
            var list = GetAll();
            var user = list.Find(p => p.ID == userID);
            if (user != null)
            {
                list.Remove(user);
            }
            set(list);
            return true;
        }

        /// <summary>
        /// 清除所有在线用户
        /// </summary>
        /// <returns></returns>
        public bool RemoveAll()
        {
            var list = new List<RoadFlow.Data.Model.OnlineUsers>();
            RoadFlow.Cache.IO.Opation.Set(key, list);
            return true;
        }

        /// <summary>
        /// 查询一个在线用户实体
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public RoadFlow.Data.Model.OnlineUsers Get(Guid userID)
        {
            var list = GetAll();
            return list.Find(p => p.ID == userID);
        }

    }
}
