using SAES_DBA;
using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_Services
{
    public class UsuarioService : Methods
    {
        public List<ModelComun> obtenerRoles()
        {
            DBA objData = new DBA();
            DataSet Cursor = new DataSet();
            List<ModelComun> lstRoles = new List<ModelComun>();
            ModelComun objModelRol = new ModelComun();
            Cursor = objData.ExcecSP("ObtenerListaRoles");


            foreach (DataRow dr in Cursor.Tables[0].Rows)
            {
                objModelRol.IdStr = dr.ItemArray[0].ToString();
                objModelRol.Descripcion = dr.ItemArray[1].ToString();
                lstRoles.Add(objModelRol);


                //objModelRol.IDRol = dr.ItemArray[1].ToString();// Convert.ToString(dr[0].ToString());
            }

            

            

                return lstRoles;
        }        
        public string InsertarUsuario(string clave, string descripcion, string status, string usuario, string role)
        {
            ModelInsertaUsuario Insert = new ModelInsertaUsuario()
            {
                User_Cve = clave,
                User_Desc = descripcion,
                User_Estatus = status,
                User_Usuario=usuario,
                User_Role=role

            };
            return DB.CallSPForInsertUpdate(Insert);
        }
        public string EditarUsuario(string clave, string descripcion, string status)
        {
            ModelEditarUsuario Editar = new ModelEditarUsuario()
            {
                tuser_clave = clave,
                tuser_desc = descripcion,
                tuser_estatus = status
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public string EditarPassword(string clave, string password)
        {
            ModelEditarPass Editar = new ModelEditarPass()
            {
                tuser_clave = clave,
                tuser_pass = password
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public string EliminarUsuario(string clave)
        {
            ModelEliminarUsuario Eliminar = new ModelEliminarUsuario()
            {
                tuser_clave = clave
            };
            return DB.CallSPForInsertUpdate(Eliminar);
        }
        public DataTable ObtenerUsuarios(string tipo_usuario)
        {
            ModelObtenerUsuariosRequest request = new ModelObtenerUsuariosRequest() { P_User_Tipo = tipo_usuario };
            List<ModelObtenerUsuariosResponse> response = DB.CallSPListResult<ModelObtenerUsuariosResponse, ModelObtenerUsuariosRequest>(request);
            return ToDataTable(response);
        }
        public string ModificaPass(string pass, string usuario)
        {
            ModelActPass ActPass = new ModelActPass()
            {
                tuser_clave = usuario,
                tuser_pass = pass
            };
            return DB.CallSPForInsertUpdate(ActPass);
        }      
        public int ValidaPregunta(string usuario, string cve_pregunta, string respuesta)
        {
            ModelValidaPregunta request = new ModelValidaPregunta() { tuser_clave = usuario, tprse_tpreg_clave = cve_pregunta, tprse_respuesta=respuesta };
            ModelObtenerValidaPregunta response = DB.CallSPResult<ModelObtenerValidaPregunta, ModelValidaPregunta>(request);
            return Convert.ToInt32(response.p_valida);
        }
        public string InsertarPregunta(string usuario, string cve_pregunta, string respuesta)
        {
            ModelInsPregunta ActPass = new ModelInsPregunta()
            {
                tprse_tuser_clave = usuario,
                tprse_tpreg_clave = cve_pregunta,
                tprse_respuesta= respuesta

            };
            return DB.CallSPForInsertUpdate(ActPass);
        }
        public ModelUsuario VerificaUsuario(string usuario, string pass)
        {
            ModelUsuario objUsuario = new ModelUsuario();
            ModelValidaUsuario request = new ModelValidaUsuario() { tuser_clave = usuario, tuser_pass = pass };
            ModelObtenerValidaUsuario response = DB.CallSPResult<ModelObtenerValidaUsuario, ModelValidaUsuario>(request);
            objUsuario.valido = response.p_valida;
            objUsuario.tuser_desc = response.tuser_desc;
            objUsuario.tuser_role = response.trole_desc;
            return objUsuario;
        }       
        public DataTable ObtenerPreguntas(string tipo_usuario)
        {
            ModelObtenerUsuariosRequest request = new ModelObtenerUsuariosRequest() { P_User_Tipo = tipo_usuario };
            List<ModelObtenerUsuariosResponse> response = DB.CallSPListResult<ModelObtenerUsuariosResponse, ModelObtenerUsuariosRequest>(request);
            return ToDataTable(response);
        }

        public ModelObtenerPermisoRepositorioResponse UsuarioRepositorio(string usuario_rol)
        {
            ModelObtenerPermisoRepositorioRequest request = new ModelObtenerPermisoRepositorioRequest() { P_User_Rol = usuario_rol };
            ModelObtenerPermisoRepositorioResponse response = DB.CallSPResult<ModelObtenerPermisoRepositorioResponse, ModelObtenerPermisoRepositorioRequest>(request);
            return response;
        }
      


        //public List<ModelObtenerPermisoResponse> ObtenerPermisoForm(string usuario, string formulario)
        //{
        //    ModelObtenerPermisoFormRequest request = new ModelObtenerPermisoFormRequest() { Usuario = usuario, Formulario=formulario };
        //    List<ModelObtenerPermisoResponse> response = DB.CallSPListResult<ModelObtenerPermisoResponse, ModelObtenerPermisoFormRequest>(request);
        //    return response;
        //}

    }
}
