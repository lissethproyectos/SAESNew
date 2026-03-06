<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcoco.aspx.cs" Inherits="SAES_v1.tcoco" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />

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
        //---- Cuenta Contable ----//
        function validar_Contable(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar cuenta contable');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Validación de Naturaleza----//
        function validar_naturaleza(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Naturaleza');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Validación de Categoria----//
        function validar_categoria(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Categoría');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Validación de Naturaleza----//
        function validar_estatus(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Estatus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos Periodo ----//
        function validar_campos_tcoco(e) {
            event.preventDefault(e);
            validarClave('ContentPlaceHolder1_txt_tcoco', 0);
            validarNombre('ContentPlaceHolder1_txt_nombre');
            validar_Contable('ContentPlaceHolder1_txt_contable');
            validar_naturaleza('ContentPlaceHolder1_ddl_naturaleza');
            validar_categoria('ContentPlaceHolder1_ddl_categoria');
            validar_estatus('ContentPlaceHolder1_ddl_estatus');
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Conceptos Cobranza</h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="form-row">
            <div class="col-sm-2">
                <label for="ContentPlaceHolder1_txt_tcoco" class="form-label">Clave</label>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_tcoco" MaxLength="4" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Descripción</label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-1" style="text-align: center;">
                <label for="ContentPlaceHolder1_customSwitches1" class="form-label">Parcialidad</label>
                <div class="custom-control custom-switch">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <input type="checkbox" class="custom-control-input" id="customSwitches1" name="customSwitches1">
                            <label class="custom-control-label" for="customSwitches1"></label>
                            <asp:HiddenField ID="checked_input1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-1" style="text-align: center;">
                <label for="ContentPlaceHolder1_customSwitches2" class="form-label">Reembolso</label>
                <div class="custom-control custom-switch">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <input type="checkbox" class="custom-control-input" id="customSwitches2" name="customSwitches2">
                            <label class="custom-control-label" for="customSwitches2"></label>
                            <asp:HiddenField ID="checked_input2" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-1" style="text-align: center;">
                <label for="ContentPlaceHolder1_customSwitches3" class="form-label">IVA</label>
                <div class="custom-control custom-switch">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <input type="checkbox" class="custom-control-input" id="customSwitches3" name="customSwitches3">
                            <label class="custom-control-label" for="customSwitches3"></label>
                            <asp:HiddenField ID="checked_input3" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
        <div class="form-row">
            <div class="col-sm-3">
                <label for="ContentPlaceHolder1_txt_contable" class="form-label">Cuenta Contable</label>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_contable" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                <label for="ContentPlaceHolder1_ddl_naturaleza" class="form-label">Naturaleza</label>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_naturaleza" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                <label for="ContentPlaceHolder1_ddl_categoria" class="form-label">Categoría</label>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_categoria" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <div class="row" id="btn_tcoco" runat="server">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtcoco" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtcoco_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                                <asp:BoundField DataField="PARCIALIDAD" HeaderText="Parcialidad" />
                                <asp:BoundField DataField="REEMBOLSO" HeaderText="Reembolso" />
                                <asp:BoundField DataField="IVA" HeaderText="IVA" />
                                <asp:BoundField DataField="CONTABLE" HeaderText="Cta.contable" />
                                <asp:BoundField DataField="C_NATURALEZA" HeaderText="NATURALEZA">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="C_CATEGORIA" HeaderText="Categoria">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>

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

        $("#customSwitches1").on("change", function () {
            if (this.checked) {
                $("#ContentPlaceHolder1_checked_input1").val('1')

            } else {
                $("#ContentPlaceHolder1_checked_input1").val('0')
            }
            console.log($("#customSwitches1").val());
            console.log($("#ContentPlaceHolder1_checked_input1").val());
        });
        $("#customSwitches2").on("change", function () {
            if (this.checked) {
                $("#ContentPlaceHolder1_checked_input2").val('1')

            } else {
                $("#ContentPlaceHolder1_checked_input2").val('0')
            }
            console.log($("#customSwitches2").val());
            console.log($("#ContentPlaceHolder1_checked_input2").val());
        });
        $("#customSwitches3").on("change", function () {
            if (this.checked) {
                $("#ContentPlaceHolder1_checked_input3").val('1')

            } else {
                $("#ContentPlaceHolder1_checked_input3").val('0')
            }
            console.log($("#customSwitches3").val());
            console.log($("#ContentPlaceHolder1_checked_input3").val());
        });

        function load_datatable() {
            let table_periodo = $("#ContentPlaceHolder1_Gridtcoco").DataTable({
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
                        title: 'SAES_Coneptos Cobranza',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                        }
                    },
                    {
                        title: 'SAES_Conceptos Cobranza',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function destroy_table() {
            $("#ContentPlaceHolder1_Gridttcoco").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }



        function desactivar_check1() {
            $("#customSwitches1").attr('checked', false);
        }
        function desactivar_check2() {
            $("#customSwitches2").attr('checked', false);
        }
        function desactivar_check3() {
            $("#customSwitches3").attr('checked', false);
        }

        function activar_check1() {
            $("#customSwitches1").attr('checked', true);
        }
        function activar_check2() {
            $("#customSwitches2").attr('checked', true);
        }
        function activar_check3() {
            $("#customSwitches3").attr('checked', true);
        }

    </script>
</asp:Content>


