<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tpcbe.aspx.cs" Inherits="SAES_v1.tpcbe" %>

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

         //----Vaidar Tipo Descuento----//
        function validartipodescuento(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Tipo descuento');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza Campus----//
        function validarcampus(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar campus');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //----Cobranza nivel----//

        function validarnivel_cob(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar nivel');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Cobranza tipo periodo----//
        function validardesc(idEl,ind) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();
            if (ind == 1) {
                errorForm(idElemento, 'La clave de descuento para el campus,nivel ya existe.');
                return false;
            } else {
                if (valor == 0) {
                    errorForm(idElemento, 'Seleccionar tipo periodo');
                    return false;
                } else {
                    validadoForm(idElemento);
                }
            }
            
        }

         //---- Clave Descuento ----//
        function validarDescuento(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar Descuento');
               return false;
           } else {
               validadoForm(idElemento);
           }
        }
        function validarNomDescuento(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar Nombre Descuento');
               return false;
           } else {
               validadoForm(idElemento);
           }
        }
        //---- Porcentaje ----//
        function validarPorcentaje(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar % descuento');
               return false;
           } else {
               validadoForm(idElemento);
           }
        }

        //---- Monto fijo ----//
        function validarMonto(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar Monto fijo');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       //----Concepto Cargo----//
        function validarconcepto_cargo(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Concepto Cargo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //----Concepto Abono----//
        function validarconcepto_abono(idEl) {
            const idElemento = idEl;
            valor = $("#" + idElemento).val();

            if (valor == 0) {
                errorForm(idElemento, 'Seleccionar Concepto abono');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Valida Campos cobranza ----//
        function validar_campos_descuento(e) {
            event.preventDefault(e);
            validarcampus('ContentPlaceHolder1_ddl_campus');
            validartipodescuento('ContentPlaceHolder1_ddl_tipo');
            validarDescuento('ContentPlaceHolder1_txt_desc');
            validarNomDescuento('ContentPlaceHolder1_txt_descripcion');
            validarPorcentaje('ContentPlaceHolder1_txt_porcentaje');
            validarEntero_descuento('ContentPlaceHolder1_txt_porcentaje');
            validarMonto('ContentPlaceHolder1_txt_monto', 0);
            validarconcepto_cargo('ContentPlaceHolder1_ddl_cargo');
            validarconcepto_abono('ContentPlaceHolder1_ddl_abono');
            return false;
        }

        function validarEntero_descuento(idEl){
        //intento convertir a entero.
        //si era un entero no le afecta, si no lo era lo intenta convertir
        const idElemento = idEl;
        valor = parseInt(idElemento)

        //Compruebo si es un valor numérico
        if (isNaN(valor)) {
            //entonces (no es numero) devuelvo el valor cadena vacia
            errorForm(idElemento, '% Descuento es valor numérico');
            return false
        }else{
            //En caso contrario (Si era un número) devuelvo el valor
            validadoForm(idElemento);
            }
        }

        function validarEntero_monto(idEl){
        //intento convertir a entero.
        //si era un entero no le afecta, si no lo era lo intenta convertir
        const idElemento = idEl;
        valor = parseInt(idElemento)

        //Compruebo si es un valor numérico
        if (isNaN(valor)) {
            //entonces (no es numero) devuelvo el valor cadena vacia
            errorForm(idElemento, 'Monto fijo es valor numérico');
            return false
        }else{
            //En caso contrario (Si era un número) devuelvo el valor
            validadoForm(idElemento);
            }
        }

        function valida_descuento(e) {
           validarEntero_descuento('ContentPlaceHolder1_txt_porcentaje');
        }

        function valida_monto(e) {
           validarEntero_monto('ContentPlaceHolder1_txt_monto');
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
            <img src="Images/Operaciones/cobranza.png" style="width: 30px;" /><small>Planes de cobro / beca</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_cobranza" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="Carga_Pcbe"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_nivel" class="form-label">Nivel Acádemico</label>
                            <asp:DropDownList ID="ddl_nivel" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="Carga_Pcbe" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-1" runat="server">
                            <label for="ContentPlaceHolder1_txt_descuento" class="form-label">Descuento</label>
                            <asp:TextBox ID="txt_desc" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-md-4" id="term_text" runat="server">
                            <label for="ContentPlaceHolder1_txt_descripcion" class="form-label">Descripción</label>
                            <asp:TextBox ID="txt_descripcion" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-md-3" runat="server" >
                            <label for="ContentPlaceHolder1_txt_porcentaje" class="form-label">Porcentaje</label>
                            <asp:TextBox ID="txt_porcentaje" CssClass="form-control"  runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_monto" class="form-label">Monto Fijo</label>
                            <asp:TextBox ID="txt_monto" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_ddl_tipo" class="form-label">Tipo Descuento</label>
                            <asp:DropDownList ID="ddl_tipo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_cargo" class="form-label">Concepto Cargo</label>
                                <asp:DropDownList ID="ddl_cargo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_abono" class="form-label">Concepto Abono</label>
                                <asp:DropDownList ID="ddl_abono" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto; margin-top: 15px;" id="descuento_btn" runat="server">
                    <div class="col-md-5" style="text-align: center;">
                        <asp:Button ID="cancelar_desc" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="cancelar_desc_Click"/>
                        <asp:Button ID="guardar_desc" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="guardar_desc_Click"/>
                        <asp:Button ID="actualizar_desc" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="actualizar_desc_Click"/>
                    </div>
                    <div class="w-100"></div>
                    <div class="col-md-5" style="text-align: center;" runat="server" id="lbl_error" visible="false">
                        <asp:Label ID="lbl_error_text" runat="server" Text="La combinación ingresada ya existe, favor de validar" CssClass="textoError"></asp:Label>
                    </div>
                </div>
                <div id="table_descuentos">
                    <asp:GridView ID="GridDescuentos" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="GridDescuentos_Click">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="C_CAMPUS" HeaderText="C_Campus">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CAMPUS" HeaderText="Campus" />
                            <asp:BoundField DataField="C_NIVEL" HeaderText="C_Nivel">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NIVEL" HeaderText="Nivel" />     
                            <asp:BoundField DataField="DESCUENTO" HeaderText="Clave Descuento"/>
                            <asp:BoundField DataField="NOM_DESCUENTO" HeaderText="Descripción" />
                            <asp:BoundField DataField="PORCENTAJE" HeaderText="Porcentaje" />
                            <asp:BoundField DataField="MONTO" HeaderText="Monto Fijo" />
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo Descuento" />
                            <asp:BoundField DataField="CARGO" HeaderText="Concepto Cargo" />
                            <asp:BoundField DataField="ABONO" HeaderText="Concepto Abono" />
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
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
            let table_programas = $("#ContentPlaceHolder1_GridDescuentos").DataTable({
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
                        title: 'SAES_Planes de Cobro / Becas',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [ 2, 4, 5, 6, 7, 8, 9, 10, 11]
                        }
                    },
                    {
                        title: 'SAES_Planes de Cobro / Becas',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [ 2, 4, 5, 6, 7, 8, 9, 10, 11]
                        }
                    }
                ],
                stateSave: true
            });
        }
        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
    </script>
</asp:Content>


