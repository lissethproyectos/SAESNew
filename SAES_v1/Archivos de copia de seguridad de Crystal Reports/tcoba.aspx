<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcoba.aspx.cs" Inherits="SAES_v1.tcoba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function error_insertar() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Ya existe un registro de ese banco, configuración y concepto de cobranza.</h2>'
            })
        }
        function error_editar() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">El registro no se puede editar por que el periodo no esta vigente.</h2>'
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
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
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


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-building" aria-hidden="true"></i>
            Configuración de Cobranza en Bancos
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="form-row">
            <div class="col-sm-3">
                Banco
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddl_banco" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_banco_SelectedIndexChanged"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqBanco" runat="server" CssClass="text-danger" ErrorMessage="Selecciona banco" ControlToValidate="ddl_banco" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                     </ContentTemplate>
                 </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Configuración
               <asp:UpdatePanel ID="updPnlConfig" runat="server">
                   <ContentTemplate>
                       <asp:DropDownList ID="ddl_config" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_config_SelectedIndexChanged">
                           <asp:ListItem Value="">------</asp:ListItem>
                           <asp:ListItem Value="T">Archivo de Texto</asp:ListItem>
                           <asp:ListItem Value="C">Por Columna</asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="reqConf" runat="server" CssClass="text-danger" ErrorMessage="Selecciona configuración" ControlToValidate="ddl_config" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   </ContentTemplate>
               </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Concepto Cobranza
                  <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_cobranza" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_cobranza_SelectedIndexChanged"></asp:DropDownList>
                          <asp:RequiredFieldValidator ID="reqCobranza" runat="server" CssClass="text-danger" ErrorMessage="Selecciona concepto cobranza" ControlToValidate="ddl_cobranza" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Estatus
                  <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control">
                              <asp:ListItem Value="">------</asp:ListItem>
                              <asp:ListItem Value="A">Activo</asp:ListItem>
                              <asp:ListItem Value="B">Baja</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="reqEstatus" runat="server" CssClass="text-danger" ErrorMessage="Selecciona estatus" ControlToValidate="ddl_estatus" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrConfig" runat="server"
                    AssociatedUpdatePanelID="updPnlConfig">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <asp:UpdatePanel ID="updPnlDatos" runat="server">
            <ContentTemplate>
                <div id="datos" runat="server" visible="false">
                    <div class="form-row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblTipoCobranza" runat="server" Text="" CssClass="font-weight-bold"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <asp:Label ID="lblConcepto" runat="server" Text="" CssClass="font-weight-bold">CONCEPTO</asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblValorIni" runat="server" Text="" CssClass="font-weight-bold">VALOR INICIO</asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-1">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblValorFin" runat="server" Text="" CssClass="font-weight-bold">VALOR FIN</asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updPnlRef" runat="server">
                        <ContentTemplate>
                            <div class="form-row" id="rowRef" runat="server">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-2">
                                    <br />
                                    Referencia
                                </div>
                                <div class="col-sm-1">
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" class="form-control" ID="txtReferencia"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor inicio" ControlToValidate="txtReferencia" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-row">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-2">
                            <br />
                            Matricula
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtMatriculaIni"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqMatIni" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor inicio" ControlToValidate="txtMatriculaIni" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtMatriculaFin"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqMatFin" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor fin" ControlToValidate="txtMatriculaFin" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblMatricula" runat="server" Text="Posición de la matricula en la referencia" CssClass="text-info font-weight-bold"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-2">
                            <br />
                            No. Transacción
                        </div>

                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtTransaccionIni"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqTIni" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor inicio" ControlToValidate="txtTransaccionIni" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtTransaccionFin"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor fin" ControlToValidate="txtTransaccionFin" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblTransaccion" runat="server" Text="Posición de la transacción en la referencia" CssClass="text-info font-weight-bold"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-2">
                            <br />
                            Fecha
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtFechaIni"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqFIni" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor inicio" ControlToValidate="txtFechaIni" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtFechaFin"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqFFin" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor fin" ControlToValidate="txtFechaFin" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-2">
                            <br />
                            Importe
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtImporteIni"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqImpIni" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor inicio" ControlToValidate="txtImporteIni" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" class="form-control" ID="txtImporteFin"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqImpFin" runat="server" CssClass="text-danger" ErrorMessage="Ingresar valor fin" ControlToValidate="txtImporteFin" InitialValue="" ValidationGroup="guardar" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlGuardar" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkBttnCancelar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnCancelarModificar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelarModificar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click" ValidationGroup="guardar">Agregar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" Visible="false" ValidationGroup="guardar" OnClick="linkBttnModificar_Click">Actualizar</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGrid" runat="server"
                    AssociatedUpdatePanelID="updPnlGrid">
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
                <asp:UpdatePanel ID="updPnlGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridTcoba" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Width="100%" OnSelectedIndexChanged="GridTcoba_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tbanc_desc" HeaderText="Banco" />
                                <asp:BoundField DataField="tcoba_ind_desc" HeaderText="Configuración">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_estatus_desc" HeaderText="Estatus" />
                                <asp:BoundField DataField="tcoba_date" HeaderText="Fecha" />
                                <asp:BoundField DataField="tcoba_tbanc_clave" HeaderText="tcoba_tbanc_clave">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_ind" HeaderText="tcoba_ind">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_estatus" HeaderText="tcoba_estatus">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_tcoco_clave" HeaderText="tcoba_tcoco_clave">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_tpers_inicio" HeaderText="tcoba_tpers_inicio">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_tpers_fin" HeaderText="tcoba_tpers_fin">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_tran_inicio" HeaderText="tcoba_tran_inicio">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_tran_fin" HeaderText="tcoba_tran_fin">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_fecha_ini" HeaderText="tcoba_fecha_ini">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_fecha_fin" HeaderText="tcoba_fecha_fin">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_imp_inicio" HeaderText="tcoba_imp_inicio">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_imp_fin" HeaderText="tcoba_imp_fin">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tcoba_referencia" HeaderText="tcoba_referencia">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
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
    <script>
        function load_datatable() {
            $('#<%= GridTcoba.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridTcoba.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'Configuracion carga layout',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6]
                        }
                    }
                ],
                "stateSave": true
            });
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>
