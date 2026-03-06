<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcuot.aspx.cs" Inherits="SAES_v1.tcuot" %>

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

       function Traslape() {
           swal({
               allowEscapeKey: false,
               allowOutsideClick: false,
               type: 'success',
               html: '<h2 class="swal2-title" id="swal2-title">Existen cuota(s) vigentes</h2>Favor de validar en el listado.'
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

       //---- Clave Concepto ----//
       function validarClaveConcepto(idEl,ind) {
           const idElemento = idEl;
           if (ind == 0) {
               let clave = document.getElementById(idElemento).value;
               if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                   errorForm(idElemento, 'Ingresar Concepto');
                   return false;
               } else {
                   validadoForm(idElemento);
               }
           } else {
               errorForm(idElemento, 'El concepto ya existe');
               return false;
           }

       }
       //---- Nombre Periodo ----//
       function validarNombreConcepto(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar descripción');
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

       function validarImporte(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar Importe');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       function validarEntero_importe(idEl){
        //intento convertir a entero.
        //si era un entero no le afecta, si no lo era lo intenta convertir
        const idElemento = idEl;
        valor = parseInt(idElemento)

        //Compruebo si es un valor numérico
        if (isNaN(valor)) {
            //entonces (no es numero) devuelvo el valor cadena vacia
            errorForm(idElemento, 'Importe es valor numérico');
            return false
        }else{
            //En caso contrario (Si era un número) devuelvo el valor
            validadoForm(idElemento);
            }
       }

       //---- Valida Campos Cuotas ----//
       function validar_campos_cuotas(e) {
           event.preventDefault(e);
           validarClaveConcepto('ContentPlaceHolder1_txt_concepto',0);
           validarImporte('ContentPlaceHolder1_txt_importe');
           validarFechaInicio('ContentPlaceHolder1_txt_fecha_i');
           validarFechaFin('ContentPlaceHolder1_txt_fecha_f');
           return false;
       }

       function valida_importe(e) {
           validarEntero_importe('ContentPlaceHolder1_txt_importe');
       }

   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/Periodos.png" style="width: 30px;" /><small>Catálogo de Cuotas</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_Campus" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_periodos" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="grid_codigos_bind"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_concepto" class="form-label">Clave</label>
                            <asp:TextBox ID="txt_concepto" MaxLength="4" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Concepto</label>
                            <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
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

                        <div class="col-md-2">
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
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_importe" class="form-label">Importe</label>
                            <asp:TextBox ID="txt_importe" MaxLength="4" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">

                    </div>
                </div>
                <div id="table_codigos">
                    <asp:GridView ID="GridCodigos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridcodigos_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Concepto" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                    <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddp_campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="Carga_Nivel"></asp:DropDownList>
                            
                    </div>
                    <div class="col-md-4">
                        <label for="ContentPlaceHolder1_ddl_nivel" class="form-label">Nivel</label>
                        <asp:DropDownList ID="ddl_nivel" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                            
                    </div>
                    <div class="col-md-4">
                        <label for="ContentPlaceHolder1_ddl_colegio" class="form-label">Colegio</label>
                        <asp:DropDownList ID="ddl_colegio" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>

                    </div>
                    <div class="col-md-3">
                        <label for="ContentPlaceHolder1_ddl_modalidad" class="form-label">Modalidad</label>
                        <asp:DropDownList ID="ddl_modalidad" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                            
                    </div>
                    <div class="col-md-3">
                        <label for="ContentPlaceHolder1_ddl_programa" class="form-label">Programa</label>
                        <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                            
                    </div>
                    <div class="col-md-3">
                        <label for="ContentPlaceHolder1_ddl_ingreso" class="form-label">Tipo Ingreso</label>
                        <asp:DropDownList ID="ddl_tipo" runat="server" CssClass="form-control"></asp:DropDownList>

                    </div>
                    <div class="col-md-3">
                        <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                        <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-12">
                        <table align="center" class="table-responsive" width="1000px" border="1"></table>
                    </div>
                    <div class="col-md-1" style="text-align: center;">
                                <label for="ContentPlaceHolder1_customSwitches1" class="form-label">Cuotas Vigentes</label>
                                <div class="custom-control custom-switch">
                                    <input type="checkbox" class="custom-control-input" id="customSwitches1" name="customSwitches1">
                                    <label class="custom-control-label" for="customSwitches1"></label>
                                    <asp:HiddenField ID="checked_input1" runat="server" />
                                </div>
                    </div>
                </div>
                
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_periodo" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_query" runat="server" CssClass="btn btn-round btn-success" Text="Consultar"  OnClick="Query_click" />
                    </div>
                </div>
                <div id="table_conceptos">
                    <asp:GridView ID="GridConcepto" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridConcepto_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Concepto" />
                            <asp:BoundField DataField="IMPORTE" HeaderText="Importe" />
                            <asp:BoundField DataField="inicio" HeaderText="Fecha Inicio" />
                            <asp:BoundField DataField="fin" HeaderText="Fecha Fin" />
                            <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                            <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                            <asp:BoundField DataField="COLEGIO" HeaderText="Colegio" />
                            <asp:BoundField DataField="MODA" HeaderText="Modalidad" />
                            <asp:BoundField DataField="PROG" HeaderText="Programa" />
                            <asp:BoundField DataField="TIIN" HeaderText="Tipo_Ingreso" />
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                <div id="table_cuotas">
                    <asp:GridView ID="Cuotas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small"  Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                            <asp:BoundField DataField="IMPORTE" HeaderText="Importe" />
                            <asp:BoundField DataField="inicio" HeaderText="Fecha Inicio" />
                            <asp:BoundField DataField="fin" HeaderText="Fecha Fin" />
                            <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                            <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                            <asp:BoundField DataField="COLEGIO" HeaderText="Colegio" />
                            <asp:BoundField DataField="MODA" HeaderText="Modalidad" />
                            <asp:BoundField DataField="PROG" HeaderText="Programa" />
                            <asp:BoundField DataField="TIIN" HeaderText="Tipo_Ingreso" />
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
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
      $("#customSwitches1").on("change", function () {
            if (this.checked) {
                $("#ContentPlaceHolder1_checked_input1").val('1')

            } else {
                $("#ContentPlaceHolder1_checked_input1").val('0')
            }
            console.log($("#customSwitches1").val());
            console.log($("#ContentPlaceHolder1_checked_input1").val());
      });


     function load_datatable() {
         let table_periodo = $("#ContentPlaceHolder1_GridConcepto").DataTable({
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
                     title: 'SAES_Catálogo de Cuotas',
                     className: 'btn-dark',
                     extend: 'excel',
                     text: 'Exportar Excel',
                     exportOptions: {
                         columns: [1, 2, 3, 4, 5, 7,8,9,10,11,13,14]
                     }
                 },
                 {
                     title: 'SAES_Catálogo de Cuotas',
                     className: 'btn-dark',
                     extend: 'pdfHtml5',
                     text: 'Exportar PDF',
                     orientation: 'landscape',
                     pageSize: 'LEGAL',
                     exportOptions: {
                         columns: [1, 2, 3, 4, 5, 7,8,9,10,11,13,14]
                     }
                 }
             ],
             stateSave: true
         });
     }

     function load_datatable_Codigos() {
         let table_periodo = $("#ContentPlaceHolder1_GridCodigos").DataTable({
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
         $("#ContentPlaceHolder1_GridPeriodo").DataTable().destroy();
     }
     function remove_class() {
         $('.selected_table').removeClass("selected_table")
     }

     function desactivar_check1() {
            $("#customSwitches1").attr('checked', false);
     }
     function activar_check1() {
            $("#customSwitches1").attr('checked', true);
     }
 </script>
</asp:Content>


