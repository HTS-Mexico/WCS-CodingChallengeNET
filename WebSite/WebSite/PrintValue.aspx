<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintValue.aspx.cs" Inherits="WebSite.PrintValue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Value</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 id="myHeading"></h1>
        </div>
    </form>

    <script>
        // Access the h1 element and set its text content
        document.getElementById("myHeading").innerText = '<%= Request.QueryString["value"] %>';
    </script>
</body>
</html>