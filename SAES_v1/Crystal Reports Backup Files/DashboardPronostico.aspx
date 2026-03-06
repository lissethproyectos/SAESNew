<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardPronostico.aspx.cs" Inherits="SAES_v1.DashboardPronostico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Styles -->
    <style>
        #chartdiv {
            width: 100%;
            height: 400px;
        }

        #chartdiv2 {
            width: 100%;
            height: 400px;
        }

        .classHide {
            display: none
        }
    </style>

    <script src="https://cdn.amcharts.com/lib/4/core.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/charts.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/themes/animated.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-row">
            <div class="col">
                <h6><i class="fa fa-tachometer fa-2x" aria-hidden="true"></i>Tablero de Pronostico de Ingreso</h6>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrTurno" runat="server"
                    AssociatedUpdatePanelID="updPnlTurno">
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
            <div class="col-sm-3">
                Turno
            <asp:UpdatePanel ID="updPnlTurno" runat="server">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddl_turno" CssClass="form-control alert-info" AutoPostBack="true" OnSelectedIndexChanged="ddl_turno_SelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Periodo
            <asp:UpdatePanel ID="upPnlPeriodo" runat="server">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddl_periodo" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Campus
            <asp:UpdatePanel ID="updPnlCampus" runat="server">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddl_campus" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
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
            <div class="col-sm-8">
                <div id="tituloChart" class="text-center" style="font-size: large; font-weight: bold"></div>
                <div id="chartdiv"></div>
            </div>
            <div class="col-sm-4">
                <div id="tituloChart2" class="text-center" style="font-size: large; font-weight: bold"></div>
                <div id="chartdiv2"></div>
            </div>
        </div>

        <div class="form-row">
            <div class="col">
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
                        <asp:GridView ID="grvDatosGrafica" runat="server" CssClass="table table-striped table-bordered" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True" Width="100%" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Campus" HeaderText="Cve">
                                    <ItemStyle Font-Size="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nivel" HeaderText="Descripcion">
                                    <ItemStyle Font-Size="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Pronostico_Total" HeaderText="Pronóstico">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="10px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Registrados" HeaderText="Registros NI">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="10px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Pronostico_Actual" HeaderText="Pronóstico Hoy">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="10px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Clave" HeaderText="Diferencia">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="10px" HorizontalAlign="Center" />
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

    <script src="Script/Graficas/Dashboard_amchart4.js"></script>
    <script src="Script/Graficas/DatosDashboard.js"></script>
</asp:Content>
