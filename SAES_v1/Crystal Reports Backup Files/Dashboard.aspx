<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SAES_v1.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ddl_chart {
            width: 100%;
            font-size: small;
        }
    </style>
    <script src="Script/utils.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Dashboard/dashboard.png" style="width: 30px;" /><small>Tableros de Control Porcentaje de Cartera Vencida</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <div class="col-md-6 col-sm-6">
            <asp:UpdatePanel ID="upd_dashboard" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-sm-6">
                            <asp:DropDownList runat="server" ID="ddl_periodo" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" CssClass="form-control-sm ddl_chart" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddl_campus" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" CssClass="form-control-sm ddl_chart" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddl_nivel" CssClass="form-control-sm ddl_chart" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <canvas id="dashboard_1"></canvas>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddl_periodo" />
                    <asp:PostBackTrigger ControlID="ddl_campus" />
                    <asp:PostBackTrigger ControlID="ddl_nivel" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

        

        <div class="col-md-6 col-sm-6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="table_tcv">
                    <asp:GridView ID="Gridtcv" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Xcobrar" HeaderText="Cuenta X Cobrar" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Vencido" HeaderText="Vencido" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Porcentaje" HeaderText="%Cartera Vencida" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


    </div>
    <!-- Chart.js -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.2.0/chart.js" integrity="sha512-opXrgVcTHsEVdBUZqTPlW9S8+99hNbaHmXtAdXXc61OUU6gOII5ku/PzZFqexHXc3hnK8IrJKHo+T7O4GRIJcw==" crossorigin="anonymous"></script>
<script>
    function load_datatable() {
        let table_periodo = $("#ContentPlaceHolder1_Gridtcv").DataTable({
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
                    title: 'SAES_Cartera Vencida',
                    className: 'btn-dark',
                    extend: 'excel',
                    text: 'Exportar Excel',
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    }
                },
                {
                    title: 'SAES_Cartera Vencida',
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
            stateSave: true
        });
    }
</script>
</asp:Content>