<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcapr.aspx.cs" Inherits="SAES_v1.tcapr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        function validarclavePrograma(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Por favor ingresa una clave valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Clave Programa No valida----//
        function validarclavePrograma_N(idEl, indicador) {
            const idElemento = idEl;
            if (indicador == 1) {
                errorForm(idElemento, 'Por favor ingresa una clave valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Seleccionar Campus----//
        function validarcampus_prog(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Debes seleccionar una campus válido');
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
        function validar_campos_campus(e) {
            event.preventDefault(e);
            validarcampus_prog('ContentPlaceHolder1_search_campus');
            validarclavePrograma('ContentPlaceHolder1_c_prog_campus');
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
            <i class="fa fa-building" aria-hidden="true"></i>
            &nbsp;Programas por Campus</h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div id="form_programa" runat="server">
            <div class="form-row">


                <%-- <div class="col-sm-0.4">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="30px" Width="30px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="Busqueda_Programa"/>
                                </div>--%>
              
            </div>
            
            <div class="form-row">
                <div class="col-sm-12">
                    Campus
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="search_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="search_campus_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </div>
                </div>
             <div class="form-row">
                  <div class="col-sm-2">
                    Clave
                               <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                   <ContentTemplate>
                                       <div class="input-group">

                                           <asp:TextBox ID="c_prog_campus" runat="server" CssClass="form-control" OnTextChanged="c_prog_campus_TextChanged" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                           <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>

                                       </div>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
                </div>
                <div class="col-md-8">
                    Programa
                   <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                       <ContentTemplate>
                           <asp:TextBox ID="n_prog_campus" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                       </ContentTemplate>
                   </asp:UpdatePanel>
                </div>
                <div class="col-sm-1">
                    Estatus
                            <asp:DropDownList ID="e_prog_campus" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-sm-1" style="text-align: center;">
                    Admisión
                            <div class="custom-control custom-switch">
                                <input type="checkbox" class="custom-control-input" id="customSwitches" name="customSwitches">
                                <label class="custom-control-label" for="customSwitches"></label>
                                <asp:HiddenField ID="checked_input" runat="server" />
                            </div>
                </div>
            </div>
            <div class="form-row">
                <div class="col">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="Gridtprog" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtprog_SelectedIndexChanged" Visible="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="row" id="btn_programa" runat="server">
                        <div class="col text-center">
                            <asp:Button ID="cancelar_prog" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_prog_Click" />
                            <asp:Button ID="guardar_prog" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_prog_Click" />
                            <asp:Button ID="update_prog" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="update_prog_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <hr />
            <div class="row">
                <div class="col">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridProgramas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridProgramas_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron programas.">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Programa" />
                                    <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                                    <asp:BoundField DataField="MODALIDAD" HeaderText="Modalidad" />
                                    <asp:BoundField DataField="ADMISION" HeaderText="Admisión" />
                                    <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                        <HeaderStyle CssClass="ocultar" />
                                        <ItemStyle CssClass="ocultar" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha de Modificacion" />
                                    <asp:BoundField DataField="C_CAMPUS" HeaderText="Campus_code">
                                        <HeaderStyle CssClass="ocultar" />
                                        <ItemStyle CssClass="ocultar" />
                                    </asp:BoundField>
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
    <script>
        $("#customSwitches").on("change", function () {
            if (this.checked) {
                $("#ContentPlaceHolder1_checked_input").val('1')

            } else {
                $("#ContentPlaceHolder1_checked_input").val('0')
            }
            console.log($("#customSwitches").val());
            console.log($("#ContentPlaceHolder1_checked_input").val());
        });

        function activar_check() {
            $("#customSwitches").attr('checked', true);
        }
        function desactivar_check() {
            $("#customSwitches").attr('checked', false);
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }

        function load_datatable() {
            let table_programas = $("#ContentPlaceHolder1_GridProgramas").DataTable({
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
                        title: 'SAES_Catálogo de Campus-Programas',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 7, 8]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus-Programas',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 7, 8]
                        }
                    }
                ],
                stateSave: true
            });

        }

        function load_datatable_tprog() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtprog").DataTable({
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
