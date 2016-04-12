using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadFlow.Platform
{
    public class WorkFlowTask : IEqualityComparer<RoadFlow.Data.Model.WorkFlowTask>
    {
        private WorkFlow bWorkFlow = new WorkFlow();
        private RoadFlow.Data.Interface.IWorkFlowTask dataWorkFlowTask;
        private RoadFlow.Data.Model.WorkFlowInstalled wfInstalled;
        private RoadFlow.Data.Model.WorkFlowExecute.Result result;
        private List<RoadFlow.Data.Model.WorkFlowTask> nextTasks = new List<RoadFlow.Data.Model.WorkFlowTask>();
		public WorkFlowTask()
		{
            this.dataWorkFlowTask = Data.Factory.Factory.GetWorkFlowTask();
		}
        
		/// <summary>
		/// 新增
		/// </summary>
		public int Add(RoadFlow.Data.Model.WorkFlowTask model)
		{
			return dataWorkFlowTask.Add(model);
		}
		/// <summary>
		/// 更新
		/// </summary>
		public int Update(RoadFlow.Data.Model.WorkFlowTask model)
		{
			return dataWorkFlowTask.Update(model);
		}
		/// <summary>
		/// 查询所有记录
		/// </summary>
		public List<RoadFlow.Data.Model.WorkFlowTask> GetAll()
		{
			return dataWorkFlowTask.GetAll();
		}
		/// <summary>
		/// 查询单条记录
		/// </summary>
		public RoadFlow.Data.Model.WorkFlowTask Get(Guid id)
		{
			return dataWorkFlowTask.Get(id);
		}
		/// <summary>
		/// 删除
		/// </summary>
		public int Delete(Guid id)
		{
			return dataWorkFlowTask.Delete(id);
		}
		/// <summary>
		/// 查询记录条数
		/// </summary>
		public long GetCount()
		{
			return dataWorkFlowTask.GetCount();
		}

        /// <summary>
        /// 去除重复的接收人，在退回任务时去重，避免一个人收到多条任务。
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns></returns>
        public bool Equals(RoadFlow.Data.Model.WorkFlowTask task1, RoadFlow.Data.Model.WorkFlowTask task2)
        {
            return task1.ReceiveID == task2.ReceiveID;
        }

        public int GetHashCode(RoadFlow.Data.Model.WorkFlowTask task)
        {
            return task.ToString().GetHashCode();
        }

        /// <summary>
        /// 更新打开时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="openTime"></param>
        /// <param name="isStatus">是否将状态更新为1</param>
        public void UpdateOpenTime(Guid id, DateTime openTime, bool isStatus = false)
        {
            dataWorkFlowTask.UpdateOpenTime(id, openTime, isStatus);
        }

        /// <summary>
        /// 得到一个流程实例的发起者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <param name="isDefault">如果为空是否返回当前登录用户ID</param>
        /// <returns></returns>
        public Guid GetFirstSnderID(Guid flowID, Guid groupID, bool isDefault = false)
        {
            Guid senderID=dataWorkFlowTask.GetFirstSnderID(flowID, groupID);
            return senderID.IsEmptyGuid() && isDefault ? Users.CurrentUserID : senderID;
        }

        /// <summary>
        /// 得到一个流程实例的发起者部门
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public Guid GetFirstSnderDeptID(Guid flowID, Guid groupID)
        {
            if (flowID.IsEmptyGuid() || groupID.IsEmptyGuid())
            {
                return Users.CurrentDeptID; 
            }
            var senderID = dataWorkFlowTask.GetFirstSnderID(flowID, groupID);
            var dept = new Users().GetDeptByUserID(senderID);
            return dept == null ? Guid.Empty : dept.ID;
        }


        /// <summary>
        /// 得到一个流程实例一个步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            return dataWorkFlowTask.GetStepSnderID(flowID, stepID, groupID);
        }

        /// <summary>
        /// 得到一个流程实例一个步骤的处理者字符串
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public string GetStepSnderIDString(Guid flowID, Guid stepID, Guid groupID)
        {
            var list = GetStepSnderID(flowID, stepID, groupID);
            StringBuilder sb = new StringBuilder(list.Count * 43);
            foreach (var li in list)
            {
                sb.Append(RoadFlow.Platform.Users.PREFIX);
                sb.Append(li);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 得到一个流程实例前一步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            return dataWorkFlowTask.GetPrevSnderID(flowID, stepID, groupID);
        }

        /// <summary>
        /// 得到一个流程实例前一步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public String GetPrevSnderIDString(Guid flowID, Guid stepID, Guid groupID)
        {
            var list = dataWorkFlowTask.GetPrevSnderID(flowID, stepID, groupID);
            StringBuilder sb = new StringBuilder(list.Count * 43);
            foreach (var li in list)
            {
                sb.Append(RoadFlow.Platform.Users.PREFIX);
                sb.Append(li);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 将json字符串转换为执行实体
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        private RoadFlow.Data.Model.WorkFlowExecute.Execute GetExecuteModel(string jsonString)
        {
            RoadFlow.Data.Model.WorkFlowExecute.Execute execute = new RoadFlow.Data.Model.WorkFlowExecute.Execute();
            RoadFlow.Platform.Organize borganize = new Organize();

            LitJson.JsonData jsondata = LitJson.JsonMapper.ToObject(jsonString);
            if (jsondata == null) return execute;

            execute.Comment = jsondata["comment"].ToString();
            string op = jsondata["type"].ToString().ToLower();
            switch (op)
            { 
                case "submit":
                    execute.ExecuteType = RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit;
                    break;
                case "save":
                    execute.ExecuteType = RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save;
                    break;
                case "back":
                    execute.ExecuteType = RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back;
                    break;
            }
            execute.FlowID = jsondata["flowid"].ToString().ToGuid();
            execute.GroupID = jsondata["groupid"].ToString().ToGuid();
            execute.InstanceID = jsondata["instanceid"].ToString();
            execute.IsSign = jsondata["issign"].ToString().ToInt() == 1;
            execute.StepID = jsondata["stepid"].ToString().ToGuid();
            execute.TaskID = jsondata["taskid"].ToString().ToGuid();
           
            var stepsjson = jsondata["steps"];
            Dictionary<Guid, List<RoadFlow.Data.Model.Users>> steps = new Dictionary<Guid, List<RoadFlow.Data.Model.Users>>();
            if (stepsjson.IsArray)
            {
                foreach (LitJson.JsonData step in stepsjson)
                {
                    var id = step["id"].ToString().ToGuid();
                    var member = step["member"].ToString();
                    if (id == Guid.Empty || member.IsNullOrEmpty())
                    {
                        continue;
                    }
                    steps.Add(id, borganize.GetAllUsers(member));
                }
            }
            execute.Steps = steps;
            return execute;
        }

        /// <summary>
        /// 处理流程
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public RoadFlow.Data.Model.WorkFlowExecute.Result Execute(string jsonString)
        {
            return Execute(GetExecuteModel(jsonString));
        }

        /// <summary>
        /// 发起一个流程
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="users"></param>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        public bool StartFlow(Guid flowID, List<Data.Model.Users> users, string title, string instanceID="")
        {
            if (users.Count == 0)
            {
                return false;
            }
            try
            {
                foreach (var user in users)
                {
                    RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel = new Data.Model.WorkFlowExecute.Execute();
                    executeModel.ExecuteType = Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save;
                    executeModel.FlowID = flowID;
                    executeModel.InstanceID = instanceID;
                    executeModel.Title = title;
                    executeModel.Sender = user;
                    createFirstTask(executeModel);
                }
                return true;
            }
            catch (Exception err)
            {
                Platform.Log.Add(err);
                return false;
            }
        }

        /// <summary>
        /// 处理流程
        /// </summary>
        /// <param name="executeModel">处理实体</param>
        /// <returns></returns>
        public RoadFlow.Data.Model.WorkFlowExecute.Result Execute(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            result = new RoadFlow.Data.Model.WorkFlowExecute.Result();
            nextTasks = new List<RoadFlow.Data.Model.WorkFlowTask>();
            if (executeModel.FlowID == Guid.Empty)
            {
                result.DebugMessages = "流程ID错误";
                result.IsSuccess = false;
                result.Messages = "执行参数错误";
                return result;
            }
            

            wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
            if (wfInstalled == null)
            {
                result.DebugMessages = "未找到流程运行时实体";
                result.IsSuccess = false;
                result.Messages = "流程运行时为空";
                return result;
            }

            lock (executeModel.GroupID.ToString())
            {
                switch (executeModel.ExecuteType)
                {
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back:
                        executeBack(executeModel);
                        break;
                    //case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed:
                    //    executeComplete(executeModel);
                    //    break;
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save:
                        executeSave(executeModel);
                        break;
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit:
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed:
                        executeSubmit(executeModel);
                        break;
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Redirect:
                        executeRedirect(executeModel);
                        break;
                    default:
                        result.DebugMessages = "流程处理类型为空";
                        result.IsSuccess = false;
                        result.Messages = "流程处理类型为空";
                        return result;
                }

                result.NextTasks = nextTasks;
                return result;
            }
        }

        private void executeSubmit(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                //如果是第一步提交并且没有实例则先创建实例
                RoadFlow.Data.Model.WorkFlowTask currentTask = null;
                bool isFirst = executeModel.StepID == wfInstalled.FirstStepID && executeModel.TaskID == Guid.Empty && executeModel.GroupID == Guid.Empty;
                if (isFirst)
                {
                    currentTask = createFirstTask(executeModel);
                }
                else
                {
                    currentTask = Get(executeModel.TaskID);
                }
                if (currentTask == null)
                {
                    result.DebugMessages = "未能创建或找到当前任务";
                    result.IsSuccess = false;
                    result.Messages = "未能创建或找到当前任务";
                    return;
                }
                else if (currentTask.Status.In(2,3,4,5))
                {
                    result.DebugMessages = "当前任务已处理";
                    result.IsSuccess = false;
                    result.Messages = "当前任务已处理";
                    return;
                }

                var currentSteps = wfInstalled.Steps.Where(p => p.ID == executeModel.StepID);
                var currentStep = currentSteps.Count() > 0 ? currentSteps.First() : null;
                if (currentStep == null)
                {
                    result.DebugMessages = "未找到当前步骤";
                    result.IsSuccess = false;
                    result.Messages = "未找到当前步骤";
                    return;
                }

                //如果当前步骤是子流程步骤，并且策略是 子流程完成后才能提交 则要判断子流程是否已完成
                if (currentStep.Type == "subflow" 
                    && currentStep.SubFlowID.IsGuid()
                    && currentStep.Behavior.SubFlowStrategy == 0 
                    && currentTask.SubFlowGroupID.HasValue 
                    && !currentTask.SubFlowGroupID.Value.IsEmptyGuid() 
                    && !GetInstanceIsCompleted(currentStep.SubFlowID.ToGuid(), currentTask.SubFlowGroupID.Value))
                {
                    result.DebugMessages = "当前步骤的子流程实例未完成,子流程：" + currentStep.SubFlowID + ",实例组：" + currentTask.SubFlowGroupID.ToString();
                    result.IsSuccess = false;
                    result.Messages = "当前步骤的子流程未完成,不能提交!";
                    return;
                }

                //如果是完成任务或者没有后续处理步骤，则完成任务
                if (executeModel.ExecuteType == Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed
                    || executeModel.Steps == null || executeModel.Steps.Count == 0)
                {
                    executeComplete(executeModel);
                    scope.Complete();
                    return;
                }

                int status = 0;

                #region 处理策略判断
                if (currentTask.StepID != wfInstalled.FirstStepID)//第一步不判断策略
                {
                    switch (currentStep.Behavior.HanlderModel)
                    {
                        case 0://所有人必须处理
                            var taskList = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                            if (taskList.Count > 1)
                            {
                                var noCompleted = taskList.Where(p => p.Status != 2);
                                if (noCompleted.Count() - 1 > 0)
                                {
                                    status = -1;
                                }
                            }
                            Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign);
                            break;
                        case 1://一人同意即可
                            var taskList1 = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                            foreach (var task in taskList1)
                            {
                                if (task.ID != currentTask.ID)
                                {
                                    if (task.Status.In(0, 1))
                                    {
                                        Completed(task.ID, "", false, 4);
                                    }
                                }
                                else
                                {
                                    Completed(task.ID, executeModel.Comment, executeModel.IsSign);
                                }
                            }
                            break;
                        case 2://依据人数比例
                            var taskList2 = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                            if (taskList2.Count > 1)
                            {
                                decimal percentage = currentStep.Behavior.Percentage <= 0 ? 100 : currentStep.Behavior.Percentage;//比例
                                if ((((decimal)(taskList2.Where(p => p.Status == 2).Count() + 1) / (decimal)taskList2.Count) * 100).Round() < percentage)
                                {
                                    status = -1;
                                }
                                else
                                {
                                    foreach (var task in taskList2)
                                    {
                                        if (task.ID != currentTask.ID && task.Status.In(0, 1))
                                        {
                                            Completed(task.ID, "", false, 4);
                                        }
                                    }
                                }
                            }
                            Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign);
                            break;
                        case 3://独立处理
                            Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign);
                            break;
                    }
                }
                else
                {
                    Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign);
                }
                #endregion

                //如果条件不满足则创建一个状态为-1的后续任务，等条件满足后才修改状态，待办人员才能看到任务。
                if (status == -1)
                {
                    var tempTasks = createTempTasks(executeModel, currentTask);
                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in tempTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    nextTasks.AddRange(tempTasks);
                    string stepName = nextStepName.Distinct().ToArray().Join1(",");
                    result.DebugMessages += string.Format("已发送到:{0},其他人未处理,不创建后续任务", stepName);
                    result.IsSuccess = true;
                    result.Messages += string.Format("已发送到:{0},等待他人处理!", stepName);
                    result.NextTasks = nextTasks;
                    scope.Complete();
                    return;
                }
                
                foreach (var step in executeModel.Steps)
                {
                    foreach (var user in step.Value)
                    {
                        if (wfInstalled == null) //子流程有多个人员时此处会为空，所以判断并重新获取
                        {
                            wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
                        }
                        var nextSteps = wfInstalled.Steps.Where(p => p.ID == step.Key);
                        if (nextSteps.Count() == 0)
                        {
                            continue;
                        }
                        var nextStep = nextSteps.First();

                        bool isPassing = 0 == nextStep.Behavior.Countersignature;

                        #region 如果下一步骤为会签，则要检查当前步骤的平级步骤是否已处理
                        if (0 != nextStep.Behavior.Countersignature)
                        {
                            var prevSteps = bWorkFlow.GetPrevSteps(executeModel.FlowID, nextStep.ID);
                            switch (nextStep.Behavior.Countersignature)
                            { 
                                case 1://所有步骤同意
                                    isPassing = true;
                                    foreach (var prevStep in prevSteps)
                                    {
                                        if (!IsPassing(prevStep, executeModel.FlowID, executeModel.GroupID, currentTask.PrevID, currentTask.Sort))
                                        {
                                            isPassing = false;
                                            break;
                                        }
                                    }
                                    break;
                                case 2://一个步骤同意即可
                                    isPassing = false;
                                    foreach (var prevStep in prevSteps)
                                    {
                                        if (IsPassing(prevStep, executeModel.FlowID, executeModel.GroupID, currentTask.PrevID, currentTask.Sort))
                                        {
                                            isPassing = true;
                                            break;
                                        }
                                    }
                                    break;
                                case 3://依据比例
                                    int passCount = 0;
                                    foreach (var prevStep in prevSteps)
                                    {
                                        if (IsPassing(prevStep, executeModel.FlowID, executeModel.GroupID, currentTask.PrevID, currentTask.Sort))
                                        {
                                            passCount++;
                                        }
                                    }
                                    isPassing = (((decimal)passCount / (decimal)prevSteps.Count) * 100).Round() >= (nextStep.Behavior.CountersignaturePercentage <= 0 ? 100 : nextStep.Behavior.CountersignaturePercentage);
                                    break;
                            }
                            if (isPassing)
                            {
                                var tjTasks = GetTaskList(currentTask.ID, false);
                                foreach (var tjTask in tjTasks)
                                {
                                    if (tjTask.ID == currentTask.ID || tjTask.Status.In(2, 3, 4, 5))
                                    {
                                        continue;
                                    }
                                    Completed(tjTask.ID, "", false, 4);
                                }
                            }
                        }
                        #endregion

                        if (isPassing)
                        {
                            RoadFlow.Data.Model.WorkFlowTask task = new RoadFlow.Data.Model.WorkFlowTask();
                            if (nextStep.WorkTime > 0)
                            {
                                task.CompletedTime = RoadFlow.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                            }

                            task.FlowID = executeModel.FlowID;
                            task.GroupID = currentTask != null ? currentTask.GroupID : executeModel.GroupID;
                            task.ID = Guid.NewGuid();
                            task.Type = 0;
                            task.InstanceID = executeModel.InstanceID;
                            if (!executeModel.Note.IsNullOrEmpty())
                            {
                                task.Note = executeModel.Note;
                            }
                            task.PrevID = currentTask.ID;
                            task.PrevStepID = currentTask.StepID;
                            task.ReceiveID = user.ID;
                            task.ReceiveName = user.Name;
                            task.ReceiveTime = RoadFlow.Utility.DateTimeNew.Now;
                            task.SenderID = executeModel.Sender.ID;
                            task.SenderName = executeModel.Sender.Name;
                            task.SenderTime = task.ReceiveTime;
                            task.Status = status;
                            task.StepID = step.Key;
                            task.StepName = nextStep.Name;
                            task.Sort = currentTask.Sort + 1;
                            task.Title = executeModel.Title.IsNullOrEmpty() ? currentTask.Title : executeModel.Title;

                            #region 如果当前步骤是子流程步骤，则要发起子流程实例
                            if (nextStep.Type == "subflow" && nextStep.SubFlowID.IsGuid())
                            {
                                RoadFlow.Data.Model.WorkFlowExecute.Execute subflowExecuteModel = new RoadFlow.Data.Model.WorkFlowExecute.Execute();
                                if (!nextStep.Event.SubFlowActivationBefore.IsNullOrEmpty())
                                {
                                    object obj = ExecuteFlowCustomEvent(nextStep.Event.SubFlowActivationBefore.Trim(),
                                        new RoadFlow.Data.Model.WorkFlowCustomEventParams()
                                        {
                                            FlowID = executeModel.FlowID,
                                            GroupID = currentTask.GroupID,
                                            InstanceID = currentTask.InstanceID,
                                            StepID = executeModel.StepID,
                                            TaskID = currentTask.ID
                                        });
                                    if (obj is RoadFlow.Data.Model.WorkFlowExecute.Execute)
                                    {
                                        subflowExecuteModel = obj as RoadFlow.Data.Model.WorkFlowExecute.Execute;
                                    }
                                }
                                subflowExecuteModel.ExecuteType = RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save;
                                subflowExecuteModel.FlowID = nextStep.SubFlowID.ToGuid();
                                subflowExecuteModel.Sender = user;
                                if (subflowExecuteModel.Title.IsNullOrEmpty())
                                {
                                    subflowExecuteModel.Title = bWorkFlow.GetFlowName(subflowExecuteModel.FlowID);
                                }
                                if (subflowExecuteModel.InstanceID.IsNullOrEmpty())
                                {
                                    subflowExecuteModel.InstanceID = "";
                                }
                                var subflowTask = createFirstTask(subflowExecuteModel, true);
                                task.SubFlowGroupID = subflowTask.GroupID;
                            }
                            #endregion

                            if (!HasNoCompletedTasks(executeModel.FlowID, step.Key, currentTask.GroupID, user.ID))
                            {
                                Add(task);
                            }
                            nextTasks.Add(task);
                        }
                    }

                }

                if (nextTasks.Count > 0)
                {
                    //激活临时任务
                    dataWorkFlowTask.UpdateTempTasks(nextTasks.First().FlowID, nextTasks.First().StepID, nextTasks.First().GroupID,
                        nextTasks.First().CompletedTime, nextTasks.First().ReceiveTime);

                    #region 抄送
                    if (wfInstalled == null)
                    {
                        wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
                    }
                    foreach (var step in executeModel.Steps)
                    {
                        var nextSteps = wfInstalled.Steps.Where(p => p.ID == step.Key);
                        if (nextSteps.Count() > 0)
                        {
                            var nextStep = nextSteps.First();
                            if (!nextStep.Behavior.CopyFor.IsNullOrEmpty())
                            {
                                var users = new Organize().GetAllUsers(nextStep.Behavior.CopyFor);
                                foreach (var user in users)
                                {
                                    RoadFlow.Data.Model.WorkFlowTask task = new RoadFlow.Data.Model.WorkFlowTask();
                                    if (nextStep.WorkTime > 0)
                                    {
                                        task.CompletedTime = RoadFlow.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                                    }
                                    task.FlowID = executeModel.FlowID;
                                    task.GroupID = currentTask != null ? currentTask.GroupID : executeModel.GroupID;
                                    task.ID = Guid.NewGuid();
                                    task.Type = 5;
                                    task.InstanceID = executeModel.InstanceID;
                                    task.Note = executeModel.Note.IsNullOrEmpty() ? "抄送任务" : executeModel.Note + "(抄送任务)";
                                    task.PrevID = currentTask.ID;
                                    task.PrevStepID = currentTask.StepID;
                                    task.ReceiveID = user.ID;
                                    task.ReceiveName = user.Name;
                                    task.ReceiveTime = RoadFlow.Utility.DateTimeNew.Now;
                                    task.SenderID = executeModel.Sender.ID;
                                    task.SenderName = executeModel.Sender.Name;
                                    task.SenderTime = task.ReceiveTime;
                                    task.Status = status;
                                    task.StepID = step.Key;
                                    task.StepName = nextStep.Name;
                                    task.Sort = currentTask.Sort + 1;
                                    task.Title = executeModel.Title.IsNullOrEmpty() ? currentTask.Title : executeModel.Title;
                                    if (!HasNoCompletedTasks(executeModel.FlowID, step.Key, currentTask.GroupID, user.ID))
                                    {
                                        Add(task);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in nextTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    string stepName = nextStepName.Distinct().ToArray().Join1(",");
                    result.DebugMessages += string.Format("已发送到:{0}", stepName);
                    result.IsSuccess = true;
                    result.Messages += string.Format("已发送到:{0}", stepName);
                    result.NextTasks = nextTasks;
                }
                else
                {
                    var tempTasks = createTempTasks(executeModel, currentTask);
                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in tempTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    nextTasks.AddRange(tempTasks);
                    string stepName = nextStepName.Distinct().ToArray().Join1(",");
                    result.DebugMessages += string.Format("已发到:{0},等待其它步骤处理", stepName);
                    result.IsSuccess = true;
                    result.Messages += string.Format("已发送:{0},等待其它步骤处理", stepName);
                    result.NextTasks = nextTasks;
                }
                scope.Complete();
            }
        }

        /// <summary>
        /// 退回任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeBack(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            var currentTask = Get(executeModel.TaskID);
            if (currentTask == null)
            {
                result.DebugMessages = "未能找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未能找到当前任务";
                return;
            }
            else if (currentTask.Status.In(2, 3, 4, 5))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }

            var currentSteps = wfInstalled.Steps.Where(p => p.ID == currentTask.StepID);
            var currentStep = currentSteps.Count() > 0 ? currentSteps.First() : null;

            if (currentStep == null)
            {
                result.DebugMessages = "未能找到当前步骤";
                result.IsSuccess = false;
                result.Messages = "未能找到当前步骤";
                return;
            }
            if (currentTask.StepID == wfInstalled.FirstStepID)
            {
                result.DebugMessages = "当前任务是流程第一步,不能退回";
                result.IsSuccess = false;
                result.Messages = "当前任务是流程第一步,不能退回";
                return;
            }
            if (executeModel.Steps.Count == 0)
            {
                result.DebugMessages = "没有选择要退回的步骤";
                result.IsSuccess = false;
                result.Messages = "没有选择要退回的步骤";
                return;
            }
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                List<RoadFlow.Data.Model.WorkFlowTask> backTasks = new List<RoadFlow.Data.Model.WorkFlowTask>();
                int status = 0;
                switch (currentStep.Behavior.BackModel)
                {
                    case 0://不能退回
                        result.DebugMessages = "当前步骤设置为不能退回";
                        result.IsSuccess = false;
                        result.Messages = "当前步骤设置为不能退回";
                        return;
                    #region 根据策略退回
                    case 1:
                        switch (currentStep.Behavior.HanlderModel)
                        {
                            case 0://所有人必须同意,如果一人不同意则全部退回
                                var taskList = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                                foreach (var task in taskList)
                                {
                                    if (task.ID != currentTask.ID)
                                    {
                                        if (task.Status.In(0, 1))
                                        {
                                            Completed(task.ID, "", false, 5);
                                            //backTasks.Add(task);
                                        }
                                    }
                                    else
                                    {
                                        Completed(task.ID, executeModel.Comment, executeModel.IsSign, 3);
                                    }
                                }
                                break;
                            case 1://一人同意即可
                                var taskList1 = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                                if (taskList1.Count > 1)
                                {
                                    var noCompleted = taskList1.Where(p => p.Status != 3);
                                    if (noCompleted.Count() - 1 > 0)
                                    {
                                        status = -1;
                                    }
                                }
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3);
                                break;
                            case 2://依据人数比例
                                var taskList2 = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                                if (taskList2.Count > 1)
                                {
                                    decimal percentage = currentStep.Behavior.Percentage <= 0 ? 100 : currentStep.Behavior.Percentage;//比例
                                    if ((((decimal)(taskList2.Where(p => p.Status == 3).Count() + 1) / (decimal)taskList2.Count) * 100).Round() < 100 - percentage)
                                    {
                                        status = -1;
                                    }
                                    else
                                    {
                                        foreach (var task in taskList2)
                                        {
                                            if (task.ID != currentTask.ID && task.Status.In(0, 1))
                                            {
                                                Completed(task.ID, "", false, 5);
                                                //backTasks.Add(task);
                                            }
                                        }
                                    }
                                }
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3);
                                break;
                            case 3://独立处理
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3);
                                break;
                        }
                        backTasks.Add(currentTask);
                        break;
                    #endregion
                }

                if (status == -1)
                {
                    result.DebugMessages += "已退回,等待他人处理";
                    result.IsSuccess = true;
                    result.Messages += "已退回,等待他人处理!";
                    result.NextTasks = nextTasks;
                    scope.Complete();
                    return;
                }

                foreach (var backTask in backTasks)
                {
                    if (backTask.Status.In(2, 3))//已完成的任务不能退回
                    {
                        continue;
                    }
                    if (backTask.ID == currentTask.ID)
                    {
                        Completed(backTask.ID, executeModel.Comment, executeModel.IsSign, 3);
                    }
                    else
                    {
                        Completed(backTask.ID, "", false, 3, "他人已退回");
                    }
                }

                List<RoadFlow.Data.Model.WorkFlowTask> tasks = new List<RoadFlow.Data.Model.WorkFlowTask>();
                if (currentStep.Behavior.HanlderModel.In(0, 1, 2))//退回时要退回其它步骤发来的同级任务。
                {
                    var tjTasks = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort);
                    foreach (var tjTask in tjTasks)
                    {
                        if (!executeModel.Steps.ContainsKey(tjTask.PrevStepID))
                        {
                            executeModel.Steps.Add(tjTask.PrevStepID, new List<Data.Model.Users>());
                        }
                    }
                }
                foreach (var step in executeModel.Steps)
                {
                    var tasks1 = GetTaskList(executeModel.FlowID, step.Key, executeModel.GroupID);
                    if (tasks1.Count > 0)
                    {
                        tasks1 = tasks1.OrderByDescending(p => p.Sort).ToList();
                        int maxSort = tasks1.First().Sort;
                        tasks.AddRange(tasks1.FindAll(p => p.Sort == maxSort));
                    }
                }

                #region 处理会签形式的退回
                //当前步骤是否是会签步骤
                var countersignatureStep = bWorkFlow.GetNextSteps(executeModel.FlowID, executeModel.StepID).Find(p => p.Behavior.Countersignature != 0);
                bool IsCountersignature = countersignatureStep != null;
                bool isBack = true;
                if (IsCountersignature)
                {
                    var steps = bWorkFlow.GetPrevSteps(executeModel.FlowID, countersignatureStep.ID);
                    switch (countersignatureStep.Behavior.Countersignature)
                    {
                        case 1://所有步骤处理，如果一个步骤退回则退回
                            isBack = false;
                            foreach (var step in steps)
                            {
                                if (IsBack(step, executeModel.FlowID, currentTask.GroupID, currentTask.PrevID, currentTask.Sort))
                                {
                                    isBack = true;
                                    break;
                                }
                            }
                            break;
                        case 2://一个步骤退回,如果有一个步骤同意，则不退回
                            isBack = true;
                            foreach (var step in steps)
                            {
                                if (!IsBack(step, executeModel.FlowID, currentTask.GroupID, currentTask.PrevID, currentTask.Sort))
                                {
                                    isBack = false;
                                    break;
                                }
                            }
                            break;
                        case 3://依据比例退回
                            int backCount = 0;
                            foreach (var step in steps)
                            {
                                if (IsBack(step, executeModel.FlowID, currentTask.GroupID, currentTask.PrevID, currentTask.Sort))
                                {
                                    backCount++;
                                }
                            }
                            isBack = (((decimal)backCount / (decimal)steps.Count) * 100).Round() >= (countersignatureStep.Behavior.CountersignaturePercentage <= 0 ? 100 : countersignatureStep.Behavior.CountersignaturePercentage);
                            break;
                    }

                    if (isBack)
                    {
                        var tjTasks = GetTaskList(currentTask.ID, false);
                        foreach (var tjTask in tjTasks)
                        {
                            if (tjTask.ID == currentTask.ID || tjTask.Status.In(2, 3, 4, 5))
                            {
                                continue;
                            }
                            Completed(tjTask.ID, "", false, 5);
                        }
                    }
                }
                #endregion

                //如果退回步骤是子流程步骤，则要作废子流程实例
                if (currentStep.Type == "subflow" && currentStep.SubFlowID.IsGuid() && currentTask.SubFlowGroupID.HasValue)
                {
                    DeleteInstance(currentStep.SubFlowID.ToGuid(), currentTask.SubFlowGroupID.Value, true);
                }

                if (isBack)
                {
                    var backTaskList = tasks.Distinct(this);
                    if (backTaskList.Count() == 0)
                    {
                        Completed(currentTask.ID, "", false, 0, "");
                        result.DebugMessages += "没有接收人,退回失败!";
                        result.IsSuccess = false;
                        result.Messages += "没有接收人,退回失败!";
                        result.NextTasks = nextTasks;
                        scope.Complete();
                        return;
                    }

                    foreach (var task in backTaskList)
                    {
                        if (task != null)
                        {
                            //删除抄送
                            if (task.Type == 5)
                            {
                                Delete(task.ID);
                                continue;
                            }

                            RoadFlow.Data.Model.WorkFlowTask newTask = task;
                            newTask.ID = Guid.NewGuid();
                            newTask.PrevID = currentTask.ID;
                            newTask.Note = "退回任务";
                            newTask.ReceiveTime = RoadFlow.Utility.DateTimeNew.Now;
                            newTask.SenderID = currentTask.ReceiveID;
                            newTask.SenderName = currentTask.ReceiveName;
                            newTask.SenderTime = RoadFlow.Utility.DateTimeNew.Now;
                            newTask.Sort = currentTask.Sort + 1;
                            newTask.Status = 0;
                            newTask.Type = 4;
                            newTask.Comment = "";
                            newTask.OpenTime = null;
                            //newTask.PrevStepID = currentTask.StepID;
                            if (currentStep.WorkTime > 0)
                            {
                                newTask.CompletedTime = RoadFlow.Utility.DateTimeNew.Now.AddHours((double)currentStep.WorkTime);
                            }
                            else
                            {
                                newTask.CompletedTime = null;
                            }
                            newTask.CompletedTime1 = null;
                            Add(newTask);
                            nextTasks.Add(newTask);
                        }
                    }

                    //删除临时任务
                    var nextSteps = bWorkFlow.GetNextSteps(executeModel.FlowID, executeModel.StepID);
                    foreach(var step in nextSteps)
                    {
                        dataWorkFlowTask.DeleteTempTasks(currentTask.FlowID, step.ID, currentTask.GroupID,
                            IsCountersignature ? Guid.Empty : currentStep.ID
                            );
                    }
                }

                scope.Complete();
            }

            if (nextTasks.Count > 0)
            {
                List<string> nextStepName = new List<string>();
                foreach (var nstep in nextTasks)
                {
                    nextStepName.Add(nstep.StepName);
                }
                string msg = string.Format("已退回到:{0}", nextStepName.Distinct().ToArray().Join1(","));
                result.DebugMessages += msg;
                result.IsSuccess = true;
                result.Messages += msg;
                result.NextTasks = nextTasks;
            }
            else
            {
                result.DebugMessages += "已退回,等待其它步骤处理";
                result.IsSuccess = true;
                result.Messages += "已退回,等待其它步骤处理";
                result.NextTasks = nextTasks;
            }
            return;
        }

        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeSave(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            //如果是第一步提交并且没有实例则先创建实例
            RoadFlow.Data.Model.WorkFlowTask currentTask = null;
            bool isFirst = executeModel.StepID == wfInstalled.FirstStepID && executeModel.TaskID == Guid.Empty && executeModel.GroupID == Guid.Empty;
            if (isFirst)
            {
                currentTask = createFirstTask(executeModel);
            }
            else
            {
                currentTask = Get(executeModel.TaskID);
            }
            if (currentTask == null)
            {
                result.DebugMessages = "未能创建或找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未能创建或找到当前任务";
                return;
            }
            else if (currentTask.Status.In(2, 3, 4))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            else
            {
                currentTask.InstanceID = executeModel.InstanceID;
                nextTasks.Add(currentTask);
                if (isFirst)
                {
                    currentTask.Title = executeModel.Title.IsNullOrEmpty() ? "未命名任务" : executeModel.Title;
                    Update(currentTask);
                }
                else
                {
                    if (!executeModel.Title.IsNullOrEmpty())
                    {
                        currentTask.Title = executeModel.Title;
                        Update(currentTask);
                    }
                }
            }

            result.DebugMessages = "保存成功";
            result.IsSuccess = true;
            result.Messages = "保存成功";
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="executeModel"></param>
        /// <param name="isCompleteTask">是否需要调用Completed方法完成当前任务</param>
        private void executeComplete(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel, bool isCompleteTask = true)
        {
            if (executeModel.TaskID == Guid.Empty || executeModel.FlowID == Guid.Empty)
            {
                result.DebugMessages = "完成流程参数错误";
                result.IsSuccess = false;
                result.Messages = "完成流程参数错误";
                return;
            }
            var task = Get(executeModel.TaskID);
            if (task == null)
            {
                result.DebugMessages = "未找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未找到当前任务";
                return;
            }
            else if (isCompleteTask && task.Status.In(2, 3, 4))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            if (isCompleteTask)
            {
                Completed(task.ID, executeModel.Comment, executeModel.IsSign);
            }

            #region 更新业务表标识字段的值为1
            if (wfInstalled.TitleField != null && wfInstalled.TitleField.LinkID != Guid.Empty && !wfInstalled.TitleField.Table.IsNullOrEmpty()
                && !wfInstalled.TitleField.Field.IsNullOrEmpty() && wfInstalled.DataBases.Count() > 0)
            {
                var firstDB = wfInstalled.DataBases.First();
                new DBConnection().UpdateFieldValue(
                    wfInstalled.TitleField.LinkID,
                    wfInstalled.TitleField.Table,
                    wfInstalled.TitleField.Field,
                    "1",
                   string.Format("{0}='{1}'", firstDB.PrimaryKey, task.InstanceID));
            }
            #endregion

            #region 执行子流程完成后事件
            var parentTasks = GetBySubFlowGroupID(task.GroupID);
            if (parentTasks.Count > 0)
            {
                var parentTask = parentTasks.First();
                var flowRunModel = bWorkFlow.GetWorkFlowRunModel(parentTask.FlowID);
                if (flowRunModel != null)
                {
                    var steps = flowRunModel.Steps.Where(p => p.ID == parentTask.StepID);
                    if (steps.Count() > 0 && !steps.First().Event.SubFlowCompletedBefore.IsNullOrEmpty())
                    {
                        ExecuteFlowCustomEvent(steps.First().Event.SubFlowCompletedBefore.Trim(), new RoadFlow.Data.Model.WorkFlowCustomEventParams()
                        {
                            FlowID = parentTask.FlowID,
                            GroupID = parentTask.GroupID,
                            InstanceID = parentTask.InstanceID,
                            StepID = parentTask.StepID,
                            TaskID = parentTask.ID
                        });
                    }
                }
            }
            #endregion

            result.DebugMessages += "已完成";
            result.IsSuccess = true;
            result.Messages += "已完成";
        }

        /// <summary>
        /// 转交任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeRedirect(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            RoadFlow.Data.Model.WorkFlowTask currentTask = Get(executeModel.TaskID);
            if (currentTask == null)
            {
                result.DebugMessages = "未能创建或找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未能创建或找到当前任务";
                return;
            }
            else if (currentTask.Status.In(2, 3, 4, 5))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            else if (currentTask.Status == -1)
            {
                result.DebugMessages = "当前任务正在等待他人处理";
                result.IsSuccess = false;
                result.Messages = "当前任务正在等待他人处理";
                return;
            }
            if (executeModel.Steps.First().Value.Count == 0)
            {
                result.DebugMessages = "未设置转交人员";
                result.IsSuccess = false;
                result.Messages = "未设置转交人员";
                return;
            }
            string receiveName = currentTask.ReceiveName;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (var user in executeModel.Steps.First().Value)
                {
                    currentTask.ID = Guid.NewGuid();
                    currentTask.ReceiveID = user.ID;
                    currentTask.ReceiveName = user.Name;
                    currentTask.OpenTime = null;
                    currentTask.Status = 0;
                    currentTask.IsSign = 0;
                    currentTask.Type = 3;
                    currentTask.Note = string.Format("该任务由{0}转交", receiveName);
                    if (!HasNoCompletedTasks(currentTask.FlowID, currentTask.StepID, currentTask.GroupID, user.ID))
                    {
                        Add(currentTask);
                    }
                    nextTasks.Add(currentTask);
                }
                Completed(executeModel.TaskID, executeModel.Comment, executeModel.IsSign, 2, "已转交他人处理");
                scope.Complete();
            }
            List<string> nextStepName = new List<string>();
            foreach (var user in executeModel.Steps.First().Value)
            {
                nextStepName.Add(user.Name);
            }
            string userName = nextStepName.Distinct().ToArray().Join1(",");
            result.DebugMessages = string.Concat("已转交给:", userName);
            result.IsSuccess = true;
            result.Messages = string.Concat("已转交给:", userName);
            return;
        }

        /// <summary>
        /// 创建临时任务（待办人员看不到）
        /// </summary>
        /// <param name="executeModel"></param>
        private List<Data.Model.WorkFlowTask> createTempTasks(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel, Data.Model.WorkFlowTask currentTask)
        {
            List<Data.Model.WorkFlowTask> tasks = new List<Data.Model.WorkFlowTask>();
            foreach (var step in executeModel.Steps)
            {
                foreach (var user in step.Value)
                {
                    var nextSteps = wfInstalled.Steps.Where(p => p.ID == step.Key);
                    if (nextSteps.Count() == 0)
                    {
                        continue;
                    }
                    var nextStep = nextSteps.First();
                    RoadFlow.Data.Model.WorkFlowTask task = new RoadFlow.Data.Model.WorkFlowTask();
                    if (nextStep.WorkTime > 0)
                    {
                        task.CompletedTime = RoadFlow.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                    }
                    task.FlowID = executeModel.FlowID;
                    task.GroupID = currentTask != null ? currentTask.GroupID : executeModel.GroupID;
                    task.ID = Guid.NewGuid();
                    task.Type = 0;
                    task.InstanceID = executeModel.InstanceID;
                    if (!executeModel.Note.IsNullOrEmpty())
                    {
                        task.Note = executeModel.Note;
                    }
                    task.PrevID = currentTask.ID;
                    task.PrevStepID = currentTask.StepID;
                    task.ReceiveID = user.ID;
                    task.ReceiveName = user.Name;
                    task.ReceiveTime = RoadFlow.Utility.DateTimeNew.Now;
                    task.SenderID = executeModel.Sender.ID;
                    task.SenderName = executeModel.Sender.Name;
                    task.SenderTime = task.ReceiveTime;
                    task.Status = -1;
                    task.StepID = step.Key;
                    task.StepName = nextStep.Name;
                    task.Sort = currentTask.Sort + 1;
                    task.Title = executeModel.Title.IsNullOrEmpty() ? currentTask.Title : executeModel.Title;

                    if (!HasNoCompletedTasks(executeModel.FlowID, step.Key, currentTask.GroupID, user.ID))
                    {
                        Add(task);
                    }
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        /// <summary>
        /// 判断一个步骤是否通过
        /// </summary>
        /// <param name="step"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private bool IsPassing(RoadFlow.Data.Model.WorkFlowInstalledSub.Step step, Guid flowID, Guid groupID, Guid taskID, int sort)
        {
            var tasks = GetTaskList(flowID, step.ID, groupID).FindAll(p => p.Sort == sort && p.Type != 5);
            if (tasks.Count == 0)
            {
                return false;
            }
            bool isPassing = true;
            switch (step.Behavior.HanlderModel)
            { 
                case 0://所有人必须处理
                case 3://独立处理
                    isPassing = tasks.Where(p => p.Status != 2).Count() == 0;
                    break;
                case 1://一人同意即可
                    isPassing = tasks.Where(p => p.Status == 2).Count() > 0;
                    break;
                case 2://依据人数比例
                    isPassing = (((decimal)(tasks.Where(p => p.Status == 2).Count() + 1) / (decimal)tasks.Count) * 100).Round() >= (step.Behavior.Percentage <= 0 ? 100 : step.Behavior.Percentage);
                    break;
            }
            return isPassing;
        }

        /// <summary>
        /// 判断一个步骤是否退回
        /// </summary>
        /// <param name="step"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private bool IsBack(RoadFlow.Data.Model.WorkFlowInstalledSub.Step step, Guid flowID, Guid groupID, Guid taskID, int sort)
        {
            var tasks = GetTaskList(flowID, step.ID, groupID).FindAll(p => p.Sort == sort && p.Type != 5);
            if (tasks.Count == 0)
            {
                return false;
            }
            bool isBack = true;
            switch (step.Behavior.HanlderModel)
            {
                case 0://所有人必须处理
                case 3://独立处理
                    isBack = tasks.Where(p => p.Status.In(3,5)).Count() > 0;
                    break;
                case 1://一人同意即可
                    isBack = tasks.Where(p => p.Status.In(2,4)).Count() == 0;
                    break;
                case 2://依据人数比例
                    isBack = (((decimal)(tasks.Where(p => p.Status.In(3, 5)).Count() + 1) / (decimal)tasks.Count) * 100).Round() >= 100 - (step.Behavior.Percentage <= 0 ? 100 : step.Behavior.Percentage);
                    break;
            }
            return isBack;
        }

        /// <summary>
        /// 创建第一个任务
        /// </summary>
        /// <param name="executeModel"></param>
        /// <param name="isSubFlow">是否是创建子流程任务</param>
        /// <returns></returns>
        private RoadFlow.Data.Model.WorkFlowTask createFirstTask(RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel, bool isSubFlow = false)
        {
            if (wfInstalled == null || isSubFlow)
            {
                wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
            }
            
            var nextSteps = wfInstalled.Steps.Where(p => p.ID == wfInstalled.FirstStepID);
            if (nextSteps.Count() == 0)
            {
                return null;
            }
            RoadFlow.Data.Model.WorkFlowTask task = new RoadFlow.Data.Model.WorkFlowTask();
            if (nextSteps.First().WorkTime > 0)
            {
                task.CompletedTime = RoadFlow.Utility.DateTimeNew.Now.AddHours((double)nextSteps.First().WorkTime);
            }
            task.FlowID = executeModel.FlowID;
            task.GroupID = Guid.NewGuid();
            task.ID = Guid.NewGuid();
            task.Type = 0;
            task.InstanceID = executeModel.InstanceID;
            if (!executeModel.Note.IsNullOrEmpty())
            {
                task.Note = executeModel.Note;
            }
            task.PrevID = Guid.Empty;
            task.PrevStepID = Guid.Empty;
            task.ReceiveID = executeModel.Sender.ID;
            task.ReceiveName = executeModel.Sender.Name;
            task.ReceiveTime = RoadFlow.Utility.DateTimeNew.Now;
            task.SenderID = executeModel.Sender.ID;
            task.SenderName = executeModel.Sender.Name;
            task.SenderTime = task.ReceiveTime;
            task.Status = 0;
            task.StepID = wfInstalled.FirstStepID;
            task.StepName = nextSteps.First().Name;
            task.Sort = 1;
            task.Title = executeModel.Title.IsNullOrEmpty() ? "未命名任务(" + wfInstalled.Name + ")" : executeModel.Title;
            Add(task);
            if (isSubFlow)
            {
                wfInstalled = null;
            }
            return task;
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
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, string query = "", string title = "", string flowid = "", string sender = "", string date1 = "", string date2 = "", int type = 0)
        {
            return dataWorkFlowTask.GetTasks(userID, out pager, query, title, flowid, RoadFlow.Platform.Users.RemovePrefix(sender), date1, date2, type);
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
        /// <param name="isCompleted">是否完成 0:全部 1:未完成 2:已完成</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, string query = "", string title = "", string flowid = "", string date1 = "", string date2 = "", int status = 0)
        {
            return dataWorkFlowTask.GetInstances(flowID, senderID, receiveID, out pager, query, title, flowid, date1, date2, status);
        }

        /// <summary>
        /// 执行自定义方法
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public object ExecuteFlowCustomEvent(string eventName, object eventParams, string dllName = "")
        {
            if (dllName.IsNullOrEmpty())
            {
                dllName = eventName.Substring(0, eventName.IndexOf('.'));
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(dllName);
            string typeName = System.IO.Path.GetFileNameWithoutExtension(eventName);
            string methodName = eventName.Substring(typeName.Length + 1);
            Type type = assembly.GetType(typeName, true);

            object obj = System.Activator.CreateInstance(type, false);
            var method = type.GetMethod(methodName);

            if (method != null)
            {
                return method.Invoke(obj, new object[] { eventParams });
            }
            else
            {
                throw new MissingMethodException(typeName, methodName);
            }
        }
        
        /// <summary>
        /// 删除流程实例
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int DeleteInstance(Guid flowID, Guid groupID, bool hasInstanceData = false)
        {
            if (hasInstanceData)
            {
                var tasks = GetTaskList(flowID, groupID);
                if (tasks.Count > 0 && !tasks.First().InstanceID.IsNullOrEmpty())
                {
                    var wfRunModel = bWorkFlow.GetWorkFlowRunModel(flowID);
                    if (wfRunModel != null && wfRunModel.DataBases.Count() > 0)
                    {
                        var dataBase = wfRunModel.DataBases.First();
                        new DBConnection().DeleteData(dataBase.LinkID, dataBase.Table, dataBase.PrimaryKey, tasks.First().InstanceID);
                    }
                }
            }
            return dataWorkFlowTask.Delete(flowID, groupID);
        }

        /// <summary>
        /// 完成一个任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="comment"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        public int Completed(Guid taskID, string comment = "", bool isSign = false, int status = 2, string note = "")
        {
            return dataWorkFlowTask.Completed(taskID, comment, isSign, status, note);
        }

        /// <summary>
        /// 得到一个流程实例一个步骤的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        {
            return dataWorkFlowTask.GetTaskList(flowID, stepID, groupID);
        }

        /// <summary>
        /// 得到一个实例的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        {
            return dataWorkFlowTask.GetTaskList(flowID, groupID);
        }

        /// <summary>
        /// 得到和一个任务同级的任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="isStepID">是否区分步骤ID，多步骤会签区分的是上一步骤ID</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskList(Guid taskID, bool isStepID = true)
        {
            return dataWorkFlowTask.GetTaskList(taskID, isStepID);
        }

        /// <summary>
        /// 得到和一个任务同级的任务(同一步骤内)
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="stepID">步骤ID</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskListByStepID(Guid taskID, Guid stepID)
        {
            return dataWorkFlowTask.GetTaskList(taskID).FindAll(p => p.StepID == stepID);
        }

        /// <summary>
        /// 得到一个任务的前一任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetPrevTaskList(Guid taskID)
        {
            return dataWorkFlowTask.GetPrevTaskList(taskID);
        }

        /// <summary>
        /// 得到一个任务的后续任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetNextTaskList(Guid taskID)
        {
            return dataWorkFlowTask.GetNextTaskList(taskID);
        }

        /// <summary>
        /// 得到一个任务可以退回的步骤
        /// </summary>
        /// <param name="taskID">当前任务ID</param>
        /// <param name="backType">退回类型</param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public Dictionary<Guid, string> GetBackSteps(Guid taskID, int backType, Guid stepID, RoadFlow.Data.Model.WorkFlowInstalled wfInstalled)
        {
            Dictionary<Guid, string> dict = new Dictionary<Guid, string>();
            var steps = wfInstalled.Steps.Where(p => p.ID == stepID);
            if (steps.Count() == 0)
            {
                return dict;
            }
            var step = steps.First();
            switch (backType)
            { 
                case 0://退回前一步
                    var task = Get(taskID);
                    if (task != null)
                    {
                        if (step.Behavior.Countersignature != 0)//如果是会签步骤，则要退回到前面所有步骤
                        {
                            var backSteps = bWorkFlow.GetPrevSteps(task.FlowID, step.ID);
                            foreach (var backStep in backSteps)
                            {
                                dict.Add(backStep.ID, backStep.Name);
                            }
                        }
                        else
                        {
                            dict.Add(task.PrevStepID, bWorkFlow.GetStepName(task.PrevStepID, wfInstalled));
                            //switch (step.Behavior.HanlderModel)
                            //{ 
                            //    case 0:
                            //    case 2:
                            //        //var tasks = GetTaskList(task.FlowID, task.StepID, task.GroupID).FindAll(p => p.Sort == task.Sort);
                            //        //foreach (var t in tasks)
                            //        //{
                            //        //    if (!dict.ContainsKey(t.PrevStepID))
                            //        //    {
                            //        //        dict.Add(t.PrevStepID, bWorkFlow.GetStepName(t.PrevStepID, wfInstalled));
                            //        //    }
                            //        //}
                            //        //break;
                            //    case 1:
                            //        //dict.Add(task.PrevStepID, bWorkFlow.GetStepName(task.PrevStepID, wfInstalled));
                            //        //break;
                            //    case 3:
                            //        dict.Add(task.PrevStepID, bWorkFlow.GetStepName(task.PrevStepID, wfInstalled));
                            //        break;
                            //}
                        }
                    }
                    break;
                case 1://退回第一步
                    dict.Add(wfInstalled.FirstStepID, bWorkFlow.GetStepName(wfInstalled.FirstStepID, wfInstalled));
                    break;
                case 2://退回某一步
                    if (step.Behavior.BackType == 2 && step.Behavior.BackStepID != Guid.Empty)
                    {
                        dict.Add(step.Behavior.BackStepID, bWorkFlow.GetStepName(step.Behavior.BackStepID, wfInstalled));
                    }
                    else
                    {
                        var task0 = Get(taskID);
                        if (task0 != null)
                        {
                            var taskList = GetTaskList(task0.FlowID, task0.GroupID).Where(p => p.Status.In(2,3,4)).OrderBy(p => p.Sort);
                            foreach (var task1 in taskList)
                            {
                                if (!dict.Keys.Contains(task1.StepID) && task1.StepID != stepID)
                                {
                                    dict.Add(task1.StepID, bWorkFlow.GetStepName(task1.StepID, wfInstalled));
                                }
                            }
                        }
                    }
                    break;
            }
            return dict;
        }

        /// <summary>
        /// 更新一个任务后续任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="comment"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        public int UpdateNextTaskStatus(Guid taskID, int status)
        {
            int i = 0;
            var taskList = GetTaskList(taskID);

            foreach (var task in taskList)
            {
                i += dataWorkFlowTask.UpdateNextTaskStatus(task.ID, status);
            }
            
            return i;
        }

        /// <summary>
        /// 查询一个流程是否有任务数据
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasTasks(Guid flowID)
        {
            return dataWorkFlowTask.HasTasks(flowID);
        }

        /// <summary>
        /// 查询一个用户在一个步骤是否有未完成任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            return dataWorkFlowTask.HasNoCompletedTasks(flowID, stepID, groupID, userID);
        }

        /// <summary>
        /// 得到状态显示标题
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetStatusTitle(int status)
        {
            string title = string.Empty;
            switch (status)
            {
                case -1:
                    title = "等待中";
                    break;
                case 0:
                    title = "待处理";
                    break;
                case 1:
                    title = "已打开";
                    break;
                case 2:
                    title = "已完成";
                    break;
                case 3:
                    title = "已退回";
                    break;
                case 4:
                    title = "他人已处理";
                    break;
                case 5:
                    title = "他人已退回";
                    break;
                default:
                    title = "其它";
                    break;
            }

            return title;
        }

        /// <summary>
        /// 得到一个流程实例一个步骤一个人员的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            return dataWorkFlowTask.GetUserTaskList(flowID, stepID, groupID, userID);
        }

        /// <summary>
        /// 判断一个任务是否可以收回
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool HasWithdraw(Guid taskID)
        {
            var taskList = GetNextTaskList(taskID);
            if (taskList.Count == 0) return false;
            foreach (var task in taskList)
            {
                if (task.Status.In(1,2,3,4,5))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 收回任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool WithdrawTask(Guid taskID)
        {
            var taskList1 = GetTaskList(taskID);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (var task in taskList1)
                {
                    var taskList2 = GetNextTaskList(task.ID);
                    foreach (var task2 in taskList2)
                    {
                        if (task2.Status.In(-1,0,1,5))
                        {
                            Delete(task2.ID);
                        }
                        if (task2.SubFlowGroupID.HasValue)
                        {
                            DeleteInstance(Guid.Empty, task2.SubFlowGroupID.Value);
                        }
                    }

                    if (task.ID == taskID || task.Status == 4)
                    {
                        Completed(task.ID, "", false, 1, "");
                    }
                }
                scope.Complete();
                return true;
            }
        }

        /// <summary>
        /// 指派任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="user">要指派的人员</param>
        /// <returns></returns>
        public string DesignateTask(Guid taskID, RoadFlow.Data.Model.Users user)
        {
            var task = Get(taskID);
            if (task == null)
            {
                return "未找到任务";
            }
            else if (task.Status.In(2, 3, 4, 5))
            {
                return "该任务已处理";
            }
            string receiveName = task.ReceiveName;
            task.ReceiveID = user.ID;
            task.ReceiveName = user.Name;
            task.OpenTime = null;
            task.Status = 0;
            task.Note = string.Format("该任务由{0}指派", receiveName);
            Update(task);

            return "指派成功";
        }

        /// <summary>
        /// 管理员强制退回任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public string BackTask(Guid taskID)
        {
            var task = Get(taskID);
            if (task == null) 
            {
                return "未找到任务";
            }
            else if (task.Status.In(2, 3, 4, 5))
            {
                return "该任务已处理";
            }
            if(wfInstalled==null) 
            {
                wfInstalled = bWorkFlow.GetWorkFlowRunModel(task.FlowID);
            }
            RoadFlow.Data.Model.WorkFlowExecute.Execute executeModel = new RoadFlow.Data.Model.WorkFlowExecute.Execute();
            executeModel.ExecuteType = RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back;
            executeModel.FlowID = task.FlowID;
            executeModel.GroupID = task.GroupID;
            executeModel.InstanceID = task.InstanceID;
            executeModel.Note = "管理员退回";
            executeModel.Sender = new Users().Get(task.ReceiveID);
            executeModel.StepID = task.StepID;
            executeModel.TaskID = task.ID;
            executeModel.Title = task.Title;
            var steps = wfInstalled.Steps.Where(p => p.ID == task.StepID);
            if(steps.Count()==0) 
            {
                return "未找到步骤";
            }
            else if (steps.First().Behavior.BackType == 2 && steps.First().Behavior.BackStepID == Guid.Empty)
            {
                return "未设置退回步骤";
            }
            Dictionary<Guid, List<RoadFlow.Data.Model.Users>> execSteps = new Dictionary<Guid, List<RoadFlow.Data.Model.Users>>();
            var backsteps = GetBackSteps(taskID, steps.First().Behavior.BackType, task.StepID, wfInstalled);
            foreach (var back in backsteps)
            {
                execSteps.Add(back.Key, new List<RoadFlow.Data.Model.Users>());
            }
            executeModel.Steps = execSteps;
            var result = Execute(executeModel);
            return result.Messages;
        }

        /// <summary>
        /// 排序流程任务列表
        /// </summary>
        /// <param name="tasks"></param>
        public List<RoadFlow.Data.Model.WorkFlowTask> Sort(List<RoadFlow.Data.Model.WorkFlowTask> tasks)
        {
            return tasks.OrderBy(p => p.Sort).ToList();
        }
       
        /// <summary>
        /// 得到一个任务的状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int GetTaskStatus(Guid taskID)
        {
            return dataWorkFlowTask.GetTaskStatus(taskID);
        }

        /// <summary>
        /// 判断一个任务是否可以处理
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool IsExecute(Guid taskID)
        {
            return GetTaskStatus(taskID) <= 1;
        }

        /// <summary>
        /// 判断sql流转条件是否满足
        /// </summary>
        /// <param name="linkID"></param>
        /// <param name="table"></param>
        /// <param name="tablepk"></param>
        /// <param name="instabceID">实例ID</param>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool TestLineSql(Guid linkID, string table, string tablepk, string instabceID, string where)
        {
            if (instabceID.IsNullOrEmpty())
            {
                return false;
            }
            DBConnection dbconn = new DBConnection();
            var conn = dbconn.Get(linkID);
            if (conn == null)
            {
                return false;
            }
            string sql = "SELECT * FROM " + table + " WHERE " + tablepk + "='" + instabceID + "' AND (" + where + ")".ReplaceSelectSql();
            if (!dbconn.TestSql(conn, sql))
            {
                return false;
            }
            System.Data.DataTable dt = dbconn.GetDataTable(conn, sql);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 判断实例是否已完成
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public bool GetInstanceIsCompleted(Guid flowID, Guid groupID)
        {
            var tasks = GetTaskList(flowID, groupID);
            return tasks.Find(p => p.Status.In(0, 1)) == null;
        }

        /// <summary>
        /// 根据SubFlowID得到一个任务
        /// </summary>
        /// <param name="subflowGroupID"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID)
        {
            return dataWorkFlowTask.GetBySubFlowGroupID(subflowGroupID);
        }

        
    }
}