﻿using System;
using System.Collections.Generic;

namespace RoadFlow.Data.Interface
{
    public interface IWorkFlow
    {
        /// <summary>
        /// 新增
        /// </summary>
        int Add(RoadFlow.Data.Model.WorkFlow model);

        /// <summary>
        /// 更新
        /// </summary>
        int Update(RoadFlow.Data.Model.WorkFlow model);

        /// <summary>
        /// 查询所有记录
        /// </summary>
        List<RoadFlow.Data.Model.WorkFlow> GetAll();

        /// <summary>
        /// 查询单条记录
        /// </summary>
        Model.WorkFlow Get(Guid id);

        /// <summary>
        /// 删除
        /// </summary>
        int Delete(Guid id);

        /// <summary>
        /// 查询记录条数
        /// </summary>
        long GetCount();

        /// <summary>
        /// 查询所有类型
        /// </summary>
        List<string> GetAllTypes();

        /// <summary>
        /// 查询所有ID和名称
        /// </summary>
        Dictionary<Guid, string> GetAllIDAndName();

        /// <summary>
        /// 查询所有记录
        /// </summary>
        List<RoadFlow.Data.Model.WorkFlow> GetByTypes(string typeString);
    }
}
