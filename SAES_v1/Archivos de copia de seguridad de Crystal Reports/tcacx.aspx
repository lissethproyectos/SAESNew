<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tcacx.aspx.cs" Inherits="SAES_v1.tcacx" %>

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
    </style>
    
   <script>       
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
   </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="x_title">
        <h2>
            <img src="Images/Operaciones/grade.jpg" style="width: 30px;" /><small>Calificación por Componentes</small></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:UpdatePanel ID="upd_tcacx" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="form_tcacx" runat="server">
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_periodo" class="form-label">Periodo</label>
                            <asp:DropDownList ID="ddl_periodo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_periodo_SelectedIndexChanged"></asp:DropDownList>

                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_campus" class="form-label">Campus</label>
                            <asp:DropDownList ID="ddl_campus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_campus_SelectedIndexChanged"></asp:DropDownList>                            
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-8">
                            <label for="ContentPlaceHolder1_ddl_materia" class="form-label">Materia</label>
                            <asp:DropDownList ID="ddl_materia" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddl_materia_SelectedIndexChanged"></asp:DropDownList>                            
                        </div>
                    </div>
                    <div class="row g-3 justify-content-center" style="margin-top: 15px;">
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_grupo" class="form-label">Grupo</label>
                            <asp:DropDownList ID="ddl_grupo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_grupo_SelectedIndexChanged"></asp:DropDownList>                            
                        </div>
                        <div class="col-md-4">
                            <label for="ContentPlaceHolder1_ddl_componente" class="form-label">Componente</label>
                            <asp:DropDownList ID="ddl_componente" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_componente_SelectedIndexChanged"></asp:DropDownList>                            
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center; margin: auto;" id="btn_tcacx" runat="server">
                    <div class="col-md-4" style="text-align: center; margin-top: 15px;">
                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-round btn-secondary" Text="Cancelar" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-round btn-success" Text="Actualizar" Visible="false" OnClick="btn_update_Click" />                     
                    </div>
                </div>
                <div id="table_tcacx">
                    <asp:GridView ID="Gridtcacx" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" RowStyle-Font-Size="small">
                        <Columns>
                            <%--<asp:ButtonField ButtonType="image" ImageUrl="~/Images/Generales/hacer-clic.png" ControlStyle-Height="24px" ControlStyle-Width="24px" CommandName="select" HeaderText="Seleccionar" ItemStyle-CssClass="button_select" ItemStyle-Width="70px" />--%>
                            <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre"/>
                            <asp:BoundField DataField="Clave" HeaderText="Clave" />    
                            <asp:BoundField DataField="Programa" HeaderText="Programa" />   
                            <asp:TemplateField HeaderText="Calificacion">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_Calificacion" CssClass="form-control" runat="server" ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" /> 
                        </Columns>
                        <SelectedRowStyle CssClass="selected_table" />
                        <HeaderStyle BackColor="#2a3f54" ForeColor="white" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
 
</asp:Content>
