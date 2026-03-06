using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
   
    [SPName("Insertar_Usuario")]
    public class ModelInsertaUsuario : BaseModelRequest
    {
        
        [SPParameterName("P_User_Cve", 0)]
        public string User_Cve { get; set; }

        [SPParameterName("P_User_Desc", 1)]
        public string User_Desc { get; set; }

        [SPParameterName("P_User_Usuario", 2)]
        public string User_Usuario { get; set; }        

        [SPParameterName("P_User_Estatus", 3)]
        public string User_Estatus { get; set; }

        [SPParameterName("P_User_Role", 4)]
        public string User_Role { get; set; }
    }

    [SPName("Actualiza_Usuario")]
    public class ModelEditarUsuario : BaseModelRequest
    {
        [SPParameterName("P_User_Cve", 0)]
        public string tuser_clave { get; set; }

        [SPParameterName("P_User_Desc", 1)]
        public string tuser_desc { get; set; }
       
        [SPParameterName("P_User_Estatus", 2)]
        public string tuser_estatus { get; set; }
    }

    [SPName("ActualizaPassword")]
    public class ModelEditarPass : BaseModelRequest
    {
        [SPParameterName("P_User_Cve", 0)]
        public string tuser_clave { get; set; }

        [SPParameterName("P_Password", 1)]
        public string tuser_pass { get; set; }
    }

    [SPName("Eliminar_Usuario")]
    public class ModelEliminarUsuario : BaseModelRequest
    {
        [SPParameterName("P_User_Cve", 0)]
        public string tuser_clave { get; set; }
    }

    [SPName("Actualiza_Password")]
    public class ModelActPass : BaseModelRequest
    {
        [SPParameterName("P_CVE", 0)]
        public string tuser_clave { get; set; }
        [SPParameterName("P_USER_PASS", 1)]
        public string tuser_pass { get; set; }
    }
    [SPName("Insertar_Pregunta")]
    public class ModelInsPregunta : BaseModelRequest
    {
        [SPParameterName("P_TPRSE_TUSER_CLAVE", 0)]
        public string tprse_tuser_clave { get; set; }

        [SPParameterName("P_TPRSE_TPREG_CLAVE", 1)]
        public string tprse_tpreg_clave { get; set; }

        [SPParameterName("P_TPRSE_RESPUESTA", 2)]
        public string tprse_respuesta { get; set; }
    }
    [SPName("Valida_Pregunta")]
    public class ModelValidaPregunta : BaseModelRequest
    {
        [SPParameterName("P_USUARIO", 0)]
        public string tuser_clave { get; set; }
        [SPParameterName("P_CVE_PREG", 1)]
        public string tprse_tpreg_clave { get; set; }
        [SPParameterName("P_RESPUESTA_PREG", 2)]
        public string tprse_respuesta { get; set; }
    }
    public class ModelObtenerValidaPregunta : BaseModelResponse
    {
        [SPResponseColumnName("P_VALIDA")]
        public string p_valida { get; set; }
    }

    public class ModelRol : BaseModelRequest
    {
        public string IDRol { get; set; }
        public string Nombre { get; set; }
    }
    public class ModelUsuario : BaseModelRequest
    {
        [Required]
        public string tuser_usuario { get; set; }
        public string tuser_estatus { get; set; }
        public string tuser_clave { get; set; }
        public string tuser_date { get; set; }
        public string tuser_desc { get; set; }
        public string tuser_role { get; set; }
        public string valido { get; set; }

    }

    [SPName("ObtenerListaUsuarios")]
    public class ModelObtenerUsuariosRequest : BaseModelRequest
    {
        [SPParameterName("P_Tipo_Usuario", 0)]
        public string P_User_Tipo { get; set; }
    }

    
     [SPName("Valida_Usuario")]
    public class ModelValidaUsuario : BaseModelRequest
    {
        [SPParameterName("P_USUARIO", 0)]
        public string tuser_clave { get; set; }
        [SPParameterName("P_PASS", 1)]
        public string tuser_pass { get; set; }
    }
    public class ModelObtenerValidaUsuario : BaseModelResponse
    {
        [SPResponseColumnName("P_VALIDA")]
        public string p_valida { get; set; }
        [SPResponseColumnName("TUSER_DESC")]
        public string tuser_desc { get; set; }
        [SPResponseColumnName("TROLE_DESC")]
        public string trole_desc { get; set; }
    }
    public class ModelObtenerUsuariosResponse : BaseModelResponse
    {
        [SPResponseColumnName("TUSER_CLAVE")]
        public string tuser_clave { get; set; }

        [SPResponseColumnName("TUSER_DESC")]
        public string tuser_desc { get; set; }

        [SPResponseColumnName("TUSER_ESTATUS")]
        public string tuser_estatus { get; set; }

        [SPResponseColumnName("TUSER_DATE")]
        public string tuser_date { get; set; }     

    }

    [SPName("P_QRY_PERMISOS_REPOSITORIO")]
    public class ModelObtenerPermisoRepositorioRequest : BaseModelRequest
    {
        [SPParameterName("P_Rol", 0)]
        public string P_User_Rol { get; set; }

    }
    public class ModelObtenerPermisoRepositorioResponse : BaseModelResponse
    {
        [SPResponseColumnName("IDPrivilegio")]
        public string IDPrivilegio { get; set; }

        [SPResponseColumnName("Permiso")]
        public string Permiso { get; set; }

        [SPResponseColumnName("Nombre")]
        public string Nombre { get; set; }
    }



}
