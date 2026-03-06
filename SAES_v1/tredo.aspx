<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tredo.aspx.cs" Inherits="SAES_v1.tredo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />

    <style>
        span button {
            margin-bottom: 0px !important;
        }

        .image_button_docs {
            margin-left: 30px;
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

        //---- Fecha_Limite ----//
        function validarflimite(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar una fecha valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Fecha_Entrega ----//
        function validarfentrega(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar una fecha valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos Solicitud ----//
        function validar_campos_documentos(e) {
            event.preventDefault(e);
            validarflimite('ContentPlaceHolder1_txt_fecha_l');
            validarfentrega('ContentPlaceHolder1_txt_fecha_e');
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-folder-open" aria-hidden="true"></i>
            &nbsp;Documentos
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_documentos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tredo" runat="server">
                    <div class="row">
                        <div class="col-sm-2">
                            Matrícula
                             <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                 <ContentTemplate>
                                     <div class="input-group">
                                         <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" OnTextChanged="txt_matricula_TextChanged" AutoPostBack="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                                         <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                     </div>
                                 </ContentTemplate>
                             </asp:UpdatePanel>

                        </div>
                        <div class="col-sm-10">
                            Nombre
                             <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                 <ContentTemplate>
                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tpers_clave" HeaderText="Matrícula" />
                                        <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="tpers_paterno" HeaderText="Apellido Paterno" />
                                        <asp:BoundField DataField="tpers_materno" HeaderText="Apellido Materno" />
                                        <asp:BoundField DataField="tpers_num" HeaderText="pidm">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
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
                    <div class="row">
                        <div class="col-sm-2">
                            Clave Documento
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                     <div class="input-group">
                                         <asp:TextBox ID="txt_clave_doc" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                         <asp:LinkButton ID="linkBttnCveDoc" class="btn btn-success" runat="server" OnClick="linkBttnCveDoc_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                     </div>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-7">
                            Documento
                              <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                  <ContentTemplate>
                                      <asp:TextBox ID="txt_documento" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3">
                            Estatus
                              <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                  <ContentTemplate>
                                      <asp:TextBox ID="txt_estatus" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridDoctos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDoctos_SelectedIndexChanged" Visible="False" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron documentos.">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                            <asp:BoundField DataField="DOCUMENTO" HeaderText="Documento" />
                                        </Columns>
                                        <RowStyle Font-Size="Small" />
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                            Fecha Límite
                              <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                  <ContentTemplate>
                                      <asp:TextBox ID="txt_fecha_l" runat="server" CssClass="form-control"></asp:TextBox>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
                            <script>
                                function ctrl_fecha_i() {
                                    $('#ContentPlaceHolder1_txt_fecha_l').datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                        <div class="col-sm-2">
                            Fecha Entrega
                              <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                  <ContentTemplate>
                                      <asp:TextBox ID="txt_fecha_e" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
                            <script>
                                function ctrl_fecha_f() {
                                    $('#ContentPlaceHolder1_txt_fecha_e').datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                        <div class="col-sm-3">
                            Estatus
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_periodo" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>


                
                <div class="row" id="btn_documentos" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                                       <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Modificar" Visible="false" OnClick="btn_update_Click" />

                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="btn_save_Click" />
                        <asp:LinkButton ID="linkBttnDoctos" runat="server" CssClass="btn btn-round btn-success" CausesValidation="False" CommandName="Select" Text="Ver Documentos" OnClick="linkBttnDoctos_Click"></asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col">

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridDocumentos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDocumentos_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron documentos.">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttnSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID_NUM" HeaderText="Id_Num">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PERIODO" HeaderText="Periodo" />
                                        <asp:BoundField DataField="PROGRAMA" HeaderText="Programa" />
                                        <asp:BoundField DataField="CONSECUTIVO" HeaderText="Consecutivo">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CLAVE_DOCTO" HeaderText="Clave" />
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Documento" />
                                        <asp:BoundField DataField="C_ESTATUS" HeaderText="c_estatus">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                        <asp:BoundField DataField="F_LIMITE" HeaderText="Fecha Limite" />
                                        <asp:BoundField DataField="FECHA_LIMITE" HeaderText="Format_fecha_l">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="F_ENTREGA" HeaderText="Fecha Entrega" />
                                        <asp:BoundField DataField="FECHA_ENTREGA" HeaderText="Format_fecha_e">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                                        <asp:BoundField DataField="ST" HeaderText="ST Reg." />
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>



                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>


        <!-- Modal -->
        <div class="modal fade bd-example-modal-lg" id="modalDoctos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Documentos</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        <%--        <button type="button" class="btn btn-primary">Save changes</button>--%>
                    </div>
                </div>
            </div>
        </div>


    </div>
    <script>
        function load_datatable() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridDocumentos").DataTable({
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

        function load_datatable_Alumnos() {
                $('#<%= GridAlumnos.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridAlumnos.ClientID %>').find("tr:first"))).DataTable({
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

        function load_datatable_Doctos() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridDoctos").DataTable({
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
    </div>
</asp:Content>
