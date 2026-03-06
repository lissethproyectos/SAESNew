<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tapba.aspx.cs" Inherits="SAES_v1.tapba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <script>
        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error_layout2() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Ya existe un layout de ese banco y fecha de pago</h2>'
            })
        }

        function error_layout() {
            swal({
                title: "Are you sure?",
                text: "Once deleted, you will not be able to recover this imaginary file!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        swal("Poof! Your imaginary file has been deleted!", {
                            icon: "success",
                        });
                    } else {
                        swal("Your imaginary file is safe!");
                    }
                });
        }

        function save() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se cargo el archivo exitosamente</h2>Favor de validar en el listado.'
            })
        }

        function pago_aplicado() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Los pagos han sido aplicados exitosamente</h2>Favor de validar en el listado.'
            })
        }
    </script>
    <style>
        span button {
            margin-bottom: 0px !important;
        }

        legend {
            display: block;
            padding-left: 2px;
            padding-right: 2px;
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="x_title">
            <h2>
                <i class="fa fa-check-square" aria-hidden="true"></i>Aplicación de pagos por banco
            </h2>
            <div class="clearfix"></div>
        </div>
        <div class="form-row">
            <div class="col-sm-5">
                <asp:Label ID="Label1" runat="server" Text="Banco" class="form-label"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_banco" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_banco_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqBanco" runat="server" CssClass="text-danger" ErrorMessage="Selecciona banco" ControlToValidate="ddl_banco" InitialValue="" ValidationGroup="subir" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-4">
                Configuración
               <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                   <ContentTemplate>
                       <asp:DropDownList ID="ddl_config" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_config_SelectedIndexChanged" AutoPostBack="True">
                           <asp:ListItem Value="">------</asp:ListItem>
                           <asp:ListItem Value="T">Archivo de Texto</asp:ListItem>
                           <asp:ListItem Value="C">Por Columna</asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="reqConf" runat="server" CssClass="text-danger" ErrorMessage="Selecciona configuración" ControlToValidate="ddl_config" InitialValue="" ValidationGroup="subir" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   </ContentTemplate>
               </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Fecha Carga
                  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                      <ContentTemplate>
                          <asp:TextBox ID="txt_fecha_c" runat="server" class="form-control" OnTextChanged="txt_fecha_c_TextChanged" type="date" pattern="\d{4}-\d{2}-\d{2}" AutoPostBack="True"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ErrorMessage="Se requiere fecha de carga" ControlToValidate="txt_fecha_c" ValidationGroup="subir"></asp:RequiredFieldValidator>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <%--<div class="col-sm-2">
                <br />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkBttnVer" runat="server" CssClass="btn btn-secondary" OnClick="linkBttnVer_Click">Buscar</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>--%>
        </div>
        <div class="form-row">
            <div class="col">
                Archivo
                 <div class="input-group">
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                             <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                             <asp:RequiredFieldValidator ID="reqFile" runat="server" CssClass="text-danger" ErrorMessage="Selecciona archivo" ControlToValidate="fileUpload" InitialValue="" ValidationGroup="subir" SetFocusOnError="True"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="valArchivo" CssClass="text-danger" runat="server" ControlToValidate="fileUpload" ErrorMessage="Archivo incorrecto, debe ser un archivo CSV" ValidationExpression="(.*?)\.(txt|TXT|Txt|csv|CSV|Csv)$" ValidationGroup="subir"></asp:RegularExpressionValidator>
                             <asp:HiddenField ID="hddnFile" runat="server" />
                             </ContentTemplate>
                         <Triggers>
                             <asp:PostBackTrigger ControlID="linkBttnUpload" />
                         </Triggers>
                     </asp:UpdatePanel>
                     <%--                     <asp:LinkButton ID="linkBttnUpload" runat="server" OnClick="linkBttnUpload_Click" CssClass="btn btn-success">Subir</asp:LinkButton>--%>
                 </div>

            </div>
        </div>

        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkBttnUpload" runat="server" OnClick="linkBttnUpload_Click" CssClass="btn btn-success" ValidationGroup="subir"><i class="fa fa-cloud-upload" aria-hidden="true"></i> Subir</asp:LinkButton>
                        <asp:LinkButton ID="linkBtnnAplica" runat="server" OnClick="linkBtnnAplica_Click" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Aplicar</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGrid" runat="server"
                    AssociatedUpdatePanelID="updPnlGrid">
                    <ProgressTemplate>
                        <asp:Image ID="img10" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>

        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridtapba" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tapba_tpers_num" HeaderText="Matricula" />
                                <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tapba_consecutivo" HeaderText="No. Transacción" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tapba_fecha_pago" HeaderText="Fecha Pago" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tapba_importe" HeaderText="Importe">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tapba_comentario" HeaderText="Obsevaciones" />
                            </Columns>
                            <RowStyle Font-Size="Small" />
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>



    <!-- Modal -->
    <div class="modal fade" id="modalEliminar" tabindex="-1" role="dialog" aria-labelledby="modalDel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirmar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col text-center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/interrogacion.png" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblMsjError" runat="server" CssClass="font-weight-bold" Font-Size="16px"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="linkBttnCancelar" runat="server" class="btn btn-secondary" data-dismiss="modal">CANCELAR</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnEliminar" runat="server" class="btn btn-primary" OnClick="linkBttnEliminar_Click">OK</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal -->
    <div class="modal fade" id="modalAplicar" tabindex="-1" role="dialog" aria-labelledby="modalApli" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalAplicar">Aplicar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <h5>¿Esta seguro de aplicar los pagos?</h5>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="linkCancelarAplicar" runat="server" class="btn btn-secondary" data-dismiss="modal">CANCELAR</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnAplicar" runat="server" class="btn btn-primary" OnClick="linkBttnAplicar_Click">OK</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script>
        function load_datatable() {
            $('#<%= Gridtapba.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtapba.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "language": {
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
                "scrollResize": true,
                "scrollY": '500px',
                "scrollCollapse": true,
                "order": [
                    [0, "asc"]
                ],
                "autoWidth": true,
                "dom": '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'Archivo banco',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        }
                    },
                    {
                        title: 'Archivo banco',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6]
                        }
                    }
                ],
                "stateSave": true
            });
        }
        function ctrl_fecha_c() {
            $.ajax({
                type: "POST",
                url: "tapba.aspx/grid_bind_tapba",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });



            $('#ContentPlaceHolder1_txt_fecha_c').datepicker({
                uiLibrary: 'bootstrap4',
                locale: 'es-es',
                format: "dd/mm/yyyy"
            });
        }
    </script>
</asp:Content>
