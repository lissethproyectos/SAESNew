<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tredo.aspx.cs" Inherits="SAES_v1.tredo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    
    <style>
        span button {
            margin-bottom: 0px !important;
        }
        .image_button_docs{
            margin-left:30px;
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
            <img src="Images/Admisiones/expediente.png" style="width: 30px;" /><small>Documentos</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_documentos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tredo" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="grid_alumnos_bind"/>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Matrícula</label>
                            <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" OnTextChanged="txt_matricula_TextChanged" AutoPostBack="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Nombre</label>
                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="w-100"></div>  
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="grid_doctos_bind"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_dtxt_clave_doc" class="form-label">Clave Documento</label>
                            <asp:TextBox ID="txt_clave_doc" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>                      
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_documento" class="form-label">Documento</label>
                            <asp:TextBox ID="txt_documento" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_dddl_estatus" class="form-label">Estatus</label>
                            <asp:TextBox ID="txt_estatus" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="w-100"></div>  
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_fecha_l" class="form-label">Fecha Límite</label>
                            <asp:TextBox ID="txt_fecha_l"  runat="server" CssClass="form-control"></asp:TextBox>
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

                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_fecha_e" class="form-label">Fecha Entrega</label>
                            <asp:TextBox ID="txt_fecha_e"  runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
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
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_consulta_docs" class="form-label">Consulta Documentos</label><br />
                            <asp:ImageButton ID="consulta_docs" runat="server" ImageUrl="~/Images/Admisiones/dosier.png" Width="40px" Height="40px" CssClass="image_button_docs" OnClick="consulta_docs_Click"/>
                        </div>
                        <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_periodo" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>
                <div id="table_campus">
                    <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Matrícula" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                            <asp:BoundField DataField="PATERNO" HeaderText="Apellido Paterno" />
                            <asp:BoundField DataField="MATERNO" HeaderText="Apellido Materno" />
                            <asp:BoundField DataField="PIDM" HeaderText="pidm">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
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
                <div id="table_doctos">
                    <asp:GridView ID="GridDoctos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDoctos_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="DOCUMENTO" HeaderText="Documento" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_documentos" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click"/>
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="btn_save_Click"/>
                    </div>
                </div>
                <div id="table_tredo">
                    <asp:GridView ID="GridDocumentos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDocumentos_SelectedIndexChanged">
                         <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
</asp:Content>
