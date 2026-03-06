<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpaco.aspx.cs" Inherits="SAES_v1.tpaco" %>

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

        //----Cobranza Periodo----//
        function validarperiodo(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Selecciona periodo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza Campus----//
        function validarcampus_cob(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Cobranza nivel----//

        function validarnivel_cob(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar nivel');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza tipo periodo----//
        function validartperiodo_cob(idEl, ind) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();
            if (ind == 1) {
                errorForm(idElemento, 'El tipo de periodo para el campus,nivel y periodo ya existe.');
                return false;
            } else {
                if (valor == 0) {
                    errorForm(idElemento, 'Seleccionar tipo periodo');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            }

        }

        //---- periodo----//
        function validarPeriodo(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar periodo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Descuento Ins----//
        function validarIns(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Ingresar % descuento Insc.');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Descuento Col----//
        function validarCol(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Ingresar % descuento Cole.');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza concepto----//

        function validarConcepto(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar concepto Cobranza');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza calendario----//

        function validarCalendario(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar concepto calendario');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos cobranza ----//
        function validar_campos_cobranza(e) {
            event.preventDefault(e);
            validartperiodo_cob('ContentPlaceHolder1_cobranza_p');
            validarcampus_cob('ContentPlaceHolder1_cobranza_c');
            validarnivel_cob('ContentPlaceHolder1_cobranza_n');
            validartperiodo_cob('ContentPlaceHolder1_cobranza_tipo_p', 0);
            validarPeriodo('ContentPlaceHolder1_cobranza_p');
            validarIns('ContentPlaceHolder1_descuento_ins');
            validarCol('ContentPlaceHolder1_descuento_col');
            validarConcepto('ContentPlaceHolder1_cobranza_concepto');
            validarCalendario('ContentPlaceHolder1_cobranza_conc_cal');
            return false;
        }

        function validarEntero_desc_insc(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
            valor = parseInt(idElemento)

            //Compruebo si es un valor numérico
            if (isNaN(valor)) {
                //entonces (no es numero) devuelvo el valor cadena vacia
                errorForm(idElemento, 'Descuento Insc. es valor numérico');
                return false
            } else {
                //En caso contrario (Si era un número) devuelvo el valor
                validadoForm(idElemento);
            }
        }

        function validarEntero_desc_cole(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
            valor = parseInt(idElemento)

            //Compruebo si es un valor numérico
            if (isNaN(valor)) {
                //entonces (no es numero) devuelvo el valor cadena vacia
                errorForm(idElemento, 'Descuento Cole. es valor numérico');
                return false
            } else {
                //En caso contrario (Si era un número) devuelvo el valor
                validadoForm(idElemento);
            }
        }

        function valida_desc_insc(e) {
            validarEntero_desc_insc('ContentPlaceHolder1_descuento_ins');
        }

        function valida_desc_cole(e) {
            validarEntero_desc_cole('ContentPlaceHolder1_descuento_col');
        }

    </script>
    <style>
        #operacion ul {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
           <i class="fa fa-usd" aria-hidden="true"></i><small>Parámetros Cobranza Campus</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="form-row">
        <div class="col text-center">
            <asp:UpdateProgress ID="updPgr1" runat="server"
                AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:Image ID="img9" runat="server"
                        AlternateTexkt="Espere un momento, por favor.." Height="50px"
                        ImageUrl="~/Images/Sitemaster/loader.gif"
                        ToolTip="Espere un momento, por favor.." Width="50px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="form_cobranza" runat="server">
                <div class="form-row">
                    <div class="col-md-3" runat="server" id="dd_term">
                        <label for="ContentPlaceHolder1_dd_periodo" class="form-label">Periodo</label>
                        <asp:DropDownList ID="dd_periodo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dd_periodo_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="" ControlToValidate="dd_periodo" runat="server" CssClass="text-danger" ErrorMessage="* Seleccionar Periodo" Text="* Seleccionar Periodo" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <label for="ContentPlaceHolder1_cobranza_c" class="form-label">Campus</label>
                        <asp:DropDownList ID="cobranza_c" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cobranza_c_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label for="ContentPlaceHolder1_cobranza_n" class="form-label">Nivel Acádemico</label>
                        <asp:DropDownList ID="cobranza_n" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label for="ContentPlaceHolder1_cobranza_tipo_p" class="form-label">Tipo Periodo</label>
                        <asp:UpdatePanel ID="updPnlTipoPer" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cobranza_tipo_p" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="" ControlToValidate="cobranza_tipo_p" runat="server" CssClass="text-danger" ErrorMessage="* Seleccionar Tipo Periodo" Text="* Seleccionar Tipo Periodo" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="col-md-3" id="term_text" runat="server" visible="false">
                        <label for="ContentPlaceHolder1_cobranza_p" class="form-label">Periodo</label>
                        <div class="input-group">
                            <asp:TextBox ID="cobranza_p" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="cobranza_p_TextChanged"></asp:TextBox>

                            <asp:LinkButton ID="search_term" runat="server" OnClick="search_term_Click" CssClass="btn btn-info"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            <%--<asp:ImageButton ID="search_term" runat="server" ImageUrl="~/Images/Operaciones/lupa.png" CssClass="imgbtn" OnClick="search_term_Click" />--%>
                        </div>
                    </div>

                </div>
                <hr />
                <div class="form-row">
                    <div class="col-md-4">
                        <label for="ContentPlaceHolder1_cobranza_conc_cal" class="form-label">Concepto Calendario</label>
                        <asp:UpdatePanel ID="updPnlConceptoCal" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cobranza_conc_cal" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="" ControlToValidate="cobranza_conc_cal" runat="server" CssClass="text-danger" ErrorMessage="* Seleccionar Concepto Calendario" Text="* Seleccionar Concepto Calendario" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-4">
                        <label for="ContentPlaceHolder1_cobranza_concepto" class="form-label">Concepto Cobranza</label>
                        <asp:UpdatePanel ID="updPnlConceptoCob" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cobranza_concepto" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="" ControlToValidate="cobranza_concepto" runat="server" CssClass="text-danger" ErrorMessage="* Seleccionar Concepto Cobranza" Text="* Seleccionar Concepto Cobranza" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="updPnlDescIns" runat="server">
                            <ContentTemplate>
                                <label for="ContentPlaceHolder1_descuento_ins" class="form-label">Descuento Inscripción</label>
                                <div class="input-group">
                                    <asp:TextBox ID="descuento_ins" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-text" style="border-bottom-left-radius: 0px; border-top-left-radius: 0px;">%</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="updPnlDescCol" runat="server">
                        <ContentTemplate>
                            <label for="ContentPlaceHolder1_descuento_col" class="form-label">Descuento Colegiatura</label>
                            <div class="input-group">
                                <asp:TextBox ID="descuento_col" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-text" style="border-bottom-left-radius: 0px; border-top-left-radius: 0px;">%</span>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            </div>
            <div class="form-row">
                <div class="col-md-8"></div>
                <div class="col-md-2">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="descuento_ins"
                        CssClass="text-danger" ErrorMessage="* Solo Números" ValidationExpression="\d+"
                        ValidationGroup="guardar" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-2">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="descuento_col"
                        CssClass="text-danger" ErrorMessage="* Solo Números" ValidationExpression="\d+"
                        ValidationGroup="guardar" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-row" id="cobranza_btn" runat="server">
                <div class="col text-center" style="text-align: center;">
                    <asp:UpdatePanel ID="updPnlBotones" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="cancelar_cob" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_cob_Click" Visible="false" />
                            <asp:Button ID="guardar_cob" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_cob_Click" ValidationGroup="guardar" />
                            <asp:Button ID="actualizar_cob" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="actualizar_cob_Click" ValidationGroup="guardar" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="w-100"></div>
                <div class="col-md-5" style="text-align: center;" runat="server" id="lbl_error" visible="false">
                    <asp:Label ID="lbl_error_text" runat="server" Text="La combinación ingresada ya existe, favor de validar" CssClass="textoError"></asp:Label>
                </div>
            </div>
            <div class="form-row">
                <div class="col">
                    <asp:UpdatePanel ID="updPnlGrid" runat="server">
                        <ContentTemplate>
                            <div id="table_cobranza">
                                <asp:GridView ID="GridCobranza" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="GridCobranza_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontaron parámetros de cobranza.">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="C_CAMPUS" HeaderText="C_Campus">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                                        <asp:BoundField DataField="C_NIVEL" HeaderText="C_Nivel">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                                        <asp:BoundField DataField="CLAVE" HeaderText="Clave">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_PERIODO" HeaderText="Tipo de Periodo" />
                                        <asp:BoundField DataField="DESC_INSC" HeaderText="Descuento Inscripción" />
                                        <asp:BoundField DataField="DESC_COL" HeaderText="Descuento Parcialidad" />
                                        <asp:BoundField DataField="C_CONCE_CAL" HeaderText="C_Conce_Cal">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONCE_CALENDARIO" HeaderText="Concepto Calendario" />
                                        <asp:BoundField DataField="C_CONCE_COB" HeaderText="C_Conce_Cob">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONCE_COBRANZA" HeaderText="Concepto Cobranza" />
                                        <asp:BoundField DataField="PERIODO" HeaderText="Periodo">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function load_datatable_cobranza() {
            let table_programas = $("#ContentPlaceHolder1_GridCobranza").DataTable({
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
                        title: 'SAES_Catálogo de Campus-Cobranza',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 5, 6, 7, 9, 11, 12]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus-Cobranza',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 5, 6, 7, 9, 11, 12]
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
