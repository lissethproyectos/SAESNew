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
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Estado de Cuenta</h2>Existente para el periodo'
            })
        }
     </script>

    <style>
        .icon_regresa{
            width:100%;
            text-align:center;
            border-color:#FFF !important;
        }
        .icon_regresa:hover{
            background-color:#fff !important;
            color: #26b99a;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="x_title">
        <h2>
            <img src="Images/Admisiones/lucro.png" style="width: 30px;" /><small>Simulador de Cuotas</small></h2>
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
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-2" style="margin:auto;text-align:center;">
                            <asp:LinkButton ID="back" runat="server" CssClass=" icon_regresa  btn-outline-secondary" OnClick="back_Click"><i class="fas fa-arrow-circle-left fa-2x"></i></asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Matrícula</label>
                            <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Nombre</label>
                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="w-100"></div>  
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_periodo" class="form-label">Periodo</label>
                            <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>                      
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_Campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_Campus" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_Programa" class="form-label">Programa</label>
                            <asp:DropDownList ID="ddl_Programa" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="w-100"></div>  
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_tipo_ingreso" class="form-label">Tipo de ingreso</label>
                            <asp:DropDownList ID="ddl_tipo_ingreso" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_tasa_f" class="form-label">Tasa Financiera</label>
                            <asp:DropDownList ID="ddl_tasa_f" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_plan_beca" class="form-label">Plan de Cobro/Beca</label>
                            <asp:DropDownList ID="ddl_plan_beca" runat="server" CssClass="form-control" OnSelectedIndexChanged="Carga_Simula" AutoPostBack="true"></asp:DropDownList>
                        </div>    
                        <asp:Label ID="lbl_id_pers" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_consecutivo" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                <div id="table_tsimu">
                    <<asp:GridView ID="GridSimulador" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false">
                         <Columns>
                            <asp:BoundField DataField="PARC" HeaderText="Parc" ></asp:BoundField>
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" ITEMSTYLE-HORIZONTALALIGN="Center"/>
                            <asp:BoundField DataField="CONCEPTO" HeaderText="Concepto" ITEMSTYLE-HORIZONTALALIGN="Left"/>
                             <asp:BoundField DataField="IMPORTE" HeaderText="Importe" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="DESCUENTO" HeaderText="Descuento" ITEMSTYLE-HORIZONTALALIGN="Right"></asp:BoundField>
                            <asp:BoundField DataField="BECA" HeaderText="Beca" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="SALDO" HeaderText="Saldo" ITEMSTYLE-HORIZONTALALIGN="Right"/>
                            <asp:BoundField DataField="VENCIMIENTO" HeaderText="Vencimiento" ITEMSTYLE-HORIZONTALALIGN="Center"/>
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="LightSeaGreen" ForeColor="white" />
                    </asp:GridView>>
                </div>
                <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                    <div class="col-md-1" style="text-align:center;">
                            <label for="ContentPlaceHolder1_simualdor" class="form-label">Generar PDF</label>
                            <asp:ImageButton ID="ImgPdf" runat="server" ImageUrl="~/Images/Operaciones/PDF.jpg" Height="40px" Width="40px"
                                             TOOLTIP="Generar PDF"  VISIBLE="false" OnClick="PDF_Click"/>
                    </div>
                    <div class="col-md-1" style="text-align:center;">
                            <label for="ContentPlaceHolder1_simualdor" class="form-label">Estado Cuenta</label>
                            <asp:ImageButton ID="ImgEdocta" runat="server" ImageUrl="~/Images/Operaciones/Edo_cuenta.png" Height="45px" Width="45px"
                                             TOOLTIP="Generar Estado de Cuenta"  VISIBLE="false" OnClick="Edo_Cta_Click"/>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

