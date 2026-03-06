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
            <i class="fa fa-cog" aria-hidden="true"></i>&nbsp;Grupos Materia</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <div class="form-row">
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_ddl_periodo" class="form-label">Periodo</label>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" ErrorMessage="Seleccionar Periodo" ControlToValidate="ddl_periodo" CssClass="text-danger" InitialValue="0" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_ddl_turno" class="form-label">Turno</label>
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_turno" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqTurno" runat="server" ErrorMessage="Seleccionar Turno" ControlToValidate="ddl_turno" CssClass="text-danger" InitialValue="0" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_ddl_campus" class="form-label">Campus</label>
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" Font-Size="Small" OnSelectedIndexChanged="Carga_Programa" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqCampus" runat="server" ErrorMessage="Seleccionar Campus" ControlToValidate="ddl_campus" CssClass="text-danger" InitialValue="0" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_ddl_programa" class="form-label">Programa</label>
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" OnSelectedIndexChanged="Carga_Perprog" AutoPostBack="true">
                            <asp:ListItem Value="0">---------</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPrograma" runat="server" ErrorMessage="Seleccionar Programa" ControlToValidate="ddl_programa" CssClass="text-danger" InitialValue="0" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_ddl_per_prog" class="form-label">Periodo Programa</label>
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_per_prog" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">---------</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPeriodo_Programa" runat="server" ErrorMessage="Seleccionar Periodo Programa" ControlToValidate="ddl_per_prog" CssClass="text-danger" InitialValue="0" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <%-- <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda" OnClick="grid_materias_bind" VISIBLE="true" />
                        </div>--%>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_materia" class="form-label">Clave</label>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_materia" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Busca_Materia"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="reqMateria" runat="server" ErrorMessage="Ingresar Materia" ControlToValidate="txt_materia" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-9">
                <label for="ContentPlaceHolder1_txt_nombre_materia" class="form-label">Materia</label>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre_materia" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="reqNomMateria" runat="server" ErrorMessage="Ingresar Nombre Materia" ControlToValidate="txt_nombre_materia" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-1">
                <label for="ContentPlaceHolder1_txt_grupo" class="form-label">Grupo</label>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_grupo" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="reqGrupo" runat="server" ErrorMessage="Ingresar Grupo" ControlToValidate="txt_grupo" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPnlBuscaMateria" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel7">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <div class="accordion" id="accordionGrid">
                    <div class="card">
                        <div id="collapseGrid" class="collapse" data-parent="#accordionGrid">
                            <div class="card-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="Gridtmate" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtmate_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                                                    <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                                <asp:BoundField DataField="MATERIA" HeaderText="Materia" />
                                                <asp:BoundField DataField="CRED" HeaderText="Créditos" />
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
            </div>
        </div>


        <div class="form-row">
            <%--<div class="col-smd-0.4">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                ToolTip="Búsqueda" OnClick="grid_salones_bind" Visible="true" />
                        </div>--%>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_salon" class="form-label">Clave</label>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_salon" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Busca_Salon"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBuscaSalon" class="btn btn-success" runat="server" OnClick="linkBttnBuscaSalon_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="reqSalon" runat="server" ErrorMessage="Ingresar Salón" ControlToValidate="txt_salon" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_nombre_salon" class="form-label">Salón</label>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre_salon" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="reqSalonNombre" runat="server" ErrorMessage="Ingresar Nombre Salón" ControlToValidate="txt_nombre_salon" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="reqStatus" runat="server" ErrorMessage="Seleccionar Status" ControlToValidate="ddl_estatus" CssClass="text-danger" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_capacidad" class="form-label">Capacidad</label>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_capacidad" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_inscritos" class="form-label">Inscritos</label>
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_inscritos" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_disponibles" class="form-label">Disponibilidad</label>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_disponibles" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <div class="accordion" id="accordionGridTsalo">
                    <div class="card">
                        <div id="collapseGridTsalo" class="collapse" data-parent="#accordionGridTsalo">
                            <div class="card-body">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="Gridtsalo" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" OnSelectedIndexChanged="Gridtsalo_SelectedIndexChanged" Visible="True">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                                                    <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                                <asp:BoundField DataField="SALON" HeaderText="Salón" />
                                                <asp:BoundField DataField="MINIMO" HeaderText="Mínimo" />
                                                <asp:BoundField DataField="MAXIMO" HeaderText="Máximo" />
                                                <asp:BoundField DataField="C_TIPO" HeaderText="c_tipo">
                                                    <HeaderStyle CssClass="ocultar" />
                                                    <ItemStyle CssClass="ocultar" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
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
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
                <div id="btn_tgrup" runat="server" class="form-row">
                    <div class="col text-center">
                        <asp:Button ID="cancel_tgrup" runat="server" CssClass="btn btn-round btn-secondary" OnClick="cancel_tgrup_Click" Text="Cancelar" />
                        <asp:Button ID="save_tgrup" runat="server" CssClass="btn btn-round btn-success" OnClick="save_tgrup_Click" OnClientClick="destroy_table();" Text="Agregar" ValidationGroup="guardar" />
                        <asp:Button ID="search_tgrup" runat="server" CssClass="btn btn-round btn-success" OnClick="grid_grupos_bind" OnClientClick="destroy_table();" Text="Consultar" />
                        <asp:Button ID="update_tgrup" runat="server" CssClass="btn btn-round btn-success" OnClick="update_tgrup_Click" OnClientClick="destroy_table();" Text="Actualizar" Visible="false" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtgrup" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnSelectedIndexChanged="Gridtgrup_SelectedIndexChanged"  Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron grupos.">
                            <Columns>
                                <asp:ButtonField ButtonType="image" CommandName="select" ControlStyle-Height="24px" ControlStyle-Width="24px" HeaderText="Seleccionar" ImageUrl="~/Images/Generales/hacer-clic.png" ItemStyle-CssClass="button_select" />
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                <asp:BoundField DataField="MATERIA" HeaderText="Materia" />
                                <asp:BoundField DataField="GRUPO" HeaderText="Grupo" />
                                <asp:BoundField DataField="SALON" HeaderText="Salón" />
                                <asp:BoundField DataField="NOMBRE_SALON" HeaderText="Nombre Salón">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CAPACIDAD" HeaderText="Capacidad" />
                                <asp:BoundField DataField="INSCRITOS" HeaderText="Inscritos" />
                                <asp:BoundField DataField="DISPONIBLE" HeaderText="Disponible" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
    <script>
        function load_datatable() {
            $('#<%= Gridtgrup.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtgrup.ClientID %>').find("tr:first"))).DataTable({
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
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
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
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    }
                ]
            });
        }

        function load_datatable_materias() {
            $('#<%= Gridtmate.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtmate.ClientID %>').find("tr:first"))).DataTable({
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
            $('#<%= Gridtsalo.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtsalo.ClientID %>').find("tr:first"))).DataTable({
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

