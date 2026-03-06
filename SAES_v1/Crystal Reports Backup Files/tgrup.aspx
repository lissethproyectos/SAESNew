<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tgrup.aspx.cs" Inherits="SAES_v1.tgrup" %>

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

        function Exist() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Grupo Materia Existente !!</h2>Favor de validar en el listado.'
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
            $("#demograficos").addClass("current-page");
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


        //----Validación de Periodo----//
        function validar_periodo(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar periodo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Turno----//
        function validar_turno(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar turno');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Campus----//
        function validar_campus(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

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

        function validar_materia(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Capturar Materia');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function Noexiste_materia(idEl) {

            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'No existe Materia');
                return false;
            } else {
                validadoForm(idElemento);
            }
          
        }

        function validar_grupo(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Capturar Grupo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validar_salon(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Capturar Salón');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

      
        //---- Valida fomulario completo ----//
        function validar_tgrup(e) {
            event.preventDefault(e);
            validar_periodo('ContentPlaceHolder1_ddl_periodo');
            validar_turno('ContentPlaceHolder1_ddl_turno');
            validar_campus('ContentPlaceHolder1_ddl_campus');
            return false;
        }

        //---- Valida fomulario completo ----//
        function mivalidacion(e) {
            event.preventDefault(e);
            validar_periodo('ContentPlaceHolder1_ddl_periodo');
            validar_turno('ContentPlaceHolder1_ddl_turno');
            validar_campus('ContentPlaceHolder1_ddl_campus');
            validar_materia('ContentPlaceHolder1_txt_materia');
            validar_grupo('ContentPlaceHolder1_txt_grupo');
            validar_salon('ContentPlaceHolder1_txt_salon');
            validar_estatus('ContentPlaceHolder1_ddl_estatus');
            return false;
        }

         //---- Valida fomulario completo ----//
        function nomateria(e) {
            event.preventDefault(e);
            Noexiste_materia('ContentPlaceHolder1_txt_materia');
            return false;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/grupo.jpg" style="width: 30px;" /><small>Grupos Materia</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_zip" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_periodo" class="form-label">Periodo</label>
                            <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_turno" class="form-label">Turno</label>
                            <asp:DropDownList ID="ddl_turno" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" Font-Size="Small" OnSelectedIndexChanged="Carga_Programa" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-2">
                            ------------------------
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_programa" class="form-label">Programa</label>
                            <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" Font-Size="Small" OnSelectedIndexChanged="Carga_Perprog" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_ddl_per_prog" class="form-label">Periodo Programa</label>
                            <asp:DropDownList ID="ddl_per_prog" runat="server" CssClass="form-control" Font-Size="Small" ></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            ------------------------
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda" OnClick="grid_materias_bind" VISIBLE="true" />
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_materia" class="form-label">Clave</label>
                            <asp:TextBox ID="txt_materia" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Busca_Materia"></asp:TextBox>
                        </div>
                        <div class="col-md-7">
                            <label for="ContentPlaceHolder1_txt_nombre_materia" class="form-label">Materia</label>
                            <asp:TextBox ID="txt_nombre_materia" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <label for="ContentPlaceHolder1_txt_grupo" class="form-label">Grupo</label>
                            <asp:TextBox ID="txt_grupo" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                    <div id="tabla_tmate">
                    <asp:GridView ID="Gridtmate" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtmate_SelectedIndexChanged" visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="MATERIA" HeaderText="Materia" />
                            <asp:BoundField DataField="CRED" HeaderText="Créditos"/>
                            

                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                     <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda" OnClick="grid_salones_bind" VISIBLE="true" />
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_salon" class="form-label">Clave</label>
                            <asp:TextBox ID="txt_salon" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Busca_Salon"></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_txt_nombre_salon" class="form-label">Salón</label>
                            <asp:TextBox ID="txt_nombre_salon" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                         <div class="col-md-2">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control" Font-Size="Small" ></asp:DropDownList>
                        </div>
                    </div>
                                    <div id="tabla_tsalo">
                    <asp:GridView ID="Gridtsalo" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtsalo_SelectedIndexChanged" visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="SALON" HeaderText="Salón" />
                            <asp:BoundField DataField="MINIMO" HeaderText="Mínimo"/>
                            <asp:BoundField DataField="MAXIMO" HeaderText="Máximo"/>
                             <asp:BoundField DataField="C_TIPO" HeaderText="c_tipo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_capacidad" class="form-label">Capacidad</label>
                            <asp:TextBox ID="txt_capacidad" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_inscritos" class="form-label">Inscritos</label>
                            <asp:TextBox ID="txt_inscritos" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_disponibles" class="form-label">Disponibilidad</label>
                            <asp:TextBox ID="txt_disponibles" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>


                </br>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tgrup" runat="server">
                    <div class="col-md-6" style="text-align: center">
                        <asp:Button ID="cancel_tgrup" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancel_tgrup_Click"/>
                        <asp:Button ID="save_tgrup" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClientClick="destroy_table();" OnClick="save_tgrup_Click"/>
                        <asp:Button ID="search_tgrup" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" OnClientClick="destroy_table();" OnClick="grid_grupos_bind"/>
                        <asp:Button ID="update_tgrup" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClientClick="destroy_table();" OnClick="update_tgrup_Click"/>
                    </div>
                </div>
            </div>
                <div id="tabla_tgrup">
                    <asp:GridView ID="Gridtgrup" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtgrup_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="MATERIA" HeaderText="Materia" />
                            <asp:BoundField DataField="GRUPO" HeaderText="Grupo"/>
                            <asp:BoundField DataField="SALON" HeaderText="Salón" />
                            <asp:BoundField DataField="NOMBRE_SALON" HeaderText="Nombre Salón">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CAPACIDAD" HeaderText="Capacidad"/>
                            <asp:BoundField DataField="INSCRITOS" HeaderText="Inscritos" />
                            <asp:BoundField DataField="DISPONIBLE" HeaderText="Disponible"/>
                            <asp:BoundField DataField="C_ESTATUS" HeaderText="c_estatus">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
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
            let table_z = $("#ContentPlaceHolder1_Gridtgrup").DataTable({
                language: {
                    sProcessing: 'Procesando...',
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
                }, scrollY: '500px',
                scrollCollapse: true,
                order: [
                    [2, "asc"]
                ],
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'SAES_Catálogo de Grupos Materia',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5,6, 7, 8,9]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Grupos Materia',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5,6, 7, 8,9 ]
                        }
                    }
                ]
            });
        }

        function load_datatable_materias() {
            let table_z = $("#ContentPlaceHolder1_Gridtmate").DataTable({
                language: {
                    sProcessing: 'Procesando...',
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
                }, scrollY: '500px',
                scrollCollapse: true,
                order: [
                    [2, "asc"]
                ],
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                   
                ]
            });
        }

        function load_datatable_salones() {
            let table_z = $("#ContentPlaceHolder1_Gridtsalo").DataTable({
                language: {
                    sProcessing: 'Procesando...',
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
                }, scrollY: '500px',
                scrollCollapse: true,
                order: [
                    [2, "asc"]
                ],
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                   
                ]
            });
        }

        function destroy_table() {
            $("#ContentPlaceHolder1_Gridtgrup").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>

