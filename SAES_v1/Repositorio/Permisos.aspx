<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="SAES_v1.Repositorio.Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-cog"></i>
            Configuración de Permisos

        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4">
                Rol
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                        <asp:DropDownList ID="ddlRol" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                            </ContentTemplate>
                     </asp:UpdatePanel>
            </div>
    </div>
    
    <div id="Permisos_1" runat="server" style="font-size: 15px">
                <asp:Panel ID="UpdatePanel" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="form-row">
                                <div class="col-sm-3">
                                    <asp:Label ID="lblCampus" runat="server" Text="Lista de Campus"></asp:Label>
                                    
                                    <asp:ListBox ID="lstCampus" runat="server" Height="150px" CssClass="form-control" OnSelectedIndexChanged="lstCampus_SelectedIndexChanged" AutoPostBack="True" SelectionMode="Multiple"></asp:ListBox>
                                    <asp:CheckBox ID="CheckBox_campus" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                    <asp:Label ID="Label2" runat="server" Text="Seleccionar todos"></asp:Label>

                                </div>
                                <div class="col-sm-4">
                                    <asp:Label ID="lblUsuariosCampus" runat="server" Text="Usuarios en el Campus"></asp:Label>
                                    <asp:ListBox ID="lstUsuariosCampus" runat="server" Height="150px" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                                <div class="col-sm-1 text-center">
                                    <br />
                                    <asp:LinkButton ID="linkBttnAgregarUsuCampus" CssClass="btn btn-success" runat="server" OnClick="linkBttnAgregarUsuCampus_Click">
                                            <i class="fa fa-chevron-right" aria-hidden="true"></i></asp:LinkButton>
                                    <br />
                                    <asp:LinkButton ID="linkBttnEliminarUsuCampus" CssClass="btn btn-success" runat="server" OnClick="linkBttnEliminarUsuCampus_Click"><i class="fa fa-chevron-left" aria-hidden="true"></i></asp:LinkButton>

                                    <%--   <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                            Width="25px" OnClick="cmdAgregar_Click" /><br />
                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                            Width="25px" OnClick="cmdBorrar_Click" />--%>
                                </div>
                                <div class="col-sm-4 ">
                                    <asp:Label ID="Label9" runat="server" Text="Lista de Usuarios"></asp:Label>
                                    <asp:ListBox ID="lstUsuarios" runat="server" Height="150px" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>


                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </asp:Panel>
                <hr />
                <asp:Panel ID="UpdatePanel1" runat="server">
                    <div id="panel_1">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="form-row">
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label4" runat="server" Text="Lista de Niveles"></asp:Label>
                                        <asp:ListBox ID="ListNiveles" runat="server" Height="150px" CssClass="form-control"
                                            OnSelectedIndexChanged="ListNiveles_SelectedIndexChanged" AutoPostBack="True"
                                            SelectionMode="Multiple"></asp:ListBox>
                                        <asp:CheckBox ID="CheckBox_Niveles" runat="server" OnCheckedChanged="CheckBox_Niveles_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label3" runat="server" Text="Seleccionar todos"></asp:Label></td>

                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="Label5" runat="server" Text="Usuarios en el Nivel"></asp:Label>
                                        <asp:ListBox ID="ListUsuariosNivel" runat="server" Height="150px" CssClass="form-control"
                                            SelectionMode="Multiple"></asp:ListBox>

                                    </div>
                                    <div class="col-sm-1 text-center">
                                        <%--  <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                                Width="25px" OnClick="cmdAgregar_Click_n" /><br />
                                            <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                                Width="25px" OnClick="cmdBorrar_Click_n" />--%>
                                        <br />
                                        <asp:LinkButton ID="linkBttnDelsuNivel" runat="server" CssClass="btn btn-success" OnClick="linkBttnDelsuNivel_Click"><i class="fa fa-chevron-right" aria-hidden="true"></i></asp:LinkButton>

                                        <br />
                                        <asp:LinkButton ID="linkBttnAddUsuNivel" runat="server" CssClass="btn btn-success" OnClick="linkBttnAddUsuNivel_Click"><i class="fa fa-chevron-left" aria-hidden="true"></i></asp:LinkButton>



                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="Label10" runat="server" Text="Lista de Usuarios"></asp:Label>
                                        <asp:ListBox ID="ListUsuarios" runat="server" Height="150px" CssClass="form-control"
                                            SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="UpdatePanel2" runat="server">




                    <div id="panel_2" style="text-align: center;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col text-left font-weight-bold">
                                        <h4>
                                            <asp:Label ID="Label16" runat="server" Text="Asignar Documentos por Usuario" CssClass="text-info"></asp:Label>
                                        </h4>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label7" runat="server" Text="Lista de Tipos de Documentos"></asp:Label>
                                        <asp:ListBox ID="ListDocumentos" runat="server" Height="150px" CssClass="form-control" OnSelectedIndexChanged="ListDocumentos_SelectedIndexChanged"
                                            SelectionMode="Multiple" AutoPostBack="True"></asp:ListBox>
                                        <asp:CheckBox ID="CheckBox_Doc" runat="server" OnCheckedChanged="CheckBox_Doc_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label6" runat="server" Text="Seleccionar todos"></asp:Label></td>

                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="Label8" runat="server" Text="Documentos por Usuario"></asp:Label>
                                        <asp:ListBox ID="ListUsuariosDocumentos" runat="server" Height="150px" CssClass="form-control"
                                            SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    <div class="col-sm-1">

                                        <br />
                                        <asp:LinkButton ID="linkBttnDelUsuDoctos" runat="server" CssClass="btn btn-success" OnClick="linkBttnDelUsuDoctos_Click"><i class="fa fa-chevron-right" aria-hidden="true"></i></asp:LinkButton>

                                        <br />
                                        <asp:LinkButton ID="linkBttnInsUsuDoctos" runat="server" CssClass="btn btn-success" OnClick="linkBttnInsUsuDoctos_Click"><i class="fa fa-chevron-left" aria-hidden="true"></i></asp:LinkButton>

                                        <%--                                            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                                Width="25px" OnClick="cmdAgregar_Click_d" /><br />
                                            <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                                Width="25px" OnClick="cmdBorrar_Click_d" />
                                        --%>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="Label11" runat="server" Text="Lista de Usuarios"></asp:Label>
                                        <asp:ListBox ID="ListUsuarios_1" runat="server" Height="150px" CssClass="form-control"
                                            SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>

                                <br />
                                <div class="form">
                                    <div class="col">
                                        <h4>
                                            <asp:Label ID="Label17" runat="server" Text="Asignar Documentos por Estatus" CssClass="title"></asp:Label>
                                        </h4>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label12" runat="server" Text="Lista de Tipos de Documentos"></asp:Label>
                                        <asp:ListBox ID="ListDocs" runat="server" Height="150px" CssClass="form-control"
                                            SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ListDocs_SelectedIndexChanged"></asp:ListBox>
                                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged1" AutoPostBack="true" /><asp:Label ID="Label15" runat="server" Text="Seleccionar todos"></asp:Label></td>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="Label13" runat="server" Text="Documentos por Estatus"></asp:Label>
                                        <asp:ListBox ID="ListDocE" runat="server" Height="150px" CssClass="form-control"
                                            SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    <div class="col-sm-1">

                                        <br />
                                        <asp:LinkButton ID="linkBttnDelDoctosEstatus" runat="server" CssClass="btn btn-success" OnClick="linkBttnDelDoctosEstatus_Click"><i class="fa fa-chevron-right" aria-hidden="true"></i></asp:LinkButton>

                                        <br />
                                        <asp:LinkButton ID="linkBttnInsDoctosEstatus" runat="server" CssClass="btn btn-success" OnClick="linkBttnInsDoctosEstatus_Click"><i class="fa fa-chevron-left" aria-hidden="true"></i></asp:LinkButton>





                                        <%--  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                                Width="25px" OnClick="cmAgregarEstatus_Click" /><br />
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                                Width="25px" OnClick="cmBorrarEstatus_Click" />--%>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="Label14" runat="server" Text="Lista de Estatus"></asp:Label>
                                        <asp:ListBox ID="ListEstatus" runat="server" Height="150px" CssClass="form-control"
                                            SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <input id="bloqueado" type="image" value="button" onclick="slide_down_4(); return false" src="../Images/Repositorio/bloqueado.png" style="width: 20px;" />
                            <input id="desbloqueado" type="image" value="button" onclick="slide_up_4(); return false" src="../Images/Repositorio/desbloqueado.png" style="width: 20px;" />
                        </div>
                        <div class="col-md-6">
                            <label id="lbl_permisos_1" onclick="slide_down_lbl_p(); return false">Privilegios de la aplicación</label>
                            <label id="lbl_permisos_2" onclick="slide_up_lbl_p(); return false">Privilegios de la aplicación</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:Panel ID="GridView" runat="server">
                                <div id="privilegios_table">

                                    <div class="ex3">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="Permisos_App" runat="server" AutoGenerateColumns="False" RowHeaderColumn="IDPrivilegio" Width="100%" HorizontalAlign="Center" CssClass="table table-striped table-bordered" OnRowCommand="Permisos_App_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="IDPrivilegio" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">

                                                            <HeaderStyle CssClass="ColumnaOculta" />

                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="Expand" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID="MenuP" runat="server">
                                                            <%--<asp:Image ID="MenuP_" runat="server"CssClass="fa fa-plus-circle" />--%>
                                                                                                               <i class="fa fa-plus-circle text-success" aria-hidden="true"></i>

                                                                </asp:LinkButton>
                                                                <asp:LinkButton CommandName="Collapse" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID="MenuP2" runat="server" Visible="False">
                                                            <%--<asp:Image ID="Image5" runat="server" ImageUrl="../Images/Repositorio/eliminar.png" CssClass="column_image" />--%>
                                                                  <i class="fa fa-minus-circle" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Permiso" HeaderText="Permiso" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                                            <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50%">
                                                            <ItemStyle HorizontalAlign="Justify" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Activar/Desactivar">

                                                            <ItemTemplate>
                                                                <label class="switch">
                                                                    <asp:CheckBox ID="Checkbox_P" runat="server" OnCheckedChanged="Checkbox_P_CheckedChanged" AutoPostBack="true" />
                                                                    <%--<asp:CheckBox ID="Checkbox_P" runat="server" OnCheckedChanged="Checkbox_P_CheckedChanged" AutoPostBack="true" xmlns:asp="#unknown"/>--%>

                                                                    <span class="slider round"></span>
                                                                </label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <SelectedRowStyle CssClass="selected_table" />
                                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div id="save_button" style="margin-top: 15px;">
                                            <asp:Panel ID="Button" runat="server" HorizontalAlign="Center">
                                                <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="Button1_Click" />
                                            </asp:Panel>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                </asp:Panel>

                <%--</ContentTemplate>--%>
                <%--</asp:UpdatePanel>--%>
    </div>
    
    </div>



</asp:Content>
