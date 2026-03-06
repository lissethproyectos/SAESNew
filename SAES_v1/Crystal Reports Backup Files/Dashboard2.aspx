<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard2.aspx.cs" Inherits="SAES_v1.Dashboard2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Styles -->
    <style>
        #chartdiv {
            width: 100%;
            height: 400px;
        }

        .classHide {
            display: none
        }
    </style>

    <!-- Resources -->
    <script src="https://cdn.amcharts.com/lib/5/index.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/xy.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/themes/Animated.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-row">
            <div class="col">
                <h6><i class="fa fa-tachometer fa-2x" aria-hidden="true"></i> Tablero de Control Porcentaje de Cartera Vencida</h6>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col-sm-4">
                Periodo
            <asp:UpdatePanel ID="upPnlPeriodo" runat="server">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddl_periodo" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Campus
            <asp:UpdatePanel ID="updPnlCampus" runat="server">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddl_campus" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Nivel
             <asp:UpdatePanel ID="updPnlNivel" runat="server">
                 <ContentTemplate>
                     <asp:DropDownList runat="server" ID="ddl_nivel" CssClass="form-control" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                 </ContentTemplate>
             </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="upPgrPeriodo" runat="server"
                    AssociatedUpdatePanelID="upPnlPeriodo">
                    <ProgressTemplate>
                        <asp:Image ID="img9" runat="server"
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
                        <asp:Image ID="img10" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPnlPgrCampus" runat="server"
                    AssociatedUpdatePanelID="updPnlCampus">
                    <ProgressTemplate>
                        <asp:Image ID="img11" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col-sm-8 text-center">
                <div id="precarga">
                    <input type="image" class="center" src="../Images/Sitemaster/loader.gif" style="width: 50px; height: 50px" />
                </div>
                <div id="tituloChart" class="text-center" style="font-size: large; font-weight: bold"></div>
                <div id="chartdiv"></div>
            </div>
            <div class="col-sm-4 text-center">
                <asp:UpdateProgress ID="updPgrDatosGrafica" runat="server"
                    AssociatedUpdatePanelID="updPnlDatosGrafica">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="updPnlDatosGrafica" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvDatosGrafica" runat="server" CssClass="table table-striped table-bordered" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="grvDatosGrafica_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                    <ItemStyle Font-Size="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Por_Cobrar" HeaderText="Por Cobrar" DataFormatString="{0:C}">
                                    <ItemStyle Font-Size="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Vencido" HeaderText="Vencido">
                                    <ItemStyle Font-Size="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Porcentaje" HeaderText="%">
                                    <ItemStyle Font-Size="10px" />
                                </asp:BoundField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" CssClass="btn btn-dark"><i class="fa fa-print"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Clave">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
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
            $('#<%= grvDatosGrafica.ClientID %>').prepend($("<thead></thead>").append($('#<%= grvDatosGrafica.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'SAES',
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
                "stateSave": true
            });
        }
    </script>

    <script src="Script/Graficas/Dashboard.js"></script>
    <script src="Script/Graficas/DatosDashboard.js"></script>
</asp:Content>
