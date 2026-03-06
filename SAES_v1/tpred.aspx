<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpred.aspx.cs" Inherits="SAES_v1.tpred" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />

    <style>
        span button {
            margin-bottom: 0px !important;
        }

        .card-header .title {
            font-size: 17px;
            color: #000;
        }

        .card-header .accicon {
            float: right;
            font-size: 20px;
            width: 1.2em;
        }

        .card-header {
            cursor: pointer;
            border-bottom: none;
        }

        .card {
            border: 1px solid #ddd;
        }

        .card-body {
            border-top: 1px solid #ddd;
        }

        .card-header:not(.collapsed) .rotate-icon {
            transform: rotate(180deg);
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

        //---- Clave ----//
        function validarClave(idEl, ind) {
            const idElemento = idEl;
            if (ind == 0) {
                let clave = document.getElementById(idElemento).value;
                if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                    errorForm(idElemento, 'Favor de ingresar Matrícula');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            } else {
                errorForm(idElemento, 'La clave ingresada ya existe');
                return false;
            }

        }
        //---- Nombre ----//
        function validarNombre(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar Clave válida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarPeriodo(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar Periodo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarOrigen(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar Carrera origen');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarFolio(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Favor de ingresar Folio');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        function validarFechaInicio(idEl) {
            const idElemento = idEl;
            let fecha = document.getElementById(idElemento).value;
            if (fecha == null || fecha.length == 0 || /^\s+$/.test(fecha)) {
                errorForm(idElemento, 'Favor de ingresar fecha valida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }


        //---- Valida Campos Periodo ----//
        function validar_campos_tpreq(e) {
            event.preventDefault(e);
            validarClave('ContentPlaceHolder1_TxtCuenta', 0);
            validarNombre('ContentPlaceHolder1_txt_esc_proc');
            validarFechaInicio('ContentPlaceHolder1_txt_fecha_i');
            validarPeriodo('ContentPlaceHolder1_txt_periodo');
            validarOrigen('ContentPlaceHolder1_txt_origen');
            validarFolio('ContentPlaceHolder1_txt_folio');
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-file-text" aria-hidden="true"></i>&nbsp;Materias en Predictamen</h2>
        <div class="clearfix"></div>
    </div>


    <div class="x_content">

        <div id="form_periodos" runat="server">
            <div class="form-row">
                <div class="col-sm-2">
                    Matrícula
                <asp:UpdatePanel ID="updPnlBusca" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="TxtCuenta" MaxLength="10" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="TxtCuenta_TextChanged"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                <div class="col-sm-10">
                    Nombre(s)
                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                  <ContentTemplate>
                                      <asp:TextBox ID="TxtNombre" MaxLength="10" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-row" id="containerGridAlumnos" runat="server" visible="false">
                        <div class="col">
                            <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtpers_SelectedIndexChanged" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="tpers_num" HeaderText="Id">
                                        <HeaderStyle CssClass="ocultar" />
                                        <ItemStyle CssClass="ocultar" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tpers_clave" HeaderText="Matricula" />
                                    <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="tpers_paterno" HeaderText="Paterno" />
                                    <asp:BoundField DataField="tpers_materno" HeaderText="Materno" />
                                    <asp:BoundField DataField="tpers_cgenero" HeaderText="Genero" />
                                    <asp:BoundField DataField="tpers_curp" HeaderText="CURP" />
                                </Columns>
                                <RowStyle Font-Size="Small" />
                                <SelectedRowStyle CssClass="selected_table" />
                                <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-row">
                <div class="col-sm-12">
                    Programa
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged">
                                 <asp:ListItem>-------</asp:ListItem>
                             </asp:DropDownList>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                </div>
            </div>

            <%--<div class="accordion" id="accordionExample">
                        <div class="card">
                            <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                <span class="title">Programa</span>
                                <span class="accicon"><i class="fas fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseOne" class="collapse show" data-parent="#accordionExample">
                                <div class="card-body">
                                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
               
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <span class="title">Collapsible Group Item #2</span>
                                <span class="accicon"><i class="fas fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseTwo" class="collapse" data-parent="#accordionExample">
                                <div class="card-body">
                                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
               
                                </div>
                            </div>
                        </div>

                    </div>--%>




            <br />
            <div class="card bg-light">
                <div class="card-header font-weight-bold">
                    <h5>Revalidación / Equivalencia</h5>
                </div>
                <div class="card-body">
                    <div class="form-row">
                        <div class="col-sm-6">
                            <label for="ContentPlaceHolder1_txt_esc_proc" class="form-label">Escuela de Procedencia</label>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlEscuelaProcedencia" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="col-md-6">
                            <label for="ContentPlaceHolder1_txt_origen" class="form-label">Carrera Origen</label>
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_origen" MaxLength="80" runat="server" CssClass="form-control"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_txt_folio" class="form-label">Folio</label>
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_folio" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_txt_fecha_i" class="form-label">Fecha Dictamen</label>
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_fecha_i" runat="server" class="form-control"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <script>
                                function ctrl_fecha_i() {

                                    $('#ContentPlaceHolder1_txt_fecha_i').datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: "dd/mm/yyyy"
                                    });
                                }
                            </script>
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <label for="ContentPlaceHolder1_txt_periodo" class="form-label">Periodo Aplicación</label>
                                    <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                    <%--<div class="input-group">
                                        <asp:TextBox ID="txt_periodo" MaxLength="6" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        <asp:LinkButton ID="linkBttnBuscarPeriodo" class="btn btn-success" runat="server" OnClick="linkBttnBuscarPeriodo_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                    </div>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%-- <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_txt_nom_per" class="form-label">Nombre Periodo</label>
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_nom_per" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>--%>
                        <div class="col-sm-5">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-center">
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar Escuela" OnClick="btn_save_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridTpreq" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridTpreq_SelectedIndexChanged" BorderWidth="2px" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron carreras.">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="clave" HeaderText="Clave" />
                                            <asp:BoundField DataField="Escuela" HeaderText="Escuela" />
                                            <asp:BoundField DataField="carrera" HeaderText="Carrera" />
                                            <asp:BoundField DataField="folio" HeaderText="Folio" />
                                            <asp:BoundField DataField="fecha_dict" HeaderText="Fecha Dictamen" />
                                            <asp:BoundField DataField="estatus" HeaderText="Estatus" />
                                            <asp:BoundField DataField="fecha" HeaderText="Fecha Registro" />
                                        </Columns>
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="Plan" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True" OnRowEditing="Plan_RowEditing" OnRowDataBound="Plan_RowDataBound" OnRowUpdating="Plan_RowUpdating" OnRowCancelingEdit="Plan_RowCancelingEdit">
                                        <Columns>
                                            <asp:BoundField DataField="consec" HeaderText="Consecutivo" ReadOnly="true" />
                                            <asp:BoundField DataField="area" HeaderText="Area" ReadOnly="true" />
                                            <asp:BoundField DataField="clave" HeaderText="Clave" ReadOnly="true" />
                                            <asp:BoundField DataField="materia" HeaderText="Materia" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Materia Origenización / Calificación Estatus" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <%--<asp:Button ID="btn_tpred" runat="server" CssClass="btn btn-success" Text="Guardar PreDictamen" OnClick="Agregar_Materias" />--%>
                                                    <%--                                                    <asp:LinkButton ID="linkBttnGuardarPedictamen" runat="server" CssClass="btn btn-info" OnClick="linkBttnGuardarPedictamen_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Guardar PreDictamen</asp:LinkButton>--%>
                                                    <asp:LinkButton ID="linkBttnImprimirPedictamen" runat="server" CssClass="btn btn-info" OnClick="linkBttnImprimirPedictamen_Click"><i class="fa fa-print" aria-hidden="true"></i> Imprimir</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-8">
                                                            Materia Origen
                                                    <asp:TextBox ID="mat_origen" runat="server" CssClass="form-control" Text='<%# Bind("sugerida") %>' Enabled="False" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            Calificación
                                                    <asp:DropDownList ID="CboCalif" runat="server" CssClass="form-control" Enabled="False"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            Estatus
                                                    <asp:DropDownList ID="CboSt" runat="server" CssClass="form-control" Enabled="False">
                                                        <asp:ListItem Value="A">Activo</asp:ListItem>
                                                        <asp:ListItem Value="B">Baja</asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="55%" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" />
                                            <asp:BoundField DataField="tpred_estatus" HeaderText="Estatus" ReadOnly="true">
                                                <HeaderStyle CssClass="ocultar" />
                                                <ItemStyle CssClass="ocultar" />
                                                <FooterStyle CssClass="ocultar" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cali" HeaderText="Estatus" ReadOnly="true">
                                                <HeaderStyle CssClass="ocultar" />
                                                <ItemStyle CssClass="ocultar" />
                                                <FooterStyle CssClass="ocultar" />
                                            </asp:BoundField>
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
            </div>








            <div class="row justify-content-right" style="text-align: right; margin: auto;" id="Div2" runat="server">
                <div class="col-md-10" style="text-align: right; margin-top: 15px;">
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="resultado" runat="server" CssClass="bold" ForeColor="Red" Width="200px"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-row">
                <div class="col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                            <%--                            <asp:Button ID="btn_tpred" runat="server" CssClass="btn btn-round btn-success" Text="Guardar PreDictamen" OnClick="Agregar_Materias" Visible="false" />--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                            <%--                            <asp:Button ID="btn_pdf" runat="server" CssClass="btn btn-round btn-success" Text="Imprimir" OnClick="PDF_Click" Visible="false" />--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>




            <!-- Modal Escuelas-->
            <%--<div class="modal" id="modalEscuelas" tabindex="-1" role="dialog" aria-labelledby="modalEscuelasLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalEscuelasLabel">Escuelas</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridEscuelas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" OnSelectedIndexChanged="GridEscuelas_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                            <asp:BoundField DataField="Escuela" HeaderText="Escuela" />
                                        </Columns>
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>--%>








            <!-- Modal Periodos-->
            <div class="modal fade bd-example-modal-lg" id="modalPeriodos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Periodos</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridPeriodos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridPeriodos_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                                            <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                        </Columns>
                                        <SelectedRowStyle CssClass="selected_table" />
                                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
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

        </div>

        <script>
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


<%--            function load_datatable_Escuelas() {
                $('#<%= GridEscuelas.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridEscuelas.ClientID %>').find("tr:first"))).DataTable({
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
                    ordering: false
                });
            }--%>

            function load_datatable_Periodos() {
                $('#<%= GridPeriodos.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridPeriodos.ClientID %>').find("tr:first"))).DataTable({

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
                    ordering: false
                });
            }

            function load_datatable_Plan() {
                $('#<%= Plan.ClientID %>').prepend($("<thead></thead>").append($('#<%= Plan.ClientID %>').find("tr:first"))).DataTable({
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

            function destroy_table() {
                $("#ContentPlaceHolder1_Gridtespr").DataTable().destroy();
            }
            function remove_class() {
                $('.selected_table').removeClass("selected_table")
            }
        </script>
    </div>
</asp:Content>



