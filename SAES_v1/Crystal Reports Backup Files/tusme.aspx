<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tusme.aspx.cs" Inherits="SAES_v1.tusme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-row">
        <div class="col-sm-4">
            Rol
            <asp:UpdatePanel ID="updPnlRol" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-sm-8">
            Menu
              <asp:UpdatePanel ID="updPnlMenu" runat="server">
                  <ContentTemplate>
                      <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"></asp:DropDownList>
                  </ContentTemplate>
              </asp:UpdatePanel>
        </div>
    </div>
    <div class="form-row">
        <div class="col text-center">
            <asp:UpdateProgress ID="updPgrMenu" runat="server"
                AssociatedUpdatePanelID="updPnlMenu">
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
            <asp:UpdateProgress ID="updPgrRol" runat="server"
                AssociatedUpdatePanelID="updPnlRol">
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
                    <asp:Image ID="imgMenu" runat="server"
                        AlternateText="Espere un momento, por favor.." Height="50px"
                        ImageUrl="~/Images/Sitemaster/loader.gif"
                        ToolTip="Espere un momento, por favor.." Width="50px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <div class="form-row">
        <div class="col text-center">
            <asp:UpdateProgress ID="updPgrBotones" runat="server"
                AssociatedUpdatePanelID="updPnlBotones">
                <ProgressTemplate>
                    <asp:Image ID="img5" runat="server"
                        AlternateText="Espere un momento, por favor.." Height="50px"
                        ImageUrl="~/Images/Sitemaster/loader.gif"
                        ToolTip="Espere un momento, por favor.." Width="50px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <div class="form-row">
        <div class="col text-center">
            <asp:UpdatePanel ID="updPnlBotones" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click">Aplicar Cambios</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="form-row">
        <div class="col">
            <div id="" style="overflow:scroll; height:400px;">
            <asp:UpdatePanel ID="updPnlGrid" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdUsuMenu" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="grdUsuMenu_SelectedIndexChanged" OnPageIndexChanging="grdUsuMenu_PageIndexChanging" EmptyDataText="No existen registros." ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:BoundField HeaderText="Clave" DataField="mede_clave" />
                            <asp:BoundField HeaderText="Opción Menú" DataField="mede_desc" />
                            <asp:TemplateField HeaderText="Consulta">
                                <ItemTemplate>
                                    <%--<asp:CheckBox ID="chkCons" runat="server" Checked='<%#Convert.ToBoolean(Eval("usme_select")) %>' OnCheckedChanged="chkCons_CheckedChanged" AutoPostBack="true" onClick="limpiar_datableForms();" />--%>
                                    <asp:CheckBox ID="chkCons" runat="server" Checked='<%#Convert.ToBoolean(Eval("usme_select")) %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actualización">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAct" runat="server" Checked='<%#Convert.ToBoolean(Eval("usme_update")) %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
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
        function load_datatableForms() {
            $('#<%= grdUsuMenu.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdUsuMenu.ClientID %>').find("tr:first"))).DataTable({
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
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    },
                    {
                        className: 'btn-dark',
                        text: 'Exportar Pdf',
                        exportOptions: {
                            columns: [1, 2]
                        }
                    }
                ],
                "stateSave": true
            });
        };
        function limpiar_datableForms() {
            var table = $('#<%= grdUsuMenu.ClientID %>').DataTable();
            table.destroy();
        };
        function ValorCh(valor) {
            alert(valor);
        }
    </script>
</asp:Content>
