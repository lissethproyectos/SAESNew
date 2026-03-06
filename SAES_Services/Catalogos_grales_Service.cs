using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelCobranza;

namespace SAES_Services
{
    public class Catalogos_grales_Service : Methods
    {
      
        public DataTable QRY_TSTSO()
        {
            ModelTstsoRequest request = new ModelTstsoRequest() {  };
            List<ModelTstsoResponse> response = DB.CallSPListResult<ModelTstsoResponse, ModelTstsoRequest>(request);
            return ToDataTable(response);
        }

    


        public DataTable QRY_TBANC()
        {
            ModeltbancRequest request = new ModeltbancRequest() { };
            List<ModeltbancResponse> response = DB.CallSPListResult<ModeltbancResponse, ModeltbancRequest>(request);
            return ToDataTable(response);
        }

        public DataTable Qry_tbanc_Combo()
        {
            ModeltbancRequest request = new ModeltbancRequest() {};
            List<ModeltbancResponse> response = DB.CallSPListResult<ModeltbancResponse, ModeltbancRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public string Ins_tbanc(string clave_banc, string codigo, string descripcion, string usu_alta, string estatus, string ruta_logo, string convenio)
        {
            ModelInstbanc Insert = new ModelInstbanc()
            {
                tbanc_clave = clave_banc,
                tbanc_tcoco_clave = codigo,
                tbanc_desc = descripcion,
                tbanc_tuser_clave = usu_alta,
                tbanc_estatus = estatus,
                tbanc_ruta_logo=ruta_logo,
                tbanc_convenio=convenio
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public ModelInsTstscResponse Ins_tstsc(string p_tstso_desc, string p_tstso_estatus, string p_tstso_user, string p_tstso_clave)
        {
            ModelInsTstsc Insert = new ModelInsTstsc()
            {
                tstso_desc = p_tstso_desc,
                tstso_estatus = p_tstso_estatus,
                tstso_user = p_tstso_user,
                tstso_clave = p_tstso_clave
            };
            //return DB.CallSPForInsertUpdate(Insert);
            ModelInsTstscResponse response = DB.CallSPResult<ModelInsTstscResponse, ModelInsTstsc>(Insert);
            return response;
        }

        public string Upd_tstsc(string p_tstso_desc, string p_tstso_estatus, string p_tstso_user, string p_tstso_clave)
        {
            ModelUpdTstsc Update = new ModelUpdTstsc()
            {
                tstso_desc = p_tstso_desc,
                tstso_estatus = p_tstso_estatus,
                tstso_user = p_tstso_user,
                tstso_clave = p_tstso_clave
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public string Upd_tbanc(string clave_banc, string descripcion, string codigo, string usu_alta, string estatus, string ruta_logo, string convenio)
        {
            ModelUpdtbanc Update = new ModelUpdtbanc()
            {
                tbanc_clave = clave_banc,
                tbanc_tcoco_clave = codigo,
                tbanc_desc = descripcion,
                tbanc_tuser_clave = usu_alta,
                tbanc_estatus = estatus,
                tbanc_ruta_logo= ruta_logo,
                tbanc_convenio= convenio
            };
            return DB.CallSPForInsertUpdate(Update);
        }


    }

}
