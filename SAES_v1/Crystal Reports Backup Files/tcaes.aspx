<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcaes.aspx.cs" Inherits="SAES_v1.tcaes" %>

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
        function Nodatos() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">No existen datos para mostrar</h2>'
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

             //---- Valida Campos  ----//
       function validar_campos_tcaes(e) {
           event.preventDefault(e);
           validarPeriodo('ContentPlaceHolder1_ddl_periodo');
           validarCampus('ContentPlaceHolder1_ddl_campus');
           validarNivel('ContentPlaceHolder1_ddl_nivel');
           validarTcaes('ContentPlaceHolder1_ddl_tcaes');
           return false;
        }

        function validarPeriodo(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == "0") {
                errorForm(idElemento, 'Debes seleccionar Periodo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarCampus(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == "0") {
                errorForm(idElemento, 'Debes seleccionar Campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarNivel(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == "0") {
                errorForm(idElemento, 'Debes seleccionar Nivel');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarTcaes(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == "0") {
                errorForm(idElemento, 'Debes seleccionar Concepto Calendario');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

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

    </script>
    <style>
        .ddl_chart {
            width: 100%;
            font-size: small;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/calendario.png" style="width: 30px;" /><small>Calendario Escolar</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <div class="col-md-12 col-sm-6">
            <asp:UpdatePanel ID="upd_dashboard" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddl_periodo" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" CssClass="form-control-sm ddl_chart" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddl_campus" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" CssClass="form-control-sm ddl_chart" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddl_nivel" CssClass="form-control-sm ddl_chart" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_ddl_tcaes" class="form-label">Concepto Calendario</label>
                            <asp:DropDownList runat="server" ID="ddl_tcaes" CssClass="form-control-sm ddl_chart" ></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                         <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_fecha_i" class="form-label">Fecha Inicio</label>
                            <asp:TextBox ID="txt_fecha_i" runat="server" class="form-control" ></asp:TextBox>
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

                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_fecha_f" class="form-label">Fecha Fin</label>
                            <asp:TextBox ID="txt_fecha_f" runat="server" class="form-control" ></asp:TextBox>
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
                     <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcaes" runat="server" visible="false">
                                <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                                    <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                                </div>
                    </div>


                    <div id="form_tcave" runat="server">
                                <div id="table_tcaes">
                                <asp:GridView ID="Gridtcaes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false" OnSelectedIndexChanged="GridTcaes_SelectedIndexChanged">
                                    <Columns>
                                        <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Concepto" />
                                        <asp:BoundField DataField="Fecha_I" HeaderText="Fecha Inicio" ITEMSTYLE-HORIZONTALALIGN="Center"/>
                                        <asp:BoundField DataField="Fecha_F" HeaderText="Fecha Fin" ITEMSTYLE-HORIZONTALALIGN="Center"/>
                                        <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus_code">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estatus" HeaderText="Estatus" ITEMSTYLE-HORIZONTALALIGN="Center"/>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha Registro" ITEMSTYLE-HORIZONTALALIGN="Center"/>
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>
                                </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    </div>
   
<script>
     function load_datatable() {
         let table_periodo = $("#ContentPlaceHolder1_Gridtcaes").DataTable({
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
                     title: 'SAES_Calendario Escolar',
                     className: 'btn-dark',
                     extend: 'excel',
                     text: 'Exportar Excel',
                     exportOptions: {
                         columns: [1, 2, 3, 4, 5,6]
                     }
                 },
                 {
                     title: 'SAES_Calendario Escolar',
                     className: 'btn-dark',
                     extend: 'pdfHtml5',
                     text: 'Exportar PDF',
                     orientation: 'landscape',
                     pageSize: 'LEGAL',
                     exportOptions: {
                         columns: [1, 2, 3, 4, 5,6]
                     }
                 }
             ],
             stateSave: true
         });
     }

 </script>
</asp:Content>


