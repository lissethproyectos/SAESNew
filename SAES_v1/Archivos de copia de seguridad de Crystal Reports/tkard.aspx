<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tkard.aspx.cs" Inherits="SAES_v1.tkard" %>

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

    <%--<script language="javascript" type="text/javascript">
        $(function () {            
            $('#<%=txb_matricula.ClientID%>').autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "tkard.aspx/GetCompanyName",
                    data: "{ 'search':'" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return { value: item.Nombre }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Alumno no encontrado");
                    }
                });
                },
            select: function (e, u) {
                    $("#ContentPlaceHolder1_hfd_numero_persona").val(u.item.value);
            }
        });
    });
    </script>--%>

    <script>
        //function load_datatable() {
        //    $("#ContentPlaceHolder1_GridSolicitudes").DataTable({
        //        language: {
        //            bProcessing: 'Procesando...',
        //            sLengthMenu: 'Mostrar _MENU_ registros',
        //            sZeroRecords: 'No se encontraron resultados',
        //            sEmptyTable: 'Ningún dato disponible en esta tabla',
        //            sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
        //            sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
        //            sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
        //            sInfoPostFix: '',
        //            sSearch: 'Buscar:',
        //            sUrl: '',
        //            sInfoThousands: '',
        //            sLoadingRecords: 'Cargando...',
        //            oPaginate: {
        //                sFirst: 'Primero',
        //                sLast: 'Último',
        //                sNext: 'Siguiente',
        //                sPrevious: 'Anterior'
        //            }
        //        },
        //        scrollResize: true,
        //        scrollY: '500px',
        //        scrollCollapse: true,
        //        order: [
        //            [0, "asc"]
        //        ],
        //        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
        //        "autoWidth": true,
        //        dom: '<"top"if>rt<"bottom"lBp><"clear">',
        //        buttons: [

        //        ],
        //        stateSave: true
        //    });
        //}
    </script>

    <script>
        function load_datatable_Gridtkard() {
            $("#ContentPlaceHolder1_Gridtkard").DataTable({
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="x_title">
        <h2>
            <i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Kardex (Historial Académico)</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tcodo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tcodo" runat="server">
                    <div class="form-row">
                        <%-- <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="txb_matricula_OnTextChanged"/>
                        </div>--%>
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_txb_matricula" class="form-label">Matricula</label>
                            <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                <ContentTemplate>
                                    <div class="input-group">
                                        <asp:TextBox ID="txb_matricula" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        <asp:HiddenField ID="hfd_numero_persona" runat="server" Value="" />
                                        <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_txb_nombre" class="form-label">Nombre(s)</label>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txb_nombre" runat="server" CssClass="form-control" AutoPostBack="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_txb_apellido_p" class="form-label">Apellido Paterno</label>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txb_apellido_p" runat="server" CssClass="form-control" AutoPostBack="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_txb_apellido_m" class="form-label">Apellido Materno</label>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txb_apellido_m" runat="server" CssClass="form-control" AutoPostBack="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div id="col">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridSolicitudes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridSolicitudes_SelectedIndexChanged" Visible="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CLAVE" HeaderText="Matrícula" />
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                            <asp:BoundField DataField="PATERNO" HeaderText="Apellido Paterno" />
                                            <asp:BoundField DataField="MATERNO" HeaderText="Apellido Materno" />
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
                        <div class="col-sm-12">
                            <label for="ContentPlaceHolder1_ddl_programa" class="form-label">Programa</label>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row g-6 justify-content-center" style="margin-top: 15px;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Gridtkard_Captura" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small">
                                    <Columns>
                                        <%--<asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />--%>
                                        <asp:TemplateField HeaderText="Periodo">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_Periodo_Insert" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Clave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txb_Clave_Insert" Text="" CssClass="form-control" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materia">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_Materia_Insert" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_clave_insert_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grupo">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txb_Grupo_Insert" Text="" CssClass="form-control" runat="server" AutoPostBack="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_Calificacion_Insert" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acreditación">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_Acreditacion_Insert" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>
                                <asp:HiddenField ID="edit_periodo" runat="server" Value="" />
                                <asp:HiddenField ID="edit_materia" runat="server" Value="" />
                                <asp:HiddenField ID="edit_grupo" runat="server" Value="" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Label CssClass="form-label" ID="lb_Comentario" runat="server" AssociatedControlID="tbx_Comentario">Comentario</asp:Label>
                                    <asp:TextBox ID="tbx_Comentario" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcodo" runat="server">
                            <div class="col text-center">
                                <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                                <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="btn_save_Click" />
                                <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="btn_update_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row">
                    <div class="col">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Gridtkard" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtkard_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                        <asp:BoundField DataField="Materia" HeaderText="Materia" />
                                        <asp:BoundField DataField="Grupo" HeaderText="Grupo" />
                                        <asp:BoundField DataField="Calificacion" HeaderText="Calificación" />
                                        <asp:BoundField DataField="Acredita" HeaderText="Acreditación" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:TemplateField HeaderText="Auditoria">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttnVer" runat="server" CssClass="btn btn-secondary" OnClick="linkBttnVer_Click">Detalle</asp:LinkButton>
                                                <%--<asp:ImageButton ID="Imgb_ShowDetalle" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" ItemStyle-Width="70px" runat="server" OnClick="GetDetailAuditoria_click" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Font-Size="Small" />
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>
                                <asp:HiddenField ID="htbx_ClavePeriodo" Value="" runat="server" />
                                <asp:HiddenField ID="htbx_ClaveMateria" Value="" runat="server" />
                                <asp:HiddenField ID="htbx_ClaveCalificacion" Value="" runat="server" />
                                <asp:HiddenField ID="htbx_ClaveAcredita" Value="" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="row">
                    <div class="col">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gridtkarddet" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtkard_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="Consecutivo" HeaderText="Consecutivo" />
                                        <asp:BoundField DataField="Calificacion" HeaderText="Calificación" />
                                        <asp:BoundField DataField="Acreditacion" HeaderText="Acreditación" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="Comentario" HeaderText="Comentario" />
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script>
            function load_datatable_alumnos() {
                $('#<%= GridSolicitudes.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridSolicitudes.ClientID %>').find("tr:first"))).DataTable({
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
                            title: 'Cat Alumnos',
                            className: 'btn-dark',
                            extend: 'pdfHtml5',
                            text: 'Exportar PDF',
                            orientation: 'landscape',
                            exportOptions: {
                                columns: [1, 2, 3, 4, 5, 6]
                            }
                        }
                    ],
                    "stateSave": true
                });
            }

        </script>
    </div>



</asp:Content>
