<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tress.aspx.cs" Inherits="SAES_v1.tress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <style>
        span button {
            margin-bottom: 0px !important;
        }

        legend {
            display: block;
            padding-left: 2px;
            padding-right: 2px;
            border: none;
        }
    </style>
    <script>
        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function registro_duplicado() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Registro duplicado</h2>Favor de validar'
            })
        }

        function error_transaccion() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Transacción de Base de Datos</h2>'
            })
        }

        function save() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
        }

        function update() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
        }

        function valida_creditos(totCreditos) {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">No cumple con el total de créditos requeridos</h2>Créditos requeridos ' + totCreditos + "</br>Créditos cursados"
            })
        }

        function valida_promedio() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">No cumple con el promedio requerido</h2>Favor de validar en el listado.'
            })
        }

        function NoExiste() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Código Postal NO existe'
            })
        }

        function NoexisteAlumno() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Matrícula NO existe'
            })
        }


        function CreditosIncompletos(cred_alu, cred_req) {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Créditos Incompletos</h2>Porcentaje de avance créditos del alumno:' + cred_alu + '<br/>Porcentaje de créditos requeridos:' + cred_req
            })
        }

        //-----  Función de agregar error  ------//
        function errorForm(idElementForm, textoAlerta) {
            let elemento = idElementForm;
            let textoInterno = textoAlerta;
            let elementoId = document.getElementById(elemento);
            elementoId.classList.add('error');
            elementoId.classList.remove('validado');
            elementoId.classList.remove('sinvalidar');
            errorId = 'error-' + String(elemento);
            let tieneError = document.getElementById(errorId);
            if (!tieneError) {
                const parrafo = document.createElement("p");
                const contParrafo = document.createTextNode(textoInterno);
                parrafo.appendChild(contParrafo);
                parrafo.classList.add('textoError');
                parrafo.id = 'error-' + String(elemento)
                //Depende de estructura de HTML
                elementoId.parentNode.appendChild(parrafo);
            }
        }

        //----- Función de remover error ------//
        function validadoForm(idElementForm) {
            let elemento = idElementForm;
            let elementoId = document.getElementById(elemento);
            elementoId.classList.remove('error');
            elementoId.classList.add('validado');
            elementoId.classList.remove('sinvalidar');
            errorId = 'error-' + String(elemento);
            let tieneError = document.getElementById(errorId);
            if (tieneError) {
                tieneError.remove();
            }
        }

        //---- Matricula ----//
        function validarMatricula(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Matrícula');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-user" aria-hidden="true"></i>&nbsp;Registro Servicio Social
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="card text-bg-light">
            <div class="card-header font-weight-bold">Datos del Alumno</div>
            <div class="card-body">
                <div class="form-row">
                    <div class="col text-center">
                        <asp:UpdateProgress ID="updPgrBusca" runat="server"
                            AssociatedUpdatePanelID="updPnlBusca">
                            <ProgressTemplate>
                                <asp:Image runat="server"
                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                    ImageUrl="~/Images/Sitemaster/loader.gif"
                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-sm-2">
                        <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Si no recuerdas la matricula del alumno, dar click en el icono de la lupa."></i>&nbsp;
                Matrícula
                <asp:UpdatePanel ID="updPnlBusca" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="linkBttnBusca_Click"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-5">
                        Alumno
                <asp:UpdatePanel ID="updPnlNombre" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-5">
                        Programa
                <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hddnNivel" runat="server" />
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPrograma" runat="server" ErrorMessage="Favor de seleccionar un programa" ControlToValidate="ddl_programa" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tpers_clave" HeaderText="Matrícula" />
                                        <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="tpers_paterno" HeaderText="Apellido Paterno" />
                                        <asp:BoundField DataField="tpers_materno" HeaderText="Apellido Materno" />
                                        <asp:BoundField DataField="tpers_cgenero" HeaderText="C_Genero">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpers_genero" HeaderText="Genero" />
                                        <asp:BoundField DataField="tpers_civil" HeaderText="C_Civil">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpers_ecivil" HeaderText="Estado Civil" />
                                        <asp:BoundField DataField="tpers_curp" HeaderText="CURP" />
                                        <asp:BoundField DataField="tpers_fecha" HeaderText="Fecha Nacimiento" />
                                        <asp:BoundField DataField="tpers_usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="tpers_fecha_reg" HeaderText="Fecha Registro" />
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="card text-bg-light">
            <div class="card-header font-weight-bold">Datos de Servicio Social</div>
            <div class="card-body">
                <div class="form-row">
                    <div class="col-sm-5">
                        Periodo
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" ErrorMessage="Favor de seleccionar periodo" ControlToValidate="ddl_periodo" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-7">
                        Programa
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa_ss" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqProgramaSS" runat="server" ErrorMessage="Favor de seleccionar un programa" ControlToValidate="ddl_programa_ss" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-sm-8">
                        Modalidad
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList runat="server" ID="ddl_modalidad" CssClass="form-control">
                                 <asp:ListItem>-------</asp:ListItem>
                                 <asp:ListItem Value="I">SS Interno</asp:ListItem>
                                 <asp:ListItem Value="E">SS Empresa Externa</asp:ListItem>
                                 <asp:ListItem Value="G">Empleado Gobierno</asp:ListItem>
                                 <asp:ListItem Value="D">SS por Edad/Enfermedad</asp:ListItem>
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqModalidad" runat="server" ErrorMessage="Favor de seleccionar modalidad" ControlToValidate="ddl_modalidad" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-2">
                        No. Horas SS
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_horas" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqHoras" runat="server" ErrorMessage="Ingresar horas" ControlToValidate="txt_horas" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valNumerico" runat="server" ErrorMessage="Solo núumeros" ValidationExpression="\d+" ControlToValidate="txt_horas" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="guardar"></asp:RegularExpressionValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-2">
                        No. Horas Cumplidas
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_horas_cumplidas" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txt_horas_cumplidas_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Ingresar horas cumplidas" ControlToValidate="txt_horas_cumplidas" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-sm-2">
                        Estatus
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_estatus" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_estatus_SelectedIndexChanged">
                            <asp:ListItem Value="A">Activo</asp:ListItem>
                            <asp:ListItem Value="B">Baja</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqEstatus" runat="server" ErrorMessage="Seleccionar Estatus" ControlToValidate="ddl_estatus" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-2">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <label for="ContentPlaceHolder1_txt_fecha_inicio" class="form-label">
                                    Fecha Inicio
                                <asp:TextBox ID="txt_fecha_inicio" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                    <script>
                                        $('#ContentPlaceHolder1_txt_fecha_inicio').datepicker({
                                            uiLibrary: 'bootstrap4',
                                            locale: 'es-es',
                                            format: "dd/mm/yyyy"
                                        })
                                    </script>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="reqFechaInicio" runat="server" ErrorMessage="Ingresar fecha inicial" ControlToValidate="txt_fecha_inicio" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="id" id="colFechaFin" runat="server" visible="false">
                                <div class="col-sm-7">
                                    <label for="ContentPlaceHolder1_txt_fecha_fin" class="form-label">
                                        <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Se activa cuando se encuentre en estatus TERMINADO."></i>&nbsp;Fecha Final
                                <asp:TextBox ID="txt_fecha_fin" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    </label>
                                    <asp:RequiredFieldValidator ID="reqFechaFin" runat="server" ErrorMessage="Ingresar fecha final" ControlToValidate="txt_fecha_fin" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>

                                    <script>
                                        $('#ContentPlaceHolder1_txt_fecha_fin').datepicker({
                                            uiLibrary: 'bootstrap4',
                                            locale: 'es-es',
                                            format: "dd/mm/yyyy",
                                            DI
                                        })
                                    </script>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>

            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBotones" runat="server"
                    AssociatedUpdatePanelID="updPnlBotones">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <asp:UpdatePanel ID="updPnlBotones" runat="server">
            <ContentTemplate>
                <div class="form-row" id="btn_tress" runat="server">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btn_cancel_update" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" Visible="False" OnClick="btn_cancel_update_Click" />
                        <asp:Button ID="btn_cancel_save" runat="server" CssClass="btn btn-round btn-secondary" OnClick="btn_cancel_save_Click" Text="Cancelar" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" ValidationGroup="guardar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" ValidationGroup="guardar" OnClick="btn_update_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGrid" runat="server"
                    AssociatedUpdatePanelID="updPnlGrid">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtress" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtress_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tress_tpees_clave" HeaderText="Periodo" />
                                <asp:BoundField DataField="tress_tprog_clave" HeaderText="Programa" />
                                <asp:BoundField DataField="tress_tprss_clave" HeaderText="Descripción" />
                                <asp:BoundField DataField="tress_desc_modalidad" HeaderText="Modalidad" />
                                <asp:BoundField DataField="trees_estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="trees_horas" HeaderText="# Horas" />
                                <asp:BoundField DataField="trees_horas_cumplidas" HeaderText="# Horas Cumplidas" />
                                <asp:BoundField DataField="tress_modalidad">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tress_fecha_inicio" HeaderText="Fecha Inicio" />
                                <asp:BoundField DataField="trees_fecha_fin" HeaderText="Fecha Final" />

                            </Columns>
                            <RowStyle Font-Size="Small" />
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <script>


            function ctrl_fecha_inicio() {
                $('#ContentPlaceHolder1_txt_fecha_inicio').datepicker({
                    uiLibrary: 'bootstrap4',
                    locale: 'es-es',
                    format: "dd/mm/yyyy"
                });
            }



            function ctrl_fecha_final() {
                $('#ContentPlaceHolder1_txt_fecha_fin').datepicker({
                    uiLibrary: 'bootstrap4',
                    locale: 'es-es',
                    format: "dd/mm/yyyy"
                });
            }

            function load_datatable_alumnos() {
                $('#<%= GridAlumnos.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridAlumnos.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "language": {
                        bProcessing: 'Procesando...',
                        sLengthMenu: 'Mostrar _MENU_ registros',
                        sZeroRecords: 'No se encontraron resultados',
                        sEmptyTable: 'Ningún dato disponible en esta tabla',
                        sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
                        sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
                        sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
                        sInfoPostFix: '',
                        sSearch: 'Buscar:',
                        sUrl: '',
                        sInfoThousands: '',
                        sLoadingRecords: 'Cargando...',
                        oPaginate: {
                            sFirst: 'Primero',
                            sLast: 'Último',
                            sNext: 'Siguiente',
                            sPrevious: 'Anterior'
                        }
                    },
                    "scrollResize": true,
                    "scrollY": '500px',
                    "scrollCollapse": true,
                    "order": [
                        [0, "asc"]
                    ],
                    "autoWidth": true,
                    "dom": '<"top"if>rt<"bottom"lBp><"clear">',
                    buttons: [
                        {
                            title: 'SAES',
                            className: 'btn-dark',
                            extend: 'excel',
                            text: 'Exportar Excel',
                            exportOptions: {
                                columns: [0, 1, 2, 3]
                            }
                        },
                        {
                            title: 'Cat Alumnos',
                            className: 'btn-dark',
                            extend: 'pdfHtml5',
                            text: 'Exportar PDF',
                            orientation: 'landscape',
                            exportOptions: {
                                columns: [1, 2, 3, 4, 5, 6]
                            }
                        }
                    ],
                    "stateSave": true
                });
            }

            function load_datatable() {
                $('#<%= Gridtress.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtress.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "language": {
                        bProcessing: 'Procesando...',
                        sLengthMenu: 'Mostrar _MENU_ registros',
                        sZeroRecords: 'No se encontraron resultados',
                        sEmptyTable: 'Ningún dato disponible en esta tabla',
                        sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
                        sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
                        sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
                        sInfoPostFix: '',
                        sSearch: 'Buscar:',
                        sUrl: '',
                        sInfoThousands: '',
                        sLoadingRecords: 'Cargando...',
                        oPaginate: {
                            sFirst: 'Primero',
                            sLast: 'Último',
                            sNext: 'Siguiente',
                            sPrevious: 'Anterior'
                        }
                    },
                    "scrollResize": true,
                    "scrollY": '500px',
                    "scrollCollapse": true,
                    "order": [
                        [0, "asc"]
                    ],
                    "autoWidth": true,
                    "dom": '<"top"if>rt<"bottom"lBp><"clear">',
                    buttons: [
                        {
                            title: 'SAES',
                            className: 'btn-dark',
                            extend: 'excel',
                            text: 'Exportar Excel',
                            exportOptions: {
                                columns: [1, 2, 3, 4, 5, 6, 7]
                            }
                        },
                        {
                            title: 'Registo  Servicio  Social',
                            className: 'btn-dark',
                            extend: 'pdfHtml5',
                            text: 'Exportar PDF',
                            orientation: 'landscape',
                            exportOptions: {
                                columns: [1, 2, 3, 4, 5, 6, 7]
                            }
                        }
                    ],
                    "stateSave": true
                });
            }
        </script>
    </div>
</asp:Content>
