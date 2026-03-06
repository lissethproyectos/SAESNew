<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcodi.aspx.cs" Inherits="SAES_v1.tcodi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
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
            <i class="fa fa-address-card" aria-hidden="true"></i>
            &nbsp;Dirección Contactos
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
                <asp:UpdateProgress ID="updPgrContacto" runat="server"
                    AssociatedUpdatePanelID="updPnlContacto">
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
                <asp:UpdateProgress ID="updPgrTipoDir" runat="server"
                    AssociatedUpdatePanelID="updPnlTipoDir">
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
                Matrícula
                <asp:UpdatePanel ID="updPnlBusca" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="linkBttnBusca_Click"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-10">
                Alumno
                <asp:UpdatePanel ID="updPnlNombre" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
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
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <div id="datosContacto" runat="server">
                    <div class="form-row">
                        <div class="col-sm-3">

                            <asp:UpdatePanel ID="updPnlContacto" runat="server">
                                <ContentTemplate>
                                    <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Admisión/Contacto Estudiante/Datos Personales"></i>&nbsp;
                            Contacto
                         <asp:DropDownList ID="ddl_tcont" runat="server" CssClass="form-control"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-6">
                            <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Operación/Tipos/Dirección"></i>&nbsp;

                            Tipo de Dirección
                  <asp:UpdatePanel ID="updPnlTipoDir" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_tipo_direccion" runat="server" CssClass="form-control"></asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3">
                            Estatus
                  <asp:UpdatePanel ID="updPnlEstatus" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-6">
                            <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Operación/Demográficos/Paises"></i>&nbsp;

                            Pais
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddl_pais" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_pais_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-6">
                            <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Operación/Demográficos/Entidad Federativa"></i>&nbsp;

                            Estado
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddl_estado" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_estado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                        </div>

                    </div>

                    <div class="form-row">
                        <div class="col-sm-9">
                            <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Operación/Demográficos/Alcaldías Municipios"></i>&nbsp;

                            Delegación-Municipio
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddl_delegacion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_delegacion_SelectedIndexChanged"></asp:DropDownList>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Operación/Demográficos/Códigos Postales"></i>&nbsp;

                                    Codigo Postal
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_zip" MaxLength="5" runat="server" CssClass="form-control" OnTextChanged="txt_zip_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:LinkButton ID="linkBttnBuscarCP" CssClass="btn btn-success" runat="server" OnClick="linkBttnBuscarCP_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridCP" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridCP_SelectedIndexChanged" Visible="false" EmptyDataText="No se encontraron códigos postales" ShowHeaderWhenEmpty="True">
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
                                            <asp:BoundField DataField="Clave" HeaderText="Código Postal" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Colonia" />
                                            <asp:BoundField DataField="TESTA" HeaderText="Estado">
                                                <HeaderStyle CssClass="ocultar" />
                                                <ItemStyle CssClass="ocultar" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TDELE" HeaderText="Delegacion">
                                                <HeaderStyle CssClass="ocultar" />
                                                <ItemStyle CssClass="ocultar" />
                                            </asp:BoundField>
                                        </Columns>
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-4">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    Colonia
            <asp:DropDownList ID="ddl_colonia" runat="server" CssClass="form-control"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    Ciudad
            <asp:TextBox ID="txt_ciudad" runat="server" CssClass="form-control"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            Calle
                  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                      <ContentTemplate>
                          <asp:TextBox ID="txt_direccion" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_direccion_TextChanged"></asp:TextBox>
                          <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
                          <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
                      </ContentTemplate>
                  </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <div class="form-row" id="btn_tcodi" runat="server">
                                <div class="col-sm-12 text-center">
                                    <asp:Button ID="btn_cancel_update" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_update_Click" Visible="False" />
                                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridDireccion" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDireccion_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontro datos de contacto de este alumno.">
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
                                <asp:BoundField DataField="ID_NUM" HeaderText="Id_Num">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_DIR" HeaderText="T_direccion">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo Dirección" />
                                <asp:BoundField DataField="CONSECUTIVO" HeaderText="Consecutivo" />
                                <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" />
                                <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus_code">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                                <asp:BoundField DataField="tcodi_tpais_clave" HeaderText="C_Pais">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcodi_testa_clave" HeaderText="C_Estado">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcodi_tdele_clave" HeaderText="C_Dele">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcodi_tcopo_clave" HeaderText="Zip">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcodi_colonia" HeaderText="Colonia">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcodi_ciudad" HeaderText="Ciudad">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cl_contacto" HeaderText="Clave_contacto" />
                                <asp:BoundField DataField="contacto" HeaderText="Contacto" />
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
            let table_solicitudes = $("#ContentPlaceHolder1_GridDireccion").DataTable({
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
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function load_datatable_Alumnos() {
            $('#<%= GridAlumnos.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridAlumnos.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function load_datatable_CP() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridCP").DataTable({
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
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>

