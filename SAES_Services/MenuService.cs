using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_Services
{
    public class MenuService : Methods
    {
        public DataTable ObtenerMenuOpciones(string menu)
        {
            ModelObtenerMenusRequest request = new ModelObtenerMenusRequest() { menu = menu };
            List<ModelObtenerMenusResponse> response = DB.CallSPListResult<ModelObtenerMenusResponse, ModelObtenerMenusRequest>(request);
            return ToDataTable(response);
        }
        public List<ModelObtenRolesResponse> obtenListMenu()
        {
            ModelObtenRolesRequest request = new ModelObtenRolesRequest() { };
            List<ModelObtenRolesResponse> response = DB.CallSPListResult<ModelObtenRolesResponse, ModelObtenRolesRequest>(request);
            return response;
        }

        public List<ModelObtenMenuResponse> obtenListMenuPrincipal()
        {
            ModelObtenMenuRequest request = new ModelObtenMenuRequest() { };
            List<ModelObtenMenuResponse> response = DB.CallSPListResult<ModelObtenMenuResponse, ModelObtenMenuRequest>(request);
            return response;
        }

        public List<ModelObtenSubMenuResponse> obtenListSubMenu(int P_Nivel,  string P_Menu)
        {
            ModelObtenSubMenuRequest request = new ModelObtenSubMenuRequest() { Nivel= P_Nivel, Menu = P_Menu };
            List<ModelObtenSubMenuResponse> response = DB.CallSPListResult<ModelObtenSubMenuResponse, ModelObtenSubMenuRequest>(request);
            return response;
        }

        public string InsertarMenuOpciones(string menu, string opcion, string descripcion, string usuario, string estatus, string relacion, string forma)
        {
            ModelInsertarOpciones Editar = new ModelInsertarOpciones()
            {
                tmenu = menu,
                topcion = opcion,
                tdescripcion = descripcion,
                tusuario = usuario,
                testatus = estatus,
                trelacion = relacion,
                tforma = forma
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public string EditarMenuOpciones(string menu, string opcion, string descripcion, string estatus, string relacion, string forma)
        {
            ModelEditarOpciones Editar = new ModelEditarOpciones()
            {
                tmenu = menu,
                topcion = opcion,
                tdescripcion = descripcion,
                testatus = estatus,
                trelacion = relacion,
                tforma = forma
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public DataTable ObtenerMenus()
        {
            ModelObtenMenusRequest request = new ModelObtenMenusRequest() {  };
            List<ModelObtenMenusResponse> response = DB.CallSPListResult<ModelObtenMenusResponse, ModelObtenMenusRequest>(request);
            return ToDataTable(response);
        }
        public string InsertarMenu(int menu, string descripcion, string usuario, string estatus)
        {
            ModelInsertarMenu Editar = new ModelInsertarMenu()
            {
                menu_clave = menu,
                menu_desc = descripcion,
                menu_usuario = usuario,
                menu_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public string EditarMenu(int menu, string descripcion, string estatus)
        {
            ModelEditarMenu Editar = new ModelEditarMenu()
            {
                menu_clave = menu,
                menu_desc = descripcion,
                menu_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public DataTable ObtenerPermisosForms(int rol, int menu)
        {
            ModelObtenerPermisosFormsRequest request = new ModelObtenerPermisosFormsRequest() { trole=rol, tmenu = menu };
            List<ModelObtenerPermisosFormsResponse> response = DB.CallSPListResult<ModelObtenerPermisosFormsResponse, ModelObtenerPermisosFormsRequest>(request);
            return ToDataTable(response);
        }
        public List<ModelObtenerPermisosFormsResponse> ObtenerListPermisosForms(int rol, int menu)
        {
            ModelObtenerPermisosFormsRequest request = new ModelObtenerPermisosFormsRequest() { trole = rol, tmenu = menu };
            List<ModelObtenerPermisosFormsResponse> response = DB.CallSPListResult<ModelObtenerPermisosFormsResponse, ModelObtenerPermisosFormsRequest>(request);
            return response;
        }
        public string GuardarPermisosForms(string p_rol, string p_menu, string p_clave, string p_select, string p_update, string p_usuario)
        {
            ModelAgregarPermisosForm Guardar = new ModelAgregarPermisosForm()
            {
                rol = p_rol,
                menu = p_menu,
                clave = p_clave,
                act = p_update,
                sel= p_select,
                usuario=p_usuario
            };
            return DB.CallSPForInsertUpdate(Guardar);
        }
        public string Ins_Tusme(string p_rol, string p_menu, string p_clave, string p_select, string p_update, string p_usuario)
        {
            ModelInsTusmeForm Editar = new ModelInsTusmeForm()
            {
                rol = p_rol,
                menu = p_menu,
                clave = p_clave,
                act = p_update,
                sel = p_select,
                usuario = p_usuario
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public string EliminarPermisosForms(string p_rol, string p_menu, string p_clave)
        {
            ModelEliminarPermisosForm Eliminar = new ModelEliminarPermisosForm()
            {
                rol = p_rol,
                menu = p_menu,
                clave = p_clave
            };
            return DB.CallSPForInsertUpdate(Eliminar);
        }

        public ModelObtenerPermisoFormResponse ObtenerPermisoFormulario(string p_usuario, string p_forma)
        {
            ModelObtenerPermisoFormRequest request = new ModelObtenerPermisoFormRequest() { usuario = p_usuario, forma = p_forma };
            ModelObtenerPermisoFormResponse response = DB.CallSPResult<ModelObtenerPermisoFormResponse, ModelObtenerPermisoFormRequest>(request);
            return response;
        }
    }
}
