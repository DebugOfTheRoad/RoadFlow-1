using System;
using System.Collections.Generic;
using System.Text;

namespace RoadFlow.Platform
{
    public class Users
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public const string PREFIX = "u_";
        private RoadFlow.Data.Interface.IUsers dataUsers;
        public Users()
        {
            this.dataUsers = Data.Factory.Factory.GetUsers();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(RoadFlow.Data.Model.Users model)
        {
            return dataUsers.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(RoadFlow.Data.Model.Users model)
        {
            return dataUsers.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.Users> GetAll()
        {
            return dataUsers.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public RoadFlow.Data.Model.Users Get(Guid id)
        {
            return dataUsers.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataUsers.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataUsers.GetCount();
        }

        /// <summary>
        /// 根据帐号查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.Users GetByAccount(string account)
        {
            return account.IsNullOrEmpty() ? null : dataUsers.GetByAccount(account);
        }

        /// <summary>
        /// 得到系统初始密码
        /// </summary>
        /// <returns></returns>
        public string GetInitPassword()
        {
            return RoadFlow.Utility.Config.SystemInitPassword;
        }
        /// <summary>
        /// 得到加密后的密码字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptionPassword(string password)
        {
            if (password.IsNullOrEmpty())
            {
                return "";
            }
            RoadFlow.Utility.HashEncrypt hash = new RoadFlow.Utility.HashEncrypt();
            return hash.MD5System(hash.MD5System(password)); //hash.SHA512Encrypt(hash.SHA512Encrypt(password.Trim()));
        }

        /// <summary>
        /// 得到一个用户加密后的密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetUserEncryptionPassword(string userID, string password)
        {
            return password.IsNullOrEmpty() || userID.IsNullOrEmpty() ? "" : EncryptionPassword(userID.Trim().ToLower() + password.Trim());
        }

        /// <summary>
        /// 初始化一个用户密码
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool InitPassword(Guid userID)
        {
            return dataUsers.UpdatePassword(GetUserEncryptionPassword(userID.ToString(), GetInitPassword()), userID);
        }

        /// <summary>
        /// 查询一个岗位下所有人员
        /// </summary>
        /// <param name="organizeID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.Users> GetAllByOrganizeID(Guid organizeID)
        {
            return dataUsers.GetAllByOrganizeID(organizeID);
        }

        /// <summary>
        /// 得到一个用户的所有岗位
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Dictionary<Guid, bool> Guid 岗位ID bool 是否主要岗位</returns>
        public Dictionary<Guid, bool> GetAllStation(Guid userID)
        {
            UsersRelation ur = new UsersRelation();
            var urs = ur.GetAllByUserID(userID);
            Dictionary<Guid, bool> dict = new Dictionary<Guid, bool>();
            foreach (var u in urs)
            {
                if (!dict.ContainsKey(u.OrganizeID))
                {
                    dict.Add(u.OrganizeID, u.IsMain == 1);
                }
            }
            return dict;
        }
        /// <summary>
        /// 得到一个用户的主要岗位
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Guid GetMainStation(Guid userID)
        {
            var ur = new UsersRelation().GetMainByUserID(userID);
            return ur == null ? Guid.Empty : ur.OrganizeID;
        }

        /// <summary>
        /// 查询一组岗位下所有人员
        /// </summary>
        /// <param name="organizeID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.Users> GetAllByOrganizeIDArray(Guid[] organizeIDArray)
        {
            return dataUsers.GetAllByOrganizeIDArray(organizeIDArray);
        }

        /// <summary>
        /// 得到一个用户所在部门
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public RoadFlow.Data.Model.Organize GetDeptByUserID(Guid userID)
        { 
            Guid stationID=GetMainStation(userID);
            if(stationID==Guid.Empty)
            {
                return null;
            }
            var parents = new RoadFlow.Platform.Organize().GetAllParent(stationID);
            parents.Reverse();
            foreach (var parent in parents)
            {
                if (parent.Type == 2 || parent.Type==1)
                {
                    return parent;
                }
            }
            return null;
        }

        /// <summary>
        /// 当前用户部门ID
        /// </summary>
        public static Guid CurrentDeptID
        {
            get
            {
                var dept = new Users().GetDeptByUserID(CurrentUserID);
                return dept == null ? Guid.Empty : dept.ID;
            }
        }

        /// <summary>
        /// 当前用户部门名称
        /// </summary>
        public static string CurrentDeptName
        {
            get
            {
                var dept = new Users().GetDeptByUserID(CurrentUserID);
                return dept == null ? "" : dept.Name;
            }
        }

        /// <summary>
        /// 检查帐号是否重复
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="userID">人员ID(此人员除外)</param>
        /// <returns></returns>
        public bool HasAccount(string account, string userID = "")
        {
            return account.IsNullOrEmpty() ? false : dataUsers.HasAccount(account.Trim(), userID);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password">明文的密码</param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UpdatePassword(string password, Guid userID)
        {
            return password.IsNullOrEmpty() ? false : dataUsers.UpdatePassword(GetUserEncryptionPassword(userID.ToString(), password.Trim()), userID);
        }
        /// <summary>
        /// 得到当前登录用户ID
        /// </summary>
        public static Guid CurrentUserID
        {
            get
            {
                object session = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()];
                return session == null ? Guid.Empty : session.ToString().ToGuid();
            }
        }
        /// <summary>
        /// 得到当前登录用户
        /// </summary>
        public static RoadFlow.Data.Model.Users CurrentUser
        {
            get
            {
                object obj = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.User.ToString()];
                if (obj == null)
                {
                    Guid userID = CurrentUserID;
                    if (userID == Guid.Empty)
                    {
                        return null;
                    }
                    else
                    {
                        var user = new Users().Get(userID);
                        if (user != null)
                        {
                            System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.User.ToString()] = user;
                        }
                        return user;
                    }
                }
                else
                {
                    return obj as RoadFlow.Data.Model.Users;
                }
            }
        }

        /// <summary>
        /// 当前用户名称
        /// </summary>
        [Obsolete("由于该方法拼写错误,请使用CurrentUserName属性")]
        public static string CurrentUsreName
        {
            get
            {
                return CurrentUser == null ? "" : CurrentUser.Name;
            }
        }
        /// <summary>
        /// 当前用户名称
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                return CurrentUser == null ? "" : CurrentUser.Name;
            }
        }

        /// <summary>
        /// 当前用户的所有角色
        /// </summary>
        public static List<Guid> CurrentUserRoles
        {
            get
            {
                Guid userID = CurrentUserID;
                if (userID.IsEmptyGuid())
                {
                    return new List<Guid>();
                }
                List<Guid> list = new List<Guid>();
                var userRoles = new UsersRole().GetByUserIDFromCache(userID);
                foreach (var userRole in userRoles)
                {
                    list.Add(userRole.RoleID);
                }
                return list;
            }
        }

        /// <summary>
        /// 得到一个不重复的帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetAccount(string account)
        {
            if (account.IsNullOrEmpty())
            {
                return "";
            }
            string newAccount = account.Trim();
            int i = 0;
            while (HasAccount(newAccount))
            {
                newAccount += (++i).ToString();
            }
            return newAccount;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public int UpdateSort(Guid userID, int sort)
        {
            return dataUsers.UpdateSort(userID, sort);
        }
        /// <summary>
        /// 根据ID得到名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(Guid id)
        {
            var user = Get(id);
            return user == null ? "" : user.Name;
        }
        /// <summary>
        /// 去除ID前缀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string RemovePrefix(string id)
        {
            return id.IsNullOrEmpty() ? "" : id.Replace(PREFIX, "");
        }

        /// <summary>
        /// 去除ID前缀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RemovePrefix1(string id)
        {
            return id.IsNullOrEmpty() ? "" : id.Replace(PREFIX, "");
        }

        /// <summary>
        /// 得到一个人员的主管
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetLeader(Guid userID)
        {
            var mainStation = GetMainStation(userID);
            if (mainStation == null)
            {
                return "";
            }
            RoadFlow.Platform.Organize borg = new Organize();
            var station = borg.Get(mainStation);
            if (station == null)
            {
                return "";
            }
            if (!station.Leader.IsNullOrEmpty())
            {
                return station.Leader;
            }
            var parents = borg.GetAllParent(station.Number);
            foreach (var parent in parents)
            {
                if (!parent.Leader.IsNullOrEmpty())
                {
                    return parent.Leader;
                }
            }
            return "";
        }

        /// <summary>
        /// 得到一个人员的分管领导
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetChargeLeader(Guid userID)
        {
            var mainStation = GetMainStation(userID);
            if (mainStation == null)
            {
                return "";
            }
            RoadFlow.Platform.Organize borg = new Organize();
            var station = borg.Get(mainStation);
            if (station == null)
            {
                return "";
            }
            if (!station.ChargeLeader.IsNullOrEmpty())
            {
                return station.ChargeLeader;
            }
            var parents = borg.GetAllParent(station.Number);
            foreach (var parent in parents)
            {
                if (!parent.ChargeLeader.IsNullOrEmpty())
                {
                    return parent.ChargeLeader;
                }
            }
            return "";
        }

        /// <summary>
        /// 判断一个人员是否是部门主管
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsLeader(Guid userID)
        {
            string leader = GetLeader(userID);
            return leader.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// 判断一个人员是否是部门分管领导
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsChargeLeader(Guid userID)
        {
            string leader = GetChargeLeader(userID);
            return leader.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// 判断一个人员是否在一个组织机构字符串里
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="memberString"></param>
        /// <returns></returns>
        public bool IsContains(Guid userID, string memberString)
        {
            if (memberString.IsNullOrEmpty())
            {
                return false;
            }
            var user = new Organize().GetAllUsers(memberString).Find(p => p.ID == userID);
            return user != null;
        }
    }

    public class UsersEqualityComparer : IEqualityComparer<RoadFlow.Data.Model.Users>
    {
        public bool Equals(RoadFlow.Data.Model.Users user1, RoadFlow.Data.Model.Users user2)
        {
            return user1 == null || user2 == null || user1.ID == user2.ID;
        }
        public int GetHashCode(RoadFlow.Data.Model.Users user)
        {
            return user.ToString().GetHashCode();
        }
    }
}
