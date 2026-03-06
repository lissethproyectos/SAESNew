using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SAES_v1.Repositorio
{
    public partial class Configuracion : System.Web.UI.Page
    {
       
        applyWeb.Data.Data objDocumentos = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        applyWeb.Data.Data objAreas = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        applyWeb.Data.Data objSAES = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ///Page Load-PostBack Documentos///
                ScriptManager.RegisterStartupScript(this, this.GetType(), "show_tab", "show_documento();", true);
                form_doc.Attributes.Add("style", "display:none");
                btn_doc.Attributes.Add("style", "display:none");
                CargaListaRoles();
                ///Page Load-PostBack permisos///
                CargaListaCampus();

            }
            ObtenerListaTipoDocumento();
            
        }

        #region Métodos para la pestaña de Catálogos de Documentos
        protected void ObtenerListaTipoDocumento()
        {
            ArrayList arrP = new ArrayList();
            DataSet dsDocumentos = (DataSet)objDocumentos.ExecuteSP("Obtener_ListaTipoDocumento", arrP);
            gvTipoDocumento.DataSource = dsDocumentos;
            gvTipoDocumento.DataBind();
            gvTipoDocumento.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvTipoDocumento.UseAccessibleHeader = true;

        }

        protected void Sincronizar_doc_Click(object sender, EventArgs e)
        {
            insert_update();
            sincroniza_niveles();
            sincroniza_tipo_alumno();
            sincroniza_campus();
            sincroniza_relacion_doc_nivel();
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Sincronización Completada','La sincronización de datos se realizo con éxito', 'success')</script>");

            this.Response.AddHeader("REFRESH", "2;URL=Configuracion.aspx#nav-documentos");
        }

        protected void insert_update()
        {

            string strQuery = "";
            strQuery = "SELECT DISTINCT TDOCU_CLAVE,TDOCU_DESC ,'JPG,JPEG,PDF' FORMATO,'1' FORZOSO, ' ' DESCRIP, '1' MIN, '4000' MAX FROM TDOCU WHERE TDOCU_ESTATUS='A' ORDER BY 1";

            MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            MySqlDataReader data_saes;
            MySqlCommand consulta_saes = new MySqlCommand();
            ConexionMySql_saes.Open();
            consulta_saes.Connection = ConexionMySql_saes;
            consulta_saes.CommandType = CommandType.Text;
            consulta_saes.CommandText = strQuery;

            data_saes = consulta_saes.ExecuteReader();


            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

            ConexionMySql.Open();

            while (data_saes.Read())
            {
                string id_saes = data_saes.GetString(0);
                string Nombre = data_saes.GetString(1);
                string Descripcion = data_saes.GetString(4);
                string Formato = data_saes.GetString(2);
                string forzoso = data_saes.GetString(3);
                string min = data_saes.GetString(5);
                string max = data_saes.GetString(6);

                MySqlCommand cmd = new MySqlCommand("SP_Documentos", ConexionMySql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_saes_in", id_saes);
                cmd.Parameters.AddWithValue("@Nombre_in", Nombre);
                cmd.Parameters.AddWithValue("@Descripcion_in", Descripcion);
                cmd.Parameters.AddWithValue("@TamanoMinimo_in", Convert.ToInt32(min));
                cmd.Parameters.AddWithValue("@TamanoMaximo_in", Convert.ToInt32(max));
                cmd.Parameters.AddWithValue("@Formato_in", Formato);
                //cmd.Parameters.AddWithValue("@Nivel", Nivel);
                //cmd.Parameters.AddWithValue("@tipo_alumno", tipo_alumno);
                //cmd.Parameters.AddWithValue("@residencia", residencia);
                cmd.Parameters.AddWithValue("@forzoso_in", Convert.ToInt32(forzoso));
                cmd.Parameters.AddWithValue("@fecha_in", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
            ConexionMySql.Close();
            ConexionMySql_saes.Close();
        }

        protected void sincroniza_niveles()
        {
            string strQuery = "";
            strQuery = "SELECT DISTINCT TNIVE_CLAVE,TNIVE_DESC FROM TNIVE WHERE TNIVE_ESTATUS='A' ORDER BY 1 ";
            MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            MySqlDataReader data_saes;
            MySqlCommand consulta_saes = new MySqlCommand();
            ConexionMySql_saes.Open();
            consulta_saes.Connection = ConexionMySql_saes;
            consulta_saes.CommandType = CommandType.Text;
            consulta_saes.CommandText = strQuery;

            data_saes = consulta_saes.ExecuteReader();


            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

            ConexionMySql.Open();

            while (data_saes.Read())
            {
                string Codigo = data_saes.GetString(0);
                string Descripcion = data_saes.GetString(1);

                MySqlCommand cmd = new MySqlCommand("SP_sinc_niveles", ConexionMySql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo_in", Codigo);
                cmd.Parameters.AddWithValue("@Descripcion_in", Descripcion);
                cmd.Parameters.AddWithValue("@fecha_in", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
            ConexionMySql.Close();
            ConexionMySql_saes.Close();
        }

        protected void sincroniza_tipo_alumno()
        {
            string strQuery = "";
            strQuery = "SELECT DISTINCT TTIIN_CLAVE,TTIIN_DESC FROM TTIIN WHERE TTIIN_ESTATUS='A' ORDER BY 1";
            MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            MySqlDataReader data_saes;
            MySqlCommand consulta_saes = new MySqlCommand();
            ConexionMySql_saes.Open();
            consulta_saes.Connection = ConexionMySql_saes;
            consulta_saes.CommandType = CommandType.Text;
            consulta_saes.CommandText = strQuery;

            data_saes = consulta_saes.ExecuteReader();


            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

            ConexionMySql.Open();

            while (data_saes.Read())
            {
                string Codigo = data_saes.GetString(0);
                string Descripcion = data_saes.GetString(1);

                MySqlCommand cmd = new MySqlCommand("SP_sinc_tipo_alu", ConexionMySql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo_in", Codigo);
                cmd.Parameters.AddWithValue("@Descripcion_in", Descripcion);
                cmd.Parameters.AddWithValue("@fecha_in", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
            ConexionMySql.Close();
            ConexionMySql_saes.Close();
        }

        protected void sincroniza_campus()
        {
            string strQuery = "";
            strQuery = "SELECT DISTINCT TCAMP_CLAVE,TCAMP_DESC,CONCAT('Calle: ',TCAMP_CALLE,' Colonia: ',TCAMP_COLONIA,' CP: ',TCAMP_TCOPO_CLAVE) DIRECCION FROM TCAMP WHERE TCAMP_ESTATUS='A' ORDER BY 1";
            MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            MySqlDataReader data_saes;
            MySqlCommand consulta_saes = new MySqlCommand();
            ConexionMySql_saes.Open();
            consulta_saes.Connection = ConexionMySql_saes;
            consulta_saes.CommandType = CommandType.Text;
            consulta_saes.CommandText = strQuery;

            data_saes = consulta_saes.ExecuteReader();


            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

            ConexionMySql.Open();

            while (data_saes.Read())
            {
                string Codigo = data_saes.GetString(0);
                string Descripcion = data_saes.GetString(1);
                string Direccion = data_saes.GetString(2);

                MySqlCommand cmd = new MySqlCommand("Insertar_Campus", ConexionMySql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo_in", Codigo);
                cmd.Parameters.AddWithValue("@Nombre_in", Descripcion);
                cmd.Parameters.AddWithValue("@Direccion_in", Direccion);


                cmd.ExecuteNonQuery();
            }
            ConexionMySql.Close();
            ConexionMySql_saes.Close();
        }

        static void sincroniza_relacion_doc_nivel()
        {
            string strQuery = "";
            strQuery = "SELECT DISTINCT 'N' IDPROCEDENCIA,TCODO_TTIIN_CLAVE,TCODO_TMODA_CLAVE,TCODO_TDOCU_CLAVE,TCODO_TNIVE_CLAVE FROM TCODO WHERE TCODO_ESTATUS='A' ORDER BY TCODO_TDOCU_CLAVE";
            MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            MySqlDataReader data_saes;
            MySqlCommand consulta_saes = new MySqlCommand();
            ConexionMySql_saes.Open();
            consulta_saes.Connection = ConexionMySql_saes;
            consulta_saes.CommandType = CommandType.Text;
            consulta_saes.CommandText = strQuery;

            data_saes = consulta_saes.ExecuteReader();


            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

            ConexionMySql.Open();

            while (data_saes.Read())
            {

                string IDProcedencia = data_saes.GetString(0);
                string IDTipoIngreso = data_saes.GetString(1);
                string IDModalidad = data_saes.GetString(2);
                string Id_SAES = data_saes.GetString(3);
                string IDNivel = data_saes.GetString(4);

                string strQueryok = "SELECT IDTipoDocumento FROM TipoDocumento WHERE id_saes='" + Id_SAES + "'";
                MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
                DataSet dssql = new DataSet();
                MySqlCommand commandmysql = new MySqlCommand(strQueryok, ConexionMySql);
                mysqladapter.SelectCommand = commandmysql;
                mysqladapter.Fill(dssql);
                mysqladapter.Dispose();
                commandmysql.Dispose();
                string IDTipoDocumento = dssql.Tables[0].Rows[0][0].ToString();

                string strQueryvalida = "SELECT COUNT(*) FROM TiposDocumento_nivel WHERE  IDTipoDocumento='" + IDTipoDocumento + "' AND IDProcedencia='" + IDProcedencia + "' AND IDTipoIngreso='" + IDTipoIngreso + "' AND IDModalidad='" + IDModalidad + "' AND IDNivel='" + IDNivel + "'";
                MySqlDataAdapter mysqladapter_1 = new MySqlDataAdapter();
                DataSet dssql_1 = new DataSet();
                MySqlCommand commandmysql_1 = new MySqlCommand(strQueryvalida, ConexionMySql);
                mysqladapter_1.SelectCommand = commandmysql_1;
                mysqladapter_1.Fill(dssql_1);
                mysqladapter_1.Dispose();
                commandmysql_1.Dispose();

                if (dssql_1.Tables[0].Rows[0][0].ToString() == "0")
                {
                    string strquery_insert = "INSERT INTO TiposDocumento_nivel VALUES ('" + IDTipoDocumento + "','" + IDProcedencia + "','" + IDTipoIngreso + "','" + IDModalidad + "','" + IDNivel + "',CURRENT_TIMESTAMP())";
                    MySqlCommand myCommandinserta = new MySqlCommand(strquery_insert, ConexionMySql);
                    myCommandinserta.ExecuteNonQuery();
                }




            }
            ConexionMySql.Close();
            ConexionMySql_saes.Close();
        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        protected void actualizar_documento_Click(object sender, EventArgs e)
        {
            if (!valida_campos())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos();", true);
            }
            else
            {
                UpdateTipoDocumento(Convert.ToInt32(IDTipoDocumento.Text), documento.Value, descripcion.Value, tamanomaximo.Value, tamanomaximo.Value, Formato.Value, "1");
            }            
        }

        protected bool valida_campos()
        {
            if (documento.Value == "" || tamanominimo.Value == "" || tamanomaximo.Value == "" || Formato.Value == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UpdateTipoDocumento(int IDTipoDocumento, string Documento, string Descripcion, string TamanoMinimo, string TamanoMaximo, string Formato, string Forzoso)
        {
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
            string query = "UPDATE TipoDocumento SET Nombre = @Documento, Descripcion = @Descripcion, TamanoMinimo = @TamanoMinimo, TamanoMaximo = @TamnoMaximo, Formato = @Formato, Forzoso = @Forzoso WHERE IDTipoDocumento = @IDTipoDocumento";

            MySqlCommand com = new MySqlCommand(query, ConexionMySql);
            com.Parameters.Add("@IDTIpoDocumento", MySqlDbType.Int32).Value = IDTipoDocumento;
            com.Parameters.Add("@Documento", MySqlDbType.VarChar).Value = Documento;
            com.Parameters.Add("@Descripcion", MySqlDbType.VarChar).Value = Descripcion;
            com.Parameters.Add("@TamanoMinimo", MySqlDbType.VarChar).Value = TamanoMinimo;
            com.Parameters.Add("@TamnoMaximo", MySqlDbType.VarChar).Value = TamanoMaximo;
            com.Parameters.Add("@Formato", MySqlDbType.VarChar).Value = Formato;
            com.Parameters.Add("@Forzoso", MySqlDbType.VarChar).Value = Forzoso;
            ConexionMySql.Open();
            try
            {
                com.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
            }
            catch
            {

            }
            finally
            {
                ConexionMySql.Close();
            }
            

        }

        #endregion

        #region Métodos para la pestaña de Permisos

        protected void CargaListaRoles()
        {
            ArrayList arrParam = new ArrayList();
            ddlRol.DataSource = objAreas.ExecuteSP("Obtener_Lista_Roles", arrParam);
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataValueField = "IDRol";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("--- Seleccionar ---", "0"));
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRol.SelectedValue == "0")
            {
                //Permisos_1.Visible = false;
            }
            else
            if (ddlRol.SelectedIndex > 0)
            {

                //this.Response.AddHeader("REFRESH", "2;URL=UsuarioArea.aspx?rol=" + ddlRol.SelectedValue.ToString());
                //Response.Redirect("Permisos.aspx?rol=" + ddlRol.SelectedValue.ToString());

            }

        }

        protected void CargaListaCampus()
        {
            ArrayList arrParametros = new ArrayList();
            DataSet dsCampus = objAreas.ExecuteSP("ObtenerListaCampus", arrParametros);
            lstCampus.DataSource = dsCampus;
            lstCampus.DataTextField = "Nombre";
            lstCampus.DataValueField = "IDCampus";
            lstCampus.DataBind();
        }

        protected void lstCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaListaCampus_Usuarios();            
            CargaListaUsuarios_C();
            //ListUsuariosNivel.Items.Clear();
            //ListUsuarios.Items.Clear();
            //ListNiveles.ClearSelection();
            //ListUsuariosDocumentos.Items.Clear();
            //ListUsuarios_1.Items.Clear();
            //ListDocumentos.ClearSelection();
            lstCampus.Focus();

        }

        protected void CargaListaCampus_Usuarios()
        {

            string IDRol = ddlRol.SelectedItem.Value.ToString();
            string IDCampus = lstCampus.SelectedItem.Value.ToString();
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
            arrParametros.Add(new applyWeb.Data.Parametro("@IDCampus", IDCampus));
            DataSet dsUsuariosCampus = objAreas.ExecuteSP("ObtenerUsuariosCampus", arrParametros);
            lstUsuariosCampus.DataSource = dsUsuariosCampus;
            lstUsuariosCampus.DataTextField = "Nombre";
            lstUsuariosCampus.DataValueField = "IDUsuario";
            lstUsuariosCampus.DataBind();
        }

        protected void CargaListaUsuarios_C()
        {
            string IDRol = ddlRol.SelectedItem.Value.ToString();

            string IDCampus = lstCampus.SelectedItem.Value.ToString();
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(new applyWeb.Data.Parametro("@IDRol_in", IDRol));
            arrParametros.Add(new applyWeb.Data.Parametro("@IDCampus_in", IDCampus));
            DataSet dsUsuario_C = objAreas.ExecuteSP("Obtener_ListaUsuario_C", arrParametros);
            lstUsuarios.DataSource = dsUsuario_C;
            lstUsuarios.DataTextField = "Nombre";
            lstUsuarios.DataValueField = "IDUsuario";
            lstUsuarios.DataBind();
        }

        protected void cmdAgregar_Click(object sender, ImageClickEventArgs e)
        {
            for (int x = 0; x < lstCampus.GetSelectedIndices().Length; x++)
            {
                string IDCampus = lstCampus.Items[lstCampus.GetSelectedIndices()[x]].Value;

                for (int i = 0; i < lstUsuarios.GetSelectedIndices().Length; i++)
                {
                    ArrayList arrParam = new ArrayList();
                    string IDUsuario = lstUsuarios.Items[lstUsuarios.GetSelectedIndices()[i]].Value;
                    string IDRol = ddlRol.SelectedItem.Value.ToString();

                    string user_log = Session["usuario"].ToString();

                    arrParam.Add(new applyWeb.Data.Parametro("@IDCampus_in", IDCampus));
                    arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario_in", IDUsuario));
                    arrParam.Add(new applyWeb.Data.Parametro("@IDRol_in", IDRol));
                    arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                    objAreas.ExecuteInsertSP("Insertar_Permisos_Campus", arrParam);
                }
            }

            CargaListaCampus_Usuarios();
            CargaListaUsuarios_C();
            
        }
        protected void cmdBorrar_Click(object sender, ImageClickEventArgs e)
        {
            for (int x = 0; x < lstCampus.GetSelectedIndices().Length; x++)
            {
                string IDCampus = lstCampus.Items[lstCampus.GetSelectedIndices()[x]].Value;

                for (int i = 0; i < lstUsuariosCampus.GetSelectedIndices().Length; i++)
                {
                    ArrayList arrParam = new ArrayList();
                    string IDUsuario = lstUsuariosCampus.Items[lstUsuariosCampus.GetSelectedIndices()[i]].Value;
                    string IDRol = ddlRol.SelectedItem.Value.ToString();
                    string user_log = Session["usuario"].ToString();

                    arrParam.Add(new applyWeb.Data.Parametro("@IDCampus_in", IDCampus));
                    arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario_in", IDUsuario));
                    arrParam.Add(new applyWeb.Data.Parametro("@IDRol_in", IDRol));
                    arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                    objAreas.ExecuteInsertSP("Eliminar_Permisos_Campus", arrParam);
                }
            }
            CargaListaCampus_Usuarios();
            CargaListaUsuarios_C();
            
        }
        #endregion
    }
}