<%@ Page Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SAES_v1.Repositorio.Configuracion" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                errorForm(idElemento, 'Por favor ingresa el nombre del documento');
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
                errorForm(idElemento, 'Por favor ingresa el tamaño mínimo del documento');
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
                errorForm(idElemento, 'Por favor ingresa el tamaño máximo del documento');
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
                errorForm(idElemento, 'Favor de ingresar un formato correcto. Si es mas de 1 se deberá separar por una coma (,)');
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
            <img src="../Images/Repositorio/proceso.png" style="width: 45px;" /><small>Configuración del repositorio de documentos</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <nav style="margin-top: 10px;">
            <div class="nav nav-tabs justify-content-end pills" id="nav-tab" role="tablist">
                <a class="nav-item nav-link justify-content-end active" id="nav-documentos-tab" data-toggle="tab" href="#nav-documentos" role="tab" aria-controls="nav-documentos" aria-selected="true">Catálogo de Documentos</a>
                <a class="nav-item nav-link justify-content-end" id="nav-permisos-tab" data-toggle="tab" href="#nav-permisos" role="tab" aria-controls="nav-permisos" aria-selected="false">Permisos-Repositorio</a>
            </div>
        </nav>
        <div class="tab-content" id="nav-tabContent">
            <!--Pestaña Configuración de  Documentos-->
            <div class="tab-pane fade" id="nav-documentos" role="tabpanel" aria-labelledby="nav-documentos-tab">
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
            <!--/Pestaña Configuración de  Documentos-->
            <!--Pestaña Configuración de Permisos-->
            <div class="tab-pane fade" id="nav-permisos" role="tabpanel" aria-labelledby="nav-permisos-tab">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                            <div class="col-md-4">
                                <label for="ContentPlaceHolder1_ddlRol" class="form-label">Rol</label>
                                <asp:DropDownList ID="ddlRol" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- start accordion -->
                <div class="accordion" id="accordion" role="tablist" aria-multiselectable="true" style="margin-top: 15px;">
                    <div class="panel">
                        <a class="panel-heading" role="tab" id="headingOne" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                            <h4 class="panel-title">Campus</h4>
                        </a>
                        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row g-3 justify-content-center" style="margin-top: 15px;width:70%;margin:auto;">
                                            <div class="col-md-3 ju">
                                                <asp:ListBox ID="lstCampus" runat="server" Height="150px" Width="200px" SelectionMode="Multiple" CssClass="form-control listbox" OnSelectedIndexChanged="lstCampus_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                                            </div>
                                            <div class="col-md-1" style="text-align:center;margin-top:40px;">
                                                <asp:Image ID="doble" runat="server" ImageUrl="~/Images/Repositorio/Doble.png" Width="40px" Height="40px" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:ListBox ID="lstUsuariosCampus" runat="server" Height="150px" Width="200px" SelectionMode="Multiple" CssClass="form-control listbox"></asp:ListBox>
                                            </div>
                                            <div class="col-md-1" style="text-align: center;margin-top: 40px;">
                                                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/Repositorio/Izq.png"
                                                    Width="25px" OnClick="cmdAgregar_Click" /><br />
                                                <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/Repositorio/Derecha.png"
                                                    Width="25px" OnClick="cmdBorrar_Click" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:ListBox ID="lstUsuarios" runat="server" Height="150px" Width="200px" SelectionMode="Multiple" CssClass="form-control listbox"></asp:ListBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="panel">
                        <a class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            <h4 class="panel-title">Niveles</h4>
                        </a>
                        <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                            <div class="panel-body">
                                <p>
                                    <strong>Collapsible Item 2 data</strong>
                                </p>
                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor,
                            </div>
                        </div>
                    </div>
                    <div class="panel">
                        <a class="panel-heading collapsed" role="tab" id="headingThree" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                            <h4 class="panel-title">Tipos de Documentos</h4>
                        </a>
                        <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                            <div class="panel-body">
                                <p>
                                    <strong>Collapsible Item 3 data</strong>
                                </p>
                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor
                            </div>
                        </div>
                    </div>
                    <div class="panel">
                        <a class="panel-heading collapsed" role="tab" id="headingfour" data-toggle="collapse" data-parent="#accordion" href="#collapsefour" aria-expanded="false" aria-controls="collapsefour">
                            <h4 class="panel-title">Permisos de repositorio</h4>
                        </a>
                        <div id="collapsefour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                            <div class="panel-body">
                                <p>
                                    <strong>Collapsible Item 3 data</strong>
                                </p>
                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end of accordion -->
            </div>
            <!--/Pestaña Configuración de Permisos-->
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
                        text: 'Exportar Excel'
                    },
                    {
                        title: 'SAES_Catálogo de Documentos-Repositorio',
                        className: 'btn-dark',
                        extend: 'pdf',
                        text: 'Exportar PDF'
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

</asp:Content>
