using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelCalendarioEscolar;

namespace SAES_Services
{
    public class CalendarioEscolarService : Methods
    {
        public DataTable ObtenerTcoca()
        {
            ModelObtenTcocaRequest request = new ModelObtenTcocaRequest() { };
            List<ModelObtenTcocaResponse> response = DB.CallSPListResult<ModelObtenTcocaResponse, ModelObtenTcocaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerCalendario(string p_tpees_clave, string p_tcaes_tcamp_clave, string p_tcaes_tnive_clave)
        {
            ModelObtenCalendarioRequest request = new ModelObtenCalendarioRequest() 
            { 
                tpees_clave= p_tpees_clave, 
                tcaes_tcamp_clave=p_tcaes_tcamp_clave, 
                tcaes_tnive_clave= p_tcaes_tnive_clave };
            List<ModelObtenCalendarioResponse> response = DB.CallSPListResult<ModelObtenCalendarioResponse, ModelObtenCalendarioRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarTcocaResponse InsertarTcoca(string p_tcoca_clave, string p_tcoca_desc, string p_tcoca_tuser_clave, string p_tespr_estatus)
        {
            ModelInsertarTcoca Insert = new ModelInsertarTcoca()
            {
                tcoca_clave = p_tcoca_clave,
                tcoca_desc = p_tcoca_desc,
                tcoca_tuser_clave = p_tcoca_tuser_clave,
                tespr_estatus = p_tespr_estatus
            };
            ModelInsertarTcocaResponse response = DB.CallSPResult<ModelInsertarTcocaResponse, ModelInsertarTcoca>(Insert);
            return response;
        }

        public string InsertarTcaes(string p_tcaes_tpees_clave, string p_tcaes_tcamp_clave, string p_tcaes_tnive_clave, string p_tcaes_tcoca_clave,
            string p_tcaes_inicio, string p_tcaes_fin, string p_tcaes_tuser_clave, string p_tcaes_estatus
            )
        {
            ModelInsertarTcaes Insert = new ModelInsertarTcaes()
            {
                tcaes_tpees_clave = p_tcaes_tpees_clave,
                tcaes_tcamp_clave = p_tcaes_tcamp_clave,
                tcaes_tnive_clave = p_tcaes_tnive_clave,
                tcaes_tcoca_clave = p_tcaes_tcoca_clave,
                tcaes_inicio= p_tcaes_inicio,
                tcaes_fin= p_tcaes_fin,
                tcaes_tuser_clave= p_tcaes_tuser_clave,
                tcaes_estatus= p_tcaes_estatus
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public string EditarTcaes(string p_tcaes_tpees_clave, string p_tcaes_tcamp_clave, string p_tcaes_tnive_clave, string p_tcaes_tcoca_clave,
         string p_tcaes_inicio, string p_tcaes_fin, string p_tcaes_tuser_clave, string p_tcaes_estatus
         )
        {
            ModelEditarTcaes Update = new ModelEditarTcaes()
            {
                tcaes_tpees_clave = p_tcaes_tpees_clave,
                tcaes_tcamp_clave = p_tcaes_tcamp_clave,
                tcaes_tnive_clave = p_tcaes_tnive_clave,
                tcaes_tcoca_clave = p_tcaes_tcoca_clave,
                tcaes_inicio = p_tcaes_inicio,
                tcaes_fin = p_tcaes_fin,
                tcaes_tuser_clave = p_tcaes_tuser_clave,
                tcaes_estatus = p_tcaes_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public string EditarTcoca(string p_tcoca_clave, string p_tcoca_desc, string p_tcoca_tuser_clave, string p_tespr_estatus)
        {
            ModelEditarTcoca Update = new ModelEditarTcoca()
            {
                tcoca_clave = p_tcoca_clave,
                tcoca_desc = p_tcoca_desc,
                tcoca_tuser_clave = p_tcoca_tuser_clave,
                tespr_estatus = p_tespr_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }
    }
}
