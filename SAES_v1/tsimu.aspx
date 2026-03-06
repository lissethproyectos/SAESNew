<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tsimu.aspx.cs" Inherits="SAES_v1.tsimu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function Exitoso() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Estado de Cuenta</h2>Generado exitósamente.'
            })
        }
        function YaExiste() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Estado de Cuenta</h2>Existente para el periodo'
            })
        }
    </script>

    <style>
        .icon_regresa {
            width: 100%;
            text-align: center;
            border-color: #FFF !important;
        }

            .icon_regresa:hover {
                background-color: #fff !important;
                color: #26b99a;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-file-invoice-dollar"></i> Simulador de Cuotas</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <ul class="nav nav-tabs justify-content-end">
            <li class="nav-item">
                <a class="nav-link " href="tpers.aspx">Datos Generales</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="taldi.aspx">Dirección</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="talte.aspx">Teléfono</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="talco.aspx">Correo</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" href="tadmi.aspx">Solicitud</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="tredo.aspx">Documentos</a>
            </li>
        </ul>

        <asp:UpdatePanel ID="upd_simulador" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tadmi" runat="server">
                    <div class="row">
                        <div class="col-sm-3">
                            Matrícula
                                             <asp:UpdatePanel ID="updPnlBusca" runat="server">
                                                 <ContentTemplate>
                                                     <div class="input-group">
                                                         <asp:TextBox ID="txt_matricula" MaxLength="10" OnTextChanged="linkBttnBusca_Click" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                         <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                                                     </div>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-9">
                            Nombre
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            Periodo
                        <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-6">
                            <label for="ContentPlaceHolder1_ddl_Campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_Campus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col-sm-4">
                       Programa
                        <asp:DropDownList ID="ddl_Programa" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>                   
                    <div class="col-sm-4">
                        Tipo de ingreso
                        <asp:DropDownList ID="ddl_tipo_ingreso" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        Tasa Financiera
                        <asp:DropDownList ID="ddl_tasa_f" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                        </div>
                                        <div class="row">
                    <div class="col-md-4">
                        Plan de Cobro/Beca
                        <asp:DropDownList ID="ddl_plan_beca" runat="server" CssClass="form-control" OnSelectedIndexChanged="Carga_Simula" AutoPostBack="true"></asp:DropDownList>
                    </div>
                                            </div>
                                        <div class="row">
                    <div class="col">
                    <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                                            </div>
                </div>
                <hr />
                <div class="row text-center">
                    <div class="col">
                        <asp:GridView ID="GridSimulador" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false">
                            <Columns>
                                <asp:BoundField DataField="PARC" HeaderText="Parc"></asp:BoundField>
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CONCEPTO" HeaderText="Concepto" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="IMPORTE" HeaderText="Importe" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="DESCUENTO" HeaderText="Descuento" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="BECA" HeaderText="Beca" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="SALDO" HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="VENCIMIENTO" HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <RowStyle Font-Size="Small" />
                                                                <SelectedRowStyle CssClass="selected_table" />
                                                                <EmptyDataRowStyle ForeColor="#2A3F54" Width="14px" />
                                                                <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row text-center">
                    <div class="col">
                        <asp:LinkButton ID="back" runat="server" CssClass="btn btn-round btn-dark" OnClick="back_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;Regresar</asp:LinkButton>
                       &nbsp;                         <asp:LinkButton ID="linkBttnGenerarPdf" runat="server" CssClass="btn btn-round btn-dark" OnClick="linkBttnGenerarPdf_Click">Generar PDF</asp:LinkButton>
                                                                                    <asp:LinkButton ID="linkBttnGenEdoCta" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGenEdoCta_Click"><i class="fa fa-ticket" aria-hidden="true" OnClick="PDF_Click"></i>&nbsp;Generar Estado de Cuenta</asp:LinkButton>

                    
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

