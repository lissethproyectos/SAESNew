<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoAdministracion.aspx.cs" Inherits="SAES_v1.Repositorio.ListadoAdministracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Template/Sitemaster/vendors/jquery/dist/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.js" integrity="sha512-n/4gHW3atM3QqRcbCn6ewmpxcLAHGaDjpEBu4xZd47N0W2oQ+6q7oc3PXstrJYXcbNU1OHdQ1T7pAP+gi5Yu8g==" crossorigin="anonymous"></script>
    <style>
        .Oculto {
            display: none;
        }

        #ContentPlaceHolder1_gvAlumnos_info {
            float: left;
        }

        #ContentPlaceHolder1_gvAlumnos tbody tr {
            cursor: pointer;
        }

        #ContentPlaceHolder1_gvAlumnos_length {
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
    <%-- <div class="x_title">
        <h2>
            <img src="../Images/Repositorio/negocios-y-finanzas.png" style="width: 45px;" /><small>Expedientes de alumnos</small></h2>
        <div class="clearfix"></div>
    </div>--%>
    <div class="x_title">
        <h2>
            <i class="fa fa-folder-open" aria-hidden="true"></i>&nbsp;Expedientes de alumnos</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:TextBox ID="TextBox1" runat="server" CssClass="Oculto"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="Oculto"></asp:TextBox>

        <div class="form-row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrGridAlumnos" runat="server"
                    AssociatedUpdatePanelID="updPnlGridAlumnos">
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
            <div class="col text-right">
                <asp:CheckBox ID="chkSoloCompletos" runat="server"
                    Text="Mostrar solo expedientes Completos"
                    OnCheckedChanged="chkSoloCompletos_CheckedChanged" AutoPostBack="true" />
            </div>
        </div>
        <div class="form-row">
            <div class="col text-center">
                <asp:UpdatePanel runat="server" ID="updPnlGridAlumnos">
                    <ContentTemplate>
                        <asp:GridView ID="gvAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" OnRowCommand="gvAlumnos_RowCommand" ShowHeaderWhenEmpty="True" DataKeyNames="IDAlumno">
                            <Columns>
                                <asp:BoundField DataField="IDAlumno" HeaderText="Matrícula" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Campus" HeaderText="Campus" />
                                <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                                <asp:BoundField DataField="Programa" HeaderText="Programa" />
                                <asp:BoundField DataField="Estatus_Alumno" HeaderText="Estatus Alumno" />
                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                <asp:BoundField DataField="FechaUltNotificacion" HeaderText="Fecha Notificación" ItemStyle-CssClass="Oculto" HeaderStyle-CssClass="Oculto">
                                    <HeaderStyle CssClass="Oculto" />
                                    <ItemStyle CssClass="Oculto" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NoAlerta" HeaderText="No. Alerta" ItemStyle-CssClass="Oculto" HeaderStyle-CssClass="Oculto">
                                    <HeaderStyle CssClass="Oculto" />
                                    <ItemStyle CssClass="Oculto" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Modalidad" HeaderText="Modalidad" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel2" runat="server" CausesValidation="False" CommandName="Expediente" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
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
        function load_datatable() {
            $('#<%= gvAlumnos.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvAlumnos.ClientID %>').find("tr:first"))).DataTable({
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
                        title: 'SAES',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6]
                        }
                    },
                    {
                        title: 'Cat Alumnos',
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
                stateSave: true
            });
        }
    </script>
</asp:Content>
