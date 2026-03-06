<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ttiel.aspx.cs" validateRequest="false" Inherits="SAES_v1.ttiel" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
        
    <style>
        span button {
            margin-bottom: 0px !important;
        }
        .icon_regresa{
            width:100%;
            text-align:center;
            border-color:#FFF !important;
        }
        .icon_regresa:hover{
            background-color:#fff !important;
            color: #26b99a;
        }
        .visiblecancel{
            visibility:hidden;
        }
    </style>
    
   <script>

       document.addEventListener("DOMContentLoaded", function (event) {
           
       });

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

       //---- Clave ----//
       function validarClave(idEl,ind) {
           const idElemento = idEl;
           if (ind == 0) {
               let clave = document.getElementById(idElemento).value;
               if (clave == null || clave.length == 0 || /^\s+$/.test(clave)) {
                   errorForm(idElemento, 'Favor de ingresar clave válida');
                   return false;
               } else {
                   validadoForm(idElemento);
               }
           } else {
               errorForm(idElemento, 'La clave ingresada ya existe');
               return false;
           }

       }
       //---- Nombre ----//
       function validarNombre(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Favor de ingresar descripción válida');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }


       //---- Valida Campos Periodo ----//
       function validar_campos_tbapa_consulta(e) {
           event.preventDefault(e);
           var Periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;
           console.log(Periodo);
           if (Periodo == "")
               errorForm("ContentPlaceHolder1_ddl_periodo", 'Selecciona un periodo');
           else
               validadoForm("ContentPlaceHolder1_ddl_periodo");

           var Campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
           console.log(Campus);
           if (Campus == "")
               errorForm("ContentPlaceHolder1_ddl_campus", 'Selecciona un campus');
           else
               validadoForm("ContentPlaceHolder1_ddl_campus");

           var Nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
           console.log(Nivel);
           if (Nivel == "")
               errorForm("ContentPlaceHolder1_ddl_nivel", 'Selecciona un nivel');
           else
               validadoForm("ContentPlaceHolder1_ddl_nivel");
       }

       function validar_campos_tbapa_insert(e) {
           event.preventDefault(e);
           var Periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;
           if (Periodo == "")
               errorForm("ContentPlaceHolder1_ddl_periodo", 'Selecciona un periodo');
           else
               validadoForm("ContentPlaceHolder1_ddl_periodo");

           var Campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
           if (Campus == "")
               errorForm("ContentPlaceHolder1_ddl_campus", 'Selecciona un campus');
           else
               validadoForm("ContentPlaceHolder1_ddl_campus");

           var Nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
           if (Nivel == "")
               errorForm("ContentPlaceHolder1_ddl_nivel", 'Selecciona un nivel');
           else
               validadoForm("ContentPlaceHolder1_ddl_nivel");

           var porcBaja = document.getElementById("ContentPlaceHolder1_txt_porcentaje").value;
           if (porcBaja == "" || porcBaja == "0")
               errorForm("ContentPlaceHolder1_txt_porcentaje", 'Indica un porcentaje de devolucion de baja');
           else
               validadoForm("ContentPlaceHolder1_txt_porcentaje");

           var FechaInicio = document.getElementById("ContentPlaceHolder1_txt_fecha_l").value;
           if (FechaInicio == "")
               errorForm("ContentPlaceHolder1_txt_fecha_l", 'Elige una fecha de Inicio');
           else
               validadoForm("ContentPlaceHolder1_txt_fecha_l");

           var FechaFin = document.getElementById("ContentPlaceHolder1_txt_fecha_f").value;
           if (FechaFin == "")
               errorForm("ContentPlaceHolder1_txt_fecha_f", 'Elige una fecha de termino');
           else
               validadoForm("ContentPlaceHolder1_txt_fecha_f");
       }

       function checkValue(event) {
           var key = event.which || event.keyCode,
               value = event.target.value,
               n = value + String.fromCharCode(key);
           var isValid = n.match(/^((100(\.0{1,2})?)|(\d{1,2}(\.\d{1,2})?))$/) == null ? false : true;
           if (isValid)
               return true;
           else
               return false;           
       }

        function destroy_table() {
            $("#Gridtbapa").DataTable().destroy();
        }
        function destroy_table() {
            $("#Gridtbapa").DataTable().destroy();
        }
        function remove_class() {
            $("#Gridtbapa").removeClass("selected_table")
       }
       function cleartxt_matricula(val) {
           $('#ContentPlaceHolder1_txtMatricula').val(val);  
       }
   </script>

    <script>
        function load_datatable() {
            $("#ContentPlaceHolder1_GridSolicitudes").DataTable({
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

                ],
                stateSave: true
            });
        }
    </script>

    <script>
        function downloaddoc() {
            var xml = $('#ContentPlaceHolder1_txb_Respuesta').val();
            var element = document.createElement('a');
            element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(xml));
            element.setAttribute('download', "Certificado.txt");

            element.style.display = 'none';
            document.body.appendChild(element);

            element.click();

            document.body.removeChild(element);
        }
    </script>
    <script>
        function alert_warning_Confirm(folio) {
            swal({
                title: "¿Generar nuevo folio?",
                text: "Se generara la titulacion con el siguiente folio: " + folio,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Si, generar folio.',
                cancelButtonText: "No, cancelar."
            }).then(
            function () {
                var element = document.getElementById('ContentPlaceHolder1_btn_generarCertificado');
                element.click();
            },
            function () {
                console.log("Cancel")
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="x_title">
        <h2><img src="Images/Operaciones/tcodo.png" style="width: 30px;" /><small>Titulación Electrónica</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_ttiel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tceel" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda"  VISIBLE="true" OnClick="txb_matricula_OnTextChanged"/>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txb_claveTitulacion" class="form-label">Matrícula</label>
                            <asp:TextBox ID="txb_matricula" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:TextBox>
                            <asp:HiddenField ID ="hfd_numero_persona" runat="server" Value="" />
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_nombre" class="form-label">Nombre(s)</label>
                            <asp:Label ID="lb_nombre" runat="server" CssClass="form-control" ></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_apellidoPaterno" class="form-label">Apellido Paterno</label>
                            <asp:Label ID="lb_apellidoPaterno" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_apellidoMaterno" class="form-label">Apellido Materno</label>
                            <asp:Label ID="lb_apellidoMaterno" runat="server" CssClass="form-control" Enabled="false" />
                            <asp:HiddenField ID="hdf_correo" runat="server" Value="" />
                        </div>
                    </div>   
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_CURP" class="form-label">CURP</label>
                            <asp:Label ID="lb_CURP" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;"></div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div id="table_campus">
                            <asp:GridView ID="GridSolicitudes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridSolicitudes_SelectedIndexChanged" Visible="false">
                                <Columns>
                                    <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                                    <asp:BoundField DataField="CLAVE" HeaderText="Matrícula" />
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                    <asp:BoundField DataField="PATERNO" HeaderText="Apellido Paterno" />
                                    <asp:BoundField DataField="MATERNO" HeaderText="Apellido Materno" />
                                    <asp:BoundField DataField="C_GENERO" HeaderText="C_Genero">
                                        <HeaderStyle CssClass="ocultar" />
                                        <ItemStyle CssClass="ocultar" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GENERO" HeaderText="Genero" />
                                    <asp:BoundField DataField="C_CIVIL" HeaderText="C_Civil">
                                        <HeaderStyle CssClass="ocultar" />
                                        <ItemStyle CssClass="ocultar" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="E_CIVIL" HeaderText="Estado Civil" />
                                    <asp:BoundField DataField="CURP" HeaderText="CURP" />
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha Nacimiento" />
                                    <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />
                                    <asp:BoundField DataField="FECHA_REG" HeaderText="Fecha Registro" />
                                </Columns>
                                <SelectedRowStyle CssClass="selected_table" />
                                <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;"></div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_programa" class="form-label">Programa</label>
                            <asp:DropDownList ID="ddl_programa" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_programa_SelectedIndexChanged"></asp:DropDownList>                            
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;"></div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_campusClave" class="form-label">Campus</label>
                            <asp:Label ID="lb_campusClave" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_abrebCampus" class="form-label">-</label>
                            <asp:Label ID="lb_abrebCampus" runat="server" CssClass="form-control" AutoPostBack="false" Enabled="false"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_campus" class="form-label">-</label>
                            <asp:Label ID="lb_campus" runat="server" CssClass="form-control" AutoPostBack="false" Enabled="false"></asp:Label>
                        </div>
                    </div>
                   <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_claveIncorporante" class="form-label">Clave Incorporante</label>
                            <asp:Label ID="lb_claveIncorporante" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                        <div class="col-md-5">
                            <label for="ContentPlaceHolder1_lb_nombreLegal" class="form-label">Nombre Legal</label>
                            <asp:Label ID="lb_nombreLegal" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_RVOE" class="form-label">RVOE</label>
                            <asp:Label ID="lb_RVOE" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_RVOE" class="form-label">Reconocimiento</label>
                            <asp:DropDownList ID="ddl_reconocimiento" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_fechaExpedicion" class="form-label">Fecha Inicio</label>
                            <asp:Label ID="lb_fechaInicio" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_fechaExpedicion" class="form-label">Fecha Termino</label>
                            <asp:Label ID="lb_fechaTermino" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:Label>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">                        
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txb_fechaExpedicion" class="form-label">Fecha Expedición</label>
                            <asp:TextBox ID="txb_fechaExpedicion" runat="server" CssClass="form-control"></asp:TextBox>
                            <script>
                                function ctrl_fecha_expedicion() {
                                    $("#ContentPlaceHolder1_txb_fechaExpedicion").datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txb_fechaExamenProfecional" class="form-label">Fecha Examen Profecional</label>
                            <asp:TextBox ID="txb_fechaExamenProfecional" runat="server" CssClass="form-control"></asp:TextBox>
                            <script>
                                function ctrl_fecha_examen_profecional() {
                                    $("#ContentPlaceHolder1_txb_fechaExamenProfecional").datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;"></div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_califMinima" class="form-label">Modalidad Titulacion:</label>
                            <asp:DropDownList ID="ddl_modalidadTitulacion" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-11">
                            <label for="ContentPlaceHolder1_lb_califMaxima" class="form-label">Servicio social:</label>
                            <asp:DropDownList ID="ddl_servicioSocial" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;"></div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_califMinima" class="form-label">Instituto Procedencia:</label>
                            <asp:Label ID="lb_institutoProcedencia" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_califMaxima" class="form-label">Estudio Antecedente:</label>
                            <asp:DropDownList ID="ddl_estudioAntecedente" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                        </div>                        
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txb_fechaTerminacion" class="form-label">Fecha Terminación</label>
                            <asp:TextBox ID="txb_fechaTerminacion" runat="server" CssClass="form-control"></asp:TextBox>
                            <script>
                                function ctrl_fecha_terminacion() {
                                    $("#ContentPlaceHolder1_txb_fechaTerminacion").datepicker({
                                        uiLibrary: 'bootstrap4',
                                        locale: 'es-es',
                                        format: 'dd/mm/yyyy'
                                    });
                                }
                            </script>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;"></div>
                    <div class="form-label" style="margin-top: 15px;">
                        <h2>
                            <label class="form-label" style="text-align:left">Funcionario</label>
                        </h2>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_nombreFuncionario" class="form-label">Nombre(s)</label>
                            <asp:Label ID="lb_nombreFuncionario" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                         <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_califMinAprobatoria" class="form-label">Apellido Paterno</label>
                            <asp:Label ID="lb_apellidoPFuncionario" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                         <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_califMinAprobatoria" class="form-label">Apellido Materno</label>
                            <asp:Label ID="lb_apellidoMFuncionario" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                         <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_califMinAprobatoria" class="form-label">CURP</label>
                            <asp:Label ID="lb_CURPFuncionario" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_lb_califMinAprobatoria" class="form-label">Cargo</label>
                            <asp:Label ID="lb_CargoFuncionario" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:Label>
                            <asp:HiddenField ID ="hdf_idCargoFuncionario" runat="server" Value="" />
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;"></div>
                </div>

                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tceel" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-success" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_generarConfirmarCertificado" runat="server" CssClass="btn btn-round btn-success" Text="Generar Certificado" OnClick="btn_confirm_generate_certificado_Click" />
                        <asp:Button ID="btn_generarCertificado" runat="server" CssClass="btn btn-round btn-success visiblecancel" Text="Generar Certificado" OnClick="btn_generate_certificado_Click" />
                        <asp:Button ID="btn_enviarCertificado" runat="server" CssClass="btn btn-round btn-success" Text="Enviar Certificado" OnClick="btn_send_certificado_Click" />

                        <asp:Button ID="btn_ObtenCertificadoActual" runat="server" CssClass="btn btn-round btn-success visiblecancel" Text="ObtenerCertificadoActual" OnClick="btn_obtenCertificadoActual_Click" />
                    </div>
                </div>

                <div class="row" style="text-align: center; margin: auto;" id="Div1" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:TextBox ID="txb_Respuesta" class="form-control" runat="server" Height="193px" TextMode="MultiLine" Width="1064px" ></asp:TextBox>
                        <asp:HiddenField ID ="hdf_IdCampus" runat="server" Value="" />
                        <asp:HiddenField ID ="hdf_Folio" runat="server" Value="" />
                        <asp:HiddenField ID ="hdf_IdEntidadFederativaCampus" runat="server" Value="" />
                        <asp:HiddenField ID ="hdf_EntidadFederativaCampus" runat="server" Value="" />
                        <asp:HiddenField ID ="hdf_IdEntidadFederativa" runat="server" Value="" />
                        <asp:HiddenField ID ="hdf_EntidadFederativa" runat="server" Value="" />
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
 
</asp:Content>
