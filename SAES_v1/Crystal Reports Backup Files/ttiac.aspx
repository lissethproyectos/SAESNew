<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ttiac.aspx.cs" Inherits="SAES_v1.ttiac" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/gijgo.min.js" type="text/javascript"></script>    
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.6/js/messages/messages.es-es.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    
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
    </style>
    
   <script>

       document.addEventListener("DOMContentLoaded", function (event) {
           
       });

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
                   errorForm(idElemento, 'Ingresar clave');
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
               errorForm(idElemento, 'Ingresar descripción');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       //---- Nombre ----//
       function validarclcert(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar Clave certificación');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       function validarsicert(idEl) {
           const idElemento = idEl;
           let nombre = document.getElementById(idElemento).value;
           if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
               errorForm(idElemento, 'Ingresar Siglas certificación');
               return false;
           } else {
               validadoForm(idElemento);
           }
       }

       //---- Valida Campos Periodo ----//
       function validar_campos_ttiac(e) {
           event.preventDefault(e);
           validarClave('ContentPlaceHolder1_txb_clave',0);
           validarNombre('ContentPlaceHolder1_txb_descripcion');
           validarclcert('ContentPlaceHolder1_txb_claveCert');
           validarsicert('ContentPlaceHolder1_txb_siglasCert');
           return false;
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

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/tcodo.png" style="width: 30px;" /><small>Tipos de acreditación</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tcodo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tcodo" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txb_clave" class="form-label">Clave</label>
                            <asp:TextBox ID="txb_clave" MaxLength="4" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txb_descripcion" class="form-label">Descripción</label>
                            <asp:TextBox ID="txb_descripcion" MaxLength="40" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txb_estatus" class="form-label">Estatus</label>
                            <asp:DropDownList ID="ddl_estatus" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:DropDownList>
                        </div>
                    </div>   
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txb_estatus" class="form-label">Clave Certificación</label>
                            <asp:TextBox ID="txb_claveCert" MaxLength="2" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ContentPlaceHolder1_txb_estatus" class="form-label">Siglas Certif.</label>
                            <asp:TextBox ID="txb_siglasCert" MaxLength="10" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcodo" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-success" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-round btn-success" Text="Guardar" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" OnClick="btn_update_Click" />
                    </div>
                </div>

                <div id="table_ttiac">
                    <div style="margin-top: 15px;">
                        <asp:GridView ID="Gridttiac" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small" OnSelectedIndexChanged="Gridttiac_SelectedIndexChanged">
                            <Columns>
                                <asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="ClaveCert" HeaderText="Clave Cert." />
                                <asp:BoundField DataField="SiglasCert" HeaderText="Siglas Cert." />
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" />                                
                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro"  DataFormatString="{0:dd/MM/yyyy}"/>
                            </Columns>
                            <SelectedRowStyle CssClass="selected_table" />
                            <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                        </asp:GridView>
                    </div>  
                    <asp:HiddenField ID="hdf_claveOld" runat="server" Value="" />
                </div>                

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
 
</asp:Content>
