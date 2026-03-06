<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpred2.aspx.cs" Inherits="SAES_v1.tpred2" %>
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
                   errorForm(idElemento, 'Favor de ingresar Matrícula');
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
               errorForm(idElemento, 'Favor de ingresar Clave válida');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       function validarPeriodo(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Favor de ingresar Periodo');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       function validarOrigen(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Favor de ingresar Carrera origen');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       function validarFolio(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Favor de ingresar Folio');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       function validarFechaInicio(idEl) {
           const idElemento = idEl;
           let fecha = document.getElementById(idElemento).value;
           if (fecha == null || fecha.length == 0 || /^\s+$/.test(fecha)) {
               errorForm(idElemento, 'Favor de ingresar fecha valida');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }


       //---- Valida Campos Periodo ----//
       function validar_campos_tpreq(e) {
           event.preventDefault(e);
           validarClave('ContentPlaceHolder1_TxtCuenta', 0);
           validarNombre('ContentPlaceHolder1_txt_esc_proc');
           validarFechaInicio('ContentPlaceHolder1_txt_fecha_i');
           validarPeriodo('ContentPlaceHolder1_txt_periodo');
           validarOrigen('ContentPlaceHolder1_txt_origen');
           validarFolio('ContentPlaceHolder1_txt_folio');
           return false;
       }

   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/tpred.png" style="width: 30px;" /><small>Materias en Predictamen</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tpred" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_periodos" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px; ">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="Busqueda_Alumnos"/>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_Matricula" class="form-label">Matrícula</label>
                            <asp:TextBox ID="TxtCuenta" MaxLength="10" runat="server" CssClass="form-control" OnTextChanged="Carga_Alumno"  AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Alumno</label>
                            <asp:TextBox ID="TxtNombre" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>

                    <div id="table_tprog">
                    <asp:GridView ID="GridProgramas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false" OnSelectedIndexChanged="GridProgramas_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="Campus" HeaderText="Campus" />
                            <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                            <asp:BoundField DataField="Clave" HeaderText="Clave"/>
                            <asp:BoundField DataField="Programa" HeaderText="Programa" />
                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                            <asp:BoundField DataField="Turno" HeaderText="Turno" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>

                    <div class="row g-3 justify-content-center" style="margin-top: 15px; "  >
                        <div class="col-md-.5">
                            Campus:
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbl_Campus" runat="server"  ForeColor="Blue"  ></asp:Label>
                        </div>
                         <div class="col-md-.5">
                            Nivel:
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbl_Nivel" runat="server"  ForeColor="Blue"  ></asp:Label>
                        </div>
                         <div class="col-md-1">
                            Programa:
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lbl_Programa" runat="server"  ForeColor="Blue"  ></asp:Label>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px; "  >
                         <div class="col-md-.5">
                            Estatus:
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbl_Estatus" runat="server"  ForeColor="Blue"  ></asp:Label>
                        </div>
                         <div class="col-md-.5">
                            Turno:
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbl_Turno" runat="server"  ForeColor="Blue"  ></asp:Label>
                        </div>
                         <div class="col-md-1">

                        </div>
                        <div class="col-md-4">

                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px; "  >
                         ________________________________________  REVALIDACIÓN / EQUIVALENCIA  ____________________________________________________
                    </div>
                </div>

                <div id="table_Alumnos" class="align-center" >
                        <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="50%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false" BorderStyle="Double">
                            <Columns>
                                <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                <asp:BoundField DataField="Alumno" HeaderText="Alumno" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
               </div>

               

                 <div id="table_tpreq">
                    <asp:GridView ID="GridTpreq" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false" OnSelectedIndexChanged="GridTpreq_SelectedIndexChanged" BorderWidth="2px">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="clave" HeaderText="Clave" />
                            <asp:BoundField DataField="Escuela" HeaderText="Escuela" />
                            <asp:BoundField DataField="carrera" HeaderText="Carrera"/>
                            <asp:BoundField DataField="folio" HeaderText="Folio" />
                            <asp:BoundField DataField="fecha_dict" HeaderText="Fecha_Dictamen" />
                            <asp:BoundField DataField="estatus" HeaderText="Estatus" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha_Registro" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="LightSteelBlue" ForeColor="white" />
                    </asp:GridView>
                </div>

                <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="Busqueda_Escuelas"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_esc_proc" class="form-label">Escuela de Procedencia</label>
                            <asp:TextBox ID="txt_esc_proc" MaxLength="6" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_nom_proc" class="form-label">Nombre Escuela</label>
                            <asp:TextBox ID="txt_nom_proc" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_txt_origen" class="form-label">Carrera Origen</label>
                            <asp:TextBox ID="txt_origen" MaxLength="80" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                </div>
                <div id="table_Escuelas" class="align-center">
                        <asp:GridView ID="GridEscuelas" runat="server" CssClass="table table-striped table-bordered" Width="50%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridEscuelas_SelectedIndexChanged" Visible="false">
                            <Columns>
                                <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                <asp:BoundField DataField="Escuela" HeaderText="Escuela" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
               </div>

               <div class="row g-3 justify-content-center" style="margin-top: 15px;">

                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_folio" class="form-label">Folio</label>
                            <asp:TextBox ID="txt_folio" MaxLength="15" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_fecha_i" class="form-label">Fecha Dictamen</label>
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
                </div>

                <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="Busqueda_Periodos"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_periodo" class="form-label">Periodo Aplicación</label>
                            <asp:TextBox ID="txt_periodo" MaxLength="6" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_nom_per" class="form-label">Nombre Periodo</label>
                            <asp:TextBox ID="txt_nom_per" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                </div>
                <div id="table_Periodos" class="align-center">
                        <asp:GridView ID="GridPeriodos" runat="server" CssClass="table table-striped table-bordered" Width="50%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridPeriodos_SelectedIndexChanged" Visible="false">
                            <Columns>
                                <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
               </div>

                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tespr" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" Visible="false"/>
                        
                    </div>
                </div>

                <div id="table_Tpred" class="align-center">
                        <asp:GridView ID="Plan" runat="server" CssClass="table table-striped table-bordered" Width="50%" AutoGenerateColumns="false" RowStyle-Font-Size="small"  Visible="false">
                            <Columns>
                                <asp:BoundField DataField="consec" HeaderText="Consecutivo" />
                                <asp:BoundField DataField="area" HeaderText="Area" />
                                <asp:BoundField DataField="clave" HeaderText="Clave" />
                                <asp:BoundField DataField="materia" HeaderText="Materia" />
                                <ASP:TEMPLATEFIELD HEADERTEXT="------- Materia Origen -------------------------- Calificación Estatus"  ItemStyle-HorizontalAlign="Right">
                                    <ITEMTEMPLATE >
                                        <ASP:LABEL ID="Lbl" RUNAT="server" ItemStyle-Width="10px" Text="-->" 
                                            BACKCOLOR="WhiteSmoke"  />
                                        <ASP:TEXTBOX ID="mat_origen" RUNAT="server" width="320px" 
                                            BACKCOLOR="LightBlue"  BorderStyle="None" 
                                            style="text-align:left"/>    
                                            <ASP:DROPDOWNLIST ID="CboCalif" RUNAT="server" WIDTH="60px" BACKCOLOR="WhiteSmoke"> </ASP:DROPDOWNLIST>
                                        <ASP:DROPDOWNLIST ID="CboSt" RUNAT="server" WIDTH="80px" BACKCOLOR="WhiteSmoke"> </ASP:DROPDOWNLIST>
                                    </ITEMTEMPLATE>
                                </ASP:TEMPLATEFIELD>
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
               </div>
               <div class="row justify-content-right" style="text-align: right; margin: auto;" id="Div2" runat="server">
                    <div class="col-md-10" style="text-align: right; margin-top: 15px;">
                        <asp:Label ID="resultado" runat="server" CssClass="bold" ForeColor="Red" WIDTH="200px" ></asp:Label>
                    </div>
                </div>

               <div class="row justify-content-center" style="text-align: center; margin: auto;" id="Div1" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        
                        <asp:Button ID="btn_tpred" runat="server" CssClass="btn btn-round btn-success" Text="Guardar PreDictamen" OnClick="Agregar_Materias" Visible="false"/>
                        
                    </div>
                   <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        
                        <asp:Button ID="btn_pdf" runat="server" CssClass="btn btn-round btn-success" Text="Imprimir" OnClick="PDF_Click" Visible="false"/>
                        
                    </div>
               </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
 <script>
     function load_datatable_Alumnos() {
         let table_periodo = $("#ContentPlaceHolder1_GridAlumnos").DataTable({
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

     function load_datatable_Programas() {
         let table_periodo = $("#ContentPlaceHolder1_GridProgramas").DataTable({
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

     function load_datatable_Escuelas() {
         let table_periodo = $("#ContentPlaceHolder1_GridEscuelas").DataTable({
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

     function load_datatable_Periodos() {
         let table_periodo = $("#ContentPlaceHolder1_GridPeriodos").DataTable({
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

     function load_datatable_Plan() {
         let table_periodo = $("#ContentPlaceHolder1_Plan").DataTable({
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
         $("#ContentPlaceHolder1_Gridtespr").DataTable().destroy();
     }
     function remove_class() {
         $('.selected_table').removeClass("selected_table")
     }
 </script>
</asp:Content>
