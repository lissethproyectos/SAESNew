using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelCargaAcademica
    {
        [SPName("P_QRY_GRUPOS_MATERIA")]
        public class ModelObtenerDatosGruposMateriaRequest : BaseModelRequest
        {
            [SPParameterName("P_Programa", 0)]
            public string P_Programa { get; set; }

            [SPParameterName("P_Periodo_Programa", 1)]
            public string P_Periodo_Programa { get; set; }

            [SPParameterName("P_Campus", 2)]
            public string P_Campus { get; set; }

            [SPParameterName("P_Periodo", 3)]
            public string P_Periodo { get; set; }

            [SPParameterName("P_Grupo_Turno", 4)]
            public string P_Grupo_Turno { get; set; }

            [SPParameterName("P_Materia", 5)]
            public string P_Materia { get; set; }
        }
        public class ModelObtenerDatoGruposMateriaResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("materia")]
            public string materia { get; set; }

            [SPResponseColumnName("grupo")]
            public string grupo { get; set; }

            [SPResponseColumnName("salon")]
            public string salon { get; set; }

            [SPResponseColumnName("nombre_salon")]
            public string nombre_salon { get; set; }

            [SPResponseColumnName("capacidad")]
            public string capacidad { get; set; }

            [SPResponseColumnName("inscritos")]
            public string inscritos { get; set; }

            [SPResponseColumnName("disponible")]
            public string disponible { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }
        }

        [SPName("P_QRY_TSALO")]
        public class ModelObtenerEscenariosAcadRequest : BaseModelRequest
        {

        }
        public class ModelEscenariosAcadResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("minimo")]
            public string minimo { get; set; }

            [SPResponseColumnName("maximo")]
            public string maximo { get; set; }

            [SPResponseColumnName("c_tipo")]
            public string c_tipo { get; set; }

            [SPResponseColumnName("tipo")]
            public string tipo { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("Estatus")]
            public string Estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }
        }


        [SPName("P_INS_TSALO")]
        public class ModelEscenariosAcademicosRequest : BaseModelRequest
        {

            [SPParameterName("p_tsalo_clave", 0)]
            public string tsalo_clave { get; set; }

            [SPParameterName("p_tsalo_desc", 1)]
            public string tsalo_desc { get; set; }

            [SPParameterName("p_tsalo_minimo", 2)]
            public string tsalo_minimo { get; set; }

            [SPParameterName("p_tsalo_maximo", 3)]
            public string tsalo_maximo { get; set; }

            [SPParameterName("p_tsalo_tipo", 4)]
            public string tsalo_tipo { get; set; }

            [SPParameterName("p_tsalo_tuser_clave", 5)]
            public string tsalo_tuser_clave { get; set; }

            [SPParameterName("p_tsalo_estatus", 6)]
            public string tsalo_estatus { get; set; }
        }
        public class ModelInsertarEscenariosAcademicosResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }           
        }



        [SPName("P_QRY_ESCUELA_PROCEDENCIA")]
        public class ModelObtenerEscuelaProcedenciaRequest : BaseModelRequest
        {
            [SPParameterName("p_tpreq_tprog_clave", 0)]
            public string tpreq_tprog_clave { get; set; }

            [SPParameterName("p_tpreq_tespr_clave", 1)]
            public string tpreq_tespr_clave { get; set; }

            [SPParameterName("p_matricula", 2)]
            public string matricula { get; set; }
        }
        public class ModelObtenerEscuelaProcedenciaResponse : BaseModelResponse
        {
            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("carrera")]
            public string carrera { get; set; }

            [SPResponseColumnName("folio")]
            public string folio { get; set; }

            [SPResponseColumnName("fecha_dict")]
            public string fecha_dict { get; set; }

            [SPResponseColumnName("tpreq_tpees_clave")]
            public string tpreq_tpees_clave { get; set; }

            [SPResponseColumnName("tpees_desc")]
            public string tpees_desc { get; set; }

            [SPResponseColumnName("tpreq_estatus")]
            public string tpreq_estatus { get; set; }
        }


        [SPName("P_INS_TPREQ")]
        public class ModelInsertaTPreq : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string tpreq_tpers_num { get; set; }

            [SPParameterName("p_tpreq_tprog_clave", 1)]
            public string tpreq_tprog_clave { get; set; }

            [SPParameterName("p_tpreq_tespr_clave", 2)]
            public string tpreq_tespr_clave { get; set; }

            [SPParameterName("p_tpreq_carrera", 3)]
            public string tpreq_carrera { get; set; }

            [SPParameterName("p_tpreq_tpees_clave", 4)]
            public string tpreq_tpees_clave { get; set; }

            [SPParameterName("p_tpreq_folio", 5)]
            public string tpreq_folio { get; set; }

            [SPParameterName("p_tpreq_fecha_dict", 6)]
            public string tpreq_fecha_dict { get; set; }

            [SPParameterName("p_tpreq_estatus", 7)]
            public string tpreq_estatus { get; set; }

            [SPParameterName("p_tpreq_user", 8)]
            public string tpreq_user { get; set; }
        }

        [SPName("P_UPD_TPREQ")]
        public class ModelEditaTPreq : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string tpreq_tpers_num { get; set; }

            [SPParameterName("p_tpreq_tprog_clave", 1)]
            public string tpreq_tprog_clave { get; set; }

            [SPParameterName("p_tpreq_tespr_clave", 2)]
            public string tpreq_tespr_clave { get; set; }

            [SPParameterName("p_tpreq_carrera", 3)]
            public string tpreq_carrera { get; set; }

            [SPParameterName("p_tpreq_tpees_clave", 4)]
            public string tpreq_tpees_clave { get; set; }

            [SPParameterName("p_tpreq_folio", 5)]
            public string tpreq_folio { get; set; }

            [SPParameterName("p_tpreq_fecha_dict", 6)]
            public string tpreq_fecha_dict { get; set; }

            [SPParameterName("p_tpreq_estatus", 7)]
            public string tpreq_estatus { get; set; }

            [SPParameterName("p_tpreq_user", 8)]
            public string tpreq_user { get; set; }
        }
    }
}
