<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_News";
	string DBTablePK = "ID";
	string DBTableTitle = "Title";
if(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString["instanceid1"];}	RoadFlow.Platform.Dictionary BDictionary = new RoadFlow.Platform.Dictionary();
	RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
	RoadFlow.Platform.WorkFlowTask BWorkFlowTask = new RoadFlow.Platform.WorkFlowTask();
	string fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus);
	string TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);
%>
<link href="Scripts/Forms/flowform.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Forms/common.js" type="text/javascript" ></script>
<script src="../../Scripts/Ueditor/ueditor.config.js" type="text/javascript" ></script>
<script src="../../Scripts/Ueditor/ueditor.all.min.js" type="text/javascript" ></script>
<script src="../../Scripts/Ueditor/lang/zh-cn/zh-cn.js" type="text/javascript" ></script>
<input type="hidden" id="Form_HasUEditor" name="Form_HasUEditor" value="1" />
<input type="hidden" id="Form_ValidateAlertType" name="Form_ValidateAlertType" value="1" />
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_News.Title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_News" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_News", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p> <span style="text-align: center;"> </span></p><p style="text-align: center;"><span style="font-size: 24px;"></span></p><p style="text-align: center;"><span style="font-size: 24px;"><strong>信 息 发 布</strong></span></p><p style="text-align: center;"> </p><table class="flowformtable" cellspacing="1" cellpadding="0"><tbody><tr class="firstRow"><td width="81" valign="top" style="-ms-word-break: break-all;">标题：<br/></td><td width="1036" valign="top"><input type="text" id="TempTest_News.Title" type1="flow_text" name="TempTest_News.Title" value="" style="width:80%" maxlength="500" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td width="81" valign="top" style="-ms-word-break: break-all;">副标题：<br/></td><td width="1036" valign="top" style="-ms-word-break: break-all;"><textarea isflow="1" type1="flow_textarea" id="TempTest_News.Title1" name="TempTest_News.Title1" class="mytext" style="width: 80%; height: 70px;" maxlength="1000"></textarea> <span style="font-size: 24px;"></span></td></tr><tr><td width="81" valign="top" style="-ms-word-break: break-all;">分类：<br/></td><td width="1036" valign="top" style="word-break: break-all;"><select class="myselect" id="TempTest_News.Type" name="TempTest_News.Type" isflow="1" type1="flow_select"><option value=""></option><%=BDictionary.GetOptionsByID("a8b9101f-4f8b-4830-9de1-c86ad89405c3".ToGuid(), RoadFlow.Platform.Dictionary.OptionValueField.ID, "")%></select></td></tr><tr><td width="81" valign="top" style="-ms-word-break: break-all;">内容：<br/></td><td width="1036" valign="top" style="-ms-word-break: break-all;"><textarea isflow="1" model="html" type1="flow_textarea" id="TempTest_News.Contents" name="TempTest_News.Contents" class="mytextarea" style="width:99%;height:350px"><%=BWorkFlow.GetFromFieldData(initData, "TempTest_News","Contents")%></textarea></td></tr></tbody></table><p> </p>