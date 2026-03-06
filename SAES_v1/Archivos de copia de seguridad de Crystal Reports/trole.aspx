<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="trole.aspx.cs" Inherits="SAES_v1.trole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-row">
                    <div class="col-sm-2">
                        Rol
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* La clave es requerida" Text="* Requerido" ValidationGroup="valDatos" ControlToValidate="txtClave" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-sm-6">
                        Descripción
                        <asp:TextBox ID="txtRole" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* El rol es requerido" Text="* Requerido" ValidationGroup="valDatos" ControlToValidate="txtRole" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-sm-4">
                        Estatus
                        <asp:DropDownList ID="DDLEstatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="A">Alta</asp:ListItem>
                            <asp:ListItem Value="B">Baja</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col text-center">
                        <asp:UpdatePanel ID="updPnlGuardar" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="linkBttnCancelar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                                <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click" ValidationGroup="valDatos">Agregar</asp:LinkButton>
                                <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" Visible="false" OnClick="linkBttnModificar_Click" ValidationGroup="valDatos">Actualizar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGuardar" runat="server"
                    AssociatedUpdatePanelID="updPnlGuardar">
                    <ProgressTemplate>
                        <asp:Image ID="img1" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGrid" runat="server"
                    AssociatedUpdatePanelID="updPnlGrid">
                    <ProgressTemplate>
                        <asp:Image ID="img2" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvCatRoles" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="grvCatRoles_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Rol" DataField="trole_clave" />
                                <asp:BoundField HeaderText="Descripción" DataField="trole_desc" />
                                <asp:BoundField HeaderText="Estatus" DataField="trole_estatus" />
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
        function load_datatableRoles() {
            $('#<%= grvCatRoles.ClientID %>').prepend($("<thead></thead>").append($('#<%= grvCatRoles.ClientID %>').find("tr:first"))).DataTable({
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
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                "dom": '<"top"if>rt<"bottom"lBp><"clear">',
                "buttons": [
                    {
                        className: 'btn-dark',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1]
                        }
                    },
                    {
                        className: 'btn-dark',
                        text: 'Exportar Pdf',
                        action: function (e, dt, button, config) {
                            window.location = 'https://datatables.net/forums/discussion/52180/redirect-to-url-when-push-edit-button';
                        }
                    }
                ],
                "stateSave": true
            });
        }
    </script>
</asp:Content>
