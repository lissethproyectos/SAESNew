<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Periodos_Escolares.aspx.cs" Inherits="SAES_v1.Periodos_Escolares" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/calendario.png" style="width: 30px;" /><small>Periodos Escolares</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <div class="row justify-content-center" style="text-align: center; margin: auto;">
            <div class="col-2">
                <asp:Label ID="lblcterm" runat="server" Text="Clave"></asp:Label>
            </div>
            <div class="col-3">
                <asp:Label ID="lblpterm" runat="server" Text="Periodo"></asp:Label>
            </div>
            <div class="col-3">
                <asp:Label ID="lbloterm" runat="server" Text="Oficial"></asp:Label>
            </div>
            <div class="col-2">
                <asp:Label ID="lbleterm" runat="server" Text="Estatus"></asp:Label>
            </div>
        </div>
        <div class="row justify-content-center" style="text-align: center; margin: auto;">
            <div class="col-2">
                <asp:TextBox ID="c_term" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="p_term" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="of_term" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-2">
                <asp:DropDownList ID="e_term" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row justify-content-center" style="text-align: center; margin: auto;">
            <div class="col-3">
                <asp:Label ID="lblf_inicio_term" runat="server" Text="Fecha Inicio"></asp:Label>
            </div>
            <div class="col-3">
                <asp:Label ID="lblf_fin_term" runat="server" Text="Fecha Termino"></asp:Label>
            </div>
        </div>
        <div class="row justify-content-center" style="text-align: center; margin: auto;">
            <div class="col-3">
                <asp:TextBox ID="f_inicio_term" runat="server" CssClass="date-picker form-control" Onclick="this.type='date'"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="f_fin_term" runat="server" CssClass="date-picker form-control" Onclick="this.type='date'"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row justify-content-center" style="text-align: center; margin: auto;">
            <div class="col-3">
                <asp:Button ID="save_term" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" />
                <asp:Button ID="update_term" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>
