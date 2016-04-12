<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebForm.Platform.WorkFlowDelegation.Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
        <tr>
            <th style="width:100px;">委托人：</th>
            <td><input type="text" name="UserID" <%=isOneSelf?"disabled='disabled'":"" %> dept="0" station="0" unit="0" user="1" group="0" id="UserID" class="mymember" validate="empty" value="<%=workFlowDelegation.UserID.IsEmptyGuid() ? "" : "u_"+workFlowDelegation.UserID %>" /></td>
        </tr>
        <tr>
            <th>被委托人：</th>
            <td><input type="text" name="ToUserID" dept="0" station="0" unit="0" user="1" group="0" id="ToUserID" class="mymember" validate="empty" value="<%=workFlowDelegation.ToUserID.IsEmptyGuid() ? "" : "u_"+workFlowDelegation.ToUserID%>" /></td>
        </tr>
        <tr>
            <th>开始时间：</th>
            <td><input type="text" name="StartTime" id="StartTime" class="mycalendar" istime="1" validate="empty" value="<%=workFlowDelegation.StartTime!=DateTime.MinValue ? workFlowDelegation.StartTime.ToDateTimeString() : ""%>" /></td>
        </tr>
        <tr>
            <th>结束时间：</th>
            <td><input type="text" name="EndTime" id="EndTime" class="mycalendar" istime="1" validate="empty" value="<%=workFlowDelegation.EndTime!=DateTime.MinValue ? workFlowDelegation.EndTime.ToDateTimeString() : ""%>" /></td>
        </tr>
        <tr>
            <th>委托流程：</th>
            <td><select class="myselect" style="width:120px;" id="FlowID" name="FlowID"><option value="">==全部==</option><%=FlowOptions %></select></td>
        </tr>
        <tr>
            <th>备注说明：</th>
            <td><textarea class="mytextarea" id="Note" name="Note" cols="1" rows="1" style="width:95%;height:50px;"><%=workFlowDelegation.Note %></textarea></td>
        </tr>
    
    </table>
    <div class="buttondiv">
        <input type="submit" value="确定保存" class="mybutton" onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
        <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="new RoadUI.Window().close();" />
    </div>
</form>
</body>
</html>
