using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_Services
{
    public class TiposAcreditacion : Methods
    {
        public TiposAcreditacion() : base() { }

        public DataTable obtenTiposAcreditacion()
        {
            ModelTipoAcreditacionRequest request = new ModelTipoAcreditacionRequest() { Nada = "" };
            List<ModelTipoAcreditacionResponse> response = DB.CallSPListResult<ModelTipoAcreditacionResponse, ModelTipoAcreditacionRequest>(request);
            return ToDataTableForDropDownList(response, false);
        }

        public string InsertTipoAcreditacion(string clave, string descripcion, string usuario, string clavecert, string siglas, string estatus)
        {
            ModeltTipoAcreditacionForInsertRequest req = new ModeltTipoAcreditacionForInsertRequest()
            {
                Clave = clave,
                Descripcion = descripcion,
                Usuario = usuario,
                ClaveCert = clavecert,
                Siglas = siglas,
                Estatus = estatus
            };
            return DB.CallSPForInsertUpdate(req);
        }

        public string UpdateTipoAcreditacion(string claveOld,string clave, string descripcion, string usuario, string clavecert, string siglas, string estatus)
        {
            ModeltTipoAcreditacionForUpdateRequest req = new ModeltTipoAcreditacionForUpdateRequest()
            {
                ClaveOld = claveOld,
                Clave = clave,
                Descripcion = descripcion,
                Usuario = usuario,
                ClaveCert = clavecert,
                Siglas = siglas,
                Estatus = estatus
            };
            return DB.CallSPForInsertUpdate(req);
        }
    }
}
