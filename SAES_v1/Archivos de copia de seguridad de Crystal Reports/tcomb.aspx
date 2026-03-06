<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcomb.aspx.cs" Inherits="SAES_v1.tcomb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- No existe concepto cobranza</h2>'
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
       function validarClave(idEl,ind) {
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

        //---- Clave Programa ----//
        function validarTasa_Concepto(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresa Tasa');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Seleccionar Campus----//
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

        function noexist() {
           swal({
               allowEscapeKey: false,
               allowOutsideClick: false,
               type: 'success',
               html: '<h2 class="swal2-title" id="swal2-title">No existen datos para mostrar</h2>Favor de validar en el catálogo.'
           })
       }

        //---- Valida Campos Campus ----//
        function validar_campos_tasa(e) {
            event.preventDefault(e);
            validarclaveTasa('ContentPlaceHolder1_search_tasa');
            validarclaveConcepto('ContentPlaceHolder1_txt_concepto');
            return false;
        }
    </script>
    <style>
        #operacion ul{
            display:block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/capr.png" style="width: 30px;" /><small>Conceptos por Tasa</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_programa" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_search_tasa" class="form-label">Tasa</label>
                            <asp:DropDownList ID="search_tasa" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="search_tasa_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                                <div class="col-md-0.4">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="30px" Width="30px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="Busqueda_tasa"/>
                                </div>
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_c_prog_campus" class="form-label">Clave</label>
                                    <asp:TextBox ID="txt_concepto" runat="server" CssClass="form-control" OnTextChanged="txt_concepto_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Concepto</label>
                                    <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                                    <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div id="table_tdocu" class="align-center">
                                <asp:GridView ID="Gridttasa" runat="server" CssClass="table table-striped table-bordered" Width="50%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridConc_SelectedIndexChanged" Visible="false">
                                <Columns>
                                <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                                </Columns>
                                <SelectedRowStyle CssClass="selected_table" />
                                <HeaderStyle BackColor="SteelBlue" ForeColor="white" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto; margin-top: 15px;" id="btn_tasa_conceptos" runat="server">
                    <div class="col-md-3" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="cancelar_tasa" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_tasa_Click"/>
                        <asp:Button ID="guardar_tasa" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_tasa_Click"/>
                        <asp:Button ID="update_tasa" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="update_tasa_Click"/>
                    </div>
                </div>
                <div id="tabla_conceptos" style="margin-top: 15px;" runat="server">
                    <asp:GridView ID="GridConceptos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridConceptos_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="TASA" HeaderText="Tasa">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombre_tasa" HeaderText="Descripción">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Concepto" />
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
                            <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }

        function load_datatable() {
            let table_programas = $("#ContentPlaceHolder1_GridConceptos").DataTable({                
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
                        title: 'SAES_Catálogo de Tasa-Conceptos',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4,5, 6,8]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Tasa-Conceptos',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4,5, 6,8]
                        }
                    }
                ],
                stateSave: true
            });

        }

        function load_datatable_ttasa() {
            let table_periodo = $("#ContentPlaceHolder1_Gridttasa").DataTable({
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


