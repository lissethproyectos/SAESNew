using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelDocumento;

namespace SAES_Services
{
    public class DocumentoService : Methods
    {
        public string Upd_tcodo(string clave_esta, string clave_pais, string descripcion, string usu_alta, string estatus)
        {
            ModelEditarTcodo Update = new ModelEditarTcodo()
            {
                //testa_clave = clave_esta,
                //testa_tpais_clave = clave_pais,
                //testa_desc = descripcion,
                //testa_tuser_clave = usu_alta,
                //testa_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }
        public string EditarDoctos(string p_matricula, string p_tredo_tpees_clave, string p_tredo_consecutivo, string p_tredo_tdocu_clave,
            string p_tredo_tstdo_clave, string p_tredo_fecha_limite, string p_tredo_fecha_entrega, string p_tredo_user,
            string p_tredo_date, string p_tredo_estatus)
        {
            ModelEditarTredo Update = new ModelEditarTredo()
            {
                matricula = p_matricula,
                tredo_tpees_clave = p_tredo_tpees_clave,
                tredo_consecutivo = p_tredo_consecutivo,
                tredo_tdocu_clave = p_tredo_tdocu_clave,
                tredo_tstdo_clave = p_tredo_tstdo_clave,
                tredo_fecha_limite= p_tredo_fecha_limite,
                tredo_fecha_entrega= p_tredo_fecha_entrega,
                tredo_user= p_tredo_user,
                tredo_date= p_tredo_date,
                tredo_estatus= p_tredo_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }


        public DataTable obtenEstatusDoctos()
        {
            ModelEstatusDoctoRequest request = new ModelEstatusDoctoRequest() {  };
            List<ModelEstatusDoctoResponse> response = DB.CallSPListResult<ModelEstatusDoctoResponse, ModelEstatusDoctoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenConfiguracionDoctos(string p_tcodo_tdocu_clave, string p_tcodo_tmoda_clave, string p_tcodo_tprog_clave, 
            string p_tcodo_ttiin_clave, string p_tcodo_tnive_clave, string p_tcodo_tcamp_clave, string p_tcodo_tcole_clave)
        {
            ModelTcodoRequest request = new ModelTcodoRequest() {
                tcodo_tdocu_clave = p_tcodo_tdocu_clave,
                tcodo_tmoda_clave = p_tcodo_tmoda_clave,
                tcodo_tprog_clave = p_tcodo_tprog_clave,
                tcodo_ttiin_clave = p_tcodo_ttiin_clave,
                tcodo_tnive_clave= p_tcodo_tnive_clave,
                tcodo_tcamp_clave= p_tcodo_tcamp_clave,
                tcodo_tcole_clave= p_tcodo_tcole_clave
            };
            List<ModelTcodoResponse> response = DB.CallSPListResult<ModelTcodoResponse, ModelTcodoRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarTstdoResponse InsertarTstdo(string p_tstdo_clave, string p_tstdo_desc, string p_tstdo_coment, 
            string p_tstdo_user, string p_tstdo_estatus)
        {
            ModelInsertarTstdo Insert = new ModelInsertarTstdo()
            {
                tstdo_clave = p_tstdo_clave,
                tstdo_desc = p_tstdo_desc,
                tstdo_coment = p_tstdo_coment,
                tstdo_user = p_tstdo_user,
                tstdo_estatus= p_tstdo_estatus
            };
            ModelInsertarTstdoResponse response = DB.CallSPResult<ModelInsertarTstdoResponse, ModelInsertarTstdo>(Insert);
            return response;
        }


        public string UpdateTstdo(string p_tstdo_clave, string p_tstdo_desc, string p_tstdo_coment,
           string p_tstdo_user, string p_tstdo_estatus)
        {
            ModelUpdTstdo Update = new ModelUpdTstdo()
            {
                tstdo_clave = p_tstdo_clave,
                tstdo_desc = p_tstdo_desc,
                tstdo_coment = p_tstdo_coment,
                tstdo_user = p_tstdo_user,
                tstdo_estatus = p_tstdo_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }
    }
}
