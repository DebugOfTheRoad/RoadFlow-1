<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowSend.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.FlowSend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%
        WebForm.Common.Tools.CheckLogin(false);

        string flowid = Request.QueryString["flowid"];
        string stepid = Request.QueryString["stepid"];
        string groupid = Request.QueryString["groupid"];
        string taskid = Request.QueryString["taskid"];
        string instanceid = Request.QueryString["instanceid"];
        if (instanceid.IsNullOrEmpty())
        {
            instanceid = Request.QueryString["instanceid1"];
        }
        
        RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();
        RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
        RoadFlow.Data.Model.WorkFlowInstalled wfInstalled = bworkFlow.GetWorkFlowRunModel(flowid);
        if (wfInstalled == null)
        {
            Response.Write("未找到流程运行实体");
            Response.End();
        } 
    
        var steps = wfInstalled.Steps.Where(p => p.ID == stepid.ToGuid());
        if(steps.Count()==0)
        {
            Response.Write("未找到当前步骤");
            Response.End();
        }
        var currentStep = steps.First();
        var nextSteps = bworkFlow.GetNextSteps(wfInstalled.ID, currentStep.ID).OrderBy(p => p.Position_x).ThenBy(p => p.Position_y).ToList();
        int i = 0;
     %>
        <table cellpadding="0" cellspacing="1" border="0" width="95%" align="center" style="margin-top:6px;">
    <%if (!currentStep.Note.IsNullOrEmpty()){ %>
        <tr>
            <td style="padding:2px 0 0 0; color:#cc0000;"><%=currentStep.Note %></td>
        </tr>
    <%} %>
    <%
        //判断流转条件
        if (currentStep.Behavior.FlowType == 0 && nextSteps.Count() > 0)
        {
            List<Guid> removeIDList = new List<Guid>();
            RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams = new RoadFlow.Data.Model.WorkFlowCustomEventParams();
            eventParams.FlowID = flowid.ToGuid();
            eventParams.GroupID = groupid.ToGuid();
            eventParams.StepID = stepid.ToGuid();
            eventParams.TaskID = taskid.ToGuid();
            eventParams.InstanceID = instanceid;

            System.Text.StringBuilder nosubmitMsg = new System.Text.StringBuilder();
            foreach (var step in nextSteps)
            {
                var lines = wfInstalled.Lines.Where(p => p.ToID == step.ID && p.FromID == steps.First().ID);
                if (lines.Count() > 0)
                {
                    var line = lines.First();
                    if (!line.SqlWhere.IsNullOrEmpty())
                    {
                        if (wfInstalled.DataBases.Count() == 0)
                        {
                            removeIDList.Add(step.ID);
                            //nosubmitMsg.Append("流程未设置数据连接");
                            //nosubmitMsg.Append("\\n");
                        }
                        else
                        {
                            if (!btask.TestLineSql(wfInstalled.DataBases.First().LinkID, wfInstalled.DataBases.First().Table,
                                 wfInstalled.DataBases.First().PrimaryKey, instanceid, line.SqlWhere))
                            {
                                removeIDList.Add(step.ID);
                                //nosubmitMsg.Append(string.Concat("提交条件未满足"));
                                //nosubmitMsg.Append("\\n");
                            }
                        }
                    }
                    if (!line.CustomMethod.IsNullOrEmpty())
                    {
                        object obj = btask.ExecuteFlowCustomEvent(line.CustomMethod.Trim(), eventParams);
                        var objType = obj.GetType();
                        var boolType = typeof(Boolean);
                        if (objType != boolType && "1" != obj.ToString())
                        {
                            removeIDList.Add(step.ID);
                            nosubmitMsg.Append(obj.ToString());
                            nosubmitMsg.Append("\\n");
                        }
                        else if (objType == boolType && !(bool)obj)
                        {
                            removeIDList.Add(step.ID);
                            nosubmitMsg.Append(obj.ToString());
                            nosubmitMsg.Append("\\n");
                        }
                    }
                    #region 组织机构关系判断
                    Guid SenderID = RoadFlow.Platform.Users.CurrentUserID;
                    if ("1" == line.Organize_SenderChargeLeader && !busers.IsChargeLeader(SenderID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if (!line.Organize_SenderIn.IsNullOrEmpty() && !busers.IsContains(SenderID, line.Organize_SenderIn))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if ("1" == line.Organize_SenderLeader && !busers.IsLeader(SenderID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if (!line.Organize_SenderNotIn.IsNullOrEmpty() && busers.IsContains(SenderID, line.Organize_SenderNotIn))
                    {
                        removeIDList.Add(step.ID);
                    }
                    Guid sponserID = Guid.Empty;//发起者ID
                    if (currentStep.ID == wfInstalled.FirstStepID)//如果是第一步则发起者就是发送者
                    {
                        sponserID = SenderID;
                    }
                    else
                    {
                        sponserID = btask.GetFirstSnderID(eventParams.FlowID, eventParams.GroupID);
                    }
                    if ("1" == line.Organize_SponsorChargeLeader && !busers.IsChargeLeader(sponserID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if (!line.Organize_SponsorIn.IsNullOrEmpty() && !busers.IsContains(sponserID, line.Organize_SponsorIn))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if ("1" == line.Organize_SponsorLeader && !busers.IsLeader(sponserID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if (!line.Organize_SponsorNotIn.IsNullOrEmpty() && busers.IsContains(sponserID, line.Organize_SponsorNotIn))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if ("1" == line.Organize_NotSenderChargeLeader && busers.IsChargeLeader(SenderID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if ("1" == line.Organize_NotSenderLeader && busers.IsLeader(SenderID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if ("1" == line.Organize_NotSponsorChargeLeader && busers.IsChargeLeader(sponserID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    if ("1" == line.Organize_NotSponsorLeader && busers.IsLeader(sponserID))
                    {
                        removeIDList.Add(step.ID);
                    }
                    #endregion

                }
            }
            foreach (Guid rid in removeIDList)
            {
                nextSteps.RemoveAll(p => p.ID == rid);
            }

            if (nextSteps.Count == 0)
            {
                string alertMsg = nosubmitMsg.ToString();
                alertMsg = alertMsg.IsNullOrEmpty() ? "后续步骤条件均不符合,任务不能提交!" : alertMsg;
                Response.Write("<script>alert('" + alertMsg + "');top.mainDialog.close();</script>");
                Response.End();
            }
        }
    foreach (var step in nextSteps)
    {
        string checked1 = i++ == 0 ? "checked=\"checked\"" : "";//默认选中第一个步骤
        string disabled = step.Behavior.RunSelect == 0 ? "disabled=\"disabled\"" : "";//是否允许运行时选择人员
        string selectRang = step.Behavior.SelectRange.IsNullOrEmpty() ? "" : "rootid=\"" + step.Behavior.SelectRange.Trim() + "\"";//选择范围
        string selectType = string.Empty;//选择类型
        var defaultMember = string.Empty;//默认处理人员

        //如果是调试模式并且当前登录人员包含在调试人员中 则默认为发起者
        if ((wfInstalled.Debug == 1 || wfInstalled.Debug == 2) && wfInstalled.DebugUsers.Exists(p => p.ID == RoadFlow.Platform.Users.CurrentUserID))
        {
            defaultMember = RoadFlow.Platform.Users.PREFIX + RoadFlow.Platform.Users.CurrentUserID.ToString();
        }
        else
        {
            switch (step.Behavior.HandlerType)
            {
                case 0:
                    selectType = "unit='1' dept='1' station='1' workgroup='1' user='1'";
                    break;
                case 1:
                    selectType = "unit='0' dept='1' station='0' workgroup='0' user='0'";
                    break;
                case 2:
                    selectType = "unit='0' dept='0' station='1' workgroup='0' user='0'";
                    break;
                case 3:
                    selectType = "unit='0' dept='0' station='0' workgroup='1' user='0'";
                    break;
                case 4:
                    selectType = "unit='0' dept='0' station='0' workgroup='0' user='1'";
                    break;
                case 5://发起者
                    Guid userid = btask.GetFirstSnderID(wfInstalled.ID, groupid.ToGuid());
                    if (userid != Guid.Empty)
                    {
                        defaultMember = RoadFlow.Platform.Users.PREFIX + userid.ToString();
                    }
                    if (defaultMember.IsNullOrEmpty() && currentStep.ID == wfInstalled.FirstStepID)
                    {
                        defaultMember = RoadFlow.Platform.Users.PREFIX + RoadFlow.Platform.Users.CurrentUserID.ToString();
                    }
                    break;
                case 6://前一步骤处理者
                    //defaultMember = btask.GetStepSnderIDString(wfInstalled.ID, currentStep.ID, groupid.ToGuid());
                    //if (defaultMember.IsNullOrEmpty() && currentStep.ID == wfInstalled.FirstStepID)
                    //{
                        defaultMember = RoadFlow.Platform.Users.PREFIX + RoadFlow.Platform.Users.CurrentUserID.ToString();
                    //}
                    break;
                case 7://某一步骤处理者
                    defaultMember = btask.GetStepSnderIDString(wfInstalled.ID, step.Behavior.HandlerStepID, groupid.ToGuid());
                    if (defaultMember.IsNullOrEmpty() && step.Behavior.HandlerStepID == wfInstalled.FirstStepID)
                    {
                        defaultMember = RoadFlow.Platform.Users.PREFIX + RoadFlow.Platform.Users.CurrentUserID.ToString();
                    }
                    break;
                case 8://字段值
                    string linkString = step.Behavior.ValueField;
                    if (!linkString.IsNullOrEmpty() && !instanceid.IsNullOrEmpty() && wfInstalled.DataBases.Count() > 0)
                    {
                        defaultMember = new RoadFlow.Platform.DBConnection().GetFieldValue(linkString, wfInstalled.DataBases.First().PrimaryKey, instanceid);
                    }
                    break;
                case 9://发起者主管
                    Guid firstSenderID = btask.GetFirstSnderID(wfInstalled.ID, groupid.ToGuid());
                    if (firstSenderID.IsEmptyGuid() && currentStep.ID == wfInstalled.FirstStepID)//如果是第一步则发起者为当前人员
                    {
                        firstSenderID = RoadFlow.Platform.Users.CurrentUserID;
                    }
                    if (!firstSenderID.IsEmptyGuid())
                    {
                        defaultMember = busers.GetLeader(firstSenderID);
                    }
                    break;
                case 10://发起者分管领导
                    Guid firstSenderID1 = btask.GetFirstSnderID(wfInstalled.ID, groupid.ToGuid());
                    if (firstSenderID1.IsEmptyGuid() && currentStep.ID == wfInstalled.FirstStepID)//如果是第一步则发起者为当前人员
                    {
                        firstSenderID1 = RoadFlow.Platform.Users.CurrentUserID;
                    }
                    if (!firstSenderID1.IsEmptyGuid())
                    {
                        defaultMember = busers.GetChargeLeader(firstSenderID1);
                    }
                    break;
                case 11://前一步处理者领导
                    defaultMember = busers.GetLeader(RoadFlow.Platform.Users.CurrentUserID);
                    break;
                case 12://前一步处理者分管领导
                    defaultMember = busers.GetChargeLeader(RoadFlow.Platform.Users.CurrentUserID);
                    break;
                    
            }
        }
        
        if (defaultMember.IsNullOrEmpty())
        {
            defaultMember = step.Behavior.DefaultHandler;
        }
     %>
        <tr>
            <td style="padding:9px 0 2px 0;">
            <input type="hidden" name="nextstepid" value="@step.ID" />
            <%if (currentStep.Behavior.FlowType == 1){ %>
            <input type="radio" <%=checked1 %> value="<%=step.ID %>" name="step" id="step_<%=step.ID %>" style="vertical-align:middle;" />
            <%}else if (currentStep.Behavior.FlowType == 2){ %>
            <input type="checkbox" <%=checked1 %> value="<%=step.ID %>" name="step" id="Checkbox1" style="vertical-align:middle;" />
            <%}else{%> 
            <input type="checkbox" checked="checked" disabled="disabled" value="<%=step.ID %>" name="step" id="Checkbox2" style="vertical-align:middle;" />
            <%}%> 
            <label for="step_<%=step.ID %>" style="vertical-align:middle;"><%=step.Name %></label>
            </td>
        </tr>
        <tr>
            <td style="padding:2px 0 4px 0;">
            <input type="text" class="mymember" <%=disabled %> <%=selectRang %> <%=selectType %> value="<%=defaultMember %>" id="user_<%=step.ID %>" name="user_<%=step.ID %>" style="width:280px;" title="选择处理人员" isChangeType="<%=selectRang.Length>0?"1":"0" %>" /> <!--span style="color:#999;">//选择处理人员</span-->
            </td>
        </tr>
        <tr><td style="height:6px; border-bottom:1px dashed #e8e8e8;"></td></tr>
    <%} %>
    </table>
    <div style="width:95%; margin:12px auto 0 auto; text-align:center;">
        <input type="submit" class="mybutton" onclick="return confirm1();" name="Save" value="&nbsp;确&nbsp;定&nbsp;" style="margin-right:5px;" />
        <input type="button" class="mybutton" value="&nbsp;取&nbsp;消&nbsp;" onclick="new RoadUI.Window().close();" />
    </div>
    <script type="text/javascript">
        var frame = null;
        var openerid = '<%=Request.QueryString["openerid"]%>';
        var nextStepsCount = <%=nextSteps.Count()%>;
        $(function ()
        {
            var iframes = top.frames;
            for (var i = 0; i < iframes.length; i++)
            {
                if (iframes[i].name == openerid + "_iframe")
                {
                    frame = iframes[i]; break;
                }
            }
            if (frame == null) return;
            if(nextStepsCount == 0)//如果后面没有步骤，则完成该流程实例
            {
                var options = {};
                options.type = "completed";
                options.steps = [];
                frame.formSubmit(options);
                new RoadUI.Window().close();
            }
            else if(nextStepsCount>2)
            {
                top.mainDialog.resize(480,(nextStepsCount-2)*45+280);
            }
        });
        function confirm1()
        {
            var opts = {};
            opts.type = "submit";
            opts.steps = [];
            var isSubmit = true;
            $(":checked[name='step']").each(function ()
            {
                var step = $(this).val();
                var member = $("#user_" + step).val() || "";
                if ($.trim(member).length == 0)
                {
                    alert($(this).next().text() + " 没有选择处理人员!");
                    isSubmit = false;
                    return false;
                }
                opts.steps.push({ id: step, member: member });
            });
            if(!isSubmit)
            {
                return false;
            }
            if(opts.steps.length==0)
            {
                alert("没有选择要处理的步骤!");
                return false;
            }
            if (isSubmit)
            {
                frame.formSubmit(opts);
                new RoadUI.Window().close();
            }
        }
    </script>
    </form>
</body>
</html>
