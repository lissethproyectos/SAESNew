using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelAlumno
    {
        [SPName("P_QRY_TPERS")]
        public class ModelObtenerAlumnosRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerAlumnosResponse : BaseModelResponse
        {
            [SPResponseColumnName("CLAVE")]
            public string tpers_clave { get; set; }

            [SPResponseColumnName("NOMBRE")]
            public string tpers_nombre { get; set; }

            [SPResponseColumnName("PATERNO")]
            public string tpers_paterno { get; set; }

            [SPResponseColumnName("MATERNO")]
            public string tpers_materno { get; set; }

            [SPResponseColumnName("C_GENERO")]
            public string tpers_cgenero { get; set; }

            [SPResponseColumnName("GENERO")]
            public string tpers_genero { get; set; }

            [SPResponseColumnName("C_CIVIL")]
            public string tpers_civil { get; set; }

            [SPResponseColumnName("E_CIVIL")]
            public string tpers_ecivil { get; set; }

            [SPResponseColumnName("CURP")]
            public string tpers_curp { get; set; }

            [SPResponseColumnName("FECHA")]
            public string tpers_fecha { get; set; }

            [SPResponseColumnName("USUARIO")]
            public string tpers_usuario { get; set; }

            [SPResponseColumnName("FECHA_REG")]
            public string tpers_fecha_reg { get; set; }

            [SPResponseColumnName("ID")]
            public string tpers_num { get; set; }


        }

        [SPName("P_QRY_BECAS_ALUMNO")]
        public class ModelObtenerBecasAlumnoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }
        }

        public class ModelObtenerBecasAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("tepcb_tpees_clave")]
            public string tepcb_tpees_clave { get; set; }

            [SPResponseColumnName("tepcb_tpcbe_clave")]
            public string tepcb_tpcbe_clave { get; set; }

            [SPResponseColumnName("tpcbe_desc")]
            public string tpcbe_desc { get; set; }

            [SPResponseColumnName("tpcbe_porcentaje")]
            public string tpcbe_porcentaje { get; set; }

            [SPResponseColumnName("tepcb_estatus")]
            public string tepcb_estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("monto_beca")]
            public string monto_beca { get; set; }           
        }

        [SPName("P_QRY_DATOS_TPERS")]
        public class ModelObtenerDatosAlumnoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerDatosAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("CLAVE")]
            public string tpers_clave { get; set; }

            [SPResponseColumnName("NOMBRE")]
            public string tpers_nombre { get; set; }

            [SPResponseColumnName("PATERNO")]
            public string tpers_paterno { get; set; }

            [SPResponseColumnName("MATERNO")]
            public string tpers_materno { get; set; }

            [SPResponseColumnName("C_GENERO")]
            public string tpers_cgenero { get; set; }

            [SPResponseColumnName("GENERO")]
            public string tpers_genero { get; set; }

            [SPResponseColumnName("C_CIVIL")]
            public string tpers_civil { get; set; }

            [SPResponseColumnName("E_CIVIL")]
            public string tpers_ecivil { get; set; }

            [SPResponseColumnName("CURP")]
            public string tpers_curp { get; set; }

            [SPResponseColumnName("FECHA")]
            public string tpers_fecha { get; set; }

            [SPResponseColumnName("USUARIO")]
            public string tpers_usuario { get; set; }

            [SPResponseColumnName("FECHA_REG")]
            public string tpers_fecha_reg { get; set; }

        }

        [SPName("P_QRY_TPROG_TPERS")]
        public class ModelObtenerProgsAlumnoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerProgsAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("descripcion")]
            public string Descripcion { get; set; }

            [SPResponseColumnName("tprog_tnive_clave")]
            public string tprog_tnive_clave { get; set; }

            [SPResponseColumnName("testu_tcamp_clave")]
            public string testu_tcamp_clave { get; set; }
        }

        [SPName("P_QRY_TPROG_TPERS_MATRICULA")]
        public class ModelObtenerProgsAlumnoMatRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerProgsAlumnoMatResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("descripcion")]
            public string Descripcion { get; set; }
        }

        [SPName("P_QRY_BECAS")]
        public class ModelObtenerBecasRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Campus", 1)]
            public string Campus { get; set; }

            [SPParameterName("P_Programa", 2)]
            public string Programa { get; set; }

            [SPParameterName("P_Periodo", 3)]
            public string Periodo { get; set; }
        }

        public class ModelObtenerBecasResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("descripcion")]
            public string Descripcion { get; set; }
        }



        [SPName("P_QRY_SOLICITUD_INGRESO")]
        public class ModelObtenerSolicitudesIngresoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerSolicitudesIngresoResponse : BaseModelResponse
        {
            [SPResponseColumnName("id_num")]
            public string id_num { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("periodo")]
            public string periodo { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("turno")]
            public string turno { get; set; }

            [SPResponseColumnName("campus")]
            public string campus { get; set; }

            [SPResponseColumnName("programa")]
            public string programa { get; set; }

            [SPResponseColumnName("tiin")]
            public string tiin { get; set; }

            [SPResponseColumnName("tasa")]
            public string tasa { get; set; }

            [SPResponseColumnName("e_pro")]
            public string e_pro { get; set; }

            [SPResponseColumnName("promedio")]
            public string promedio { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

        }


        [SPName("P_QRY_ULT_PERIODO_ALU")]
        public class ModelObtenerPeriodoAlumnoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerPeriodoAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("descripcion")]
            public string Descripcion { get; set; }
        }

        [SPName("P_QRY_TESTU")]
        public class ModelObtenerProgramaPeriodoAlumnoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_ID", 0)]

            public string testu_tpers_id { get; set; }
        }

        public class ModelObtenerProgramaPeriodoAlumnoResponse : BaseModelResponse
        {

            [SPResponseColumnName("testu_tpees_clave")]
            public string testu_tpees_clave { get; set; }

            [SPResponseColumnName("tcamp_desc")]
            public string tcamp_desc { get; set; }

            [SPResponseColumnName("testu_tprog_clave")]
            public string testu_tprog_clave { get; set; }

            [SPResponseColumnName("testu_periodo")]
            public string testu_periodo { get; set; }

            [SPResponseColumnName("desc_turno")]
            public string desc_turno { get; set; }

            [SPResponseColumnName("ttasa_desc")]
            public string ttasa_desc { get; set; }

            [SPResponseColumnName("testu_tstal_clave")]
            public string testu_tstal_clave { get; set; }

            [SPResponseColumnName("testu_user")]
            public string testu_user { get; set; }

            [SPResponseColumnName("testu_date")]
            public string testu_date { get; set; }

            [SPResponseColumnName("tcamp_clave")]
            public string tcamp_clave { get; set; }

            [SPResponseColumnName("ttasa_clave")]
            public string ttasa_clave { get; set; }

            [SPResponseColumnName("testu_tespe_clave")]
            public string testu_tespe_clave { get; set; }
        }

        [SPName("P_INS_TESTU")]//Insertar_Pais
        public class ModelInsertarTestu : BaseModelRequest
        {

            [SPParameterName("p_testu_tpers_id", 0)]
            public string testu_tpers_id { get; set; }

            [SPParameterName("p_testu_tpees_clave", 1)]
            public string testu_tpees_clave { get; set; }

            [SPParameterName("p_testu_tcamp_clave", 2)]
            public string testu_tcamp_clave { get; set; }

            [SPParameterName("p_testu_tprog_clave", 3)]
            public string testu_tprog_clave { get; set; }

            [SPParameterName("p_testu_ttasa_clave", 4)]
            public string testu_ttasa_clave { get; set; }

            [SPParameterName("p_testu_tstal_clave", 5)]
            public string testu_tstal_clave { get; set; }

            [SPParameterName("p_testu_periodo", 6)]
            public string testu_periodo { get; set; }

            [SPParameterName("p_testu_tespe_clave", 7)]
            public string testu_tespe_clave { get; set; }

            [SPParameterName("p_testu_turno", 8)]
            public string testu_turno { get; set; }

            [SPParameterName("p_testu_user", 9)]
            public string testu_user { get; set; }

            [SPParameterName("p_calculo_cuotas", 10)]
            public string calculo_cuotas { get; set; }


        }

        public class ModelInsertarTestuResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_UPD_TESTU")]
        public class ModelEditarTestu : BaseModelRequest
        {

            [SPParameterName("p_testu_tpers_id", 0)]
            public string testu_tpers_id { get; set; }

            [SPParameterName("p_testu_tpees_clave", 1)]
            public string testu_tpees_clave { get; set; }

            [SPParameterName("p_testu_tcamp_clave", 2)]
            public string testu_tcamp_clave { get; set; }

            [SPParameterName("p_testu_tprog_clave", 3)]
            public string testu_tprog_clave { get; set; }

            [SPParameterName("p_testu_ttasa_clave", 4)]
            public string testu_ttasa_clave { get; set; }

            [SPParameterName("p_testu_tstal_clave", 5)]
            public string testu_tstal_clave { get; set; }

            [SPParameterName("p_testu_periodo", 6)]
            public int testu_periodo { get; set; }

            [SPParameterName("p_testu_tespe_clave", 7)]
            public string testu_tespe_clave { get; set; }

            [SPParameterName("p_testu_turno", 8)]
            public string testu_turno { get; set; }

            [SPParameterName("p_testu_user", 9)]
            public string testu_user { get; set; }            

            [SPParameterName("p_calculo_cuotas", 10)]
            public string calculo_cuotas { get; set; }
            
        }

        public class ModelEditarTestuResponse : BaseModelResponse
        {
            [SPResponseColumnName("PeriodoVigente")]
            public string PeriodoVigente { get; set; }
        }

        [SPName("P_UPD_TCODA")]
        public class ModelEditarTcoda : BaseModelRequest
        {

            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Tipo", 1)]
            public string Tipo { get; set; }

            [SPParameterName("P_Consecutivo", 2)]
            public string Consecutivo { get; set; }

            [SPParameterName("P_Nombre_Contacto", 3)]
            public string Nombre_Contacto { get; set; }

            [SPParameterName("P_Fecha_Nacimiento", 4)]
            public string Fecha_Nacimiento { get; set; }

            [SPParameterName("P_Rfc", 5)]
            public string Rfc { get; set; }

            [SPParameterName("P_Usuario", 6)]
            public string Usuario { get; set; }

        }


        [SPName("P_UPD_TADMI")]
        public class ModelEditarTadmi : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("p_tadmi_tpees_clave", 1)]
            public string tadmi_tpees_clave { get; set; }

            [SPParameterName("p_tadmi_consecutivo", 2)]
            public string tadmi_consecutivo { get; set; }

            [SPParameterName("p_tadmi_turno", 3)]
            public string tadmi_turno { get; set; }

            [SPParameterName("p_tadmi_tprog_clave", 4)]
            public string tadmi_tprog_clave { get; set; }

            [SPParameterName("p_tadmi_ttiin_clave", 5)]
            public string tadmi_ttiin_clave { get; set; }

            [SPParameterName("p_tadmi_tcamp_clave", 6)]
            public string tadmi_tcamp_clave { get; set; }

            [SPParameterName("p_tadmi_tespr_clave", 7)]
            public string tadmi_tespr_clave { get; set; }

            [SPParameterName("p_tadmi_promedio", 8)]
            public string tadmi_promedio { get; set; }

            [SPParameterName("p_tadmi_ttasa_clave", 9)]
            public string tadmi_ttasa_clave { get; set; }

            [SPParameterName("p_tadmi_user", 10)]
            public string tadmi_user { get; set; }
        }

        [SPName("P_INS_TBAAP")]//Insertar baja, y se cancelan los pagos anticipados
        public class ModelInstbaap : BaseModelRequest
        {

            [SPParameterName("p_tbaap_tpers_num", 0)]
            public string tbaap_tpers_num { get; set; }

            [SPParameterName("p_tbaap_tprog_clave", 1)]
            public string tbaap_tprog_clave { get; set; }

            [SPParameterName("p_tbaap_tpees_clave", 2)]
            public string tbaap_tpees_clave { get; set; }

            [SPParameterName("p_tbaap_ttiba_clave", 3)]
            public string tbaap_ttiba_clave { get; set; }

            [SPParameterName("p_tbaap_fecha_baja", 4)]
            public string tbaap_fecha_baja { get; set; }

            [SPParameterName("p_tbaap_estima", 5)]
            public string tbaap_estima { get; set; }

            [SPParameterName("p_tbaap_date", 6)]
            public string tbaap_date { get; set; }

            [SPParameterName("p_tbaap_user", 7)]
            public string tbaap_user { get; set; }
        }
        public class ModelInstbaapResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_UPD_TBAAP")]//Insertar_Pais
        public class ModelModificarBaja : BaseModelRequest
        {
            [SPParameterName("p_tbaap_tpers_num", 0)]
            public string tbaap_tpers_num { get; set; }

            [SPParameterName("p_tbaap_tprog_clave", 1)]
            public string tbaap_tprog_clave { get; set; }

            [SPParameterName("p_tbaap_tpees_clave", 2)]
            public string tbaap_tpees_clave { get; set; }

            [SPParameterName("p_tbaap_ttiba_clave", 3)]
            public string tbaap_ttiba_clave { get; set; }

            [SPParameterName("p_tbaap_fecha_baja", 4)]
            public string tbaap_fecha_baja { get; set; }

            [SPParameterName("p_tbaap_estima", 5)]
            public string tbaap_estima { get; set; }

            [SPParameterName("p_tbaap_user", 6)]
            public string tbaap_user { get; set; }
        }

        [SPName("P_QRY_TPROG_TPERS_ACT")]
        public class ModelObtenerProgActAlumnoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string P_Matricula { get; set; }
        }

        public class ModelObtenerProgActAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("paterno")]
            public string paterno { get; set; }

            [SPResponseColumnName("materno")]
            public string materno { get; set; }

            [SPResponseColumnName("testu_tpees_clave")]
            public string testu_tpees_clave { get; set; }

            [SPResponseColumnName("testu_tcamp_clave")]
            public string testu_tcamp_clave { get; set; }
        }

        [SPName("P_MODIFICA_CUOTA")]
        public class ModelCalculaCuotas : BaseModelRequest
        {        

            [SPParameterName("P_PERIODO", 0)]
            public string testu_tpees_clave { get; set; }

            [SPParameterName("P_ID", 1)]
            public string testu_tpers_id { get; set; }

            [SPParameterName("P_TASA", 2)]
            public string testu_ttasa_clave { get; set; }

            [SPParameterName("P_CAMPUS", 3)]
            public string testu_tcamp_clave { get; set; }

            [SPParameterName("P_PROGRAMA", 4)]
            public string testu_tprog_clave { get; set; }

            [SPParameterName("P_USER", 5)]
            public string testu_user { get; set; }

        }

        [SPName("P_GENERA_CUOTA")]
        public class ModelGeneraCuotas : BaseModelRequest
        {
            [SPParameterName("P_PERIODO", 0)]
            public string testu_tpees_clave { get; set; }

            [SPParameterName("P_ID", 1)]
            public string testu_tpers_id { get; set; }           

            [SPParameterName("P_CAMPUS", 2)]
            public string testu_tcamp_clave { get; set; }

            [SPParameterName("P_PROGRAMA", 3)]
            public string testu_tprog_clave { get; set; }

            [SPParameterName("P_USER", 4)]
            public string testu_user { get; set; }

        }

        [SPName("P_UPD_TPERS")]//Modificar tpers
        public class ModelModificarTpers : BaseModelRequest
        {
            [SPParameterName("p_tpers_nombre", 0)]
            public string tpers_nombre { get; set; }

            [SPParameterName("p_tpers_paterno", 1)]
            public string tpers_paterno { get; set; }

            [SPParameterName("p_tpers_materno", 2)]
            public string tpers_materno { get; set; }

            [SPParameterName("p_tpers_genero", 3)]
            public string tpers_genero { get; set; }

            [SPParameterName("p_tpers_fecha_nac", 4)]
            public string tpers_fecha_nac { get; set; }

            [SPParameterName("p_tpers_edo_civ", 5)]
            public string tpers_edo_civ { get; set; }

            [SPParameterName("p_tpers_curp", 6)]
            public string tpers_curp { get; set; }

            [SPParameterName("p_tpers_usuario", 7)]
            public string tpers_usuario { get; set; }

            [SPParameterName("p_tpers_id", 8)]
            public string tpers_id { get; set; }
        }

        [SPName("P_QRY_DIRECCION_ALUM")]
        public class ModelObtenDireccionesAluRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }
        public class ModelObtenDireccionesAluResponse : BaseModelResponse
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

            [SPResponseColumnName("taldi_tpais_clave")]
            public string taldi_tpais_clave { get; set; }

            [SPResponseColumnName("taldi_testa_clave")]
            public string taldi_testa_clave { get; set; }


            [SPResponseColumnName("taldi_tdele_clave")]
            public string taldi_tdele_clave { get; set; }

            [SPResponseColumnName("taldi_tcopo_clave")]
            public string taldi_tcopo_clave { get; set; }

            [SPResponseColumnName("taldi_colonia")]
            public string taldi_colonia { get; set; }

            [SPResponseColumnName("taldi_ciudad")]
            public string taldi_ciudad { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

        }

        [SPName("P_QRY_TALTE")]
        public class ModelObtenerTelefonosRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }
        public class ModelObtenerTelefonosResponse : BaseModelResponse
        {
            [SPResponseColumnName("id_num")]
            public string id_num { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("tipo_tel")]
            public string tipo_tel { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("lada")]
            public string lada { get; set; }

            [SPResponseColumnName("telefono")]
            public string telefono { get; set; }

            [SPResponseColumnName("extension")]
            public string extension { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

        }

        [SPName("INS_TALTE")]//Insertar baja, y se cancelan los pagos anticipados
        public class ModelInsTalte : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_talte_ttele_clave", 1)]
            public string talte_ttele_clave { get; set; }

            [SPParameterName("p_talte_lada", 2)]
            public string talte_lada { get; set; }

            [SPParameterName("p_talte_tel", 3)]
            public string talte_tel { get; set; }

            [SPParameterName("p_talte_ext", 4)]
            public string talte_ext { get; set; }

            [SPParameterName("p_talte_estatus", 5)]
            public string talte_estatus { get; set; }

            [SPParameterName("p_talte_user", 6)]
            public string talte_user { get; set; }
        }

        [SPName("UPD_TALTE")]//Insertar baja, y se cancelan los pagos anticipados
        public class ModelEditarTalte : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_talte_consec", 1)]
            public string talte_consec { get; set; }

            [SPParameterName("p_talte_ttele_clave", 2)]
            public string talte_ttele_clave { get; set; }

            [SPParameterName("p_talte_lada", 3)]
            public string talte_lada { get; set; }

            [SPParameterName("p_talte_tel", 4)]
            public string talte_tel { get; set; }

            [SPParameterName("p_talte_ext", 5)]
            public string talte_ext { get; set; }

            [SPParameterName("p_talte_estatus", 6)]
            public string talte_estatus { get; set; }

            [SPParameterName("p_talte_user", 7)]
            public string talte_user { get; set; }
        }

        [SPName("P_QRY_TMAIL_ALUMNO")]
        public class ModelObtenerCorreosRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerCorreosResponse : BaseModelResponse
        {
            [SPResponseColumnName("id_num")]
            public string id_num { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("tipo_mail")]
            public string tipo_mail { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("correo")]
            public string correo { get; set; }

            [SPResponseColumnName("preferido")]
            public string preferido { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

        }

        [SPName("INS_TALCO")]//Actualizar correo
        public class ModelInstalco : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_talco_tmail_clave", 1)]
            public string talco_tmail_clave { get; set; }

            [SPParameterName("p_talco_correo", 2)]
            public string talco_correo { get; set; }

            [SPParameterName("p_talco_preferido", 3)]
            public string talco_preferido { get; set; }

            [SPParameterName("p_talco_estatus", 4)]
            public string talco_estatus { get; set; }

            [SPParameterName("p_talco_user", 5)]
            public string talco_user { get; set; }
        }



        [SPName("UPD_TALCO")]//Editar correo
        public class ModelUpdtalco : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_talco_tmail_clave", 1)]
            public string talco_tmail_clave { get; set; }

            [SPParameterName("p_talco_correo", 2)]
            public string talco_correo { get; set; }

            [SPParameterName("p_talco_preferido", 3)]
            public string talco_preferido { get; set; }

            [SPParameterName("p_talco_estatus", 4)]
            public string talco_estatus { get; set; }

            [SPParameterName("p_talco_user", 5)]
            public string talco_user { get; set; }
        }


        [SPName("P_UPD_TCNCO")]//Editar correo contacto
        public class ModelUpdtcnco : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_tcnco_tcoda_consec", 1)]
            public string tcnco_tcoda_consec { get; set; }

            [SPParameterName("p_tcnco_consec", 2)]
            public string tcnco_consec { get; set; }

            [SPParameterName("p_tcnco_tmail_clave", 3)]
            public string tcnco_tmail_clave { get; set; }

            [SPParameterName("p_tcnco_correo", 4)]
            public string tcnco_correo { get; set; }

            [SPParameterName("p_tcnco_preferido", 5)]
            public string tcnco_preferido { get; set; }


            [SPParameterName("p_tcnco_estatus", 6)]
            public string tcnco_estatus { get; set; }


            [SPParameterName("p_tcnco_user", 7)]
            public string tcnco_user { get; set; }
        }

        [SPName("P_QRY_DOCUMENTOS")]
        public class ModelObtenerDocumentosRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerDocumentosResponse : BaseModelResponse
        {
            [SPResponseColumnName("Id_Num")]
            public string Id_Num { get; set; }

            [SPResponseColumnName("periodo")]
            public string periodo { get; set; }

            [SPResponseColumnName("programa")]
            public string programa { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("clave_docto")]
            public string clave_docto { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("f_limite")]
            public string f_limite { get; set; }

            [SPResponseColumnName("fecha_limite")]
            public string fecha_limite { get; set; }

            [SPResponseColumnName("f_entrega")]
            public string f_entrega { get; set; }

            [SPResponseColumnName("fecha_entrega")]
            public string fecha_entrega { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("st")]
            public string st { get; set; }

        }




        [SPName("P_QRY_DOCUMENTOS_DISPONIBLES")]
        public class ModelObtenerDocumentosDisponiblesRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerDocumentosDisponiblesResponse : BaseModelResponse
        {
            [SPResponseColumnName("CLAVE")]
            public string CLAVE { get; set; }

            [SPResponseColumnName("DOCUMENTO")]
            public string DOCUMENTO { get; set; }
        }


        [SPName("P_QRY_CONTACTO_ALUMNO")]
        public class ModelObtenerContactosRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerContactosResponse : BaseModelResponse
        {

            [SPResponseColumnName("consecutivo")]
            public string clave { get; set; }

            [SPResponseColumnName("contacto")]
            public string descripcion { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("tipo")]
            public string tipo { get; set; }

            [SPResponseColumnName("contacto")]
            public string contacto { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("curp")]
            public string curp { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("usuario")]
            public string usuario { get; set; }

            [SPResponseColumnName("fecha_reg")]
            public string fecha_reg { get; set; }
        }

        [SPName("P_INS_CONTACTO_ALUMNO")]//Insertar correo
        public class ModelInsTcoda : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("P_tcoda_tipo", 1)]
            public string tcoda_tipo { get; set; }

            [SPParameterName("P_tcoda_nombre", 2)]
            public string tcoda_nombre { get; set; }

            [SPParameterName("P_tcoda_fecha_nac", 3)]
            public string tcoda_fecha_nac { get; set; }

            [SPParameterName("P_tcoda_rfc", 4)]
            public string tcoda_rfc { get; set; }

            [SPParameterName("P_tcoda_usuario", 5)]
            public string tcoda_usuario { get; set; }
        }

        [SPName("P_INS_TALDI")]//Insertar Direccion
        public class ModelInsTaldi : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_taldi_tdire_clave", 1)]
            public string taldi_tdire_clave { get; set; }

            [SPParameterName("p_taldi_calle", 2)]
            public string taldi_calle { get; set; }

            [SPParameterName("p_taldi_colonia", 3)]
            public string taldi_colonia { get; set; }

            [SPParameterName("p_taldi_testa_clave", 4)]
            public string taldi_testa_clave { get; set; }

            [SPParameterName("p_taldi_tdele_clave", 5)]
            public string taldi_tdele_clave { get; set; }

            [SPParameterName("p_taldi_tpais_clave", 6)]
            public string taldi_tpais_clave { get; set; }

            [SPParameterName("p_taldi_tcopo_clave", 7)]
            public string taldi_tcopo_clave { get; set; }

            [SPParameterName("p_taldi_ciudad", 8)]
            public string taldi_ciudad { get; set; }

            [SPParameterName("p_taldi_estatus", 9)]
            public string taldi_estatus { get; set; }

            [SPParameterName("p_taldi_user", 10)]
            public string taldi_user { get; set; }
        }


        [SPName("P_UPD_TALDI")]//Actualiza Direccion
        public class ModelUpdTaldi : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_taldi_tdire_clave", 1)]
            public string taldi_tdire_clave { get; set; }

            [SPParameterName("p_taldi_calle", 2)]
            public string taldi_calle { get; set; }

            [SPParameterName("p_taldi_colonia", 3)]
            public string taldi_colonia { get; set; }

            [SPParameterName("p_taldi_testa_clave", 4)]
            public string taldi_testa_clave { get; set; }

            [SPParameterName("p_taldi_tdele_clave", 5)]
            public string taldi_tdele_clave { get; set; }

            [SPParameterName("p_taldi_tpais_clave", 6)]
            public string taldi_tpais_clave { get; set; }

            [SPParameterName("p_taldi_tcopo_clave", 7)]
            public string taldi_tcopo_clave { get; set; }

            [SPParameterName("p_taldi_ciudad", 8)]
            public string taldi_ciudad { get; set; }

            [SPParameterName("p_taldi_estatus", 9)]
            public string taldi_estatus { get; set; }

            [SPParameterName("p_taldi_user", 10)]
            public string taldi_user { get; set; }

            [SPParameterName("p_taldi_consec", 11)]
            public string taldi_consec { get; set; }
        }

        [SPName("P_INS_TCOTE")]//Insertar telefono de contacto
        public class ModelInsertarTelContacto : BaseModelRequest
        {

            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_tcote_tcoda_consec", 1)]
            public string tcote_tcoda_consec { get; set; }           

            [SPParameterName("p_tcote_ttele_clave", 2)]
            public string tcote_ttele_clave { get; set; }

            [SPParameterName("p_tcote_lada", 3)]
            public string tcote_lada { get; set; }

            [SPParameterName("p_tcote_tel", 4)]
            public string tcote_tel { get; set; }

            [SPParameterName("p_tcote_ext", 5)]
            public string tcote_ext { get; set; }

            [SPParameterName("p_tcote_estatus", 6)]
            public string tcote_estatus { get; set; }

            [SPParameterName("p_tcote_user",7)]
            public string tcote_user { get; set; }

        }

        public class ModelInsertarTelContactoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }


        [SPName("P_UPD_TCOTE")]//Insertar telefono de contacto
        public class ModelEditarTelContacto : BaseModelRequest
        {

            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_tcote_tcoda_consec", 1)]
            public string tcote_tcoda_consec { get; set; }

            [SPParameterName("p_tcote_ttele_clave", 2)]
            public string tcote_ttele_clave { get; set; }

            [SPParameterName("p_tcote_lada", 3)]
            public string tcote_lada { get; set; }

            [SPParameterName("p_tcote_tel", 4)]
            public string tcote_tel { get; set; }

            [SPParameterName("p_tcote_ext", 5)]
            public string tcote_ext { get; set; }

            [SPParameterName("p_tcote_estatus", 6)]
            public string tcote_estatus { get; set; }

            [SPParameterName("p_tcote_user", 7)]
            public string tcote_user { get; set; }

            [SPParameterName("p_tcote_consec", 8)]
            public string tcote_consec { get; set; }
        }


        [SPName("P_QRY_TEL_CONTACTOS")]
        public class ModelObtenerTelContactosRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerTelContactosResponse : BaseModelResponse
        {
            [SPResponseColumnName("id_num")]
            public string id_num { get; set; }

            [SPResponseColumnName("consecutivo")]
            public string consecutivo { get; set; }

            [SPResponseColumnName("tipo_tel")]
            public string tipo_tel { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }

            [SPResponseColumnName("lada")]
            public string lada { get; set; }

            [SPResponseColumnName("telefono")]
            public string telefono { get; set; }

            [SPResponseColumnName("extension")]
            public string extension { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("cl_contacto")]
            public string cl_contacto { get; set; }

            [SPResponseColumnName("contacto")]
            public string contacto { get; set; }
        }


        [SPName("P_QRY_EMAIL_CONTACTOS")]
        public class ModelObtenerEmailContactosRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerEmailContactosResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("contacto")]
            public string contacto { get; set; }
        }

        public class ModelValidaTalcoRequest
        {
            public string p_matricula { get; set; }
        }

        public class ModelValidaTalcoResponse
        {
            public int Indicador { get; set; }
        }

    }
}
