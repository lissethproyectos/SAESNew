<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SAES_v1.Default" %>

<!DOCTYPE html>
<html>
<head>

    <meta charset="UTF-8">
    <title></title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/meyer-reset/2.0/reset.min.css">
    <link href="Content/loaders.css" rel="stylesheet" />
    <link href="Content/Style.css" rel="stylesheet" />

    <!-- jQuery -->
    <script src="Template/Sitemaster/vendors/jquery/dist/jquery.min.js"></script>
    <script>
        window.console = window.console || function (t) { };
    </script>
    <script>
        if (document.location.search.match(/type=embed/gi)) {
            window.parent.postMessage("resize", "*");
        }



    </script>
    <script src="https://kit.fontawesome.com/aff8921cf8.js" crossorigin="anonymous"></script>
    <style>
        .center {
            text-align: center;
        }
    </style>
</head>

<body translate="no">
    <div style="display: none;"></div>
    <div style="margin-bottom: 5%; font-size: xxx-large; color: white;">
        <h2>SAES| Sistema de Administración Escolar</h2>
    </div>
    <form id="formulario" class="login-form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="flex-row">
            <label class="lf--label" for="username">
                <i class="fas fa-user"></i>
            </label>
            <asp:TextBox ID="username" CssClass="lf--input" runat="server"></asp:TextBox>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrIngresar" runat="server"
                    AssociatedUpdatePanelID="updPnlIngresar">
                    <ProgressTemplate>
                        <asp:Image ID="imgNuevo" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="30px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="30px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="flex-row">
            <label class="lf--label" for="password">
                <i class="fas fa-lock"></i>
            </label>
            <asp:TextBox ID="password" CssClass="lf--input" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <asp:UpdatePanel ID="updPnlIngresar" runat="server">
            <ContentTemplate>
                <asp:Button ID="entrar" CssClass="lf--submit" Text="Entrar" runat="server" OnClick="entrar_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="center">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="reset" runat="server" Text="¿Olvidaste tu contraseña?" CssClass="lf--forgot" OnClick="reset_Click"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="mensaje" runat="server" Style="color:red; font-size: 16px;"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <br />
    <div style="text-align: center">
        <img src="Images/Sitemaster/Logo_1.png" style="width: 35%;" />
    </div>
    <script>

        function slide() {
            $('#formulario').fadeOut('slow');
            $('#loader').fadeIn('slow');
            return false;
        }
    </script>
</body>
</html>
