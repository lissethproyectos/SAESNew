<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpees.aspx.cs" Inherits="SAES_v1.tpees" %>

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

        function validarFechas() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- La fecha final debe ser mayor a la fecha inicial</h2>'
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

        //---- Clave Periodo ----//
        function validarClavePeriodo(idEl, ind) {
            const idElemento = idEl;
            if (ind == 0) {
                let clave = document.getElementById(idElemento).value;
                if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                    errorForm(idElemento, 'Favor de ingresar un periodo valido');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            } else {
                errorForm(idElemento, 'El periodo ingresado ya existe');
                return false;
            }

        }
        //---- Nombre Periodo ----//
        function validarNombrePeriodo(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar un nombre valido');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Fecha Inicio Periodo ----//
        function validarFechaInicio(idEl) {
            const idElemento = idEl;
            let fecha = document.getElementById(idElemento).value;
            if (fecha == null || fecha.length == 0 || /^\s+$/.test(fecha)) {
                errorForm(idElemento, 'Ingresar fecha valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Fecha Fin Periodo ----//
        function validarFechaFin(idEl) {
            const idElemento = idEl;
            let fecha = document.getElementById(idElemento).value;
            if (fecha == null || fecha.length == 0 || /^\s+$/.test(fecha)) {
                errorForm(idElemento, 'Ingresar fecha valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }


        //---- Valida Campos Periodo ----//
        function validar_campos_periodo(e) {
            event.preventDefault(e);
            validarClavePeriodo('ContentPlaceHolder1_txt_periodo', 0);
            validarNombrePeriodo('ContentPlaceHolder1_txt_nombre');
            validarFechaInicio('ContentPlaceHolder1_txt_fecha_i');
            validarFechaFin('ContentPlaceHolder1_txt_fecha_f');
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col">
            <div class="x_title">
                <h2>
                    <i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Periodos Escolares
                </h2>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <%--<div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrCampus" runat="server"
                    AssociatedUpdatePanelID="upd_Campus">
                    <ProgressTemplate>
                        <asp:Image ID="img2" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>--%>

        <div id="form_periodos" runat="server">
            <div class="form-row">
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_txt_periodo" class="form-label">Clave</label>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_periodo" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Periodo</label>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_txt_oficial" class="form-label">Oficial</label>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_oficial" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-row">
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_txt_fecha_i" class="form-label">Fecha Inicio</label>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_fecha_i" runat="server" class="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <script>
                        function ctrl_fecha_i() {

                            $('#ContentPlaceHolder1_txt_fecha_i').datepicker({
                                uiLibrary: 'bootstrap4',
                                locale: 'es-es',
                                format: "dd/mm/yyyy"
                            });
                        }
                    </script>
                </div>
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_txt_fecha_f" class="form-label">Fecha Fin</label>
                     <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                    <asp:TextBox ID="txt_fecha_f" runat="server" class="form-control"></asp:TextBox>
                             </ContentTemplate>
                    </asp:UpdatePanel>
                    <script>
                        function ctrl_fecha_f() {

                            $('#ContentPlaceHolder1_txt_fecha_f').datepicker({
                                uiLibrary: 'bootstrap4',
                                locale: 'es-es',
                                format: "dd/mm/yyyy"
                            });
                        }
                    </script>
                </div>
            </div>
        </div>

        <div class="form-row" style="text-align: center; margin: auto;" id="btn_periodo" runat="server">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlBotones" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" Visible="false" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridPeriodo" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridPeriodo_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="Periodo" />
                                <asp:BoundField DataField="OFICIAL" HeaderText="Oficial" />
                                <asp:BoundField DataField="FECHA_INI" HeaderText="Fecha Inicial" />
                                <asp:BoundField DataField="FECHA_FIN" HeaderText="Fecha Final" />
                                <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus_code">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="desc_estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />

                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
    <script>
        function load_datatable() {
            $('#<%= GridPeriodo.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridPeriodo.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function destroy_table() {
            $("#ContentPlaceHolder1_GridPeriodo").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>
