<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcanc.aspx.cs" Inherits="SAES_v1.tcanc" %>

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

        function error_sel() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Debes seleccionar al menos un pago</h2>'
            })
        }


        function registro_duplicado() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Registro duplicado/h2>Favor de validar'
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



        function NoExiste() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Código Postal NO existe'
            })
        }

        function NoexisteAlumno() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Matrícula NO existe'
            })
        }


        //---- Matricula ----//
        function validarMatricula(idEl) {
            const idElemento = idEl;
            let nombre = document.getElementById(idElemento).value;
            if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                errorForm(idElemento, 'Ingresar Matrícula');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-external-link" aria-hidden="true"></i>
            &nbsp;Cancelar Conceptos de Cartera
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">
        <div class="card text-bg-light">
            <div class="card-header font-weight-bold">Datos del Alumno</div>
            <div class="card-body">
                <div class="form-row">
                    <div class="col text-center">
                        <asp:UpdateProgress ID="updPgrBusca" runat="server"
                            AssociatedUpdatePanelID="updPnlBusca">
                            <ProgressTemplate>
                                <asp:Image runat="server"
                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                    ImageUrl="~/Images/Sitemaster/loader.gif"
                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-sm-2">
                        <i class="fa fa-question-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Si no recuerdas la matricula del alumno, dar click en el icono de la lupa."></i>&nbsp;
                        Matrícula
                <asp:UpdatePanel ID="updPnlBusca" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_matricula" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="linkBttnBusca_Click"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4">
                        Alumno
                <asp:UpdatePanel ID="updPnlNombre" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4">
                        Programa
                <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hddnNivel" runat="server" />
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPrograma" runat="server" ErrorMessage="Favor de seleccionar un programa" ControlToValidate="ddl_programa" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-2">
                        Periodo
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" ErrorMessage="Favor de seleccionar periodo" ControlToValidate="ddl_periodo" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false">
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
                                        <asp:BoundField DataField="tpers_clave" HeaderText="Matrícula" />
                                        <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="tpers_paterno" HeaderText="Apellido Paterno" />
                                        <asp:BoundField DataField="tpers_materno" HeaderText="Apellido Materno" />
                                        <asp:BoundField DataField="tpers_cgenero" HeaderText="C_Genero">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpers_genero" HeaderText="Genero" />
                                        <asp:BoundField DataField="tpers_civil" HeaderText="C_Civil">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpers_ecivil" HeaderText="Estado Civil" />
                                        <asp:BoundField DataField="tpers_curp" HeaderText="CURP" />
                                        <asp:BoundField DataField="tpers_fecha" HeaderText="Fecha Nacimiento" />
                                        <asp:BoundField DataField="tpers_usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="tpers_fecha_reg" HeaderText="Fecha Registro" />
                                    </Columns>
                                    <SelectedRowStyle CssClass="selected_table" />
                                    <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBotones" runat="server"
                    AssociatedUpdatePanelID="updPnlBotones">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <asp:UpdatePanel ID="updPnlBotones" runat="server">
            <ContentTemplate>
                <div class="form-row" id="btn_tcanc" runat="server">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btn_cancel_update" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" Visible="False" OnClick="btn_cancel_update_Click" />
                        <asp:Button ID="btn_cancel_save" runat="server" CssClass="btn btn-round btn-secondary" OnClick="btn_cancel_save_Click" Text="Cancelar" />
                        <asp:Button ID="btn_cancelar_conc" runat="server" CssClass="btn btn-round btn-success" Text="Aplicar Cancelación" ValidationGroup="guardar" OnClick="btn_cancelar_conc_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGrid" runat="server"
                    AssociatedUpdatePanelID="updPnlGrid">
                    <ProgressTemplate>
                        <asp:Image runat="server"
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
                        <asp:GridView ID="Gridtcanc" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkValida" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="periodo" HeaderText="Periodo" />
                                <asp:BoundField DataField="consecutivo" HeaderText="Consecutivo Cartera" />
                                <asp:BoundField DataField="edcu_clave" HeaderText="Concepto Cargo" />
                                <asp:BoundField DataField="edcu_desc" HeaderText="Descripción Cargo" />
                                <asp:BoundField DataField="parcialidad" HeaderText="Parcialidad" />
                                <asp:BoundField DataField="importe" HeaderText="Importe" />
                                <asp:BoundField DataField="balance" HeaderText="Balance" />
                                <asp:BoundField DataField="vencimiento" HeaderText="Vencimiento" />
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
    <div class="modal fade" id="modalCancelarConceptos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col text-center">
                            <i class="fa fa-exclamation-circle fa-5x text-info" aria-hidden="true"></i>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-center">
                            <h3 class="font-weight-bold">¿Cancelar conceptos?</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="linkBttnSalir" runat="server" class="btn btn-secondary" data-dismiss="modal">No</asp:LinkButton>
                                    <asp:LinkButton ID="linkBttnActualizar" runat="server" class="btn btn-success" OnClick="linkBttnActualizar_Click">Si</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script>
        function load_datatable_alumnos() {
            $('#<%= GridAlumnos.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridAlumnos.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'SAES',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        }
                    },
                    {
                        title: 'Cat Alumnos',
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

        function load_datatable() {
            $('#<%= Gridtcanc.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtcanc.ClientID %>').find("tr:first"))).DataTable({
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
                "autoWidth": true,
                "dom": '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'SAES',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8]
                        }
                    },
                    {
                        title: 'Registo  Servicio  Social',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8]
                        }
                    }
                ],
                "stateSave": true
            });
        }
    </script>
</asp:Content>
