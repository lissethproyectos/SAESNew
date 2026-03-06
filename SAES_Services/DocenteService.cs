using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCalendarioEscolar;
using static SAES_DBO.Models.ModelDocente;

namespace SAES_Services
{
    public class DocenteService : Methods
    {
        public DataTable ObtenerCategorias()
        {
            ModelObtenerCategoriasRequest request = new ModelObtenerCategoriasRequest() { };
            List<ModelObtenerCategoriasResponse> response = DB.CallSPListResult<ModelObtenerCategoriasResponse, ModelObtenerCategoriasRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public List<ModelObtenerCategoriasDocenteResponse> ObtenerCategoriasDocente(string p_clave)
        {
            ModelObtenerCategoriasDocenteRequest request = new ModelObtenerCategoriasDocenteRequest() { Clave = p_clave };
            List<ModelObtenerCategoriasDocenteResponse> response = DB.CallSPListResult<ModelObtenerCategoriasDocenteResponse, ModelObtenerCategoriasDocenteRequest>(request);
            return response;
        }

        public DataTable ObtenerIdiomas(string p_clave)
        {
            ModelObtenerIdiomasRequest request = new ModelObtenerIdiomasRequest() { Clave = p_clave };
            List<ModelObtenerIdiomasResponse> response = DB.CallSPListResult<ModelObtenerIdiomasResponse, ModelObtenerIdiomasRequest>(request);
            return ToDataTable(response);
        }
        public DataTable ObtenerCarreras(string p_matricula)
        {
            ModelObtenerCarrerasRequest request = new ModelObtenerCarrerasRequest() { Matricula = p_matricula };
            List<ModelObtenerCarrerasResponse> response = DB.CallSPListResult<ModelObtenerCarrerasResponse, ModelObtenerCarrerasRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerDireccion(string P_Clave)
        {
            ModelObtenerDireccionRequest request = new ModelObtenerDireccionRequest() { Clave = P_Clave };
            List<ModelObtenerDireccionResponse> response = DB.CallSPListResult<ModelObtenerDireccionResponse, ModelObtenerDireccionRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerTelefono(string P_Matricula)
        {
            ModelObtenerTelefonoRequest request = new ModelObtenerTelefonoRequest() { Matricula = P_Matricula };
            List<ModelObtenerTelefonoResponse> response = DB.CallSPListResult<ModelObtenerTelefonoResponse, ModelObtenerTelefonoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerCorreo(string P_Clave)
        {
            ModelObtenerCorreoRequest request = new ModelObtenerCorreoRequest() { Clave = P_Clave };
            List<ModelObtenerCorreoResponse> response = DB.CallSPListResult<ModelObtenerCorreoResponse, ModelObtenerCorreoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerDisponibilidad(string P_Clave)
        {
            ModelObtenerDisponibilidadRequest request = new ModelObtenerDisponibilidadRequest() { Clave = P_Clave };
            List<ModelObtenerDisponibilidadResponse> response = DB.CallSPListResult<ModelObtenerDisponibilidadResponse, ModelObtenerDisponibilidadRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarDisponibilidadResponse InsertarDisponibilidad(string P_Matricula, string P_Dia, string P_FIni, string P_FFin, 
            string P_Usuario, string P_Status)
        {
            ModelInsertarDisponibilidad Insert = new ModelInsertarDisponibilidad()
            {
                Matricula = P_Matricula,
                Dia = P_Dia,
                FIni = P_FIni,
                FFin = P_FFin,
                Usuario = P_Usuario,
                Status = P_Status,

            };
            ModelInsertarDisponibilidadResponse response = DB.CallSPResult<ModelInsertarDisponibilidadResponse, ModelInsertarDisponibilidad>(Insert);
            return response;
        }

        public string EliminarDisponibilidad(string P_Matricula, string P_Dia, string P_FIni, string P_FFin,
            string P_Usuario, string P_Status)
        {
            ModelEliminarDisponibilidad Borrar = new ModelEliminarDisponibilidad()
            {
                Matricula = P_Matricula,
                Dia = P_Dia,
                FIni = P_FIni,
                FFin = P_FFin,
                Usuario = P_Usuario,
                Status = P_Status,
            };
            return DB.CallSPForInsertUpdate(Borrar);

        }



        public ModelInsertarTelefonoDocenteResponse InsertarTelefono(string p_matricula, string p_talte_consec, string p_talte_ttele_clave,
string p_talte_lada, string p_talte_tel, string p_p_talte_ext, string p_talte_estatus, string p_talte_user)
        {
            ModelInsertarTelefonoDocente Insertar = new ModelInsertarTelefonoDocente()
            {
                matricula = p_matricula,
                talte_consec = p_talte_consec,
                talte_ttele_clave = p_talte_ttele_clave,
                talte_lada = p_talte_lada,
                talte_tel = p_talte_tel,
                talte_ext = p_p_talte_ext,
                talte_estatus = p_talte_estatus,
                talte_user = p_talte_user
            };
            ModelInsertarTelefonoDocenteResponse response = DB.CallSPResult<ModelInsertarTelefonoDocenteResponse, ModelInsertarTelefonoDocente>(Insertar);
            return response;

        }

        public string EditarTelefono(string p_matricula, string p_talte_consec, string p_talte_ttele_clave,
string p_talte_lada, string p_talte_tel, string p_p_talte_ext, string p_talte_estatus, string p_talte_user)
        {
            ModelEditarTelefonoDocente Editar = new ModelEditarTelefonoDocente()
            {
                matricula = p_matricula,
                talte_consec = p_talte_consec,
                talte_ttele_clave = p_talte_ttele_clave,
                talte_lada = p_talte_lada,
                talte_tel = p_talte_tel,
                talte_ext = p_p_talte_ext,
                talte_estatus = p_talte_estatus,
                talte_user = p_talte_user
            };
            return DB.CallSPForInsertUpdate(Editar);

        }

        public string InsertarTaldi(string p_matricula, string p_taldi_tdire_clave, string p_taldi_calle,
     string p_taldi_colonia, string p_taldi_testa_clave, string p_taldi_tdele_clave, string p_taldi_tpais_clave, string p_taldi_tcopo_clave,
     string p_taldi_ciudad, string p_taldi_estatus, string p_taldi_user
     )
        {
            ModelInsertarTaldi Insertar = new ModelInsertarTaldi()
            {
                matricula = p_matricula,
                taldi_tdire_clave = p_taldi_tdire_clave,
                taldi_calle = p_taldi_calle,
                taldi_colonia = p_taldi_colonia,
                taldi_testa_clave = p_taldi_testa_clave,
                taldi_tdele_clave = p_taldi_tdele_clave,
                taldi_tpais_clave = p_taldi_tpais_clave,
                taldi_tcopo_clave = p_taldi_tcopo_clave,
                taldi_ciudad = p_taldi_ciudad,
                taldi_estatus = p_taldi_estatus,
                taldi_user = p_taldi_user
            };
            return DB.CallSPForInsertUpdate(Insertar);

        }

        public string EditarTaldi(string p_matricula, string p_taldi_tdire_clave, string p_taldi_calle,
string p_taldi_colonia, string p_taldi_testa_clave, string p_taldi_tdele_clave, string p_taldi_tpais_clave, string p_taldi_tcopo_clave,
string p_taldi_ciudad, string p_taldi_estatus, string p_taldi_user, string p_taldi_consec
)
        {
            ModelEditarTaldi Insertar = new ModelEditarTaldi()
            {
                matricula = p_matricula,
                taldi_tdire_clave = p_taldi_tdire_clave,
                taldi_calle = p_taldi_calle,
                taldi_colonia = p_taldi_colonia,
                taldi_testa_clave = p_taldi_testa_clave,
                taldi_tdele_clave = p_taldi_tdele_clave,
                taldi_tpais_clave = p_taldi_tpais_clave,
                taldi_tcopo_clave = p_taldi_tcopo_clave,
                taldi_ciudad = p_taldi_ciudad,
                taldi_estatus = p_taldi_estatus,
                taldi_user = p_taldi_user,
                taldi_consec = p_taldi_consec
            };
            return DB.CallSPForInsertUpdate(Insertar);

        }


        public string EditarTe(string p_matricula, string p_taldi_tdire_clave, string p_taldi_calle, string p_taldi_colonia, string p_taldi_testa_clave, string p_taldi_tdele_clave, string p_taldi_tpais_clave, string p_taldi_tcopo_clave,
            string p_taldi_ciudad, string p_taldi_estatus, string p_taldi_user, string p_taldi_consec)
        {
            ModelEditarTaldi Insertar = new ModelEditarTaldi()
            {
                matricula = p_matricula,
                taldi_tdire_clave = p_taldi_tdire_clave,
                taldi_calle = p_taldi_calle,
                taldi_colonia = p_taldi_colonia,
                taldi_testa_clave = p_taldi_testa_clave,
                taldi_tdele_clave = p_taldi_tdele_clave,
                taldi_tpais_clave = p_taldi_tpais_clave,
                taldi_tcopo_clave = p_taldi_tcopo_clave,
                taldi_ciudad = p_taldi_ciudad,
                taldi_estatus = p_taldi_estatus,
                taldi_user = p_taldi_user,
                taldi_consec = p_taldi_consec
            };
            return DB.CallSPForInsertUpdate(Insertar);

        }

        public List<ModelObtenerDocentesResponse> ListaDocentes(string P_Clave)
        {
            ModelObtenerDocentesRequest request = new ModelObtenerDocentesRequest() { Clave = P_Clave };
            List<ModelObtenerDocentesResponse> response = DB.CallSPListResult<ModelObtenerDocentesResponse, ModelObtenerDocentesRequest>(request);
            return response;
        }

        public string InsertarTalco(string p_matricula, string p_talco_tmail_clave,
 string p_talco_correo, string p_talco_preferido, string p_talco_estatus, string p_talco_user)
        {
            ModelInsertarTalco Insertar = new ModelInsertarTalco()
            {
                matricula = p_matricula,
                talco_tmail_clave = p_talco_tmail_clave,
                talco_correo = p_talco_correo,
                talco_preferido = p_talco_preferido,
                talco_estatus = p_talco_estatus,
                talco_user = p_talco_user
            };
            return DB.CallSPForInsertUpdate(Insertar);

        }

        public ModelInsertarTdocaResponse InsertarTdoca(string p_matricula, string p_tdoca_carrera, string p_tdoca_st_carrera, string p_tdoca_ced_carrera, string p_tdoca_user)
        {
            ModelInsertarTdoca Insertar = new ModelInsertarTdoca()
            {
                matricula = p_matricula,
                tdoca_carrera = p_tdoca_carrera,
                tdoca_st_carrera = p_tdoca_st_carrera,
                tdoca_ced_carrera = p_tdoca_ced_carrera,
                tdoca_user = p_tdoca_user
            };
            ModelInsertarTdocaResponse response = DB.CallSPResult<ModelInsertarTdocaResponse, ModelInsertarTdoca>(Insertar);
            return response;
        }

        public string InsertarTdoid(string p_clave, string p_tdoid_idioma, string p_tdoid_porc_idioma, string p_tdoid_user)
        {
            ModelInsertarTdoid Insertar = new ModelInsertarTdoid()
            {
                clave = p_clave,
                tdoid_idioma = p_tdoid_idioma,
                tdoid_porc_idioma = p_tdoid_porc_idioma,
                tdoid_user = p_tdoid_user
            };
            return DB.CallSPForInsertUpdate(Insertar);
        }

        public string EditarTdoce(string p_clave, string p_tdoce_tcado_clave, string p_tdoce_estatus, string p_tdoce_user)
        {
            ModelEditarTdoce Editar = new ModelEditarTdoce()
            {
                clave = p_clave,
                tdoce_tcado_clave = p_tdoce_tcado_clave,
                tdoce_estatus = p_tdoce_estatus,
                tdoce_user = p_tdoce_user
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public string EditarTalco(string p_matricula, string p_talco_consec, string p_talco_tmail_clave,
string p_talco_correo, string p_talco_preferido, string p_talco_estatus, string p_talco_user)
        {
            ModelEditarTalco Editar = new ModelEditarTalco()
            {
                matricula = p_matricula,
                talco_tmail_clave = p_talco_tmail_clave,
                talco_correo = p_talco_correo,
                talco_preferido = p_talco_preferido,
                talco_estatus = p_talco_estatus,
                talco_user = p_talco_user,
                talco_consec = p_talco_consec
            };
            return DB.CallSPForInsertUpdate(Editar);

        }
    }
}
