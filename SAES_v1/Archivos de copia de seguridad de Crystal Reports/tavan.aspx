<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tavan.aspx.cs" Inherits="SAES_v1.tavan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function NoexisteAlumno() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Matrícula NO existe'
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="x_title">
        <h2>
<i class="fa fa-line-chart" aria-hidden="true"></i>
            &nbsp;Avance Académico
        </h2>
        <div class="clearfix"></div>
    </div>
        <div class="container">
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrContenedor" runat="server"
                    AssociatedUpdatePanelID="updPnlContenedor">
                    <progresstemplate>
                        <asp:Image ID="img10" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </progresstemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <asp:UpdatePanel ID="updPnlContenedor" runat="server">
            <contenttemplate>
                <div class="form-row">
                    <div class="col-sm-3">                     
                                Matricula
                 <div class="input-group">
                     <asp:TextBox runat="server" class="form-control" ID="txt_matricula" ></asp:TextBox>
                     <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                 </div>
                    </div>
                    <div class="col-sm-9">
                        Alumno
                 <asp:UpdatePanel ID="updPnlCampus" runat="server">
                     <contenttemplate>
                         <asp:TextBox runat="server" class="form-control disabled" ID="txt_nombre" ReadOnly="true"></asp:TextBox>
                         <asp:HiddenField ID="hddnIdAlumno" runat="server" />
                     </contenttemplate>
                 </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col">
                        Programa
                <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                    <contenttemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>
                    </contenttemplate>
                </asp:UpdatePanel>
                    </div>
                </div>
            </contenttemplate>
             <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_programa" EventName="SelectedIndexChanged" />
                            </Triggers>
        </asp:UpdatePanel>
         <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrPrograma" runat="server"
                    AssociatedUpdatePanelID="updPnlPrograma">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <hr />
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrAlumnos" runat="server"
                    AssociatedUpdatePanelID="updPnlAlumnos">
                    <ProgressTemplate>
                        <asp:Image runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <asp:UpdatePanel ID="updPnlAlumnos" runat="server">
            <contenttemplate>
                <div class="form-row" id="containerGridAlumnos" runat="server">
                    <div class="col">
                        <asp:GridView ID="Gridtpers" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtpers_SelectedIndexChanged">
                            <columns>
                                <asp:TemplateField>
                                    <itemtemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tpers_num" HeaderText="Id">
                                    <headerstyle cssclass="ocultar" />
                                    <itemstyle cssclass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tpers_clave" HeaderText="Matricula" />
                                <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tpers_paterno" HeaderText="Paterno" />
                                <asp:BoundField DataField="tpers_materno" HeaderText="Materno" />
                                <asp:BoundField DataField="tpers_cgenero" HeaderText="Genero" />
                                <asp:BoundField DataField="tpers_curp" HeaderText="CURP" />
                            </columns>
                            <rowstyle font-size="Small" />
                            <selectedrowstyle cssclass="selected_table" />
                            <headerstyle backcolor="#2a3f54" forecolor="white" />
                        </asp:GridView>

                    </div>
                </div>
            </contenttemplate>
        </asp:UpdatePanel>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlCancelar" runat="server">
                    <contenttemplate>
                        <asp:LinkButton ID="linkBttnCancelar" visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
                <div class="form-row" id="containerHistorialAcad" runat="server">
                    <div class="col">
                        <iframe id="miniContenedor" frameborder="0" marginheight="0" marginwidth="0"
                            name="miniContenedor" style="height: 500px" width="100%"></iframe>
                    </div>
                </div>
            </contenttemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function load_datatable_alumnos() {
            $('#<%= Gridtpers.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridtpers.ClientID %>').find("tr:first"))).DataTable({
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
    </script>
</asp:Content>
