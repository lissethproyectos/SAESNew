using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelMenu
    {
        [SPName("ObtenerListaMnuOpciones")]
        public class ModelObtenerMenusRequest : BaseModelRequest
        {
            [SPParameterName("P_Menu", 0)]
            public string menu { get; set; }
        }
        public class ModelObtenerMenusResponse : BaseModelResponse
        {
            [SPResponseColumnName("tmenu_desc")]
            public string menu_desc { get; set; }

            [SPResponseColumnName("tmede_rel")]
            public string mede_rel { get; set; }

            [SPResponseColumnName("tmede_clave")]
            public string mede_clave { get; set; }

            [SPResponseColumnName("tmede_desc")]
            public string mede_desc { get; set; }

            [SPResponseColumnName("tmede_estatus")]
            public string mede_estatus { get; set; }

            [SPResponseColumnName("tmede_forma")]
            public string mede_forma { get; set; }

            [SPResponseColumnName("tmede_date")]
            public string mede_date { get; set; }           
        }

        [SPName("Actualiza_Menu_Opciones")]
        public class ModelEditarOpciones : BaseModelRequest
        {
            [SPParameterName("P_Menu", 0)]
            public string tmenu { get; set; }

            [SPParameterName("P_Opcion", 1)]
            public string topcion { get; set; }

            [SPParameterName("P_Descripcion", 2)]
            public string tdescripcion { get; set; }

            [SPParameterName("P_Estatus", 3)]
            public string testatus { get; set; }
            [SPParameterName("P_Relacion", 4)]
            public string trelacion { get; set; }

            [SPParameterName("P_Forma", 5)]
            public string tforma { get; set; }
        }


        [SPName("Insertar_Menu_Opciones")]
        public class ModelInsertarOpciones : BaseModelRequest
        {
            [SPParameterName("P_Menu", 0)]
            public string tmenu { get; set; }

            [SPParameterName("P_Opcion", 1)]
            public string topcion { get; set; }

            [SPParameterName("P_Descripcion", 2)]
            public string tdescripcion { get; set; }

            [SPParameterName("P_Usuario", 3)]
            public string tusuario { get; set; }

            [SPParameterName("P_Estatus", 4)]
            public string testatus { get; set; }

            [SPParameterName("P_Relacion", 5)]
            public string trelacion { get; set; }

            [SPParameterName("P_Forma", 6)]
            public string tforma { get; set; }
        }

        [SPName("Insertar_Menu")]
        public class ModelInsertarMenu : BaseModelRequest
        {
            [SPParameterName("P_Menu_Clave", 0)]
            public int menu_clave { get; set; }

            [SPParameterName("P_menu_desc", 1)]
            public string menu_desc { get; set; }

            [SPParameterName("p_menu_usuario", 2)]
            public string menu_usuario { get; set; }

            [SPParameterName("p_menu_estatus", 3)]
            public string menu_estatus { get; set; }
        }
        [SPName("Actualiza_Menu")]
        public class ModelEditarMenu : BaseModelRequest
        {
            [SPParameterName("P_Menu_Clave", 0)]
            public int menu_clave { get; set; }

            [SPParameterName("P_menu_desc", 1)]
            public string menu_desc { get; set; }

            [SPParameterName("p_menu_estatus", 2)]
            public string menu_estatus { get; set; }
        }

        [SPName("ObtenerListaPermisosForms")]
        public class ModelObtenerPermisosFormsRequest : BaseModelRequest
        {
            [SPParameterName("P_Rol", 0)]
            public int trole { get; set; }

            [SPParameterName("P_Menu", 1)]
            public int tmenu { get; set; }
        }
        public class ModelObtenerPermisosFormsResponse : BaseModelResponse
        {
            [SPResponseColumnName("tmede_clave")]
            public string mede_clave { get; set; }

            [SPResponseColumnName("tmede_desc")]
            public string mede_desc { get; set; }

            [SPResponseColumnName("tusme_update")]
            public string usme_update { get; set; }

            [SPResponseColumnName("tusme_select")]
            public string usme_select { get; set; }
            [SPResponseColumnName("tmede_tmenu_clave")]
            public string mede_tmenu_clave { get; set; }

        }

        [SPName("Insertar_Permisos_Forms")]
        public class ModelAgregarPermisosForm : BaseModelRequest
        {
            [SPResponseColumnName("P_Rol")]
            public string rol { get; set; }

            [SPResponseColumnName("P_Menu")]
            public string menu { get; set; }

            [SPResponseColumnName("P_Clave")]
            public string clave { get; set; }

            [SPResponseColumnName("P_Act")]
            public string act { get; set; }

            [SPResponseColumnName("P_Sel")]
            public string sel { get; set; }

            [SPResponseColumnName("P_User")]
            public string usuario { get; set; }
        }

        [SPName("P_INS_TUSME")]
        public class ModelInsTusmeForm : BaseModelRequest
        {
            [SPParameterName("P_Rol", 0)]
            public string rol { get; set; }

            [SPParameterName("P_Menu", 1)]
            public string menu { get; set; }

            [SPParameterName("P_Clave", 2)]
            public string clave { get; set; }

            [SPParameterName("P_Act", 3)]
            public string act { get; set; }

            [SPParameterName("P_Sel", 4)]
            public string sel { get; set; }

            [SPParameterName("P_User", 5)]
            public string usuario { get; set; }
        }

        [SPName("Eliminar_Permisos_Forms")]
        public class ModelEliminarPermisosForm : BaseModelRequest
        {
            [SPParameterName("P_Rol", 0)]
            public string rol { get; set; }

            [SPParameterName("P_Menu", 1)]
            public string menu { get; set; }

            [SPParameterName("P_Clave", 2)]
            public string clave { get; set; }
        }

        [SPName("ObtenerPermisoFormulario")]
        public class ModelObtenerPermisoFormRequest : BaseModelRequest
        {
            [SPParameterName("P_Usuario", 0)]
            public string usuario { get; set; }

            [SPParameterName("P_Forma", 1)]
            public string forma { get; set; }
        }
        public class ModelObtenerPermisoFormResponse : BaseModelResponse
        {
            [SPResponseColumnName("tusme_update")]
            public string usme_update { get; set; }

            [SPResponseColumnName("tusme_select")]
            public string usme_select { get; set; }
        }
    }
}
