<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="trefe.aspx.cs" Inherits="SAES_v1.trefe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="cuerpo">
        <asp:UpdatePanel ID="UpdDatos" runat="server">
            <ContentTemplate>

                <script type="text/javascript" src="/Servicios_Web_ULA/Historia_Academica/Scripts/jsUpdateProgress_Login.js"></script>

                <div class="menu">
                    <asp:Label ID="Lblfechanac" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="Lblfechavenc" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="LblNombre" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="Lblrefoxxo" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LblCorreo" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LblTelefono" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LblPeriodo" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LblDatos" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="Lblerror" runat="server" Text="Label" Visible="true"></asp:Label>
                    <br />

                </div>
                <br />

                FAVOR DE LLAMAR A SOPORTE<br />



                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />

                <br />
                <br />

                <script>

                    <asp:Label ID="Lblfecha" runat="server" Text=""></asp:Label>
                    function getData() {

                        var boton = document.getElementById('CmdLogin');

                        boton.click();

                    }

                </script>
            </ContentTemplate>

        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdDatos">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center; width: 224px; height: 110px; background-color: #FFFFFF; font-family: Arial, Helvetica, sans-serif; font-size: 10px; font-weight: bold;">
                    <br />
                    <br />
                    <img src="/Servicios_Web_ULA/Historia_Academica/Images/ajax-loader.gif" /><br />
                    <br />
                    <br />
                    <div id="textoajax"></div>

                    <br />
                    <br />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
