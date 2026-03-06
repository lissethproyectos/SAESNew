<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tepcb.aspx.cs" Inherits="SAES_v1.tepcb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script>
        function save() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
        }

        function update() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se actualizaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
        }

        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
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
                        <i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Asignación plan de cobro/beca
                    </h2>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBusca" runat="server"
                    AssociatedUpdatePanelID="updPnlBusca">
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
                Matrícula
                                             <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                                 <ContentTemplate>
                                                     <div class="input-group">
                                                         <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                                                         <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                                     </div>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
            </div>
            <div class="col-sm-9">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        Nombre(s)                           
                                                    <asp:TextBox ID="txt_alumno" MaxLength="10" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col">
                Programa
                                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                              <ContentTemplate>
                                                  <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
            </div>

        </div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div class="row" id="rowAlumnos" runat="server">
                    <div class="col">
                        <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tpers_clave" HeaderText="Matrícula" />
                                <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tpers_paterno" HeaderText="Apellido Paterno" />
                                <asp:BoundField DataField="tpers_materno" HeaderText="Apellido Materno" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <hr />
        <asp:UpdatePanel ID="updPnlGridTepcb" runat="server">
            <ContentTemplate>
                <div id="rowGridTepcb" runat="server" visible="false">
                    <div class="form-row">
                        <div class="col-sm-6">
                            Periodo Escolar
                    <asp:DropDownList ID="ddl_periodo" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            Clave Beca
                            <asp:DropDownList ID="ddl_becas" runat="server" CssClass="form-control">
                                <asp:ListItem>-------</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            Estatus
                     <asp:DropDownList ID="ddl_estatus" CssClass="form-control" runat="server">
                         <asp:ListItem>-------</asp:ListItem>
                         <asp:ListItem Value="A">Activo</asp:ListItem>
                         <asp:ListItem Value="I">Ináctivo</asp:ListItem>
                     </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col text-center">
                            <asp:UpdatePanel ID="updPnlGuardar" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="linkBttnCancelar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnCancelarModificar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelarModificar_Click">Cancelar</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click" ValidationGroup="guardar">Agregar</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" Visible="false" OnClick="linkBttnModificar_Click" ValidationGroup="valDatos">Actualizar</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnPagosAplicados" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnPagosAplicados_Click"><i class="fa fa-ticket" aria-hidden="true" OnClick="PDF_Click"></i>&nbsp;Ver Estado de Cuenta</asp:LinkButton>

                                    <asp:LinkButton ID="linkBttnPlanesCobro" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnPlanesCobro_Click"><i class="fa fa-usd" aria-hidden="true" OnClick="PDF_Click"></i>&nbsp;Ver Planes de Cobro</asp:LinkButton>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                               <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                            <asp:GridView ID="GridTepcb" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridTepcb_SelectedIndexChanged" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="tepcb_tpees_clave" HeaderText="Periodo" />
                                    <asp:BoundField DataField="tepcb_tpcbe_clave" HeaderText="Clave" />
                                    <asp:BoundField DataField="tpcbe_desc" HeaderText="Descripción" />
                                    <asp:BoundField DataField="tpcbe_porcentaje" HeaderText="Porcentaje" />
                                    <asp:BoundField DataField="monto_beca" HeaderText="Monto" />
                                    <asp:BoundField DataField="tepcb_estatus" HeaderText="Estatus" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="modal fade" id="modalActualizaEstatus" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Estatus Beca</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
            <div class="row">
            <div class="col text-center">
          <i class="fa fa-question-circle fa-4x text-info" aria-hidden="true"></i>
                </div>
                </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <h5><asp:Label ID="lblMsjEstatus" runat="server" Text=""></asp:Label></h5>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
      </div>
      <div class="modal-footer">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                          <button type="button" class="btn btn-round btn-secondary" data-dismiss="modal">Cerrar</button>
                  <asp:LinkButton ID="linkBttnAplicar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnAplicar_Click">Aplicar</asp:LinkButton>
              </ContentTemplate>
          </asp:UpdatePanel>
      </div>
    </div>
  </div>
</div>
    </div>

    


    <script>
        function load_datatable_alumnos() {
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
</asp:Content>
