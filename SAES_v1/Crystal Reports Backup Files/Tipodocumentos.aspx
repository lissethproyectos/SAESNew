<%@ Page Title="" Language="C#" MasterPageFile="~/Repositorio/Site1.Master" AutoEventWireup="true" CodeBehind="Tipodocumentos.aspx.cs" Inherits="SAES_v1.Repositorio.Tipodocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Template/Sitemaster/vendors/jquery/dist/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.js" integrity="sha512-n/4gHW3atM3QqRcbCn6ewmpxcLAHGaDjpEBu4xZd47N0W2oQ+6q7oc3PXstrJYXcbNU1OHdQ1T7pAP+gi5Yu8g==" crossorigin="anonymous"></script>
    <style>
        #ContentPlaceHolder1_gvTipoDocumento_info {
            float: left;
        }

        #ContentPlaceHolder1_gvTipoDocumento_length {
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

        .ocultar {
            display: none;
        }

        table.dataTable tbody > tr.selected, table.dataTable tbody > tr > .selected {
            background-color: #88e5cf !important;
        }

        .ddlist {
            left: 725px;
            text-align: center;
        }

        .center_column {
            text-align: center;
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

        .right_col {
            min-height: 700px !important;
        }

        .textoError {
            color: red;
            /*position: absolute;*/
            font-size: .9em;
            font-weight: 700;
            margin-bottom: 1px;
            /*top: 13px;*/
        }

        .listbox {
            margin: auto;
            border-radius: 5px;
        }

        .ul_submenu {
            list-style-type: disc;
            margin-block-start: 1em;
            margin-block-end: 1em;
            margin-inline-start: 0px;
            margin-inline-end: 0px;
            padding-inline-start: 40px;
            margin-top: 0;
            margin-bottom: 0rem;
        }

        .ul_permiso {
            list-style-type: disc;
            margin-block-start: 1em;
            margin-block-end: 1em;
            margin-inline-start: 0px;
            margin-inline-end: 0px;
            padding-inline-start: 80px;
            margin-top: 0;
            margin-bottom: 0rem;
        }

        .table-mod_2 {
            color: #fff;
            width: 10px;
            border-right-style: hidden;
        }

        .ex3 {
            width: 100%;
            height: 300px;
            overflow: auto;
        }

        /* The switch - the box around the slider */
        .switch {
            position: relative;
            display: inline-block;
            width: 35px;
            height: 18px;
        }

            /* Hide default HTML checkbox */
            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        /* The slider */
        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 15px;
                width: 15px;
                left: 1px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #169f85;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #169f85;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(16px);
            -ms-transform: translateX(16px);
            transform: translateX(16px);
        }
        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .ColumnaOculta {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_3 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_4 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_7 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_8 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_9 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_11 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_12 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_13 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_14 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_16 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_17 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_18 {
            display: none;
        }

        #ContentPlaceHolder1_Permisos_App_MenuP_19 {
            display: none;
        }
    </style>
    <script>
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

        //----Prevenir la entrada de letras en el teléfono------//
        function isNumberKey(evt) {
            let charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            } else {
                return true;
            }
        }

        //---- Documento ----//
        function validarDocumento(idEl) {
            const idElemento = idEl;
            let documento = document.getElementById(idElemento).value;
            if (documento == null || documento.length == 0 || /^\s+$/.test(documento)) {
                errorForm(idElemento, 'Ingresa nombre del documento');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Tamaño Mínimo ----//
        function validartamanominimo(idEl) {
            const idElemento = idEl;
            let tamin = document.getElementById(idElemento).value;
            if (tamin == null || tamin.length == 0 || /^\s+$/.test(tamin)) {
                errorForm(idElemento, 'Ingresa tamaño mínimo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Tamaño Máximo ----//
        function validartamanomaximo(idEl) {
            const idElemento = idEl;
            let tamax = document.getElementById(idElemento).value;
            if (tamax == null || tamax.length == 0 || /^\s+$/.test(tamax)) {
                errorForm(idElemento, 'Ingresa tamaño máximo');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }

        //---- Formato ----//
        function validarformato(idEl) {
            const idElemento = idEl;
            let formato = document.getElementById(idElemento).value;
            console.log(formato);
            let er = new RegExp(/^[A-Z,]+$/, 'i');
            //alert(er.test(formato));
            if (formato == null || formato.length == 0 || !(er.test(formato))) {
                errorForm(idElemento, 'Ingresar formato correcto. Si es mas de 1 se deberá separar por una coma (,)');
                return false;
            } else {
                validadoForm(idElemento);
            }
        }
        //------  Función de validar todo el formulario ---------//
        function validar_campos(e) {
            event.preventDefault(e);
            validarDocumento('ContentPlaceHolder1_documento');
            validartamanominimo('ContentPlaceHolder1_tamanominimo');
            validartamanomaximo('ContentPlaceHolder1_tamanomaximo');
            validarformato('ContentPlaceHolder1_Formato');
            update_doc();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="x_title">
        <h2>
            <img src="../Images/Repositorio/proceso.png" style="width: 45px;" /><small>Configuración de Documentos del repositorio</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <div id="sinc_documentos" class="row justify-content-start" style="margin-top: 15px;">
            <div class="col-10">
                <asp:Button ID="sincroniza_documentos" runat="server" CssClass="btn btn-success" Text="Sincronizar Documentos" OnClick="Sincronizar_doc_Click" />
            </div>
        </div>
        <asp:UpdatePanel ID="upd_documento" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_doc" runat="server">
                    <div class="row g-3" style="margin-top: 15px;">
                        <div class="col-md-4 ocultar">
                            <label for="ContentPlaceHolder1_IDTipoDocumento" class="form-label">IDTipoDocumento</label>
                            <asp:TextBox ID="IDTipoDocumento" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_documento" class="form-label">Documento</label>
                            <input type="text" class="form-control" name="documento" placeholder="" id="documento" onblur="validarDocumento('ContentPlaceHolder1_documento')" oninput="validarDocumento('ContentPlaceHolder1_documento')" autocomplete="off" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" />
                        </div>
                        <div class="col-md-8">
                            <label for="ContentPlaceHolder1_descripcion" class="form-label">Descripcion</label>
                            <input type="text" class="form-control" name="descripcion" placeholder="" id="descripcion" autocomplete="off" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_tamanominimo" class="form-label">Tamaño Minimo(KB)</label>
                            <input type="text" class="form-control" name="tamanominimo" inputmode="numeric" placeholder="" maxlength="5" id="tamanominimo" onblur="validartamanominimo('ContentPlaceHolder1_tamanominimo')" oninput="validartamanominimo('ContentPlaceHolder1_tamanominimo')" onkeypress="return isNumberKey(event)" autocomplete="off" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_tamanomaximo" class="form-label">Tamaño Máximo(KB)</label>
                            <input type="text" class="form-control" name="tamanomaximo" inputmode="numeric" placeholder="" maxlength="5" id="tamanomaximo" onblur="validartamanomaximo('ContentPlaceHolder1_tamanomaximo')" oninput="validartamanomaximo('ContentPlaceHolder1_tamanomaximo')" onkeypress="return isNumberKey(event)" autocomplete="off" runat="server" />

                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_Formato" class="form-label">Formato</label>
                            <input type="text" class="form-control" name="Formato" placeholder="" id="Formato" onblur="validarformato('ContentPlaceHolder1_Formato')" oninput="validarformato('ContentPlaceHolder1_Formato')" autocomplete="off" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" />
                        </div>
                    </div>
                    <br />
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_doc" runat="server">
                    <div class="col-md-3" style="text-align: center;">
                        <asp:Button ID="cancelar_documento" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClientClick="cancelar_doc();return false" />
                        <asp:Button ID="actualizar_documento" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="actualizar_documento_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="actualizar_documento" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:GridView ID="gvTipoDocumento" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" Width="100%">
            <Columns>
                <asp:BoundField DataField="IDTipoDocumento" HeaderText="IDTipoDocumento">
                    <HeaderStyle CssClass="ocultar" />
                    <ItemStyle CssClass="ocultar" />
                </asp:BoundField>
                <asp:BoundField DataField="Nombre" HeaderText="Documento" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="tamanominimo" HeaderText="Tamaño Mínimo (KB)" />
                <asp:BoundField DataField="tamanomaximo" HeaderText="Tamaño Máximo (KB)" />
                <asp:BoundField DataField="Formato" HeaderText="Formato" />
                <asp:BoundField DataField="Forzoso" HeaderText="Forzoso">
                    <HeaderStyle CssClass="ocultar" />
                    <ItemStyle CssClass="ocultar" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
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
    <!--Funciones pestaña documentos-->
    <script>
        $(document).ready(function () {

            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
            });

            let table_doc = $("#ContentPlaceHolder1_gvTipoDocumento").DataTable({
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
                        title: 'SAES_Catálogo de Documentos-Repositorio',
                        className: 'btn-dark',
                        extend: 'excel',
                        text: 'Exportar Excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5]
                        }
                    },
                    {
                        title: 'SAES_Catálogo de Documentos-Repositorio',
                        className: 'btn-dark',
                        extend: 'pdfHtml5',
                        text: 'Exportar PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5]
                        }
                    }
                ]
            });

            $('#ContentPlaceHolder1_gvTipoDocumento tbody').on('click', 'tr', function () {
                var data = table_doc.row(this).data();
                var IDTipoDocumento = data[0];
                var documento = data[1];
                var descripcion = data[2];
                var tammin = data[3];
                var tammax = data[4];
                var formato = data[5];

                if ($(this).hasClass("selected")) {
                    $("#ContentPlaceHolder1_form_doc").slideUp("slow");
                    $("#ContentPlaceHolder1_btn_doc").fadeOut("slow");
                } else {
                    $("#ContentPlaceHolder1_IDTipoDocumento").val(IDTipoDocumento);
                    $("#ContentPlaceHolder1_documento").val(documento);
                    $("#ContentPlaceHolder1_descripcion").val(descripcion);
                    $("#ContentPlaceHolder1_tamanominimo").val(tammin);
                    $("#ContentPlaceHolder1_tamanomaximo").val(tammax);
                    $("#ContentPlaceHolder1_Formato").val(formato);
                    $("#ContentPlaceHolder1_form_doc").slideDown("slow");
                    $("#ContentPlaceHolder1_btn_doc").fadeIn("slow");
                    validar_campos();
                }

            });
        });
        function show_documento() {
            $('#nav-documentos').tab('show');
        }
        function cancelar_doc() {
            $("#ContentPlaceHolder1_gvTipoDocumento tbody tr").removeClass("selected");
            $("#ContentPlaceHolder1_IDTipoDocumento").removeClass("focus");
            $("#ContentPlaceHolder1_documento").removeClass("focus");
            $("#ContentPlaceHolder1_tamanominimo").removeClass("focus");
            $("#ContentPlaceHolder1_tamanomaximo").removeClass("focus");
            $("#ContentPlaceHolder1_Formato").removeClass("focus");
            $("#ContentPlaceHolder1_form_doc").slideUp("slow");
            $("#ContentPlaceHolder1_btn_doc").fadeOut("slow");
        }
        function update_doc() {
            $("#ContentPlaceHolder1_form_doc").slideDown("slow");
            $("#ContentPlaceHolder1_btn_doc").fadeIn("slow");
        }
    </script>
    <!--/Funciones pestaña documentos-->
</asp:Content>
