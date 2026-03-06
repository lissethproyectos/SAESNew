<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadPage.aspx.cs" Inherits="SAES_v1.Repositorio.UploadPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
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
</asp:Content>
