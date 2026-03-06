<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tdote.aspx.cs" Inherits="SAES_v1.tdote" %>

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
                errorForm(idElemento, 'Ingresar Matrícula');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Lada ----//
        function validarlada(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar clave lada');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Telefono ----//
        function validarTelefono(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Teléfono');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Tipo Direccion----//
        function validar_t_telefono(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar tipo Teléfono');
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

        //---- Valida Campos Solicitud ----//
        function validar_campos_telefono(e) {
            event.preventDefault(e);
            validarMatricula('ContentPlaceHolder1_txt_matricula');
            validarlada('ContentPlaceHolder1_txt_lada');
            validarTelefono('ContentPlaceHolder1_txt_telefono');
            validar_t_telefono('ContentPlaceHolder1_ddl_tipo_telefono');
            validar_estatus('ContentPlaceHolder1_ddl_estatus');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-phone" aria-hidden="true"></i>&nbsp;Teléfono
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <div class="container-fluid">
            <div class="form-row">             
                <div class="col-sm-2">
                    <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Clave</label>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <div class="input-group">
                                <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                                <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-6">
                    <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Docente</label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-4">
                    <label for="ContentPlaceHolder1_ddl_tipo_telefono" class="form-label">Tipo de Teléfono</label>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_tipo_telefono" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-row">
                <div class="col">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridDocentes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDocentes_SelectedIndexChanged" Visible="False" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
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
            <div class="form-row">
                <div class="col-sm-2">
                    <label for="ContentPlaceHolder1_txt_lada" class="form-label">Lada</label>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_lada" MaxLength="3" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_txt_telefono" class="form-label">Teléfono</label>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_telefono" MaxLength="12" runat="server" CssClass="form-control" ></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-2">
                    <label for="ContentPlaceHolder1_txt_extension" class="form-label">Extensión</label>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_extension" MaxLength="5" runat="server" CssClass="form-control" ></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-3">
                    <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>

            <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
        </div>


        <br />
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBotones" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel9">
                    <ProgressTemplate>
                        <asp:Image ID="img2" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <div class="row" id="btn_talte" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Visible="false" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridTelefono" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridTelefono_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID_NUM" HeaderText="Id_Num">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_TEL" HeaderText="T_telefono">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo Teléfono" />
                                <asp:BoundField DataField="CONSECUTIVO" HeaderText="Consecutivo">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LADA" HeaderText="Lada" />
                                <asp:BoundField DataField="TELEFONO" HeaderText="Teléfono" />
                                <asp:BoundField DataField="EXTENSION" HeaderText="Extensión" />
                                <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus_code">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
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
            $('#<%= GridTelefono.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridTelefono.ClientID %>').find("tr:first"))).DataTable({
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

