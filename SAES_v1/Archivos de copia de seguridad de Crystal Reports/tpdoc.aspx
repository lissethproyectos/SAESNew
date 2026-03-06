<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpdoc.aspx.cs" Inherits="SAES_v1.tpdoc" %>

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
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
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
        function validarNombre(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Nombre');
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

        function validarGenero(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == "0") {
                errorForm(idElemento, 'Seleccionar Genero');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarEstado_C(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == "0") {
                errorForm(idElemento, 'Seleccionar Estado Civil');
                return false;
            } else {
                validadoForm(idElemento);
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

        function Noexiste(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;

            errorForm(idElemento, 'No existe Matrícula');
            return false;
        }

        //---- Valida Campos Solicitud ----//
        function validar_campos_solicitud(e) {
            event.preventDefault(e);
            validarNombre('ContentPlaceHolder1_txt_nombre');
            validarApellidoP('ContentPlaceHolder1_txt_apellido_p');
            validarApellidoM('ContentPlaceHolder1_txt_apellido_m');
            validarCurp('ContentPlaceHolder1_txt_curp');
            validarGenero('ContentPlaceHolder1_ddl_genero');
            validarEstado_C('ContentPlaceHolder1_ddl_estado_c');
            validarFecha('ContentPlaceHolder1_txt_f_nac');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-address-card" aria-hidden="true"></i>&nbsp;Datos Personales Docente
        </h2>
        <div class="clearfix"></div>
    </div>

    <div class="container-fluid">
        <div class="form-row">
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Clave</label>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Carga_Estudiante"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Docente(s)</label>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_txt_apellido_p" class="form-label">Apellido Paterno</label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_apellido_p" MaxLength="60" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                <label for="ContentPlaceHolder1_txt_apellido_m" class="form-label">Apellido Materno</label>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_apellido_m" MaxLength="60" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrSolicitudes" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel13">
                    <ProgressTemplate>
                        <asp:Image ID="img2" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <div class="accordion" id="accordionGrid">
                    <div class="card">
                        <div id="collapseGrid" class="collapse" data-parent="#accordionGrid">
                            <div class="card-body">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridSolicitudes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridSolicitudes_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="form-row">
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_ddl_genero" class="form-label">Genero</label>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_genero" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_ddl_estado_c" class="form-label">Estado Civil</label>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_estado_c" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_txt_curp" class="form-label">CURP</label>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_curp" MaxLength="18" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_f_nac" class="form-label">Fecha Nacimiento</label>
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_f_nac" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <script>
                    function ctrl_fecha_nac() {
                        $('#ContentPlaceHolder1_txt_f_nac').datepicker({
                            uiLibrary: 'bootstrap4',
                            locale: 'es-es',
                            format: 'dd/mm/yyyy'
                        });
                    }
                </script>

            </div>
            <%-- <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_consulta_docs" class="form-label">Consulta Datos Generales</label><br />
                            <asp:ImageButton ID="consulta_docs" runat="server" ImageUrl="~/Images/Admisiones/dosier.png" Width="40px" Height="40px" CssClass="image_button_docs" OnClick="consulta_docs_Click" />
                        </div>--%>
        </div>

        <br />
         <div class="row">
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
                <div class="row" id="btn_tpdoc" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                        <asp:LinkButton ID="linkBttnBuscar" CssClass="btn btn-round btn-success" runat="server" OnClick="linkBttnBuscar_Click">Consulta Datos Generales</asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </div>
    <script>
        function load_datatable() {
            $('#<%= GridSolicitudes.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridSolicitudes.ClientID %>').find("tr:first"))).DataTable({
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

