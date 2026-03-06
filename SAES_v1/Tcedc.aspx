<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tcedc.aspx.cs" Inherits="SAES_v1.Tcedc" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="cuerpo">
        <asp:UpdatePanel ID="UpdDatos" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                <div id="printDiv">      
                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div3" runat="server">
                    <div class="col-md-1">
                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Operaciones/edo_cuenta.png" Height="50px" Width="50px"
                                    VISIBLE="true"/>
                    </div>
                    <div class="col-md-3" style="text-align: right; ">
                        <asp:Label ID="lbltitulo" runat="server"  Text="CONSULTA ESTADO DE CUENTA"  Font-Size="Medium" style="text-align: right"/>       
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div1" runat="server">
                    <div class="col-md-3" style="text-align: right; ">
                        <asp:Label ID="LblMatricula" runat="server"  Text="MATRÍCULA"  Font-Size="Medium" style="text-align: right"/>       
                    </div>
                    <div class="col-md-6" style="text-align: left";>
                            <asp:TextBox ID="TxtMatricula" runat="server" MaxLength="10" OnTextChanged="Alumno" Visible="True" WIDTH="100px"></asp:TextBox>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: left; margin: auto;" id="Div2" runat="server">
                    <div class="col-md-3" style="text-align: right; ">
                        <asp:Label ID="Lblnombre_alumno" runat="server"  Text="NOMBRE DEL ALUMNO:"  Font-Size="Medium" style="text-align: right"/>       
                    </div>
                    <div class="col-md-6" style="text-align: left; ">
                            <asp:Label ID="LblNombre" runat="server" CssClass="bold" style="color:black"></asp:Label>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: left; margin: auto;" id="Div4" runat="server">
                    <div class="col-md-3" style="text-align: right; ">
                        <asp:Label ID="Label8" runat="server"  Text="PROGRAMA:"  Font-Size="Medium" style="text-align: right"/>       
                    </div>
                    <div class="col-md-6" style="text-align: left;">
                            <asp:Label ID="Label6" runat="server" CssClass="bold" style="color:black"></asp:Label>
                    </div>
                </div>

                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div5" runat="server">
                        <div class="col-md-4" style="text-align: center; background-color:#2e7b92; color: White;">
                            <asp:Label ID="Label7" runat="server"  Text="Resumen Estado de Cuenta:"  Font-Size="Medium" style="text-align: right"/>       
                        </div>

                </div>
                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div6" runat="server">
                    <div class="col-md-2" style="text-align: right; background-color:lightgray; color: Black;">
                            <asp:Label ID="Label9" runat="server"  Text="Total de Cargos $"   style="text-align: right"/>       
                    </div>  
                     <div class="col-md-2" style="text-align: right; background-color:lightgray; color: black;">
                            <asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Medium" CSSCLASS="bold"></asp:Label>      
                    </div>
               </div>
               <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div7" runat="server">
                   <div class="col-md-2" style="text-align: right; background-color:lightgray; color: Black;">
                            <asp:Label ID="Label10" runat="server"  Text="Total de Pagos $"   style="text-align: right"/>       
                    </div>
                    <div class="col-md-2" style="text-align: right; background-color:lightgray; color: black;">
                            <asp:Label ID="Label3" runat="server" Text="Label" CSSCLASS="bold" Font-Size="Medium"></asp:Label>      
                    </div>

                </div>
                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div8" runat="server">
                   <div class="col-md-2" style="text-align: right; background-color:lightgray; color: Black;">
                            <asp:Label ID="Label11" runat="server"  Text="Total de Beca/Descuentos $"   style="text-align: right"/>       
                    </div>
                    <div class="col-md-2" style="text-align: right; background-color:lightgray; color: black;">
                            <asp:Label ID="Label4" runat="server" Text="Label" CSSCLASS="bold" Font-Size="Medium"></asp:Label>      
                    </div>

                </div>
                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div13" runat="server">
                   <div class="col-md-2" style="text-align: right; background-color:lightgray; color: Black;">
                            <asp:Label ID="Label15" runat="server"  Text="Total de Cancelaciones $"   style="text-align: right"/>       
                    </div>
                    <div class="col-md-2" style="text-align: right; background-color:lightgray; color: black;">
                            <asp:Label ID="Label16" runat="server" Text="Label" CSSCLASS="bold" Font-Size="Medium"></asp:Label>      
                    </div>

                </div>
                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div9" runat="server">
                   <div class="col-md-2" style="text-align: right; background-color:lightgray; color: Black;">
                            <asp:Label ID="Label12" runat="server"  Text="Saldo Final $"   style="text-align: right"/>       
                    </div>
                    <div class="col-md-2" style="text-align: right; background-color:lightgray; color: black;">
                            <asp:Label ID="Label5" runat="server" Text="Label" CSSCLASS="bold" Font-Size="Medium"></asp:Label>      
                    </div>

                </div>
                </br>
                <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div10" runat="server">
                        <div class="col-md-2" style="text-align: right;  color: Black;">
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Operaciones/btn_referencias.png" Height="37px" Width="150px"
                                 TOOLTIP="Ref.Bancarias"  VISIBLE="true" OnClick="Referencia_Click" BorderWidth="1"/>
                        </div>
                        <div class="col-md-2" style="text-align: right;  color: Black;">
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Operaciones/btn_descargar.png" Height="37px" Width="150px"
                                 TOOLTIP="Descargar PDF"  VISIBLE="true" OnClick="CmdLogin_Click1" BorderWidth="1" />
                        </div>
                        <div class="col-md-2" style="text-align: right;  color: Black;">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Operaciones/btn_salir.png" Height="37px" Width="150px"
                                 TOOLTIP="Salir"  VISIBLE="true" OnClick="Cerrar_Click" BorderWidth="1"/>
                        </div>
                </div>
                </br>
               <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div11" runat="server">
                        <div class="col-md-4" style="text-align: center;  color: Black;">
                            <asp:Label ID="Label13" runat="server"  Text="DETALLE DE MOVIMIENTOS"   style="text-align: center"/> 
                        </div>
                </div>>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
                    <ContentTemplate>
                        <div class="menu" align="center">
                            <div class="row justify-content-center" style="text-align: right; margin: auto;" id="Div12" runat="server">
                                <div class="col-md-9" style="text-align: center; background-color:#3c9ab6; color: White;">
                                        <asp:Label ID="Label14" runat="server" Font-Size="Medium" Text="Periodo------------Concepto--------------------------------------Costo--------Pagos------Fecha Venc.-------Saldo"   style="text-align: center"/>       
                                </div>
                            </div>
                        </div>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                
                <br />
                <table align="center" class="table-responsive" width="100%">
                      <tr>
                        <td class="table-responsive" align="center" >
                                <asp:GridView ID="GridViewConceptos" runat="server"  BORDERCOLOR="#006457" BORDERSTYLE="None" BORDERWIDTH="1px" CELLPADDING="3"
                                            autogeneratecolumns="False"
                                            ROWSTYLE-FONT-SIZE="Small"
                                            Visible="false"
                                            onselectedindexchanged="GridConceptos_Click">
                                    <columns>
                                                <asp:ButtonField buttontype="Image" ImageUrl="~/Images/sel.png"  ControlStyle-Height="20px" ControlStyle-Width="20px" CommandName="Select" />
                                                <asp:BoundField DataField="transaccion" 
                                                     HeaderText="No.Transacción" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="transaccion" />
                                                 <asp:BoundField DataField="codigo" 
                                                     HeaderText="Código" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="codigo" />
                                                 <asp:BoundField DataField="descrip" 
                                                     HeaderText="Concepto Cargo" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="descrip" />
                                                 <asp:BoundField DataField="balance" 
                                                     HeaderText="Saldo" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="balance" 
                                                     ITEMSTYLE-HORIZONTALALIGN="Right"/>
                                                 <asp:BoundField DataField="vencim" 
                                                     HeaderText="Fecha Vencimiento" 
                                                     SortExpression="vencim" />
                                    </columns>
                                        <selectedrowstyle backcolor="LightBlue"
                                             forecolor="Black"
                                             font-bold="true"/>
                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        <HEADERSTYLE BACKCOLOR="#3c9ab6" FORECOLOR="White" />
                                        <RowStyle Font-Size="Small"></RowStyle>
                                    </asp:GridView>
                          </td>
                    </tr>
                </table>
                <table align="center" class="table-responsive" width="100%">
                      <tr>
                        <td class="table-responsive" align="center" >
                                <asp:GridView ID="GridViewRef" runat="server"  BORDERCOLOR="#006457" BORDERSTYLE="None" BORDERWIDTH="1px" CELLPADDING="3"
                                            autogeneratecolumns="False"
                                            ROWSTYLE-FONT-SIZE="Small"
                                            Visible="false">
                                    <columns>
                                                <asp:BoundField DataField="codigo" 
                                                     HeaderText="codigo" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="codigo" />
                                                 <asp:BoundField DataField="banorte" 
                                                     HeaderText="banorte" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="banorte" />
                                                 <asp:BoundField DataField="bancomer" 
                                                     HeaderText="bancomer" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="bancomer" />
                                                 <asp:BoundField DataField="santander" 
                                                     HeaderText="santander" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="santander" 
                                                     ITEMSTYLE-HORIZONTALALIGN="Right"/>
                                                 <asp:BoundField DataField="pidm" 
                                                     HeaderText="pidm" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="pidm" 
                                                     ITEMSTYLE-HORIZONTALALIGN="Right"/>
                                                <asp:BoundField DataField="periodo" 
                                                     HeaderText="periodo" 
                                                     InsertVisible="True" ReadOnly="False" 
                                                     SortExpression="periodo" 
                                                     ITEMSTYLE-HORIZONTALALIGN="Right"/>
                                    </columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        <HEADERSTYLE BACKCOLOR="#3c9ab6" FORECOLOR="White" />
                                        <RowStyle Font-Size="Small"></RowStyle>
                                    </asp:GridView>
                          </td>
                    </tr>
                </table>
                    <asp:Label ID="Lblerror" runat="server" Text="--- Selecciona un concepto a la vez para la generación del cupón de pago en bancos---" CSSCLASS="bold" Width="800px" visible="false" ForeColor="Red" ></asp:Label>
   </br></br>
               
               </div>
                
            
                <br />
                <br />
               
                
                <br />
                <br />
                <script>


                    function getData() {

                        var boton = document.getElementById('CmdLogin');

                        boton.click();

                    }

                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>




