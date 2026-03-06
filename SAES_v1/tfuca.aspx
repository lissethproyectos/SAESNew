<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tfuca.aspx.cs" Inherits="SAES_v1.tfuca" %>

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

        function error_clave() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- La clave ingresada ya existe</h2>'
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-users" aria-hidden="true"></i>
            &nbsp;Funcionarios por Campus</h2>
        <div class="clearfix"></div>
    </div>

    <hr />
    <div class="container-fluid">
        <div class="form-row">
            <div class="col-sm-8">
                Campus
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqCampus" runat="server" ErrorMessage="Favor de seleccionar campus" ControlToValidate="ddl_campus" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Clave Funcionario
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_funcionarios" CssClass="form-control" runat="server">
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="reqFuncionarios" runat="server" ErrorMessage="Favor de seleccionar un funcionario" ControlToValidate="ddl_funcionarios" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-4">
                Nombre
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNombre" runat="server" ErrorMessage="Favor de ingresar nombre" ControlToValidate="txt_nombre" ValidationGroup="guardar" CssClass="text-danger" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Ap. Paterno
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                     <ContentTemplate>
                         <asp:TextBox ID="txt_paterno" runat="server" CssClass="form-control"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqPaterno" runat="server" ErrorMessage="Favor de ingresar apellido paterno" ControlToValidate="txt_paterno" ValidationGroup="guardar" CssClass="text-danger" SetFocusOnError="True"></asp:RequiredFieldValidator>
                     </ContentTemplate>
                 </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Ap. Materno
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_materno" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqMaterno" runat="server" ErrorMessage="Favor de ingresar apellido materno" ControlToValidate="txt_materno" ValidationGroup="guardar" CssClass="text-danger" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-4">
                CURP
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_curp" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqCurp" runat="server" ErrorMessage="Favor de ingresar CURP" ControlToValidate="txt_curp" ValidationGroup="guardar" CssClass="text-danger" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Estatus
                <asp:UpdatePanel ID="updPnlEstatus" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="updPnlBotones" runat="server">
            <ContentTemplate>
                <div class="row" id="btn_tfuca" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" ValidationGroup="guardar" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtfuca" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="Gridtfuca_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="tfuca_clave" HeaderText="Clave" />
                                <asp:BoundField DataField="tfuca_desc" HeaderText="Descripción" />
                                <asp:BoundField DataField="tfuca_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tfuca_paterno" HeaderText="Paterno" />
                                <asp:BoundField DataField="tfuca_materno" HeaderText="Materno" />
                                <asp:BoundField DataField="tfuca_curp" HeaderText="CURP" />
                                <asp:BoundField DataField="tfuca_tuser_clave" HeaderText="Fecha Registro" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha Registro" />
                                <asp:BoundField DataField="estatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="tfuca_tcamp_clave" HeaderText="Campus" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
