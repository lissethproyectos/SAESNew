<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tprog2.aspx.cs" Inherits="SAES_v1.tprog2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="x_title">
        <h2>
            <i class="fa fa-graduation-cap" aria-hidden="true"></i>&nbsp;Programas Académicos
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tprog" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tprog" runat="server">
                    <div class="form-row">
                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_txt_tprog" class="form-label">Clave</label>
                            <asp:TextBox ID="txt_tprog" MaxLength="10" runat="server" CssClass="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Descripción</label>
                            <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div id="Div1" runat="server">
                    <div class="form-row">
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_ddl_tnive" class="form-label">Nivel Estudios</label>
                            <asp:DropDownList ID="ddl_tnive" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_ddl_tcole" class="form-label">Colegio</label>
                            <asp:DropDownList ID="ddl_tcole" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_ddl_tmoda" class="form-label">Modalidad</label>
                            <asp:DropDownList ID="ddl_tmoda" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                 <div id="Div2" runat="server">
                    <div class="form-row">
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_txt_creditos" class="form-label">No.Créditos</label>
                            <asp:TextBox ID="txt_creditos" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_txt_cursos" class="form-label">No.cursos</label>
                            <asp:TextBox ID="txt_cursos" MaxLength="15" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_txt_periodos" class="form-label">No.Periodos</label>
                            <asp:TextBox ID="txt_periodos" MaxLength="15" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="Div3" runat="server">
                    <div class="row">
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_txt_clave_inc" class="form-label">Clave Incorporante</label>
                            <asp:TextBox ID="txt_clave_inc" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_txt_rvoe" class="form-label">Clave RVOE</label>
                            <asp:TextBox ID="txt_rvoe" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_txt_fecha_rvoe" class="form-label">Fecha RVOE</label>
                            <asp:TextBox ID="txt_fecha_rvoe" runat="server" class="form-control" ></asp:TextBox>
                            <script>
                                function ctrl_fecha_rvoe() {
                                    
                                    $('#ContentPlaceHolder1_txt_fecha_rvoe').datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: "dd/mm/yyyy"
                                    });
                                }
                            </script>
                        </div>
                    </div>
                </div>
                <div class="form-row" id="btn_tprog" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />
                    </div>
                </div>
                <div id="table_tprog">
                    <asp:GridView ID="Gridtprog" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtprog_SelectedIndexChanged">
                        <Columns>
<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" />
                            <asp:BoundField DataField="C_ESTATUS" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                            <asp:BoundField DataField="Nivel" HeaderText="Nivel">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Colegio" HeaderText="Colegio">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Modalidad" HeaderText="Modalidad">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="creditos" HeaderText="creditos">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cursos" HeaderText="cursos">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="periodos" HeaderText="periodos">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="incorporante" HeaderText="Incorporante">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rvoe" HeaderText="rvoe">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_rvoe" HeaderText="fecha_rvoe">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
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
         let table_periodo = $("#ContentPlaceHolder1_Gridtprog").DataTable({
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
                     title: 'SAES_Catálogo Programas Académicos',
                     className: 'btn-dark',
                     extend: 'excel',
                     text: 'Exportar Excel',
                     exportOptions: {
                         columns: [1, 2, 4, 5,6,7,8,9,10,11,12,13,14]
                     }
                 },
                 {
                     title: 'SAES_Catálogo Programas Académicos',
                     className: 'btn-dark',
                     extend: 'pdfHtml5',
                     text: 'Exportar PDF',
                     orientation: 'landscape',
                     pageSize: 'LEGAL',
                     exportOptions: {
                         columns: [1, 2, 4, 5,6,7,8,9,10,11,12,13,14]
                     }
                 }
             ],
             stateSave: true
         });
     }

     function destroy_table() {
         $("#ContentPlaceHolder1_Gridttprog").DataTable().destroy();
     }
     function remove_class() {
         $('.selected_table').removeClass("selected_table")
     }
 </script>
</asp:Content>
