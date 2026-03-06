<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tseri.aspx.cs" Inherits="SAES_v1.tseri" %>

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
                    errorForm(idElemento, '<br/>Ingresar clave Programa');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            }
        }

        function validarClaveMateria(idEl, ind) {
            const idElemento = idEl;
            if (ind == 1) {
                let clave = document.getElementById(idElemento).value;
                errorForm(idElemento, 'Ingresar clave Programa');
                return false;
            }

            if (ind == 2) {
                let clave = document.getElementById(idElemento).value;
                errorForm(idElemento, 'Clave no pertenece a Plan de estudios');
                return false;

            }

            if (ind == 3) {
                let clave = document.getElementById(idElemento).value;
                if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                    errorForm(idElemento, 'Ingresar clave Materia');
                    return false;
                } else {
                    validadoForm(idElemento);
                }

            }

        }

        function validarSeriacion(idEl, ind) {
            const idElemento = idEl;
            if (ind == 0) {
                let clave = document.getElementById(idElemento).value;
                if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                    errorForm(idElemento, 'Ingresar Clave Seriación');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
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
        function validar_campos_tseri(e) {
            event.preventDefault(e);
            validarClave('ContentPlaceHolder1_txt_tprog', 0);
            validarClaveMateria('ContentPlaceHolder1_txt_tmate', 3);
            validarSeriacion('ContentPlaceHolder1_txt_seri1', 0);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-file-text" aria-hidden="true"></i>&nbsp;Seriación Materias</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <div id="form_seriacion" runat="server">
            <div class="form-row">
                <div class="col-sm-2">
                    Clave
                            <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                <ContentTemplate>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_tprog" MaxLength="10" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        <asp:LinkButton ID="linkBttnBuscaProg" class="btn btn-success" runat="server" OnClick="linkBttnBuscaProg_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:RequiredFieldValidator ID="reqPrograma" runat="server" ErrorMessage="Ingresar clave Programa" ControlToValidate="txt_tprog" ValidationGroup="buscaMat" SetFocusOnError="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-10">
                    Programa
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_nombre_prog" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </div>
            </div>
             <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="Gridtprog" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtprog_SelectedIndexChanged" EmptyDataText="No se encontraron programas." ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                                            <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                                        </Columns>
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
            <hr />
            <div class="form-row">
                <div class="col-sm-2">
                    Clave
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                     <div class="input-group">
                                         <asp:TextBox ID="txt_tmate" MaxLength="6" runat="server" CssClass="form-control" OnTextChanged="Carga_Materia" AutoPostBack="true"></asp:TextBox>
                                         <asp:LinkButton ID="linkBttnBuscaMateria" class="btn btn-success" runat="server" OnClick="linkBttnBuscaMateria_Click" ValidationGroup="buscaMat"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                     </div>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                </div>
                <div class="col-sm-10">
                    Materia
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_nombre_mate" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </div>

            </div>
            <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="Gridtmate" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtmate_SelectedIndexChanged" Visible="True" EmptyDataText="No se encontraron materias." ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AREA" HeaderText="Area" />
                                            <asp:BoundField DataField="CONS" HeaderText="Cons." />
                                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                                        </Columns>
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                    </div>
        </div>
        <div class="form-row">
            <div class="col-sm-2">
                Clave Seriación
                         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                             <ContentTemplate>
                                 <div class="input-group">
                                     <asp:TextBox ID="txt_seri1" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                                     <asp:LinkButton ID="linkBttnBuscaSeriacion" class="btn btn-success" runat="server" OnClick="linkBttnBuscaSeriacion_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                 </div>
                             </ContentTemplate>
                         </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Or
                         <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                             <ContentTemplate>
                                 <div class="input-group">
                                     <asp:TextBox ID="txt_seri2" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                                     <asp:LinkButton ID="linkBttnBuscaSeri2" class="btn btn-success" runat="server" OnClick="linkBttnBuscaSeri2_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                 </div>
                             </ContentTemplate>
                         </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Or
                         <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                             <ContentTemplate>
                                 <div class="input-group">
                                     <asp:TextBox ID="txt_seri3" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                                     <asp:LinkButton ID="linkBttnBuscaSeri3" class="btn btn-success" runat="server" OnClick="linkBttnBuscaSeri3_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                 </div>
                             </ContentTemplate>
                         </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="Gridmat" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridmat_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AREA" HeaderText="Area" />
                                            <asp:BoundField DataField="CONS" HeaderText="Cons." />
                                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                                        </Columns>
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>



        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <div class="row" id="btn_tstal" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtseri" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtseri_SelectedIndexChanged" Visible="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="clave" HeaderText="Clave" />
                                <asp:BoundField DataField="materia" HeaderText="Materia" />
                                <asp:BoundField DataField="cl_seriacion" HeaderText="Seriación" />
                                <asp:BoundField DataField="desc_seriacion" HeaderText="Nombre Materia" />
                                <asp:BoundField DataField="clave_or2" HeaderText="Or" />
                                <asp:BoundField DataField="or2" HeaderText="Nombre Materia" />
                                <asp:BoundField DataField="clave_or3" HeaderText="Or" />
                                <asp:BoundField DataField="or3" HeaderText="Nombre Materia" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>



    <!-- Modal Materias-->
    <div class="modal fade bd-example-modal-lg" id="modalMaterias" tabindex="-1" role="dialog" aria-labelledby="modalMateriasLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Materias</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal Programas-->
    <div class="modal fade bd-example-modal-lg" id="modalProgramas" tabindex="-1" role="dialog" aria-labelledby="modalProgramasLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalProgramasLabel">Programas</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                   
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>



    <!-- Modal Areas-->
    <div class="modal fade bd-example-modal-lg" id="modalAreas" tabindex="-1" role="dialog" aria-labelledby="modalProgramasLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAreasLabel">Áreas</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>


    <script>
        function load_datatable_tseri() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtseri").DataTable({
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
                        title: 'SAES_Seriación Materias',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7]
                        }
                    },
                    {
                        title: 'SAES_Seriación Materias',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function load_datatable_Programas() {
            $('#<%= Gridtprog.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtprog.ClientID %>').find("tr:first"))).DataTable({
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

        function load_datatable_Materias() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtmate").DataTable({
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

        function load_datatable_Mate() {
            let table_periodo = $("#ContentPlaceHolder1_Gridmat").DataTable({
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

        function destroy_table() {
            $("#ContentPlaceHolder1_Gridtstal").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>



