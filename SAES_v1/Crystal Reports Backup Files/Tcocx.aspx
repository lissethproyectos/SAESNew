<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcocx.aspx.cs" Inherits="SAES_v1.tcocx" %>

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

        function Error1() {
           swal({
               allowEscapeKey: false,
               allowOutsideClick: false,
               type: 'success',
               html: '<h2 class="swal2-title" id="swal2-title">ERROR !!!</h2>Valor de Porcentaje debe ser numérico'
           })
        }

        function Error2() {
           swal({
               allowEscapeKey: false,
               allowOutsideClick: false,
               type: 'success',
               html: '<h2 class="swal2-title" id="swal2-title">ERROR !!!</h2>Suma de porcentajes debe ser 100%'
           })
        }

        function NoExists() {
           swal({
               allowEscapeKey: false,
               allowOutsideClick: false,
               type: 'success',
               html: '<h2 class="swal2-title" id="swal2-title">ERROR !!!</h2>No Existe clave de materia'
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
    <ul class="nav nav-tabs justify-content-center">
            <li class="nav-item">
                <a class="nav-link" href="Tcomp.aspx">Componentes</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" href="Tcocx.aspx">Configuración</a>
            </li>
    </ul>
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/tcomp.png" style="width: 30px;" /><small>Configuración Componentes Calificación</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <div class="col-md-12 col-sm-6">
            <asp:UpdatePanel ID="upd_dashboard" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddl_campus" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" CssClass="form-control-sm ddl_chart" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddl_nivel" CssClass="form-control-sm ddl_chart" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row justify-content-center">
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_ddl_tcaes" class="form-label">Programa</label>
                            <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control-sm ddl_chart" ></asp:DropDownList>
                        </div>
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="Busqueda_Materias"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_materia" class="form-label">Materia</label>
                            <asp:TextBox ID="txt_materia" MaxLength="10" runat="server" CssClass="form-control" OnTextChanged="txt_materia_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_nombre_mat" class="form-label">Materia</label>
                            <asp:TextBox ID="txt_nombre_mat"  runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div id="table_tmate">
                    <asp:GridView ID="Gridtmate" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtmate_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px"/>
                            <asp:BoundField DataField="Clave" HeaderText="Clave" />
                            <asp:BoundField DataField="Nombre" HeaderText="Descripción" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                     <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcocx" runat="server">
                                <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                                    <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="btn_save_Click" Visible="false"/>
                                    <asp:Button ID="btn_setup" runat="server" CssClass="btn btn-round btn-success" Text="Configurar"  OnClick="btn_setup_Click" />
                                </div>
                    </div>


                    <div id="form_tcocx" runat="server">
                                <div id="table_tcocx">
                                <asp:GridView ID="Gridtcocx" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false" >
                                    <Columns>
                                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                        <asp:BoundField DataField="Componente" HeaderText="Componente" />
                                       <ASP:TEMPLATEFIELD HEADERTEXT="Porcentaje"  ItemStyle-HorizontalAlign="Center">
                                                    <ITEMTEMPLATE >
                                                        <ASP:LABEL ID="Lbl" RUNAT="server" ItemStyle-Width="10px" Text="-->" 
                                                            BACKCOLOR="WhiteSmoke"  />    
                                                         <ASP:TEXTBOX ID="Porc" RUNAT="server" width="50px" 
                                                            BACKCOLOR="DarkGray"  BorderStyle="None" 
                                                            style="text-align:left"/> 
                                                    </ITEMTEMPLATE>
                                        </ASP:TEMPLATEFIELD>
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" ITEMSTYLE-HORIZONTALALIGN="Center"/>
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

 </script>
</asp:Content>



