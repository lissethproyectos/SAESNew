<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tdido.aspx.cs" Inherits="SAES_v1.tdido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
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
                 html: '<h2 class="swal2-title" id="swal2-title">Error !! Traslape de Horario</h2>Favor de validar en el listado.'
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

          function NoexisteAlumno() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Matrícula NO existe'
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

         //---- Matricula ----//
         function validarMatricula(idEl) {
             const idElemento = idEl;
             let nombre = document.getElementById(idElemento).value;
             if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                 errorForm(idElemento, 'Ingresar Clave de Docente');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

          //----Validación de Día ----//
         function validar_Dia(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar día de la semana');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

          //----Validación de Hora inicial----//
         function validar_hini(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar Hora inicial');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

          //----Validación de Hora final----//
         function validar_h_fin(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar Hora final');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         //----Validación de Estatus----//
         function validar_estatus(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar estatus');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }



         //---- Valida Campos Disponibilidad ----//
         function validar_campos_Disponibilidad(e) {
             event.preventDefault(e);
             validarMatricula('ContentPlaceHolder1_txt_matricula');         
             validar_Dia('ContentPlaceHolder1_ddl_dia');
             validar_hini('ContentPlaceHolder1_ddl_hini');
            // validar_hfin('ContentPlaceHolder1_ddl_hfin');
             validar_estatus('ContentPlaceHolder1_ddl_estatus');
             return false;
         }

         //---- Valida Campos Disponibilidad ----//
         function validar_campos_Disponibilidad1(e) {
             event.preventDefault(e);
             validarMatricula('ContentPlaceHolder1_txt_matricula');         
             validar_Dia('ContentPlaceHolder1_ddl_dia');
             validar_hini('ContentPlaceHolder1_ddl_hini');
             validar_hfin('ContentPlaceHolder1_ddl_hfin');
             validar_estatus('ContentPlaceHolder1_ddl_estatus');
             return false;
         }

     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="x_title">
        <h2>
            <img src="Images/operaciones/disponibilidad.png" style="width: 30px;" /><small>Disponibilidad Docente</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_Campus" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_talte" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda" OnClick="grid_docentes_bind" VISIBLE="true" />
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Clave</label>
                            <asp:TextBox ID="txt_matricula"  runat="server" CssClass="form-control" OnTextChanged="txt_matricula_TextChanged" AutoPostBack="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Docente</label>
                            <asp:TextBox ID="txt_nombre"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_dia" class="form-label">Día</label>
                            <asp:DropDownList ID="ddl_dia" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_hini" class="form-label">Hora Inicio</label>
                            <asp:DropDownList ID="ddl_hini" runat="server" CssClass="form-control" OnSelectedIndexChanged="Carga_Hora_fin" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_hfin" class="form-label">Hora Fin</label>
                            <asp:DropDownList ID="ddl_hfin" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>  
                    </div>
                    <div id="table_campus">
                    <asp:GridView ID="GridDocentes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDocentes_SelectedIndexChanged" Visible="false">
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
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tdido" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click"/>
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="Agregar_Click" />
                    </div>
                </div>
                <div id="table_disponibilidad">
                    <asp:GridView ID="GridDisponibilidad" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small"  Visible="false" onselectedindexchanged="GridDisponibilidad_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="Dia" HeaderText="Dia" />
                            <asp:BoundField DataField="H_ini" HeaderText="Hora Inicial" />
                            <asp:BoundField DataField="H_fin" HeaderText="Hora Final" />
                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                            <asp:BoundField DataField="cl_inicio" HeaderText="cl_inicio">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cl_fin" HeaderText="cl_fin">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="clave" HeaderText="clave">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
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
        function load_datatable_Disponibilidad() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridDisponibilidad").DataTable({
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

        function load_datatable_Docentes() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridDocentes").DataTable({
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



