<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ttira.aspx.cs" Inherits="SAES_v1.ttira" %>

<%@ Register Assembly="SAES_Services" Namespace="SAES_Services" TagPrefix="customControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.22.2/moment.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/tempusdominus-bootstrap-4/5.0.1/js/tempusdominus-bootstrap-4.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/tempusdominus-bootstrap-4/5.0.1/css/tempusdominus-bootstrap-4.min.css" />

    <div class="container">
        <script>
            function error_consulta() {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'error',
                    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
                })
            }

            function error_sin_materias() {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'error',
                    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Se debe seleccionar al menos una materia.</h2>'
                })
            }

            function error_consulta_programas() {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'error',
                    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- No se encontraron programas para este alumno.</h2>'
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

            function error_estatus() {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'error',
                    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- La materia debe estar en estatus INSCRITO y con grupo asignado, para poder agregar horarios.</h2>'
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

            function error_hora() {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'error',
                    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Se encontro un traslape de materias</h2>'
                })
            }


            function error_tot_materias(total_materias) {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'error',
                    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Se supero el total de materias permitidas</h2><br>Materias permitidas ' + total_materias
                })
            }


            function error_grupo() {
                swal({
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    type: 'error',
                    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- El grupo que intenta agregar no corresponde a los que ya estan asignados</h2>'
                })
            }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-file-text" aria-hidden="true"></i>
            &nbsp;Carga de Materias
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid">

        <%-- <div class="card text-bg-light">
            <div class="card-header font-weight-bold">Datos del Alumno</div>
            <div class="card-body">--%>
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
                            <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" aria-controls="collapseAlumnos"
                                OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Alumno
                <asp:UpdatePanel ID="updPnlNombre" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_nombre" MaxLength="60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Programa
                <asp:UpdatePanel ID="updPnlPrograma" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hddnNivel" runat="server" />
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPrograma" runat="server" ErrorMessage="Favor de seleccionar un programa" ControlToValidate="ddl_programa" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridAlumnos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridAlumnos_SelectedIndexChanged">
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

        <div class="form-row">
            <div class="col-sm-4">
                Campus
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList runat="server" ID="ddl_campus" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Favor de seleccionar campus" ControlToValidate="ddl_campus" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
            </div>
            <div class="col-sm-3">
                Nivel
             <asp:UpdatePanel ID="updPnlNivel" runat="server">
                 <ContentTemplate>
                     <asp:DropDownList runat="server" ID="ddl_nivel" Enabled="false" CssClass="form-control"></asp:DropDownList>
                 </ContentTemplate>
             </asp:UpdatePanel>
            </div>
            <div class="col-sm-5">
                Periodo
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" ErrorMessage="Favor de seleccionar periodo" ControlToValidate="ddl_periodo" ValidationGroup="guardar" CssClass="text-danger"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
            </div>


        </div>

        <br />


        <div class="card text-bg-light">
            <div class="card-header">
                <div class="form-row">
                    <div class="col-sm-9">
                        <h4 class=" font-weight-bold"><i class="fa fa-file" aria-hidden="true"></i>&nbsp;Materia</h4>
                    </div>
                    <div class="col-sm-3 text-right">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="linkBttnAgregarMat" CssClass="btn btn-secondary" runat="server" OnClick="linkBttnAgregarMat_Click"><i class="fa fa-plus" aria-hidden="true"></i>Agregar Materia</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="form-row">
                    <div class="col-sm-12 text-center">
                        <!-- Button trigger modal -->



                    </div>
                </div>
                <asp:UpdatePanel ID="updPnlGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridttira" runat="server" CssClass="table table-striped table-bordered" Width="100%"
                            DataKeyNames="clave" AutoGenerateColumns="False" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos." OnRowCancelingEdit="Gridttira_RowCancelingEdit" OnRowEditing="Gridttira_RowEditing" OnRowUpdating="Gridttira_RowUpdating" OnRowDataBound="Gridttira_RowDataBound" OnSelectedIndexChanged="Gridttira_SelectedIndexChanged">
                            <Columns>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="clave" HeaderText="Clave" ReadOnly="True" />
                                <asp:BoundField DataField="materia" HeaderText="Materia" ReadOnly="True" />
                                <asp:BoundField DataField="grupo" HeaderText="Grupo" ReadOnly="True" />
                                <asp:TemplateField HeaderText="Estatus">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_estatus_edit" runat="server" CssClass="form-control" SelectedValue='<%# Bind("estatus") %>'>
                                            <asp:ListItem Value="0">-------</asp:ListItem>
                                            <asp:ListItem Value="IN">Inscrito</asp:ListItem>
                                            <asp:ListItem Value="PR">Propuesta</asp:ListItem>
                                            <asp:ListItem Value="BA">Baja</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="linkBttnAsigTodas" CssClass="btn btn-success" runat="server" OnClick="linkBttnAsigTodas_Click">Asignar Todas</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_estatus" runat="server" CssClass='<%# Bind("estilo") %>' SelectedValue='<%# Bind("estatus") %>' Enabled="false">
                                            <asp:ListItem Value="0">-------</asp:ListItem>
                                            <asp:ListItem Value="IN">Inscrito</asp:ListItem>
                                            <asp:ListItem Value="PR">Propuesta</asp:ListItem>
                                            <asp:ListItem Value="BA">Baja</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-secondary"  CommandName="Edit" Text="Editar"></asp:LinkButton>--%>
                                        <asp:LinkButton ID="linkBttnEliminar" runat="server" CssClass='<%# Bind("visible") %>' OnClick="linkBttnEliminar_Click">Eliminar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttnEditar" runat="server" CausesValidation="False" CssClass="btn btn-secondary" Text="Asignar" OnClick="linkBttnEditar_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="estatus">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tplan_ttima_clave" />
                            </Columns>
                            <RowStyle Font-Size="Small" />
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <div class="card text-bg-light">
            <div class="card-header">
                <h4 class=" font-weight-bold"><i class="fa fa-clock" aria-hidden="true"></i>&nbsp;Horarios</h4>
            </div>
            <div class="card-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="Gridthora" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
                            <Columns>
                                <asp:BoundField DataField="clave" HeaderText="Clave" />
                                <asp:BoundField DataField="materia" HeaderText="Materia" />
                                <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                <asp:BoundField DataField="lunes" HeaderText="Lunes" />
                                <asp:BoundField DataField="martes" HeaderText="Martes" />
                                <asp:BoundField DataField="miercoles" HeaderText="Miercoles" />
                                <asp:BoundField DataField="jueves" HeaderText="Jueves" />
                                <asp:BoundField DataField="viernes" HeaderText="Viernes" />
                                <asp:BoundField DataField="sabado" HeaderText="Sabado" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade bd-example-modal-lg" id="modalMaterias" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Materias Disponibles</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row">
                            <div class="col-sm-12">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="Gridttira_Disp" runat="server" CssClass="table table-striped table-bordered"
                                            Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron datos.">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMatDisp" CssClass="form-control" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="tgrup_tmate_clave" HeaderText="Clave" />
                                                <asp:BoundField DataField="tmate_desc" HeaderText="Materia" />
                                                <asp:BoundField DataField="tmate_desc" HeaderText="Materia" />
                                                <asp:BoundField DataField="tplan_ttima_clave" HeaderText="tplan_ttima_clave" />

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                <asp:Button ID="bttnAgregarMatAlum" class="btn btn-primary" runat="server" Text="Agregar" OnClick="bttnAgregarMatAlum_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal Confirma Inscrito -->
        <div class="modal fade bd-example-modal-lg" id="modalConfirma" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalConfirmaLabel">Informacion</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row">
                            <div class="col-sm-12">
                                <h5>¿Asignar las materias <strong>propuestas</strong>?</h5>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                <asp:LinkButton ID="linkBttnAsignar" CssClass="btn btn-success" runat="server" OnClick="linkBttnAsignar_Click">Confirmar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Horario -->
        <div class="modal" id="modalHorario" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Horario</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <div class="container">
                                    <div class="form-row">
                                        <div class="col-sm-6">
                                            Dia
                                    <asp:DropDownList ID="ddl_dia" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">--Ninguno--</asp:ListItem>
                                        <asp:ListItem Value="1">Lunes</asp:ListItem>
                                        <asp:ListItem Value="2">Martes</asp:ListItem>
                                        <asp:ListItem Value="3">Miercoles</asp:ListItem>
                                        <asp:ListItem Value="4">Jueves</asp:ListItem>
                                        <asp:ListItem Value="5">Viernes</asp:ListItem>
                                        <asp:ListItem Value="6">Sabado</asp:ListItem>
                                    </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqDia" CssClass="text-danger" runat="server" ErrorMessage="*Requerido" ValidationGroup="guardar_hora" ControlToValidate="ddl_dia">Seleccionar dia</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-sm-6">
                                            Hora Inicial
                                    <div class="input-group">
                                        <div class="input-group-text"><i class="fa fa-clock-o"></i></div>
                                        <asp:DropDownList ID="ddl_hora_inicial" runat="server" CssClass="form-control"></asp:DropDownList>

                                    </div>
                                        </div>
                                        <div class="col-sm-6">
                                            Hora Final
                                    <div class="input-group">
                                        <div class="input-group-text"><i class="fa fa-clock-o"></i></div>
                                        <asp:DropDownList ID="ddl_hora_final" runat="server" CssClass="form-control"></asp:DropDownList>

                                    </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                <asp:LinkButton ID="linkBttnGuardarHora" CssClass="btn btn-success" runat="server" ValidationGroup="guardar_hora" OnClick="linkBttnGuardarHora_Click">Guardar</asp:LinkButton>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Grupos -->
        <div class="modal fade bd-example-modal-lg" id="modalGrupos" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Horario por Grupo</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <div class="form-row">
                                    <div class="col-sm-6">
                                        Grupo
                 <customControl:GroupDropDownList ID="ddl_grupo2" runat="server" CssClass="form-control"
                     OnSelectedIndexChanged="ddl_grupo2_SelectedIndexChanged" AutoPostBack="true">
                 </customControl:GroupDropDownList>
                                    </div>

                                    <div class="col-sm-6" runat="server" visible="false">
                                        Estatus
                   <asp:DropDownList ID="ddl_estatus_editar" runat="server" CssClass="form-control" SelectedValue='<%# Bind("estatus") %>'>
                       <asp:ListItem Value="0">--------</asp:ListItem>
                       <asp:ListItem Value="IN">Inscrito</asp:ListItem>
                       <asp:ListItem Value="PR">Propuesta</asp:ListItem>
                       <asp:ListItem Value="BA">Baja</asp:ListItem>
                   </asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="form-row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="GridHorarioGrupo" runat="server" CssClass="table table-striped table-bordered"
                                            Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron horarios para este grupo.">
                                            <Columns>
                                                <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                                <asp:BoundField DataField="dia" HeaderText="Dia" />
                                                <asp:BoundField DataField="horario" HeaderText="Horario" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                <asp:LinkButton ID="linBttnGuardarGrupoMat" CssClass="btn btn-success" runat="server" OnClick="linBttnGuardarGrupoMat_Click">Asignar Horario(s)</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div id="modalEliminar" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Esta seguro de eliminar esta materia?</p>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="linkBttnDeletemat" CssClass="btn btn-success" runat="server" OnClick="linkBttnDeletemat_Click">Eliminar</asp:LinkButton>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(function () {
            $('#datetimepicker3').datetimepicker({
                format: 'LT'
            });
        });

        $(function () {
            $('#horaFinal').datetimepicker({
                format: 'LT'
            });
        });

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

        function load_datatable_materias_disp() {
            $('#<%= Gridttira_Disp.ClientID %>').prepend($("<thead></thead>").append($('#<%= Gridttira_Disp.ClientID %>').find("tr:first"))).DataTable({
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
                "paging": false,
                "scrollResize": true,
                "scrollY": '500px',
                "scrollCollapse": true,
                "autoWidth": true,
                "stateSave": true
            });
        }
        $('#collapseAlumnos').on('shown.bs.collapse', function (e) {
            $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
        });

        $('#modalMaterias').on('shown.bs.modal', function (e) {
            $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
        });

    </script>
</asp:Content>
