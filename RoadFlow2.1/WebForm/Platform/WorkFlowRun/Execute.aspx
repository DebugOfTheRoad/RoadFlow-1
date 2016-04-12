<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Execute.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.Execute" %>
<% 
    WebForm.Common.Tools.CheckLogin(false);
    string params1 = Request.Form["params"];
    string issign = Request.Form["issign"];
    string comment = Request.Form["comment"];
    
    string flowid = Request.QueryString["flowid"];
    string instanceid = Request.QueryString["instanceid"];
    string taskid = Request.QueryString["taskid"];
    string stepid = Request.QueryString["stepid"];
    string groupid = Request.QueryString["groupid"];
    if(instanceid.IsNullOrEmpty())
    {
        instanceid = Request.Form["instanceid"];
    }

    if(params1.IsNullOrEmpty())
    {
        Response.Write("参数为空!");
        Response.End();
    }
    
    var opationJSON = LitJson.JsonMapper.ToObject(params1);
    string opation = opationJSON["type"].ToString().ToLower();

    RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
    RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();
    RoadFlow.Platform.WorkFlowDelegation bworkFlowDelegation = new RoadFlow.Platform.WorkFlowDelegation();
    RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
    RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
    
    var wfInstalled=bworkFlow.GetWorkFlowRunModel(flowid);
    if(wfInstalled==null)
    {
        Response.Write("未找到流程运行时实体,请确认流程是否已安装!");
        Response.End();
    }
    
    //流程标题
    string titleField = Request.Form["Form_TitleField"];
    string title = Request.Form[titleField];

    RoadFlow.Data.Model.WorkFlowExecute.Execute execute = new RoadFlow.Data.Model.WorkFlowExecute.Execute();
    execute.Comment = comment.IsNullOrEmpty() ? "" : comment.Trim();
    switch(opation)
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
        case "completed":
            execute.ExecuteType = RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed;
            break;
        case "redirect":
            execute.ExecuteType = RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Redirect;
            break;
    }
    
    execute.FlowID = flowid.ToGuid();
    execute.GroupID = groupid.ToGuid();
    execute.InstanceID = instanceid;
    execute.IsSign = "1" == issign;
    execute.Note = "";
    execute.Sender = RoadFlow.Platform.Users.CurrentUser;
    execute.StepID = stepid.IsGuid() ? stepid.ToGuid() : wfInstalled.FirstStepID;
    execute.TaskID = taskid.ToGuid();
    execute.Title = title ?? "";
    
    LitJson.JsonData stepsjson = opationJSON["steps"];
    if(stepsjson.IsArray)
    {
        foreach(LitJson.JsonData step in stepsjson)
        {
            string id = step["id"].ToString();
            string member = step["member"].ToString();
            Guid gid;
            if(id.IsGuid(out gid))
            {
                switch(execute.ExecuteType)
                {
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit:
                        execute.Steps.Add(gid, borganize.GetAllUsers(member));
                        break;
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back:
                        execute.Steps.Add(gid, new List<RoadFlow.Data.Model.Users>());
                        break;
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save:
                        break;
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed:
                        break;
                    case RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Redirect:
                        break;
                }
            }
            if (execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Redirect)
            {
                execute.Steps.Add(Guid.Empty, borganize.GetAllUsers(member));
            }
        }
    }

    RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams = new RoadFlow.Data.Model.WorkFlowCustomEventParams();
    eventParams.FlowID = execute.FlowID;
    eventParams.GroupID = execute.GroupID;
    eventParams.StepID = execute.StepID;
    eventParams.TaskID = execute.TaskID;
    eventParams.InstanceID = execute.InstanceID;

    //保存业务数据 "1" != Request.QueryString["isSystemDetermine"]:当前步骤流转类型如果是系统判断，则先保存数据，在这里就不需要保存数据了。
    if(execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save ||
        execute.ExecuteType== RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed ||
        "1" != Request.QueryString["isSystemDetermine"]
    )
    {
        instanceid = bworkFlow.SaveFromData(instanceid, eventParams);
        if(execute.InstanceID.IsNullOrEmpty())
        {
            execute.InstanceID = instanceid;
            eventParams.InstanceID = instanceid;
        }
    }
    
    Response.Write("执行参数：" + params1 + "<br/>");
  
    var steps = wfInstalled.Steps.Where(p => p.ID == execute.StepID);
    foreach(var step in steps)
    {
        //步骤提交前事件
        if (!step.Event.SubmitBefore.IsNullOrEmpty() && 
            (execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit
            || execute.ExecuteType== RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed))
        {
            object obj = btask.ExecuteFlowCustomEvent(step.Event.SubmitBefore.Trim(), eventParams);
            Response.Write(string.Format("执行步骤提交前事件：({0}) 返回值：{1}<br/>", step.Event.SubmitBefore.Trim(), obj.ToString()));
        }
        //步骤退回前事件
        if (!step.Event.BackBefore.IsNullOrEmpty() && execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back)
        {
            object obj = btask.ExecuteFlowCustomEvent(step.Event.BackBefore.Trim(), eventParams);
            Response.Write(string.Format("执行步骤退回前事件：({0}) 返回值：{1}<br/>", step.Event.BackBefore.Trim(), obj.ToString()));
        }
    }
    
    //处理委托
    foreach(var executeStep in execute.Steps)
    {
        for (int i = 0; i < executeStep.Value.Count; i++)
        {
            Guid newUserID = bworkFlowDelegation.GetFlowDelegationByUserID(execute.FlowID,  executeStep.Value[i].ID);
            if (newUserID != Guid.Empty && newUserID != executeStep.Value[i].ID)
            {
                executeStep.Value[i] = busers.Get(newUserID);
            }
        }
    }
    
    var reslut = btask.Execute(execute);
    Response.Write(string.Format("处理流程步骤结果：{0}<br/>", reslut.IsSuccess ? "成功" : "失败"));
    Response.Write(string.Format("调试信息：{0}", reslut.DebugMessages));
    string msg = reslut.Messages;
    string logContent = string.Format("处理参数：{0}<br/>处理结果：{1}<br/>调试信息：{2}<br/>返回信息：{3}", 
        params1,
        reslut.IsSuccess ? "成功" : "失败", 
        reslut.DebugMessages, 
        reslut.Messages
        );

    RoadFlow.Platform.Log.Add(string.Format("处理了流程({0})", wfInstalled.Name), logContent, RoadFlow.Platform.Log.Types.流程相关);

    foreach (var step in steps)
    {
        //步骤提交后事件
        if (!step.Event.SubmitAfter.IsNullOrEmpty() &&
            (execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit
            || execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed))
        {
            object obj = btask.ExecuteFlowCustomEvent(step.Event.SubmitAfter.Trim(), eventParams);
            Response.Write(string.Format("执行步骤提交后事件：({0}) 返回值：{1}<br/>", step.Event.SubmitAfter.Trim(), obj.ToString()));
        }
        //步骤退回后事件
        if (!step.Event.BackAfter.IsNullOrEmpty() && execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back)
        {
            object obj = btask.ExecuteFlowCustomEvent(step.Event.BackAfter.Trim(), eventParams);
            Response.Write(string.Format("执行步骤退回后事件：({0}) 返回值：{1}<br/>", step.Event.BackAfter.Trim(), obj.ToString()));
        }
    }
    
    //归档
    if (execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit 
        || execute.ExecuteType == RoadFlow.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed)
    {
        var currentsteps = wfInstalled.Steps.Where(p=>p.ID==execute.StepID);
        if (currentsteps.Count() > 0 && currentsteps.First().Archives == 1)
        {
            string flowName, stepName;
            string formHtml = Request.Form["form_body_div_textarea"];
            string commentHtml = Request.Form["form_commentlist_div_textarea"];
            stepName = bworkFlow.GetStepName(execute.StepID, execute.FlowID, out flowName, true);
            string archivesContents = new RoadFlow.Platform.WorkFlowForm().GetArchivesString(formHtml, commentHtml);
            RoadFlow.Data.Model.WorkFlowArchives wfr = new RoadFlow.Data.Model.WorkFlowArchives();
            wfr.Comments = commentHtml;
            wfr.Contents = archivesContents;
            wfr.FlowID = execute.FlowID;
            wfr.FlowName = flowName;
            wfr.GroupID = execute.GroupID;
            wfr.ID = Guid.NewGuid();
            wfr.InstanceID = execute.InstanceID;
            wfr.StepID = execute.StepID;
            wfr.StepName = stepName;
            wfr.Title = title.IsNullOrEmpty() ? flowName + "-" + stepName : title;
            wfr.TaskID = execute.TaskID;
            wfr.WriteTime = RoadFlow.Utility.DateTimeNew.Now;
            new RoadFlow.Platform.WorkFlowArchives().Add(wfr);
        }
    }

    Response.Write("<script type=\"text/javascript\">alert('" + msg + "');top.mainDialog.close();</script>");
    
    if (reslut.IsSuccess)
    {
        //判断是打开任务还是关闭窗口
        var nextTasks = reslut.NextTasks.Where(p => p.Status.In(0, 1) && p.ReceiveID == RoadFlow.Platform.Users.CurrentUserID);
        var nextTask = nextTasks.Count() > 0 ? nextTasks.First() : null;
        if (nextTask != null)
        {
            string url = string.Format("Default.aspx?flowid={0}&stepid={1}&taskid={2}&groupid={3}&instanceid={4}&appid={5}&tabid={6}",
                nextTask.FlowID, nextTask.StepID, nextTask.ID, nextTask.GroupID, nextTask.InstanceID,
                Request.QueryString["appid"], Request.QueryString["tabid"]
                );
            Response.Write("<script type=\"text/javascript\">window.parent.location = '"+url+"';</script>");
        }
        else
        {
            Response.Write("<script type=\"text/javascript\">top.mainTab.closeTab();</script>");
        }
    }    
%>
