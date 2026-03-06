<%@ Page Title="Plan de Estudios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tplan.aspx.cs" Inherits="SAES_v1.tplan" %>

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
            $("#campus").addClass("current-page");
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

        //---- Clave Programa ----//
        function validarPrograma(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresar Programa');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Clave Materia ----//
        function validarMateria(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresar Materia');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Clave Area ----//
        function validarArea(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresar Area');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Clave Ciclo ----//
        function validarCiclo(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresar Ciclo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Periodo cursa----//
        function validarPeriodo(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresar Periodo cursa');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Consecutivo----//
        function validarConsecutivo(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresar Consecutivo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Validación de Tipo materia----//
        function validarTipo(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Tipo Materia');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Clave Programa No valida----//
        function validarclavePrograma_N(idEl, indicador) {
            const idElemento = idEl;
            if (indicador == 1) {
                errorForm(idElemento, 'Ingresa clave valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Seleccionar Programa----//
        function validarPrograma_prog(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Programa');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function noexist() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">No existen datos para mostrar</h2>Favor de validar en el catálogo.'
            })
        }

        //---- Valida Campos Campus ----//
        function validar(e) {
            event.preventDefault(e);
            validarPrograma('ContentPlaceHolder1_txt_tprog');
            validarMateria('ContentPlaceHolder1_txt_tmate');
            validarArea('ContentPlaceHolder1_txt_tarea');
            validarCiclo('ContentPlaceHolder1_txt_tpees');
            validarPeriodo('ContentPlaceHolder1_txt_periodo');
            validarConsecutivo('ContentPlaceHolder1_txt_consecutivo')
            validarTipo('ContentPlaceHolder1_ddl_tipo');
            return false;
        }

        function validarEntero_consecutivo(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
            valor = parseInt(idElemento)

            //Compruebo si es un valor numérico
            if (isNaN(valor)) {
                //entonces (no es numero) devuelvo el valor cadena vacia
                errorForm(idElemento, 'Consecutivo es valor numérico');
                return false
            } else {
                //En caso contrario (Si era un número) devuelvo el valor
                validadoForm(idElemento);
            }
        }

        function validarEntero_periodo(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
            valor = parseInt(idElemento)

            //Compruebo si es un valor numérico
            if (isNaN(valor)) {
                //entonces (no es numero) devuelvo el valor cadena vacia
                errorForm(idElemento, 'Periodo cursa es valor numérico');
                return false
            } else {
                //En caso contrario (Si era un número) devuelvo el valor
                validadoForm(idElemento);
            }
        }

        function valida_consecutivo(e) {
            validarEntero_consecutivo('ContentPlaceHolder1_txt_consecutivo');
        }

        function valida_periodo(e) {
            validarEntero_periodo('ContentPlaceHolder1_txt_periodo');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <div class="x_title">
        <h2>
            <img src="Images/Operaciones/plan.png" style="width: 30px;" /><small>Plan de Estudios</small></h2>
        <div class="clearfix"></div>
    </div>--%>

    <div class="x_title">
        <h2>
            <i class="fa fa-graduation-cap" aria-hidden="true"></i>&nbsp;Plan de Estudios
        </h2>
        <div class="clearfix"></div>
    </div>
    
    <div class="x_content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-row">
                    <%-- <div class="col-md-0.4" >
                                 <asp:ImageButton ID="Programa" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="30px" Width="30px"
                                 TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="Busqueda_Programa"/>
                            </div>--%>
                    <div class="col-sm-2">
                        <label for="ContentPlaceHolder1_c_prog_campus" class="form-label">Clave</label>
                        <asp:UpdatePanel ID="updPnlBusca" runat="server">
                            <ContentTemplate>
                                <div class="input-group">
                                    <asp:TextBox ID="txt_tprog" runat="server" CssClass="form-control" OnTextChanged="tprog_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-10">
                        <label for="ContentPlaceHolder1_txt_nom_prog" class="form-label">Programa</label>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_nom_prog" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-row">
                            <%--  <div class="col-md-0.4">
                                    <asp:ImageButton ID="Materia" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="30px" Width="30px"
                                        ToolTip="Búsqueda" Visible="true" OnClick="Busqueda_Materia" />
                                </div>--%>
                            <div class="col-sm-2">
                                <label for="ContentPlaceHolder1_txt_tmate" class="form-label">Clave</label>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_tmate" runat="server" CssClass="form-control" OnTextChanged="tmate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:LinkButton ID="linkBttnBuscarMateria" class="btn btn-success" runat="server" OnClick="linkBttnBuscarMateria_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-10">
                                <label for="ContentPlaceHolder1_txt_nom_mate" class="form-label">Materia</label>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_nom_mate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="form-row">
                            <%-- <div class="col-md-0.4">
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="30px" Width="30px"
                                    ToolTip="Búsqueda" Visible="true" OnClick="Busqueda_Area" />
                            </div>--%>
                            <div class="col-sm-2">
                                <label for="ContentPlaceHolder1_txt_tarea" class="form-label">Clave</label>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_tarea" runat="server" CssClass="form-control" OnTextChanged="tarea_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:LinkButton ID="linkBttnBuscaArea" class="btn btn-success" runat="server" OnClick="linkBttnBuscaArea_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3">
                                <label for="ContentPlaceHolder1_txt_nom_area" class="form-label">Área</label>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_nom_area" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <%-- <div class="col-md-0.4">
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="30px" Width="30px"
                                    ToolTip="Búsqueda" Visible="true" OnClick="Busqueda_Periodo" />
                            </div>--%>
                            <div class="col-md-2">
                                <label for="ContentPlaceHolder1_txt_tpees" class="form-label">Clave</label>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_tpees" runat="server" CssClass="form-control" OnTextChanged="tpees_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:LinkButton ID="linkBttnBuscaClave" class="btn btn-success" runat="server" OnClick="linkBttnBuscaClave_Click"><i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-3">
                                <label for="ContentPlaceHolder1_txt_nom_per" class="form-label">Periodo</label>
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_nom_per" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <%-- <div class="row">
                            <div class="col">
                                <div class="accordion" id="accordionExample">
                                    <div class="card">
                                        <div id="collapseRecargo" class="collapse" data-parent="#accordionExample">
                                            <div class="card-body">
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="Gridtpees" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtpees_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                                                <asp:BoundField DataField="Nombre" HeaderText="Ciclo" />
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
                        </div>--%>
                        </div>
                        <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                            <div class="col-md-3">
                                <label for="ContentPlaceHolder1_txt_consecutivo" class="form-label">Consecutivo</label>
                                <asp:TextBox ID="txt_consecutivo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ContentPlaceHolder1_txt_periodo" class="form-label">Periodo Cursa</label>
                                <asp:TextBox ID="txt_periodo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ContentPlaceHolder1_ddl_tipo" class="form-label">Tipo Materia</label>
                                <asp:DropDownList ID="ddl_tipo" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                                <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <div id="table_tplan" class="align-center">
                    <asp:GridView ID="Gridtplan" runat="server" CssClass="table table-striped table-bordered" Width="50%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtplan_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                            <asp:BoundField DataField="Consecutivo" HeaderText="Consecutivo" />
                            <asp:BoundField DataField="Clave" HeaderText="Clave" />
                            <asp:BoundField DataField="Nombre" HeaderText="Materia" />
                            <asp:BoundField DataField="c_area" HeaderText="c_area">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Area" HeaderText="Area" />
                            <asp:BoundField DataField="Ciclo" HeaderText="Ciclo" />
                            <asp:BoundField DataField="c_ciclo" HeaderText="c_ciclo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                            <asp:BoundField DataField="c_tipo" HeaderText="c_tipo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="c_estatus" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="SteelBlue" ForeColor="white" />
                    </asp:GridView>
                </div>

                <div class="row">
                    <div class="col text-center" id="btn_tplan" runat="server">

                        <asp:Button ID="cancelar_prog" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_prog_Click" />
                        <asp:Button ID="guardar_prog" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_prog_Click" />
                        <asp:Button ID="update_prog" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="update_prog_Click" />

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>









        <!-- Modal -->
        <div class="modal fade bd-example-modal-lg" id="modalProgramas" tabindex="-1" role="dialog" aria-labelledby="exampleModalPrograma" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="labelPrograma">Programas</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Gridtprog" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtprog_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Programa" />
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="SteelBlue" ForeColor="white" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Materias -->
        <div class="modal fade bd-example-modal-lg" id="modalMaterias" tabindex="-1" role="dialog" aria-labelledby="exampleModalPrograma" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="labelMaterias">Materias</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
                                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Materia" />
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="SteelBlue" ForeColor="white" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Materias -->
        <div class="modal fade bd-example-modal-lg" id="modalAreas" tabindex="-1" role="dialog" aria-labelledby="exampleModalAreas" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="labelAreas">Areas</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col">
                                        <asp:GridView ID="Gridtarea" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtarea_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Área" />
                                            </Columns>
                                            <SelectedRowStyle CssClass="selected_table" />
                                            <HeaderStyle BackColor="SteelBlue" ForeColor="white" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Periodos -->
        <div class="modal fade bd-example-modal-lg" id="modalPeriodos" tabindex="-1" role="dialog" aria-labelledby="exampleModalPeriodos" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="labelPeriodos">Periodos</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Gridtpees" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtpees_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Ciclo" />
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

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }

        function load_datatable_Programa() {
            $('#<%= Gridtprog.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtprog.ClientID %>').find("tr:first"))).DataTable({
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
            $('#<%= Gridtprog.ClientID %>').columns.adjust().draw();
        }

        function load_datatable_Materia() {
            $('#<%= Gridtmate.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtmate.ClientID %>').find("tr:first"))).DataTable({
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

        function load_datatable_Area() {
            let table_programas = $("#ContentPlaceHolder1_Gridtarea").DataTable({
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

        function load_datatable_Periodo() {
            $('#<%= Gridtpees.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtpees.ClientID %>').find("tr:first"))).DataTable({
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
                stateSave: true,
                scrollX: true,
                sScrollXInner: "100%"
            });

        }

        function load_datatable() {
            let table_programas = $("#ContentPlaceHolder1_Gridtplan").DataTable({
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
                        title: 'SAES_Plan de Estudios',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 5, 6, 8, 10, 11]
                        }
                    },
                    {
                        title: 'SAES_Plan de Estudios',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 5, 6, 8, 10, 11]
                        }
                    }
                ],
                stateSave: true
            });

        }



    </script>
</asp:Content>

