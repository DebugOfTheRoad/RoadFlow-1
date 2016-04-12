using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RoadFlow.Platform
{
    public class AppLibrary
    {
        private string cacheKey = RoadFlow.Utility.Keys.CacheKeys.AppLibrary.ToString();
        private RoadFlow.Data.Interface.IAppLibrary dataAppLibrary;
        public AppLibrary()
        {
            this.dataAppLibrary = Data.Factory.Factory.GetAppLibrary();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(RoadFlow.Data.Model.AppLibrary model)
        {
            return dataAppLibrary.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(RoadFlow.Data.Model.AppLibrary model)
        {
            return dataAppLibrary.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.AppLibrary> GetAll(bool fromCache=false)
        {
            if (!fromCache)
            {
                return dataAppLibrary.GetAll();
            }
            else
            {
                object obj = RoadFlow.Cache.IO.Opation.Get(cacheKey);
                if (obj != null)
                {
                    return obj as List<RoadFlow.Data.Model.AppLibrary>;
                }
                else
                {
                    var list = dataAppLibrary.GetAll();
                    RoadFlow.Cache.IO.Opation.Set(cacheKey, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public RoadFlow.Data.Model.AppLibrary Get(Guid id, bool fromCache=false)
        {
            if (!fromCache)
            {
                return dataAppLibrary.Get(id);
            }
            else
            {
                var all = GetAll(true);
                var app = all.Find(p => p.ID == id);
                return app == null ? dataAppLibrary.Get(id) : app;
            }
        }
        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            RoadFlow.Cache.IO.Opation.Remove(cacheKey);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataAppLibrary.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAppLibrary.GetCount();
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="numbe"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.AppLibrary> GetPagerData(out string pager, string query = "", string title = "", string type = "", string address = "")
        {
            return dataAppLibrary.GetPagerData(out pager, query, "Type,Title", RoadFlow.Utility.Tools.GetPageSize(),
                RoadFlow.Utility.Tools.GetPageNumber(), title, type, address);
        }
        /// <summary>
        /// 查询一个类别下所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.AppLibrary> GetAllByType(Guid type)
        {
            if (type.IsEmptyGuid())
            {
                return new List<RoadFlow.Data.Model.AppLibrary>();
            }
            return dataAppLibrary.GetAllByType(GetAllChildsIDString(type)).OrderBy(p=>p.Title).ToList();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string[] idArray)
        {
            return dataAppLibrary.Delete(idArray);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string idstring)
        {
            return idstring.IsNullOrEmpty() ? 0 : dataAppLibrary.Delete(idstring.Split(','));
        }
        /// <summary>
        /// 得到类型选择项
        /// </summary>
        /// <returns></returns>
        public string GetTypeOptions(string value="")
        {
            return new Dictionary().GetOptionsByCode("AppLibraryTypes", Dictionary.OptionValueField.ID, value);
        }

        /// <summary>
        /// 得到下级ID字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAllChildsIDString(Guid id, bool isSelf = true)
        {
            return new Dictionary().GetAllChildsIDString(id, true);
        }

        /// <summary>
        /// 得到一个类型选择项
        /// </summary>
        /// <param name="type">程序类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetAppsOptions(Guid type, string value = "")
        {
            if (type.IsEmptyGuid()) return "";
            var apps = GetAllByType(type);
            StringBuilder options = new StringBuilder();
            foreach (var app in apps)
            {
                options.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", app.ID, 
                    string.Compare(app.ID.ToString(), value, true) == 0 ? "selected=\"selected\"" : "",
                    app.Title
                    );
            }
            return options.ToString();
        }
        /// <summary>
        /// 根据ID得到类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetTypeByID(Guid id)
        {
            var app = Get(id);
            return app == null ? "" : app.Type.ToString();
        }

        /// <summary>
        /// 根据代码查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.AppLibrary GetByCode(string code)
        {
            return code.IsNullOrEmpty() ? null : dataAppLibrary.GetByCode(code.Trim());
        }

        /// <summary>
        /// 得到流程运行时地址
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public string GetFlowRunAddress(RoadFlow.Data.Model.AppLibrary app, string query="")
        {
            StringBuilder sb = new StringBuilder();
            if (app.Params.IsNullOrEmpty())
            {
                if (!app.Address.Contains("?"))
                {
                    sb.Append(app.Address);
                    sb.Append("?1=1");
                }
            }
            else
            {
            
                if (app.Address.Contains("?"))
                {
                    sb.Append(app.Address);
                    sb.Append("&");
                    sb.Append(app.Params.TrimStart('?').TrimStart('&'));
                }
                else
                {
                    sb.Append(app.Address);
                    sb.Append("?");
                    sb.Append(app.Params.TrimStart('?').TrimStart('&'));
                }
            }
            if (!query.IsNullOrEmpty())
            {
                sb.Append("&");
                sb.Append(query.TrimStart('?').TrimStart('&'));
            }

            return sb.ToString();
            
        }

        /// <summary>
        /// 更新应用程序库使用人员缓存
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userIdString"></param>
        public List<Guid> UpdateUseMemberCache(Guid appid)
        {
            string key = RoadFlow.Utility.Keys.CacheKeys.AppLibraryUseMember.ToString();
            var obj = RoadFlow.Cache.IO.Opation.Get(key);
            Dictionary<Guid, List<Guid>> dict;
            if (obj != null && obj is Dictionary<Guid, List<Guid>>)
            {
                dict = obj as Dictionary<Guid, List<Guid>>;
            }
            else
            {
                dict = new Dictionary<Guid, List<Guid>>();
            }
            var app = new AppLibrary().Get(appid);
            if (app == null)
            {
                return new List<Guid>();
            }
            if (dict.ContainsKey(appid))
            {
                if (app.UseMember.IsNullOrEmpty())
                {
                    dict.Remove(appid);
                    return new List<Guid>();
                }
                else
                {
                    var userIDs = new Organize().GetAllUsersIdList(app.UseMember);
                    dict[appid] = userIDs;
                    return userIDs;
                }
            }
            else if(!app.UseMember.IsNullOrEmpty())
            {
                var userIDs = new Organize().GetAllUsersIdList(app.UseMember);
                dict.Add(appid, userIDs);
                return userIDs;
            }
            return new List<Guid>();
        }
        /// <summary>
        /// 得到一个应用程序库的使用人员
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public List<Guid> GetUseMemberCache(Guid appid)
        {
            string key = RoadFlow.Utility.Keys.CacheKeys.AppLibraryUseMember.ToString();
            var obj = RoadFlow.Cache.IO.Opation.Get(key);
            if (obj != null && obj is Dictionary<Guid, List<Guid>>)
            {
                var dict = obj as Dictionary<Guid, List<Guid>>;
                if (dict.ContainsKey(appid))
                {
                    return dict[appid];
                }
            }
            var app = new AppLibrary().Get(appid);
            if (app == null || app.UseMember.IsNullOrEmpty())
            {
                return new List<Guid>();
            }
            return UpdateUseMemberCache(appid);
        }
        /// <summary>
        /// 清除应用程序库的使用人员缓存
        /// </summary>
        public void ClearUseMemberCache()
        {
            string key = RoadFlow.Utility.Keys.CacheKeys.AppLibraryUseMember.ToString();
            Cache.IO.Opation.Remove(key);
        }
    }
}
