<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="thiac.aspx.cs" Inherits="SAES_v1.thiac" %>

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

        function ver_reporte(matricula, programa, idAlumno) {
                         
            $('#precarga1').html('<div class="loading"><img src="Images/Sitemaster/loader.gif" alt="loading"  height="50px" width="50px"/><br/>Un momento, por favor...</div>');


            window.open();
            window.write("../Reports/VisualizadorCrystal.aspx?Tipo=RepHistAcad&Valor1=" + matricula + "&Valor2=" + programa + "&Valor3=" + idAlumno + "&enExcel=N", "miniContenedor", "toolbar=no", "location=no", "menubar=no")
            window.close()

            $('#precarga1').html('');

            return false;

        }


        function loader_false() {
            $('#precarga1').html('');
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <div class="col-sm-2">                     
                                Matricula
                 <div class="input-group">
                     <asp:TextBox runat="server" class="form-control" ID="txt_matricula" AutoPostBack="True" OnTextChanged="linkBttnBusca_Click"></asp:TextBox>
                     <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                 </div>
                    </div>
                    <div class="col-sm-5">
                        Alumno
                 <asp:UpdatePanel ID="updPnlCampus" runat="server">
                     <contenttemplate>
                         <asp:TextBox runat="server" class="form-control disabled" ID="txt_nombre" disabled></asp:TextBox>
                         <asp:HiddenField ID="hddnIdAlumno" runat="server" />
                     </contenttemplate>
                 </asp:UpdatePanel>
                    </div>
               
                    <div class="col-sm-5">
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

         <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrRep" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel1">
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
            <div class="col text-center">
                        <div id="precarga1" style="width: 100%">
                            <div class="loading"><img src="Images/Sitemaster/loader.gif" alt="loading"  height="50px" width="50px"/><br/>Un momento, por favor...</div>
                        </div>
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
    </script>
</asp:Content>
