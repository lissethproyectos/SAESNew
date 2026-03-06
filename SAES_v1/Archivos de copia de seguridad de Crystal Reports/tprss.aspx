<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tprss.aspx.cs" Inherits="SAES_v1.tprss" %>

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
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Clave duplicada</h2>Favor de validar'
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

        function valida_creditos(totCreditos) {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">No cumple con el total de créditos requeridos</h2>Créditos requeridos ' + totCreditos + "</br>Créditos cursados"
            })
        }

        function valida_promedio() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">No cumple con el promedio requerido</h2>Favor de validar en el listado.'
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
            <i class="fa fa-handshake-o" aria-hidden="true"></i>
            &nbsp;Programas Servicio Social
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <%--<div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBusca" runat="server"
                    AssociatedUpdatePanelID="updPnlBusca">
                    <progresstemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </progresstemplate>
                </asp:UpdateProgress>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-sm-2">
                Clave
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="txtClave" CssClass="form-control" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqClave" runat="server" ErrorMessage="Ingresar Clave" ControlToValidate="txtClave" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Descripción
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                                                         <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqDescripcion" runat="server" ErrorMessage="Ingresar Descripción" ControlToValidate="txtDescripcion" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Estatus
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                <asp:DropDownList runat="server" ID="ddlEstatus" CssClass="form-control">
                     <asp:ListItem Value="A">Activo</asp:ListItem>
                     <asp:ListItem Value="I">Ináctivo</asp:ListItem>
                 </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqEstatus" runat="server" ErrorMessage="Seleccionar Estatus" ControlToValidate="ddlEstatus" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
             </div>
        </div>
        <div class="row">
            <div class="col-sm-10">
                Empresa
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="txtEmpresa" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqEmpresa" runat="server" ErrorMessage="Ingresar Empresa" ControlToValidate="txtEmpresa" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                % Créditos cumplidos
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="txtCreditos" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqCreditos" runat="server" ErrorMessage="Ingresar Créditos" ControlToValidate="txtCreditos" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
         <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBotones" runat="server"
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
        <asp:UpdatePanel ID="updPnlBotones" runat="server">
            <ContentTemplate>
                <div class="form-row" id="btn_tprss" runat="server">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" Visible="False" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" ValidationGroup="guardar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" ValidationGroup="guardar" OnClick="btn_update_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtprss" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtprss_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tprss_clave" HeaderText="Clave" />
                                <asp:BoundField DataField="tprss_desc" HeaderText="Descripción" />
                                <asp:BoundField DataField="tprss_estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="tprss_empresa" HeaderText="Empresa" />
                                <asp:BoundField DataField="tprss_creditos" HeaderText="% Créditos" />
                                <asp:BoundField DataField="tprss_date" HeaderText="Fecha Registro" />
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
        function load_datatable() {
            $('#<%= Gridtprss.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtprss.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'Programas Servicio Social',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6]
                        }
                    },
                    {
                        title: 'Programas Servicio Social',
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
