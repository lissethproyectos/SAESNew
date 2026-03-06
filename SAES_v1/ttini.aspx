<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ttini.aspx.cs" Inherits="SAES_v1.ttini" %>

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

        function error_clave() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- La clave ingresada ya existe</h2>'
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

            //---- Clave Pais ----//
            function validarClave(idEl, ind) {
                const idElemento = idEl;
                if (ind == 0) {
                    let documento = document.getElementById(idElemento).value;
                    if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                        errorForm(idElemento, 'Por favor ingresa una clave valida');
                        return false;
                    } else {
                        validadoForm(idElemento);
                    }
                } else {
                    errorForm(idElemento, 'La clave ingresada ya existe');
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa  fa-graduation-cap" aria-hidden="true"></i>
            &nbsp;Configuración de Titulación por Nivel de Estudios</h2>
        <div class="clearfix"></div>
    </div>

    <hr />
    <div class="container-fluid">
         <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgr1" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-5">
                Opción Titulación
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_opcTit" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_opcTit_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqOpcTit" runat="server" ErrorMessage="Seleccionar opción de titulación" ControlToValidate="ddl_opcTit" CssClass="text-danger" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Nivel
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_nivel" CssClass="form-control" runat="server"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Promedio Requerido
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_promedio" runat="server" CssClass="form-control"></asp:TextBox>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="form-row">
            <div class="col-sm-6">
                Código Detalle
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_codigo" CssClass="form-control" runat="server"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2 text-center">
                ¿Créditos requeridos?
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="custom-control custom-switch">
                            <input type="checkbox" class="custom-control-input" id="customSwitches" name="customSwitches">
                            <label class="custom-control-label" for="customSwitches"></label>
                            <asp:HiddenField ID="checked_input" runat="server" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2 text-center">
                ¿Servicio social requerido?
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="custom-control custom-switch">
                            <input type="checkbox" class="custom-control-input" id="customSwitchesSS" name="customSwitchesSS">
                            <label class="custom-control-label" for="customSwitchesSS"></label>
                            <asp:HiddenField ID="checked_input_ss" runat="server" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>            
        </div>
    <hr />
    <asp:UpdatePanel ID="updPnlBotones" runat="server">
        <ContentTemplate>
            <div class="row" id="btn_ttini" runat="server">
                <div class="col text-center">
                    <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" ValidationGroup="guardar" />
                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" ValidationGroup="guardar" OnClick="btn_update_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="Gridtinni" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="Gridtinni_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                            <asp:BoundField DataField="ttini_creditos" HeaderText="Creditos" />
                            <asp:BoundField DataField="descripcion" HeaderText="Opción Titulación" />
                            <asp:BoundField DataField="ttini_promedio" HeaderText="Promedio" />
                            <asp:BoundField DataField="tipo" HeaderText="Código Detalle" />
                            <asp:BoundField DataField="ttini_date" HeaderText="Fecha Registro" />
                            <asp:BoundField DataField="clave" HeaderText="clave">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ttini_tnive_clave" HeaderText="ttini_tnive_clave">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ttini_tcoco_clave" HeaderText="ttini_tcoco_clave">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                              <asp:BoundField DataField="ttini_tseso" HeaderText="ttini_tseso">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
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
            $('#<%= Gridtinni.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtinni.ClientID %>').find("tr:first"))).DataTable({
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
        function activar_check() {
            $("#customSwitches").attr('checked', true);
        }
        function desactivar_check() {
            $("#customSwitches").attr('checked', false);
        }

        function activar_check_ss() {
            $("#customSwitchesSS").attr('checked', true);
        }
        function desactivar_check_ss() {
            $("#customSwitchesSS").attr('checked', false);
        }

    </script>
</asp:Content>
