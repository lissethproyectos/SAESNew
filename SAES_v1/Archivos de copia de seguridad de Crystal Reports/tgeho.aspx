<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tgeho.aspx.cs" Inherits="SAES_v1.tgeho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function generacion() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">GENERACIÓN DE HORARIOS COMPLETADA</h2>Favor de validar en el listado.'
            })
        }

        function Nogeneracion() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">NO SE PUEDE REALIZAR GENERACIÓN"</h2>EXISTEN ALUMNOS INSCRITOS'
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


        //----Validación de Periodo----//
        function validar_periodo_base(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar periodo base');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Turno----//
        function validar_turno(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar turno');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Campus----//
        function validar_campus(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Validación de Periodo----//
        function validar_periodo_destino(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar periodo destino');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
      
        //---- Valida fomulario completo ----//
        function validar_tgeho(e) {
            event.preventDefault(e);
            validar_periodo_base('ContentPlaceHolder1_ddl_periodo');
            validar_turno('ContentPlaceHolder1_ddl_turno');
            validar_campus('ContentPlaceHolder1_ddl_campus');
            validar_periodo_destino('ContentPlaceHolder1_ddl_periodo_destino');
            return false;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/go.png" style="width: 30px;" /><small>Proyección Grupos Materia</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_zip" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_periodo" class="form-label">Periodo Base</label>
                            <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_turno" class="form-label">Turno</label>
                            <asp:DropDownList ID="ddl_turno" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" Font-Size="Small" OnSelectedIndexChanged="Carga_Nivel" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_nivel" class="form-label">Nivel</label>
                            <asp:DropDownList ID="ddl_nivel" runat="server" CssClass="form-control" OnSelectedIndexChanged="Carga_Programa" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_programa" class="form-label">Programa</label>
                            <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_periodo_destino" class="form-label">Periodo Destino</label>
                            <asp:DropDownList ID="ddl_periodo_destino" runat="server" CssClass="form-control" Font-Size="Small" ></asp:DropDownList>
                        </div>
                        <div class="col-md-0.4">
                            <label for="ContentPlaceHolder1_ImageButton1" class="form-label">Extracción</label>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Operaciones/go.png" Height="20px" Width="20px"
                                     TOOLTIP="Extracción" OnClick="Procesar_Click" VISIBLE="true" />
                        </div>
                    </div>
                    
                </div>

                <div id="tabla_tgeho">
                    <asp:GridView ID="Gridtgeho" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" >
                        <Columns>
                            <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                            <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                            <asp:BoundField DataField="CLAVE_MATERIA" HeaderText="Clave"/>
                            <asp:BoundField DataField="MATERIA" HeaderText="Materia" />
                            <ASP:TEMPLATEFIELD HEADERTEXT="TOTAL"  ItemStyle-HorizontalAlign="Right">
                                    <ITEMTEMPLATE >
                                        <ASP:TEXTBOX ID="total" RUNAT="server" width="80px" 
                                            BACKCOLOR="LightBlue"  BorderStyle="None" 
                                            style="text-align:left"/>    
                                    </ITEMTEMPLATE>
                            </ASP:TEMPLATEFIELD>
                            <asp:BoundField DataField="PROGRAMAS" HeaderText="Programas"/>
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>

                <div id="tabla_tgrup">
                    <asp:GridView ID="GridTgrup" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" Visible="false">
                        <Columns>
                            <asp:BoundField DataField="PERIODO" HeaderText="Periodo" />
                            <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                            <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />
                            <asp:BoundField DataField="CLAVE_MATERIA" HeaderText="Clave"/>
                            <asp:BoundField DataField="MATERIA" HeaderText="Materia" />
                            <asp:BoundField DataField="PRONOSTICO" HeaderText="Pronóstico" />
                            <asp:BoundField DataField="CUPO_MINIMO" HeaderText="Cupo Mínimo" />
                            <asp:BoundField DataField="CUPO_MAXIMO" HeaderText="Cupo Máximo" />
                            <asp:BoundField DataField="PROGRAMAS" HeaderText="Programas"/>
                            <asp:BoundField DataField="GRUPOS" HeaderText="Grupos"/>
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>

                </br>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tgeho" runat="server">
                    <div class="col-md-3" style="text-align: center">
                        <asp:Button ID="cancel_tgeho" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancel_thodo_Click"/>
                        <asp:Button ID="save_tgeho" runat="server" CssClass="btn btn-round btn-success" Text="Generar Proyección" OnClientClick="destroy_table();" OnClick="Agregar_Click"/>
                        <asp:Button ID="update_thora" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClientClick="destroy_table();" OnClick="save_tgeho_Click"/>
                    </div>
                </div>

               <asp:GridView ID="GridInscritos" runat="server" Visible="false">
               </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <script>
        function load_datatable() {
            let table_z = $("#ContentPlaceHolder1_Gridthora").DataTable({
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
                        title: 'SAES_Catálogo de Grupos Materia',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 4, 5,6, 7, 9, 10]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Grupos Materia',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 4, 5,6, 7, 9, 10]
                        }
                    }
                ]
            });
        }

        function load_datatable_tgeho() {
            let table_z = $("#ContentPlaceHolder1_Gridtgeho").DataTable({
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
                        title: 'SAES_Proyección Grupos Materia',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        }
                    },
                    {
                        title: 'SAES_Proyección Grupos Materia',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        }
                    }
                ]
            });
        }

        function load_datatable_tgrup() {
            let table_z = $("#ContentPlaceHolder1_GridTgrup").DataTable({
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
                        title: 'SAES_Generación Grupos Materia',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    },
                    {
                        title: 'SAES_Generación Grupos Materia',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    }
                ]
            });
        }


        function destroy_table() {
            $("#ContentPlaceHolder1_Gridthora").DataTable().destroy();
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>



