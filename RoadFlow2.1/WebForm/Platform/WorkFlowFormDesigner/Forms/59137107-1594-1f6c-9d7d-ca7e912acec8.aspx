<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest";
	string DBTablePK = "ID";
	string DBTableTitle = "Title";
if(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString["instanceid1"];}	
RoadFlow.Platform.Dictionary BDictionary = new RoadFlow.Platform.Dictionary();
	RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
	RoadFlow.Platform.WorkFlowTask BWorkFlowTask = new RoadFlow.Platform.WorkFlowTask();
	string fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus);
	string TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);
%>
<link href="Scripts/Forms/flowform.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Forms/common.js" type="text/javascript" ></script>
<input type="hidden" id="Form_ValidateAlertType" name="Form_ValidateAlertType" value="1" />
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest.Title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p> </p><p style="text-align: center;"><strong><span style="font-size: 24px;">请  假  单</span></strong></p><p> </p><table class="flowformtable" cellspacing="1" cellpadding="0" data-sort="sortDisabled"><tbody><tr class="firstRow"><td valign="top" style="ms-word-break: break-all;">标题：</td><td valign="top" colspan="3"><input type="text" id="TempTest.Title" type1="flow_text" name="TempTest.Title" value="<%=RoadFlow.Platform.Users.CurrentUserName%>的请假申请" style="width:80%" isflow="1" class="mytext" title=""/><script type="text/javascript">function onchange_31414dcceba792ae7555cf547c5f2469(srcObj){alert("adfasdfddd2222222sadf");}</script></td></tr><tr><td width="110" valign="top" style="ms-word-break: break-all;">请假人：<br/></td><td width="428" valign="top" style="ms-word-break: break-all;"><input type="text" type1="flow_org" id="TempTest.UserID" name="TempTest.UserID" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="0" workgroup="0" unit="0" rootid=""/></td><td width="89" valign="top" style="ms-word-break: break-all;">所在部门：<br/></td><td width="449" valign="top"><input type="text" type1="flow_org" id="TempTest.DeptID" name="TempTest.DeptID" value="<%=RoadFlow.Platform.Users.CurrentDeptID%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="0" workgroup="0" unit="0" rootid=""/></td></tr><tr><td width="110" valign="top" style="ms-word-break: break-all;">请假日期：<br/></td><td width="428" valign="top" style="ms-word-break: break-all;"><input type="text" type1="flow_datetime" id="TempTest.Date1" name="TempTest.Date1" value="" defaultvalue="" istime="0" daybefor="0" dayafter="1" currentmonth="0" isflow="1" class="mycalendar" title=""/>  至  <input type="text" type1="flow_datetime" id="TempTest.Date2" name="TempTest.Date2" value="" defaultvalue="" istime="0" daybefor="0" dayafter="1" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td width="89" valign="top" style="ms-word-break: break-all;">请假天数：<br/></td><td width="449" valign="top" style="ms-word-break: break-all;"><input type="text" id="TempTest.Days" type1="flow_text" name="TempTest.Days" value="" valuetype="3" isflow="1" class="mytext" title=""/>  天</td></tr><tr><td valign="top" style="ms-word-break: break-all;">请假类型：</td><td valign="top" style="word-break: break-all;" colspan="3"><select class="mycombox" id="TempTest.Type" name="TempTest.Type" width1="150" datasource="0" listmode="0" isflow="1" type1="flow_combox"><%=BDictionary.GetOptionsByID("e7f836be-f091-460f-86e1-f0b6cdceba39".ToGuid(), RoadFlow.Platform.Dictionary.OptionValueField.ID, "")%></select></td></tr><tr><td width="110" valign="top" style="ms-word-break: break-all;">请假事由：<br/></td><td width="23" valign="top" style="word-break: break-all;" colspan="3"><textarea isflow="1" type1="flow_textarea" id="TempTest.Reason" name="TempTest.Reason" class="mytext" style="width:700px;height:80px" maxlength="500"></textarea></td></tr><tr><td width="110" valign="top" style="ms-word-break: break-all;">相关附件：<br/></td><td valign="top" style="-ms-word-break: break-all;" colspan="3"><input type="text" type1="flow_files" id="TempTest.test" name="TempTest.test" value="" style="width:200px" filetype="" isflow="1" class="myfile" title=""/> </td></tr></tbody></table><p> </p>