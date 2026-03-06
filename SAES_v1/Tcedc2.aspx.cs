using applyWeb.Data;
using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCobranza;

namespace SAES_v1
{
    public partial class Tcedc2 : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        CobranzaService serviceCobranza = new CobranzaService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos_grales_Service serviceCatalogo = new Catalogos_grales_Service();
        Catalogos serviceCatalogo2 = new Catalogos();
        BancoService serviceBanco = new BancoService();
        AlumnoService serviceAlumno=new AlumnoService();
        public static double reporte = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string userId = Global.cuenta;  //"U99201091"; // "U99204302"; //
                //string nombre = Global.nombre_alumno;
                //string programa = Global.programa;
                //string nombre_programa = Global.nombre_programa;
                if (Global.cuenta != null)
                {
                    txt_matricula.Text = Global.cuenta;
                    txt_alumno.Text = Global.nombre_alumno;
                    //txt_programa.Text = objSaldo.desc_prog;
                    Carga_Programas();
                    Carga_Periodos();

                    ddl_programa.SelectedValue = Global.programa;
                    ddl_periodo.SelectedValue = Global.periodo;
                    ddl_periodo_SelectedIndexChanged(null, null);
                  
                }
            }
        }
        protected void Carga_Programas()
        {
            List<ModelObtenerProgsAlumnoResponse> lst = new List<ModelObtenerProgsAlumnoResponse>();
            DataTable dt = new DataTable();
           
            try
            {
                lst = serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);

                ddl_programa.DataSource = lst; //
                serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();

                Session["listaNiveles"] = lst;
               
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcedc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void saldos()
        {
            ModelObtenerSaldoAlumnoResponse objSaldo = new ModelObtenerSaldoAlumnoResponse();
            objSaldo = serviceCobranza.ObtenerSaldoAlumno(Global.cuenta, Global.campus, ddl_programa.SelectedValue, ddl_periodo.SelectedValue);
            if (objSaldo != null)
            {
                //txt_matricula.Text = objSaldo.matricula;
                //txt_alumno.Text = objSaldo.nombre;
                //txt_programa.Text = objSaldo.desc_prog;

                lblCargo.Text = objSaldo.cargo;
                lblAbono.Text = objSaldo.abono;
                lblBeca.Text = objSaldo.beca;
                lblSaldo.Text = objSaldo.saldo;
                lblCancelacion.Text = objSaldo.cancelacion;

                //Carga_Periodos();
            }
        }
        protected void grid_tcedc()
        {
            try
            {
                GridTcedc.DataSource = serviceCobranza.ObtenerDatosTcedcEnc(Global.cuenta, Global.campus, ddl_programa.SelectedValue, ddl_periodo.SelectedValue);
                GridTcedc.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcedc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        public String MyNewRow(object customerId)
        {
            /* 
                * 1. Close current cell in our example phone </TD>
                * 2. Close Current Row </TR>
                * 3. Cretae new Row with ID and class <TR id='...' style='...'>
                * 4. Create blank cell <TD></TD>
                * 5. Create new cell to contain the grid <TD>
                * 6. Finall grid will close its own row
                ************************************************************/
            return String.Format(@"</td></tr><tr id ='tr{0}' style='collapsed-row'>
               <td></td><td colspan='100' style='padding:0px; margin:0px;'>", customerId);
        }

        protected void GridTcedc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    decimal total = 0;
                    int consecutivo = Convert.ToInt32(GridTcedc.DataKeys[e.Row.RowIndex].Value.ToString());
                    List<ModelObtenerDatosTcedcDetResponse> lstDatos = new List<ModelObtenerDatosTcedcDetResponse>();
                    List<ModelObtenerDatosTcedcDetResponse> lstDatosAct = new List<ModelObtenerDatosTcedcDetResponse>();

                    GridView GridDet = e.Row.FindControl("GridTcedc_Det") as GridView;
                    DataTable dt = new DataTable();
                    GridDet.DataSource = dt;
                    lstDatos = serviceCobranza.ObtenerDatosTcedcDet(Global.cuenta, Global.campus, Global.programa, consecutivo);

                    foreach (var datos in lstDatos)
                    {
                        if (datos.recibo == "1")
                            datos.tiene_recibo = true;
                        else
                            datos.tiene_recibo = false;

                        lstDatosAct.Add(datos);

                    }


                    //lstDatos= lstDatos.Where(c => c.recibo=="1").ToList().ForEach(w => w.tiene_recibo = true);


                    GridDet.DataSource = lstDatosAct;
                    GridDet.DataBind();

                    if (lstDatos.Count > 0)
                    {
                        Label lblTot = (Label)GridDet.FooterRow.FindControl("lblTotNumPago");
                        //Label lblTot2 = (Label)GridDet.FindControl("lblTotNumPago");//.FooterText = "1";
                        total = lstDatos.Sum(x => Convert.ToDecimal(x.pago));
                        lblTot.Text = Convert.ToString(total);
                    }
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcedc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }



        protected void GridTcedc_Det_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTot = e.Row.FindControl("lblTotNumPago") as Label;
                lblTot.Text = "22222";
                //Label lblTot = (Label)GridTcedc_Det.FooterRow.FindControl("lblTotNumPago");
            }
        }

        protected void linkEdoCta_Click(object sender, EventArgs e)
        {
            if (1 == 1)
            {
                if (txt_matricula.Text != "")
                {
                    string userId = Global.cuenta;  //"U99201091"; // "U99204302"; //
                    string nombre = Global.nombre_alumno;
                    string programa = Global.programa;
                    string nombre_programa = Global.nombre_programa;
                    //Label2.Text = "--";
                    //Label3.Text = "--";
                    //Label4.Text = "--";
                    //Label5.Text = "--";
                    if (IsPostBack == true)
                    {

                        string _open = "window.open('TedcuPDF.aspx', '_self');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

                    }
                }
            }
        }

        protected void linkRegresar_Click(object sender, EventArgs e)
        {
            if (Global.ruta_origen != "")
                Response.Redirect(Global.ruta_origen);
            else
                Response.Redirect("tedcu.aspx");

        }

        protected void Carga_Periodos()
        {            
            ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
            ddl_periodo.DataValueField = "Clave";
            ddl_periodo.DataTextField = "Descripcion";
            ddl_periodo.DataBind();
        }

        protected void GridTcedc_Det_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridView gv = sender as GridView;
                string server = Server.MapPath("");
                string ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepRecibo&Valor1=" + Global.campus + "&Valor2=" + txt_matricula.Text + "&Valor3=" + gv.SelectedRow.Cells[3].Text;
                string _open = "window.open('" + ruta + "', '_black');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcedc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_tcedc();

            saldos();
        }
    }
}