<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tbole.aspx.cs" Inherits="SAES_v1.tbole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrPeriodo" runat="server"
                    AssociatedUpdatePanelID="updPnlPeriodo">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrCampus" runat="server"
                    AssociatedUpdatePanelID="updPnlCampus">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrNivel" runat="server"
                    AssociatedUpdatePanelID="updPnlNivel">
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
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrPrograma" runat="server"
                    AssociatedUpdatePanelID="updPnlPrograma">
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
            <div class="col-sm-6">
                Periodo
                 <asp:UpdatePanel ID="updPnlPeriodo" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged"></asp:DropDownList>
                     </ContentTemplate>
                 </asp:UpdatePanel>
            </div>
            <div class="col-sm-6">
                Campus
                <asp:UpdatePanel ID="updPnlCampus" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_campus" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-3">
                Nivel
             <asp:UpdatePanel ID="updPnlNivel" runat="server">
                 <ContentTemplate>
                     <asp:DropDownList runat="server" ID="ddl_nivel" CssClass="form-control" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                 </ContentTemplate>
             </asp:UpdatePanel>
            </div>
            <div class="col-sm-7">
                Programa
             <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                 <ContentTemplate>
                     <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                 </ContentTemplate>
             </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlGenBol" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="linkBttnGenBol" runat="server" CssClass="btn btn-round btn-success" Text="Generar Boletas" OnClick="linkBttnGenBol_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" EmptyDataText="No se encontraron datos." OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Campus" HeaderText="Campus" />
                                <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                                <asp:BoundField DataField="Prog_Clave" HeaderText="Cve Carrera" />
                                <asp:BoundField DataField="matricula" HeaderText="Matrícula" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Paterno" HeaderText="Apellido Paterno" />
                                <asp:BoundField DataField="Materno" HeaderText="Apellido Materno" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-secondary" CommandName="Select" Text="Boleta"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle Font-Size="Small" />
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrTurno" runat="server"
                    AssociatedUpdatePanelID="updPnlGenBol">
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
            <div class="col">

                <iframe id="miniContenedor" frameborder="0" marginheight="0" marginwidth="0" name="miniContenedor"
                    style="height: 500px" width="100%"></iframe>
            </div>
        </div>
    </div>
    <script>
        function load_datatable() {
            $('#<%= GridAlumnos.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridAlumnos.ClientID %>').find("tr:first"))).DataTable({
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
</asp:Content>
