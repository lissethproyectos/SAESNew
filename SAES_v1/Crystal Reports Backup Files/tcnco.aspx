<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcnco.aspx.cs" Inherits="SAES_v1.tcnco" %>
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
                 errorForm(idElemento, 'Ingresar Matrícula');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         //---- Correo ----//
         function validarcorreo(idEl) {
             const idElemento = idEl;
             let nombre = document.getElementById(idElemento).value;
             if (nombre == null || nombre.length == 0 || !(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(nombre))) {
                 errorForm(idElemento, 'Ingresar Correo válido');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         //----Validación de Tipo Correo----//
         function validar_t_correo(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar tipo correo');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

        //----Validación de Tipo Contacto----//
         function validar_contacto(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar tipo contacto');
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
                 errorForm(idElemento, 'Seleccionar Estatus');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         //---- Valida Campos Solicitud ----//
         function validar_campos_correo(e) {
             event.preventDefault(e);
             validarMatricula('ContentPlaceHolder1_txt_matricula');
             validarcorreo('ContentPlaceHolder1_txt_correo');
             validar_t_correo('ContentPlaceHolder1_ddl_tipo_correo');            
             validar_estatus('ContentPlaceHolder1_ddl_estatus');
             validar_contacto('ContentPlaceHolder1_ddl_tcont');  
             return false;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="x_title">
        <h2>
            <img src="Images/Admisiones/correo-electronico.png" style="width: 30px;" /><small>Correo Electrónico Contacto</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_Campus" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_talco" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="grid_alumnos_bind"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Matrícula</label>
                            <asp:TextBox ID="txt_matricula"  runat="server" CssClass="form-control" OnTextChanged="txt_matricula_TextChanged" AutoPostBack="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Nombre</label>
                            <asp:TextBox ID="txt_nombre"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_contacto" class="form-label">Contacto</label>
                            <asp:DropDownList ID="ddl_tcont" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_tipo_correo" class="form-label">Tipo de Correo</label>
                            <asp:DropDownList ID="ddl_tipo_correo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_correo" class="form-label">Correo</label>
                            <asp:TextBox ID="txt_correo"  runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="text-align:center;">
                            <label for="ContentPlaceHolder1_adm_campus" class="form-label">Preferido</label>
                                    <div class="custom-control custom-switch">
                                        <input type="checkbox" class="custom-control-input" id="customSwitches" name="customSwitches">
                                        <label class="custom-control-label" for="customSwitches"></label>
                                        <asp:HiddenField ID="checked_input" runat="server" />
                                    </div>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
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
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcnco" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click"/>
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click"/>
                    </div>
                </div>
                <div id="table_talco">
                    <asp:GridView ID="GridCorreo" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridCorreo_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="ID_NUM" HeaderText="Id_Num">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                             <asp:BoundField DataField="TIPO_MAIL" HeaderText="T_correo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo Correo" />
                            <asp:BoundField DataField="CONSECUTIVO" HeaderText="Consecutivo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CORREO" HeaderText="Correo" />
                            <asp:BoundField DataField="PREFERIDO" HeaderText="Preferido" />
                            <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                            <asp:BoundField DataField="cl_contacto" HeaderText="Clave_contacto">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="contacto" HeaderText="Contacto" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function activar_check() {
            $("#customSwitches").attr('checked', true);
        }
        function desactivar_check() {
            $("#customSwitches").attr('checked', false);
        }

        function load_datatable() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridCorreo").DataTable({
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

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>

