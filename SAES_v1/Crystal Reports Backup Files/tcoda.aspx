<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcoda.aspx.cs" Inherits="SAES_v1.tcoda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <style>
        span button {
            margin-bottom: 0px !important;
        }
    </style>
    <script>
        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error_transaccion() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
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

        //---- Nombre Alumno ----//

        function validarNombreContacto(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Nombre Contacto');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

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

        //---- Apellido Paterno Alumno ----//
        function validarApellidoP(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Apellido Paterno');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Apellido Materno Alumno ----//
        function validarApellidoM(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Apellido Materno');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- CURP ----//
        function validarCurp(idEl) {
            const idElemento = idEl;
            let valor = document.getElementById(idElemento).value;
            if (valor == null || valor.length == 0 || !(/^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/.test(valor))) {
                errorForm(idElemento, 'Ingresar CURP válido');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarCurp_format(valor) {
            if (valor == null || valor.length == 0 || !(/^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/.test(valor))) {
                errorForm('ContentPlaceHolder1_txt_curp', 'Ingresar CURP válido');
                return false;
            } else {
                validadoForm('ContentPlaceHolder1_txt_curp');
            }
        }

        function validarFecha(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Fecha Nacimiento');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarContacto(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Contacto');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function Noexiste(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;

                errorForm(idElemento, 'No existe Matrícula');
                return false;
        }

        //---- Valida Campos Solicitud ----//
        function validar_campos_solicitud(e) {
            event.preventDefault(e);
            validarMatricula('ContentPlaceHolder1_txt_matricula');
            validarCurp('ContentPlaceHolder1_txt_curp');
            validarNombreContacto('ContentPlaceHolder1_txt_nombre_cont');
            validarContacto('ContentPlaceHolder1_ddl_tcont');
            validarFecha('ContentPlaceHolder1_txt_f_nac');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Admisiones/tarjeta_usuario.png" style="width: 30px;" /><small>Datos Personales Contacto</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tpers" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_solicitud" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="grid_solicitudes_bind"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Matrícula</label>
                            <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control"  AutoPostBack="true" OnTextChanged="Alumno"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Alumno</label>
                            <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_tcont" class="form-label">Contacto</label>
                            <asp:DropDownList ID="ddl_tcont" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Nombre</label>
                            <asp:TextBox ID="txt_nombre_cont" MaxLength="60" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_curp" class="form-label">CURP</label>
                            <asp:TextBox ID="txt_curp" MaxLength="18" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                        </div>
                         <div class="col-md-2">
                            <label for="ContentPlaceHolder1_CboYear" class="form-label">Año</label>
                            <asp:DropDownList ID="CboYear" runat="server" CssClass="form-control" ONSELECTEDINDEXCHANGED="Carga_Month" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_CboMonth" class="form-label">Mes</label>
                            <asp:DropDownList ID="CboMonth" runat="server" CssClass="form-control" ONSELECTEDINDEXCHANGED="Carga_Day" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_CboDay" class="form-label">Día</label>
                            <asp:DropDownList ID="CboDay" runat="server" CssClass="form-control" ONSELECTEDINDEXCHANGED="Fecha_nacimiento" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_f_nac" class="form-label">Fecha Nacimiento</label>
                            <asp:TextBox ID="txt_f_nac"  runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>                       
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tpers" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click"/>
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click"/>
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </div>
                </div>
                 <div id="table_alumno">
                    <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Matrícula" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                            <asp:BoundField DataField="PATERNO" HeaderText="Apellido Paterno" />
                            <asp:BoundField DataField="MATERNO" HeaderText="Apellido Materno" />
                            <asp:BoundField DataField="C_GENERO" HeaderText="C_Genero">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GENERO" HeaderText="Genero" />
                            <asp:BoundField DataField="C_CIVIL" HeaderText="C_Civil">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="E_CIVIL" HeaderText="Estado Civil" />
                            <asp:BoundField DataField="CURP" HeaderText="CURP" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Nacimiento" />
                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />
                            <asp:BoundField DataField="FECHA_REG" HeaderText="Fecha Registro" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                <div id="table_campus">
                    <asp:GridView ID="GridSolicitudes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridSolicitudes_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="consecutivo" HeaderText="Consecutivo" />
                            <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="contacto" HeaderText="Contacto" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="curp" HeaderText="CURP" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha Nacimiento" />
                            <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                            <asp:BoundField DataField="fecha_reg" HeaderText="Fecha Registro" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function load_datatable() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridSolicitudes").DataTable({
                language: {
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
                scrollResize: true,
                scrollY: '500px',
                scrollCollapse: true,
                order: [
                    [0, "asc"]
                ],
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                   
                ],
                stateSave: true
            });
        }

        function load_datatableAlumno() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridAlumnos").DataTable({
                language: {
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
                scrollResize: true,
                scrollY: '500px',
                scrollCollapse: true,
                order: [
                    [0, "asc"]
                ],
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                   
                ],
                stateSave: true
            });
        }

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>

