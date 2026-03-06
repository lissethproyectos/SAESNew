<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcamp.aspx.cs" Inherits="SAES_v1.tcamp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script>
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
                validarclaveCampus('ContentPlaceHolder1_c_campus',0);
                validarNombreCampus('ContentPlaceHolder1_n_campus');
                return false;
            }
        </script>
        <style>
        #operacion ul{
            display:block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/universidad.png" style="width: 30px;" /><small>Catálogo de Campus</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <ul class="nav nav-tabs justify-content-end">
            <li class="nav-item">
                <a class="nav-link active" href="tcamp.aspx">Campus</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="tcapr.aspx">Programas</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="tpaco.aspx">Cobranza</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="tcseq.aspx">Secuencias</a>
            </li>
        </ul>
        <asp:UpdatePanel ID="upd_Campus" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_Campus" runat="server">
                    <div class="row g-3" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_c_campus" class="form-label">Clave</label>
                            <asp:TextBox ID="c_campus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <label for="ContentPlaceHolder1_n_campus" class="form-label">Nombre</label>
                            <asp:TextBox ID="n_campus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_a_campus" class="form-label">Abreviatura</label>
                            <asp:TextBox ID="a_campus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_RFC_campus" class="form-label">RFC</label>
                            <asp:TextBox ID="RFC_campus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_estatus_pais" class="form-label">Estatus</label>
                            <asp:DropDownList ID="estatus_campus" runat="server" CssClass="form-control"></asp:DropDownList>
                            
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddp_campus" class="form-label">Pais</label>
                            <asp:DropDownList ID="ddp_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddp_campus_SelectedIndexChanged"></asp:DropDownList>
                            
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_dde_campus" class="form-label">Estado</label>
                            <asp:DropDownList ID="dde_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dde_campus_SelectedIndexChanged"></asp:DropDownList>
                            
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddd_campus" class="form-label">Delegacion-Municipio</label>
                            <asp:DropDownList ID="ddd_campus" runat="server" CssClass="form-control"></asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_zip_campus" class="form-label">Código Postal</label>
                            <asp:TextBox ID="zip_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="zip_campus_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddz_campus" class="form-label">Colonia</label>
                            <asp:DropDownList ID="ddz_campus" runat="server" CssClass="form-control"></asp:DropDownList>

                        </div>
                        <div class="col-md-7">
                            <label for="ContentPlaceHolder1_direc_campus" class="form-label">Dirección</label>
                            <asp:TextBox ID="direc_campus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_campus" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="cancelar_campus" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_campus_Click"/>
                        <asp:Button ID="guardar_campus" runat="server" CssClass="btn btn-round btn-success" Text="Agregar" OnClick="guardar_campus_Click"/>
                        <asp:Button ID="actualizar_campus" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="actualizar_campus_Click"/>
                    </div>
                </div>
                <div id="table_campus">
                    <asp:GridView ID="GridCampus" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridCampus_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Campus" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function load_datatable() {
            let table_campus = $("#ContentPlaceHolder1_GridCampus").DataTable({
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
                            columns: [1, 2, 3, 4, 6,7]
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
