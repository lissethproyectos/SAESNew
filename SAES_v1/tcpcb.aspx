<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcpcb.aspx.cs" Inherits="SAES_v1.tcpcb" %>

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

        function aplicar() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se transladaron las becas correctamente</h2>Favor de validar en el listado.'
            })
        }

        function error_becas_seleccionadas() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Se debe seleccionar al menos un alumno para transferir una beca</h2>Favor de validar en el listado.'
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <div class="x_title">
                    <h2>
                        <i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Consulta de planes cobro y/o becas asignados
                    </h2>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-4 font-weight-bold">
                Periodo
                  <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" ErrorMessage="Favor de seleccionar periodo." ValidationGroup="buscar" ControlToValidate="ddl_periodo" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-4">
                Campus
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Nivel
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_nivel" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged"></asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col-sm-5">
                Programa
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" AutoPostBack="True">
                              <asp:ListItem Value="">-------</asp:ListItem>
                          </asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Tipo Plan
                  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_tipo_plan" runat="server" CssClass="form-control" AutoPostBack="True" Enabled="False">
                              <asp:ListItem Value="">-------</asp:ListItem>
                              <asp:ListItem Value="B" Selected="True">Beca</asp:ListItem>
                              <asp:ListItem Value="C">Cobro</asp:ListItem>
                          </asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Estatus Asignación
                  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control" AutoPostBack="True">
                              <asp:ListItem Value="">-------</asp:ListItem>
                              <asp:ListItem Value="A">Alta</asp:ListItem>
                              <asp:ListItem Value="I">Inactivo</asp:ListItem>
                          </asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
        </div>
        <br />


        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_consulta" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" OnClick="buscar" ValidationGroup="buscar" />
                        <asp:LinkButton ID="linkBttnRolarDescuentos" CssClass="btn btn-round btn-secondary" data-toggle="modal" data-target="#modalPeriodoNuevo" runat="server"><i class="fa fa-repeat" aria-hidden="true"></i>&nbsp;Rolar Descuentos</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridTcpcb" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Rolar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRolDescuento" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="campus" HeaderText="Campus-Nivel-Prog" />
                                <asp:BoundField DataField="nivel" HeaderText="Nivel">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="programa" HeaderText="Programa">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="matricula" HeaderText="Matricula" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="periodo" HeaderText="Periodo">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="clave" HeaderText="Clave">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descrip" HeaderText="Beca" />
                                <asp:BoundField DataField="porc" HeaderText="Porcentaje" />
                                <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:C2}" />
                                <asp:BoundField DataField="estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha Registro" />
                                <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                            </Columns>
                            <RowStyle Font-Size="Small" />
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <!-- Modal -->
    <div class="modal fade" id="modalPeriodoNuevo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Periodo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-12">
                                Seleccionar el periodo para migrar la beca.
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_periodo_new" CssClass="form-control" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqPeriodoNew" runat="server" ErrorMessage="Favor de seleccionar periodo." ValidationGroup="aplicar" ControlToValidate="ddl_periodo_new" CssClass="text-danger"></asp:RequiredFieldValidator>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                     <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <asp:LinkButton ID="linkBttnAplicar" runat="server" class="btn btn-success" OnClick="linkBttnAplicar_Click" ValidationGroup="aplicar">Aplicar</asp:LinkButton>
                 </ContentTemplate>
                    </asp:UpdatePanel>
                            </div>
            </div>
        </div>
    </div>

    <script>
        function load_datatable2() {
            var periodo = document.getElementById('ContentPlaceHolder1_ddl_periodo').value;
            var campus = document.getElementById('ContentPlaceHolder1_ddl_campus').value;
            var nivel = document.getElementById('ContentPlaceHolder1_ddl_nivel').value;


            $('#<%= GridTcpcb.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridTcpcb.ClientID %>').find("tr:first"))).DataTable({
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
                "paging": false,
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
                        action: function (e, dt, button, config) {
                            window.open(
                                'http://localhost:17026/Reports/VisualizadorCrystal.aspx?Tipo=RepProgAlumnos_Excel&Valor1=' + periodo + '&Valor2=' + campus + '&Valor3=' + nivel,
                                '_blank'
                            )
                        }
                    },
                    {
                        title: 'SAES',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        action: function (e, dt, button, config) {
                            window.open(
                                'http://localhost:17026/Reports/VisualizadorCrystal.aspx?Tipo=RepProgAlumnos&Valor1=' + periodo + '&Valor2=' + campus + '&Valor3=' + nivel,
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
