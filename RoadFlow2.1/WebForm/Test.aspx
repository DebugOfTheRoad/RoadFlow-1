<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebForm.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <table>
            <tbody>
            <tr>
                <td>
                    <span style="margin-left:20px;" class="mycombox" id="test111" width1="650" multiple="1">
                       <div class="querybar">
                           标题：<input type="text" id="title" name="title" class="mytext" />
                           <input type="button" value=" 查询 " class="mybutton"/>
                       </div>
                       <table dataurl="Test1.ashx" query="">
                           <thead>
                               <tr>
                                   <th>标题</th>
                                   <th>类型</th>
                                   <th>用户</th>
                                   <th>日期</th>
                               </tr>
                           </thead>
                           <tbody></tbody>
                       </table>
                    </span>
                </td>

                <td>
                    <input type="button" value="reset" onclick="new RoadUI.Combox().reSet('Select111', '<option>xxxxxx</option><option>yyyyyyy</option>')"  />
                    <select class="mycombox" id="Select111" multiple="multiple" onchange="alert(this.value)" name="test11" >
                        <optgroup>11111</optgroup>
                        <option>21342314</option>
                        <option>dasfsdfsf</option>
                        <option>213423w14</option>
                        <option>dasfsdefsf</option>
                        <optgroup label="77">22222</optgroup>
                        <option>213423e14</option>
                        <option>dasfsdgfsf</option>
                        <option>213423w14</option>
                        <option>dasfsdefsf</option>
                        <option>213423qwerqwerweqrwqerweqre14</option>
                        <option>dasfsdgfsf</option>
                    </select>
                </td>
                <td>
                    <div style="margin-left:20px;" class="mycombox" id="Div1" width1="650" >
                       <table>
                           <thead>
                               <tr>
                                   <th>标题</th>
                                   <th>类型</th>
                                   <th>用户</th>
                                   <th>日期</th>
                               </tr>
                           </thead>
                           <tbody>
                               <tr>
                                   <td value="qewrqwer">11111</td>
                                   <td>111111</td>
                                   <td>adsfasdf</td>
                                   <td>adsfasdf</td>
                               </tr>
                               <tr>
                                   <td value="qewrqwwer">22222</td>
                                   <td>adsfasdf</td>
                                   <td>adsfasdf</td>
                                   <td>adsfasdf</td>
                               </tr>
                               <tr>
                                   <td value="qewrqewer">44444</td>
                                   <td>adsfasdf</td>
                                   <td>adsfasdf</td>
                                   <td>adsfasdf</td>
                               </tr>
                           </tbody>
                       </table>
                    </div>
                </td>
            </tr>
            </tbody>
        </table>
        
        
        
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table style="width:100%">
            <tr>
                <td align="right">
                    <select class="mycombox" id="Select2" name="test" multiple="multiple" >
                        <optgroup>11111</optgroup>
                        <option>111111111</option>
                        <option>33333</option>
                        <option>555555</option>
                        <option>44</option>
                        <optgroup label="77">xxxx</optgroup>
                        <option>xx</option>
                        <option>tttt</option>
                        <option>yyyyyyyyyyy</option>
                        <option>uuuuuuuu</option>
                        <option>2134u23qwerqwerweqrwqerweqre14</option>
                        <option>vvvvvvvvvvvvvv</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td><input type="text" class="myfile" /></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div style="margin-left:20px;">
        <select class="mycombox" id="test" name="test" multiple="multiple" >
            <optgroup>11111</optgroup>
            <option>111111111</option>
            <option>33333</option>
            <option>555555</option>
            <option>44</option>
            <optgroup label="77">xxxx</optgroup>
            <option>xx</option>
            <option>tttt</option>
            <option>yyyyyyyyyyy</option>
            <option>uuuuuuuu</option>
            <option>2134u23qwerqwerweqrwqerweqre14</option>
            <option>vvvvvvvvvvvvvv</option>
        </select>
        </div>
       
    </div>
    </form>
</body>
</html>
