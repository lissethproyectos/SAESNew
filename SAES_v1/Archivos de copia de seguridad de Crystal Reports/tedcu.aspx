<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tedcu.aspx.cs" Inherits="SAES_v1.tedcu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <script src="Script/tcedc.js"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <style>
        span button {
            margin-bottom: 0px !important;
        }


        .wizard {
            margin: 20px auto;
            background: #fff;
        }

            .wizard .nav-tabs {
                position: relative;
                margin: 0 auto;
                margin-bottom: 0;
                border-bottom-color: #e0e0e0;
            }

            .wizard > div.wizard-inner {
                position: relative;
            }

        .nav > li {
            position: relative;
            display: block;
            padding: 0 50px 0 0;
        }


        .hide {
            display: none;
        }

        .connecting-line {
            height: 2px;
            background: #e0e0e0;
            position: absolute;
            width: 80%;
            margin: 0 auto;
            left: 0;
            right: 0;
            top: 50%;
            z-index: 1;
        }

        .wizard .nav-tabs > li.active > a, .wizard .nav-tabs > li.active > a:hover, .wizard .nav-tabs > li.active > a:focus {
            color: #555555;
            cursor: default;
            border: 0;
            border-bottom-color: transparent;
        }

        span.round-tab {
            width: 70px;
            height: 70px;
            line-height: 70px;
            display: inline-block;
            border-radius: 100px;
            background: #fff;
            border: 2px solid #e0e0e0;
            z-index: 2;
            position: absolute;
            left: 0;
            text-align: center;
            font-size: 25px;
        }

            span.round-tab i {
                color: #555555;
            }

        .wizard li.active span.round-tab {
            background: #fff;
            border: 2px solid #8a2661;
        }

            .wizard li.active span.round-tab i {
                color: #bb7da1;
            }

        span.round-tab:hover {
            color: #333;
            border: 2px solid #333;
        }

        .wizard .nav-tabs > li {
            width: 33%;
        }

        .wizard li:after {
            content: " ";
            position: absolute;
            left: 46%;
            opacity: 0;
            margin: 0 auto;
            bottom: 0px;
            border: 5px solid transparent;
            border-bottom-color: #bb7da1;
            transition: 0.1s ease-in-out;
        }

        .wizard li.active:after {
            content: " ";
            position: absolute;
            left: 46%;
            opacity: 1;
            margin: 0 auto;
            bottom: 0px;
            border: 10px solid transparent;
            border-bottom-color: #bb7da1;
        }

        .wizard .nav-tabs > li a {
            width: 70px;
            height: 70px;
            margin: 20px auto;
            border-radius: 100%;
            padding: 0;
        }

            .wizard .nav-tabs > li a:hover {
                background: transparent;
            }

        .wizard .tab-pane {
            position: relative;
            padding-top: 50px;
        }

        .wizard h3 {
            margin-top: 0;
        }

        @media( max-width : 585px ) {

            .wizard {
                width: 90%;
                height: auto !important;
            }

            span.round-tab {
                font-size: 16px;
                width: 50px;
                height: 50px;
                line-height: 50px;
            }

            .wizard .nav-tabs > li a {
                width: 50px;
                height: 50px;
                line-height: 50px;
            }

            .wizard li.active:after {
                content: " ";
                position: absolute;
                left: 35%;
            }
        }

        .not-active {
            pointer-events: none;
            cursor: default;
        }
    </style>
    <%--<script>


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

        function error_saldo() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Importe no puede ser mayor a Saldo</h2>'
            })
        }

        function error_saldo_menor() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h7 class="swal2-title" id="swal2-title">Importe no puede ser menor a Saldo, favor de hacer la modificación para que pueda realizar el pago</h7>'
            })
        }

        function error_numerico() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Importe ... debe ser un valor numérico !!!</h2>'
            })
        }

        function ImportePagar() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Se debe capturar Importe a pagar</h2>'
            })
        }

        function ImporteDiferente() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Total a Pagar / Total Medio Pago deben ser iguales!</h2>'
            })
        }

        function NoExist() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">No existe Matrícula</h2>'
            })
        }

        function NoExistConcepto() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">No existe Concepto Pago</h2>'
            })
        }

        //---- Concepto Pago ----//
        function validarConcepto(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Concepto Pago');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Concepto Pago ----//
        function validarCantidad(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Importe Pago');
                return false;
            } else {
                validadoForm(idElemento);
            }
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


        function Noexiste(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;

            errorForm(idElemento, 'No existe Clave');
            return false;
        }

        //---- Valida Campos Solicitud ----//
        function validar_campos_pago(e) {
            event.preventDefault(e);
            validarConcepto('ContentPlaceHolder1_txt_concepto');
            validarCantidad('ContentPlaceHolder1_TxtCantidad');
            return false;
        }

        function validarEntero_cantidad(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
            valor = parseInt(idElemento))

            //Compruebo si es un valor numérico
            if (isNaN(valor)) {
                //entonces (no es numero) devuelvo el valor cadena vacia
                errorForm(idElemento, 'Monto fijo es valor numérico');
                return false
            } else {
                //En caso contrario (Si era un número) devuelvo el valor
                validadoForm(idElemento);
            }
        }

        function valida_cantidad(e) {
            validarEntero_cantidad('ContentPlaceHolder1_TxtCantidad');
        }

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div>
            <section>
                <div class="wizard">
                    <div class="wizard-inner">
                        <div class="connecting-line"></div>
                        <ul class="nav nav-tabs" role="tablist">
                            <li id="paso1" role="presentation" class="active" runat="server" onclick="Paso1()">
                                <a href="#step1" data-toggle="tab" aria-controls="step1" role="tab" title="Paso 1">
                                    <span class="round-tab">
                                        <i class="fa fa-file-text" aria-hidden="true"></i>
                                    </span>
                                </a>
                            </li>
                            <li role="presentation" style="display: none;" id="paso2" runat="server" onclick="Paso2()">
                                <a href="#step2" data-toggle="tab" aria-controls="step2" role="tab" title="Paso 2">
                                    <span class="round-tab">
                                        <i class="fa fa-credit-card-alt" aria-hidden="true"></i>
                                    </span>
                                </a>
                            </li>
                            <li role="presentation" style="display: block;" id="paso2_enabled" runat="server">
                                <a href="#step2" data-toggle="tab" class="not-active" aria-controls="step2" role="tab" title="Paso 2">
                                    <span class="round-tab">
                                        <i class="fa fa-credit-card-alt" aria-hidden="true"></i>
                                    </span>
                                </a>
                            </li>
                            <li role="presentation" style="display: none;" id="paso3" runat="server" onclick="Paso3()">
                                <a href="#step3" data-toggle="tab" aria-controls="step3" role="tab" title="Paso 3">
                                    <span class="round-tab">
                                        <i class="fa fa-check-circle"></i>
                                    </span>
                                </a>
                            </li>
                            <li role="presentation" style="display: block;" id="paso3_enabled" runat="server">
                                <a href="#step3" data-toggle="tab" class="not-active" aria-controls="step3" role="tab" title="Paso 3">
                                    <span class="round-tab">
                                        <i class="fa fa-check-circle"></i>
                                    </span>
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div role="form">
                        <div class="tab-content">
                            <div class="tab-pane active" role="tabpanel" id="step1">
                                <h5><strong>Estado de Cuenta</strong></h5>
                                <div id="form_solicitud" runat="server">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            Matrícula
                                             <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                                 <ContentTemplate>
                                                     <div class="input-group">
                                                         <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                                                         <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                                     </div>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    Nombre(s)                           
                                                    <asp:TextBox ID="txt_alumno" MaxLength="10" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-4">
                                            Programa
                                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                              <ContentTemplate>
                                                  <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control"></asp:DropDownList>
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                        </div>
                                   
                                        <div class="col-sm-3">
                                            Periodo
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar periodo" ControlToValidate="ddl_periodo" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
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
                                <hr />

                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="row" id="rowAlumnos" runat="server">
                                            <div class="col">
                                                <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged">
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
                                                        <%--      <asp:BoundField DataField="tpers_cgenero" HeaderText="C_Genero">
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
                                <asp:BoundField DataField="tpers_fecha_reg" HeaderText="Fecha Registro" />--%>
                                                    </Columns>
                                                    <SelectedRowStyle CssClass="selected_table" />
                                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%--    <asp:UpdatePanel ID="updPnlConceptos" runat="server">
                                    <ContentTemplate>--%>
                                <asp:UpdatePanel ID="updPnlAgregarCargo" runat="server">
                                    <ContentTemplate>
                                        <div id="divBotones" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col text-center">

                                                    <asp:LinkButton ID="linkBttnCancelar" CssClass="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                                                    <asp:LinkButton ID="linkBttnAgregarCargo" runat="server" CssClass="btn btn-round btn-secondary" data-toggle="modal" data-target="#modalConceptos" OnClick="linkBttnAgregarCargo_Click"><i class="fa fa-plus"></i>&nbsp;Nuevo Cargo</asp:LinkButton>
                                                    <asp:LinkButton ID="linkBttnPagosAplicados" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnPagosAplicados_Click"><i class="fa fa-ticket" aria-hidden="true" OnClick="PDF_Click"></i>&nbsp;Ver Estado de Cuenta</asp:LinkButton>
                                                    <asp:LinkButton ID="linkBttnComprobante" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnComprobante_Click"><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;Resumen de Notas/Facturas</asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row">
                                    <div class="col">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="Gridtedcu" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtedcu_SelectedIndexChanged" ShowFooter="True" ShowHeaderWhenEmpty="True" EmptyDataText="No existen cargos para este alumno." EmptyDataRowStyle-ForeColor="#2A3F54" EmptyDataRowStyle-Width="14px">
                                                    <Columns>
                                                        <asp:BoundField DataField="transa" HeaderText="# Transacción"></asp:BoundField>
                                                        <asp:BoundField DataField="periodo" HeaderText="Periodo" />
                                                        <asp:BoundField DataField="parcia" HeaderText="Parcialidad">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="clave" HeaderText="Clave" />
                                                        <asp:BoundField DataField="concepto" HeaderText="Concepto"></asp:BoundField>
                                                        <asp:BoundField DataField="importe_total" HeaderText="Importe" />
                                                        <asp:BoundField DataField="saldo" HeaderText="Saldo">
                                                            <ItemStyle Font-Bold="True" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Right">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("vencimiento") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotal" runat="server" Text="Total a Pagar" Font-Size="16px" Font-Bold="True"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("vencimiento") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Importe Pago">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblGranTotal" runat="server" Text="0" Font-Size="22px" Font-Bold="True"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <div class="input-group mb-3">
                                                                    <span class="input-group-text">$</span>
                                                                    <asp:TextBox ID="suma" runat="server" AutoPostBack="true" CssClass="form-control" Enabled='<%# Bind("inhabil") %>' OnTextChanged="suma_click" Text='<%# Bind("importe") %>' />
                                                                    <br />
                                                                    <asp:Label ID="lblReq" runat="server" Text="Requerido" CssClass="text-danger" Visible="false"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="15%" />
                                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="linkBttnPagar" runat="server" CssClass="btn btn-dark" OnClick="linkBttnPagar_Click" OnClientClick="Pagar();" Visible="False"><i class="fa fa-arrow-circle-right" aria-hidden="true"></i> Pagar</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="" class="btn btn-secondary"><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle Font-Size="Small" />
                                                    <SelectedRowStyle CssClass="selected_table" />
                                                    <EmptyDataRowStyle ForeColor="#2A3F54" Width="14px" />
                                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hddnSFV" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>
                            <div class="tab-pane" role="tabpanel" id="step2">
                                <h5><strong>Pago en efectivo/TDC</strong></h5>
                                <br />
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdatePanel ID="updPnlTotPagar" runat="server">
                                            <ContentTemplate>
                                                <h6>Total a Pagar</h6>
                                                <h3>
                                                    <asp:Label ID="lblTotalFinal" runat="server" Text="" CssClass="font-weight-bold"></asp:Label>
                                                    <asp:HiddenField ID="hddnGranTotal" runat="server" />
                                                </h3>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-row">
                                       <div class="col-md-2"></div>
                                    <div class="col-sm-3">
                                        Forma de Pago
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_forma_pago" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2">
                                        Importe
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_importe_efect" runat="server" CssClass="form-control"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2">
                                        <br />
                                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="linkBttnAgFormaPago" runat="server" CssClass="btn btn-success" OnClick="linkBttnAgFormaPago_Click"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Agregar</asp:LinkButton>
                                                <asp:LinkButton ID="linkBttnCancelarModificar" Visible="false" class="btn btn-secondary" runat="server" OnClick="linkBttnCancelarModificar_Click">Cancelar</asp:LinkButton>
                                                <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-success" Visible="false" OnClick="linkBttnModificar_Click" ValidationGroup="valDatos">Actualizar</asp:LinkButton>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-8">
                                        <asp:UpdatePanel ID="updPnlPagos" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridPagos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" RowStyle-Font-Size="small" Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="GridPagos_SelectedIndexChanged" OnRowDeleting="GridPagos_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">                                           
                                                                        <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="tpag1_tprog_clave" HeaderText="Clave">
                                                            <HeaderStyle CssClass="ocultar" />
                                                            <ItemStyle CssClass="ocultar" />
                                                            <FooterStyle CssClass="ocultar" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Concepto">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCvePago" runat="server" Text='<%# Bind("tpag1_tcoco_clave") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblLeyGranTotalFin" runat="server" Text="Gran Total" Font-Size="16px" Font-Bold="True"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Saldo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblImportePag1" runat="server" Text='<%# Bind("tpag1_importe") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>

                                                                <asp:Label ID="lblGranTotalFin" runat="server" Text="0" Font-Size="22px" Font-Bold="True"></asp:Label>

                                                                <%--<div class="col-md-8">
                                                                        </div>--%>
                                                                    </div>
                                                            </FooterTemplate>
                                                            <ItemStyle Width="25%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="tpag1_tedcu_consec" HeaderText="Consecutivo">
                                                            <HeaderStyle CssClass="ocultar" />
                                                            <ItemStyle CssClass="ocultar" />
                                                            <FooterStyle CssClass="ocultar" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkBttnEliminar" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn btn-success" Text="Eliminar"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle Font-Size="Small" />
                                                    <SelectedRowStyle CssClass="selected_table" />
                                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-2"></div>

                                </div>
                                <div class="row">
                                    <div class="col text-right">
                                        <asp:UpdatePanel ID="updPnlAplicaPagos" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="linkBttnAplicarpagos" runat="server" CssClass="btn btn-secondary" OnClick="Guardar_Pago"><i class="fa fa-check-circle" aria-hidden="true"></i> Aplica Pagos</asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                            </div>
                            <div class="tab-pane" role="tabpanel" id="step3">
                                <%-- <h3>Comprobante de Pago</h3>--%>
                                <h5><strong>Comprobante de Pago</strong></h5>
                                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                    <ContentTemplate>
                                        <div id="rowPagoExitoso" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-md-3"></div>
                                                <div class="col-md-6 text-center">
                                                    <img src="Images/Generales/PagoConfirmado.png" style="width: 75px; height: 75px;" />
                                                    <h4>
                                                        <asp:Label ID="lblNumFact" runat="server" Text="--"></asp:Label></h4>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:LinkButton ID="linkRegresar" runat="server" class="btn btn-dark" OnClick="linkRegresar_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;Nuevo Pago</asp:LinkButton>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col text-center">
                                                    <div id="precarga1"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col text-center">
                                                    <iframe id="miniContenedor" frameborder="0" marginheight="0" marginwidth="0" name="miniContenedor"
                                                        style="height: 500px" width="100%"></iframe>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>





        <hr />



        <%-- <asp:UpdatePanel ID="updPnlGridBtnTedcu" runat="server">
            <ContentTemplate>
                <div class="row" id="btn_tedcu" runat="server" visible="false">
                    <div class="col text-center">
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar Pago" OnClick="btn_pagos_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <%--<div class="row" id="Div1" runat="server">
            <div class="col-sm-4"></div>
            <div class="col-sm-3">
                <h5>
                    <asp:Label ID="lbltotal" runat="server" Text="Total a Pagar" CssClass="font-weight-bold" /></h5>
                <div class="input-group mb-3">
                    <span class="input-group-text">$</span>
                    <asp:TextBox ID="TxtSuma" MaxLength="10" runat="server" CssClass="form-control" ReadOnly="true" Style="text-align: right"></asp:TextBox>

                </div>
            </div>
            <div class="col-sm-4"></div>
        </div>--%>

        <div class="row g-3 justify-content-center" style="margin-top: 15px;">
            <div class="col-md-0.4">
                <asp:UpdatePanel ID="updPnlConsulta" runat="server">
                    <ContentTemplate>
                        <asp:ImageButton ID="ImgConsulta1" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                            ToolTip="Búsqueda" Visible="false" OnClick="grid_conceptos_bind" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-2" style="text-align: right; margin-top: 15px;">
                <asp:UpdatePanel ID="updPnlConc_pago" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="Conc_pago" runat="server" Text="Concepto pago" Font-Size="Medium" Style="text-align: right" Visible="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-1">
                <asp:UpdatePanel ID="updPnltxtConcepto" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_concepto" MaxLength="6" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_concepto_TextChanged" Visible="false"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="updPnltxt_nom_concepto" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nom_concepto" runat="server" CssClass="form-control" ReadOnly="true" Visible="false"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-1" style="text-align: right; margin-top: 15px;">
                <asp:UpdatePanel ID="updPnlImporte" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="Importe" runat="server" Text="Importe $" Font-Size="Medium" Style="text-align: right" Visible="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="TxtCantidad" MaxLength="10" runat="server" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-0.4">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:ImageButton ID="ImgGo" runat="server" ImageUrl="Images/Operaciones/go.png" Height="30px" Width="30px"
                            ToolTip="Guardar" Visible="false" OnClick="Agregar_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="row">
            <div class="col">
                <div id="table_tcoco">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="Gridtcoco" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false" OnSelectedIndexChanged="Gridtcoco_SelectedIndexChanged">
                                <Columns>
                                    <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                </Columns>
                                <SelectedRowStyle CssClass="selected_table" />
                                <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <div id="Div2" runat="server" class="row justify-content-center" visible="false">
            <div class="col-md-2" style="text-align: right; margin-top: 15px;">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="LblPago" runat="server" Font-Size="Medium" Style="text-align: right" Text="Total Medio Pago $" Visible="false" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-2" style="text-align: right; margin-top: 15px;">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="TxtPago" runat="server" CssClass="form-control" MaxLength="10" ReadOnly="true" Style="text-align: right" Visible="false"></asp:TextBox>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-2" style="text-align: right; margin-top: 15px;">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="LblAplica" runat="server" Font-Size="Medium" Style="text-align: right" Text="Aplica Pagos --&gt;" Visible="false" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-0.4">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:ImageButton ID="ImgAplica" runat="server" Height="30px" ImageUrl="Images/Operaciones/aplica.png" OnClick="Guardar_Pago" ToolTip="Aplicar Pago" Visible="false" Width="30px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


        <!-- Modal -->
        <div class="modal fade" id="modalConceptos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Conceptos</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row">
                            <div class="col-sm-12">
                                Periodo
                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddl_periodo_concep" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar periodo" ControlToValidate="ddl_periodo" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-sm-12">
                                Conceptos
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_conceptos" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_conceptos_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-sm-4">
                                Importe
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_importe" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="updPnlBotonesInsConcepto" runat="server">
                            <ContentTemplate>
                                <button type="button" class="btn btn-round btn-secondary" data-dismiss="modal">Cancelar</button>
                                <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click" ValidationGroup="guardar">Agregar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Saldo a Favor -->
        <div class="modal" tabindex="-1" role="dialog" id="modalSFV">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Informativo </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblMsjSFV" runat="server" Text="Label" Font-Size="16px"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="linkBttnCancelarSFV" class="btn btn-secondary" data-dismiss="modal" runat="server" OnClick="linkBttnCancelarSFV_Click">No</asp:LinkButton>
                                <asp:LinkButton ID="linkBttnAplicarSFV" class="btn btn-success" runat="server" OnClick="linkBttnAplicarSFV_Click">Si</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
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
                        title: 'SAES',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        }
                    }
                ],
                "stateSave": true
            });
        }
    </script>
</asp:Content>


