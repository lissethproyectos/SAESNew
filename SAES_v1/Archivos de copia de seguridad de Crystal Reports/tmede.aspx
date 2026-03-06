<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tmede.aspx.cs" Inherits="SAES_v1.tmede" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-row">
            <div class="col-sm-6">
                Menú
                <asp:UpdatePanel ID="updPnlDDMenu" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Clave
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Estatus
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="A">Alta</asp:ListItem>
                            <asp:ListItem Value="B">Baja</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
           
        </div>
        <div class="form-row">
             <div class="col-sm-5">
                Nombre opción
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtOpcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Relación
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtRelacion" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Forma
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtForma" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlBotones" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkBttnCancelar" runat="server" CssClass="btn btn-round btn-secondary" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click">Agregar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnModificar_Click" Visible="False">Actualizar</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBotones" runat="server"
                    AssociatedUpdatePanelID="updPnlBotones">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrDDMenu" runat="server"
                    AssociatedUpdatePanelID="updPnlDDMenu">
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
                <asp:UpdateProgress ID="updPgrMenu" runat="server"
                    AssociatedUpdatePanelID="updPnlMenu">
                    <ProgressTemplate>
                        <asp:Image ID="imgMenu" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlMenu" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdMenu" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnSelectedIndexChanged="grdMenu_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="menu_desc" HeaderText="Menú" />
                                <asp:BoundField DataField="mede_clave" HeaderText="Clave" />
                                <asp:BoundField DataField="mede_desc" HeaderText="Nombre opción" />
                                <asp:BoundField DataField="mede_estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="mede_rel" HeaderText="Relación" />
                                <asp:BoundField DataField="mede_forma" HeaderText="Forma" />
                                <asp:BoundField DataField="mede_date" HeaderText="Fecha Reg" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>

        <script>
            function load_datatableOpcionesMenu() {
                $('#<%= grdMenu.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdMenu.ClientID %>').find("tr:first"))).DataTable({
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
                            text: 'Exportar Pdf',
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

    </div>
</asp:Content>
