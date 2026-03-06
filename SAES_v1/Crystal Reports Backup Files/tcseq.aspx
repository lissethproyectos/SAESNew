<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcseq.aspx.cs" Inherits="SAES_v1.tcseq" %>

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

        function error() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">Error !!!</h2>Los valores deben ser numéricos'
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

        //----Secuencia Valor----//
        function validarValor(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Por favor ingresa un valor');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Secuencia Longitud----//
        function validarLongitud(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Por favor ingresa un valor');
                return false;
            } else {
                validadoForm(idElemento);
            }
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
            <img src="Images/Operaciones/secuencia.png" style="width: 30px;" /><small>Secuencias por Campus</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_programa" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_search_campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="search_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="search_campus_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                </div>
                <div id="table_seq_campus">
                    <asp:GridView ID="GridSequence" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="SEQ" HeaderText="Secuencia" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:TextBox ID="valor" runat="server" CssClass="form-control" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Longitud">
                                <ItemTemplate>
                                    <asp:TextBox ID="longitud" runat="server" CssClass="form-control" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="tcseq_numero" HeaderText="Valor_db">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tcseq_longitud" HeaderText="Longitud_db">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CAMPUS" HeaderText="Campus">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                    <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_seq" runat="server" >
                        <div class="col-md-3" style="text-align: center; margin-top: 15px;">
                            <asp:Button ID="cancelar_seq" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_seq_Click" />
                            <asp:Button ID="guardar_seq" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_seq_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function load_datatable() {
            let table_seq = $("#ContentPlaceHolder1_GridSequence").DataTable({
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
                dom: '<"top"if>rBt<"bottom"lp><"clear">',
                buttons: [
                    {
                        title: 'SAES_Catálogo de Campus-Secuencias',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [6,0,1,4,5]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Campus-Secuencias',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [6, 0, 1, 4, 5]
                        }
                    }
                ],
                stateSave: true
            });
        }
    </script>
</asp:Content>
