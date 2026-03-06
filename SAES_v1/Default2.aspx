<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="SAES_v1.Default2" %>

<head>

    <meta charset="UTF-8">
    <title></title>

    <link href="Content/loaders.css" rel="stylesheet" />
    <link href="Content/Style.css" rel="stylesheet" />
    <link href="Template/Sitemaster/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">

    <!-- jQuery -->
    <script src="Template/Sitemaster/vendors/jquery/dist/jquery.min.js"></script>
    <script>
        window.console = window.console || function (t) { };
    </script>
    <!--Sweet Alert-->
    <script src="Script/SweetAlert/sweetalert.js"></script>
    <link href="Content/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script src="Content/Custom/js/AlertasSweet.js"></script>
    <script>
        if (document.location.search.match(/type=embed/gi)) {
            window.parent.postMessage("resize", "*");
        }
    </script>
    <script src="https://kit.fontawesome.com/aff8921cf8.js" crossorigin="anonymous"></script>
    <style>
        .btn-success {
            background: #26b99a;
            border: 1px solid #169f85;
        }

        .btn-secondary {
            color: #fff;
            background-color: #6c757d;
            border-color: #6c757d;
        }

        .login-form {
            color: #fff;
        }

        .login-form2 {
            color: #8f8f8f;
            padding: 2em;
            position: relative;
            background: rgba(0, 0, 0, 0.15);
        }

            .login-form2:before {
                content: "";
                position: absolute;
                top: -2px;
                left: 0;
                height: 2px;
                width: 100%;
                background: linear-gradient(to right, #35c3c1, #00d6b7);
            }

        .form-control {
            width: 100%;
            display: block;
            height: calc(1.5em + 0.75rem + 2px);
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .boton-success {
            background: #26b99a;
            border: 1px solid #169f85;
        }

        .boton-round {
            border-radius: 30px;
        }

        .boton-grey {
            background: #b1b1b1;
            border: 1px solid #169f85;
        }
    </style>
</head>
<script type="text/javascript">
    $(window).on("load", function () {
        $(".loader1").fadeOut("slow");
    });
    function loader_push() {
        $(".loader1").fadeIn("slow");
    }
    function loader_stop() {
        $(".loader1").fadeOut("slow");
    }
</script>

<body translate="no">
    <div style="color: white;">
        <h2>SAES| Sistema de Administración Escolar</h2>
    </div>
    <form id="formulario" class="login-form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:MultiView ID="multiViewUsu" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-12">
                                    Pregunta de seguridad
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLPregunta" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    Respuesta
                                    <asp:TextBox ID="txtRespuesta" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col text-center">
                                    <asp:UpdatePanel ID="updPnlBotonesC" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="linkBttnCancelar" CssClass="btn btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>

                                            <asp:LinkButton ID="linkBttnVerificaRespuesta" CssClass="btn btn-success" runat="server" OnClick="linkBttnVerificaRespuesta_Click">Verificar</asp:LinkButton>
                                            <asp:LinkButton ID="linkActDatos" runat="server" OnClick="linkActDatos_Click" CssClass="form-control boton-round boton-success">Actualiza Datos</asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <asp:UpdateProgress ID="updPgrBotones" runat="server"
                                        AssociatedUpdatePanelID="updPnlBotonesC">
                                        <ProgressTemplate>
                                            <asp:Image ID="imgGuardar" runat="server"
                                                AlternateText="Espere un momento, por favor.." Height="30px"
                                                ImageUrl="~/Images/Sitemaster/loader.gif"
                                                ToolTip="Espere un momento, por favor.." Width="30px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-3 lf--input2">Nuevo Password</div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtNuevoPass" runat="server" CssClass="lf--input2" for="password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3  lf--input2">Confirmar Password</div>
                                <div class="col-md-9  lf--input2">
                                    <asp:TextBox ID="txConfPass" runat="server" for="password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:LinkButton ID="linkBttnRegresar" CssClass="btn btn-round btn-secondary" runat="server" OnClick="linkBttnRegresar_Click">Cancelar</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnGuardar" CssClass="btn btn-round btn-success" runat="server" OnClick="linkBttnGuardar_Click">Guardar</asp:LinkButton>
                                </div>
                            </div>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>






    </form>
</body>
