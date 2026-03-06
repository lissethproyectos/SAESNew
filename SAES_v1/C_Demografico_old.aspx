<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="C_Demografico.aspx.cs" Inherits="SAES_v1.C_Demografico" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" class="init">        

        function save() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
                .then(willDelete => {
                    if (willDelete) {
                        loader_push();
                        window.location.reload();
                    }
                });
        }

        function update() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
            })
                .then(willDelete => {
                    if (willDelete) {
                        loader_push();
                        window.location.reload();
                    }
                });
        }

        function incompletos(tab) {
            console.log(tab);
            if (tab == "pais_n") {
                nuevo_pais();
                $("#ContentPlaceHolder1_c_pais").addClass("focus");
                $("#ContentPlaceHolder1_n_pais").addClass("focus");
            } else if (tab == "pais_u") {
                update_pais();
                //$("#ContentPlaceHolder1_c_pais").addClass("focus");
                $("#ContentPlaceHolder1_n_pais").addClass("focus");
            } else if (tab == "estado_n") {
                nuevo_estado();
                $("#ContentPlaceHolder1_c_estado").addClass("focus");
                $("#ContentPlaceHolder1_n_estado").addClass("focus");
                $("#ContentPlaceHolder1_cbo_pais").addClass("focus");
            } else if (tab == "estado_u") {
                update_estado();
                //$("#ContentPlaceHolder1_c_estado").addClass("focus");
                $("#ContentPlaceHolder1_n_estado").addClass("focus");
            } else if (tab == "delegacion_n") {
                nuevo_delegacion();
                $("#ContentPlaceHolder1_c_deleg").addClass("focus");
                $("#ContentPlaceHolder1_n_deleg").addClass("focus");
                $("#ContentPlaceHolder1_cbop_deleg").addClass("focus");
                $("#ContentPlaceHolder1_cboe_deleg").addClass("focus");
            } else if (tab == "delegacion_u") {
                update_delegacion();
                //$("#ContentPlaceHolder1_c_deleg").addClass("focus");
                $("#ContentPlaceHolder1_n_deleg").addClass("focus");
            }

            swal({
                type: 'warning',
                html: '<h2 class="swal2-title" id="swal2-title">Datos Incompletos</h2>Los datos marcados son obligatorios.'
            })
            $(".loader").fadeOut("slow");
        }

        function valida(tab) {
            if (tab == "pais_n") { nuevo_pais(); }
            else if (tab == "estado_n") { nuevo_estado(); }
            swal({
                type: 'warning',
                html: '<h2 class="swal2-title" id="swal2-title">Datos Incorrectos</h2>La clave ingresada ya existe.'
            })
        }

    </script>
    <style>
        #ContentPlaceHolder1_GridPaises_info {
            float: left;
        }

        #ContentPlaceHolder1_GridPaises_length {
            float: left;
        }

        #ContentPlaceHolder1_GridEstados_info {
            float: left;
        }

        #ContentPlaceHolder1_GridEstados_length {
            float: left;
        }

        #ContentPlaceHolder1_GridDelegacion_info {
            float: left;
        }

        #ContentPlaceHolder1_GridDelegacion_length {
            float: left;
        }

        #ContentPlaceHolder1_GridZip_info {
            float: left;
        }

        #ContentPlaceHolder1_GridZip_length {
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

        .ddlist {
            left: 725px;
            text-align: center;
        }

        .center_column {
            text-align: center;
        }

        .ocultar {
            display: none;
        }

        table.dataTable tbody > tr.selected, table.dataTable tbody > tr > .selected {
            background-color: #88e5cf !important;
        }

        .pills {
            /* overflow: visible; */
            background: #f5f7fa;
            height: 20px;
            /* margin: 21px 0 14px; */
            padding-left: 14px;
            position: relative;
            z-index: 1;
            width: 100%;
            border-bottom: 1px solid #e6e9ed;
        }

        .nav-item {
            margin-left: 8px;
            border: 1px solid #e6e9ed !important;
            margin-top: -17px !important;
            background: #f5f7fa;
        }

            .nav-item:hover {
                margin-left: 8px;
                border: 1px solid #e6e9ed !important;
                margin-top: -17px !important;
                background: #fff;
            }

            .nav-link.active,
            .nav-item.show .nav-link {
                border-right: 5px solid #169f85 !important;
            }

        .buttons-excel {
            margin-right: 5px !important;
            border-top-right-radius: 3px !important;
            border-bottom-right-radius: 3px !important;
        }

        .buttons-pdf {
            border-top-left-radius: 3px !important;
            border-bottom-left-radius: 3px !important;
        }

        .dataTables_scroll {
            height: 270px !important;
        }

        .focus {
            border: 1px solid rgb(227, 65, 0);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/globo.png" style="width: 30px;" /><small>Catálogos Demográficos</small></h2>
        <div class="clearfix"></div>
    </div>

    <div class="x_content">
        <nav style="margin-top: 10px;">
            <div class="nav nav-tabs justify-content-end pills" id="nav-tab" role="tablist">
                <a class="nav-item nav-link justify-content-end active" id="nav-paises-tab" data-toggle="tab" href="#nav-pais" role="tab" aria-controls="nav-pais" aria-selected="true">Paises</a>
                <a class="nav-item nav-link justify-content-end" id="nav-estados-tab" data-toggle="tab" href="#nav-estado" role="tab" aria-controls="nav-estado" aria-selected="false">Estados</a>
                <a class="nav-item nav-link justify-content-end" id="nav-delegacion-tab" data-toggle="tab" href="#nav-delegacion" role="tab" aria-controls="nav-delegacion" aria-selected="false">Delegacion-Municipio</a>
                <a class="nav-item nav-link justify-content-end" id="nav-zip-tab" data-toggle="tab" href="#nav-zip" role="tab" aria-controls="nav-zip" aria-selected="false">Código Postal</a>
            </div>
        </nav>
        <div class="tab-content" id="nav-tabContent">
            <!--Pestaña Pais-->
            <div class="tab-pane fade" id="nav-pais" role="tabpanel" aria-labelledby="nav-paises-tab">
                <asp:UpdatePanel ID="upd_pais" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="add_pais" class="row justify-content-start" style="margin-top: 15px;">
                            <div class="col-10">
                                <asp:Button ID="agregar_pais" runat="server" CssClass="btn btn-success" Text="Nuevo" OnClientClick="nuevo_pais();return false" />
                            </div>
                        </div>
                        <div id="form_pais" style="margin-top: 15px; display: none;">
                            <div class="row g-3">
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_c_pais" class="form-label">Clave</label>
                                    <asp:TextBox ID="c_pais" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="ContentPlaceHolder1_n_pais" class="form-label">Nombre</label>
                                    <asp:TextBox ID="n_pais" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="ContentPlaceHolder1_g_pais" class="form-label">Gentilicio</label>
                                    <asp:TextBox ID="g_pais" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="ContentPlaceHolder1_estatus_pais" class="form-label">Estatus</label>
                                    <asp:DropDownList ID="estatus_pais" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:HiddenField ID="edo_pais" runat="server" />
                                </div>
                            </div>
                            <br />
                            <div class="row justify-content-center" style="text-align: center; margin: auto;">
                                <div class="col-3">
                                    <asp:Button ID="cancel_pais" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClientClick="cancelar_pais();return false" />
                                    <asp:Button ID="save_pais" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="save_pais_Click" OnClientClick="loader_push();" />
                                    <asp:Button ID="update_pais" runat="server" CssClass="btn btn-round btn-success ocultar" Text="Actualizar" OnClick="update_pais_Click" OnClientClick="loader_push();" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="table_pais">
                    <asp:GridView ID="GridPaises" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False">
                        <Columns>
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
                    </asp:GridView>
                </div>

            </div>
            <!--\Pestaña Pais-->
            <!--Pestaña Estado-->
            <div class="tab-pane fade" id="nav-estado" role="tabpanel" aria-labelledby="nav-estado-tab">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="add_estado" class="row justify-content-start" style="margin-top: 15px;">
                            <div class="col-10">
                                <asp:Button ID="agregar_estado" runat="server" CssClass="btn btn-success" Text="Nuevo" OnClientClick="nuevo_estado();return false" />
                            </div>
                        </div>
                        <div id="form_estado" style="display: none; margin-top: 15px;">
                            <div class="row g-3">
                                <div class="col-md-3">
                                    <label for="ContentPlaceHolder1_cbo_pais" class="form-label">Pais</label>
                                    <asp:DropDownList ID="cbo_pais" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:HiddenField ID="dde_pais" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_c_estado" class="form-label">Clave</label>
                                    <asp:TextBox ID="c_estado" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="ContentPlaceHolder1_n_estado" class="form-label">Nombre</label>
                                    <asp:TextBox ID="n_estado" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="ContentPlaceHolder1_estatus_estado" class="form-label">Estatus</label>
                                    <asp:DropDownList ID="estatus_estado" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:HiddenField ID="edo_estado" runat="server" />
                                </div>
                            </div>
                            <br />
                            <div class="row justify-content-center" style="text-align: center; margin: auto;">
                                <div class="col-3">
                                    <asp:Button ID="cancel_estado" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClientClick="cancelar_estado();return false" />
                                    <asp:Button ID="save_estado" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="save_estado_Click" OnClientClick="loader_push();" />
                                    <asp:Button ID="update_estado" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="update_estado_Click" OnClientClick="loader_push();" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="tabla_estados">
                    <asp:GridView ID="GridEstados" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Estado" />
                            <asp:BoundField DataField="C_PAIS" HeaderText="clave_pais">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PAIS" HeaderText="Pais" />
                            <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <!--\Pestaña Estado-->
            <!--Pestaña Delegación-->
            <div class="tab-pane fade" id="nav-delegacion" role="tabpanel" aria-labelledby="nav-delegacion-tab">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="add_delegacion" class="row justify-content-start" style="margin-top: 15px;">
                            <div class="col-10">
                                <asp:Button ID="agregar_delegacion" runat="server" CssClass="btn btn-success" Text="Nuevo" OnClientClick="nuevo_delegacion();return false" />
                            </div>
                        </div>

                        <div id="form_delegacion" style="display: none; margin-top: 15px;" runat="server">
                            <div class="row g-3">
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_cbop_deleg" class="form-label">Pais</label>
                                    <asp:DropDownList ID="cbop_deleg" runat="server" CssClass="form-control" Font-Size="Small" AutoPostBack="true" OnSelectedIndexChanged="cbop_deleg_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:HiddenField ID="pais_deleg" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_cboe_deleg" class="form-label">Estado</label>

                                    <asp:DropDownList ID="cboe_deleg" runat="server" CssClass="form-control" Font-Size="Small"></asp:DropDownList>
                                    <asp:HiddenField ID="state_deleg" runat="server" />

                                </div>
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_c_deleg" class="form-label">Clave</label>
                                    <asp:TextBox ID="c_deleg" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="ContentPlaceHolder1_n_deleg" class="form-label">Nombre</label>
                                    <asp:TextBox ID="n_deleg" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="ContentPlaceHolder1_e_deleg" class="form-label">Estatus</label>
                                    <asp:DropDownList ID="e_deleg" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:HiddenField ID="status_deleg" runat="server" />
                                </div>
                            </div>
                            <br />
                            <div class="row justify-content-center" style="text-align: center; margin: auto;">
                                <div class="col-3">
                                    <asp:Button ID="cancel_deleg" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClientClick="cancelar_delegacion();return false" />
                                    <asp:Button ID="save_deleg" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" />
                                    <asp:Button ID="update_deleg" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="tabla_delegacion">
                    <asp:GridView ID="GridDelegacion" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false">
                        <Columns>
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
                    </asp:GridView>
                </div>
            </div>
            <!--\Pestaña Delegación-->
            <!--Pestaña ZIP-->
            <div class="tab-pane fade" id="nav-zip" role="tabpanel" aria-labelledby="nav-zip-tab">
                <div id="add_zip" class="row justify-content-start" style="margin-top: 15px;">
                    <div class="col-10">
                        <asp:Button ID="agregar_zip" runat="server" CssClass="btn btn-success" Text="Nuevo" OnClientClick="nuevo_zip();return false" />
                    </div>
                </div>
                <div id="form_zip" style="display: none; margin-top: 15px;">
                    <div class="row g-3">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_cbop_zip" class="form-label">Pais</label>
                            <asp:DropDownList ID="cbop_zip" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:HiddenField ID="pais_zip" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_cboe_zip" class="form-label">Estado</label>
                            <asp:DropDownList ID="cboe_zip" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:HiddenField ID="state_zip" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_cbod_zip" class="form-label">Delegacion</label>
                            <asp:DropDownList ID="cbod_zip" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:HiddenField ID="country_zip" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_c_zip" class="form-label">Clave</label>
                            <asp:TextBox ID="c_zip" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_n_zip" class="form-label">Nombre</label>
                            <asp:TextBox ID="n_zip" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_e_zip" class="form-label">Estatus</label>
                            <asp:DropDownList ID="e_zip" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:HiddenField ID="status_zip" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="row justify-content-center" style="text-align: center; margin: auto;">
                        <div class="col-3">
                            <asp:Button ID="cancel_zip" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClientClick="cancelar_zip();return false" />
                            <asp:Button ID="save_zip" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" />
                            <asp:Button ID="update_zip" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" />
                        </div>
                    </div>
                </div>
                <div id="tabla_zip">
                    <asp:GridView ID="GridZip" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Colonia" />
                            <asp:BoundField DataField="C_ESTADO" HeaderText="Clave_estado">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                            <asp:BoundField DataField="C_DELEGACION" HeaderText="Clave_delegacion">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DELEGACION" HeaderText="Delegacion" />
                            <asp:BoundField DataField="ESTATUS_CODE" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <!--\Pestaña ZIP-->
        </div>


    </div>
    <!--Funciones Generales-->
    <script>
        $(document).ready(function () {

            if (location.hash) {
                $("a[href='" + location.hash + "']").tab("show");
            }
            $(document.body).on("click", "a[data-toggle='tab']", function (event) {
                location.hash = this.getAttribute("href");
            });
        });

        $(window).on("popstate", function () {
            var anchor = location.hash || $("a[data-toggle='tab']").first().attr("href");
            $("a[href='" + anchor + "']").tab("show");
        });
    </script>
    <!--\Funciones Generales-->
    <!--Funciones para pestaña pais-->
    <script>        
        $(document).ready(function () {

            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
            });

            let table_p = $("#ContentPlaceHolder1_GridPaises").DataTable({
                select: {
                    style: 'single', info: false
                },
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
                        text: 'Exportar Excel'
                    },
                    {
                        title: 'SAES_Catálogo de Paises',
                        className: 'btn-dark',
                        extend: 'pdf',
                        text: 'Exportar PDF'
                    }
                ]
            });
            $('#ContentPlaceHolder1_GridPaises tbody').on('click', 'tr', function () {
                var data = table_p.row(this).data();
                var clave_pais = data[0];
                var nombre_pais = data[1];
                var gen_pais = data[2];
                var est_pais = data[3];

                if ($(this).hasClass("selected")) {
                    $("#add_pais").fadeIn("fast");
                    $("#form_pais").slideUp("slow");
                } else {
                    $("#ContentPlaceHolder1_c_pais").val(clave_pais);
                    $("#ContentPlaceHolder1_c_pais").prop("readonly", true);
                    $("#ContentPlaceHolder1_n_pais").val(nombre_pais);
                    $("#ContentPlaceHolder1_g_pais").val(gen_pais);
                    $("#ContentPlaceHolder1_estatus_pais").find('option[value="' + est_pais + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_edo_pais").val(est_pais);
                    $("#add_pais").fadeOut("fast");
                    $("#form_pais").slideDown("slow");
                    $("#ContentPlaceHolder1_update_pais").css({ display: "initial" });
                    $("#ContentPlaceHolder1_save_pais").css({ display: "none" });
                }
            });
        });
        function nuevo_pais() {
            $("#ContentPlaceHolder1_update_pais").css({ display: "none" });
            $("#ContentPlaceHolder1_c_pais").prop("readonly", false);
            $("#ContentPlaceHolder1_save_pais").css({ display: "initial" });
            $("#ContentPlaceHolder1_c_pais").val('');
            $("#ContentPlaceHolder1_n_pais").val('');
            $("#ContentPlaceHolder1_g_pais").val('');
            $("#table_pais").fadeOut("fast");
            $("#add_pais").fadeOut("fast");
            $("#form_pais").slideDown("slow");
        }
        function cancelar_pais() {
            $("#ContentPlaceHolder1_GridPaises tbody tr").removeClass("selected");
            $("#ContentPlaceHolder1_c_pais").val('');
            $("#ContentPlaceHolder1_n_pais").val('');
            $("#ContentPlaceHolder1_c_pais").removeClass("focus");
            $("#ContentPlaceHolder1_n_pais").removeClass("focus");
            $("#ContentPlaceHolder1_g_pais").val('');
            $("#table_pais").fadeIn("fast");
            $("#add_pais").fadeIn("fast");
            $("#form_pais").slideUp("slow");
        }
        function update_pais() {
            $("#form_pais").slideDown("slow");
            $("#add_pais").fadeOut("fast");
            $("#ContentPlaceHolder1_update_pais").css({ display: "initial" });
            $("#ContentPlaceHolder1_save_pais").css({ display: "none" });
            $("#ContentPlaceHolder1_c_pais").prop("readonly", true);
        }
        function show_pais() {
            $('#nav-pais').tab('show');
        }
        $("#ContentPlaceHolder1_estatus_pais").change(function () {
            var est_pais = $("#ContentPlaceHolder1_estatus_pais").val();
            $("#ContentPlaceHolder1_edo_pais").val(est_pais);
        });
    </script>
    <!--\Funciones para pestaña pais-->
    <!--Funciones para pestaña estado-->
    <script>

        $(document).ready(function () {

            let table_e = $("#ContentPlaceHolder1_GridEstados").DataTable({
                select: {
                    style: 'single', info: false
                },
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
                    [0, "asc"]
                ],
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                "autoWidth": true,
                dom: '<"top"if>rt<"bottom"lBp><"clear">',
                buttons: [
                    {
                        title: 'SAES_Catálogo de Estados',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel'
                    },
                    {
                        title: 'SAES_Catálogo de Estados',
                        className: 'btn-dark',
                        extend: 'pdf',
                        text: 'Exportar PDF'
                    }
                ]
            });

            $('#ContentPlaceHolder1_GridEstados tbody').on('click', 'tr', function () {
                var data = table_e.row(this).data();
                var clave_estado = data[0];
                var nombre_estado = data[1];
                var clave_pais = data[2];
                var est_estado = data[4];

                if ($(this).hasClass("selected")) {
                    $("#add_estado").fadeIn("fast");
                    $("#form_estado").slideUp("slow");
                } else {
                    $("#ContentPlaceHolder1_cbo_pais").find('option[value="' + clave_pais + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_dde_pais").val(clave_pais);
                    $("#ContentPlaceHolder1_c_estado").val(clave_estado);
                    $("#ContentPlaceHolder1_c_estado").prop("readonly", true);
                    $("#ContentPlaceHolder1_cbo_pais").prop("disabled", true);
                    $("#ContentPlaceHolder1_n_estado").val(nombre_estado);
                    $("#ContentPlaceHolder1_estatus_estado").find('option[value="' + est_estado + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_edo_estado").val(est_estado);
                    $("#add_estado").fadeOut("fast");
                    $("#form_estado").slideDown("slow");
                    $("#ContentPlaceHolder1_update_estado").css({ display: "initial" });
                    $("#ContentPlaceHolder1_save_estado").css({ display: "none" });
                }
            });
        });

        function nuevo_estado() {
            $("#ContentPlaceHolder1_update_estado").css({ display: "none" });
            $("#ContentPlaceHolder1_c_estado").prop("readonly", false);
            $("#ContentPlaceHolder1_cbo_pais").prop("disabled", false);
            $("#ContentPlaceHolder1_save_estado").css({ display: "initial" });
            $("#ContentPlaceHolder1_c_estado").val('');
            $("#ContentPlaceHolder1_n_estado").val('');
            $("#ContentPlaceHolder1_cbo_pais").val(0).change();
            $("#tabla_estados").fadeOut("fast");
            $("#add_estado").fadeOut("fast");
            $("#form_estado").slideDown("slow");
        }

        function cancelar_estado() {
            $("#ContentPlaceHolder1_GridEstados tbody tr").removeClass("selected");
            $("#ContentPlaceHolder1_c_estado").val('');
            $("#ContentPlaceHolder1_n_estado").val('');
            $("#ContentPlaceHolder1_cbo_pais").val(0).change();
            $("#tabla_estados").fadeIn("fast");
            $("#add_estado").fadeIn("fast");
            $("#form_estado").slideUp("fast");
        }

        function update_estado() {
            $("#form_estado").slideDown("slow");
            $("#add_estado").fadeOut("fast");
            $("#ContentPlaceHolder1_update_estado").css({ display: "initial" });
            $("#ContentPlaceHolder1_save_estado").css({ display: "none" });
            $("#ContentPlaceHolder1_c_estado").prop("readonly", true);
            $("#ContentPlaceHolder1_cbo_pais").prop("disabled", true);
        }

        $("#ContentPlaceHolder1_cbo_pais").change(function () {
            var cbo_pais = $("#ContentPlaceHolder1_cbo_pais").val();
            $("#ContentPlaceHolder1_dde_pais").val(cbo_pais);
        });

        $("#ContentPlaceHolder1_estatus_estado").change(function () {
            var estatus_estado = $("#ContentPlaceHolder1_estatus_estado").val();
            $("#ContentPlaceHolder1_edo_estado").val(estatus_estado);
        });
    </script>
    <!--\Funciones para pestaña estado-->
    <!--Funciones para pestaña delegacion-->
    <script>
        $(document).ready(function () {

            let table_d = $("#ContentPlaceHolder1_GridDelegacion").DataTable({
                select: {
                    style: 'single', info: false
                },
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
                        text: 'Exportar Excel'
                    },
                    {
                        title: 'SAES_Catálogo de Delegaciones',
                        className: 'btn-dark',
                        extend: 'pdf',
                        text: 'Exportar PDF'
                    }
                ]
            });

            $('#ContentPlaceHolder1_GridDelegacion tbody').on('click', 'tr', function () {
                var data = table_d.row(this).data();
                var clave_delegacion = data[0];
                var nombre_delegacion = data[1];
                var clave_pais = data[2];
                var clave_estado = data[4];
                var est_delegacion = data[6];

                if ($(this).hasClass("selected")) {
                    $("#add_delegacion").fadeIn("fast");
                    $("#form_delegacion").slideUp("slow");
                } else {
                    $("#ContentPlaceHolder1_c_deleg").val(clave_delegacion);
                    $("#ContentPlaceHolder1_c_deleg").prop("readonly", true);
                    $("#ContentPlaceHolder1_cbop_deleg").prop("disabled", true);
                    $("#ContentPlaceHolder1_cboe_deleg").prop("disabled", true);
                    $("#ContentPlaceHolder1_n_deleg").val(nombre_delegacion);
                    $("#ContentPlaceHolder1_cbop_deleg").find('option[value="' + clave_pais + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_pais_deleg").val(clave_pais);
                    $("#ContentPlaceHolder1_cboe_deleg").find('option[value="' + clave_estado + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_state_deleg").val(clave_estado);
                    $("#ContentPlaceHolder1_e_deleg").find('option[value="' + est_delegacion + '"]').prop("selected", true);
                    $("#ContentPlaceHolder1_status_deleg").val(est_delegacion);
                    $("#add_delegacion").fadeOut("fast");
                    $("#form_delegacion").slideDown("slow");
                    $("#ContentPlaceHolder1_update_deleg").css({ display: "initial" });
                    $("#ContentPlaceHolder1_save_deleg").css({ display: "none" });
                }
            });
        });

        function nuevo_delegacion() {
            $("#ContentPlaceHolder1_update_deleg").css({ display: "none" });
            $("#ContentPlaceHolder1_c_deleg").prop("readonly", false);
            $("#ContentPlaceHolder1_cbop_deleg").prop("disabled", false);
            $("#ContentPlaceHolder1_cboe_deleg").prop("disabled", false);
            $("#ContentPlaceHolder1_save_deleg").css({ display: "initial" });
            $("#ContentPlaceHolder1_c_deleg").val('');
            $("#ContentPlaceHolder1_n_deleg").val('');
            $("#ContentPlaceHolder1_cbop_deleg").val(0).change();
            $("#ContentPlaceHolder1_cboe_deleg").val(0).change();
            $("#tabla_delegacion").fadeOut("fast");
            $("#add_delegacion").fadeOut("fast");
            $("#form_delegacion").slideDown("slow");
            return false;
        }

        function cancelar_delegacion() {
            $("#ContentPlaceHolder1_GridDelegacion tbody tr").removeClass("selected");
            $("#ContentPlaceHolder1_c_deleg").val('');
            $("#ContentPlaceHolder1_n_deleg").val('');
            $("#ContentPlaceHolder1_cbop_deleg").val(0).change();
            $("#ContentPlaceHolder1_cboe_deleg").val(0).change();
            $("#tabla_delegacion").fadeIn("fast");
            $("#add_delegacion").fadeIn("fast");
            $("#form_delegacion").slideUp("slow");
        }

        function update_delegacion() {
            $("#form_delegacion").slideDown("slow");
            $("#add_delegacion").fadeOut("fast");
            $("#ContentPlaceHolder1_update_deleg").css({ display: "initial" });
            $("#ContentPlaceHolder1_save_deleg").css({ display: "none" });
            $("#ContentPlaceHolder1_c_deleg").prop("readonly", true);
            $("#ContentPlaceHolder1_cbop_deleg").prop("disabled", true);
            $("#ContentPlaceHolder1_cboe_deleg").prop("disabled", true);
        }

        $("#ContentPlaceHolder1_cbop_deleg").change(function () {
            var cbod_pais = $("#ContentPlaceHolder1_cbop_deleg").val();
            $("#ContentPlaceHolder1_pais_deleg").val(cbod_pais);
        });

        $("#ContentPlaceHolder1_cboe_deleg").change(function () {
            var cbod_estado = $("#ContentPlaceHolder1_cboe_deleg").val();
            $("#ContentPlaceHolder1_state_deleg").val(cbod_estado);
        });

        $("#ContentPlaceHolder1_e_deleg").change(function () {
            var estatus_deleg = $("#ContentPlaceHolder1_e_deleg").val();
            $("#ContentPlaceHolder1_status_deleg").val(estatus_deleg);
        });
    </script>
    <!--\Funciones para pestaña delegacion-->
    <!--Funciones para pestaña Zip-->
    <script>
        $(document).ready(function () {

            let table_z = $("#ContentPlaceHolder1_GridZip").DataTable({
                select: {
                    style: 'single', info: false
                },
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
                        title: 'SAES_Catálogo de Códigos Postales',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel'
                    },
                    {
                        title: 'SAES_Catálogo de Códigos Postales',
                        className: 'btn-dark',
                        extend: 'pdf',
                        text: 'Exportar PDF'
                    }
                ]
            });

            $('#ContentPlaceHolder1_GridZip tbody').on('click', 'tr', function () {
                var data = table_z.row(this).data();
                //alert('You clicked on ' + data[0] + '\'s row');
                var clave_zip = data[0];
                var nombre_zip = data[1];
                var clave_estado = data[2];
                var clave_delegacion = data[4];
                var est_estado = data[6];

                if ($(this).hasClass("selected")) {
                    $("#add_zip").fadeIn("fast");
                    $("#form_zip").slideUp("slow");
                } else {
                    $("#ContentPlaceHolder1_c_zip").val(clave_zip);
                    $("#ContentPlaceHolder1_c_zip").prop("readonly", true);
                    $("#ContentPlaceHolder1_n_zip").val(nombre_zip);
                    $("#ContentPlaceHolder1_cboe_zip").val(clave_estado).change();
                    //$("#ContentPlaceHolder1_cboe_deleg").prop("disabled", true);
                    $("#ContentPlaceHolder1_cbod_zip").val(clave_delegacion).change();
                    $("#ContentPlaceHolder1_e_zip").val(est_estado).change();
                    $("#add_zip").fadeOut("fast");
                    $("#form_zip").slideDown("slow");
                    $("#ContentPlaceHolder1_update_zip").css({ display: "initial" });
                    $("#ContentPlaceHolder1_save_zip").css({ display: "none" });
                }
            });
        });

        function nuevo_zip() {
            $("#ContentPlaceHolder1_update_zip").css({ display: "none" });
            $("#ContentPlaceHolder1_c_zip").prop("readonly", false);
            $("#ContentPlaceHolder1_save_zip").css({ display: "initial" });
            $("#ContentPlaceHolder1_c_zip").val('');
            $("#ContentPlaceHolder1_n_zip").val('');
            $("#tabla_zip").fadeOut("fast");
            $("#add_zip").fadeOut("fast");
            $("#form_zip").slideDown("slow");
        }

        function cancelar_zip() {
            $("#ContentPlaceHolder1_GridZip tbody tr").removeClass("selected");
            $("#ContentPlaceHolder1_c_zip").val('');
            $("#ContentPlaceHolder1_n_zip").val('');
            $("#tabla_zip").fadeIn("fast");
            $("#add_zip").fadeIn("fast");
            $("#form_zip").slideUp("slow");
        }
    </script>
    <!--\Funciones para pestaña Zip-->
</asp:Content>
