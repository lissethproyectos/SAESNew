<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tfeve.aspx.cs" Inherits="SAES_v1.tfeve" %>

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

        function No_Exists() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- No existe clave de periodo</h2>'
            })
        }

        function carga_menu() {
            $("#operacion").addClass("active");
            $("#campus").addClass("current-page");
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

        //---- Clave ----//
        function validarClave(idEl, ind) {
            const idElemento = idEl;
            if (ind == 0) {
                let clave = document.getElementById(idElemento).value;
                if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                    errorForm(idElemento, 'Ingresar clave');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            } else {
                errorForm(idElemento, 'La clave ingresada ya existe');
                return false;
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

        //---- No.Parcialidad ----//
        function validarParcialidad(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresa No.Parcialidad');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Fecha Vencimiento ----//
        function validarFecha(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresa Fecha Vencimiento');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Seleccionar Periodo----//
        function validarclavePeriodo(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Periodo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Seleccionar Tasa----//
        function validarclaveTasa(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Tasa');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos Campus ----//
        function validar_campos_tfeve(e) {
            event.preventDefault(e);
            validarclavePeriodo('ContentPlaceHolder1_txt_periodo');
            validarclaveTasa('ContentPlaceHolder1_search_tasa');
            validarParcialidad('ContentPlaceHolder1_txt_numero');
            validarFecha('ContentPlaceHolder1_txt_fecha');
            return false;
        }

    </script>
    <style>
        #operacion ul {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-calendar" aria-hidden="true"></i><small>Fechas de Vencimiento</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_programa" runat="server">
                    <div class="form-row">
                        <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                            ToolTip="Búsqueda" Visible="false" OnClick="grid_periodos_bind" />
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_txt_periodo" class="form-label">Periodo</label>
                            <div class="input-group">
                                <asp:TextBox ID="txt_periodo" runat="server" CssClass="form-control" OnTextChanged="Busca_Periodo" AutoPostBack="true"></asp:TextBox>
                                <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <label for="ContentPlaceHolder1_txt_nombre_periodo" class="form-label">Periodo</label>
                            <asp:TextBox ID="txt_nombre_periodo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_search_tasa" class="form-label">Tasa</label>
                            <asp:DropDownList ID="search_tasa" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="search_tasa_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>

                    <div id="Div1" class="row" runat="server">
                        <div class="col">
                            <asp:GridView ID="Gridtpees" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtpees_SelectedIndexChanged">
                                <Columns>
                                    <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="Fecha_ini" HeaderText="Fecha_Inicio" />
                                    <asp:BoundField DataField="Fecha_fin" HeaderText="Fecha_Fin" />
                                </Columns>
                                <SelectedRowStyle CssClass="selected_table" />
                                <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                            </asp:GridView>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-row">
                                <div class="col-sm-2">
                                    <label for="ContentPlaceHolder1_txt_numero" class="form-label">No.parcialidad</label>
                                    <asp:TextBox ID="txt_numero" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <label for="ContentPlaceHolder1_txt_fecha_i" class="form-label">Fecha Vencimiento</label>
                                    <asp:TextBox ID="txt_fecha_i" runat="server" class="form-control"></asp:TextBox>
                                    <script>
                                        function ctrl_fecha() {

                                            $('#ContentPlaceHolder1_txt_fecha_i').datepicker({
                                                uiLibrary: 'bootstrap4',
                                                locale: 'es-es',
                                                format: "dd/mm/yyyy"
                                            });
                                        }
                                    </script>
                                </div>
                                <div class="col-sm-2">
                                    <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                                    <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <hr />
                <div class="row justify-content-center" style="text-align: center; margin: auto; margin-top: 15px;" id="btn_tfeve" runat="server">
                    <div class="col-md-3" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="cancelar_tfeve" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_tfeve_Click" />
                        <asp:Button ID="guardar_tfeve" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_tfeve_Click" />
                        <asp:Button ID="update_tfeve" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="update_tfeve_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="GridFechas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridFechas_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                                <asp:BoundField DataField="Periodo" HeaderText="Periodo">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nom_periodo" HeaderText="Nom_Periodo">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TASA" HeaderText="Tasa">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOMBRE" HeaderText="Descripción">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Numero" HeaderText="Número Parcialidad" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha Vencimiento" />
                                <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                <asp:BoundField DataField="Fecha_reg" HeaderText="Fecha_reg" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }

        function load_datatable() {
            let table_programas = $("#ContentPlaceHolder1_GridFechas").DataTable({
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
                    {
                        title: 'SAES_Fechas de Vencimiento',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8]
                        }
                    },
                    {
                        title: 'SAES_Fechas de Vencimiento',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8]
                        }
                    }
                ],
                stateSave: true
            });

        }

        function load_datatable_tpees() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtpees").DataTable({
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
                dom: '<"top"if>rt<"bottom"lp><"clear">',
                stateSave: true
            });
        }

    </script>
</asp:Content>



