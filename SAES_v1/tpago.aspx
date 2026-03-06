<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpago.aspx.cs" Inherits="SAES_v1.tpago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col">
            <div class="x_title">
                <h2>
                    <i class="fa fa-file-text" aria-hidden="true"></i>&nbsp;Resúmen de Notas y Facturas
                </h2>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-3">
            Matrícula
            <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                                 <ContentTemplate>
                                                     <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
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
    <hr />
    <div class="form-row">
        <div class="col">
            <asp:UpdatePanel ID="updPnlGrid" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="Gridtpago" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnRowDataBound="Gridtpago_RowDataBound" OnSelectedIndexChanged="Gridtpago_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="numero" HeaderText="# Factura/Nota" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total" HeaderText="Total" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Factura">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkBttnRecibo" runat="server" CssClass="btn btn-secondary" CommandName="select"><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="recibo">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
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
    <div class="row">
        <div class="col text-center">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="linkRegresar" runat="server" class="btn btn-success" data-dismiss="modal" OnClick="linkRegresar_Click" Visible='<%# Bind("tiene_recibo") %>'><i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;Regresar</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
