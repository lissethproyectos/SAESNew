<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SAES_v1.Default" %>

<!DOCTYPE html>
<html>
<head>

    <meta charset="UTF-8">
    <title>SAES</title>
     <meta name="viewport" content="width=device-width, initial-scale=1">

    <%--    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/meyer-reset/2.0/reset.min.css">--%>
    <link href="Template/Sitemaster/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">

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

        .login-form2 {
            width: 50%;
            padding: 2em;
            position: relative;
            background: rgba(0, 0, 0, 0.15);
            margin-left: auto;
            margin-right: auto;
        }
    </style>
</head>

<body translate="no">
    <div class="container body">
        <div class="main_container">
            <div class="row">
                <div class="col text-center">
                    <h5 class="text-white">SAES| Sistema de Administraci&oacute;n Escolar</h5>
                </div>
            </div>
            <br />
            <div class="login-form">
                <form id="formulario" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon1"><i class="fas fa-user"></i></span>
                        <asp:TextBox ID="username" CssClass="form-control" runat="server"></asp:TextBox>

                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon2"><i class="fas fa-lock"></i></span>
                        <asp:TextBox ID="password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:UpdatePanel ID="updPnlIngresar" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="entrar" CssClass="lf--submit" Text="Entrar" runat="server" OnClick="entrar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="center">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="reset" runat="server" Text="&iquest;Olvidaste tu contrase&ntilde;a?" CssClass="lf--forgot" OnClick="reset_Click"></asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="mensaje" runat="server" Style="color: red; font-size: 16px;"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </form>
            </div>
            <br />
            <div style="text-align: center">
<%--                <img src="Images/Sitemaster/Logo_1.png" style="width: 35%;" />--%>
            </div>
        </div>
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
