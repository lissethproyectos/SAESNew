using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelFinanzas
    {
        [SPName("P_QRY_TREPC")]
        public class ModelDescuentosAlumnoRequest : BaseModelRequest
        {
            [Required]

            [SPParameterName("P_Periodo", 0)]
            public string Periodo { get; set; }

            [SPParameterName("P_Campus", 1)]
            public string Campus { get; set; }

            [SPParameterName("P_Programa", 2)]
            public string Programa { get; set; }

            [SPParameterName("P_Nivel", 3)]
            public string Nivel { get; set; }

            [SPParameterName("P_Concepto", 4)]
            public string Concepto { get; set; }

            [SPParameterName("P_Beca", 5)]
            public string Beca { get; set; }

            [SPParameterName("P_Estatus", 6)]
            public string Estatus { get; set; }
        }
        public class ModelDescuentosAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("tepcb_tpers_num")]
            public string tepcb_tpers_num { get; set; }

            [SPResponseColumnName("tpers_id")]
            public string tpers_id { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("tepcb_tpees_clave")]
            public string tepcb_tpees_clave { get; set; }

            [SPResponseColumnName("tcamp_desc")]
            public string tcamp_desc { get; set; }

            [SPResponseColumnName("tnive_desc")]
            public string tnive_desc { get; set; }

            [SPResponseColumnName("tprog_clave")]
            public string tprog_clave { get; set; }

            [SPResponseColumnName("tcoco_desc")]
            public string tcoco_desc { get; set; }

            [SPResponseColumnName("beca_desc")]
            public string beca_desc { get; set; }

            [SPResponseColumnName("importe")]
            public string importe { get; set; }

            [SPResponseColumnName("tepcb_date")]
            public string tepcb_date { get; set; }

            [SPResponseColumnName("tepcb_tpcbe_clave")]
            public string tepcb_tpcbe_clave { get; set; }

            [SPResponseColumnName("tepcb_estatus")]
            public string tepcb_estatus { get; set; }
        }



        [SPName("P_QRY_CONCEPTOS_PAGADOS")]
        public class ModelConceptosPagadosRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 0)]
            public string Programa { get; set; }
        }
        public class ModelConceptosPagadosResponse : BaseModelResponse
        {
            [SPResponseColumnName("periodo")]
            public string periodo { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("edcu_clave")]
            public string edcu_clave { get; set; }

            [SPResponseColumnName("edcu_desc")]
            public string edcu_desc { get; set; }

            [SPResponseColumnName("pago_clave")]
            public string pago_clave { get; set; }

            [SPResponseColumnName("pago_desc")]
            public string pago_desc { get; set; }

            [SPResponseColumnName("importe")]
            public string importe { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("factura")]
            public string factura { get; set; }

            [SPResponseColumnName("factura_cons")]
            public string factura_cons { get; set; }
        }

        [SPName("P_QRY_CONCEPTOS_CARTERA")]
        public class ModelConceptosCarteraRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }
        }
        public class ModelConceptosCarteraResponse : BaseModelResponse
        {
            [SPResponseColumnName("periodo")]
            public string periodo { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("edcu_clave")]
            public string edcu_clave { get; set; }

            [SPResponseColumnName("edcu_desc")]
            public string edcu_desc { get; set; }

            [SPResponseColumnName("parcialidad")]
            public string parcialidad { get; set; }

            [SPResponseColumnName("importe")]
            public string importe { get; set; }

            [SPResponseColumnName("balance")]
            public string balance { get; set; }

            [SPResponseColumnName("vencimiento")]
            public string vencimiento { get; set; }
        }

        [SPName("P_UPD_DESAPLICAR_PAGO")]//Actualiza Telefono
        public class ModelUpdatePagoAlu : BaseModelRequest
        {

            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }

            [SPParameterName("P_Factura", 3)]
            public string Factura { get; set; }

            [SPParameterName("P_Factura_Cons", 4)]
            public string Factura_Cons { get; set; }

            [SPParameterName("P_Cartera_Cons", 5)]
            public string Cartera_Cons { get; set; }

            [SPParameterName("P_Importe", 6)]
            public string Importe { get; set; }

            [SPParameterName("P_Usuario", 7)]
            public string Usuario { get; set; }

        }

        [SPName("P_UPD_CANCELAR_PAGO")]//Actualiza Telefono
        public class ModelCancelarPagoAlu : BaseModelRequest
        {

            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }

            [SPParameterName("P_Usuario", 3)]
            public string Usuario { get; set; }

            [SPParameterName("P_Consecutivo", 4)]
            public string Consecutivo { get; set; }

            [SPParameterName("P_Balance", 5)]
            public string Balance { get; set; }

            [SPParameterName("P_Concepto_Cargo", 6)]
            public string Concepto_Cargo { get; set; }

        }
    }
}
