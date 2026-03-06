<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcdoc.aspx.cs" Inherits="SAES_v1.tcdoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    
    <style>
        span button {
            margin-bottom: 0px !important;
        }
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
    <script>
        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error_transaccion() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Transacción de Base de Datos</h2>'
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Admisiones/buscar.png" style="width: 30px;" /><small>Consulta de Documentos</small></h2>
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
                <a class="nav-link" href="tadmi.aspx">Solicitud</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" href="tredo.aspx">Documentos</a>
            </li>
        </ul>
        <asp:UpdatePanel ID="upd_c_documentos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:LinkButton ID="back" runat="server" CssClass=" icon_regresa  btn-outline-secondary" OnClick="back_Click"><i class="fas fa-arrow-circle-left fa-2x"></i></asp:LinkButton>
                <div id="form_tcdoc" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_periodo" class="form-label">Periodo</label>
                            <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" ></asp:DropDownList>                            
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>   
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_programa" class="form-label">Programa</label>
                            <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" ></asp:DropDownList>   
                        </div>
                        <div class="w-100"></div>  
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_documento" class="form-label">Documento</label>
                            <asp:DropDownList ID="ddl_documento" runat="server" CssClass="form-control" ></asp:DropDownList>   
                        </div>                      
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_estatus_doc" class="form-label">Estatus Docuemnto</label>
                            <asp:DropDownList ID="ddl_estatus_doc" runat="server" CssClass="form-control" ></asp:DropDownList>   
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_estatus_sol" class="form-label">Estatus Solicitud</label>
                            <asp:DropDownList ID="ddl_estatus_sol" runat="server" CssClass="form-control" ></asp:DropDownList>   
                        </div>
                        <div class="w-100"></div>  
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txt_fecha_l" class="form-label">Fecha Límite</label>
                            <asp:TextBox ID="txt_fecha_l"  runat="server" CssClass="form-control"></asp:TextBox>
                            <script>
                                function ctrl_fecha_i() {
                                    $('#ContentPlaceHolder1_txt_fecha_l').datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcdoc" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click"/>
                        <asp:Button ID="btn_consultar" runat="server" CssClass="btn btn-round btn-success" Text="Consultar" OnClick="btn_consultar_Click"/>
                    </div>
                </div>
                <div id="table_tcdoc">
                    <asp:GridView ID="GridCDocumentos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small">
                         <Columns>
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                            <asp:BoundField DataField="PERIODO" HeaderText="Periodo" />
                            <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                             <asp:BoundField DataField="PROGRAMA" HeaderText="Programa" />
                             <asp:BoundField DataField="DOCUMENTO" HeaderText="Documento" />
                             <asp:BoundField DataField="ESTATUS_DOCUMENTO" HeaderText="Estatus Documento" />
                             <asp:BoundField DataField="ESTATUS_SOLICITUD" HeaderText="Estatus Solicitud" />
                             <asp:BoundField DataField="FECHA_LIMITE" HeaderText="Fecha Limite" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function load_datatable() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridCDocumentos").DataTable({
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
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 7, 8]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>

