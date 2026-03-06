<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcodo.aspx.cs" Inherits="SAES_v1.tcodo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
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

        function noexist() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">No existen datos para mostrar</h2>Favor de validar en el catálogo.'
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

        //---- Clave ----//
        function validarClave(idEl, ind) {
            const idElemento = idEl;
            if (ind == 0) {
                let clave = document.getElementById(idElemento).value;
                if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                    errorForm(idElemento, 'Favor de ingresar clave válida');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            } else {
                errorForm(idElemento, 'La clave ingresada ya existe');
                return false;
            }

        }
        //---- Nombre ----//
        function validarNombre(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar descripción válida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }


        //---- Valida Campos Periodo ----//
        function validar_campos_tcodo(e) {
            event.preventDefault(e);
            validarClave('ContentPlaceHolder1_txt_tcodo', 0);
            validarNombre('ContentPlaceHolder1_txt_nombre');
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="x_title">
        <h2>
            <i class="fa fa-cog" aria-hidden="true"></i>
            &nbsp;Configuración Documentos</h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">

        <div class="row">
            <div class="col-sm-2">
                Clave
                                             <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                                 <ContentTemplate>
                                                     <div class="input-group">
                                                         <asp:TextBox ID="txt_tcodo" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
<%--                                                         <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>--%>
                                                     </div>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
            </div>
            <div class="col-sm-6">
                Descripción
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                 <ContentTemplate>
                                     <asp:TextBox ID="txtNombre"  runat="server" CssClass="form-control"></asp:TextBox>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Estatus
                              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                  <ContentTemplate>
                                      <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>
        </div>
        <hr />

        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrTipoDoctos" runat="server"
                    AssociatedUpdatePanelID="updPnlTipoDoctos">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>


        <asp:UpdatePanel ID="updPnlTipoDoctos" runat="server">
            <ContentTemplate>
                <div class="row" id="divTipoDoctos" runat="server" visible="false">
                    <div class="col">
                        <asp:GridView ID="Gridtdocu" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="Gridtdocu_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
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
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOMBRE" HeaderText="Descripción">
                                    <HeaderStyle Width="65%" />
                                    <ItemStyle Width="65%" />
                                </asp:BoundField>
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-sm-4">
                Campus
                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                  <ContentTemplate>
                                      <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged"></asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="reqCampus" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Campus" ControlToValidate="ddl_campus" InitialValue="000" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Nivel
                             <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                 <ContentTemplate>
                                     <asp:DropDownList ID="ddl_nivel" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged"></asp:DropDownList>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Colegio
                             <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                 <ContentTemplate>
                                     <asp:DropDownList ID="ddl_colegio" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_colegio_SelectedIndexChanged"></asp:DropDownList>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-4">
                Modalidad
                              <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                  <ContentTemplate>
                                      <asp:DropDownList ID="ddl_modalidad" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_modalidad_SelectedIndexChanged"></asp:DropDownList>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Programa
                             <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                 <ContentTemplate>
                                     <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Tipo Ingreso
                             <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                 <ContentTemplate>
                                     <asp:DropDownList ID="ddl_tipo" runat="server" CssClass="form-control"></asp:DropDownList>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>

        </div>


        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <div class="row" id="btn_tcodo" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" ValidationGroup="guardar" />
                        <asp:Button ID="btn_search" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" OnClick="btn_search_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtcodo" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtcodo_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
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
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                                <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                                <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                                <asp:BoundField DataField="COLEGIO" HeaderText="Colegio" />
                                <asp:BoundField DataField="MODALIDAD" HeaderText="Modalidad" />
                                <asp:BoundField DataField="PROGRAMA" HeaderText="Programa" />
                                <asp:BoundField DataField="TIPO" HeaderText="Tipo Ingreso" />
                                <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus_code">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


        <%--        <div class="modal fade bd-example-modal-lg" id="modalCatDoctos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Tipo de Documento</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="Gridtdocu" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="Gridtdocu_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CLAVE" HeaderText="Clave" >
                                                    <HeaderStyle Width="25%" />
                                                    <ItemStyle Width="25%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" >
                                                    <HeaderStyle Width="65%" />
                                                    <ItemStyle Width="65%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="selected_table" />
                                                <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>                             
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <script>
        function load_datatable_tcodo() {
            $('#<%= Gridtcodo.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtcodo.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'SAES_Configuración Documentos',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 10, 12]
                        }
                    },
                    {
                        title: 'SAES_Configuración Documentos',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 10, 12]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function load_datatable_tdocu() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtdocu").DataTable({
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

                scrollY: '500px',
                scrollCollapse: true,
                order: [
                    [2, "asc"]
                ], lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                dom: '<"top"if>rt<"bottom"lp><"clear">',
                stateSave: true
            });
        }

        function destroy_table() {
            $("#ContentPlaceHolder1_Gridtcodo").DataTable().destroy();
        }
        //function destroy_table() {
        //    $("#ContentPlaceHolder1_Gridtdocu").DataTable().destroy();
        //}
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>






