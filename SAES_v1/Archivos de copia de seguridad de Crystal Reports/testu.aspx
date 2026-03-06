<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="testu.aspx.cs" Inherits="SAES_v1.testu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <style>
        span button {
            margin-bottom: 0px !important;
        }
    </style>
    <script>
        function error_insertar() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">Ya existe un registro de ese alumno, programa y periodo.</h2>'
            })
        }

        function error_editar() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">El registro no se puede editar por que el periodo no esta vigente.</h2>'
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

        $("#customSwitches2").on("change", function () {
            if (this.checked) {
                $("#ContentPlaceHolder1_checked_input2").val('S')

            } else {
                $("#ContentPlaceHolder1_checked_input2").val('N')
            }
            console.log($("#customSwitches2").val());
            console.log($("#ContentPlaceHolder1_checked_input2").val());
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="x_title">
            <h2>
                <i class="fa fa-address-book-o" aria-hidden="true"></i>&nbsp;Registro Estudiantil
                </h2>
        <div class="clearfix"></div>
        </div>        
                <div class="form-row">
                    <div class="col-sm-3">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                Matricula
                 <div class="input-group">
                     <asp:TextBox runat="server" class="form-control" ID="txt_matricula"></asp:TextBox>
                     <asp:LinkButton ID="linkBttnBusca" class="btn btn-success" runat="server" OnClick="linkBttnBusca_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                 </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-9">
                        Alumno
                 <asp:UpdatePanel ID="updPnlCampus" runat="server">
                     <ContentTemplate>
                         <asp:TextBox runat="server" class="form-control" ID="txt_nombre" disabled="true"></asp:TextBox>
                         <asp:HiddenField ID="hddnIdAlumno" runat="server" />
                     </ContentTemplate>
                 </asp:UpdatePanel>
                    </div>
                </div>
        <div class="form-row">
                    <div class="col text-center">
                                            <asp:LinkButton ID="linkBttnCancelarGridTpers" Visible="false" class="btn btn-round btn-secondary" runat="server" OnClick="linkBttnCancelar_Click">Cancelar</asp:LinkButton>
                        </div>
                        </div>
                <%--<div class="form-row">
                    <div class="col">
                        Programa
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddl_programa" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    </div>
                </div>--%>
                <hr />
                <asp:UpdatePanel ID="updPnlAlumnos" runat="server">
                    <ContentTemplate>
                        <div class="form-row" id="containerGridAlumnos" runat="server">
                            <div class="col">
                                <asp:GridView ID="Gridtpers" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridtpers_SelectedIndexChanged">
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
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
          <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                <div class="container" id="containerDetalle" runat="server" visible="false">
                    <h4 class="font-weight-bold">Asignar Programa</h4>
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
                            Campus
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="ddl_campus" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar campus" ControlToValidate="ddl_campus" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-8">
                            Programa
                         <asp:UpdatePanel ID="updPnlNivel" runat="server">
                             <ContentTemplate>
                                 <asp:DropDownList runat="server" ID="ddl_programa_new" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar programa" ControlToValidate="ddl_programa_new" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                             </ContentTemplate>
                         </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            Tasa
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_tasa" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar tasa" ControlToValidate="ddl_tasa" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-4">
                            Turno
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_turno" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar turno" ControlToValidate="ddl_turno" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            Generación
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_generacion" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-danger" ErrorMessage="Debes asignar la generacion" ControlToValidate="txt_generacion" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valGeneracion" runat="server" ControlToValidate="txt_generacion" CssClass="text-danger" ErrorMessage="El # de generación debe ser numérico" SetFocusOnError="True" ValidationExpression="^(-?\d{0,13}\.\d{0,2}|\d{0,13})$" ValidationGroup="guardar">Solo números</asp:RegularExpressionValidator>

                    </ContentTemplate>
                </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            Especialidad
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_especialidad" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-4">
                            Estatus
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-danger" ErrorMessage="Debes seleccionar estatus" ControlToValidate="ddl_estatus" InitialValue="" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_customSwitches2" class="form-label font-weight-bold">¿Desea realizar el cálculo de las cuotas?</label>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <div class="custom-control custom-switch">
                                       <%-- <asp:RadioButtonList ID="customSwitches2" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                            <asp:ListItem Value="S">Si</asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                        <input type="checkbox" class="custom-control-input" id="customSwitches2" name="customSwitches2" 
                                            onclick="calcular_cuotas()">
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
                                    <asp:LinkButton ID="linkBttnModificar" runat="server" CssClass="btn btn-round btn-success" Visible="false" ValidationGroup="guardar" OnClick="linkBttnModificar_Click">Actualizar</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridTestu" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridTestu_SelectedIndexChanged" EmptyDataText="No se encontraron programas." ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkBttSel2" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="testu_tpees_clave" HeaderText="Periodo" />
                                            <asp:BoundField DataField="tcamp_desc" HeaderText="Campus" />
                                            <asp:BoundField DataField="testu_tprog_clave" HeaderText="Programa" />
                                            <asp:BoundField DataField="ttasa_clave" HeaderText="Tasa" />
                                            <asp:BoundField DataField="desc_turno" HeaderText="Turno" />
                                            <asp:BoundField DataField="testu_periodo" HeaderText="Generación" />
                                            <asp:BoundField DataField="testu_tespe_clave" HeaderText="Especialidad" />
                                            <asp:BoundField DataField="testu_tstal_clave" HeaderText="Estatus" />
                                            <asp:BoundField DataField="testu_user" HeaderText="Usuario" />
                                            <asp:BoundField DataField="testu_date" HeaderText="Fecha" />
                                            <asp:BoundField DataField="ttasa_clave" HeaderText="ttasa_clave">
                                                <HeaderStyle CssClass="ocultar" />
                                                <ItemStyle CssClass="ocultar" />
                                            </asp:BoundField>
                                        </Columns>
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
        function load_datatable_testu() {
            $('#<%= GridTestu.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridTestu.ClientID %>').find("tr:first"))).DataTable({
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
                    [1, "desc"]
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
                "stateSave": true
            });
        }
        function calcular_cuotas() {
            if ($("#customSwitches2").is(':checked')) {                
                $("#ContentPlaceHolder1_checked_input2").val('S')
            }
            else {
                $("#ContentPlaceHolder1_checked_input2").val('N')
            }
        }
        function habilitar_calcular_cuotas(habilita) {
            if (habilita === "S") {
                $("#customSwitches2").attr('checked', true);
                //alert("true");
            }
            else {
                $("#customSwitches2").attr('checked', false);
                //alert("false");

            }
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>
