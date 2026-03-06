<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tdele.aspx.cs" Inherits="SAES_v1.tdele" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function error_consulta() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
            })
        }

        function error_transaccion() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
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


        //----Validación de País----//
        function validar_pais_d(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Debes seleccionar un país válido');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Estado----//
        function validar_estado_d(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Debes seleccionar un estado válido');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Clave Delegación ----//
        function validarclaveDelegacion(idEl, ind) {
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

        //---- Nombre Delegación ----//
        function validarNombreDelegacion(idEl) {
            const idElemento = idEl;
            let tamin = document.getElementById(idElemento).value;
            if (tamin == null || tamin.length == 0 || /^\s+$/.test(tamin)) {
                errorForm(idElemento, 'Por favor ingresa el nombre del estado');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida fomulario completo ----//
        function validar_campos_delegacion(e) {
            event.preventDefault(e);
            validar_pais_d('ContentPlaceHolder1_cbop_deleg');
            validar_estado_d('ContentPlaceHolder1_cboe_deleg');
            validarclaveDelegacion('ContentPlaceHolder1_c_deleg', 0);
            validarNombreDelegacion('ContentPlaceHolder1_e_deleg');
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
            <img src="Images/Operaciones/delegacion.jpg" style="width: 30px;" /><small>Alcaldías / Municipios</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_delegacion" runat="server">
                    <div class="row g-3" style="margin-top: 15px;">
                        <div class="col-md-6">
                            <label for="ContentPlaceHolder1_cbop_deleg" class="form-label">Pais</label>
                            <asp:DropDownList ID="cbop_deleg" runat="server" CssClass="form-control" Font-Size="Small" AutoPostBack="true" OnSelectedIndexChanged="cbop_deleg_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label for="ContentPlaceHolder1_cboe_deleg" class="form-label">Estado</label>
                            <asp:DropDownList ID="cboe_deleg" runat="server" CssClass="form-control" Font-Size="Small" AutoPostBack="true" OnSelectedIndexChanged="cboe_deleg_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_c_deleg" class="form-label">Clave</label>
                            <asp:TextBox ID="c_deleg" runat="server" CssClass="form-control" Font-Size="Small"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_n_deleg" class="form-label">Nombre</label>
                            <asp:TextBox ID="n_deleg" runat="server" CssClass="form-control" Font-Size="Small"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_e_deleg" class="form-label">Estatus</label>
                            <asp:DropDownList ID="e_deleg" runat="server" CssClass="form-control" Font-Size="Small"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_delegacion" runat="server">
                    <div class="col-md-3" style="text-align: center">
                        <asp:Button ID="cancel_deleg" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancel_deleg_Click" />
                        <asp:Button ID="save_deleg" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClientClick="destroy_table();" OnClick="save_deleg_Click" />
                        <asp:Button ID="update_deleg" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClientClick="destroy_table();" OnClick="update_deleg_Click" />
                    </div>
                </div>

                <div id="tabla_delegacion">
                    <asp:GridView ID="GridDelegacion" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDelegacion_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Delegacion" />
                            <asp:BoundField DataField="C_PAIS" HeaderText="Clave_pais">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PAIS" HeaderText="País" />
                            <asp:BoundField DataField="C_ESTADO" HeaderText="Clave_estado">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
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
            let table_d = $("#ContentPlaceHolder1_GridDelegacion").DataTable({
                language: {
                    sProcessing: 'Procesando...',
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
                }, scrollY: '500px',
                scrollCollapse: true,
                order: [
                    [2, "asc"]
                ],
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'SAES_Catálogo de Delegaciones',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1,2, 4, 6, 8, 9]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Delegaciones',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 4, 6, 8, 9]
                        }
                    }
                ]
            });

        }
        function destroy_table() {
            $("#ContentPlaceHolder1_GridDelegacion").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>
