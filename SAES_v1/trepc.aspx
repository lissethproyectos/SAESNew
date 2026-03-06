<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="trepc.aspx.cs" Inherits="SAES_v1.trepc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error_transaccion() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Transacción de Base de Datos</h2>'
            })
        }

        function ver_reporte_becas(campus, nivel, programa, concepto, beca, estatus, periodo) {

            $('#precarga1').html('<div class="loading"><img src="Images/Sitemaster/loader.gif" alt="loading"  height="50px" width="50px"/><br/>Un momento, por favor...</div>');


            window.open();
            window.write("../Reports/VisualizadorCrystal.aspx?Tipo=RepBecas_Descuentos&Valor1=" + campus + "&Valor2=" + nivel + "&Valor3=" + programa + "&Valor4=" + concepto + "&Valor5=" + beca + "&Valor6=" + estatus + "&Valor7=" + periodo, "miniContenedor", "toolbar=no", "location=no", "menubar=no")
            window.close()

            //$('#precarga1').html('');

            return false;

        }

        function loader_false() {
            $('#precarga1').html('');
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-external-link" aria-hidden="true"></i>
            &nbsp;Consulta/reporte de becas/descuentos asignados
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="form-row">
            <div class="col-sm-4">
                Periodo
            <asp:UpdatePanel ID="upPnlPeriodo" runat="server">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddl_periodo" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
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
            <div class="col-sm-5">
                Programa
                <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Concepto Cobro
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_concepto" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Beca/Descuento
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_tipo" CssClass="form-control">
                            <asp:ListItem Value="">-------</asp:ListItem>
                            <asp:ListItem>Beca</asp:ListItem>
                            <asp:ListItem>Descuento</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Estatus
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_estatus" CssClass="form-control">
                            <asp:ListItem Value="">-------</asp:ListItem>
                            <asp:ListItem Value="A">Alta</asp:ListItem>
                            <asp:ListItem Value="I">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel ID="updPnlBotones" runat="server">
            <ContentTemplate>
                <div class="form-row" id="btn_trepc" runat="server">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_consultar" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" OnClick="btn_consultar_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridTrepc" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True" EmptyDataText=" No se encontraron becas/descuentos.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" CssClass="ocultar"  />
                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="ocultar"  />
                                </asp:TemplateField>
                                <asp:BoundField DataField="tpers_id" HeaderText="Matrícula" />
                                <asp:BoundField DataField="nombre" HeaderText="Alumno" />
                                <asp:BoundField DataField="tepcb_tpees_clave" HeaderText="Periodo" />
                                <asp:BoundField DataField="tcamp_desc" HeaderText="Campus" />
                                <asp:BoundField DataField="tnive_desc" HeaderText="Nivel" />
                                <asp:BoundField DataField="tprog_clave" HeaderText="Programa" />
                                <asp:BoundField DataField="tcoco_desc" HeaderText="Concepto" />
                                <asp:BoundField DataField="beca_desc" HeaderText="Beca/Descuento" />
                                <asp:BoundField DataField="importe" HeaderText="Importe" />
                                <asp:BoundField DataField="tepcb_estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="tepcb_date" HeaderText="Fecha Asig." />
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
             var campus =  document.getElementById("ContentPlaceHolder1_ddl_campus").value;
             var nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
             var programa = document.getElementById("ContentPlaceHolder1_ddl_programa").value;
             var concepto = document.getElementById("ContentPlaceHolder1_ddl_concepto").value;
             var tipo = document.getElementById("ContentPlaceHolder1_ddl_tipo").value;
             var estatus = document.getElementById("ContentPlaceHolder1_ddl_estatus").value;
             var periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;


             $('#<%= GridTrepc.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridTrepc.ClientID %>').find("tr:first"))).DataTable({
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
                 "buttons": [
                     {
                         title: 'SAES',
                         className: 'btn-dark',
                         extend: 'excel',
                         text: 'Exportar Excel',
                         exportOptions: {
                             columns: [0, 1, 2, 3,  4,  5,6,  7,8,9,10,11]
                         }
                     },
                     {
                         className: 'btn-dark',
                         text: 'Exportar Pdf',
                         action: function (e, dt, button, config) {
                             window.open(
                                 'http://localhost:17026/Reports/VisualizadorCrystal.aspx?Tipo=RepBecas_Descuentos&Valor1=' + campus + '&Valor2=' + nivel + '&Valor3=' + programa + '&Valor4=' + concepto + '&Valor5=' + tipo + '&Valor6=' + estatus + '&Valor7=' + periodo,
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


