using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;

namespace RoadFlow.Platform
{
    /// <summary>
    /// 流程表单相关类
    /// </summary>
    public class WorkFlowForm
    {

        private RoadFlow.Data.Interface.IWorkFlowForm dataWorkFlowForm;
		public WorkFlowForm()
		{
            this.dataWorkFlowForm = Data.Factory.Factory.GetWorkFlowForm();
		}
		/// <summary>
		/// 新增
		/// </summary>
		public int Add(RoadFlow.Data.Model.WorkFlowForm model)
		{
			return dataWorkFlowForm.Add(model);
		}
		/// <summary>
		/// 更新
		/// </summary>
		public int Update(RoadFlow.Data.Model.WorkFlowForm model)
		{
			return dataWorkFlowForm.Update(model);
		}
		/// <summary>
		/// 查询所有记录
		/// </summary>
		public List<RoadFlow.Data.Model.WorkFlowForm> GetAll()
		{
			return dataWorkFlowForm.GetAll();
		}
		/// <summary>
		/// 查询单条记录
		/// </summary>
		public RoadFlow.Data.Model.WorkFlowForm Get(Guid id)
		{
			return dataWorkFlowForm.Get(id);
		}
		/// <summary>
		/// 删除
		/// </summary>
		public int Delete(Guid id)
		{
			return dataWorkFlowForm.Delete(id);
		}
		/// <summary>
		/// 查询记录条数
		/// </summary>
		public long GetCount()
		{
			return dataWorkFlowForm.GetCount();
		}

        /// <summary>
        /// 得到验证提示方式Radio字符串
        /// </summary>
        /// <returns></returns>
        public string GetValidatePropTypeRadios(string name, string value, string att = "")
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("弹出(alert)","0"){ Selected="0"==value},
                new ListItem("图标和提示信息","1"){ Selected="1"==value},
                new ListItem("图标","2"){ Selected="2"==value}
            };
            return RoadFlow.Utility.Tools.GetRadioString(items, name, att);
        }

        /// <summary>
        /// 得到流程文本框输入类型Radio字符串
        /// </summary>
        /// <returns></returns>
        public string GetInputTypeRadios(string name, string value, string att="")
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("明文","text"){ Selected="0"==value},
                new ListItem("密码","password"){ Selected="1"==value}
            };
            return RoadFlow.Utility.Tools.GetRadioString(items, name, att);
        }

        /// <summary>
        /// 得到待选事件选择项
        /// </summary>
        /// <returns></returns>
        public string GetEventOptions(string name, string value, string att = "")
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("onclick","onclick"){ Selected="onclick"==value},
                new ListItem("onchange","onchange"){ Selected="onchange"==value},
                new ListItem("ondblclick","ondblclick"){ Selected="ondblclick"==value},
                new ListItem("onfocus","onfocus"){ Selected="onfocus"==value},
                new ListItem("onblur","onblur"){ Selected="onblur"==value},
                new ListItem("onkeydown","onkeydown"){ Selected="onkeydown"==value},
                new ListItem("onkeypress","onkeypress"){ Selected="onkeypress"==value},
                new ListItem("onkeyup","onkeyup"){ Selected="onkeyup"==value},
                new ListItem("onmousedown","onmousedown"){ Selected="onmousedown"==value},
                new ListItem("onmouseup","onmouseup"){ Selected="onmouseup"==value},
                new ListItem("onmouseover","onmouseover"){ Selected="onmouseover"==value},
                new ListItem("onmouseout","onmouseout"){ Selected="onmouseout"==value},
            };
            return RoadFlow.Utility.Tools.GetOptionsString(items);
        }

        /// <summary>
        /// 得到流程值类型选择项字符串
        /// </summary>
        /// <returns></returns>
        public string GetValueTypeOptions(string value)
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("字符串","0"){ Selected="0"==value},
                new ListItem("整数","1"){ Selected="1"==value},
                new ListItem("实数","2"){ Selected="2"==value},
                new ListItem("正整数","3"){ Selected="3"==value},
                new ListItem("正实数","4"){ Selected="4"==value},
                new ListItem("负整数","5"){ Selected="5"==value},
                new ListItem("负实数","6"){ Selected="6"==value},
                new ListItem("手机号码","7"){ Selected="7"==value}
            };
            return RoadFlow.Utility.Tools.GetOptionsString(items);
        }

        /// <summary>
        /// 得到默认值下拉选项字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetDefaultValueSelect(string value)
        {
            StringBuilder options = new StringBuilder(1000);
            options.Append("<option value=\"\"></option>");
            options.Append("<optgroup label=\"组织机构相关选项\"></optgroup>");
            options.AppendFormat("<option value=\"u_@RoadFlow.Platform.Users.CurrentUserID.ToString()\" {0}>当前步骤用户ID</option>", "10" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Platform.Users.CurrentUserName)\" {0}>当前步骤用户姓名</option>", "11" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Platform.Users.CurrentDeptID)\" {0}>当前步骤用户部门ID</option>", "12" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Platform.Users.CurrentDeptName)\" {0}>当前步骤用户部门名称</option>", "13" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"u_@(new RoadFlow.Platform.WorkFlowTask().GetFirstSnderID(FlowID.ToGuid(), GroupID.ToGuid(), true))\" {0}>流程发起者ID</option>", "14" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(new RoadFlow.Platform.Users().GetName(new RoadFlow.Platform.WorkFlowTask().GetFirstSnderID(FlowID.ToGuid(), GroupID.ToGuid(), true)))\" {0}>流程发起者姓名</option>", "15" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(new RoadFlow.Platform.WorkFlowTask().GetFirstSnderDeptID(FlowID.ToGuid(), GroupID.ToGuid()))\" {0}>流程发起者部门ID</option>", "16" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(new RoadFlow.Platform.Users().GetName(new RoadFlow.Platform.WorkFlowTask().GetFirstSnderDeptID(FlowID.ToGuid(), GroupID.ToGuid())))\" {0}>流程发起者部门名称</option>", "17" == value ? "selected=\"selected\"" : "");
            options.Append("<optgroup label=\"日期时间相关选项\"></optgroup>");
            options.AppendFormat("<option value=\"@(RoadFlow.Utility.DateTimeNew.ShortDate)\" {0}>短日期格式(2014-4-15)</option>", "20" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Utility.DateTimeNew.LongDate)\" {0}>长日期格式(2014年4月15日)</option>", "21" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Utility.DateTimeNew.ShortTime)\" {0}>短时间格式(23:59)</option>", "22" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Utility.DateTimeNew.LongTime)\" {0}>长时间格式(23时59分)</option>", "23" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Utility.DateTimeNew.ShortDateTime)\" {0}>短日期时间格式(2014-4-15 22:31)</option>", "24" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@(RoadFlow.Utility.DateTimeNew.LongDateTime)\" {0}>长日期时间格式(2014年4月15日 22时31分)</option>", "25" == value ? "selected=\"selected\"" : "");
            options.Append("<optgroup label=\"流程实例相关选项\"></optgroup>");
            options.AppendFormat("<option value=\"@Html.Raw(BWorkFlow.GetFlowName(FlowID.ToGuid()))\" {0}>当前流程名称</option>", "30" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"@Html.Raw(BWorkFlow.GetStepName(StepID.ToGuid(), FlowID.ToGuid(), true))\" {0}>当前步骤名称</option>", "31" == value ? "selected=\"selected\"" : "");
            return options.ToString();
        }

        /// <summary>
        /// 得到默认值下拉选项字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetDefaultValueSelectByAspx(string value)
        {
            StringBuilder options = new StringBuilder(1000);
            options.Append("<option value=\"\"></option>");
            options.Append("<optgroup label=\"组织机构相关选项\"></optgroup>");
            options.AppendFormat("<option value=\"u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>\" {0}>当前步骤用户ID</option>", "10" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Platform.Users.CurrentUserName%>\" {0}>当前步骤用户姓名</option>", "11" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Platform.Users.CurrentDeptID%>\" {0}>当前步骤用户部门ID</option>", "12" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Platform.Users.CurrentDeptName%>\" {0}>当前步骤用户部门名称</option>", "13" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"u_<%=new RoadFlow.Platform.WorkFlowTask().GetFirstSnderID(FlowID.ToGuid(), GroupID.ToGuid(), true)%>\" {0}>流程发起者ID</option>", "14" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=new RoadFlow.Platform.Users().GetName(new RoadFlow.Platform.WorkFlowTask().GetFirstSnderID(FlowID.ToGuid(), GroupID.ToGuid(), true))%>\" {0}>流程发起者姓名</option>", "15" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=new RoadFlow.Platform.WorkFlowTask().GetFirstSnderDeptID(FlowID.ToGuid(), GroupID.ToGuid())%>\" {0}>流程发起者部门ID</option>", "16" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=new RoadFlow.Platform.Users().GetName(new RoadFlow.Platform.WorkFlowTask().GetFirstSnderDeptID(FlowID.ToGuid(), GroupID.ToGuid()))%>\" {0}>流程发起者部门名称</option>", "17" == value ? "selected=\"selected\"" : "");
            options.Append("<optgroup label=\"日期时间相关选项\"></optgroup>");
            options.AppendFormat("<option value=\"<%=RoadFlow.Utility.DateTimeNew.ShortDate%>\" {0}>短日期格式(2014-4-15)</option>", "20" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Utility.DateTimeNew.LongDate%>\" {0}>长日期格式(2014年4月15日)</option>", "21" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Utility.DateTimeNew.ShortTime%>\" {0}>短时间格式(23:59)</option>", "22" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Utility.DateTimeNew.LongTime%>\" {0}>长时间格式(23时59分)</option>", "23" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Utility.DateTimeNew.ShortDateTime%>\" {0}>短日期时间格式(2014-4-15 22:31)</option>", "24" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=RoadFlow.Utility.DateTimeNew.LongDateTime%>\" {0}>长日期时间格式(2014年4月15日 22时31分)</option>", "25" == value ? "selected=\"selected\"" : "");
            options.Append("<optgroup label=\"流程实例相关选项\"></optgroup>");
            options.AppendFormat("<option value=\"<%=BWorkFlow.GetFlowName(FlowID.ToGuid())%>\" {0}>当前流程名称</option>", "30" == value ? "selected=\"selected\"" : "");
            options.AppendFormat("<option value=\"<%=BWorkFlow.GetStepName(StepID.ToGuid(), FlowID.ToGuid(), true)%>\" {0}>当前步骤名称</option>", "31" == value ? "selected=\"selected\"" : "");
            return options.ToString();
        }

        /// <summary>
        /// 得到流程文本域模式Radio字符串
        /// </summary>
        /// <returns></returns>
        public string GetTextareaModelRadios(string name, string value, string att = "")
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("普通","default"){ Selected="default"==value},
                new ListItem("HTML","html"){ Selected="html"==value}
            };
            return RoadFlow.Utility.Tools.GetRadioString(items, name, att);
        }

        /// <summary>
        /// 得到数据源Radio字符串
        /// </summary>
        /// <returns></returns>
        public string GetDataSourceRadios(string name, string value, string att = "")
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("数据字典","0"){ Selected="0"==value},
                new ListItem("自定义","1"){ Selected="1"==value},
                new ListItem("SQL语句","2"){ Selected="2"==value}
            };

            return RoadFlow.Utility.Tools.GetRadioString(items, name, att);
        }

        /// <summary>
        /// 得到组织机构选择范围Radio字符串
        /// </summary>
        /// <returns></returns>
        public string GetOrgRangeRadios(string name, string value, string att = "")
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("发起者部门","0"){ Selected="0"==value},
                new ListItem("处理者部门","1"){ Selected="1"==value},
            };
            return RoadFlow.Utility.Tools.GetRadioString(items, name, att);
        }

        /// <summary>
        /// 得到组织机构选择类型Radio字符串
        /// </summary>
        /// <returns></returns>
        public string GetOrgSelectTypeCheckboxs(string name, string value, string att = "")
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("部门","0"){ Selected="0"==value},
                new ListItem("岗位","1"){ Selected="1"==value},
                new ListItem("人员","2"){ Selected="2"==value},
                new ListItem("工作组","3"){ Selected="3"==value},
                new ListItem("单位","4"){ Selected="4"==value}
            };
            return RoadFlow.Utility.Tools.GetCheckBoxString(items, name, value.Split(','), att);
        }

        /// <summary>
        /// 得到从表编辑模式选择
        /// </summary>
        /// <returns></returns>
        public string GetEditmodeOptions(string value)
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("无",""){ Selected=""==value},
                new ListItem("文本框","text"){ Selected="text"==value},
                new ListItem("文本域","textarea"){ Selected="textarea"==value},
                new ListItem("下拉列表","select"){ Selected="select"==value},
                new ListItem("复选框","checkbox"){ Selected="checkbox"==value},
                new ListItem("单选框","radio"){ Selected="radio"==value},
                new ListItem("日期时间","datetime"){ Selected="datetime"==value},
                new ListItem("组织机构选择","org"){ Selected="org"==value},
                new ListItem("数据字典选择","dict"){ Selected="dict"==value},
                new ListItem("附件","files"){ Selected="files"==value}
            };
            return RoadFlow.Utility.Tools.GetOptionsString(items);
        }

        /// <summary>
        /// 得到从表显示模式选择
        /// </summary>
        /// <returns></returns>
        public string GetDisplayModeOptions(string value)
        {
            ListItem[] items = new ListItem[]{ 
                new ListItem("常规","normal"){ Selected="normal"==value},
                new ListItem("数据字典ID显示为标题","dict_id_title"){ Selected="dict_id_title"==value},
                new ListItem("数据字典ID显示为代码","dict_id_code"){ Selected="dict_id_code"==value},
                new ListItem("数据字典ID显示为值","dict_id_value"){ Selected="dict_id_value"==value},
                new ListItem("数据字典ID显示为备注","dict_id_note"){ Selected="dict_id_note"==value},
                new ListItem("数据字典ID显示为其它","dict_id_other"){ Selected="dict_id_other"==value},
                new ListItem("数据字典唯一代码显示为标题","dict_code_title"){ Selected="dict_code_title"==value},
                new ListItem("数据字典唯一代码显示为ID","dict_code_id"){ Selected="dict_code_id"==value},
                new ListItem("数据字典唯一代码显示为值","dict_code_value"){ Selected="dict_code_value"==value},
                new ListItem("数据字典唯一代码显示为备注","dict_code_note"){ Selected="dict_code_note"==value},
                new ListItem("数据字典唯一代码显示为其它","dict_code_other"){ Selected="dict_code_other"==value},
                new ListItem("组织机构ID显示为名称","organize_id_name"){ Selected="organize_id_name"==value},
                new ListItem("附件显示为连接","files_link"){ Selected="files_link"==value},
                new ListItem("附件显示为图片","files_img"){ Selected="files_img"==value},
                new ListItem("日期时间显示为指定格式","datetime_format"){ Selected="datetime_format"==value},
                new ListItem("数字显示为指定格式","number_format"){ Selected="number_format"==value},
                new ListItem("字符串时间显示为指定格式","string_format"){ Selected="string_format"==value},
                new ListItem("关联显示为其它表字段值","table_fieldvalue"){ Selected="table_fieldvalue"==value}
            };
            return RoadFlow.Utility.Tools.GetOptionsString(items);
        }

        /// <summary>
        /// 根据显示方式得到显示的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="displayModel"></param>
        /// <returns></returns>
        public string GetDisplayString(string value, string displayModel, string format = "", string dbconnID = "", string sql = "")
        {
            string value1 = string.Empty;
            switch ((displayModel ?? "").ToLower())
            {
                case "normal":
                default:
                    value1 = value;
                    break;
                case "dict_id_title":
                    var dict = new Dictionary().Get(value.ToGuid());
                    value1 = dict == null ? "" : dict.Title;
                    break;
                case "dict_id_code":
                    var dict1 = new Dictionary().Get(value.ToGuid());
                    value1 = dict1 == null ? "" : dict1.Code;
                    break;
                case "dict_id_value":
                    var dict2 = new Dictionary().Get(value.ToGuid());
                    value1 = dict2 == null ? "" : dict2.Value;
                    break;
                case "dict_id_note":
                    var dict3 = new Dictionary().Get(value.ToGuid());
                    value1 = dict3 == null ? "" : dict3.Note;
                    break;
                case "dict_id_other":
                    var dict4 = new Dictionary().Get(value.ToGuid());
                    value1 = dict4 == null ? "" : dict4.Other;
                    break;
                case "dict_code_title":
                    var dict5 = new Dictionary().GetByCode(value);
                    value1 = dict5 == null ? "" : dict5.Title;
                    break;
                case "dict_code_id":
                    var dict6 = new Dictionary().GetByCode(value);
                    value1 = dict6 == null ? "" : dict6.ID.ToString();
                    break;
                case "dict_code_value":
                    var dict7 = new Dictionary().GetByCode(value);
                    value1 = dict7 == null ? "" : dict7.Value;
                    break;
                case "dict_code_note":
                    var dict8 = new Dictionary().GetByCode(value);
                    value1 = dict8 == null ? "" : dict8.Note;
                    break;
                case "dict_code_other":
                    var dict9 = new Dictionary().GetByCode(value);
                    value1 = dict9 == null ? "" : dict9.Other;
                    break;
                case "organize_id_name":
                    value1 = new Organize().GetNames(value);
                    break;
                case "files_link":
                    string[] files = value.Split('|');
                    StringBuilder links = new StringBuilder();
                    //links.Append("<div>");
                    foreach (string file in files)
                    {
                        links.AppendFormat("<a target=\"_blank\" class=\"blue\" href=\"{0}\">{1}</a><br/>", file, System.IO.Path.GetFileName(file));
                    }
                    //links.Append("</div>");
                    value1 = links.ToString();
                    break;
                case "files_img":
                    string[] files1 = value.Split('|');
                    StringBuilder links1 = new StringBuilder();
                    //links.Append("<div>");
                    foreach (string file in files1)
                    {
                        links1.AppendFormat("<img src=\"{0}\" />", file);
                    }
                    //links.Append("</div>");
                    value1 = links1.ToString();
                    break;
                case "datetime_format":
                    value1 = value.ToDateTime().ToString(format ?? Utility.Config.DateFormat);
                    break;
                case "number_format":
                    value1 = value.ToDecimal().ToString(format);
                    break;
                case "table_fieldvalue":
                    DBConnection dbconn = new DBConnection();
                    DataTable dt = dbconn.GetDataTable(dbconn.Get(dbconnID.ToGuid()), sql + "'" + value + "'");
                    value1 = dt.Rows.Count > 0 && dt.Columns.Count > 0 ? dt.Rows[0][0].ToString() : "";
                    break;
            }
            return value1;
        }

        /// <summary>
        /// 得到状态显示
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetStatusTitle(int status)
        {
            string title = string.Empty;
            switch (status)
            { 
                case 0:
                    title = "已保存";break;
                case 1:
                    title = "已发布";break;
                case 2:
                    title = "已作废";break;
            }
            return title;
        }

        /// <summary>
        /// 得到编译页面的头部
        /// </summary>
        /// <param name="serverScript">服务端脚本</param>
        /// <returns></returns>
        public string GetHeadHtml(string serverScript)
        {
            return "";
        }

        /// <summary>
        /// 根据一个地址得到下拉列表项
        /// </summary>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetOptionsFromUrl(string url, string value)
        {
            string uri = url + (url.IndexOf('?') >= 0 ? "&" : "?") + "values=" + value;
            if (!uri.Contains("http", StringComparison.CurrentCultureIgnoreCase)
                && !uri.Contains("https", StringComparison.CurrentCultureIgnoreCase))
            {
                var add = System.Web.HttpContext.Current.Request.Url;
                uri = add.ToString().Substring(0, add.ToString().IndexOf("//") + 2) + add.Authority + uri;
            }
            try
            {
                string options = Utility.HttpHelper.SendGet(uri);
                return options;
            }
            catch(Exception err)
            {
                return err.Message;
            }
        }

        /// <summary>
        /// 根据sql得到下拉列表项
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetOptionsFromSql(string connID, string sql, string value)
        { 
            Guid cid;
            if(!connID.IsGuid(out cid))
            {
                return "";
            }
            DBConnection dbConn = new DBConnection();
            var dbconn = dbConn.Get(cid);
            if (dbconn == null)
            {
                return "";
            }
            DataTable dt = dbConn.GetDataTable(dbconn, sql.ReplaceSelectSql());
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            List<ListItem> items = new List<ListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Count == 0)
                {
                    continue;
                }
                string value1 = dr[0].ToString();
                string title = value1;
                if (dt.Columns.Count > 1)
                {
                    title = dr[1].ToString();
                }

                items.Add(new ListItem(title, value1) { Selected = value == value1 });
            }
            return RoadFlow.Utility.Tools.GetOptionsString(items.ToArray());
        }

        /// <summary>
        /// 根据sql得到单选按钮组
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetRadioFromSql(string connID, string sql, string name, string value, string attr = "")
        {
            Guid cid;
            if (!connID.IsGuid(out cid))
            {
                return "";
            }
            DBConnection dbConn = new DBConnection();
            var dbconn = dbConn.Get(cid);
            if (dbconn == null)
            {
                return "";
            }
            DataTable dt = dbConn.GetDataTable(dbconn, sql.ReplaceSelectSql());
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            List<ListItem> items = new List<ListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Count == 0)
                {
                    continue;
                }
                string value1 = dr[0].ToString();
                string title = value1;
                if (dt.Columns.Count > 1)
                {
                    title = dr[1].ToString();
                }

                items.Add(new ListItem(title, value1) { Selected = value == value1 });
            }
            return RoadFlow.Utility.Tools.GetRadioString(items.ToArray(), name, attr);
        }

        /// <summary>
        /// 根据sql得到复选框
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetCheckboxFromSql(string connID, string sql, string name, string value, string attr="")
        {
            Guid cid;
            if (!connID.IsGuid(out cid))
            {
                return "";
            }
            DBConnection dbConn = new DBConnection();
            var dbconn = dbConn.Get(cid);
            if (dbconn == null)
            {
                return "";
            }
            DataTable dt = dbConn.GetDataTable(dbconn, sql.ReplaceSelectSql());
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            List<ListItem> items = new List<ListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Count == 0)
                {
                    continue;
                }
                string value1 = dr[0].ToString();
                string title = value1;
                if (dt.Columns.Count > 1)
                {
                    title = dr[1].ToString();
                }

                items.Add(new ListItem(title, value1));
            }
            return RoadFlow.Utility.Tools.GetCheckBoxString(items.ToArray(), name, (value ?? "").Split(','), attr);
        }

        /// <summary>
        /// 得到Grid的html
        /// </summary>
        /// <param name="dataFormat"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataSource1"></param>
        /// <returns></returns>
        public string GetFormGridHtml(string connID, string dataFormat, string dataSource, string dataSource1)
        {
            if (!dataFormat.IsInt() || !dataSource.IsInt() || dataSource1.IsNullOrEmpty())
            {
                return "";
            }
 
            switch (dataSource)
            { 
                case "0":
                    DBConnection dbConn = new DBConnection();
                    var dbconn = dbConn.Get(connID.ToGuid());
                    if (dbconn == null)
                    {
                        return "";
                    }
                    DataTable dt = dbConn.GetDataTable(dbconn, dataSource1.ReplaceSelectSql());
                    switch (dataFormat)
                    { 
                        case "0":
                            return dataTableToHtml(dt);
                        case "1":
                            return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
                        case "2":
                            return dt.Rows.Count > 0 ? jsonToHtml(dt.Rows[0][0].ToString()) : "";
                        default:
                            return "";
                    }
                    
                case "1":
                    string str = string.Empty;
                    try
                    {
                        str = RoadFlow.Utility.HttpHelper.SendGet(dataSource1);
                        switch (dataFormat)
                        {
                            case "0":
                            case "1":
                                return str;
                            case "2":
                                return jsonToHtml(str);
                            default:
                                return "";
                        }
                    }
                    catch
                    {
                        return "";
                    }
                case "2":
                    RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams = new RoadFlow.Data.Model.WorkFlowCustomEventParams();
                    eventParams.FlowID = (System.Web.HttpContext.Current.Request.QueryString["FlowID"] ?? "").ToGuid();
                    eventParams.GroupID = (System.Web.HttpContext.Current.Request.QueryString["GroupID"] ?? "").ToGuid();
                    eventParams.StepID = (System.Web.HttpContext.Current.Request.QueryString["StepID"] ?? "").ToGuid();
                    eventParams.TaskID = (System.Web.HttpContext.Current.Request.QueryString["TaskID"] ?? "").ToGuid();
                    eventParams.InstanceID = System.Web.HttpContext.Current.Request.QueryString["InstanceID"] ?? "";
                    object obj = null;
                    try
                    {
                        obj = new WorkFlowTask().ExecuteFlowCustomEvent(dataSource1, eventParams);
                        switch (dataFormat)
                        {
                            case "0":
                                return dataTableToHtml((DataTable)obj);
                            case "1":
                                return obj.ToString();
                            case "2":
                                return jsonToHtml(obj.ToString());
                            default:
                                return "";
                        }
                    }
                    catch
                    {
                        return "";
                    }
            }

            return "";
        }

        /// <summary>
        /// 将一个DataTable转换为HTML表格
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string dataTableToHtml(DataTable dt)
        {
            StringBuilder table = new StringBuilder(2000);
            table.Append("<table border=\"1\" style=\"border-collapse:collapse;width:100%;\">");
            table.Append("<thead><tr style=\"height:25px;\">");
            foreach (DataColumn column in dt.Columns)
            {
                table.AppendFormat("<th>{0}</th>", column.ColumnName);
            }
            table.Append("</tr></thead>");
            table.Append("<tbody>");
            foreach (DataRow dr in dt.Rows)
            {
                table.Append("<tr style=\"height:22px;\">");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    table.AppendFormat("<td>{0}</td>", dr[i].ToString().HtmlEncode());
                }
                table.Append("</tr>");
            }
            table.Append("</tbody>");
            table.Append("</table>");
            return table.ToString();
        }

        /// <summary>
        /// 将json转换为HTML表格
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string jsonToHtml(string jsonStr)
        {
            LitJson.JsonData json = LitJson.JsonMapper.ToObject(jsonStr);
            if (!json.IsArray)
            {
                return "";
            }
            StringBuilder table = new StringBuilder(2000);
            table.Append("<table border=\"1\" style=\"border-collapse:collapse;width:100%;\">");
            table.Append("<tbody><tr style=\"height:25px;\">");
            foreach (LitJson.JsonData tr in json)
            {
                table.Append("<tr style=\"height:22px;\">");
                foreach(LitJson.JsonData td in tr)
                {
                    table.AppendFormat("<td>{0}</td>", td.ToString());
                }
                table.Append("</tr>");
            }
            table.Append("</tbody>");
            table.Append("</table>");
            return table.ToString();
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
        /// 查询一个分类所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.WorkFlowForm> GetAllByType(Guid id)
        {
            return dataWorkFlowForm.GetAllByType(GetAllChildsIDString(id));
        }

        /// <summary>
        /// 得到类型选择项
        /// </summary>
        /// <returns></returns>
        public string GetTypeOptions(string value = "")
        {
            return new Dictionary().GetOptionsByCode("FormTypes", Dictionary.OptionValueField.ID, value);
        }


        /// <summary>
        /// 根据sql得到Combox列表项
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetComboxTableHtmlFromSql(string connID, string sql, string value)
        {
            Guid cid;
            if (!connID.IsGuid(out cid))
            {
                return "";
            }
            DBConnection dbConn = new DBConnection();
            var dbconn = dbConn.Get(cid);
            if (dbconn == null)
            {
                return "";
            }
            DataTable dt = dbConn.GetDataTable(dbconn, sql.ReplaceSelectSql());
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder html = new StringBuilder(2000);
            html.Append("<table><thead><tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns.Count > 1 && i == 0) continue;
                html.Append("<th>");
                html.Append(dt.Columns[i].ColumnName);
                html.Append("</th>");
            }
            html.Append("</thead>");
            html.Append("<tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns.Count > 1 && j == 0) continue;
                    html.AppendFormat("<td value=\"{0}\"{1}>", dt.Rows[i][0], dt.Rows[i][0].ToString().Equals(value, StringComparison.CurrentCultureIgnoreCase) ? " selected=\"selected\"" : "");
                    html.Append(dt.Rows[i][j]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tr></tbody></table>");

            return html.ToString();
        }

        /// <summary>
        /// 得到归档HTML
        /// </summary>
        /// <param name="formHtml"></param>
        /// <param name="commentHtml"></param>
        /// <returns></returns>
        public string GetArchivesString(string formHtml, string commentHtml)
        {
            string html = "<link href=\"/Platform/WorkFlowRun/Scripts/Forms/flowform_print.css\" rel=\"stylesheet\" />"
            + "        <style type=\"text/css\" media=\"print\">"
            + "            .Noprint { display: none; }"
            + "        </style>"          
            + "        <link href=\"/Platform/WorkFlowRun/Scripts/Forms/flowform.css\" rel=\"stylesheet\" type=\"text/css\" />"
            + "        <script src=\"/Platform/WorkFlowRun/Scripts/Forms/common.js\" type=\"text/javascript\" ></script>"  
            + "<div style=\"width:98%; margin:-10px auto 0 auto;\">";

            html += formHtml;
            html += "<script type=\"text/javascript\">fieldStatus ={};</script>";
            html += "</div>";
            return html;
        }

    }
}
