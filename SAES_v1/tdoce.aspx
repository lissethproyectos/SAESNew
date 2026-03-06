<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tdoce.aspx.cs" Inherits="SAES_v1.tdoce" %>

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

        function error_update() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Actualiza Base de Datos</h2>'
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
                errorForm(idElemento, 'Ingresar Clave de Docente');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Tipo Direccion----//
        function validar_Categoria(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Categoria Docente');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Lada ----//
        function validarCarrera(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Carrera');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Estatus Carrera----//
        function validar_estatus(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar estatus Carrera');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarIdioma(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Idioma');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarPorcentajeIdioma(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar % Idioma');
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

        function validar_valor_entero(e) {
            event.preventDefault(e);
            validarEntero('ContentPlaceHolder1_txt_porcentaje');
            return false;
        }

        function validarEntero(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
            valor = parseInt(idElemento)

            //Compruebo si es un valor numérico
            if (isNaN(valor)) {
                //entonces (no es numero) devuelvo el valor cadena vacia
                errorForm(idElemento, 'Porcentaje es un valor numérico');
                return false
            } else {
                //En caso contrario (Si era un número) devuelvo el valor
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos Docente ----//
        function validar_campos_Docente(e) {
            event.preventDefault(e);
            validarMatricula('ContentPlaceHolder1_txt_matricula');
            validar_Categoria('ContentPlaceHolder1_ddl_categoria');
            validar_estatus('ContentPlaceHolder1_ddl_estatus');
            return false;
        }

        //---- Valida Campos Carrera ----//
        function validar_campos_Carrera(e) {
            event.preventDefault(e);
            validarMatricula('ContentPlaceHolder1_txt_matricula');
            validarMatricula('ContentPlaceHolder1_txt_carrera');
            validar_Categoria('ContentPlaceHolder1_ddl_estatus_carrera');
            return false;
        }

        //---- Valida Campos Carrera ----//
        function validar_campos_Idioma(e) {
            event.preventDefault(e);
            validarMatricula('ContentPlaceHolder1_txt_matricula');
            validarIdioma('ContentPlaceHolder1_txt_idioma');
            validarPorcentajeIdioma('ContentPlaceHolder1_txt_porcentaje');
            validarEntero('ContentPlaceHolder1_txt_porcentaje');
            return false;
        }



        //---- Clave Pais ----//
        function validarclavePais() {

            errorForm(idElemento, 'La clave ingresada ya existe');
            return false;

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-graduation-cap" aria-hidden="true"></i>&nbsp;Datos Académicos</h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBuscaCve" runat="server"
                    AssociatedUpdatePanelID="updPnlBuscaCve">
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
                <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Clave</label>
                <asp:UpdatePanel ID="updPnlBuscaCve" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" OnTextChanged="txt_matricula_TextChanged" AutoPostBack="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-10">
                <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Docente</label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <%--                                <asp:LinkButton ID="linkBttnAddDocente" CssClass="btn btn-success" runat="server" OnClick="linkBttnAddDocente_Click">Agregar</asp:LinkButton>--%>
                <%--<asp:ImageButton ID="Img1" runat="server" ImageUrl="Images/Operaciones/add.png" Height="30px" Width="30px"
                                ToolTip="Guardar Registro" Visible="true" OnClick="Agregar_Click" />--%>
            </div>

        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" Visible="True" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridDocentes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDocentes_SelectedIndexChanged" Visible="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <div class="card bg-light">
            <div class="card-header font-weight-bold">
                <h5><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;Calificación</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col text-center">
                        <asp:UpdateProgress ID="updPgrEdita1" runat="server"
                            AssociatedUpdatePanelID="UpdatePanel4">
                            <ProgressTemplate>
                                <asp:Image runat="server"
                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                    ImageUrl="~/Images/Sitemaster/loader.gif"
                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <label for="ContentPlaceHolder1_ddl_categoria" class="form-label">Categoría Docente</label>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_categoria" runat="server" CssClass="form-control" Enabled="False"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4">
                        <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control" Enabled="False">
                                        <asp:ListItem Value="0">-----</asp:ListItem>
                                        <asp:ListItem Value="A">Activo</asp:ListItem>
                                        <asp:ListItem Value="B">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                <asp:Button ID="btn_cancel_estatus" runat="server" CssClass="btn btn-secondary" Text="Cancelar" Visible="False" OnClick="btn_cancel_estatus_Click" />

                                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-success" Text="Actualizar" OnClick="btn_update_Click" />

                                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-success" Text="Guardar" Visible="False" OnClick="btn_save_Click" />

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <br />

        <div class="card bg-light">
            <div class="card-header font-weight-bold">
                <h5><i class="fa fa-university" aria-hidden="true"></i>&nbsp;Carrera(s)</h5>
            </div>
            <div class="card-body">
                <div class="form-row">
                    <div class="col-sm-6">
                        <label for="ContentPlaceHolder1_txt_carrera" class="form-label">Carrera</label>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_carrera" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqCarrera" runat="server" ErrorMessage="Ingresar Carrera" CssClass="text-danger" ValidationGroup="guardar_carrera" ControlToValidate="txt_carrera"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-3">
                        <label for="ContentPlaceHolder1_ddl_estatus_carrera" class="form-label">Estatus Carrera</label>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_estatus_carrera" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">----</asp:ListItem>
                                    <asp:ListItem Value="T">Titulado</asp:ListItem>
                                    <asp:ListItem Value="E">Egresado</asp:ListItem>
                                    <asp:ListItem Value="C">Carrera Trunca</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqEstatusCarrera" runat="server" ErrorMessage="Seleccionar estatus Carrera" CssClass="text-danger" ValidationGroup="guardar_carrera" ControlToValidate="ddl_estatus_carrera"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-3">
                        <label for="ContentPlaceHolder1_txt_cedula" class="form-label">Cédula Profesional</label>
                        <asp:UpdatePanel ID="updPnlAddCarrera" runat="server">
                            <ContentTemplate>
                                <div class="input-group">
                                    <asp:TextBox ID="txt_cedula" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCedula" runat="server" ErrorMessage="Ingresar Cédula" CssClass="text-danger" ValidationGroup="guardar_carrera" ControlToValidate="txt_cedula"></asp:RequiredFieldValidator>
                                    &nbsp;                           
                                    <asp:LinkButton ID="linkBttnAddCarreras" CssClass="btn btn-success" runat="server" OnClick="linkBttnAddCarreras_Click" ValidationGroup="guardar_carrera">Agregar</asp:LinkButton>
                                    <asp:CustomValidator ID="validaCarrera" runat="server" ErrorMessage="La carrera y cédula ingresada ya existe" ValidationGroup="guardar_carrera" CssClass="text-danger"></asp:CustomValidator>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <div class="row">
                    <div class="col text-center">
                        <asp:UpdateProgress ID="updPgrAddCarrera" runat="server"
                            AssociatedUpdatePanelID="updPnlAddCarrera">
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
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridCarreras" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridCatalogo_Carreras" EmptyDataText="No se encontraron carrera(s).">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Carrera" HeaderText="Carrera" />
                                        <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                                        <asp:BoundField DataField="c_estatus" HeaderText="Estatus_code">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cedula" HeaderText="Cédula Profesional" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha Registro" />
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
        <br />
        <div class="card bg-light">
            <div class="card-header font-weight-bold">
                <h5><i class="fa fa-globe" aria-hidden="true"></i>&nbsp;Idioma(s)</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-sm-4">
                        Idioma
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_idioma" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqIdioma" runat="server" ErrorMessage="Ingresar Idioma" CssClass="text-danger" ValidationGroup="guardar_idioma" ControlToValidate="txt_idioma"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-3">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                % Dominio Idioma
                                <div class="input-group">
                                    <asp:TextBox ID="txt_porcentaje" runat="server" CssClass="form-control"></asp:TextBox>&nbsp;
                                    <asp:LinkButton ID="linkBttnAddIdiomas" class="btn btn-success" runat="server" OnClick="linkBttnAddIdiomas_Click" ValidationGroup="guardar_idioma">Agregar</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="reqPorcIdioma" runat="server" ErrorMessage="Ingresar Porcentaje" CssClass="text-danger" ValidationGroup="guardar_idioma" ControlToValidate="txt_porcentaje"></asp:RequiredFieldValidator>
                                      <asp:regularExpressionValidator validationExpression="[0-9]*" ControlToValidate="txt_porcentaje" CssClass="text-danger" errorMessage="Ingresar un número" runat="server" ValidationGroup="guardar_idioma" /> 
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridIdiomas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="True" OnSelectedIndexChanged="GridCatalogo_Idiomas" EmptyDataText="No se encontraron idiomas." ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idioma" HeaderText="Idioma" />
                                        <asp:BoundField DataField="porcentaje" HeaderText="% Dominio" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha Registro" />
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
    <script>
        function load_datatable_Carreras() {
            $('#<%= GridCarreras.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridCarreras.ClientID %>').find("tr:first"))).DataTable({
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

        function load_datatable_Idiomas() {
            $('#<%= GridIdiomas.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridIdiomas.ClientID %>').find("tr:first"))).DataTable({
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
            $('#<%= GridDocentes.ClientID %>').Destroy();
            $('#<%= GridDocentes.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridDocentes.ClientID %>').find("tr:first"))).DataTable({
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


