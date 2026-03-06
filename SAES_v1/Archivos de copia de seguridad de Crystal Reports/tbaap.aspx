<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tbaap.aspx.cs" Inherits="SAES_v1.tbaap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
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
    <script>

        //---- Clave zip ----//
        function error_clave() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Ya existe un registro de ese alumno, programa y periodo.</h2>'
            })
        }


        function error_fecha_baja() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">La fecha de baja debe ser menor o igual a la fecha actual.</h2>'
            })
        }

        function error_modifica() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Ya existe una estimación de devolución, por tanto no se puede modificar.</h2>'
            })
        }

        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error_busqueda() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Matricula incorrecta, favor de verificar.</h2>'

                //    html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
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

        function save_aplica() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">La baja fue aplicada exitosamente</h2>Favor de validar en el listado.'
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

        function carga_menu() {
            $("#operacion").addClass("active");
            $("#demograficos").addClass("current-page");
        }
        //-----  Función de agregar error  ------//
        function errorForm(idElementForm, textoAlerta) {
            let elemento = idElementForm;
            let textoInterno = textoAlerta;
            let elementoId = document.getElementById(elemento);
            elementoId.classList.add('error');
            elementoId.classList.remove('validado');
            elementoId.classList.remove('sinvalidar');
            errorId = 'error-' + String(elemento);
            let tieneError = document.getElementById(errorId);
            if (!tieneError) {
                const parrafo = document.createElement("p");
                const contParrafo = document.createTextNode(textoInterno);
                parrafo.appendChild(contParrafo);
                parrafo.classList.add('textoError');
                parrafo.id = 'error-' + String(elemento)
                //Depende de estructura de HTML
                elementoId.parentNode.appendChild(parrafo);
            }
        }

        //----- Función de remover error ------//
        function validadoForm(idElementForm) {
            let elemento = idElementForm;
            let elementoId = document.getElementById(elemento);
            elementoId.classList.remove('error');
            elementoId.classList.add('validado');
            elementoId.classList.remove('sinvalidar');
            errorId = 'error-' + String(elemento);
            let tieneError = document.getElementById(errorId);
            if (tieneError) {
                tieneError.remove();
            }
        }

        //---- Clave zip ----//


        //---- Nombre zip ----//
        function validarNombreZip(idEl) {
            const idElemento = idEl;
            let tamin = document.getElementById(idElemento).value;
            if (tamin == null || tamin.length == 0 || /^\s+$/.test(tamin)) {
                errorForm(idElemento, 'Por favor ingresa el nombre de la colonia o municipio');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida fomulario completo ----//
        function validar_campos_zip(e) {
            event.preventDefault(e);
            validar_pais_z('ContentPlaceHolder1_cbop_zip');
            validar_estado_z('ContentPlaceHolder1_cboe_zip');
            validar_delegacion_z('ContentPlaceHolder1_cbod_zip');
            validarclaveZip('ContentPlaceHolder1_c_zip', 0);
            validarNombreZip('ContentPlaceHolder1_n_zip');
            return false;
        }

        //$("#customSwitches2").on("change", function () {
        //    if (this.checked) {
        //        $("#ContentPlaceHolder1_checked_input2").val('S')

        //    } else {
        //        $("#ContentPlaceHolder1_checked_input2").val('N')
        //    }
        //    console.log($("#customSwitches2").val());
        //    console.log($("#ContentPlaceHolder1_checked_input2").val());
        //});


        //function calcular_devolucion(obj) {
        //    $("#customSwitches2").checked = true;
        //    alert(obj.val);
        //}

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updPnlContenedor" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="x_title">
                    <h2>
                        <i class="fa fa-user-times" aria-hidden="true"></i>&nbsp;Baja
                    </h2>
                    <div class="clearfix"></div>
                </div>
                <div class="form-row">
                    <div class="col-sm-2">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                Matricula
                 <div class="input-group">
                     <asp:TextBox runat="server" class="form-control" ID="txt_matricula" AutoPostBack="true"></asp:TextBox>
                     <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i>    </asp:LinkButton>
                 </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-5">
                        Alumno
                 <asp:UpdatePanel ID="updPnlCampus" runat="server">
                     <ContentTemplate>
                         <asp:TextBox runat="server" class="form-control" ID="txt_nombre" disabled="true"></asp:TextBox>
                         <asp:HiddenField ID="hddnIdAlumno" runat="server" />
                     </ContentTemplate>
                 </asp:UpdatePanel>
                    </div>

                    <div class="col-sm-5">
                        Programa
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged">
                            <asp:ListItem>-------</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                </div>
                <hr />
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
                <div class="form-row" id="containerGridAlumnos" runat="server" visible="false">
                    <div class="col">
                        <asp:UpdatePanel ID="updPnlGridAlumnos" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Gridtpers" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtpers_SelectedIndexChanged" EmptyDataText="No se encontraron datos." ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tpers_num" HeaderText="Id">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpers_clave" HeaderText="Matricula" />
                                        <asp:BoundField DataField="tpers_nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="tpers_paterno" HeaderText="Paterno" />
                                        <asp:BoundField DataField="tpers_materno" HeaderText="Materno" />
                                        <asp:BoundField DataField="tpers_cgenero" HeaderText="Genero" />
                                        <asp:BoundField DataField="tpers_curp" HeaderText="CURP" />
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
            <div class="container" id="containerDetalleBaja" runat="server" visible="false">
                <div class="row">
                    <div class="col">
                        <h4 class="font-weight-bold text-secondary">Datos Baja</h4>
                    </div>
                </div>
                <hr />
                <div class="form-row">
                    <div class="col-sm-6">
                        Periodo
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_periodo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqPeriodo" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar periodo" ControlToValidate="ddl_periodo" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-6">
                        Tipo Baja
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_tipo_baja" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_tipo_baja_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar tipo baja" ControlToValidate="ddl_tipo_baja" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-sm-2">
                        <label for="ContentPlaceHolder1_txt_fecha_b" class="form-label">Fecha Baja</label>
                        <asp:TextBox ID="txt_fecha_b" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ErrorMessage="Se requiere fecha de baja" ControlToValidate="txt_fecha_b" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-4">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <label for="ContentPlaceHolder1_customSwitches2" class="form-label font-weight-bold">¿Desea una estimación de devolución?</label>
                                <div class="custom-control custom-switch">
                                    <input type="checkbox" class="custom-control-input" id="customSwitches2" name="customSwitches2" onclick="calcular_devolucion()">
                                    <%--<asp:CheckBox ID="customSwitches2" runat="server" class="custom-control-input" AutoPostBack="true"   name="customSwitches2" OnCheckedChanged="customSwitches2_CheckedChanged"/>--%>
                                    <label class="custom-control-label" for="customSwitches2"></label>
                                    <asp:HiddenField ID="checked_input2" runat="server" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col text-center">
                        <asp:UpdatePanel ID="updPnlGuardar" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="linkBttnCancelar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                                <asp:LinkButton ID="linkBttnCancelarModificar" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelarModificar_Click">Cancelar</asp:LinkButton>
                                <asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-round btn-success" OnClick="linkBttnGuardar_Click" ValidationGroup="guardar">Agregar</asp:LinkButton>
                                <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" Visible="false" OnClick="linkBttnModificar_Click" ValidationGroup="valDatos">Actualizar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col text-center">
                        <asp:UpdateProgress ID="updPgrGrid" runat="server"
                            AssociatedUpdatePanelID="updPnlGrid">
                            <ProgressTemplate>
                                <asp:Image ID="img2" runat="server"
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
                                <asp:GridView ID="Gridtbaap" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtbaap_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tbaap_tprog_clave" HeaderText="Programa" />
                                        <asp:BoundField DataField="tbaap_tpees_clave" HeaderText="Periodo" />
                                        <asp:BoundField DataField="ttiba_desc" HeaderText="Tipo Baja" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tbaap_fecha_baja" HeaderText="Fecha Baja" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tbaap_estima" HeaderText="Estimación Devolución">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tbaap_ttiba_clave" HeaderText="Id">
                                            <HeaderStyle CssClass="ocultar" />
                                            <ItemStyle CssClass="ocultar" />
                                        </asp:BoundField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="modalGenEstimacion" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Calcula estimación de devolución</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Desea realizar el cálculo de estimación?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary">Si</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
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

        function ctrl_fecha_b() {
            $('#ContentPlaceHolder1_txt_fecha_b').datepicker({
                uiLibrary: 'bootstrap4',
                locale: 'es-es',
                format: "dd/mm/yyyy"
            });
        }

        function calcular_devolucion() {
            if ($("#customSwitches2").is(':checked')) {
                //alert("Checkbox Is checked");
                $("#ContentPlaceHolder1_checked_input2").val('S')

            }
            else {
                //alert("Checkbox Is not checked");
                $("#ContentPlaceHolder1_checked_input2").val('N')

            }
        }

        function habilitar_calcular_devolucion(habilita) {
            if (habilita === "S") {
                $("#customSwitches2").attr('checked', true);
                //alert("true");
            }
            else {
                $("#customSwitches2").attr('checked', false);
                //alert("false");

            }
        }

    </script>

</asp:Content>
