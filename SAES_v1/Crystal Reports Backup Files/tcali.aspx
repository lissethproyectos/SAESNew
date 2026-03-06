<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcali.aspx.cs" Inherits="SAES_v1.tcali" %>

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
       function validarClave(idEl,ind) {
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

       function validarCali(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Favor de ingresar clave calificación');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       function validarPuntos(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre) || !(/^[0-9]+$/.test(nombre)))
           {
               errorForm(idElemento, 'Favor de ingresar valor numérico');
               return false;
           } else {
               
               validadoForm(idElemento);
           }
       }

       function validarNivel(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == "00") {
                errorForm(idElemento, 'Debes seleccionar Nivel');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

       //---- Valida Campos Periodo ----//
       function validar_campos_tcali(e) {
           event.preventDefault(e);
           validarCali('ContentPlaceHolder1_txt_tcali');
           validarPuntos('ContentPlaceHolder1_txt_puntos');
           validarNivel('ContentPlaceHolder1_ddl_tnive');
           return false;
       }

       function noexist() {
           swal({
               allowEscapeKey: false,
               allowOutsideClick: false,
               type: 'success',
               html: '<h2 class="swal2-title" id="swal2-title">No existen datos para mostrar</h2>Favor de validar en el catálogo.'
           })
       }

   </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/tcali.png" style="width: 30px;" /><small>Calificaciones por Nivel</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
        <div class="col-md-4">
            <label for="ContentPlaceHolder1_search_campus" class="form-label">Nivel</label>
            <asp:DropDownList ID="ddl_tnive" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="grid_tcali_bind"></asp:DropDownList>
        </div>
    </div>
    <br />
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tcali" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tcali" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">                      
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_tcali" class="form-label">Clave Calificación</label>
                            <asp:TextBox ID="txt_tcali" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_puntos" class="form-label">Puntos</label>
                            <asp:TextBox ID="txt_puntos" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="text-align: center;">
                             <label for="ContentPlaceHolder1_aprobatoria" class="form-label">Aprobatoria</label>
                             <div class="custom-control custom-switch">
                               <input type="checkbox" class="custom-control-input" id="aprob" name="aprob">
                               <label class="custom-control-label" for="aprob"></label>
                               <asp:HiddenField ID="checked_input" runat="server" />
                             </div>
                        </div>
                        <div class="col-md-1" style="text-align: center;">
                             <label for="ContentPlaceHolder1_promedio" class="form-label">Promedio</label>
                             <div class="custom-control custom-switch">
                               <input type="checkbox" class="custom-control-input" id="prom" name="prom">
                               <label class="custom-control-label" for="prom"></label>
                               <asp:HiddenField ID="HiddenField1" runat="server" />
                             </div>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcali" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </div>
                </div>
                <div id="table_tcali">
                    <asp:GridView ID="Gridtcali" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtcali_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="PUNTOS" HeaderText="Puntos" />
                            <asp:BoundField DataField="APROBATORIA" HeaderText="Aprobatoria" />
                            <asp:BoundField DataField="PROMEDIO" HeaderText="Promedio" />
                            <asp:BoundField DataField="C_ESTATUS" HeaderText="C_Estatus">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_cancel" />
                <asp:AsyncPostBackTrigger ControlID="ddl_tnive" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
 <script>
     function load_datatable_tcali() {
         let table_periodo = $("#ContentPlaceHolder1_Gridtcali").DataTable({
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
                     title: 'SAES_Calificaciones por Nivel',
                     className: 'btn-dark',
                     extend: 'excel',
                     text: 'Exportar Excel',
                     exportOptions: {
                         columns: [1, 2, 3, 4,6,7]
                     }
                 },
                 {
                     title: 'SAES_Calificaciones por Nivel',
                     className: 'btn-dark',
                     extend: 'pdfHtml5',
                     text: 'Exportar PDF',
                     orientation: 'landscape',
                     pageSize: 'LEGAL',
                     exportOptions: {
                         columns: [1, 2, 3, 4,6,7]
                     }
                 }
             ],
             stateSave: true
         });
     }

     function load_datatable_tnive() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtnive").DataTable({
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

     function destroy_table() {
         $("#ContentPlaceHolder1_Gridtcali").DataTable().destroy();
     }
     function remove_class() {
         $('.selected_table').removeClass("selected_table")
     }
     function activar_check_aprob() {
            $("#aprob").attr('checked', true);
     }
     function desactivar_check_aprob() {
            $("#aprob").attr('checked', false);
     }
     function activar_check_prom() {
            $("#prom").attr('checked', true);
     }
     function desactivar_check_prom() {
            $("#prom").attr('checked', false);
     }
 </script>
</asp:Content>







