<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcamp.aspx.cs" Inherits="SAES_v1.tcamp" %>

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


        function error_clave() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'error',
                html: '<h2 class="swal2-title" id="swal2-title">La clave ingresada ya existe</h2>'
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
            $("#campus").addClass("current-page");
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

        //---- Clave Campus ----//
        function validarclaveCampus(idEl, ind) {
            const idElemento = idEl;
            if (ind == 0) {
                let documento = document.getElementById(idElemento).value;
                if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                    errorForm(idElemento, 'Por favor ingresa una clave valida');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            } else {
                errorForm(idElemento, 'La clave ingresada ya existe');
                return false;
            }
        }

        //---- Nombre Campus ----//
        function validarNombreCampus(idEl) {
            const idElemento = idEl;
            let tamin = document.getElementById(idElemento).value;
            if (tamin == null || tamin.length == 0 || /^\s+$/.test(tamin)) {
                errorForm(idElemento, 'Por favor ingresa el nombre del campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos Campus ----//
        function validar_campos_campus(e) {
            event.preventDefault(e);
            validarclaveCampus('ContentPlaceHolder1_c_campus', 0);
            validarNombreCampus('ContentPlaceHolder1_n_campus');
            return false;
        }
    </script>
    <style>
        #operacion ul {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <i class="fa fa-building" aria-hidden="true"></i>
            Catálogo de Campus
        </h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_Campus" runat="server">
            <ContentTemplate>
                <div id="form_Campus" runat="server">
                    <div class="form-row">
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_c_campus" class="form-label">Clave</label>
                            <asp:TextBox ID="c_campus" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqCampus" runat="server" ErrorMessage="Ingresar Clave" CssClass="text-danger" ControlToValidate="c_campus" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-10">
                            <label for="ContentPlaceHolder1_n_campus" class="form-label">Nombre</label>
                            <asp:TextBox ID="n_campus" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqNombre" runat="server" ErrorMessage="Ingresar Nombre" CssClass="text-danger" ControlToValidate="n_campus" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-4">
                            Abreviatura
                            <asp:TextBox ID="a_campus" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="col-sm-4">
                            RFC
                            <asp:TextBox ID="RFC_campus" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqRfc" runat="server" ErrorMessage="Ingresar RFC" CssClass="text-danger" ControlToValidate="RFC_campus" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-sm-4">
                            Estatus
                            <asp:DropDownList ID="estatus_campus" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqEstatus" runat="server" ErrorMessage="Seleccionar Estatus" CssClass="text-danger" ControlToValidate="estatus_campus" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_ddp_campus" class="form-label">Pais</label>
                            <asp:DropDownList ID="ddp_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddp_campus_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="reqPais" runat="server" ErrorMessage="Seleccionar Pais" CssClass="text-danger" ControlToValidate="ddp_campus" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_dde_campus" class="form-label">Estado</label>
                            <asp:DropDownList ID="dde_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dde_campus_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="reqEstado" runat="server" ErrorMessage="Seleccionar Estado" CssClass="text-danger" ControlToValidate="dde_campus" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_ddd_campus" class="form-label">Delegacion-Municipio</label>
                            <asp:DropDownList ID="ddd_campus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddd_campus_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="reqMunicipio" runat="server" ErrorMessage="Seleccionar Delegación/Municipio" CssClass="text-danger" ControlToValidate="ddd_campus" ValidationGroup="guardar" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_zip_campus" class="form-label">Código Postal</label>
                            <asp:TextBox ID="zip_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="zip_campus_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_ddz_campus" class="form-label">Colonia</label>
                            <asp:DropDownList ID="ddz_campus" runat="server" CssClass="form-control"></asp:DropDownList>

                        </div>
                        <div class="col-sm-7">
                            <label for="ContentPlaceHolder1_direc_campus" class="form-label">Dirección</label>
                            <asp:TextBox ID="direc_campus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" id="btn_campus" runat="server">
                    <div class="col text-center">
                        <asp:Button ID="cancelar_campus" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_campus_Click" />
                        <asp:Button ID="guardar_campus" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="guardar_campus_Click" ValidationGroup="guardar" />
                        <asp:Button ID="actualizar_campus" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="actualizar_campus_Click" ValidationGroup="guardar" />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="GridCampus" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridCampus_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttSel" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar">
                                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                <asp:BoundField DataField="Nombre" HeaderText="Campus" />
                                <asp:BoundField DataField="ABREVIATURA" HeaderText="Abreviatura" />
                                <asp:BoundField DataField="RFC" HeaderText="RFC" />
                                <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha de Modificacion" />
                                <asp:BoundField DataField="C_PAIS" HeaderText="Estatus_code">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="C_ESTADO" HeaderText="">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="N_ESTADO" HeaderText="">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="C_DELE" HeaderText="">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="N_DELE" HeaderText="">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ZIP" HeaderText="">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="COLONIA" HeaderText="">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DIRECCION" HeaderText="">
                                    <HeaderStyle CssClass="ocultar" />
                                    <ItemStyle CssClass="ocultar" />
                                </asp:BoundField>
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function load_datatable() {
            $('#<%= GridCampus.ClientID %>').prepend($("<thead></thead>").append($('#<%= GridCampus.ClientID %>').find("tr:first"))).DataTable({

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
                            columns: [1, 2, 3, 4, 6, 7]
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
                            columns: [1, 2, 3, 4, 6, 7]
                        }
                    }
                ],
                stateSave: true
            });
        }

        function destroy_table() {
            $("#ContentPlaceHolder1_GridCampus").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>
