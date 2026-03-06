<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tbapa.aspx.cs" Inherits="SAES_v1.tbapa" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />

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



        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error_transaccion(message) {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- ' + message + '</h2>'
            })
        }


        function error_validacionFechas() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- La fecha de inicio no puede ser mayor a la fecha de termino</h2>'
            })
        }

        function error_traslape() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Fechas de configuración se traslapan </h2>'
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
                html: '<h2 class="swal2-title" id="swal2-title">Se actualizaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
        }

        function noexist() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">No existen datos para mostrar</h2>Favor de validar en el catálogo.'
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
                errorForm("ContentPlaceHolder1_ddl_periodo", 'Selecciona periodo');
            else
                validadoForm("ContentPlaceHolder1_ddl_periodo");

            var Campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
            console.log(Campus);
            if (Campus == "")
                errorForm("ContentPlaceHolder1_ddl_campus", 'Selecciona campus');
            else
                validadoForm("ContentPlaceHolder1_ddl_campus");

            var Nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
            console.log(Nivel);
            if (Nivel == "")
                errorForm("ContentPlaceHolder1_ddl_nivel", 'Selecciona nivel');
            else
                validadoForm("ContentPlaceHolder1_ddl_nivel");
        }

        function validar_campos_tbapa_insert(e) {
            event.preventDefault(e);
            var Periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;
            if (Periodo == "")
                errorForm("ContentPlaceHolder1_ddl_periodo", 'Selecciona periodo');
            else
                validadoForm("ContentPlaceHolder1_ddl_periodo");

            var Campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
            if (Campus == "")
                errorForm("ContentPlaceHolder1_ddl_campus", 'Selecciona campus');
            else
                validadoForm("ContentPlaceHolder1_ddl_campus");

            var Nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
            if (Nivel == "")
                errorForm("ContentPlaceHolder1_ddl_nivel", 'Selecciona nivel');
            else
                validadoForm("ContentPlaceHolder1_ddl_nivel");

            var porcBaja = document.getElementById("ContentPlaceHolder1_txt_porcentaje").value;
            if (porcBaja == "" || porcBaja == "0")
                errorForm("ContentPlaceHolder1_txt_porcentaje", 'Capturar porcentaje');
            else
                validadoForm("ContentPlaceHolder1_txt_porcentaje");

            var FechaInicio = document.getElementById("ContentPlaceHolder1_txt_fecha_l").value;
            if (FechaInicio == "")
                errorForm("ContentPlaceHolder1_datepicker", 'Capturar fecha Inicio');
            else
                validadoForm("ContentPlaceHolder1_txt_fecha_l");

            var FechaFin = document.getElementById("ContentPlaceHolder1_txt_fecha_f").value;
            if (FechaFin == "")
                errorForm("ContentPlaceHolder1_txt_fecha_f", 'Capturar fecha Fin');
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

        function load_datatable() {
            let table_solicitudes = $("#ContentPlaceHolder1_Gridtbapa").DataTable({
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
                        title: 'SAES_Parametros de devolución',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        }
                    },
                    {
                        title: 'SAES_Parametros de devolución',
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
                stateSave: true
            });
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
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="x_title">
        <h2>
            <i class="fa fa-share-square-o" aria-hidden="true"></i>&nbsp;Parametros de devolución
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tcodo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tcodo" runat="server">
                    <div class="form-row">
                        <div class="col-sm-4">
                            <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Operación/Periodos Escolares"></i>&nbsp;
Periodo
                            <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged"></asp:DropDownList>

                        </div>
                        <div class="col-sm-4">
                            <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Ruta Operación/Campus/Catálogo de Campus"></i>&nbsp;Campus
                            <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4">
                            Nivel
                            <asp:DropDownList ID="ddl_nivel" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-2">
                            % Dev. Baja
                            <asp:TextBox ID="txt_porcentaje" runat="server" class="form-control" Text="0" Style="text-align: right"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            Fecha Inicio
                            <asp:TextBox ID="txt_fecha_l" runat="server" CssClass="form-control"></asp:TextBox>
                            <script>
                                function ctrl_fecha_i() {
                                    $('#ContentPlaceHolder1_txt_fecha_l').datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                        <div class="col-sm-3">
                            Fecha Termino
                            <asp:TextBox ID="txt_fecha_f" runat="server" CssClass="form-control"></asp:TextBox>
                            <script>
                                function ctrl_fecha_f() {
                                    $('#ContentPlaceHolder1_txt_fecha_f').datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcodo" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" Visible="false" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                        <asp:Button ID="btn_search" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" Visible="false" />
                    </div>
                </div>
                <div id="table_tcodo">
                    <asp:GridView ID="Gridtbapa" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtbapa_SelectedIndexChanged" EmptyDataText="No existen parámetros." ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Consecutivo" HeaderText="No.Consecutivo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Porcentaje" HeaderText="% Dev. Baja" />
                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="FechaFin" HeaderText="Fecha Termino" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
