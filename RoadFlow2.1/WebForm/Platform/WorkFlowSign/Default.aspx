<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkFlowSign.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto; text-align:center; padding-top:100px;">
         <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <input id="Button1" type="submit" class="mybutton" value=" 上 传 " />
            <input type="submit" name="reset" class="mybutton" value="恢复默认" />
        </div>
        <div style="margin-top:80px;">您的签名：
            <%
              string signFile = string.Concat(Server.MapPath(WebForm.Common.Tools.BaseUrl + "/Files/UserSigns/"), RoadFlow.Platform.Users.CurrentUserID, ".gif");
              string signSrc = string.Concat(WebForm.Common.Tools.BaseUrl + "/Files/UserSigns/", RoadFlow.Platform.Users.CurrentUserID, ".gif");
              if (!System.IO.File.Exists(signFile))
              {
                  System.Drawing.Bitmap img = new RoadFlow.Platform.WorkFlow().CreateSignImage(RoadFlow.Platform.Users.CurrentUserName);
                  if (img != null)
                  {
                      img.Save(signFile, System.Drawing.Imaging.ImageFormat.Gif);
                  }
              }
            %>
            <img alt="" src="<%=signSrc %>" id="signimg" style="vertical-align:middle;" />
        </div>
    </div>
    </form>
</body>
</html>
