<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ttiop.aspx.cs" Inherits="SAES_v1.ttiop" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">

    <style>
        span button {
            margin-bottom: 0px !important;
        }

        .icon_regresa {
            width: 100%;
            text-align: center;
            border-color: #FFF !important;
        }

            .icon_regresa:hover {
                background-color: #fff !important;
                color: #26b99a;
            }
    </style>

    <script>

        document.addEventListener("DOMContentLoaded", function (event) {

        });

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
                    errorForm(idElemento, 'Favor de ingresar clave válida');
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
                errorForm(idElemento, 'Favor de ingresar descripción válida');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }


        //---- Valida Campos Periodo ----//
        function validar_campos_tbapa_consulta(e) {
            event.preventDefault(e);
            var Periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;
            console.log(Periodo);
            if (Periodo == "")
                errorForm("ContentPlaceHolder1_ddl_periodo", 'Selecciona un periodo');
            else
                validadoForm("ContentPlaceHolder1_ddl_periodo");

            var Campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
            console.log(Campus);
            if (Campus == "")
                errorForm("ContentPlaceHolder1_ddl_campus", 'Selecciona un campus');
            else
                validadoForm("ContentPlaceHolder1_ddl_campus");

            var Nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
            console.log(Nivel);
            if (Nivel == "")
                errorForm("ContentPlaceHolder1_ddl_nivel", 'Selecciona un nivel');
            else
                validadoForm("ContentPlaceHolder1_ddl_nivel");
        }

        function validar_campos_tbapa_insert(e) {
            event.preventDefault(e);
            var Periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;
            if (Periodo == "")
                errorForm("ContentPlaceHolder1_ddl_periodo", 'Selecciona un periodo');
            else
                validadoForm("ContentPlaceHolder1_ddl_periodo");

            var Campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
            if (Campus == "")
                errorForm("ContentPlaceHolder1_ddl_campus", 'Selecciona un campus');
            else
                validadoForm("ContentPlaceHolder1_ddl_campus");

            var Nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
            if (Nivel == "")
                errorForm("ContentPlaceHolder1_ddl_nivel", 'Selecciona un nivel');
            else
                validadoForm("ContentPlaceHolder1_ddl_nivel");

            var porcBaja = document.getElementById("ContentPlaceHolder1_txt_porcentaje").value;
            if (porcBaja == "" || porcBaja == "0")
                errorForm("ContentPlaceHolder1_txt_porcentaje", 'Indica un porcentaje de devolucion de baja');
            else
                validadoForm("ContentPlaceHolder1_txt_porcentaje");

            var FechaInicio = document.getElementById("ContentPlaceHolder1_txt_fecha_l").value;
            if (FechaInicio == "")
                errorForm("ContentPlaceHolder1_txt_fecha_l", 'Elige una fecha de Inicio');
            else
                validadoForm("ContentPlaceHolder1_txt_fecha_l");

            var FechaFin = document.getElementById("ContentPlaceHolder1_txt_fecha_f").value;
            if (FechaFin == "")
                errorForm("ContentPlaceHolder1_txt_fecha_f", 'Elige una fecha de termino');
            else
                validadoForm("ContentPlaceHolder1_txt_fecha_f");
        }

        function checkValue(event) {
            var key = event.which || event.keyCode,
                value = event.target.value,
                n = value + String.fromCharCode(key);
            var isValid = n.match(/^((100(\.0{1,2})?)|(\d{1,2}(\.\d{1,2})?))$/) == null ? false : true;
            if (isValid)
                return true;
            else
                return false;
        }

        function destroy_table() {
            $("#Gridtbapa").DataTable().destroy();
        }
        function destroy_table() {
            $("#Gridtbapa").DataTable().destroy();
        }
        function remove_class() {
            $("#Gridtbapa").removeClass("selected_table")
        }
        function cleartxt_matricula(val) {
            $('#ContentPlaceHolder1_txtMatricula').val(val);
        }
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="x_title">
        <h2>
            <i class="fa fa-folder-open" aria-hidden="true"></i>
            &nbsp;Catálógo de Opciones de Titulación
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tcodo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tcodo" runat="server">
                    <div class="form-row">
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_txb_claveTitulacion" class="form-label">Clave Titulación</label>
                            <asp:TextBox ID="txb_claveTitulacion" runat="server" CssClass="form-control" AutoPostBack="false"></asp:TextBox>
                            <asp:HiddenField ID="hfd_numero_persona" runat="server" Value="" />
                        </div>
                        <div class="col-sm-6">
                            <label for="ContentPlaceHolder1_txb_descripcion" class="form-label">Descripción</label>
                            <asp:TextBox ID="txb_descripcion" runat="server" CssClass="form-control" AutoPostBack="false"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_txb_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        &nbsp;                
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" ValidationGroup="guardar" />
                        &nbsp;
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="btn_update_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="Gridttiop" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridttiop_SelectedIndexChanged">
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
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" DataFormatString="{0:dd/MM/yyyy}" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="GridttiopDet" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small">
                            <Columns>
                                <asp:TemplateField HeaderText="Nivel">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DDL_Nivel" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Créditos">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txb_Creditos" Text="" CssClass="form-control" runat="server" AutoPostBack="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Promedio">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txb_Promedio" Text="" CssClass="form-control" runat="server" AutoPostBack="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DDL_Codigo" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>



            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>
