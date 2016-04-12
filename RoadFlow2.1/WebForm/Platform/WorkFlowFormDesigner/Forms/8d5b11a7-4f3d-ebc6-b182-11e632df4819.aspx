<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_PurchaseList";
	string DBTablePK = "ID";
	string DBTableTitle = "Name";
if(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString["instanceid1"];}	RoadFlow.Platform.Dictionary BDictionary = new RoadFlow.Platform.Dictionary();
	RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
	RoadFlow.Platform.WorkFlowTask BWorkFlowTask = new RoadFlow.Platform.WorkFlowTask();
	string fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus);
	string TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);
%>
<link href="Scripts/Forms/flowform.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Forms/common.js" type="text/javascript" ></script>
<input type="hidden" id="Form_ValidateAlertType" name="Form_ValidateAlertType" value="1" />
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_PurchaseList.Name" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_PurchaseList" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Name" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_PurchaseList", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p><br/></p><table class="flowformtable" cellpadding="0" cellspacing="1" data-sort="sortDisabled"><tbody><tr class="firstRow"><td width="152" valign="top" style="word-break: break-all;">物资名称：<br/></td><td width="366" valign="top"><input type="text" id="TempTest_PurchaseList.Name" type1="flow_text" name="TempTest_PurchaseList.Name" value="" valuetype="0" isflow="1" class="mytext" title=""/></td><td width="98" valign="top" style="word-break: break-all;">型号：<br/></td><td width="420" valign="top"><input type="text" id="TempTest_PurchaseList.Model" type1="flow_text" name="TempTest_PurchaseList.Model" value="" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td width="152" valign="top" style="word-break: break-all;">单位：<br/></td><td width="366" valign="top"><input type="text" id="TempTest_PurchaseList.Unit" type1="flow_text" name="TempTest_PurchaseList.Unit" value="" valuetype="0" isflow="1" class="mytext" title=""/></td><td width="98" valign="top" style="word-break: break-all;">数量：<br/></td><td width="420" valign="top"><input type="text" id="TempTest_PurchaseList.Quantity" type1="flow_text" name="TempTest_PurchaseList.Quantity" value="" valuetype="4" isflow="1" class="mytext" title=""/></td></tr><tr><td width="152" valign="top" style="word-break: break-all;">要求日期：</td><td width="366" valign="top"><input type="text" type1="flow_datetime" id="TempTest_PurchaseList.Date" name="TempTest_PurchaseList.Date" value="" defaultvalue="" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td width="98" valign="top" style="word-break: break-all;">类型：<br/></td><td width="420" valign="top"><select class="myselect" id="TempTest_PurchaseList.Type" name="TempTest_PurchaseList.Type" isflow="1" type1="flow_select"><option value="办公用品">办公用品</option><option value="办公家具">办公家具</option><option value="电器">电器</option></select></td></tr><tr><td width="152" valign="top" style="word-break: break-all;">备注说明：</td><td valign="top" colspan="3" width="107" style="word-break: break-all;"><textarea isflow="1" type1="flow_textarea" id="TempTest_PurchaseList.Note" name="TempTest_PurchaseList.Note" class="mytext" style="width:90%;height:50px"></textarea></td></tr></tbody></table><p><br/></p>