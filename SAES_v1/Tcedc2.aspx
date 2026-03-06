<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tcedc2.aspx.cs" Inherits="SAES_v1.Tcedc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .classHide {
            display: none
        }

        .details-control {
            background: url(https://datatables.net/examples/resources/details_open.png) no-repeat center center;
            cursor: pointer;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%
        }

        .Grid th {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%
        }

        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%
        }

        .ChildGrid th {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%
        }

        .bg-info-saldos {
            background-color: #8a2661 !important;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            //alert("pasa 1");
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "Images/Generales/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            //alert("pasa 2");

            $(this).attr("src", "Images/Generales/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="form-row">
            <div class="col-sm-2">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        Matricula
                <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Nombre(s)   
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_alumno" MaxLength="10" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Programa
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control"></asp:DropDownList>

                        <%--                        <asp:TextBox ID="txt_programa" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2">
                Periodo
                             <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                 <ContentTemplate>
                                     <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar periodo" ControlToValidate="ddl_periodo" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <div class="card text-white bg-info-saldos">
            <div class="row  text-center">
                <div class="col-sm-3">
                    <i class="fa fa-chevron-circle-up" aria-hidden="true"></i>&nbsp;Total de cargos
                </div>
                <div class="col-sm-2">
                    <i class="fa fa-chevron-circle-down"></i>&nbsp;Total de abonos
                </div>
                <div class="col-sm-2">
                    <i class="fa fa-chevron-circle-down"></i>&nbsp;Total de beca/descuentos
                </div>
                <div class="col-sm-2">
                    <i class="fa fa-chevron-circle-down"></i>&nbsp;Total de cancelaciones
                </div>
                <div class="col-sm-3">
                    <i class="fa fa-check fa-2x"></i>&nbsp;Saldo final
                </div>
            </div>
            <div class="row text-center">
                <div class="col-sm-3">
                    <h2>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblCargo" runat="server" Text="0"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h2>
                </div>
                <div class="col-sm-2">
                    <h2>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblAbono" runat="server" Text="0"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h2>
                </div>
                <div class="col-sm-2">
                    <h2>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblBeca" runat="server" Text="0"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h2>

                </div>
                <div class="col-sm-2">
                    <h2>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblCancelacion" runat="server" Text="0"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h2>

                </div>
                <div class="col-sm-3">
                    <h2>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblSaldo" runat="server" Text="0"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h2>
                </div>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-4">
                <div class="card text-white bg-info mb-3" style="max-width: 18rem;">
                    <div class="card-header">Resumen Estado de Cuenta</div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-8">Total de cargos</div>
                            <div class="col-md-4">$200</div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">Total de abonos</div>
                            <div class="col-md-4">$550</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
            </div>
        </div>--%>

        <%--<div class="row stat-cards">
          <div class="col-md-6 col-xl-3">
            <article class="stat-cards-item">
              <div class="stat-cards-icon primary">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-bar-chart-2" aria-hidden="true"><line x1="18" y1="20" x2="18" y2="10"></line><line x1="12" y1="20" x2="12" y2="4"></line><line x1="6" y1="20" x2="6" y2="14"></line></svg>
              </div>
              <div class="stat-cards-info">
                <p class="stat-cards-info__num">1478 286</p>
                <p class="stat-cards-info__title">Total visits</p>
                <p class="stat-cards-info__progress">
                  <span class="stat-cards-info__profit success">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trending-up" aria-hidden="true"><polyline points="23 6 13.5 15.5 8.5 10.5 1 18"></polyline><polyline points="17 6 23 6 23 12"></polyline></svg>4.07%
                  </span>
                  Last month
                </p>
              </div>
            </article>
          </div>
        </div>--%>
        <hr />
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridTcedc" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Width="100%" OnRowDataBound="GridTcedc_RowDataBound" DataKeyNames="tedcu_consec">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" src="Images/Generales/plus.png" />
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:GridView ID="GridTcedc_Det" runat="server" AutoGenerateColumns="False" CssClass="ChildGrid" ShowHeaderWhenEmpty="True" Width="100%" EmptyDataText="No se encontraron pagos." ShowFooter="True" OnSelectedIndexChanged="GridTcedc_Det_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Concepto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("desc_concepto") %>' Font-Bold="False"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Suma Pagos
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pago">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPago" runat="server" Text='<%# Bind("pago") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotNumPago" runat="server" Text="Label"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("pago") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="fecha_pago" HeaderText="Fecha Pago" />
                                                    <asp:TemplateField HeaderText="# Factura">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPago" runat="server" Text='<%# Bind("tpago_factura_cons") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="tpago_factura_cons" HeaderText="# Factura" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkBttnRecibo" OnClientClick='<%# "VerRecibo(&#39;"+  Eval("tpago_factura_cons")  + "&#39;); return false;"%>' runat="server" CssClass="btn btn-secondary" Visible='<%# Bind("tiene_recibo") %>'><i class="fa fa-file-text" aria-hidden="true"></i> Recibo</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="tedcu_tpees_clave" HeaderText="Periodo" />
                                <asp:BoundField DataField="tcoco_desc" HeaderText="Concepto">
                                    <ItemStyle Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tedcu_importe" HeaderText="Costo" />
                                <asp:BoundField DataField="vencimiento" HeaderText="Fecha Vencimiento" />
                                <asp:BoundField DataField="saldo_pendiente" HeaderText="Saldo" />
                                <asp:BoundField DataField="tedcu_consec">
                                    <ControlStyle CssClass="classHide" />
                                    <FooterStyle CssClass="classHide" />
                                    <HeaderStyle CssClass="classHide" />
                                    <ItemStyle CssClass="classHide" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkRegresar" runat="server" class="btn btn-round btn-dark" OnClick="linkRegresar_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;Regresar</asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="linkEdoCta" runat="server" class="btn btn-round btn-success" data-dismiss="modal" OnClick="linkEdoCta_Click"><i class="fa fa-print" aria-hidden="true"></i>&nbsp;Estado de Cuenta</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script>
        function VerRecibo(consecutivo) {
            //alert(consecutivo);
            var matricula = $("#ContentPlaceHolder1_txt_matricula").val();
            var ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepRecibo&Valor1=LV&Valor2=" + matricula + "&Valor3=" + consecutivo;
            window.open(ruta, '_blank');
        }

    </script>
</asp:Content>
