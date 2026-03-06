<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="C_Campus.aspx.cs" Inherits="SAES_v1.C_Campus" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_GridCampus_info {
            float: left;
        }

        #ContentPlaceHolder1_GridCampus tbody tr {
            cursor: pointer;
        }

        #ContentPlaceHolder1_GridCampus_length {
            float: left;
        }

        #ContentPlaceHolder1_GridProgramas_info {
            float: left;
        }

        #ContentPlaceHolder1_GridProgramas tbody tr {
            cursor: pointer;
        }

        #ContentPlaceHolder1_GridProgramas_length {
            float: left;
        }
        #ContentPlaceHolder1_GridCobranzas_info {
            float: left;
        }

        #ContentPlaceHolder1_GridCobranza tbody tr {
            cursor: pointer;
        }

        #ContentPlaceHolder1_GridCobranza_length {
            float: left;
        }
        .custom-control-input:checked ~ .custom-control-label::before {
            color: #fff;
            border-color: #1abb9c;
            background-color: #1abb9c;
        }

        .bottom {
            text-align: center;
            padding-top: 5px;
        }

        @media screen and (max-width: 540px) {
            .bottom {
                margin-top: 50px;
                text-align: center;
                display: grid;
            }
        }

        .ddlist {
            left: 725px;
            text-align: center;
        }

        .center_column {
            text-align: center;
        }

        .ocultar {
            display: none;
        }

        table.dataTable tbody > tr.selected, table.dataTable tbody > tr > .selected {
            background-color: #88e5cf !important;
        }

        .pills {
            /* overflow: visible; */
            background: #f5f7fa;
            height: 20px;
            /* margin: 21px 0 14px; */
            padding-left: 14px;
            position: relative;
            z-index: 1;
            width: 100%;
            border-bottom: 1px solid #e6e9ed;
        }

        .nav-item {
            margin-left: 8px;
            border: 1px solid #e6e9ed !important;
            margin-top: -17px !important;
            background: #f5f7fa;
        }

            .nav-item:hover {
                margin-left: 8px;
                border: 1px solid #e6e9ed !important;
                margin-top: -17px !important;
                background: #fff;
            }

            .nav-link.active,
            .nav-item.show .nav-link {
                border-right: 5px solid #1abb9c !important;
            }

        .buttons-excel {
            margin-right: 5px !important;
            border-top-right-radius: 3px !important;
            border-bottom-right-radius: 3px !important;
        }

        .buttons-pdf {
            border-top-left-radius: 3px !important;
            border-bottom-left-radius: 3px !important;
        }

        .dataTables_scroll {
            height: 270px !important;
        }

        .focus {
            border: 1px solid rgb(227, 65, 0);
        }

        .textoError {
            color: red;
            /*position: absolute;*/
            font-size: .9em;
            font-weight: 700;
            margin-bottom: 1px;
            /*top: 13px;*/
        }
        .imgbtn{
            width:25px;
            margin-top:-30px;
            margin-right:10px;
            float:right;
        }
    </style>
    <script>
        function save() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
                .then(willDelete => {
                    if (willDelete) {
                        loader_push();
                        window.location.reload();
                    }
                });
        }

        function update() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
                .then(willDelete => {
                    if (willDelete) {
                        loader_push();
                        window.location.reload();
                    }
                });
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

        //---- Clave Campus ----//
        function validarclaveCampus(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Por favor ingresa una clave valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Nombre Campus ----//
        function validarNombreCampus(idEl) {
            const idElemento = idEl;
            let tamin = document.getElementById(idElemento).value;
            if (tamin == null || tamin.length == 0 || /^\s+$/.test(tamin)) {
                errorForm(idElemento, 'Por favor ingresa el nombre del campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Valida Campos Campus ----//
        function validar_campos_campus(e) {
            event.preventDefault(e);
            validarclaveCampus('ContentPlaceHolder1_c_campus');
            validarNombreCampus('ContentPlaceHolder1_n_campus');
            return false;
        }

        //---- Clave Programa ----//
        function validarclavePrograma(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Por favor ingresa una clave valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Clave Programa No valida----//
        function validarclavePrograma_N(idEl, indicador) {
            const idElemento = idEl;
            if (indicador == 1) {
                errorForm(idElemento, 'Por favor ingresa una clave valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza Periodo----//
        function validarperiodo(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Por favor ingresa o selecciona un periodo valido');
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
                errorForm(idElemento, 'Debes seleccionar una campus válido');
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
                errorForm(idElemento, 'Debes seleccionar una nivel válido');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza tipo periodo----//
        function validartperiodo_cob(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Debes seleccionar un tipo de periodo válido');
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
            validartperiodo_cob('ContentPlaceHolder1_cobranza_tipo_p');
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/universidad.png" style="width: 30px;" /><small>Catálogo de Campus</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <nav style="margin-top: 10px;">
            <div class="nav nav-tabs justify-content-end pills" id="nav-tab" role="tablist">
                <a class="nav-item nav-link justify-content-end active" id="nav-Campus-tab" data-toggle="tab" href="#nav-Campus" role="tab" aria-controls="nav-Campus" aria-selected="true">Campus</a>
                <a class="nav-item nav-link justify-content-end" id="nav-Programas-tab" data-toggle="tab" href="#nav-Programas" role="tab" aria-controls="nav-Programas" aria-selected="false">Programas</a>
                <a class="nav-item nav-link justify-content-end" id="nav-Cobranza-tab" data-toggle="tab" href="#nav-Cobranza" role="tab" aria-controls="nav-Cobranza" aria-selected="false">Cobranza</a>
                <a class="nav-item nav-link justify-content-end" id="nav-Secuencias-tab" data-toggle="tab" href="#nav-Secuencias" role="tab" aria-controls="nav-Secuencias" aria-selected="false">Secuencias</a>
            </div>
        </nav>
        <div class="tab-content" id="nav-tabContent">
            <!--Pestaña Campus-->
            <div class="tab-pane fade show active" id="nav-Campus" role="tabpanel" aria-labelledby="nav-Campus-tab">
                <asp:UpdatePanel ID="upd_Campus" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="add_Campus" class="row justify-content-start" style="margin-top: 15px;">
                            <div class="col-10">
                                <asp:Button ID="agregar_Campus" runat="server" CssClass="btn btn-success" Text="Nuevo" OnClick="agregar_Campus_Click" />
                            </div>
                        </div>
                        <div id="form_Campus" runat="server">
                            <div class="row g-3" style="margin-top: 15px;">
                                <div class="col-md-4">
                                    <label for="ContentPlaceHolder1_c_campus" class="form-label">Clave</label>
                                    <input id="c_campus" runat="server" class="form-control" type="text" onblur="validarclaveCampus('ContentPlaceHolder1_c_campus')" oninput="validarclaveCampus('ContentPlaceHolder1_c_campus')" autocomplete="off" />
                                </div>
                                <div class="col-md-8">
                                    <label for="ContentPlaceHolder1_n_campus" class="form-label">Nombre</label>
                                    <input id="n_campus" runat="server" class="form-control" type="text" onblur="validarNombreCampus('ContentPlaceHolder1_n_campus')" oninput="validarNombreCampus('ContentPlaceHolder1_n_campus')" autocomplete="off" />
                                </div>
                                <div class="col-md-4">
                                    <label for="ContentPlaceHolder1_a_campus" class="form-label">Abreviatura</label>
                                    <input id="a_campus" runat="server" class="form-control" type="text" />
                                </div>
                                <div class="col-md-4">
                                    <label for="ContentPlaceHolder1_RFC_campus" class="form-label">RFC</label>
                                    <input id="RFC_campus" runat="server" class="form-control" type="text" />
                                </div>
                                <div class="col-md-4">
                                    <label for="ContentPlaceHolder1_estatus_pais" class="form-label">Estatus</label>
                                    <asp:DropDownList ID="estatus_campus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:HiddenField ID="edo_campus" runat="server" />
                                </div>
                            </div>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="upd_dir" class="row justify-content-start" style="margin-top: 15px;">
                                        <div class="col-10">
                                            <asp:Button ID="upd_btn_dir" runat="server" CssClass="btn btn-success" Text="Cambiar Dirección" OnClick="upd_btn_dir_Click" />
                                        </div>
                                    </div>
                                    <div class="row g-3" style="margin-top: 15px;">
                                        <div class="col-md-4">
                                            <label for="ContentPlaceHolder1_ddp_campus" class="form-label">Pais</label>
                                            <asp:DropDownList ID="ddp_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddp_campus_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:HiddenField ID="hd_ddp_campus" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <label for="ContentPlaceHolder1_dde_campus" class="form-label">Estado</label>
                                            <asp:DropDownList ID="dde_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dde_campus_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:HiddenField ID="hd_dde_campus" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <label for="ContentPlaceHolder1_ddd_campus" class="form-label">Delegacion-Municipio</label>
                                            <asp:DropDownList ID="ddd_campus" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:HiddenField ID="hd_ddd_campus" runat="server" />
                                        </div>
                                        <div class="col-md-2">
                                            <label for="ContentPlaceHolder1_zip_campus" class="form-label">Código Postal</label>
                                            <asp:TextBox ID="zip_campus" runat="server" CssClass="form-control" OnTextChanged="zip_campus_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="ContentPlaceHolder1_ddz_campus" class="form-label">Colonia</label>
                                            <asp:DropDownList ID="ddz_campus" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:HiddenField ID="hd_ddz_campus" runat="server" />
                                        </div>
                                        <div class="col-md-7">
                                            <label for="ContentPlaceHolder1_direc_campus" class="form-label">Dirección</label>
                                            <input id="direc_campus" runat="server" class="form-control" type="text" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_campus" runat="server">
                            <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="cancelar_campus" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClientClick="cancelar_campus(); return false" />
                                <asp:Button ID="guardar_campus" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_campus_Click" />
                                <asp:Button ID="actualizar_campus" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="actualizar_campus_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="table_campus">
                    <asp:GridView ID="GridCampus" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Campus" />
                            <asp:BoundField DataField="ABREVIATURA" HeaderText="Abreviatura" />
                            <asp:BoundField DataField="RFC" HeaderText="RFC" />
                            <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha de Modificacion" />
                            <asp:BoundField DataField="C_PAIS" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C_ESTADO" HeaderText="">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="N_ESTADO" HeaderText="">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C_DELE" HeaderText="">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="N_DELE" HeaderText="">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ZIP" HeaderText="">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COLONIA" HeaderText="">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DIRECCION" HeaderText="">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <!--\Pestaña Campus-->

            <!--Pestaña Programas-->
            <div class="tab-pane fade" id="nav-Programas" role="tabpanel" aria-labelledby="nav-Programas-tab">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="form_programa" runat="server">
                            <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                                <div class="col-md-8">
                                    <label for="ContentPlaceHolder1_search_campus" class="form-label">Busqueda de Campus</label>
                                    <asp:DropDownList ID="search_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="search_campus_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                                        <div class="col-md-2">
                                            <label for="ContentPlaceHolder1_c_prog_campus" class="form-label">Clave</label>
                                            <asp:TextBox ID="c_prog_campus" runat="server" CssClass="form-control" OnTextChanged="c_prog_campus_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-7">
                                            <label for="ContentPlaceHolder1_n_prog_campus" class="form-label">Programa</label>
                                            <asp:TextBox ID="n_prog_campus" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1" style="text-align: center;">
                                            <label for="ContentPlaceHolder1_adm_campus" class="form-label">Admisión</label>
                                            <div class="custom-control custom-switch">
                                                <input type="checkbox" class="custom-control-input" id="customSwitches" name="customSwitches">
                                                <label class="custom-control-label" for="customSwitches"></label>
                                                <asp:HiddenField ID="checked_input" runat="server" />                                               
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="ContentPlaceHolder1_e_prog_campus" class="form-label">Estatus</label>
                                            <asp:DropDownList ID="e_prog_campus" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row justify-content-center" style="text-align: center; margin: auto; margin-top: 15px;" id="btn_programa" runat="server">
                            <div class="col-md-3" style="text-align: center;margin-top: 15px;">
                                <asp:Button ID="cancelar_prog" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_prog_Click"/>
                                <asp:Button ID="guardar_prog" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_prog_Click"/>
                                <asp:Button ID="update_prog" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="update_prog_Click"/>
                            </div>
                        </div>
                        <div id="tabla_programas" style="margin-top: 15px;">
                            <asp:GridView ID="GridProgramas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Programa" />
                                    <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                                    <asp:BoundField DataField="MODALIDAD" HeaderText="Modalidad" />
                                    <asp:BoundField DataField="ADMISION" HeaderText="Admisión" />
                                    <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                        <HeaderStyle CssClass="ocultar" />
                                        <ItemStyle CssClass="ocultar" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha de Modificacion" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!--\Pestaña Programas-->

            <!--Pestaña Cobranza-->
            <div class="tab-pane fade" id="nav-Cobranza" role="tabpanel" aria-labelledby="nav-Cobranza-tab">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="form_cobranza" runat="server">
                            <div class="row g-3 justify-content-center" style="margin-top: 15px;">
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
                                    <asp:DropDownList ID="cobranza_tipo_p" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3" id="term_text" runat="server">
                                    <label for="ContentPlaceHolder1_cobranza_p" class="form-label">Periodo</label>
                                    <asp:TextBox ID="cobranza_p" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="cobranza_p_TextChanged"></asp:TextBox><asp:ImageButton ID="search_term" runat="server" ImageUrl="~/Images/Operaciones/lupa.png" CssClass="imgbtn" OnClick="search_term_Click"/>
                                </div>
                                <div class="col-md-3" runat="server" id="dd_term">
                                    <label for="ContentPlaceHolder1_dd_periodo" class="form-label">Periodo</label>
                                    <asp:DropDownList ID="dd_periodo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dd_periodo_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-5">
                                    <label for="ContentPlaceHolder1_cobranza_conc_cal" class="form-label">Concepto Calendario</label>
                                    <asp:DropDownList ID="cobranza_conc_cal" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-5">
                                    <label for="ContentPlaceHolder1_cobranza_concepto" class="form-label">Concepto Cobranza</label>
                                    <asp:DropDownList ID="cobranza_concepto" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-5">
                                    <label for="ContentPlaceHolder1_descuento_ins" class="form-label">Descuento de Inscripción</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="descuento_ins" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-text" style="border-bottom-left-radius: 0px; border-top-left-radius: 0px;">%</span>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <label for="ContentPlaceHolder1_descuento_col" class="form-label">Descuento de Colegiatura</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="descuento_col" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-text" style="border-bottom-left-radius: 0px; border-top-left-radius: 0px;">%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center" style="text-align: center; margin: auto; margin-top: 15px;" id="cobranza_btn" runat="server">
                            <div class="col-md-5" style="text-align: center;">
                                <asp:Button ID="cancelar_cob" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClientClick="window.location.reload();"/>
                                <asp:Button ID="guardar_cob" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_cob_Click"/>
                                <asp:Button ID="actualizar_cob" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="actualizar_cob_Click"/>
                            </div>
                            <div class="w-100"></div>
                            <div class="col-md-5" style="text-align: center;" runat="server" id="lbl_error" visible="false">
                                <asp:Label ID="lbl_error_text" runat="server" Text="La combinación ingresada ya existe, favor de validar" CssClass="textoError"></asp:Label>
                            </div>
                        </div>
                        <div id="div_oculto" class="ocultar">
                            <asp:TextBox ID="campus" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="nivel" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="t_periodo" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="periodo_txt" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="conce_cal_txt" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="conce_cob_txt" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="desc_ins" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="desc_col" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_oculto" runat="server" CssClass="btn btn-round btn-secondary" Text="" OnClick="btn_oculto_Click"/>
                        </div>
                        <div id="table_cobranza">
                            <asp:GridView ID="GridCobranza" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False">
                                <Columns>
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
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!--/Pestaña Cobranza-->
        </div>
    </div>
    <!--Funciones Generales-->
    <script>
        $(document).ready(function () {

            if (location.hash) {
                $("a[href='" + location.hash + "']").tab("show");
            }
            $(document.body).on("click", "a[data-toggle='tab']", function (event) {
                location.hash = this.getAttribute("href");
            });
        });

        $(window).on("popstate", function () {
            var anchor = location.hash || $("a[data-toggle='tab']").first().attr("href");
            $("a[href='" + anchor + "']").tab("show");
        });
        $("#customSwitches").on("change", function () {
            if (this.checked) {
                $("#ContentPlaceHolder1_checked_input").val('1')

            } else {
                $("#ContentPlaceHolder1_checked_input").val('0')
            }
            console.log($("#customSwitches").val());
            console.log($("#ContentPlaceHolder1_checked_input").val());
        });
    </script>
    <!--\Funciones Generales-->
    <!--Funciones pestaña Campus-->
    <script>
        $(document).ready(function () {

            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
            });

            let table_campus = $("#ContentPlaceHolder1_GridCampus").DataTable({
                select: {
                    style: 'single', info: false
                },
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
                            columns: [0, 1, 2, 3, 5, 6]
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
                            columns: [0,1,2, 3, 5, 6]
                        }
                    }
                ]
            });
            $('#ContentPlaceHolder1_GridCampus tbody').on('click', 'tr', function () {
                var data = table_campus.row(this).data();
                var clave_campus = data[0];
                var nombre_campus = data[1];
                var abr_campus = data[2];
                var rfc_campus = data[3];
                var estatus_campus = data[4];
                var pais_campus = data[7];
                var estado_campus = data[8];
                var n_estado_campus = data[9];
                var dele_campus = data[10];
                var n_dele_campus = data[11];
                var zip_campus = data[12];
                var col_campus = data[13];
                var dir_campus = data[14];

                if ($(this).hasClass("selected")) {
                    $("#add_Campus").fadeIn("fast");
                    $("#ContentPlaceHolder1_form_Campus").slideUp("slow");
                    $("#ContentPlaceHolder1_btn_campus").fadeOut("slow");
                    $("#ContentPlaceHolder1_guardar_campus").css({ display: "none" });
                } else {
                    $("#ContentPlaceHolder1_c_campus").val(clave_campus);
                    $("#ContentPlaceHolder1_c_campus").prop("readonly", true);
                    $("#ContentPlaceHolder1_n_campus").val(nombre_campus);
                    $("#ContentPlaceHolder1_a_campus").val(abr_campus);
                    $("#ContentPlaceHolder1_RFC_campus").val(rfc_campus);
                    $("#ContentPlaceHolder1_estatus_campus").find('option[value="' + estatus_campus + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_ddp_campus").find('option[value="' + pais_campus + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_ddp_campus").prop("disabled", true);
                    $("#ContentPlaceHolder1_hd_ddp_campus").val(pais_campus);
                    $('#ContentPlaceHolder1_dde_campus').append(new Option(n_estado_campus, estado_campus, false, true));
                    $("#ContentPlaceHolder1_dde_campus").find('option[value="' + estado_campus + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_dde_campus").prop("disabled", true);
                    $("#ContentPlaceHolder1_hd_dde_campus").val(estado_campus);
                    $("#ContentPlaceHolder1_ddd_campus").find('option[value="' + dele_campus + '"]').prop("selected", true);
                    $('#ContentPlaceHolder1_ddd_campus').append(new Option(n_dele_campus, dele_campus, false, true));
                    $("#ContentPlaceHolder1_ddd_campus").prop("disabled", true);
                    $("#ContentPlaceHolder1_hd_ddd_campus").val(dele_campus);
                    $("#ContentPlaceHolder1_zip_campus").val(zip_campus);
                    $("#ContentPlaceHolder1_zip_campus").prop("readonly", true);
                    $("#ContentPlaceHolder1_ddz_campus").find('option[value="' + zip_campus + '"]').prop("selected", true);
                    $('#ContentPlaceHolder1_ddz_campus').append(new Option(col_campus, zip_campus, false, true));
                    $("#ContentPlaceHolder1_ddz_campus").prop("disabled", true);
                    $("#ContentPlaceHolder1_hd_ddz_campus").val(col_campus);
                    $("#ContentPlaceHolder1_direc_campus").val(dir_campus);
                    $("#ContentPlaceHolder1_direc_campus").prop("disabled", true);
                    $("#add_Campus").fadeOut("fast");
                    $("#ContentPlaceHolder1_form_Campus").slideDown("slow");
                    $("#ContentPlaceHolder1_actualizar_campus").css({ display: "initial" });
                    $("#ContentPlaceHolder1_guardar_campus").css({ display: "none" });
                    $("#ContentPlaceHolder1_btn_campus").fadeIn("slow");
                    $("#ContentPlaceHolder1_upd_btn_dir").css({ display: "initial" });
                }
            });
        });

        function nuevo_campus() {
            $("#add_Campus").fadeOut("fast");
            $("#ContentPlaceHolder1_form_Campus").slideDown("slow");
            $("#ContentPlaceHolder1_btn_campus").fadeIn("slow");
            $("#ContentPlaceHolder1_actualizar_campus").css({ display: "none" });

        }
        function cancelar_campus() {
            $("#ContentPlaceHolder1_GridCampus tbody tr").removeClass("selected");
            $("#add_Campus").fadeIn("fast");
            $("#ContentPlaceHolder1_form_Campus").slideUp("slow");
            $("#ContentPlaceHolder1_btn_campus").fadeOut("slow");

        }

        function update_campus() {
            $("#add_Campus").fadeOut("fast");
            $("#ContentPlaceHolder1_form_Campus").slideDown("slow");
            $("#ContentPlaceHolder1_btn_campus").fadeIn("slow");
            $("#ContentPlaceHolder1_guardar_campus").css({ display: "none" });
            $("#ContentPlaceHolder1_c_campus").prop("readonly", true);
            $("#ContentPlaceHolder1_ddp_campus").prop("disabled", true);
            $("#ContentPlaceHolder1_dde_campus").prop("disabled", true);
            $("#ContentPlaceHolder1_ddd_campus").prop("disabled", true);
            $("#ContentPlaceHolder1_zip_campus").prop("readonly", true);
            $("#ContentPlaceHolder1_ddz_campus").prop("disabled", true);
            $("#ContentPlaceHolder1_direc_campus").prop("disabled", true);
            $("#ContentPlaceHolder1_upd_btn_dir").css({ display: "initial" });

        }
    </script>
    <!--\Funciones pestaña Campus-->
    <!--Funciones pestaña Programas-->
    <script>
        function load_datatable() {
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
            });

            let table_programas = $("#ContentPlaceHolder1_GridProgramas").DataTable({
                select: {
                    style: 'single', info: false
                },
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
                        title: 'SAES_Catálogo de Campus-Programas',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 6, 7]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus-Programas',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 6,7]
                        }
                    }
                ]
            });

            $('#ContentPlaceHolder1_GridProgramas tbody').on('click', 'tr', function () {
                var data = table_programas.row(this).data();
                var clave_programa = data[0];
                var nombre_programa = data[1];
                var admision_programa = data[4];
                var estatus_programa = data[5];
                if ($(this).hasClass("selected")) {

                    $("#ContentPlaceHolder1_c_prog_campus").val('');
                    $("#ContentPlaceHolder1_n_prog_campus").val('');
                    $("#ContentPlaceHolder1_guardar_prog").css({ display: "initial" });
                    $("#ContentPlaceHolder1_update_prog").css({ display: "none" });
                    $("#ContentPlaceHolder1_c_prog_campus").prop("readonly", false);
                    $("#ContentPlaceHolder1_n_prog_campus").prop("readonly", false);
                }
                else {
                    $("#ContentPlaceHolder1_c_prog_campus").val(clave_programa);
                    $("#ContentPlaceHolder1_n_prog_campus").val(nombre_programa);
                    $("#ContentPlaceHolder1_c_prog_campus").prop("readonly", true);
                    $("#ContentPlaceHolder1_n_prog_campus").prop("readonly", true);
                    $("#ContentPlaceHolder1_guardar_prog").css({ display: "none" });
                    $("#ContentPlaceHolder1_update_prog").css({ display: "initial" });
                    $("#ContentPlaceHolder1_e_prog_campus").find('option[value="' + estatus_programa + '"]').prop("selected", true);
                    if (admision_programa == "S") {
                        $("#customSwitches").prop("checked", true);
                        $("#ContentPlaceHolder1_checked_input").val('1')
                    } else {
                        $("#customSwitches").prop("checked", false);
                        $("#ContentPlaceHolder1_checked_input").val('0')
                    }
                    validarclavePrograma('ContentPlaceHolder1_c_prog_campus');
                }                
            });
        }

        function btn_programa(){
            $("#ContentPlaceHolder1_btn_programa").fadeIn("slow");
            $("#ContentPlaceHolder1_tabla_programas").fadeIn("slow");
        }
        function btn_programa_ocultar() {
            $("#ContentPlaceHolder1_btn_programa").fadeOut("slow");
            $("#tabla_programas").fadeOut("fast");
        }


    </script>
    <!--\Funciones pestaña Campus-->
    <!--Funciones pestaña cobranza-->
    <script>
        function load_datatable_cobranza() {
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
            });

            let table_programas = $("#ContentPlaceHolder1_GridCobranza").DataTable({
                select: {
                    style: 'single', info: false
                },
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
                ]
            });

            $('#ContentPlaceHolder1_GridCobranza tbody').on('click', 'tr', function () {
                var data = table_programas.row(this).data();
                var c_campus = data[0];
                var c_nivel = data[2];
                var c_tipo_p = data[4];
                var periodo = data[12];
                var conce_cal = data[8];
                var conce_cob = data[10];
                var desc_ins = data[6];
                var desc_col = data[7];
                if ($(this).hasClass("selected")) {
                   
                }
                else {
                    $('#ContentPlaceHolder1_campus').val(c_campus);
                    $('#ContentPlaceHolder1_nivel').val(c_nivel);
                    $('#ContentPlaceHolder1_t_periodo').val(c_tipo_p);
                    $('#ContentPlaceHolder1_periodo_txt').val(periodo);
                    $('#ContentPlaceHolder1_conce_cal_txt').val(conce_cal);
                    $('#ContentPlaceHolder1_conce_cob_txt').val(conce_cob);
                    $('#ContentPlaceHolder1_desc_ins').val(desc_ins);
                    $('#ContentPlaceHolder1_desc_col').val(desc_col);
                    $('#ContentPlaceHolder1_btn_oculto').trigger('click');                    
                }
            });
        }        
    </script>
    <!--\Funciones pestaña cobranza-->
</asp:Content>
