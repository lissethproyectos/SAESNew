using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelDocente
    {
        [SPName("P_QRY_DIRECCION_DOCENTE")]
        public class ModelObtenerDireccionRequest : BaseModelRequest
        {
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerDireccionResponse : BaseModelResponse
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
        }

        [SPName("P_QRY_TCADO_ACTIVOS")]
        public class ModelObtenerCategoriasRequest : BaseModelRequest
        {
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerCategoriasResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }
        }


        [SPName("P_QRY_DOCE")]
        public class ModelObtenerCategoriasDocenteRequest : BaseModelRequest
        {
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerCategoriasDocenteResponse : BaseModelResponse
        {
            [SPResponseColumnName("categoria")]
            public string categoria { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }
        }

        [SPName("P_QRY_TDOID")]
        public class ModelObtenerIdiomasRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerIdiomasResponse : BaseModelResponse
        {
            [SPResponseColumnName("idioma")]
            public string idioma { get; set; }

            [SPResponseColumnName("porcentaje")]
            public string porcentaje { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }
        }



        [SPName("P_QRY_TDOCA")]
        public class ModelObtenerCarrerasRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }

        public class ModelObtenerCarrerasResponse : BaseModelResponse
        {
            [SPResponseColumnName("carrera")]
            public string carrera { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("cedula")]
            public string cedula { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }
        }

        [SPName("P_QRY_TELEFONO_DOCENTE")]
        public class ModelObtenerTelefonoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }       

        public class ModelObtenerTelefonoResponse : BaseModelResponse
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

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }
        }

        [SPName("P_QRY_CORREO_DOCENTE")]
        public class ModelObtenerCorreoRequest : BaseModelRequest
        {
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerCorreoResponse : BaseModelResponse
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


        [SPName("P_INS_TELEFONO_DOCENTE")]
        public class ModelInsertarTelefonoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }


        [SPName("P_INS_TALDI")]
        public class ModelInsertarTaldi : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
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


        [SPName("P_UPD_TALDI")]
        public class ModelEditarTaldi : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
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


        [SPName("P_INS_TALTE")]
        public class ModelInsertarTelefonoDocente : BaseModelRequest
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
        public class ModelInsertarTelefonoDocenteResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_UPD_TALTE")]
        public class ModelEditarTelefonoDocente : BaseModelRequest
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


        [SPName("P_QRY_TPERS_DOCENTE")]
        public class ModelObtenerDocentesRequest : BaseModelRequest
        {
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerDocentesResponse : BaseModelResponse
        {
            [SPResponseColumnName("CLAVE")]
            public string clave { get; set; }

            [SPResponseColumnName("NOMBRE")]
            public string nombre { get; set; }

            [SPResponseColumnName("PATERNO")]
            public string paterno { get; set; }

            [SPResponseColumnName("MATERNO")]
            public string materno { get; set; }

            [SPResponseColumnName("C_GENERO")]
            public string c_genero { get; set; }

            [SPResponseColumnName("GENERO")]
            public string genero { get; set; }

            [SPResponseColumnName("C_CIVIL")]
            public string c_civil { get; set; }

            [SPResponseColumnName("E_CIVIL")]
            public string e_civil { get; set; }

            [SPResponseColumnName("CURP")]
            public string curp { get; set; }

            [SPResponseColumnName("FECHA")]
            public string fecha { get; set; }

            [SPResponseColumnName("USUARIO")]
            public string usuario { get; set; }

            [SPResponseColumnName("FECHA_REG")]
            public string fecha_reg { get; set; }

            [SPResponseColumnName("pidm")]
            public string pidm { get; set; }
        }

        [SPName("P_INS_TALCO")]
        public class ModelInsertarTalco : BaseModelRequest
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

        [SPName("P_INS_TDOCA")]
        public class ModelInsertarTdoca : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_tdoca_carrera", 1)]
            public string tdoca_carrera { get; set; }

            [SPParameterName("p_tdoca_st_carrera", 2)]
            public string tdoca_st_carrera { get; set; }

            [SPParameterName("p_tdoca_ced_carrera", 3)]
            public string tdoca_ced_carrera { get; set; }

            [SPParameterName("p_tdoca_user", 4)]
            public string tdoca_user { get; set; }
        }
        public class ModelInsertarTdocaResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }


        [SPName("P_INS_TDOCE")]
        public class ModelEditarTdoce : BaseModelRequest
        {
            [SPParameterName("p_clave", 0)]
            public string clave { get; set; }

            [SPParameterName("p_tdoce_tcado_clave", 1)]
            public string tdoce_tcado_clave { get; set; }

            [SPParameterName("p_tdoce_estatus", 2)]
            public string tdoce_estatus { get; set; }

            [SPParameterName("p_tdoce_user", 3)]
            public string tdoce_user { get; set; }
        }

        [SPName("P_INS_TDOID")]
        public class ModelInsertarTdoid : BaseModelRequest
        {
            [SPParameterName("p_clave", 0)]
            public string clave { get; set; }

            [SPParameterName("p_tdoid_idioma", 1)]
            public string tdoid_idioma { get; set; }

            [SPParameterName("p_tdoid_porc_idioma", 2)]
            public string tdoid_porc_idioma { get; set; }

            [SPParameterName("p_tdoid_user", 3)]
            public string tdoid_user { get; set; }
        }

        [SPName("P_UPD_TALCO")]
        public class ModelEditarTalco : BaseModelRequest
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

            [SPParameterName("p_talco_consec", 6)]
            public string talco_consec { get; set; }
        }


        [SPName("P_QRY_DOCENTE_DISPONIBILIDAD")]
        public class ModelObtenerDisponibilidadRequest : BaseModelRequest
        {
            [SPParameterName("P_Clave", 0)]
            public string Clave { get; set; }
        }

        public class ModelObtenerDisponibilidadResponse : BaseModelResponse
        {
            [SPResponseColumnName("dia")]
            public string dia { get; set; }

            [SPResponseColumnName("H_ini")]
            public string H_ini { get; set; }

            [SPResponseColumnName("H_fin")]
            public string H_fin { get; set; }

            [SPResponseColumnName("cl_inicio")]
            public string cl_inicio { get; set; }

            [SPResponseColumnName("cl_fin")]
            public string cl_fin { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }
        }


        [SPName("P_INS_TDIDO")]//Actualiza Telefono
        public class ModelInsertarDisponibilidad : BaseModelRequest
        {

            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Dia", 1)]
            public string Dia { get; set; }

            [SPParameterName("P_FIni", 2)]
            public string FIni { get; set; }

            [SPParameterName("P_FFin", 3)]
            public string FFin { get; set; }

            [SPParameterName("P_Usuario", 3)]
            public string Usuario { get; set; }

            [SPParameterName("P_Status", 3)]
            public string Status { get; set; }

        }
        public class ModelInsertarDisponibilidadResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }


        [SPName("P_DEL_TDIDO")]//Actualiza Telefono
        public class ModelEliminarDisponibilidad : BaseModelRequest
        {

            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Dia", 1)]
            public string Dia { get; set; }

            [SPParameterName("P_FIni", 2)]
            public string FIni { get; set; }

            [SPParameterName("P_FFin", 3)]
            public string FFin { get; set; }

            [SPParameterName("P_Usuario", 3)]
            public string Usuario { get; set; }

            [SPParameterName("P_Status", 3)]
            public string Status { get; set; }

        }
    }
}
