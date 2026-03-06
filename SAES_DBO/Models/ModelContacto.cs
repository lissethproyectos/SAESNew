using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelContacto
    {
        [SPName("P_QRY_TCODI")]
        public class ModelObtenerDireccionesRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerDireccionesResponse : BaseModelResponse
        {
            [SPResponseColumnName("id_num")]
            public string id_num { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("tipo_dir")]
            public string tipo_dir { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("direccion")]
            public string direccion { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("tcodi_tpais_clave")]
            public string tcodi_tpais_clave { get; set; }

            [SPResponseColumnName("tcodi_testa_clave")]
            public string tcodi_testa_clave { get; set; }

            [SPResponseColumnName("tcodi_tdele_clave")]
            public string tcodi_tdele_clave { get; set; }

            [SPResponseColumnName("tcodi_tcopo_clave")]
            public string tcodi_tcopo_clave { get; set; }

            [SPResponseColumnName("tcodi_colonia")]
            public string tcodi_colonia { get; set; }

            [SPResponseColumnName("tcodi_ciudad")]
            public string tcodi_ciudad { get; set; }

            [SPResponseColumnName("cl_contacto")]
            public string contacto { get; set; }

            [SPResponseColumnName("contacto")]
            public string cl_contacto { get; set; }

        }
    }
}
