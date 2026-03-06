<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcrss.aspx.cs" Inherits="SAES_v1.tcrss" %>

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
            <i class="fa fa-file-text" aria-hidden="true"></i>
            &nbsp;Consulta/Reporte Registro de Titulación
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
            <div class="col-sm-6">
                Programa
                <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col-sm-6">
                Programa SS<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa_ss" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Modalidad SS<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_modalidad" CssClass="form-control">
                            <asp:ListItem Value="">-------</asp:ListItem>
                            <asp:ListItem Value="I">SS Interno</asp:ListItem>
                            <asp:ListItem Value="E">SS Empresa Externa</asp:ListItem>
                            <asp:ListItem Value="G">Empleado Gobierno</asp:ListItem>
                            <asp:ListItem Value="D">SS por Edad/Enfermedad</asp:ListItem>
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
                            <asp:ListItem Value="I">Iniciado</asp:ListItem>
                            <asp:ListItem Value="R">Trámite</asp:ListItem>
                            <asp:ListItem Value="T">Terminado</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel ID="updPnlBotones" runat="server">
            <ContentTemplate>
                <div class="form-row" id="btn_tcrss" runat="server">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btn_consultar" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" OnClick="btn_consultar_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtcrss" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText=" No se encontraron registros de titulacion.">
                            <Columns>
                                <asp:BoundField DataField="tpers_id" HeaderText="Matrícula" />
                                <asp:BoundField DataField="nombre" HeaderText="Alumno" />
                                <asp:BoundField DataField="tress_tpees_clave" HeaderText="Periodo" HtmlEncode="false" />
                                <asp:BoundField DataField="tcamp_desc" HeaderText="Campus" />
                                <asp:BoundField DataField="tnive_desc" HeaderText="Nivel" />
                                <asp:BoundField DataField="tprog_clave" HeaderText="Programa" HtmlEncode="false" />
                                <asp:BoundField DataField="tprss_desc" HeaderText="Programa SS" HtmlEncode="false" />
                                <asp:BoundField DataField="Modalidad" HeaderText="Modalidad SS" />
                                <asp:BoundField DataField="trees_horas" HeaderText="Horas Cumplidas" />
                                <asp:BoundField DataField="trees_estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="trees_date" HeaderText="Fecha Estatus" />
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
            var campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
            var nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
            var programa = document.getElementById("ContentPlaceHolder1_ddl_programa").value;
            var estatus = document.getElementById("ContentPlaceHolder1_ddl_estatus").value;
            var moda = document.getElementById("ContentPlaceHolder1_ddl_modalidad").value;
            var programa_ss = document.getElementById("ContentPlaceHolder1_ddl_programa_ss").value;
            var periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;

            

            $('#<%= Gridtcrss.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtcrss.ClientID %>').find("tr:first"))).DataTable({
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
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,  10]
                        }
                    },
                    {
                        className: 'btn-dark',
                        text: 'Exportar Pdf',
                        action: function (e, dt, button, config) {
                            window.open(
                                'http://localhost:17026/Reports/VisualizadorCrystal.aspx?Tipo=RepRegistroSS&Valor1=' + campus + '&Valor2=' + programa + '&Valor3=' + nivel + '&Valor4=' + programa_ss + '&Valor5=' + moda + '&Valor6=' + periodo,
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
