<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisualizadorCrystal.aspx.cs" Inherits="SAES_v1.Reports.VisualizadorCrystal" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script>
        function loader_false() {
            $('#precarga1').html('');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <cr:crystalreportviewer ID="CR_Reportes" runat="server" autodatabind="true"></cr:crystalreportviewer>
        </div>
    </form>
</body>
</html>
