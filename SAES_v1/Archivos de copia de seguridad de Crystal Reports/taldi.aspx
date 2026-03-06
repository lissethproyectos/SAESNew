<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="taldi.aspx.cs" Inherits="SAES_v1.taldi" %>

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
            <i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Dirección
        </h2>
        <div class="clearfix"></div>
    </div>

    <div id="container-fluid">
        <div class="form-row">
            <div class="col-sm-2">
                Matrícula
                                    <asp:UpdatePanel ID="updPnlMatricula" runat="server">
                                        <ContentTemplate>
                                            <div class="input-group">
                                                <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="linkBttnBusca_Click"></asp:TextBox>
                                                <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                <!--Configurar BackEnd la longitud de la BD-->
            </div>
            <div class="col-sm-5">
                Nombre
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Tipo de Dirección
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_tipo_direccion" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqTipoDir" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Tipo de Dirección" ControlToValidate="ddl_tipo_direccion" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Estatus
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqEstatus" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Estatus" ControlToValidate="ddl_estatus" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>

                                </ContentTemplate>
                            </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tpers_clave" HeaderText="Matrícula" />
                                <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tpers_paterno" HeaderText="Apellido Paterno" />
                                <asp:BoundField DataField="tpers_materno" HeaderText="Apellido Materno" />
                                <asp:BoundField DataField="tpers_num" HeaderText="pidm">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
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
        <div class="form-row">
            <div class="col-md-6">
                Pais
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_pais" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_pais_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqPais" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Pais" ControlToValidate="ddl_pais" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="valPaisCP" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Pais" ControlToValidate="ddl_pais" ValidationGroup="busca_cp" InitialValue="0"></asp:RequiredFieldValidator>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                Estado
                               <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                   <ContentTemplate>
                                       <asp:DropDownList ID="ddl_estado" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_estado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="reqEstado" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Estado" ControlToValidate="ddl_estado" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>

                                   </ContentTemplate>
                               </asp:UpdatePanel>
            </div>
            </div>
            
            
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlCP" runat="server">
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-9">
                Delegación-Municipio
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_delegacion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_delegacion_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqDelegacion" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Delegación" ControlToValidate="ddl_delegacion" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Código Postal
                               <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                   <ContentTemplate>
                                       <div class="input-group">
                                           <asp:TextBox ID="txt_zip" MaxLength="5" runat="server" CssClass="form-control" OnTextChanged="txt_zip_TextChanged" AutoPostBack="true"></asp:TextBox>
                                           <asp:LinkButton ID="linkBttnBuscaCP" class="btn btn-success" runat="server" ValidationGroup="busca_cp" OnClick="linkBttnBuscaCP_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                       </div>
                                       <asp:RequiredFieldValidator ID="reqCP" runat="server" CssClass="text-danger" ErrorMessage="Agregar CP" ControlToValidate="txt_zip" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
            </div>
            </div>
         <div class="form-row">
            <div class="col-sm-4">
                Colonia
                              <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                  <ContentTemplate>
                                      <asp:DropDownList ID="ddl_colonia" runat="server" CssClass="form-control"></asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="reqColonia" runat="server" CssClass="text-danger" ErrorMessage="Seleccionar Colonia" ControlToValidate="ddl_colonia" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>

                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Ciudad
                              <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                  <ContentTemplate>
                                      <asp:TextBox ID="txt_ciudad" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_ciudad_TextChanged"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="reqCiudad" runat="server" CssClass="text-danger" ErrorMessage="Agregar Ciudad" ControlToValidate="txt_ciudad" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>       
            <div class="col-sm-4">
                Calle
                               <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                   <ContentTemplate>
                                       <asp:TextBox ID="txt_direccion" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="reqDireccion" runat="server" CssClass="text-danger" ErrorMessage="Agregar Dirección" ControlToValidate="txt_direccion" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
            </div>
        </div>
         <div class="form-row">
            <div class="col">
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
                </div>
             </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridCP" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridCP_SelectedIndexChanged" Visible="false" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron códigos postales.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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

        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
                <div class="form-row" id="btn_taldi" runat="server">
                    <div class="col text-center">
                        <asp:LinkButton ID="btn_cancelar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="btn_cancelar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="btn_cancelar_update" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="btn_cancelar_update_Click">Cancelar</asp:LinkButton>

                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" ValidationGroup="guardar" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" ValidationGroup="guardar" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridDireccion" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDireccion_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontro ninguna dirección de este alumno.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID_NUM" HeaderText="Id_Num">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLAVE" HeaderText="Matrícula" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
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
                                <asp:BoundField DataField="taldi_tpais_clave" HeaderText="C_Pais">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="taldi_testa_clave" HeaderText="C_Estado">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="taldi_tdele_clave" HeaderText="C_Dele">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="taldi_tcopo_clave" HeaderText="Zip">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="taldi_colonia" HeaderText="Colonia">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="taldi_ciudad" HeaderText="Ciudad">
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
    </div>
    <script>
        function load_datatable() {
            $('#<%= GridDireccion.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridDireccion.ClientID %>').find("tr:first"))).DataTable({

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
