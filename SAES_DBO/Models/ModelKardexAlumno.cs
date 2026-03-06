using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("P_OBTEN_KAREDEX_ALUMNO")]
    public class ModelKardexAlumnoRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_TPERS_NUM", 0)]
        public int NumeroPersona { get; set; }
        [Required]
        [SPParameterName("P_TPROG_CLAVE", 1)]
        public string ClavePorgrama { get; set; }
    }
    public class ModelKardexAlumnoResponse : BaseModelResponse
    {
        [SPResponseColumnName("PERIODO")]
        public string Periodo { get; set; }
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("MATERIA")]
        public string Materia { get; set; }
        [SPResponseColumnName("GRUPO")]
        public string Grupo { get; set; }
        [SPResponseColumnName("CALIFICACION")]
        public string Clasificacion { get; set; }
        [SPResponseColumnName("ACREDITA")]
        public string Acreditacion { get; set; }
        [SPResponseColumnName("USUARIO")]
        public string Usuario { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }
        [SPResponseColumnName("VALORCONTAR")]
        public int Mostrar { get; set; }
    }

    public class ModelKardexAlumnoForInsertResponse : BaseModelResponse
    {
        [SPResponseColumnName("PERIODO")]
        public string Periodo { get; set; }
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("MATERIA")]
        public string Materia { get; set; }
        [SPResponseColumnName("GRUPO")]
        public string Grupo { get; set; }
        [SPResponseColumnName("CALIFICACION")]
        public string Clasificacion { get; set; }
        [SPResponseColumnName("ACREDITA")]
        public string Acreditacion { get; set; }
        [SPResponseColumnName("USUARIO")]
        public string Usuario { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }
    }

    [SPName("P_INSERTA_KARDEX_ALUMNO")]
    public class ModelInsertKardexAlumnoResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_TPERS_NUM", 0)]
        public int NumeroPersona { get; set; }
        [Required]
        [SPParameterName("P_TPROG_CLAVE", 1)]
        public string ClavePrograma { get; set; }
        [Required]
        [SPParameterName("P_TPEES_CLAVE", 2)]
        public string ClavePeriodo { get; set; }
        [Required]
        [SPParameterName("P_TMATE_CLAVE", 3)]
        public string ClaveMateria { get; set; }
        [Required]
        [SPParameterName("P_TGRUP_CLAVE", 4)]
        public string ClaveGrupo { get; set; }
        [Required]
        [SPParameterName("P_TTIPO_CLAVE", 5)]
        public string ClaveAcreditacion { get; set; }
        [Required]
        [SPParameterName("P_TCALI_CLAVE", 6)]
        public string ClaveCalificacion { get; set; }
        [Required]
        [SPParameterName("P_USER", 8)]
        public string User { get; set; }
    }

    [SPName("P_INS_TTIR1")]
    public class ModelInsertMateriaAlumnoResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_matricula", 0)]
        public string matricula { get; set; }

        [Required]
        [SPParameterName("p_ttir1_tpees_clave", 1)]
        public string ttir1_tpees_clave { get; set; }

        [Required]
        [SPParameterName("p_ttir1_tcamp_clave", 2)]
        public string ttir1_tcamp_clave { get; set; }
        [Required]
        [SPParameterName("p_ttir1_tprog_clave", 3)]
        public string ttir1_tprog_clave { get; set; }
        [Required]
        [SPParameterName("p_ttir1_tmate_clave", 4)]
        public string ttir1_tmate_clave { get; set; }
        [Required]
        [SPParameterName("p_ttir1_tgrup_clave", 5)]
        public string ttir1_tgrup_clave { get; set; }
        [Required]
        [SPParameterName("p_ttir1_tnive_clave", 6)]
        public string ttir1_tnive_clave { get; set; }
        [Required]
        [SPParameterName("p_ttir1_user", 8)]
        public string ttir1_user { get; set; }
    }


    //[SPName("P_QRY_VALIDA_TOT_MATERIAS")]
    //public class ModelValidaTotMaterias : BaseModelRequest
    //{

    //    [SPParameterName("P_Matricula", 0)]
    //    public string matricula { get; set; }

    //    [SPParameterName("P_Campus", 1)]
    //    public string campus { get; set; }

    //    [SPParameterName("P_Programa", 2)]
    //    public string programa { get; set; }

    //    [SPParameterName("P_Periodo", 3)]
    //    public string periodo { get; set; }
    //}
    [SPName("P_QRY_VALIDA_TOTAL_MATERIAS")]
    public class ModelValidaTotMaterias : BaseModelRequest
    {

        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("P_Campus", 1)]
        public string campus { get; set; }

        [SPParameterName("P_Programa", 2)]
        public string programa { get; set; }

        [SPParameterName("P_Periodo", 3)]
        public string periodo { get; set; }

        [SPParameterName("P_TIPO_MATERIA", 4)]
        public string tipo_materia { get; set; }
    }
    public class ModelValidaTotMateriasResponse : BaseModelResponse
    {
        [SPResponseColumnName("Validado")]
        public string Validado { get; set; }

        [SPResponseColumnName("TotPropuestos")]
        public string TotPropuestos { get; set; }

        [SPResponseColumnName("TotMaterias")]
        public string TotMaterias { get; set; }
    }



    [SPName("P_INS_HORA_MATERIA")]
    public class ModelInsertarHoraMateria : BaseModelRequest
    {

        [SPParameterName("p_matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("p_thora_thocl_inicio", 1)]
        public string thora_thocl_inicio { get; set; }

        [SPParameterName("p_thora_thocl_fin", 2)]
        public string thora_thocl_fin { get; set; }

        [SPParameterName("p_thora_tpees_clave", 3)]
        public string thora_tpees_clave { get; set; }

        [SPParameterName("p_thora_tmate_clave", 4)]
        public string thora_tmate_clave { get; set; }

        [SPParameterName("p_thora_tgrup_clave", 5)]
        public string thora_tgrup_clave { get; set; }

        [SPParameterName("p_thora_tdias_clave", 6)]
        public string thora_tdias_clave { get; set; }

        [SPParameterName("p_thora_tcamp_clave", 7)]
        public string thora_tcamp_clave { get; set; }
    }

    public class ModelInsertarHoraMateriaResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }



    [SPName("P_INS_HORARIO_ALUMNO")]
    public class ModelInsertarHorarioAlumno : BaseModelRequest
    {

        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("P_Periodo", 1)]
        public string periodo { get; set; }

        [SPParameterName("P_Campus", 2)]
        public string campus { get; set; }

        [SPParameterName("P_Programa", 3)]
        public string programa { get; set; }

        [SPParameterName("P_Materia", 4)]
        public string materia { get; set; }

        [SPParameterName("P_Grupo", 5)]
        public string grupo { get; set; }

        [SPParameterName("P_Usuario", 6)]
        public string usuario { get; set; }
    }
    public class ModelInsertarHorarioAlumnoResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }



    [SPName("P_INS_HORARIO_MASIVO_GRAL")]
    public class ModelInsertarHorarioMasivo : BaseModelRequest
    {

        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("P_Periodo", 1)]
        public string periodo { get; set; }

        [SPParameterName("P_Campus", 2)]
        public string campus { get; set; }

        [SPParameterName("P_Programa", 3)]
        public string programa { get; set; }

        [SPParameterName("P_Materia", 4)]
        public string materia { get; set; }

        [SPParameterName("P_Usuario", 5)]
        public string usuario { get; set; }

    }
    public class ModelInsertarHorarioMasivoAlumnoResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }



    [SPName("P_UPD_MATERIA_ALUMNO")]
    public class ModelUpdMateriaAlumno : BaseModelRequest
    {

        [SPParameterName("p_matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("P_Periodo", 1)]
        public string periodo { get; set; }

        [SPParameterName("P_Programa", 2)]
        public string programa { get; set; }

        [SPParameterName("P_Campus", 3)]
        public string campus { get; set; }

        [SPParameterName("P_Materia", 4)]
        public string materia { get; set; }

        [SPParameterName("P_Estatus", 5)]
        public string estatus { get; set; }

        [SPParameterName("P_Grupo", 6)]
        public string grupo { get; set; }

        [SPParameterName("P_Nivel", 7)]
        public string nivel { get; set; }

        [SPParameterName("P_Usuario", 8)]
        public string usuario { get; set; }
    }


    [SPName("P_UPD_MATERIAS_PROPUESTAS")]
    public class ModelUpdMateriasPropuestas : BaseModelRequest
    {

        [SPParameterName("p_matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("P_Periodo", 1)]
        public string periodo { get; set; }

        [SPParameterName("P_Programa", 2)]
        public string programa { get; set; }

        [SPParameterName("P_Campus", 3)]
        public string campus { get; set; }

        [SPParameterName("P_Usuario", 4)]
        public string usuario { get; set; }
    }


    [SPName("P_ACTUALIZA_KARDEX_ALUMNO")]
    public class ModelActualizaKardexAlumnoResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_TPERS_NUM", 0)]
        public int NumeroPersona { get; set; }
        [Required]
        [SPParameterName("P_TPROG_CLAVE", 1)]
        public string ClavePrograma { get; set; }
        [Required]
        [SPParameterName("P_TPEES_CLAVE_OLD", 2)]
        public string ClavePeriodo_OLD { get; set; }
        [Required]
        [SPParameterName("P_TPEES_CLAVE_NEW", 3)]
        public string ClavePeriodo_NEW { get; set; }
        [Required]
        [SPParameterName("P_TMATE_CLAVE_OLD", 4)]
        public string ClaveMateria_OLD { get; set; }
        [Required]
        [SPParameterName("P_TMATE_CLAVE_NEW", 5)]
        public string ClaveMateria_NEW { get; set; }
        [Required]
        [SPParameterName("P_TGRUP_CLAVE_OLD", 6)]
        public string ClaveGrupo_OLD { get; set; }
        [Required]
        [SPParameterName("P_TGRUP_CLAVE_NEW", 7)]
        public string ClaveGrupo_NEW { get; set; }
        [Required]
        [SPParameterName("P_TTIPO_CLAVE_OLD", 8)]
        public string ClaveAcreditacion_Old { get; set; }
        [Required]
        [SPParameterName("P_TTIPO_CLAVE_NEW", 9)]
        public string ClaveAcreditacion_New { get; set; }
        [Required]
        [SPParameterName("P_TCALI_CLAVE_OLD", 10)]
        public string ClaveCalificacion_Old { get; set; }
        [Required]
        [SPParameterName("P_TCALI_CLAVE_NEW", 11)]
        public string ClaveCalificacion_New { get; set; }
        [Required]
        [SPParameterName("P_COMMENTS", 12)]
        public string Comentario { get; set; }
        [Required]
        [SPParameterName("P_USER", 13)]
        public string User { get; set; }
    }

    [SPName("P_OBTEN_BITACORA_KARDEX")]
    public class ModelBitacoraKardexAlumnoRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_TPERS_NUM", 0)]
        public int NumeroPersona { get; set; }
        [Required]
        [SPParameterName("P_TPROG_CLAVE", 1)]
        public string ClavePrograma { get; set; }
        [Required]
        [SPParameterName("P_TPEES_CLAVE", 2)]
        public string ClavePeriodo { get; set; }
        [Required]
        [SPParameterName("P_TMATE_CLAVE", 3)]
        public string ClaveMateria { get; set; }
    }
    public class ModelBitacoraKardexAlumnoResponse : BaseModelResponse
    {
        [SPResponseColumnName("CONSECUTIVO")]
        public string Consecutivo { get; set; }
        [SPResponseColumnName("CALIFICACION")]
        public string Calificacion { get; set; }
        [SPResponseColumnName("ACREDITACION")]
        public string Acreditacion { get; set; }
        [SPResponseColumnName("USUARIO")]
        public string Usuario { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }
        [SPResponseColumnName("COMENTARIO")]
        public string Comentario { get; set; }
    }

    [SPName("P_QRY_PERS_PROG")]
    public class ModelAlumnoProgRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_Periodo", 0)]
        public string testu_periodo { get; set; }
        [Required]
        [SPParameterName("P_Campus", 1)]
        public string testu_campus { get; set; }

        [Required]
        [SPParameterName("P_Nivel", 2)]
        public string testu_nivel { get; set; }

        [Required]
        [SPParameterName("P_Programa", 3)]
        public string testu_programa { get; set; }
    }
    public class ModelAlumnoProgResponse : BaseModelResponse
    {
        [SPResponseColumnName("PERIODO")]
        public string Periodo { get; set; }

        [SPResponseColumnName("CAMPUS")]
        public string Campus { get; set; }

        [SPResponseColumnName("NIVEL")]
        public string Nivel { get; set; }

        [SPResponseColumnName("PROG_CLAVE")]
        public string Prog_Clave { get; set; }

        [SPResponseColumnName("PROG_DESC")]
        public string Prog_Desc { get; set; }

        [SPResponseColumnName("MATRICULA")]
        public string Matricula { get; set; }

        [SPResponseColumnName("PATERNO")]
        public string Paterno { get; set; }

        [SPResponseColumnName("MATERNO")]
        public string Materno { get; set; }

        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
    }



    [SPName("P_QRY_MATERIAS_ALUMNO")]
    public class ModelObtenTtiraRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }
        [Required]
        [SPParameterName("P_Periodo", 1)]
        public string periodo { get; set; }
        [Required]
        [SPParameterName("P_Programa", 2)]
        public string programa { get; set; }        
    }
    public class ModelObtenTtiraResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("materia")]
        public string materia { get; set; }

        [SPResponseColumnName("grupo")]
        public string grupo { get; set; }

        [SPResponseColumnName("estatus")]
        public string estatus { get; set; }

        [SPResponseColumnName("estilo")]
        public string estilo { get; set; }

        [SPResponseColumnName("visible")]
        public string visible { get; set; }

        [SPResponseColumnName("tplan_ttima_clave")]
        public string tplan_ttima_clave { get; set; }
    }


    [SPName("P_QRY_HORARIOS_ALUMNO")]
    public class ModelObtenThoraRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }
        [Required]
        [SPParameterName("P_Periodo", 1)]
        public string periodo { get; set; }
        [Required]
        [SPParameterName("P_Campus", 2)]
        public string campus { get; set; }

        [SPParameterName("P_Materia", 3)]
        public string materia { get; set; }        
    }
    public class ModelObtenThoraResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("materia")]
        public string materia { get; set; }

        [SPResponseColumnName("grupo")]
        public string grupo { get; set; }

        [SPResponseColumnName("lunes")]
        public string lunes { get; set; }

        [SPResponseColumnName("martes")]
        public string martes { get; set; }
        
        [SPResponseColumnName("miercoles")]
        public string miercoles { get; set; }

        [SPResponseColumnName("jueves")]
        public string jueves { get; set; }

        [SPResponseColumnName("viernes")]
        public string viernes { get; set; }

        [SPResponseColumnName("sabado")]
        public string sabado { get; set; }
    }


    [SPName("P_QRY_MATERIAS_DISPONIBLES")]
    public class ModelObtenTtiraDispRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }
        [Required]
        [SPParameterName("P_Campus", 1)]
        public string campus { get; set; }
        [Required]
        [SPParameterName("P_Programa", 2)]
        public string programa { get; set; }

        [SPParameterName("P_Periodo", 3)]
        public string periodo { get; set; }
    }
    public class ModelObtenTtiraDispResponse : BaseModelResponse
    {
        [SPResponseColumnName("tgrup_tmate_clave")]
        public string tgrup_tmate_clave { get; set; }

        [SPResponseColumnName("tmate_desc")]
        public string tmate_desc { get; set; }

        [SPResponseColumnName("tplan_tpees_clave")]
        public string tplan_tpees_clave { get; set; }

        [SPResponseColumnName("tplan_ttima_clave")]
        public string tplan_ttima_clave { get; set; }

        
    }


    [SPName("P_QRY_HORARIO_GRUPO")]
    public class ModelObtenHorarioGrupoRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_Periodo", 0)]
        public string periodo { get; set; }
        [Required]
        [SPParameterName("P_Campus", 1)]
        public string campus { get; set; }
        [Required]
        [SPParameterName("P_Materia", 2)]
        public string materia { get; set; }
        [Required]
        [SPParameterName("P_Grupo", 3)]
        public string grupo { get; set; }
    }
    public class ModelObtenHorarioGrupoResponse : BaseModelResponse
    {
        [SPResponseColumnName("grupo")]
        public string grupo { get; set; }

        [SPResponseColumnName("dia")]
        public string dia { get; set; }

        [SPResponseColumnName("horario")]
        public string horario { get; set; }
    }


    [SPName("P_INS_TTIRL")]
    public class ModelInsTtirlRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_matricula", 0)]
        public string matricula { get; set; }
        [Required]
        [SPParameterName("p_ttir1_tpees_clave", 1)]
        public string ttir1_tpees_clave { get; set; }
        [Required]
        [SPParameterName("p_ttir1_tcamp_clave", 2)]
        public string ttir1_tcamp_clave { get; set; }

        [SPParameterName("p_ttir1_tnive_clave", 3)]
        public string ttir1_tnive_clave { get; set; }

        [SPParameterName("p_ttir1_tprog_clave", 4)]
        public string ttir1_tprog_clave { get; set; }

        [SPParameterName("p_ttir1_tmate_clave", 5)]
        public string ttir1_tmate_clave { get; set; }

        [SPParameterName("p_ttir1_user", 6)]
        public string ttir1_user { get; set; }
    }


    [SPName("P_QRY_GRUPOS_DISP")]
    public class ModelObtenTgrupDispRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_Materia", 0)]
        public string materia { get; set; }

        [SPParameterName("P_Grupo", 1)]
        public string grupo { get; set; }

        [SPParameterName("P_Periodo", 2)]
        public string periodo { get; set; }

        [SPParameterName("P_Campus", 3)]
        public string campus { get; set; }

        [SPParameterName("P_Programa", 4)]
        public string programa { get; set; }

        [SPParameterName("P_Matricula", 5)]
        public string matricula { get; set; }
    }
    public class ModelObtenTgrupDispResponse : BaseModelResponse
    {
        [SPResponseColumnName("tgrup_clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("tgrup_clave")]
        public string Descripcion { get; set; }

        [SPResponseColumnName("clasificacion")]
        public string Clasificacion { get; set; }        

    }

}
