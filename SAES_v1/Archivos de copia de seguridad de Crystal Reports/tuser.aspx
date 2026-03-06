<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tuser.aspx.cs" Inherits="SAES_v1.tuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrRol" runat="server"
                    AssociatedUpdatePanelID="updPnlRol">
                    <ProgressTemplate>
                        <asp:Image ID="imgRolr" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                Rol
                <asp:UpdatePanel ID="updPnlRol" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <div class="col-sm-4">
                Cve
                <asp:UpdatePanel ID="updPnlUsuario" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* El usuario es requerido" ValidationGroup="valDatos" ControlToValidate="txtUsuario" Text="* Requerido" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-6">
                Usuario
                <asp:UpdatePanel ID="updPnlNombre" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" PlaceHolder="Nombre Paterno Materno"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* El nombre es requerido" Text="* Requerido" ValidationGroup="valDatos" ControlToValidate="txtNombre" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Estatus
                <asp:UpdatePanel ID="updPnlStatus" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DDLStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="A">Activo</asp:ListItem>
                            <asp:ListItem Value="B">Baja</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlGuardar" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkBttnCancelar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click" ValidationGroup="valDatos">Agregar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnModificar_Click" Visible="false" ValidationGroup="valDatos">Actualizar</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGuardar" runat="server"
                    AssociatedUpdatePanelID="updPnlGuardar">
                    <ProgressTemplate>
                        <asp:Image ID="imgGuardar" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>

        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrUsuarios" runat="server"
                    AssociatedUpdatePanelID="updPnlUsuarios">
                    <ProgressTemplate>
                        <asp:Image ID="imgUsuarios" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlUsuarios" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvUsuarios" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="grvUsuarios_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="15px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="tuser_clave" HeaderText="Cve" />
                                <asp:BoundField DataField="tuser_desc" HeaderText="Usuario" />
                                <asp:BoundField DataField="tuser_estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="tuser_date" HeaderText="Fecha Registro" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Button trigger modal -->


    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Tipo de Usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-center">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function load_datatableUsuarios() {
            $('#<%= grvUsuarios.ClientID %>').prepend($("<thead></thead>").append($('#<%= grvUsuarios.ClientID %>').find("tr:first"))).DataTable({
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
                        action: function (e, dt, button, config) {
                            window.open(
                                'http://localhost:17026/Reports/VisualizadorCrystal.aspx?Tipo=RepCatUsuariosExc',
                                '_blank'
                            )
                        }
                    },
                    {
                        className: 'btn-dark',
                        text: 'Ver Pdf',
                        action: function (e, dt, button, config) {
                            window.open(
                                'http://localhost:17026/Reports/VisualizadorCrystal.aspx?Tipo=RepCatUsuarios',
                                '_blank'
                            )
                        }
                    }
                ],
                "stateSave": true
            });
        }
    </script>
</asp:Content>
