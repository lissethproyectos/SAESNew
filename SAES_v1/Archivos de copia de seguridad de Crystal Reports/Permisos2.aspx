<%@ Page Title="" Language="C#" MasterPageFile="~/Repositorio/Site1.Master" AutoEventWireup="true" CodeBehind="Permisos2.aspx.cs" Inherits="SAES_v1.Repositorio.Permisos2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Template/Sitemaster/vendors/jquery/dist/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.js" integrity="sha512-n/4gHW3atM3QqRcbCn6ewmpxcLAHGaDjpEBu4xZd47N0W2oQ+6q7oc3PXstrJYXcbNU1OHdQ1T7pAP+gi5Yu8g==" crossorigin="anonymous"></script>
    <script type="text/javascript">

        /*boton 1 sube*/
        function slide_up() {
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#lstccampus").empty();

            return false;
        }
        /*boton 1 baja*/
        function slide_down() {
            $("#panel").slideDown("slow");
            $("#flecha_down").hide();
            $("#flecha_up").show();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /*boton 2 sube*/
        function slide_up_1() {
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();

            return false;
        }
        /*boton 2 baja*/
        function slide_down_1() {
            $("#panel_1").slideDown("slow");
            $("#flecha_down_1").hide();
            $("#flecha_up_1").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /*boton 3 sube*/
        function slide_up_2() {
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            //location.reload();
            return false;
        }
        /*boton 3 baja*/
        function slide_down_2() {
            $("#panel_2").slideDown("slow");
            $("#flecha_down_2").hide();
            $("#flecha_up_2").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /*boton 4 sube*/
        function slide_up_4() {
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            $("#lbl_permisos_2").hide();
            $("#lbl_permisos_1").show();
            //location.reload();
            return false;
        }
        /*Boton 4 baja*/
        function slide_down_4() {
            $("#privilegios_table").slideDown("slow");
            $("#save_button").slideDown("slow");
            $("#desbloqueado").show();
            $("#lbl_permisos_2").show();
            $("#lbl_permisos_1").hide();
            $("#bloqueado").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            return false;
        }
        /*boton 5 sube*/
        function slide_up_e() {
            $("#Panel_estatus").slideUp("slow");
            $("#flecha_down_3").show();
            $("#flecha_up_3").hide();
            $("#ListDoc").empty();

            return false;
        }

        /* etiqueta 1 sube*/
        function slide_up_lbl() {
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#lbl_add_camp").show();
            $("#lbl_add_camp_1").hide();
            return false;
        }
        /* etiqueta 2 baja*/
        function slide_down_lbl_n() {
            $("#panel_1").slideDown("slow");
            $("#flecha_down_1").hide();
            $("#flecha_up_1").show();
            $("#lbl_add_nivel").hide();
            $("#lbl_add_nivel_1").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /* etiqueta 2 sube*/
        function slide_up_lbl_n() {
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#lbl_add_nivel").show();
            $("#lbl_add_nivel_1").hide();
            return false;
        }
        /* etiqueta 3 baja*/
        function slide_down_lbl_d() {
            $("#panel_2").slideDown("slow");
            $("#flecha_down_2").hide();
            $("#flecha_up_2").show();
            $("#lbl_add_doc").hide();
            $("#lbl_add_doc_1").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /* etiqueta 3 sube*/
        function slide_up_lbl_d() {
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#lbl_add_doc").show();
            $("#lbl_add_doc_1").hide();
            return false;
        }

        /* etiqueta 4 sube*/
        function slide_up_lbl_p() {
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            $("#lbl_permisos_2").hide();
            $("#lbl_permisos_1").show();
            //location.reload();
            return false;
        }
        /* etiqueta 4 baja*/
        function slide_down_lbl_p() {
            $("#privilegios_table").slideDown("slow");
            $("#save_button").slideDown("slow");
            $("#desbloqueado").show();
            $("#lbl_permisos_2").show();
            $("#lbl_permisos_1").hide();
            $("#bloqueado").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            return false;
        }

        function slide_down_fast() {
            $("#panel").slideDown("fast");
            $("#flecha_down").hide();
            $("#flecha_up").show();
            return false;
        }
        function slide_down_fast_n() {
            $("#panel_1").slideDown("fast");
            $("#flecha_down_1").hide();
            $("#flecha_up_1").show();
            return false;
        }
        function slide_down_fast_d() {
            $("#panel_2").slideDown("fast");
            $("#flecha_down_2").hide();
            $("#flecha_up_2").show();
            return false;
        }
        function hideRow(row, gv) {
            Row_num = row;
            rows = document.getElementById(gv).rows;
            rows[Row_num].style.display = 'none';
        }
        function ShowRow(row, gv) {
            Row_num = row;
            rows = document.getElementById(gv).rows;
            rows[Row_num].style.display = 'block'
        }
        function guardar_permisos() {
            swal("Permisos modificados exitosamente", "Los permisos del perifl han sido modificados exitosamente.", "success")
                .then(willDelete => {
                    if (willDelete) {
                        window.location = "Permisos.aspx";
                        //swal("Deleted!", "Your imaginary file has been deleted!", "success");
                    }
                });
        }
    </script>

    <style>
        .title {
            font-weight: bold;
        }

        #lbl_permisos_2 {
            display: none;
        }

        #panel {
            padding: 50px;
            display: none;
        }

        #flecha_up {
            display: none;
        }

        .auto-style1 {
            width: 15px;
            height: 15px;
        }

        #panel_1 {
            padding: 50px;
            display: none;
        }

        #flecha_up_1 {
            display: none;
        }

        #panel_2 {
            padding: 50px;
            display: none;
        }

        #flecha_up_2 {
            display: none;
        }

        #lbl_add_camp_1 {
            display: none;
        }

        #lbl_add_nivel_1 {
            display: none;
        }

        #lbl_add_doc_1 {
            display: none;
        }

        #lbl_add_estatus_1 {
            display: none;
        }

        .auto-style2 {
            height: 28px;
        }

        .ul_submenu {
            list-style-type: disc;
            margin-block-start: 1em;
            margin-block-end: 1em;
            margin-inline-start: 0px;
            margin-inline-end: 0px;
            padding-inline-start: 40px;
            margin-top: 0;
            margin-bottom: 0rem;
        }

        .ul_permiso {
            list-style-type: disc;
            margin-block-start: 1em;
            margin-block-end: 1em;
            margin-inline-start: 0px;
            margin-inline-end: 0px;
            padding-inline-start: 80px;
            margin-top: 0;
            margin-bottom: 0rem;
        }

        .table-mod_2 {
            color: #fff;
            width: 10px;
            border-right-style: hidden;
        }

        .ex3 {
            width: 100%;
            height: 300px;
            overflow: auto;
        }
        /* The switch - the box around the slider */
        .switch {
            position: relative;
            display: inline-block;
            width: 35px;
            height: 18px;
        }

            /* Hide default HTML checkbox */
            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        /* The slider */
        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 15px;
                width: 15px;
                left: 1px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #169f85;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #169f85;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(16px);
            -ms-transform: translateX(16px);
            transform: translateX(16px);
        }
        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .ColumnaOculta {
            display: none;
        }

        #privilegios_table {
            display: none;
        }

        #save_button {
            display: none;
        }

        #desbloqueado {
            display: none;
        }

        .column_image {
            width: 15px;
        }


        .card-header .accicon {
            float: right;
            font-size: 20px;
            width: 1.2em;
        }

        .card-header {
            cursor: pointer;
            border-bottom: none;
        }

        .card {
            border: 1px solid #ddd;
        }

        .card-body {
            border-top: 1px solid #ddd;
        }

        .card-header:not(.collapsed) .rotate-icon {
            transform: rotate(180deg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-cog"></i>
            Configuración de Permisos

        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <div class="form-row">
            <div class="col-sm-6">
                Rol
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddlRol" runat="server" AutoPostBack="True"
                              OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" CssClass="form-control">
                          </asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col-sm-12">
                <div class="accordion" id="accordionExample">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false">
                            <h4><span class="title"><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Agregar Campus</span>
                                <span class="accicon"><i class="fas fa-angle-down rotate-icon"></i></span></h4>
                        </div>
                        <div id="collapseOne" class="collapse" data-parent="#accordionExample">
                            <div class="card-body">
                                <asp:Panel ID="UpdatePanel" runat="server">
                                            <div class="form-row">
                                                <div class="col-sm-5">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblCampus" runat="server" Text="Lista de Campus"></asp:Label>
                                                            <asp:ListBox ID="lstCampus" runat="server" CssClass="form-control" OnSelectedIndexChanged="lstCampus_SelectedIndexChanged" AutoPostBack="True" Rows="2"></asp:ListBox>
                                                            <asp:CheckBox ID="CheckBox_campus" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label2" runat="server" Text="Seleccionar todos"></asp:Label></td>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="form-row">
                                                <%-- <div class="col-sm-1 text-center">
                                                    <br />
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Repositorio/Doble.png" Width="40px" Height="40px" />
                                                    <asp:LinkButton ID="linkBttnAgregarCampus" CssClass="btn btn-success" runat="server"><i class="fa fa-chevron-circle-right" aria-hidden="true"></i></asp:LinkButton>

                                                </div>--%>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblUsuariosCampus" runat="server" Text="Usuarios en el Campus"></asp:Label>
                                                    <asp:ListBox ID="lstUsuariosCampus" runat="server" Height="150px" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                                <div class="col-sm-1 text-center">
                                                    <br />
                                                    <asp:LinkButton ID="linkBttnAgregarUsuCampus" CssClass="btn btn-success" runat="server" OnClick="linkBttnAgregarUsuCampus_Click"><i class="fa fa-chevron-right" aria-hidden="true"></i></asp:LinkButton>
                                                    <br />
                                                    <asp:LinkButton ID="linkBttnEliminarUsuCampus" CssClass="btn btn-success" runat="server" OnClick="linkBttnEliminarUsuCampus_Click"><i class="fa fa-chevron-left" aria-hidden="true"></i></asp:LinkButton>


                                                    <%--                                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                                        Width="25px" OnClick="cmdAgregar_Click" /><br />
                                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                                        Width="25px" OnClick="cmdBorrar_Click" />
                                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Repositorio/Doble.png" Width="40px" Height="40px" />--%>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="Label9" runat="server" Text="Lista de Usuarios"></asp:Label>
                                                    <asp:ListBox ID="lstUsuarios" runat="server" Height="150px" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>

                                                </div>
                                            </div>
                                    
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            <h4><span class="title"><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Agregar Niveles</span>
                                <span class="accicon"><i class="fas fa-angle-down rotate-icon"></i></span></h4>
                        </div>
                        <div id="collapseTwo" class="collapse" data-parent="#accordionExample">
                            <div class="card-body">
                                <asp:Panel ID="UpdatePanel1" runat="server">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>--%>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <asp:Label ID="Label4" runat="server" CssClass="font-weight-bold" Text="Lista de Niveles"></asp:Label>
                                            <asp:ListBox ID="ListNiveles" runat="server" CssClass="form-control" OnSelectedIndexChanged="ListNiveles_SelectedIndexChanged" AutoPostBack="True"
                                                SelectionMode="Single" Rows="2"></asp:ListBox>
                                            <asp:CheckBox ID="CheckBox_Niveles" runat="server" OnCheckedChanged="CheckBox_Niveles_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label3" runat="server" Text="Seleccionar todos"></asp:Label></td>

                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <asp:Label ID="Label5" runat="server" CssClass="font-weight-bold" Text="Usuarios en el Nivel"></asp:Label>
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID="ListUsuariosNivel" runat="server" Height="150px" CssClass="form-control"
                                                        SelectionMode="Multiple"></asp:ListBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-1 text-center">
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="linkBttnDelNivel" runat="server" CssClass="btn btn-success" OnClick="linkBttnDelNivel_Click">
                                                        <i class="fa fa-chevron-left" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                    <br />
                                                    <asp:LinkButton ID="linkBttnAddNivel" runat="server" CssClass="btn btn-success" OnClick="linkBttnAddNivel_Click">
                                                        <i class="fa fa-chevron-right" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-sm-5">
                                            <asp:Label ID="Label10" runat="server" CssClass="font-weight-bold" Text="Lista de Usuarios"></asp:Label>
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID="ListUsuarios" runat="server" Height="150px" CssClass="form-control"
                                                        SelectionMode="Multiple"></asp:ListBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <%--</ContentTemplate>
                                        <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                                        </triggers>
                                    </asp:UpdatePanel>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false">
                            <h4>
                                <span class="title"><i class="fa fa-folder-open" aria-hidden="true"></i>&nbsp;Agregar Tipo de Documentos</span>
                                <span class="accicon"><i class="fas fa-angle-down rotate-icon"></i></span></h4>
                        </div>
                        <div id="collapseThree" class="collapse" data-parent="#accordionExample">
                            <div class="card-body">
                                <asp:Panel ID="UpdatePanel2" runat="server">
                                  <%--  <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>--%>
                                            <div class="row">
                                                <div class="col">
                                                    <asp:Label ID="Label16" runat="server" Text="Asignar Documentos por Usuario" CssClass="text-info font-weight-bold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-sm-5">
                                                    <asp:Label ID="Label7" runat="server" Text="Lista de Tipos de Documentos"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="ListDocumentos" runat="server" CssClass="form-control" OnSelectedIndexChanged="ListDocumentos_SelectedIndexChanged" AutoPostBack="True" Rows="2"></asp:ListBox>
                                                            <asp:CheckBox ID="CheckBox_Doc" runat="server" OnCheckedChanged="CheckBox_Doc_CheckedChanged" AutoPostBack="true" />
                                                            <asp:Label ID="Label6" runat="server" Text="Seleccionar todos"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-sm-5">
                                                    <asp:Label ID="Label8" runat="server" Text="Documentos por Usuario"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="ListUsuariosDocumentos" runat="server" Height="150px" CssClass="form-control"
                                                                SelectionMode="Multiple"></asp:ListBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-1 text-center">
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                        <ContentTemplate>
                                                            <br />
                                                            <asp:LinkButton ID="linkBttnDelUsuDoctos" runat="server" CssClass="btn btn-success" OnClick="linkBttnDelUsuDoctos_Click">
                                                        <i class="fa fa-chevron-right" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <br />
                                                            <asp:LinkButton ID="linkBttnAddUsuDoctos" runat="server" CssClass="btn btn-success" OnClick="linkBttnAddUsuDoctos_Click">
                                                        <i class="fa fa-chevron-left" aria-hidden="true"></i>
                                                            </asp:LinkButton>

                                                            <%--<asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                                        Width="25px" OnClick="cmdAgregar_Click_d" /><br />
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                                        Width="25px" OnClick="cmdBorrar_Click_d" />--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="Label11" runat="server" Text="Lista de Usuarios"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="ListUsuarios_1" runat="server" Height="150px" CssClass="form-control"
                                                                SelectionMode="Multiple"></asp:ListBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col">
                                                    <asp:Label ID="Label17" runat="server" Text="Asignar Documentos por Estatus" CssClass="text-info font-weight-bold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-sm-5">
                                                    <asp:Label ID="Label12" runat="server" Text="Lista de Tipos de Documentos"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="ListDocs" runat="server" Height="150px" CssClass="form-control"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ListDocs_SelectedIndexChanged"></asp:ListBox>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged1" AutoPostBack="true" />
                                                            <asp:Label ID="Label15" runat="server" Text="Seleccionar todos"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-sm-5">
                                                    <asp:Label ID="Label13" runat="server" Text="Documentos por Estatus"></asp:Label>
                                                     <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                        <ContentTemplate>
                                                    <asp:ListBox ID="ListDocE" runat="server" Height="150px" CssClass="form-control"
                                                        SelectionMode="Multiple"></asp:ListBox>
                                                              </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-1 text-center">
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                            <br />
                                                            <asp:LinkButton ID="linkBttnDoctosEstatusIzq" runat="server" CssClass="btn btn-success" OnClick="linkBttnDoctosEstatusIzq_Click">
                                                        <i class="fa fa-chevron-right" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <br />

                                                            <asp:LinkButton ID="linkBttnDoctosEstatusDer" runat="server" CssClass="btn btn-success" OnClick="linkBttnDoctosEstatusDer_Click">
                                                        <i class="fa fa-chevron-left" aria-hidden="true"></i>
                                                            </asp:LinkButton>



                                                            <%-- <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                                        Width="25px" OnClick="cmAgregarEstatus_Click" /><br />
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                                        Width="25px" OnClick="cmBorrarEstatus_Click" />--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="Label14" runat="server" Text="Lista de Estatus"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="ListEstatus" runat="server" Height="150px" CssClass="form-control"
                                                                SelectionMode="Multiple"></asp:ListBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        <%--</ContentTemplate>--%>
                                       <%-- <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                        </Triggers>--%>
                                    <%--</asp:UpdatePanel>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseCuatro" aria-expanded="false">
                            <h4><span class="title">Privilegios de la aplicación</span>
                                <span class="accicon"><i class="fas fa-angle-down rotate-icon"></i></span></h4>
                        </div>
                        <div id="collapseCuatro" class="collapse" data-parent="#accordionExample">
                            <div class="card-body">


                                <asp:Panel ID="Panel3" runat="server">
                                    <%-- <table>
                            <tr>
                                <td class="auto-style2">
                                    <input id="bloqueado" type="image" value="button" onclick="slide_down_4(); return false" src="../Images/Repositorio/bloqueado.png" style="width: 20px;" />
                                    <input id="desbloqueado" type="image" value="button" onclick="slide_up_4(); return false" src="../Images/Repositorio/desbloqueado.png" style="width: 20px;" />
                                </td>
                                <td class="auto-style2">
                                    <label id="lbl_permisos_1" onclick="slide_down_lbl_p(); return false">Privilegios de la aplicación</label>
                                    <label id="lbl_permisos_2" onclick="slide_up_lbl_p(); return false">Privilegios de la aplicación</label>
                                </td>
                            </tr>
                        </table>

                        <table id="Table3" runat="server" align="center" class="table-responsive" width="100%" border="1"></table>
                                    --%>

                                    <asp:Panel ID="GridView" runat="server">
                                        <div>

                                            <div class="ex3">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="Permisos_App" runat="server" AutoGenerateColumns="False" RowHeaderColumn="IDPrivilegio" Width="100%" HorizontalAlign="Center" CssClass="table table-striped table-bordered" OnRowCommand="Permisos_App_RowCommand">
                                                            <Columns>
                                                                <asp:BoundField DataField="IDPrivilegio" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">

                                                                    <HeaderStyle CssClass="ColumnaOculta" />

                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton CommandName="Expand" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID="MenuP" runat="server">
                                                                            <asp:Image ID="MenuP_" runat="server" ImageUrl="../Images/Repositorio/anadir.png" CssClass="column_image" />
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton CommandName="Collapse" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID="MenuP2" runat="server" Visible="False">
                                                                            <asp:Image ID="Image5" runat="server" ImageUrl="../Images/Repositorio/eliminar.png" CssClass="column_image" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Permiso" HeaderText="Permiso" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                                                    <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50%">
                                                                    <ItemStyle HorizontalAlign="Justify" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Activar/Desactivar">

                                                                    <ItemTemplate>
                                                                        <label class="switch">
                                                                            <asp:CheckBox ID="Checkbox_P" runat="server" OnCheckedChanged="Checkbox_P_CheckedChanged" AutoPostBack="true" />
                                                                            <%--<asp:CheckBox ID="Checkbox_P" runat="server" OnCheckedChanged="Checkbox_P_CheckedChanged" AutoPostBack="true" xmlns:asp="#unknown"/>--%>

                                                                            <span class="slider round"></span>
                                                                        </label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="table-dark_1" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <div id="save_button" style="margin-top: 15px;">
                                                    <asp:Panel ID="Button" runat="server" HorizontalAlign="Center">
                                                        <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="Button1_Click" />
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-12">
                <asp:UpdatePanel ID="combos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>






                        <%--</ContentTemplate>--%>
                        <%--</asp:UpdatePanel>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>


</asp:Content>
