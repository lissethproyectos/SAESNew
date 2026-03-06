<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpreg.aspx.cs" Inherits="SAES_v1.tpreg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //---- Nombre Campus ----//
        function validarNombreCampus(idEl) {
            const idElemento = idEl;
            let tamin = document.getElementById(idElemento).value;
            if (tamin == null || tamin.length == 0 || /^\s+$/.test(tamin)) {
                errorForm(idElemento, 'Por favor ingresa el nombre del campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-row">
            <div class="col-sm-2">Clave
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqClave" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="guardar" ControlToValidate="txtClave" Text="Ingresar clave" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="expregCve" runat="server" ErrorMessage="La cve debe ser numérico" ValidationExpression="^[1-9]+\d*$" ValidationGroup="guardar" ForeColor="Red" ControlToValidate="txtClave" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-7">Descripción
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqDesc" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="guardar" ControlToValidate="txtDescripcion" Text="Ingresar descripción" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>       
            <div class="col-smd-3">Estatus
                <asp:UpdatePanel ID="updPnlEstatus" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="A">Alta</asp:ListItem>
                            <asp:ListItem Value="B">Baja</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="guardar" ControlToValidate="ddlEstatus" Text="Seleccionar estatus" ForeColor="Red"></asp:RequiredFieldValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlBotones" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkBttnCancelar" runat="server" CssClass="btn btn-round btn-secondary" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click" ValidationGroup="guardar">Agregar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnModificar_Click" Visible="False" ValidationGroup="guardar">Actualizar</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrPreg" runat="server"
                    AssociatedUpdatePanelID="updPnlPreg">
                    <ProgressTemplate>
                        <asp:Image ID="img1" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
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
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlPreg" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdPreguntas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="grdMenu_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="preg_clave" HeaderText="Clave" />
                                <asp:BoundField DataField="preg_desc" HeaderText="Descripción" />
                                <asp:BoundField DataField="preg_estatus" HeaderText="Estatus" />
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
        function load_datatablePreguntas() {
            $('#<%= grdPreguntas.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdPreguntas.ClientID %>').find("tr:first"))).DataTable({
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
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    }
                ],
                "stateSave": true
            });
        }
      </script>
</asp:Content>
