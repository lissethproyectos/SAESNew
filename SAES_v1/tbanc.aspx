<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tbanc.aspx.cs" Inherits="SAES_v1.tbanc" %>

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
        function validar_campos_tbanc(e) {
            event.preventDefault(e);
            validarClave('ContentPlaceHolder1_txt_tbanc', 0);
            validarNombre('ContentPlaceHolder1_txt_nombre');
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div id="form_periodos" runat="server">
            <div class="form-row">
                <div class="col-sm-2">
                    Clave
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                          <ContentTemplate>
                              <asp:TextBox ID="txt_tbanc" MaxLength="3" runat="server" CssClass="form-control"></asp:TextBox>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                </div>
                <div class="col-sm-4">
                    Descripción
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                          <ContentTemplate>
                              <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                </div>
                <div class="col-sm-2">
                    Estatus
                      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                          <ContentTemplate>
                              <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                </div>
                <div class="col-sm-4">
                    Concepto Cobranza
                      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                          <ContentTemplate>
                              <asp:DropDownList ID="ddl_tcoco" runat="server" CssClass="form-control"></asp:DropDownList>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-2">
                # de Convenio
                  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                      <ContentTemplate>
                          <asp:TextBox ID="txt_convenio" runat="server" CssClass="form-control"></asp:TextBox>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <div class="col-sm-6">
                Logotipo
                <div class="input-group">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload ID="fileUploadLogo" runat="server" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="linkBttnUpload" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:LinkButton ID="linkBttnUpload" runat="server" OnClick="linkBttnUpload_Click" CssClass="btn btn-success">Subir</asp:LinkButton>
                </div>
            </div>
            <div class="col-sm-4">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/sin-imagen.png" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>
        <div class="row" id="btn_tbanc" runat="server">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div id="table_tbanc">
                    <asp:UpdatePanel ID="updPnlGrid" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="Gridtbanc" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtbanc_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                                    <asp:BoundField DataField="CODIGO" HeaderText="Cod Detalle" />
                                    <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus">
                                        <HeaderStyle CssClass="ocultar" />
                                        <ItemStyle CssClass="ocultar" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                                    <asp:BoundField DataField="ruta_logo" HeaderText="Ruta Logo" />
                                    <asp:BoundField DataField="convenio" HeaderText="Convenio"  />
                                </Columns>
                                <RowStyle Font-Size="Small" />
                                <SelectedRowStyle CssClass="selected_table" />
                                <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>


    <script>
        function load_datatable() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtbanc").DataTable({
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
                        title: 'SAES_Catálogo de Tipos Dirección',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 4, 5]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Tipos Dirección',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 4, 5]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function destroy_table() {
            $("#ContentPlaceHolder1_Gridtbanc").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>


