<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="treti.aspx.cs" Inherits="SAES_v1.treti" %>

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

        function registro_duplicado() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Registro duplicado/h2>Favor de validar'
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

        function valida_creditos(totCreditos, totCursados) {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">No cumple con el total de créditos requeridos</h2>Créditos requeridos ' + totCreditos + "</br>Créditos cursados" + totCursados
            })
        }

        function valida_promedio(promedio_Requerido, promedio) {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">No cumple con el promedio requerido</h2>PromedioRequerido:' + promedio_Requerido + '<br/>Promedio del alumno:' + promedio
            })
        }

        function NoExiste() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Código Postal NO existe'
            })
        }

        function NoexisteAlumno() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
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
                errorForm(idElemento, 'Ingresar Matrícula');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Ciudad ----//
        function validarCiudad(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Ciudad');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----  Dirección ----//
        function validarDirección(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Dirección');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----  Zip ----//
        function validarZip(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Códgio Postal');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Tipo Direccion----//
        function validar_t_direccion(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Dirección');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de País----//
        function validar_pais(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar País');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Estado----//
        function validar_estado(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Estado');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Delegación----//
        function validar_delegacion(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Delegación');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Colonia----//
        function validar_colonia(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Colonia');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de tipo contacto----//
        function validar_contacto(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Contacto');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos Solicitud ----//
        function validar_campos_direccion(e) {
            event.preventDefault(e);
            validarMatricula('ContentPlaceHolder1_txt_matricula');
            validarCiudad('ContentPlaceHolder1_txt_ciudad');
            validarDirección('ContentPlaceHolder1_txt_direccion');
            validarZip('ContentPlaceHolder1_txt_zip');
            validar_t_direccion('ContentPlaceHolder1_ddl_tipo_direccion');
            validar_pais('ContentPlaceHolder1_ddl_pais');
            validar_estado('ContentPlaceHolder1_ddl_estado');
            validar_delegacion('ContentPlaceHolder1_ddl_delegacion');
            validar_colonia('ContentPlaceHolder1_ddl_colonia');
            validar_contacto('ContentPlaceHolder1_ddl_tcont')
            return false;
        }

        function validar_campos_tcopo(e) {
            event.preventDefault(e);
            validar_estado('ContentPlaceHolder1_ddl_estado');
            validar_delegacion('ContentPlaceHolder1_ddl_delegacion');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-graduation-cap" aria-hidden="true"></i>
            &nbsp;Registro a Titulación
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBusca" runat="server"
                    AssociatedUpdatePanelID="updPnlBusca">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrPrograma" runat="server"
                    AssociatedUpdatePanelID="updPnlPrograma">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrInicial" runat="server"
                    AssociatedUpdatePanelID="updPnlInicial">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrEstatus" runat="server"
                    AssociatedUpdatePanelID="updPnlEstatus">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGrid" runat="server"
                    AssociatedUpdatePanelID="updPnlGrid">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPrgBotones" runat="server"
                    AssociatedUpdatePanelID="updPnlBotones">
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
            <div class="col-sm-2">
                <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Si no recuerdas la matricula del alumno, dar click en el icono de la lupa."></i>&nbsp;
                Matrícula
                <asp:UpdatePanel ID="updPnlBusca" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="linkBttnBusca_Click"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Alumno
                <asp:UpdatePanel ID="updPnlNombre" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Campus
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_campus" CssClass="form-control" Enabled="false" AutoPostBack="True"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-6">
                Programa           
                <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hddnNivel" runat="server" />
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPrograma" runat="server" ErrorMessage="Favor de seleccionar un programa" ControlToValidate="ddl_programa" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-6">
                Nivel           
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_nivel" CssClass="form-control" Enabled="False"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Favor de seleccionar un nivel" ControlToValidate="ddl_nivel" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-6">
                Periodo
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" ErrorMessage="Favor de seleccionar periodo" ControlToValidate="ddl_periodo" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Opción de Titulación
                <asp:UpdatePanel ID="updPnlOpcTit" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_opc_titulacion" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqOpcTi" runat="server" ErrorMessage="Favor de seleccionar una opción de titulación" ControlToValidate="ddl_opc_titulacion" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Estatus           
                <asp:UpdatePanel ID="updPnlEstatus" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_estatus" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_estatus_SelectedIndexChanged">
                            <asp:ListItem>-------</asp:ListItem>
                            <asp:ListItem Value="I">Iniciado</asp:ListItem>
                            <asp:ListItem Value="R">Trámite</asp:ListItem>
                            <asp:ListItem Value="B">Baja</asp:ListItem>
                            <asp:ListItem Value="T">Terminado</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqEstatus" runat="server" ErrorMessage="Favor de seleccionar un estatus" ControlToValidate="ddl_estatus" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="tpers_clave" HeaderText="Matrícula" />
                                <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tpers_paterno" HeaderText="Apellido Paterno" />
                                <asp:BoundField DataField="tpers_materno" HeaderText="Apellido Materno" />
                                <asp:BoundField DataField="tpers_cgenero" HeaderText="C_Genero">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tpers_genero" HeaderText="Genero" />
                                <asp:BoundField DataField="tpers_civil" HeaderText="C_Civil">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tpers_ecivil" HeaderText="Estado Civil" />
                                <asp:BoundField DataField="tpers_curp" HeaderText="CURP" />
                                <asp:BoundField DataField="tpers_fecha" HeaderText="Fecha Nacimiento" />
                                <asp:BoundField DataField="tpers_usuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="tpers_fecha_reg" HeaderText="Fecha Registro" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel ID="updPnlInicial" runat="server">
            <ContentTemplate>
                <div class="form-row" id="rowInicial" runat="server" visible="false">
                    <div class="col-sm-3">
                        No. de Foja
                <asp:TextBox ID="txt_foja" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqFoja" runat="server" ErrorMessage="Favor de ingresar nombre" ControlToValidate="txt_foja" ValidationGroup="guardar" CssClass="text-danger" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                        Cédula Profesional
                <asp:TextBox ID="txt_cedula" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqCedula" runat="server" ErrorMessage="Favor de ingresar cédula" ControlToValidate="txt_cedula" ValidationGroup="guardar" CssClass="text-danger" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                        No. de Libro
                <asp:TextBox ID="txt_libro" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqLibro" runat="server" ErrorMessage="Favor de ingresar libro" ControlToValidate="txt_libro" ValidationGroup="guardar" CssClass="text-danger" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:UpdatePanel ID="updPnlBotones" runat="server">
            <ContentTemplate>
                <div class="form-row" id="btn_treti" runat="server">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btn_cancel_save" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_save_Click" />
                        <asp:Button ID="btn_cancel_update" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_update_Click" Visible="False" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" ValidationGroup="guardar" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" ValidationGroup="guardar" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtreti" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtreti_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="treti_ttiop_clave" HeaderText="Opción Tit." />
                                <asp:BoundField DataField="ttiop_desc" HeaderText="Descripción" />
                                <asp:BoundField DataField="treti_status_desc" HeaderText="Estatus" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="treti_tuser_clave" HeaderText="Usuario" />
                                <asp:BoundField DataField="treti_status" HeaderText="Estatus" />
                                <asp:BoundField DataField="treti_foja" HeaderText="Foja" />
                                <asp:BoundField DataField="treti_libro" HeaderText="Libro" />
                                <asp:BoundField DataField="treti_cedula" HeaderText="Cédula" />
                                <asp:BoundField DataField="treti_tpees_clave" HeaderText="Periodo" />
                            </Columns>
                            <RowStyle Font-Size="Small" />
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script>
        function load_datatable_alumnos() {
            $('#<%= GridAlumnos.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridAlumnos.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "language": {
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
                "scrollResize": true,
                "scrollY": '500px',
                "scrollCollapse": true,
                "order": [
                    [0, "asc"]
                ],
                "autoWidth": true,
                "dom": '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'SAES',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        }
                    },
                    {
                        title: 'Cat Alumnos',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6]
                        }
                    }
                ],
                "stateSave": true
            });
        }
        function load_datatable() {
            $('#<%= Gridtreti.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtreti.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "language": {
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
                "scrollResize": true,
                "scrollY": '500px',
                "scrollCollapse": true,
                "order": [
                    [0, "asc"]
                ],
                "autoWidth": true,
                "dom": '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'SAES',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        }
                    },
                    {
                        title: 'Cat Alumnos',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6]
                        }
                    }
                ],
                "stateSave": true
            });
        }

    </script>
</asp:Content>
