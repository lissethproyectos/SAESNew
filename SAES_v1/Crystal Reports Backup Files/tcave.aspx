<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcave.aspx.cs" Inherits="SAES_v1.tcave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
            function Nodatos() {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'success',
                    html: '<h2 class="swal2-title" id="swal2-title">No existen datos para mostrar</h2>'
                })
            }
    </script>
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
            <img src="Images/Dashboard/dashboard.png" style="width: 30px;" /><small>Reporte Porcentaje de Cartera Vencida</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <div class="col-md-8 col-sm-6">
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
                </ContentTemplate>
               </asp:UpdatePanel>

        </div>
        <div class="row justify-content-center" style="text-align: center; margin: auto; margin-top: 15px;" id="btn_programa" runat="server">
                    <div class="col-md-3" style="text-align: center; margin-top: 15px;">
                        
                        <asp:Button ID="guardar_prog" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" OnClick="CV_Click"/>
                        
                    </div>
        </div>

        <div id="form_tcave" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="table_tcv">
                    <asp:GridView ID="Gridtcv" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false">
                        <Columns>
                            <asp:BoundField DataField="Campus" HeaderText="Campus" />
                            <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                            <asp:BoundField DataField="Xcobrar_Par" HeaderText="CxC Parcialidad" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="FV1" HeaderText="Fecha Vencimiento(1)" />
                            <asp:BoundField DataField="Vencido1" HeaderText="Vencido(1)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Porc1" HeaderText="%Cartera Vencida(1)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="FV2" HeaderText="Fecha Vencimiento(2)" />
                            <asp:BoundField DataField="Vencido2" HeaderText="Vencido(2)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Porc2" HeaderText="%Cartera Vencida(2)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="FV3" HeaderText="Fecha Vencimiento(3)" />
                            <asp:BoundField DataField="Vencido3" HeaderText="Vencido(3)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Porc3" HeaderText="%Cartera Vencida(3)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="FV4" HeaderText="Fecha Vencimiento(1)" />
                            <asp:BoundField DataField="Vencido4" HeaderText="Vencido(1)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Porc4" HeaderText="%Cartera Vencida(1)" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Xcobrar_Total" HeaderText="CxC Total" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Total_Vencido" HeaderText="Vencido Total" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="Porc_Vencido_Total" HeaderText="%Cartera Vencida total" ITEMSTYLE-HORIZONTALALIGN="Right"/>
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
                         columns: [0, 1, 2, 3,4,5,6,7,8,9,10,11,12,13,14,15,16,17]
                     }
                 }
             ],
             stateSave: true
         });
     }

 </script>
</asp:Content>

