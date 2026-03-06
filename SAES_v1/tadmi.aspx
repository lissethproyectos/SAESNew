<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tadmi.aspx.cs" Inherits="SAES_v1.tadmi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function NoexisteAlumno() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Matrícula NO existe'
            })
        }

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

        //----Validación de Turno----//
        function validar_turno(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Turno');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Periodo----//
        function validar_periodo(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Periodo');
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
                errorForm(idElemento, 'Seleccionar Campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Validación de Programa----//
        function validar_programa(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Programa');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Tipo Ingreso----//
        function validar_t_ingreso(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar tipo ingreso');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Tasa Financiera----//
        function validar_t_financiera(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Tasa financiera');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarEnteroEnCampo(idEl) {
            const idElemento = idEl;
            let field = document.getElementById(idElemento);
            let valueInt = parseInt(field.value);
            if (!Number.isInteger(valueInt)) {
                errorForm(idElemento, 'Promedio es un valor numérico');
                return false;
            } else {
                field.value = valueInt;
                return true;
            }
        }

        function validar_escuela(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Escuela');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validar_promedio(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Promedio');
                return false;
            } else {
                validarEntero('ContentPlaceHolder1_txt_promedio');
                validadoForm(idElemento);
            }
        }

        function validarEntero(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
            valor = parseInt(idElemento)

            //Compruebo si es un valor numérico
            if (isNaN(valor)) {
                //entonces (no es numero) devuelvo el valor cadena vacia
                errorForm(idElemento, 'Promedio es un valor numérico');
                return false
            } else {
                //En caso contrario (Si era un número) devuelvo el valor
                validadoForm(idElemento);
            }
        }



        //---- Valida Campos Solicitud ----//
        function validar_campos_solicitud(e) {
            event.preventDefault(e);
            validarMatricula('ContentPlaceHolder1_txt_matricula');
            validar_turno('ContentPlaceHolder1_ddl_turno');
            validar_periodo('ContentPlaceHolder1_ddl_periodo');
            validar_campus('ContentPlaceHolder1_ddl_Campus');
            validar_programa('ContentPlaceHolder1_ddl_Programa');
            validar_t_ingreso('ContentPlaceHolder1_ddl_tipo_ingreso');
            validar_t_financiera('ContentPlaceHolder1_ddl_tasa_f');
            validar_escuela('ContentPlaceHolder1_txt_escuela_pro');
            validar_promedio('ContentPlaceHolder1_txt_promedio');
            validarEnteroEnCampo('ContentPlaceHolder1_txt_promedio');
            return false;
        }

        //---- Valida Campos Solicitud ----//
        function validar_valor_entero(e) {
            event.preventDefault(e);
            validarEntero('ContentPlaceHolder1_txt_promedio');
            return false;
        }

    </script>
    <style>
        .buscar {
            margin-bottom: 20px;
            margin-top: 26px;
        }

        .icon_simula {
            width: 100%;
            text-align: center;
            border-color: #FFF !important;
            color: #6c757d !important;
        }

            .icon_simula:hover {
                background-color: #fff !important;
                color: #26b99a;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-file-text" aria-hidden="true"></i>&nbsp;Solicitud
        </h2>
        <div class="clearfix"></div>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-2">
                Matrícula
                                             <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                                 <ContentTemplate>
                                                     <div class="input-group">
                                                         <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="linkBttnBusca_Click"></asp:TextBox>
                                                         <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                                     </div>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
            </div>
            <div class="col-sm-10">
                Nombre
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
               <div class="col text-center">
                <asp:UpdateProgress ID="updPgrAlumnos" runat="server"
                    AssociatedUpdatePanelID="updPnlAlumnos">
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
            <div class="col">
                <asp:UpdatePanel ID="updPnlAlumnos" runat="server">
                    <ContentTemplate>
                        <div id="table_campus">
                            <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false">
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
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                Turno
                              <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                  <ContentTemplate>
                                      <asp:DropDownList ID="ddl_turno" runat="server" CssClass="form-control"></asp:DropDownList>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>

            <div class="col-sm-3">
                Periodo
                             <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                 <ContentTemplate>
                                     <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control"></asp:DropDownList>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Campus
                             <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                 <ContentTemplate>
                                     <asp:DropDownList ID="ddl_Campus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_Campus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>
            <div class="col-md-3">
                Tipo de ingreso
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_tipo_ingreso" runat="server" CssClass="form-control"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                Tasa Financiera
                              <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                  <ContentTemplate>
                                      <asp:DropDownList ID="ddl_tasa_f" runat="server" CssClass="form-control"></asp:DropDownList>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>
            <div class="col-sm-6">
                Programa
                              <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                  <ContentTemplate>
                                      <asp:DropDownList ID="ddl_Programa" runat="server" CssClass="form-control"></asp:DropDownList>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>
            <div class="col-md-3">
                Estatus
                              <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                  <ContentTemplate>
                                      <asp:TextBox ID="txt_estatus_sol" runat="server" CssClass="form-control" Visible="true" Text="Iniciada" ReadOnly="true"></asp:TextBox>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-2">
                Escuela
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="input-group">
                                    <asp:TextBox ID="txt_escuela_pro" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:LinkButton ID="linkBttnBuscaEscuela" class="btn btn-success" runat="server" OnClick="linkBttnBuscaEscuela_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
            </div>
            <div class="col-sm-6">
                Procedencia
                  <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                      <ContentTemplate>
                          <asp:TextBox ID="txt_nom_esc" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <div class="col-md-2">
                Promedio
                  <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                      <ContentTemplate>
                          <asp:TextBox ID="txt_promedio" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_promedio_TextChanged"></asp:TextBox>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
        </div>
          <div class="row">
               <div class="col text-center">
                <asp:UpdateProgress ID="updPgrEscuelas" runat="server"
                    AssociatedUpdatePanelID="updPnlEscuelas">
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
            <div class="col">
                <asp:UpdatePanel ID="updPnlEscuelas" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridEscuelas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridEscuelas_SelectedIndexChanged" Visible="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="clave" HeaderText="Clave" />
                                <asp:BoundField DataField="descripcion" HeaderText="Escuela" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>

         <div class="row">
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
                <div class="row" id="btn_tadmi" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Modificar" Visible="false" OnClick="btn_update_Click" />
                        <asp:LinkButton ID="simualdor" runat="server" CssClass="btn btn-round btn-success" OnClick="simulador_Click">Simulador</asp:LinkButton>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridSolicitud" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridSolicitud_SelectedIndexChanged">
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
                                <asp:BoundField DataField="PERIODO" HeaderText="Periodo" />
                                <asp:BoundField DataField="CONSECUTIVO" HeaderText="Consecutivo">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TURNO" HeaderText="Turno" />
                                <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                                <asp:BoundField DataField="PROGRAMA" HeaderText="Programa" />
                                <asp:BoundField DataField="TIIN" HeaderText="Tipo de Ingreso" />
                                <asp:BoundField DataField="TASA" HeaderText="Tasa Financiera" />
                                <asp:BoundField DataField="E_PRO" HeaderText="E_PRO">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PROMEDIO" HeaderText="Promedio" />
                                <asp:BoundField DataField="C_ESTATUS" HeaderText="C_ESTATUS">
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
            let table_solicitudes = $("#ContentPlaceHolder1_GridSolicitud").DataTable({
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

        function load_datatable_Escuelas() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridEscuelas").DataTable({
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
