<%@ Page Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="CargaDocumentos.aspx.cs" Inherits="SAES_v1.Repositorio.CargaDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.js" integrity="sha512-n/4gHW3atM3QqRcbCn6ewmpxcLAHGaDjpEBu4xZd47N0W2oQ+6q7oc3PXstrJYXcbNU1OHdQ1T7pAP+gi5Yu8g==" crossorigin="anonymous"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Script/fileinput.js"></script>
    <script>
        function JQDialog(contentUrl, closeurl) {
            var $dialog = $('<div class=""></div>')
                .html('<iframe style="border: 0px;overflow-x:hidden;" src="' + contentUrl + '" width="100%" height="100%" scrolling="no"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 480,
                    width: 1300,
                    closeOnEscape: false,
                    dialogClass: 'hide-close',
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    title: ('<div style="text-align: right;"><a href="' + closeurl + '"><i class="fas fa-times"></i></a></div>')
                });
            $dialog.dialog('open'); return false;

        }
        function JQDialog_preview(contentUrl, closeurl) {
            var $dialog = $('<div class=""></div>')

                .html('<iframe style="border: 0px; " src="' + contentUrl + '" width="100%" height="100%" align="center"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 680,
                    width: 950,
                    closeOnEscape: false,
                    dialogClass: 'hide-close',
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    title: ('<div style="text-align: right;"><a href="' + closeurl + '"><i class="fas fa-times"></i></a></div>')
                });
            $dialog.dialog('open'); return false;

        }
        function cargar_doc(contentUrl, closeurl) {
            swal({
                //title: 'Documentos Existentes',
                //text: "",
                //type: 'info',
                html: '<h2 class="swal2-title" id="swal2-title"><img src="../images/repositorio/icono_sesion_alert.png" style="width: 100px;"><br><br>Documentos Existentes</h2>El alumno ya cuenta con un archivo cargado, si deseas continuar se eliminará el archivo actual.<br>¿Deseas continuar?',
                showCancelButton: true,
                cancelButtonText: 'No',
                confirmButtonText: 'Si'
            }).then(function () {
                var $dialog = $('<div class=""></div>')

                    .html('<iframe style="border: 0px; " src="' + contentUrl + '" width="100%" height="100%" align="center"></iframe>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        height: 480,
                        width: 950,
                        closeOnEscape: false,
                        dialogClass: 'hide-close',
                        show: {
                            effect: "fade",
                            duration: 1000
                        },
                        title: ('<div style="text-align: right;"><a href="' + closeurl + '"><i class="fas fa-times"></i></a></div>')
                    });
                $dialog.dialog('open');
                return false;
            }, function (dismiss) {
                console.log(dismiss);
                if (dismiss == 'cancel') {
                    window.location.reload();
                }
            })
        }
        function JQDialog_Comentarios(contentUrl, closeurl) {
            var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px;overflow-x:hidden;" src="' + contentUrl + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    autoResize: true,
                    modal: true,
                    height: 300,
                    width: 1300,
                    dialogClass: 'hide-close',
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    hide: {
                        effect: "fade",
                        duration: 1000
                    },
                    title: ('<div style="text-align: right;"><a href="' + closeurl + '"><i class="fas fa-times"></i></a></div>')
                });
            $dialog.dialog('open'); return false;

        }
    </script>
    <style>
        .ColumnaOculta {
            display: none;
        }

        .progress_1 {
            display: flex;
            height: 1rem;
            overflow: hidden;
            font-size: .75rem;
            background-color: #e9ecef;
            border-radius: .25rem;
        }

        #ContentPlaceHolder1_List_Docs_info {
            float: left;
        }

        #ContentPlaceHolder1_List_Docs tbody tr {
            cursor: pointer;
        }

        #ContentPlaceHolder1_List_Docs_length {
            float: left;
        }

        .bottom {
            text-align: center;
            padding-top: 5px;
        }

        @media screen and (max-width: 540px) {
            .bottom {
                margin-top: 50px;
                text-align: center;
                display: grid;
            }
        }

        .dataTables_paginate .active a {
            background: #2a3f54 !important;
            border-color: #2a3f54 !important;
            color: #fff !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="../Images/Repositorio/folder.png" style="width: 45px;" /><small>Carga de Documentos</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <div class="row justify-content-center">
            <div class="col-md-12" style="text-align: center">
                <h3>
                    <asp:Label ID="Nombre_Alumno" runat="server"></asp:Label></h3>
            </div>
            <div class="col-md-12" style="margin-bottom: 15px;">
                <div class="progress_1" style="width: 50%; height: 35px; margin: auto;">
                    <div id="html_progress_bar" class="progress-bar progress-bar-striped progress-bar-animated" runat="server">
                        <asp:Label ID="lbl_bar" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <asp:GridView ID="List_Docs" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered" HorizontalAlign="Center" OnRowCommand="List_Docs_RowCommand" OnRowDataBound="List_Docs_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IDTipoDocumento" HeaderText="IDTipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                    <HeaderStyle CssClass="ColumnaOculta" />
                    <ItemStyle CssClass="ColumnaOculta" />
                </asp:BoundField>
                <asp:BoundField DataField="IDDocumento" HeaderText="IDDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                    <HeaderStyle CssClass="ColumnaOculta" />
                    <ItemStyle CssClass="ColumnaOculta" />
                </asp:BoundField>
                <asp:BoundField DataField="IDAlumno" HeaderText="IDAlumno" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                    <HeaderStyle CssClass="ColumnaOculta" />
                    <ItemStyle CssClass="ColumnaOculta" />
                </asp:BoundField>
                <asp:BoundField DataField="Documento" HeaderText="Documento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                    <HeaderStyle CssClass="ColumnaOculta" />
                    <ItemStyle CssClass="ColumnaOculta" />
                </asp:BoundField>
                <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                <asp:TemplateField HeaderText="Especificaciones del documento">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Documento") %>' Font-Bold="true"></asp:Label>.
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Comentarios" HeaderText="Comentarios de Revisión" Visible="false" />
                <asp:BoundField DataField="Minimo" HeaderText="Minimo" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                    <HeaderStyle CssClass="ColumnaOculta" />
                    <ItemStyle CssClass="ColumnaOculta" />
                </asp:BoundField>
                <asp:BoundField DataField="Maximo" HeaderText="Maximo" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                    <HeaderStyle CssClass="ColumnaOculta" />
                    <ItemStyle CssClass="ColumnaOculta" />
                </asp:BoundField>
                <asp:BoundField DataField="Formato" HeaderText="Formato" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                    <HeaderStyle CssClass="ColumnaOculta" />
                    <ItemStyle CssClass="ColumnaOculta" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="True" HeaderText="Comentarios de Revisión">
                    <ItemTemplate>

                        <asp:ImageButton ID="imgComentarios" runat="server" Height="24px"
                            ImageUrl="~/Images/Repositorio/comentarios.png" CommandName="Comentarios" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            AlternateText="Subir Archivo" Visible="true" OnClientClick="CallButtonEvent()" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="True" HeaderText="Subir Documento">
                    <ItemTemplate>

                        <asp:ImageButton ID="imgExpediente" runat="server" Height="24px"
                            ImageUrl="~/Images/Repositorio/Upload_n.png" CommandName="Subir" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            AlternateText="Subir Archivo" Visible="true" OnClientClick="CallButtonEvent()" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="True" HeaderText="Ver Documento">
                    <ItemTemplate>

                        <asp:ImageButton ID="imgPreview" runat="server" Height="24px"
                            ImageUrl="~/Images/Repositorio/preview.png" CommandName="Preview" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            AlternateText="Vista Previa" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
        <script>
            function load_datatable() {
                let table_documentos = $("#ContentPlaceHolder1_List_Docs").DataTable({
                    language: {
                        bProcessing: 'Procesando...',
                        sLengthMenu: 'Mostrar _MENU_ registros',
                        sZeroRecords: 'No se encontraron resultados',
                        sEmptyTable: 'Ningún dato disponible en esta tabla',
                        sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
                        sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
                        sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
                        sInfoPostFix: '',
                        sSearch: 'Buscar:',
                        sUrl: '',
                        sInfoThousands: '',
                        sLoadingRecords: 'Cargando...',
                        oPaginate: {
                            sFirst: 'Primero',
                            sLast: 'Último',
                            sNext: 'Siguiente',
                            sPrevious: 'Anterior'
                        }
                    },
                    scrollResize: true,
                    scrollY: '500px',
                    scrollCollapse: true,
                    order: [
                        [0, "asc"]
                    ],
                    lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                    "autoWidth": true,
                    dom: '<"top"if>rt<"bottom"p><"clear">',
                    stateSave: true
                });
                $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
            }
        </script>
</asp:Content>
