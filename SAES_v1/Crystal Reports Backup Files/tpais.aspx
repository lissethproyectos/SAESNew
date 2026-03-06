<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpais.aspx.cs" Inherits="SAES_v1.tpais" %>

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

        //---- Clave Pais ----//
        function validarclavePais(idEl, ind) {
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

        //---- Nombre Pis ----//
        function validarNombrePais(idEl) {
            const idElemento = idEl;
            let tamin = document.getElementById(idElemento).value;
            if (tamin == null || tamin.length == 0 || /^\s+$/.test(tamin)) {
                errorForm(idElemento, 'Por favor ingresa el nombre del país');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //---- Valida Campos Pais ----//
        function validar_campos_pais(e) {
            //event.preventDefault(e);
            validarclavePais('ContentPlaceHolder1_c_pais', 0);
            validarNombrePais('ContentPlaceHolder1_n_pais');
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
            <img src="Images/Operaciones/globo.png" style="width: 30px;" /><small>Catálogo Paises</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrPais" runat="server"
                    AssociatedUpdatePanelID="upd_pais">
                    <ProgressTemplate>
                        <asp:Image ID="img2" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="~/Images/Sitemaster/loader.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <asp:UpdatePanel ID="upd_pais" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_pais" runat="server">
                    <div class="form-row">
                        <div class="col-sm-2">
                            <label for="ContentPlaceHolder1_c_pais" class="form-label">Clave</label>
                            <asp:TextBox ID="c_pais" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="ContentPlaceHolder1_n_pais" class="form-label">Nombre</label>
                            <asp:TextBox ID="n_pais" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_g_pais" class="form-label">Gentilicio</label>
                            <asp:TextBox ID="g_pais" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-sm-3">
                            <label for="ContentPlaceHolder1_estatus_pais" class="form-label">Estatus</label>
                            <asp:DropDownList ID="estatus_pais" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_pais" runat="server">
                    <div class="col-md-3" style="text-align: center;">
                        <asp:Button ID="cancel_pais" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancel_pais_Click" />
                        <asp:Button ID="save_pais" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="save_pais_Click" OnClientClick="destroy_table();" />
                        <asp:Button ID="update_pais" runat="server" CssClass="btn btn-round btn-success ocultar" Text="Actualizar" Visible="false" OnClick="update_pais_Click" OnClientClick="destroy_table();" />
                    </div>
                </div>
                <div id="table_pais">
                    <asp:GridView ID="GridPaises" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridPaises_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Pais" />
                            <asp:BoundField DataField="GENTIL" HeaderText="Gentilicio" />
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

        $("#ContentPlaceHolder1_estatus_pais").change(function () {
            var estatus = this.value;
            console.log(estatus);
            $("#ContentPlaceHolder1_edo_pais").val(estatus)
        });
        function load_datatable() {
            let table_p = $("#ContentPlaceHolder1_GridPaises").DataTable({
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
                        title: 'SAES_Catálogo de Paises',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 5, 6]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Paises',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 5, 6]
                        }
                    }
                ],
                stateSave: false
            });
        }
        function destroy_table() {
            $("#ContentPlaceHolder1_GridPaises").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }

    </script>
</asp:Content>
