<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comentarios.aspx.cs" Inherits="SAES_v1.Repositorio.Comentarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .WordWrap {
            width: 100%;
            word-break: break-all
        }

        .WordBreak {
            width: 100px;
            OVERFLOW: hidden;
            TEXT-OVERFLOW: ellipsis
        }
        .ui-dialog-titlebar .ui-widget-header .ui-corner-all .ui-helper-clearfix{
            display:none;
        }
        .ui-dialog-content {
                width: 100%!important;
                margin-top:15px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; margin-bottom:15px;"><asp:Label ID="Label1" runat="server" Text="Comentarios de Documento" Font-Size="X-Large" Font-Names="Raleway"></asp:Label></div>
        <div class="WordWrap" style="font-family: raleway">
            <asp:GridView ID="GridViewComentarios" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table-striped">
                <Columns>
                    <asp:BoundField HeaderText="Revisor" DataField="Nombre">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Comentario" DataField="Comentario">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="50%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Estatus" HeaderText="Estatus">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha" DataField="FechaUltModif">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
