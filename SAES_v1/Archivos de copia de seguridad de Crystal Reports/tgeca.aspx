<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tgeca.aspx.cs" Inherits="SAES_v1.tgeca" %>

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

        function valida_cuotas() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Ya existen cuotas para ese periodo, favor de verificar.</h2>'
            })
        }

        function FiltPeriodos() {
            $('#<%= ddl_periodo_origen.ClientID %>').select2();
        }

        function generar() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se generó el estado de cuenta exitosamente</h2>Favor de validar en el listado.'
            })
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
         <div class="row">
            <div class="col">
                <div class="x_title">
                    <h2>
                        <i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Generación Estado de Cuenta
                    </h2>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPrTipo" runat="server"
                    AssociatedUpdatePanelID="updPnlTipo">
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
            <div class="col-sm-3">
                Tipo Opción
                <asp:UpdatePanel ID="updPnlTipo" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_tipo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_tipo_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="I" Selected="True">Individual</asp:ListItem>
                            <asp:ListItem Value="M">Masivo</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <hr />
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="upPgrInd" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel8">
                    <ProgressTemplate>
                        <asp:Image ID="img9" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrMasivo" runat="server"
                    AssociatedUpdatePanelID="updPnlMasivo">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div id="rowIndividual" runat="server">
                    <div class="form-row">
                        <div class="col-sm-3">
                            Matrícula
                <asp:UpdatePanel ID="updPnlBusca" runat="server">
                    <ContentTemplate>
                        <div class="input-group">
                            <asp:TextBox ID="txt_matricula" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-9">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    Nombre(s)
                        <asp:TextBox ID="txt_nombre" MaxLength="10" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    
                    <div class="form-row">  
                        <div class="col-sm-9">
                Programa
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_programa_ind" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem>-------</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                        <div class="col-sm-3">
                            Periodo Destino
                 <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddl_periodo_destino_ind" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_periodo_destino_ind_SelectedIndexChanged">
                             <asp:ListItem>-------</asp:ListItem>
                         </asp:DropDownList>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updPnlIndividual" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="updPnlMasivo" runat="server">
            <ContentTemplate>
                <div id="rowMasivo" runat="server" visible="false">
                    <div class="form-row">
                        <div class="col-sm-6">
                            Periodo Origen
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_periodo_origen" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_periodo_destino_SelectedIndexChanged">
                            <asp:ListItem>-------</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-6">
                            Periodo Destino
                 <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddl_periodo_destino" CssClass="form-control" runat="server" AutoPostBack="True">
                             <asp:ListItem>-------</asp:ListItem>
                         </asp:DropDownList>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                        </div>
                    </div>
                    <hr />
                    <div class="form-row">
                        <div class="col-sm-6">
                            Campus
                            <asp:DropDownList ID="ddl_campus" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged">
                                <asp:ListItem>-------</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-6">
                            Nivel
                            <asp:DropDownList ID="ddl_nivel" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_nivel_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem>-------</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-row">
            <div class="col-sm-9">
                Programa
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged">
                            <asp:ListItem>-------</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
                    <div class="col-sm-3">
                        <asp:UpdatePanel ID="updPnlTasa" runat="server">
                <ContentTemplate>
                        Tasa              
                        <asp:DropDownList ID="ddl_tasa" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_tasa_SelectedIndexChanged">
                            <asp:ListItem>-------</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
            </asp:UpdatePanel>
                    </div>
                
        </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        
        
        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged" Visible="false" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tpers_clave" HeaderText="Matrícula" />
                                <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tpers_paterno" HeaderText="Apellido Paterno" />
                                <asp:BoundField DataField="tpers_materno" HeaderText="Apellido Materno" />
                                <asp:BoundField DataField="tpers_num" HeaderText="pidm">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
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

        <hr />
                <asp:UpdatePanel ID="updPnlGuardar" runat="server">
                    <ContentTemplate>
        <div class="form-row" id="btn_tgeca" runat="server">
            <div class="col text-center">
        
                        <asp:LinkButton ID="linkBttnCancelar" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="linkBttnGenerar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGenerar_Click" ValidationGroup="guardar">Generar Cuotas</asp:LinkButton>
                 
            </div>
        </div>
               </ContentTemplate>
                </asp:UpdatePanel>

        <div class="form-row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridAlumnosCuotas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron cuotas para este periodo.">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="matricula" HeaderText="Matrícula" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />                                
                                <asp:BoundField DataField="campus" HeaderText="Campus" />
                                <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                                <asp:BoundField DataField="programa" HeaderText="Programa" />
                                <asp:BoundField DataField="tasa" HeaderText="Tasa" />
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                            title: 'SAES',
                            className: 'btn-dark',
                            extend: 'pdfHtml5',
                            text: 'Exportar PDF',
                            orientation: 'landscape',
                            pageSize: 'LEGAL',
                            exportOptions: {
                                columns: [0, 1, 2, 3]
                            }
                        }
                    ],
                    "stateSave": true
                });
            }
            function load_datatable_cuotas_periodo() {
                $('#<%= GridAlumnosCuotas.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridAlumnosCuotas.ClientID %>').find("tr:first"))).DataTable({
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
                                columns: [1, 2, 3, 4, 5, 6]
                            }
                        },
                        {
                            title: 'Cuotas generadas',
                            className: 'btn-dark',
                            extend: 'pdfHtml5',
                            text: 'Exportar PDF',
                            orientation: 'landscape',
                            pageSize: 'LEGAL',
                            exportOptions: {
                                columns: [0, 1, 2, 3]
                            }
                        }
                    ],
                    "stateSave": true
                });
            }

        </script>
   
</asp:Content>
