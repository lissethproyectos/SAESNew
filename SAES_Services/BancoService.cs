using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelBanco;
using static SAES_DBO.Models.ModelCobranza;

namespace SAES_Services
{
    public class BancoService:Methods
    {
        public ModelInsertarTcobaResponse InsertarTcoba(string p_tcoba_tbanc_clave, string p_tcoba_ind, string p_tcoba_tcoco_clave, 
            string p_tcoba_tpers_inicio,
          string p_tcoba_tpers_fin,   string p_tcoba_tran_inicio, string p_tcoba_tran_fin,
          string p_tcoba_fecha_ini,   string p_tcoba_fecha_fin,
          string p_tcoba_imp_inicio,  string p_tcoba_imp_fin,
          string p_tcoba_tuser_clave, string p_tcoba_estatus,
          string p_tcoba_referencia
          )
        {
            ModelInsertarTcoba Insert = new ModelInsertarTcoba()
            {
                tcoba_tbanc_clave = p_tcoba_tbanc_clave,
                tcoba_ind = p_tcoba_ind,
                tcoba_tcoco_clave = p_tcoba_tcoco_clave,
                tcoba_tpers_inicio = p_tcoba_tpers_inicio,
                tcoba_tpers_fin = (p_tcoba_tpers_fin=="")?"0": p_tcoba_tpers_fin,
                tcoba_tran_inicio = p_tcoba_tran_inicio,
                tcoba_tran_fin = (p_tcoba_tran_fin == "") ? "0" : p_tcoba_tran_fin,
                tcoba_fecha_ini = p_tcoba_fecha_ini,
                tcoba_fecha_fin = (p_tcoba_fecha_fin == "") ? "0" : p_tcoba_fecha_fin,
                tcoba_imp_inicio = p_tcoba_imp_inicio,
                tcoba_imp_fin = (p_tcoba_imp_fin == "") ? "0" : p_tcoba_imp_fin,
                tcoba_tuser_clave = p_tcoba_tuser_clave,
                tcoba_estatus = p_tcoba_estatus,
                tcoba_referencia = p_tcoba_referencia
            };
            ModelInsertarTcobaResponse response = DB.CallSPResult<ModelInsertarTcobaResponse, ModelInsertarTcoba>(Insert);
            return response;
        }

        public string EditarTcoba(string p_tcoba_tbanc_clave, string p_tcoba_ind, string p_tcoba_tcoco_clave, string p_tcoba_tpers_inicio, string p_tcoba_tpers_fin, string p_tcoba_tran_inicio, string p_tcoba_tran_fin,
         string p_tcoba_fecha_ini, string p_tcoba_fecha_fin, string p_tcoba_imp_inicio, string p_tcoba_imp_fin, 
         string p_tcoba_tuser_clave, string p_tcoba_estatus, string p_tcoba_referencia
         )
        {
            ModelEditarTcoba Update = new ModelEditarTcoba()
            {
                tcoba_tbanc_clave = p_tcoba_tbanc_clave,
                tcoba_ind = p_tcoba_ind,
                tcoba_tcoco_clave = p_tcoba_tcoco_clave,
                tcoba_tpers_inicio = p_tcoba_tpers_inicio,
                tcoba_tpers_fin = (p_tcoba_tpers_fin == "") ? "0" : p_tcoba_tpers_fin,
                tcoba_tran_inicio = p_tcoba_tran_inicio,
                tcoba_tran_fin = (p_tcoba_tran_fin == "") ? "0" : p_tcoba_tran_fin,
                tcoba_fecha_ini = p_tcoba_fecha_ini,
                tcoba_fecha_fin = (p_tcoba_fecha_fin == "") ? "0" : p_tcoba_fecha_fin,
                tcoba_imp_inicio = p_tcoba_imp_inicio,
                tcoba_imp_fin = (p_tcoba_imp_fin == "") ? "0" : p_tcoba_imp_fin,
                tcoba_tuser_clave = p_tcoba_tuser_clave,
                tcoba_estatus = p_tcoba_estatus,
                tcoba_referencia = p_tcoba_referencia
            };
            return DB.CallSPForInsertUpdate(Update);

        }

        //public string InsertarTapba(string p_tapba_tbanc_clave, string p_tapba_carga_date, string p_tapba_consecutivo, string p_tapba_tpers_num, string p_tapba_referencia, decimal p_tapba_importe, 
        //    string p_tapba_fecha_pago, string p_tapba_estatus, string p_tapba_comentario, string p_tapba_tuser_clave, string p_tapba_tcoba_ind
        //)
        public string InsertarTapba(ModelObtenerLayout datosLayout)
        {
            ModelInstapba Insert = new ModelInstapba()
            {
                tapba_tbanc_clave = datosLayout.tapba_tbanc_clave,
                tapba_carga_date = datosLayout.tapba_carga_date,
                tapba_consecutivo = datosLayout.tapba_consecutivo,
                tapba_tpers_num = datosLayout.tapba_tpers_num,
                tapba_referencia = datosLayout.tapba_referencia,
                tapba_importe = Convert.ToString(datosLayout.tapba_importe) == "0" ? "" : Convert.ToString(datosLayout.tapba_importe),
                tapba_fecha_pago = datosLayout.tapba_fecha_pago,
                tapba_estatus = datosLayout.tapba_estatus,
                tapba_comentario = datosLayout.tapba_observaciones,
                tapba_tuser_clave = datosLayout.tapba_tuser_clave,
                tapba_tcoba_ind= datosLayout.tapba_tcoba_ind
            };
            return DB.CallSPForInsertUpdate(Insert);

        }

        public string EliminarTapba(string p_tapba_tbanc_clave, //string p_tapba_carga_date, 
          string p_tapba_carga_date, string p_tapba_tcoba_ind
      )
        {
            ModelDeltapba Delete = new ModelDeltapba()
            {
                tapba_tbanc_clave = p_tapba_tbanc_clave,
                tapba_carga_date = p_tapba_carga_date,
                tapba_tcoba_ind = p_tapba_tcoba_ind
            };
            return DB.CallSPForInsertUpdate(Delete);

        }

        public string AplicarPagos(string p_tapba_tbanc_clave, string p_tapba_carga_date, string p_tapba_tcoba_ind, string p_usuario)
        {
            ModelAplicarPagos Update = new ModelAplicarPagos()
            {
                tapba_tbanc_clave = p_tapba_tbanc_clave,
                tapba_carga_date = p_tapba_carga_date,
                tapba_tcoba_ind = p_tapba_tcoba_ind,
                usuario = p_usuario
            };
            return DB.CallSPForInsertUpdate(Update);

        }

        public List<ModelObtenerTcobaResponse> ObtenerDatosTcoba()
        {
            ModelObtenerTcobaRequest request = new ModelObtenerTcobaRequest() {};
            List<ModelObtenerTcobaResponse> response = DB.CallSPListResult<ModelObtenerTcobaResponse, ModelObtenerTcobaRequest>(request);
            return response;
        }

        public List<ModelObtenerConfTcobaResponse> ObtenerConfiguracionTcoba(string p_tcoba_tbanc_clave, string p_tcoba_ind)
        {
            ModelObtenerConfTcobaRequest request = new ModelObtenerConfTcobaRequest() { tcoba_tbanc_clave=p_tcoba_tbanc_clave, tcoba_ind=p_tcoba_ind };
            List<ModelObtenerConfTcobaResponse> response = DB.CallSPListResult<ModelObtenerConfTcobaResponse, ModelObtenerConfTcobaRequest>(request);
            return response;
        }

        public DataTable ObtenerDatosTapba(string p_tapba_tbanc_clave, string p_tapba_tcoba_ind, string p_tapba_carga_date)
        {
            ModelObtenerTapbaRequest request = new ModelObtenerTapbaRequest() { tapba_tbanc_clave = p_tapba_tbanc_clave, tapba_tcoba_ind = p_tapba_tcoba_ind, tapba_carga_date= p_tapba_carga_date };
            List<ModelObtenerTapbaResponse> response = DB.CallSPListResult<ModelObtenerTapbaResponse, ModelObtenerTapbaRequest>(request);
            return ToDataTable(response);
        }

        public ModelObtenerLengthMaxLayoutResponse ObtenerLongitudMaximaLayout(string p_tcoba_tbanc_clave, string p_tcoba_ind)
        {
            ModelObtenerLengthMaxLayoutRequest request = new ModelObtenerLengthMaxLayoutRequest() { tcoba_tbanc_clave= p_tcoba_tbanc_clave, tcoba_ind=p_tcoba_ind  };
            ModelObtenerLengthMaxLayoutResponse response = DB.CallSPResult<ModelObtenerLengthMaxLayoutResponse, ModelObtenerLengthMaxLayoutRequest>(request);
            return response;
        }

        public ModelValidaLayoutResponse ValidaLayout(string p_tapba_tbanc_clave, string p_tapba_carga_date, string p_tapba_tcoba_ind)
        {
            ModelValidaLayoutRequest request = new ModelValidaLayoutRequest() { tapba_tbanc_clave = p_tapba_tbanc_clave, tapba_carga_date= p_tapba_carga_date, tapba_tcoba_ind = p_tapba_tcoba_ind };
            ModelValidaLayoutResponse response = DB.CallSPResult<ModelValidaLayoutResponse, ModelValidaLayoutRequest>(request);
            return response;
        }
    }
}
