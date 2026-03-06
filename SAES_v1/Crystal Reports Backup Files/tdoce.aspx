<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tdoce.aspx.cs" Inherits="SAES_v1.tdoce" %>
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

          function NoexisteAlumno() {
            swal({
                allowEscapeKey: false,
                allowOutsideClick: false,
                type: 'success',
                html: '<h2 class="swal2-title" id="swal2-title">ERROR !!</h2>Matrícula NO existe'
            })
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

         //---- Matricula ----//
         function validarMatricula(idEl) {
             const idElemento = idEl;
             let nombre = document.getElementById(idElemento).value;
             if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                 errorForm(idElemento, 'Ingresar Clave de Docente');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

          //----Validación de Tipo Direccion----//
         function validar_Categoria(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar Categoria Docente');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         //---- Lada ----//
         function validarCarrera(idEl) {
             const idElemento = idEl;
             let nombre = document.getElementById(idElemento).value;
             if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                 errorForm(idElemento, 'Ingresar Carrera');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

          //----Validación de Estatus Carrera----//
         function validar_estatus(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar estatus Carrera');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         function validarIdioma(idEl) {
             const idElemento = idEl;
             let nombre = document.getElementById(idElemento).value;
             if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                 errorForm(idElemento, 'Ingresar Idioma');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         function validarPorcentajeIdioma(idEl) {
             const idElemento = idEl;
             let nombre = document.getElementById(idElemento).value;
             if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
                 errorForm(idElemento, 'Ingresar % Idioma');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }
       

         //----Validación de Estatus----//
         function validar_estatus(idEl) {
             const idElemento = idEl;
             valor = $("#" + idElemento).val();

             if (valor == 0) {
                 errorForm(idElemento, 'Seleccionar estatus');
                 return false;
             } else {
                 validadoForm(idElemento);
             }
         }

         function validar_valor_entero(e) {
            event.preventDefault(e);
            validarEntero('ContentPlaceHolder1_txt_porcentaje');
            return false;
        }

         function validarEntero(idEl){
        //intento convertir a entero.
        //si era un entero no le afecta, si no lo era lo intenta convertir
        const idElemento = idEl;
        valor = parseInt(idElemento)

        //Compruebo si es un valor numérico
        if (isNaN(valor)) {
            //entonces (no es numero) devuelvo el valor cadena vacia
            errorForm(idElemento, 'Porcentaje es un valor numérico');
            return false
        }else{
            //En caso contrario (Si era un número) devuelvo el valor
            validadoForm(idElemento);
            }
        }

         //---- Valida Campos Docente ----//
         function validar_campos_Docente(e) {
             event.preventDefault(e);
             validarMatricula('ContentPlaceHolder1_txt_matricula');         
             validar_Categoria('ContentPlaceHolder1_ddl_categoria');
             validar_estatus('ContentPlaceHolder1_ddl_estatus');
             return false;
         }

          //---- Valida Campos Carrera ----//
         function validar_campos_Carrera(e) {
             event.preventDefault(e);
             validarMatricula('ContentPlaceHolder1_txt_matricula'); 
             validarMatricula('ContentPlaceHolder1_txt_carrera');         
             validar_Categoria('ContentPlaceHolder1_ddl_estatus_carrera');
             return false;
         }

         //---- Valida Campos Carrera ----//
         function validar_campos_Idioma(e) {
             event.preventDefault(e);
             validarMatricula('ContentPlaceHolder1_txt_matricula'); 
             validarIdioma('ContentPlaceHolder1_txt_idioma');         
             validarPorcentajeIdioma('ContentPlaceHolder1_txt_porcentaje');
             validarEntero('ContentPlaceHolder1_txt_porcentaje');
             return false;
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="x_title">
        <h2>
            <img src="Images/operaciones/academia.png" style="width: 30px;" /><small>Datos Académicos</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_Campus" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_talte" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="ImgConsulta" runat="server" ImageUrl="Images/Operaciones/busca.png" Height="20px" Width="20px"
                                     TOOLTIP="Búsqueda" OnClick="grid_docentes_bind" VISIBLE="true" />
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_matricula" class="form-label">Clave</label>
                            <asp:TextBox ID="txt_matricula"  runat="server" CssClass="form-control" OnTextChanged="txt_matricula_TextChanged" AutoPostBack="true"></asp:TextBox><!--Configurar BackEnd la longitud de la BD-->
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_nombre" class="form-label">Docente</label>
                            <asp:TextBox ID="txt_nombre"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="Img1" runat="server" ImageUrl="Images/Operaciones/add.png" Height="30px" Width="30px"
                                     TOOLTIP="Guardar Registro"  VISIBLE="true" OnClick="Agregar_Click"/>
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_categoria" class="form-label">Categoría Docente</label>
                            <asp:DropDownList ID="ddl_categoria" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>  
                    </div>
                    <div id="table_campus">
                    <asp:GridView ID="GridDocentes" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="GridDocentes_SelectedIndexChanged" Visible="false">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="CLAVE" HeaderText="Matrícula" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                            <asp:BoundField DataField="PATERNO" HeaderText="Apellido Paterno" />
                            <asp:BoundField DataField="MATERNO" HeaderText="Apellido Materno" />
                            <asp:BoundField DataField="PIDM" HeaderText="pidm">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
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
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                         <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Images/Operaciones/carrera_docente.png" Height="20px" Width="20px"
                                       VISIBLE="true" />CARRERAS(S)
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_txt_carrera" class="form-label">Carrera</label>
                            <asp:TextBox ID="txt_carrera"  runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_ddl_estatus_carrera" class="form-label">Estatus Carrera</label>
                            <asp:DropDownList ID="ddl_estatus_carrera" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ContentPlaceHolder1_txt_cedula" class="form-label">Cédula Profesional</label>
                            <asp:TextBox ID="txt_cedula"  runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-0.4">
                            <asp:ImageButton ID="Img2" runat="server" ImageUrl="Images/Operaciones/add.png" Height="30px" Width="30px"
                                     TOOLTIP="Guardar Registro"  VISIBLE="true" OnClick="Agregar_Carrera"/>
                        </div>
                    </div>
                <div id="table_carreras">
                    <asp:GridView ID="GridCarreras" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small"  Visible="false" onselectedindexchanged="GridCatalogo_Carreras">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="Carrera" HeaderText="Carrera" />
                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                            <asp:BoundField DataField="c_estatus" HeaderText="Estatus_code">
                                <HeaderStyle CssClass="ocultar" />
                                <ItemStyle CssClass="ocultar" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cedula" HeaderText="Cédula Profesional" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha Registro" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
                <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                         <div class="col-md-0.4">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="Images/Operaciones/idioma.jpg" Height="30px" Width="30px"
                                       VISIBLE="true" />IDIOMA(S)
                        </div>
                </div>
                <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                    <div class="col-md-4">
                        <label for="ContentPlaceHolder1_txt_idioma" class="form-label">Idioma</label>
                        <asp:TextBox ID="txt_idioma"  runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label for="ContentPlaceHolder1_txt_porcentaje" class="form-label">% Dominio Idioma</label>
                        <asp:TextBox ID="txt_porcentaje"  runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-0.4">
                        <asp:ImageButton ID="Img3" runat="server" ImageUrl="Images/Operaciones/add.png" Height="30px" Width="30px"
                                    TOOLTIP="Guardar Registro"  VISIBLE="true"  OnClick="Agregar_Idioma"/>
                    </div>
                </div>
                
                <div id="table_idiomas">
                    <asp:GridView ID="GridIdiomas" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small"  Visible="false" onselectedindexchanged="GridCatalogo_Idiomas">
                        <Columns>
                            <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" />
                            <asp:BoundField DataField="idioma" HeaderText="Idioma" />
                            <asp:BoundField DataField="porcentaje" HeaderText="% Dominio" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha Registro" />
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
        <script>
        function load_datatable_Carreras() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridCarreras").DataTable({
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

            function load_datatable_Idiomas() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridIdiomas").DataTable({
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

        function load_datatable_Docentes() {
            let table_solicitudes = $("#ContentPlaceHolder1_GridDocentes").DataTable({
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

        function remove_class() {
            $('.selected_table').removeClass("selected_table")
        }
        </script>
</asp:Content>


