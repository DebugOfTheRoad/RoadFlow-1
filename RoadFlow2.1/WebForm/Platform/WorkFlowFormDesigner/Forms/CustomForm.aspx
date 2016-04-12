﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomForm.aspx.cs" Inherits="WebForm.Platform.WorkFlowFormDesigner.Forms.CustomForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%
    string id = Request.QueryString["instanceid"];
    string title = string.Empty;
    string contents = string.Empty;
    if(id.IsInt())
    {
        string sql = "SELECT * FROM TempTest_CustomForm WHERE ID=" + id;
        System.Data.DataTable dt = new RoadFlow.Data.MSSQL.DBHelper().GetDataTable(sql);
        if(dt.Rows.Count>0)
        {
            title = dt.Rows[0]["Title"].ToString();
            contents = dt.Rows[0]["Contents"].ToString();
        }
    }
    %>
    <br /><br /><br />
    <!--任务标题字段-->
    <input type="hidden" id="Form_TitleField" name="Form_TitleField" value="Title" />
    <!--保存数据的方法名称,在App_Code下类(CustomFormSave)方法(QianShi)-->
    <input type="hidden" id="Form_CustomSaveMethod" name="Form_CustomSaveMethod" value="WebForm.Common.CustomFormSave.QianShi" />
    <div style="text-align:center; font-size:24px; font-weight:bold;">请 示</div>
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
        <tr>
            <th style="width: 80px;">
                标题：
            </th>
            <td>
                <input type="text" name="Title" id="Title" class="mytext" value="<%=title %>" validate="empty" style="width: 80%" />
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">
                请示内容：
            </th>
            <td>
                <textarea class="mytextarea" style="width:80%; height:120px;" id="Contents" validate="empty" name="Contents"><%=contents %></textarea>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
