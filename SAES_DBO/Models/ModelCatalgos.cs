using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{



    [SPName("p_obten_periodo")]
    public class ModelPeriodoRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_nada", 0)]
        public string Nada { get; set; }
    }
    public class ModelPeriodoResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string Clave { get; set; }
        [SPResponseColumnName("descripcion")]
        public string Descripcion { get; set; }
    }

    [SPName("p_obten_campus")]
    public class ModelCampusRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_nada", 0)]
        public string Nada { get; set; }
    }
    public class ModelCampusResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string Clave { get; set; }
        [SPResponseColumnName("descripcion")]
        public string Descripcion { get; set; }
    }


    [SPName("P_QRY_TTIOP")]
    public class ModelTtiopRequest : BaseModelRequest
    {

    }
    public class ModelTtiopResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("descripcion")]
        public string Descripcion { get; set; }
    }


    [SPName("P_QRY_TTIOP_NIVEL")]
    public class ModelTtiopNivelRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_Nivel", 0)]
        public string Nivel { get; set; }
    }
    public class ModelTtiopNivelpResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("descripcion")]
        public string Descripcion { get; set; }
    }


    [SPName("P_QRY_TMODA_ACTIVOS")]
    public class ModelTModaActivosRequest : BaseModelRequest
    {

    }
    public class ModelTModaActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("descripcion")]
        public string descripcion { get; set; }
    }

    [SPName("P_OBTEN_CAMPUS_DOCENTE")]
    public class ModelCampusDocenteRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_USER", 0)]
        public string Usuario { get; set; }
        [Required]
        [SPParameterName("P_PERIODO", 1)]
        public string Periodo { get; set; }
    }
    public class ModelCampusDocenteResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("CAMPUS")]
        public string Descripcion { get; set; }
    }

    [SPName("P_QRY_NIVEL")]
    public class ModelNivelRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_campus", 0)]
        public string Campus { get; set; }
    }
    public class ModelNivelResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string Clave { get; set; }
        [SPResponseColumnName("descripcion")]
        public string Descripcion { get; set; }
    }

    [SPName("P_QRY_TIPO_PERIODO")]
    public class ModelTipoPeriodoRequest : BaseModelRequest
    {

    }
    public class ModelTipoPeriodoResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string Clave { get; set; }
        [SPResponseColumnName("descripcion")]
        public string Descripcion { get; set; }
    }


    [SPName("P_OBTEN_PROGRAMA")]
    public class ModelProgramaRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CAMPUS", 0)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("P_NIVEL", 1)]
        public string Nivel { get; set; }
    }
    public class ModelProgramaResponse : BaseModelResponse
    {
        [SPResponseColumnName("TPROG_CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("TPROG_DESC")]
        public string Descripcion { get; set; }
    }

    [SPName("P_QRY_PROGRAMA")]
    public class ModelCatProgramaRequest : BaseModelRequest
    {

    }
    public class ModelCatProgramaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }

    [SPName("P_QRY_PROGRAMA_CAMPUS")]
    public class ModelProgramaCampusRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CAMPUS", 0)]
        public string Campus { get; set; }
    }
    public class ModelProgramaCampusResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string Clave { get; set; }
        [SPResponseColumnName("Descripcion")]
        public string Descripcion { get; set; }
    }


    [SPName("P_QRY_PROG_CAPR")]
    public class ModelProgramaVigenteRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CAMPUS", 0)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("P_NIVEL", 1)]
        public string Nivel { get; set; }
    }
    public class ModelProgramaVigenteResponse : BaseModelResponse
    {
        [SPResponseColumnName("TPROG_CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("TPROG_DESC")]
        public string Descripcion { get; set; }
    }

    [SPName("P_OBTEN_MATERIA")]
    public class ModelMateriaRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CAMPUS", 0)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("P_NIVEL", 1)]
        public string Nivel { get; set; }
        [SPParameterName("P_PROGRAMA", 2)]
        public string Programa { get; set; }
    }
    public class ModelMateriaResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("DESCRIPCION_MATERIA")]
        public string Descripcion { get; set; }
    }

    [SPName("P_OBTEN_MATERIA_DOCENTE")]
    public class ModelMateriaDocenteRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_USER", 0)]
        public string Usuario { get; set; }
        [Required]
        [SPParameterName("P_CAMPUS", 1)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("P_PERIODO", 2)]
        public string Periodo { get; set; }
    }
    public class ModelMateriaDocenteResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("MATE")]
        public string Descripcion { get; set; }
    }

    [SPName("P_OBTEN_GRUPO")]
    public class ModelGrupoRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CAMPUS", 0)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("P_NIVEL", 1)]
        public string Nivel { get; set; }
        [SPParameterName("P_PROGRAMA", 2)]
        public string Programa { get; set; }
        [SPParameterName("P_MATERIA", 3)]
        public string Materia { get; set; }
        [SPParameterName("P_PERIODO", 4)]
        public string Periodo { get; set; }
    }
    public class ModelGrupoResponse : BaseModelResponse
    {
        [SPResponseColumnName("GRUPO")]
        public string Clave { get; set; }
        [SPResponseColumnName("GRUPO")]
        public string Descripcion { get; set; }
    }

    [SPName("P_OBTEN_GRUPO_DOCENTE")]
    public class ModelGrupoDocenteRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_USER", 0)]
        public string Usuario { get; set; }
        [Required]
        [SPParameterName("P_CAMPUS", 1)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("P_PERIODO", 2)]
        public string Periodo { get; set; }
        [Required]
        [SPParameterName("P_MATERIA", 3)]
        public string Materia { get; set; }
    }
    public class ModelGrupoDocenteResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("CLAVE")]
        public string Descripcion { get; set; }
    }

    [SPName("P_OBTEN_COMPONENTE")]
    public class ModelComponenteRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATERIA", 0)]
        public string Materia { get; set; }
    }
    public class ModelComponenteResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("COMPONENTE")]
        public string Descripcion { get; set; }
    }

    [SPName("P_OBTEN_CALI")]
    public class ModelCalificacionesRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_PROGRAMA", 0)]
        public string Programa { get; set; }
    }
    public class ModelCalificacionesResponse : BaseModelResponse
    {
        [SPResponseColumnName("PUNTOS")]
        public string Clave { get; set; }
        [SPResponseColumnName("CALI")]
        public string Descripcion { get; set; }
    }

    [SPName("P_OBTEN_MATRICULA")]
    public class ModelMatriculaRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
    }
    public class ModelMatriculaResponse : BaseModelResponse
    {
        [SPResponseColumnName("NUMERO")]
        public string Numero { get; set; }
        [SPResponseColumnName("MATRICULA")]
        public string Matricula { get; set; }
        [SPResponseColumnName("ID")]
        public string Id { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
        [SPResponseColumnName("PATERNO")]
        public string ApellidoPaterno { get; set; }
        [SPResponseColumnName("MATERNO")]
        public string ApellidoMaterno { get; set; }
    }

    [SPName("P_OBTEN_ALUMNO_PROGRAMA")]
    public class ModelAlumnoProgramaRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_TPERS_NUM", 0)]
        public string PERS_NUM { get; set; }
    }
    public class ModelAlumnoProgramaResponse : BaseModelResponse
    {
        [SPResponseColumnName("PROGRAMA")]
        public string Clave { get; set; }
        [SPResponseColumnName("DESCRIPCION")]
        public string Descripcion { get; set; }
    }




    [SPName("P_OBTEN_PROGRAMA_TCEEL")]
    public class ModelAlumnoProgramaTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelAlumnoProgramaTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("IdCampus")]
        public string IdCampus { get; set; }
        [SPResponseColumnName("tcamp_clave")]
        public string ClaveCampus { get; set; }
        [SPResponseColumnName("tcamp_desc")]
        public string DescripcionCampus { get; set; }
        [SPResponseColumnName("idCarrera")]
        public string IdCarrera { get; set; }
        [SPResponseColumnName("tprog_desc")]
        public string NombreLegal { get; set; }
        [SPResponseColumnName("numero")]
        public string RVOE { get; set; }
        [SPResponseColumnName("fechaExpedicion")]
        public string FechaExpedicion { get; set; }
        [SPResponseColumnName("idTipoPeriodo")]
        public string IdPeriodo { get; set; }
        [SPResponseColumnName("tprog_periodicidad")]
        public string Periodicidad { get; set; }
        [SPResponseColumnName("clavePlan")]
        public string ClavePlanEstudios { get; set; }
        [SPResponseColumnName("idNivelEstudios")]
        public string IdNivelEstudios { get; set; }
        [SPResponseColumnName("tnive_desc")]
        public string DescripcionNivelEstudios { get; set; }
        [SPResponseColumnName("totalCreditos")]
        public string Creditos { get; set; }
        [SPResponseColumnName("calificacionMinima")]
        public string CalificacionMinima { get; set; }
        [SPResponseColumnName("calificacionMaxima")]
        public string CalificacionMaxima { get; set; }
        [SPResponseColumnName("calificacionMinimaAprobatoria")]
        public string CalificacionMinimaAprobatoria { get; set; }
    }

    [SPName("P_TIPO_CERT_TCEEL")]
    public class ModelTipoCertificadoTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelTipoCertificadoTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("IdTipoCertificacion")]
        public string IdTipoCertificacion { get; set; }
    }

    [SPName("P_OBTEN_DATOS_TOTALES_TCEEL")]
    public class ModelDatosTotalesTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelDatosTotalesTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("Asignaturas_total")]
        public string TotalMaterias { get; set; }
        [SPResponseColumnName("Total_promedio")]
        public string TotalPromedio { get; set; }
        [SPResponseColumnName("creditosObtenidos")]
        public string TotalCreditos { get; set; }
        [SPResponseColumnName("numeroCiclos")]
        public string NumeroCiclos { get; set; }
    }

    [SPName("P_DATOS_FUNCIONARIO_TCEEL")]
    public class ModelObtenDatosFuncionarioTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenDatosFuncionarioTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("nombre")]
        public string Nombre { get; set; }
        [SPResponseColumnName("primerApellido")]
        public string ApellidoP { get; set; }
        [SPResponseColumnName("segundoApellido")]
        public string ApellidoM { get; set; }
        [SPResponseColumnName("curp")]
        public string CURP { get; set; }
        [SPResponseColumnName("tfuca_desc")]
        public string DescripcionCargo { get; set; }
        [SPResponseColumnName("idCargo")]
        public string IdCargo { get; set; }
    }

    [SPName("P_DATOS_MATERIA_TCEEL")]
    public class ModelObtenDatosMateriaTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenDatosMateriaTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("tkard_tmate_clave")]
        public string Clave { get; set; }
        [SPResponseColumnName("idAsignatura")]
        public string IdAsignatura { get; set; }
        [SPResponseColumnName("tmate_desc")]
        public string Materia { get; set; }
        [SPResponseColumnName("ciclo")]
        public string Ciclo { get; set; }
        [SPResponseColumnName("tkard_tpees_clave")]
        public string CicloSAES { get; set; }
        [SPResponseColumnName("calificacion")]
        public string Calificacion { get; set; }
        [SPResponseColumnName("ttiac_desc")]
        public string Acreditacion { get; set; }
        [SPResponseColumnName("tipo_mate")]
        public string ClaveTipo { get; set; }
        [SPResponseColumnName("idObservaciones")]
        public string IdObservaciones { get; set; }
        [SPResponseColumnName("idTipoAsignatura")]
        public string IdTipoAsignatura { get; set; }
        [SPResponseColumnName("creditos")]
        public string Creditos { get; set; }
    }

    [SPName("P_OBTEN_CAMPUS_TCEEL")]
    public class ModelObtenCampusTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenCampusTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("IdCampus")]
        public string IdCampus { get; set; }
        [SPResponseColumnName("IdEntidadFederativa")]
        public string IdEntidadFederativa { get; set; }
        [SPResponseColumnName("tcamp_clave")]
        public string ClaveCampus { get; set; }
        [SPResponseColumnName("tcamp_desc")]
        public string DescripcionCampus { get; set; }
    }

    [SPName("P_OBTEN_ALUMNO_TCEEL")]
    public class ModelObtenAlumnoTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
    }
    public class ModelObtenAlumnoTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("nombre")]
        public string Nombre { get; set; }
        [SPResponseColumnName("primerApellido")]
        public string PrimerApellido { get; set; }
        [SPResponseColumnName("segundoApellido")]
        public string SegundoApellido { get; set; }
        [SPResponseColumnName("idGenero")]
        public string IdGenero { get; set; }
        [SPResponseColumnName("genero")]
        public string Genero { get; set; }
        [SPResponseColumnName("fechaNacimiento")]
        public string FechaNacimiento { get; set; }
        [SPResponseColumnName("curp")]
        public string CURP { get; set; }
    }

    [SPName("P_OBTEN_MATERIA_PROGRAMA")]
    public class ModelMateriaNoAcreditadaRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_TPROG_CLAVE", 1)]
        public string ClavePrograma { get; set; }
    }
    public class ModelMateriaNoAcreditadaResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("MATERIA")]
        public string Descripcion { get; set; }
    }
    [SPName("P_OBTEN_ACREDITACION")]
    public class ModelAcreditacionRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_NADA", 0)]
        public string Nada { get; set; }
    }
    public class ModelAcreditacionResponse : BaseModelResponse
    {
        [SPResponseColumnName("ACREDITA")]
        public string Clave { get; set; }
        [SPResponseColumnName("DESCRIPCION")]
        public string Descripcion { get; set; }
    }

    [SPName("P_TODO_PERIODO")]
    public class ModelTodoPeriodoRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_nada", 0)]
        public string Nada { get; set; }
    }
    public class ModelTodoPeriodoResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("DESCRIPCION")]
        public string Descripcion { get; set; }
    }

    [SPName("P_VALIDA_FOLIO_TCEEL")]
    public class ModelObtenValidaFolioTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenValidaFolioTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("cuentafolio")]
        public string CuentaFolio { get; set; }
    }

    [SPName("P_OBTIENE_FOLIO_TCEEL")]
    public class ModelObtenFolioTCEELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenFolioTCEELResponse : BaseModelResponse
    {
        [SPResponseColumnName("folio")]
        public string Folio { get; set; }
    }

    [SPName("ObtenerListaMenu")]
    public class ModelObtenMenusRequest : BaseModelRequest
    {

    }
    public class ModelObtenMenusResponse : BaseModelResponse
    {
        [SPResponseColumnName("tmenu_clave")]
        public string menu_clave { get; set; }
        [SPResponseColumnName("tmenu_desc")]
        public string menu_desc { get; set; }

        [SPResponseColumnName("tmenu_usuario")]
        public string menu_usuario { get; set; }

        [SPResponseColumnName("tmenu_date")]
        public string menu_date { get; set; }


        [SPResponseColumnName("tmenu_estatus")]
        public string menu_estatus { get; set; }
    }




    [SPName("P_QRY_TCOPO_UBICACION")]
    public class ModelObtenDatosCPRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }

        [SPParameterName("p_tcopo_testa_clave", 1)]
        public string tcopo_testa_clave { get; set; }

        [SPParameterName("p_tcopo_tdele_clave", 2)]
        public string tcopo_tdele_clave { get; set; }
    }
    public class ModelObtenDatosCPResponse : BaseModelResponse
    {
        [SPResponseColumnName("tcopo_testa_clave")]
        public string testa { get; set; }

        [SPResponseColumnName("tcopo_tdele_clave")]
        public string tdele { get; set; }

        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }


    [SPName("P_QRY_TCOPO_COLONIAS")]
    public class ModelObtenColoniasCPRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
    }
    public class ModelObtenColoniasCPResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }



    [SPName("ObtenerListaRoles")]
    public class ModelObtenRolesRequest : BaseModelRequest
    {

    }
    public class ModelObtenRolesResponse : BaseModelResponse
    {
        [SPResponseColumnName("trole_clave")]
        public string trole_clave { get; set; }
        [SPResponseColumnName("Nombre")]
        public string trole_desc { get; set; }

        [SPResponseColumnName("trole_estatus")]
        public string trole_estatus { get; set; }
    }




    [SPName("P_QRY_MENU")]
    public class ModelObtenMenuRequest : BaseModelRequest
    {

    }
    public class ModelObtenMenuResponse : BaseModelResponse
    {
        [SPResponseColumnName("tmenu_id")]
        public string tmenu_id { get; set; }

        [SPResponseColumnName("tmenu_desc")]
        public string tmenu_desc { get; set; }

        [SPResponseColumnName("tmenu_icono")]
        public string tmenu_icono { get; set; }



    }

    [SPName("P_QRY_SUBMENU")]
    public class ModelObtenSubMenuRequest : BaseModelRequest
    {
        [SPParameterName("P_Nivel", 0)]
        public int Nivel { get; set; }

        [SPParameterName("P_Menu", 1)]
        public string Menu { get; set; }
    }
    public class ModelObtenSubMenuResponse : BaseModelResponse
    {
        [SPResponseColumnName("tmede_padre")]
        public string tmede_padre { get; set; }

        [SPResponseColumnName("tmede_desc")]
        public string tmede_desc { get; set; }

        [SPResponseColumnName("tmede_forma")]
        public string tmede_forma { get; set; }
    }


    [SPName("Insertar_Rol")]
    public class ModelInsertarRol : BaseModelRequest
    {

        [SPParameterName("P_CVE", 0)]
        public string trole_clave { get; set; }

        [SPParameterName("P_ROL", 1)]
        public string trole_desc { get; set; }

        [SPParameterName("P_USU_ALTA", 2)]
        public string trole_usuario { get; set; }

        [SPParameterName("P_ESTATUS", 3)]
        public string trole_estatus { get; set; }
    }

    [SPName("Insertar_Nivel")]
    public class ModelInsertarNivel : BaseModelRequest
    {

        [SPParameterName("p_tnive_clave", 0)]
        public string tnive_clave { get; set; }

        [SPParameterName("p_tnive_desc", 1)]
        public string tnive_desc { get; set; }

        [SPParameterName("p_tnie_user", 2)]
        public string tnie_user { get; set; }

        [SPParameterName("p_tnive_estatus", 3)]
        public string tnive_estatus { get; set; }
    }

    public class ModelInsertarNivelResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }



    [SPName("P_INS_TDELE")]
    public class ModelInsertarTdele : BaseModelRequest
    {

        [SPParameterName("p_tdele_clave", 0)]
        public string tdele_clave { get; set; }

        [SPParameterName("p_tdele_desc", 1)]
        public string tdele_desc { get; set; }

        [SPParameterName("p_tdele_testa_clave", 2)]
        public string tdele_testa_clave { get; set; }

        [SPParameterName("p_tdele_tpais_clave", 3)]
        public string tdele_tpais_clave { get; set; }

        [SPParameterName("p_tdele_user", 4)]
        public string tdele_user { get; set; }

        [SPParameterName("p_tdele_estatus", 5)]
        public string tdele_estatus { get; set; }
    }

    public class ModelInsertarTdeleResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }



    [SPName("P_INS_TCOLE")]
    public class ModelInsertarTcole : BaseModelRequest
    {
        [SPParameterName("p_tcole_clave", 0)]
        public string tcole_clave { get; set; }

        [SPParameterName("p_tcole_desc", 1)]
        public string tcole_desc { get; set; }

        [SPParameterName("p_tcole_user", 2)]
        public string tcole_user { get; set; }

        [SPParameterName("p_tcole_estatus", 3)]
        public string tcole_estatus { get; set; }
    }

    public class ModelInsertarTcoleResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_INS_TAREA")]
    public class ModelInsertarTarea : BaseModelRequest
    {
        [SPParameterName("p_tarea_clave", 0)]
        public string tarea_clave { get; set; }

        [SPParameterName("p_tarea_desc", 1)]
        public string tarea_desc { get; set; }

        [SPParameterName("p_tarea_user", 2)]
        public string tarea_user { get; set; }

        [SPParameterName("p_tarea_ind_espec", 3)]
        public string tarea_ind_espec { get; set; }

        [SPParameterName("p_tarea_estatus", 4)]
        public string tarea_estatus { get; set; }
    }

    public class ModelInsertarTareaResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }


    [SPName("P_UPD_TAREA")]
    public class ModelEditarTarea : BaseModelRequest
    {
        [SPParameterName("p_tarea_clave", 0)]
        public string tarea_clave { get; set; }

        [SPParameterName("p_tarea_desc", 1)]
        public string tarea_desc { get; set; }

        [SPParameterName("p_tarea_user", 2)]
        public string tarea_user { get; set; }

        [SPParameterName("p_tarea_ind_espec", 3)]
        public string tarea_ind_espec { get; set; }

        [SPParameterName("p_tarea_estatus", 4)]
        public string tarea_estatus { get; set; }
    }



    [SPName("P_INS_TMODA")]
    public class ModelInsertarTModa : BaseModelRequest
    {
        [SPParameterName("p_tmoda_clave", 0)]
        public string tmoda_clave { get; set; }

        [SPParameterName("p_tmoda_desc", 1)]
        public string tmoda_desc { get; set; }

        [SPParameterName("p_tmoda_user", 2)]
        public string tmoda_user { get; set; }

        [SPParameterName("p_tmoda_estatus", 3)]
        public string tmoda_estatus { get; set; }
    }

    public class ModelInsertarTModaResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }


    [SPName("P_UPD_TCOLE")]
    public class ModelEditarTcole : BaseModelRequest
    {
        [SPParameterName("p_tcole_clave", 0)]
        public string tcole_clave { get; set; }

        [SPParameterName("p_tcole_desc", 1)]
        public string tcole_desc { get; set; }

        [SPParameterName("p_tcole_user", 2)]
        public string tcole_user { get; set; }

        [SPParameterName("p_tcole_estatus", 3)]
        public string tcole_estatus { get; set; }
    }



    [SPName("INS_TCAMP")]
    public class ModelInsertarCampus : BaseModelRequest
    {

        [SPParameterName("p_tcamp_clave", 0)]
        public string p_tcamp_clave { get; set; }

        [SPParameterName("p_tcamp_desc", 1)]
        public string p_tcamp_desc { get; set; }

        [SPParameterName("p_tcamp_calle", 2)]
        public string p_tcamp_calle { get; set; }

        [SPParameterName("p_tcamp_colonia", 3)]
        public string p_tcamp_colonia { get; set; }

        [SPParameterName("p_tcamp_tpais_clave", 4)]
        public string p_tcamp_tpais_clave { get; set; }

        [SPParameterName("p_tcamp_testa_clave", 5)]
        public string p_tcamp_testa_clave { get; set; }


        [SPParameterName("p_tcamp_tdele_clave", 6)]
        public string p_tcamp_tdele_clave { get; set; }

        [SPParameterName("p_tcamp_tcopo_clave", 7)]
        public string p_tcamp_tcopo_clave { get; set; }

        [SPParameterName("p_tcamp_user", 8)]
        public string p_tcamp_user { get; set; }

        [SPParameterName("p_tcamp_estatus", 9)]
        public string p_tcamp_estatus { get; set; }

        [SPParameterName("p_tcamp_abr", 10)]
        public string p_tcamp_abr { get; set; }

        [SPParameterName("p_tcamp_rfc", 11)]
        public string p_tcamp_rfc { get; set; }

        [SPParameterName("p_tcamp_incorp", 12)]
        public string p_tcamp_incorp { get; set; }
    }

    public class ModelInsertarCampusResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }


    [SPName("UPD_TCAMP")]
    public class ModelEditarCampus : BaseModelRequest
    {

        [SPParameterName("p_tcamp_clave", 0)]
        public string p_tcamp_clave { get; set; }

        [SPParameterName("p_tcamp_desc", 1)]
        public string p_tcamp_desc { get; set; }

        [SPParameterName("p_tcamp_calle", 2)]
        public string p_tcamp_calle { get; set; }

        [SPParameterName("p_tcamp_colonia", 3)]
        public string p_tcamp_colonia { get; set; }

        [SPParameterName("p_tcamp_tpais_clave", 4)]
        public string p_tcamp_tpais_clave { get; set; }

        [SPParameterName("p_tcamp_testa_clave", 5)]
        public string p_tcamp_testa_clave { get; set; }


        [SPParameterName("p_tcamp_tdele_clave", 6)]
        public string p_tcamp_tdele_clave { get; set; }

        [SPParameterName("p_tcamp_tcopo_clave", 7)]
        public string p_tcamp_tcopo_clave { get; set; }

        [SPParameterName("p_tcamp_user", 8)]
        public string p_tcamp_user { get; set; }

        [SPParameterName("p_tcamp_estatus", 9)]
        public string p_tcamp_estatus { get; set; }

        [SPParameterName("p_tcamp_abr", 10)]
        public string p_tcamp_abr { get; set; }

        [SPParameterName("p_tcamp_rfc", 11)]
        public string p_tcamp_rfc { get; set; }

        [SPParameterName("p_tcamp_incorp", 12)]
        public string p_tcamp_incorp { get; set; }
    }


    [SPName("P_UPD_TDELE")]
    public class ModelEditarTdele : BaseModelRequest
    {

        [SPParameterName("p_tdele_clave", 0)]
        public string tdele_clave { get; set; }

        [SPParameterName("p_tdele_desc", 1)]
        public string tdele_desc { get; set; }

        [SPParameterName("p_tdele_testa_clave", 2)]
        public string tdele_testa_clave { get; set; }

        [SPParameterName("p_tdele_tpais_clave", 3)]
        public string tdele_tpais_clave { get; set; }

        [SPParameterName("p_tdele_user", 4)]
        public string tdele_user { get; set; }

        [SPParameterName("p_tdele_estatus", 5)]
        public string tdele_estatus { get; set; }
    }


    [SPName("Actualiza_Rol")]
    public class ModelEditarRol : BaseModelRequest
    {
        [SPParameterName("P_CVE", 0)]
        public string trole_clave { get; set; }

        [SPParameterName("P_ROL", 1)]
        public string trole_desc { get; set; }

        [SPParameterName("P_ESTATUS", 2)]
        public string trole_estatus { get; set; }
    }
    [SPName("ObtenerListaCatPreguntas")]
    public class ModelObtenPreguntaRequest : BaseModelRequest
    {

    }
    public class ModelObtenPreguntaResponse : BaseModelResponse
    {
        [SPResponseColumnName("tpreg_clave")]
        public string preg_clave { get; set; }

        [SPResponseColumnName("tpreg_desc")]
        public string preg_desc { get; set; }

        [SPResponseColumnName("tpreg_estatus")]
        public string preg_estatus { get; set; }
    }


    [SPName("Insertar_Cat_Pregunta")]
    public class ModelInsertarPregunta : BaseModelRequest
    {

        [SPParameterName("P_CVE", 0)]
        public int preg_clave { get; set; }

        [SPParameterName("P_DESCRIPCION", 1)]
        public string preg_desc { get; set; }

        [SPParameterName("P_ESTATUS", 2)]
        public string preg_estatus { get; set; }
    }

    [SPName("Actualiza_Pregunta")]
    public class ModelEditarPregunta : BaseModelRequest
    {

        [SPParameterName("P_CVE", 0)]
        public int preg_clave { get; set; }

        [SPParameterName("P_DESCRIPCION", 1)]
        public string preg_desc { get; set; }

        [SPParameterName("P_ESTATUS", 2)]
        public string preg_estatus { get; set; }
    }

    [SPName("P_QRY_TPEES")]
    public class ModelObtenPeriodosEscolaresRequest : BaseModelRequest
    {

    }
    public class ModelObtenPeriodosEscolaresResponse : BaseModelResponse
    {
        [SPResponseColumnName("tpees_clave")]
        public string clave { get; set; }

        [SPResponseColumnName("tpees_desc")]
        public string nombre { get; set; }

        [SPResponseColumnName("tpees_ofic")]
        public string oficial { get; set; }

        [SPResponseColumnName("tpees_inicio")]
        public string fecha_ini { get; set; }

        [SPResponseColumnName("tpees_fin")]
        public string fecha_fin { get; set; }

        [SPResponseColumnName("tpees_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("desc_estatus")]
        public string desc_estatus { get; set; }

        [SPResponseColumnName("tpees_date")]
        public string fecha { get; set; }
    }

    [SPName("P_QRY_TPEES_TEPCB")]
    public class ModelObtenPeriodosProgramaRequest : BaseModelRequest
    {

    }
    public class ModelObtenPeriodosProgramaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }




    [SPName("P_QRY_TPCBE_CONCEPTO")]
    public class ModelObtenConceptosDescuentoRequest : BaseModelRequest
    {

    }
    public class ModelObtenConceptosDescuentoResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }


    [SPName("P_QRY_TESPR")]
    public class ModelObtenEscuelasProcedenciaRequest : BaseModelRequest
    {

    }
    public class ModelObtenEscuelasProcedenciaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("descripcion")]
        public string descripcion { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("estatus")]
        public string estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }

    [SPName("P_QRY_TPEES_VIGENTES")]
    public class ModelObtenPeriodosEscolaresVigentesRequest : BaseModelRequest
    {

    }
    public class ModelObtenPeriodosEscolaresVigentesResponse : BaseModelResponse
    {
        [SPResponseColumnName("tpees_clave")]
        public string clave { get; set; }

        [SPResponseColumnName("tpees_desc")]
        public string nombre { get; set; }

        [SPResponseColumnName("tpees_ofic")]
        public string oficial { get; set; }

        [SPResponseColumnName("tpees_inicio")]
        public string fecha_ini { get; set; }

        [SPResponseColumnName("tpees_fin")]
        public string fecha_fin { get; set; }

        [SPResponseColumnName("tpees_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("desc_estatus")]
        public string desc_estatus { get; set; }

        [SPResponseColumnName("tpees_date")]
        public string fecha { get; set; }
    }

    [SPName("P_QRY_TPEES_ACTIVOS")]
    public class ModelObtenPeriodosEscolaresActivosRequest : BaseModelRequest
    {

    }
    public class ModelObtenPeriodosEscolaresActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("tpees_clave")]
        public string clave { get; set; }

        [SPResponseColumnName("tpees_desc")]
        public string nombre { get; set; }

        [SPResponseColumnName("tpees_ofic")]
        public string oficial { get; set; }

        [SPResponseColumnName("tpees_inicio")]
        public string fecha_ini { get; set; }

        [SPResponseColumnName("tpees_fin")]
        public string fecha_fin { get; set; }

        [SPResponseColumnName("tpees_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("desc_estatus")]
        public string desc_estatus { get; set; }

        [SPResponseColumnName("tpees_date")]
        public string fecha { get; set; }
    }

    [SPName("Insertar_Periodo_Escolar")]
    public class ModelInsertarPeriodoEscolar : BaseModelRequest
    {

        [SPParameterName("p_clave", 0)]
        public int clave { get; set; }

        [SPParameterName("P_Descripcion", 1)]
        public string descripcion { get; set; }

        [SPParameterName("P_Ofic", 2)]
        public string oficio { get; set; }

        [SPParameterName("P_Inicio", 3)]
        public string fecha_inicio { get; set; }

        [SPParameterName("P_Fin", 4)]
        public string fecha_fin { get; set; }

        [SPParameterName("P_Usuario", 5)]
        public string usuario { get; set; }

        [SPParameterName("P_Estatus", 6)]
        public string estatus { get; set; }
    }

    [SPName("Actualiza_Periodo_Escolar")]
    public class ModelEditarPeriodoEscolar : BaseModelRequest
    {

        [SPParameterName("p_clave", 0)]
        public int clave { get; set; }

        [SPParameterName("P_Descripcion", 1)]
        public string descripcion { get; set; }

        [SPParameterName("P_Ofic", 2)]
        public string oficio { get; set; }

        [SPParameterName("P_Inicio", 3)]
        public string fecha_inicio { get; set; }

        [SPParameterName("P_Fin", 4)]
        public string fecha_fin { get; set; }

        [SPParameterName("P_Usuario", 5)]
        public string usuario { get; set; }

        [SPParameterName("P_Estatus", 6)]
        public string estatus { get; set; }
    }

    [SPName("P_QRY_TPAIS")]//ObtenerListaPaises
    public class ModelObtenPaisesRequest : BaseModelRequest
    {

    }
    public class ModelObtenPaisesResponse : BaseModelResponse
    {
        [SPResponseColumnName("TPAIS_CLAVE")]
        public string clave { get; set; }

        [SPResponseColumnName("TPAIS_DESC")]
        public string nombre { get; set; }

        [SPResponseColumnName("TPAIS_GENTIL")]
        public string gentil { get; set; }

        [SPResponseColumnName("TPAIS_ESTATUS")]
        public string estatus_code { get; set; }

        [SPResponseColumnName("estatus")]
        public string estatus { get; set; }

        [SPResponseColumnName("TPAIS_DATE")]
        public string fecha { get; set; }
    }

    [SPName("P_INS_TPAIS")]//Insertar_Pais
    public class ModelInsertarPais : BaseModelRequest
    {

        [SPParameterName("p_pais_clave", 0)]
        public string clave { get; set; }

        [SPParameterName("p_pais_desc", 1)]
        public string nombre { get; set; }

        [SPParameterName("p_pais_gentil", 2)]
        public string gentil { get; set; }

        [SPParameterName("p_pais_tuser_clave", 3)]
        public string usuario { get; set; }

        [SPParameterName("p_pais_estatus", 4)]
        public string estatus { get; set; }
    }

    [SPName("P_UPD_TPAIS")]//Actualiza_Pais
    public class ModelEditarPais : BaseModelRequest
    {

        [SPParameterName("p_pais_clave", 0)]
        public string clave { get; set; }

        [SPParameterName("p_pais_desc", 1)]
        public string nombre { get; set; }

        [SPParameterName("p_pais_gentil", 2)]
        public string gentil { get; set; }

        [SPParameterName("p_pais_tuser_clave", 3)]
        public string usuario { get; set; }

        [SPParameterName("p_pais_estatus", 4)]
        public string estatus { get; set; }
    }

    [SPName("P_QRY_TPAIS_ACTIVOS")]//ObtenerListaPaises
    public class ModelObtenPaisesActivosRequest : BaseModelRequest
    {

    }
    public class ModelObtenPaisesActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string clave { get; set; }

        [SPResponseColumnName("Nombre")]
        public string nombre { get; set; }
    }

    [SPName("P_QRY_TDELE")]//ObtenerListaDelegaciones
    public class ModelObtenDelegacionesRequest : BaseModelRequest
    {
        [SPParameterName("P_Pais", 0)]
        public string pais { get; set; }

        [SPParameterName("P_Municipio", 1)]
        public string municipio { get; set; }


        [SPParameterName("P_Estatus", 2)]
        public string estatus { get; set; }

    }
    public class ModelObtenDelegacionesResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string clave { get; set; }

        [SPResponseColumnName("Nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_pais")]
        public string c_pais { get; set; }

        [SPResponseColumnName("pais")]
        public string pais { get; set; }

        [SPResponseColumnName("c_estado")]
        public string c_estado { get; set; }

        [SPResponseColumnName("estado")]
        public string estado { get; set; }

        [SPResponseColumnName("estatus_code")]
        public string estatus_code { get; set; }

        [SPResponseColumnName("estatus")]
        public string fecha { get; set; }

        [SPResponseColumnName("fecha")]
        public string estatus { get; set; }
    }

    [SPName("P_QRY_TDELE_TESTA")]//ObtenerListaMunicipios
    public class ModelObtenMunicipiosRequest : BaseModelRequest
    {
        [SPParameterName("P_TPAIS", 0)]
        public string pais { get; set; }

        [SPParameterName("P_TESTA", 1)]
        public string estado { get; set; }
    }

    public class ModelObtenMunicipiosResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string clave { get; set; }

        [SPResponseColumnName("Nombre")]
        public string nombre { get; set; }
    }


    [SPName("P_QRY_TESTA_TPAIS")]//ObtenerListaEstados
    public class ModelObtenEstadosRequest : BaseModelRequest
    {
        [SPParameterName("P_Pais", 0)]
        public string pais { get; set; }
    }
    public class ModelObtenEstadosResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string clave { get; set; }

        [SPResponseColumnName("Descripcion")]
        public string descripcion { get; set; }
    }

    [SPName("ObtenerListaEntidadFederativa")]
    public class ModelObtenEntFederativaRequest : BaseModelRequest
    {

    }
    public class ModelObtenEntFederativaResponse : BaseModelResponse
    {
        [SPResponseColumnName("tpais_clave")]
        public string clave { get; set; }

        [SPResponseColumnName("tpais_descripcion")]
        public string nombre { get; set; }

        [SPResponseColumnName("trole_estatus")]
        public string c_pais { get; set; }
    }

    [SPName("P_QRY_CAMPUS")]
    public class ModelObtenCampusRequest : BaseModelRequest
    {

    }
    public class ModelObtenCampusResponse : BaseModelResponse
    {
        [SPResponseColumnName("IDCampus")]
        public string Clave { get; set; }

        [SPResponseColumnName("Nombre")]
        public string Descripcion { get; set; }
    }



    [SPName("P_QRY_CAT_CAMPUS")]
    public class ModelObtenCatCampusRequest : BaseModelRequest
    {

    }
    public class ModelObtenCatCampusResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("Nombre")]
        public string Nombre { get; set; }

        [SPResponseColumnName("Abreviatura")]
        public string Abreviatura { get; set; }

        [SPResponseColumnName("RFC")]
        public string RFC { get; set; }

        [SPResponseColumnName("Estatus_Code")]
        public string Estatus_Code { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("Fecha")]
        public string Fecha { get; set; }

        [SPResponseColumnName("C_Pais")]
        public string C_Pais { get; set; }

        [SPResponseColumnName("C_Estado")]
        public string C_Estado { get; set; }

        [SPResponseColumnName("N_Estado")]
        public string N_Estado { get; set; }

        [SPResponseColumnName("C_Dele")]
        public string C_Dele { get; set; }

        [SPResponseColumnName("N_Dele")]
        public string N_Dele { get; set; }

        [SPResponseColumnName("Colonia")]
        public string Colonia { get; set; }

        [SPResponseColumnName("Direccion")]
        public string Direccion { get; set; }

        [SPResponseColumnName("ZIP")]
        public string ZIP { get; set; }
    }

    [SPName("P_QRY_TURNO")]
    public class ModelObtenTurnosRequest : BaseModelRequest
    {

    }
    public class ModelObtenTurnosResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("Descripcion")]
        public string Descripcion { get; set; }
    }

    [SPName("P_QRY_COMBO_GRALS")]
    public class ModelObtenComboRequest : BaseModelRequest
    {
        [SPParameterName("P_Tipo", 0)]
        public string Tipo { get; set; }

        [SPParameterName("P_Parametro1", 1)]
        public string Parametro1 { get; set; }
    }
    public class ModelObtenComboResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("Descripcion")]
        public string Descripcion { get; set; }
    }




    [SPName("P_UPD_TBAAP")]//Actualiza_Pais
    public class ModelEditarBaja : BaseModelRequest
    {

        [SPParameterName("p_pais_clave", 0)]
        public string clave { get; set; }

        [SPParameterName("p_pais_desc", 1)]
        public string nombre { get; set; }

        [SPParameterName("p_pais_gentil", 2)]
        public string gentil { get; set; }

        [SPParameterName("p_pais_tuser_clave", 3)]
        public string usuario { get; set; }

        [SPParameterName("p_pais_estatus", 4)]
        public string estatus { get; set; }
    }

    [SPName("P_QRY_TBAAP")]//ObtenerListaPaises
    public class ModelObtenBajasRequest : BaseModelRequest
    {
        [SPParameterName("p_tbaap_tpers_num", 0)]
        public string p_tbaap_tpers_num { get; set; }

        [SPParameterName("p_tbaap_tprog_clave", 1)]
        public string tbaap_tprog_clave { get; set; }

    }
    public class ModelObtenBajasResponse : BaseModelResponse
    {
        [SPResponseColumnName("tpers_nombre")]
        public string tpers_nombre { get; set; }

        [SPResponseColumnName("tbaap_tprog_clave")]
        public string tbaap_tprog_clave { get; set; }

        [SPResponseColumnName("tbaap_tpees_clave")]
        public string tbaap_tpees_clave { get; set; }

        [SPResponseColumnName("ttiba_desc")]
        public string ttiba_desc { get; set; }

        [SPResponseColumnName("tbaap_fecha_baja")]
        public string tbaap_fecha_baja { get; set; }

        [SPResponseColumnName("tbaap_estima")]
        public string tbaap_estima { get; set; }

        [SPResponseColumnName("tbaap_ttiba_clave")]
        public string tbaap_ttiba_clave { get; set; }
    }

    [SPName("P_QRY_TSEQN")]
    public class ModelSecuenciasRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CAMPUS", 0)]
        public string Campus { get; set; }
    }
    public class ModelSecuenciasResponse : BaseModelResponse
    {
        [SPResponseColumnName("seq")]
        public string seq { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("tcseq_numero")]
        public string tcseq_numero { get; set; }

        [SPResponseColumnName("tcseq_longitud")]
        public string tcseq_longitud { get; set; }

        [SPResponseColumnName("campus")]
        public string campus { get; set; }
    }

    [SPName("P_QRY_TTELE")]
    public class ModelTelefonoRequest : BaseModelRequest
    {

    }
    public class ModelTelefonoResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_QRY_TTELE_ACTIVOS")]
    public class ModelTelefonoActivosRequest : BaseModelRequest
    {

    }
    public class ModelTelefonoActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }



    [SPName("P_QRY_TTASA_ACTIVOS")]
    public class ModelTasaActivosRequest : BaseModelRequest
    {

    }
    public class ModelTasaActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("descripcion")]
        public string descripcion { get; set; }
    }


    [SPName("P_QRY_TORCA")]
    public class ModelTorcaRequest : BaseModelRequest
    {

    }
    public class ModelTorcaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("estatus")]
        public string estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }

    [SPName("P_QRY_TORCA_ACTIVOS")]
    public class ModelTorcaActivosRequest : BaseModelRequest
    {

    }
    public class ModelTorcaActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("estatus")]
        public string estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_QRY_TFUCA")]
    public class ModelTfucaRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CAMPUS", 0)]
        public string Campus { get; set; }
    }
    public class ModelTfucaResponse : BaseModelResponse
    {

        [SPResponseColumnName("tfuca_clave")]
        public string tfuca_clave { get; set; }

        [SPResponseColumnName("tfuca_desc")]
        public string tfuca_desc { get; set; }

        [SPResponseColumnName("tfuca_nombre")]
        public string tfuca_nombre { get; set; }

        [SPResponseColumnName("tfuca_paterno")]
        public string tfuca_paterno { get; set; }

        [SPResponseColumnName("tfuca_materno")]
        public string tfuca_materno { get; set; }

        [SPResponseColumnName("tfuca_curp")]
        public string tfuca_curp { get; set; }

        [SPResponseColumnName("tfuca_tuser_clave")]
        public string tfuca_tuser_clave { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }

        [SPResponseColumnName("estatus")]
        public string estatus { get; set; }

        [SPResponseColumnName("tfuca_tcamp_clave")]
        public string tfuca_tcamp_clave { get; set; }
    }

    [SPName("P_QRY_TTIIN_ACTIVOS")]
    public class ModelTtiinActivosRequest : BaseModelRequest
    {

    }
    public class ModelTtiinActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("descripcion")]
        public string descripcion { get; set; }
    }


    [SPName("P_UPD_TTELE")]//Actualiza_Telefono
    public class ModelEditarTelefono : BaseModelRequest
    {

        [SPParameterName("Clave_ttele", 0)]
        public string clave_ttele { get; set; }

        [SPParameterName("Desc_ttele", 1)]
        public string desc_ttele { get; set; }

        [SPParameterName("Usuario", 2)]
        public string usuario { get; set; }

        [SPParameterName("Estatus", 3)]
        public string estatus { get; set; }

    }


    [SPName("P_INS_TTELE2")]//Actualiza Telefono
    public class ModelInsertarTelefono : BaseModelRequest
    {

        [SPParameterName("Clave_ttele", 0)]
        public string clave_ttele { get; set; }

        [SPParameterName("Desc_ttele", 1)]
        public string desc_ttele { get; set; }

        [SPParameterName("Usuario", 2)]
        public string usuario { get; set; }

        [SPParameterName("Estatus", 3)]
        public string estatus { get; set; }

    }
    public class ModelInsertarTelefonoResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_QRY_TMAIL")]
    public class ModelEMailRequest : BaseModelRequest
    {

    }
    public class ModelEMailResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_QRY_TMAIL_ACTIVOS")]
    public class ModelEMailActivosRequest : BaseModelRequest
    {

    }
    public class ModelEMailActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_QRY_TCALI")]
    public class ModelTCaliRequest : BaseModelRequest
    {
        [SPParameterName("P_Nivel", 0)]
        public string Nivel { get; set; }

    }
    public class ModelTCaliResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("puntos")]
        public string puntos { get; set; }

        [SPResponseColumnName("aprobatoria")]
        public string aprobatoria { get; set; }

        [SPResponseColumnName("promedio")]
        public string promedio { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_QRY_TNIVE_ACTIVOS")]
    public class ModelNivelesActivosRequest : BaseModelRequest
    {

    }
    public class ModelNivelesActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }

    [SPName("P_QRY_TCOLE_ACTIVOS")]
    public class ModelColegiosActivosRequest : BaseModelRequest
    {

    }
    public class ModelColegiosActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }


    [SPName("P_QRY_TCAPR_TCOLE")]
    public class ModelColegiosporProgramaRequest : BaseModelRequest
    {
        [SPParameterName("p_tcapr_tcamp_clave", 0)]
        public string tcapr_tcamp_clave { get; set; }

        [SPParameterName("p_tprog_tnive_clave", 1)]
        public string tprog_tnive_clave { get; set; }
    }
    public class ModelColegiosporProgramaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }

    [SPName("P_QRY_TCAPR_TMODA")]
    public class ModelModalidadporProgramaRequest : BaseModelRequest
    {
        [SPParameterName("p_tcapr_tcamp_clave", 0)]
        public string tcapr_tcamp_clave { get; set; }

        [SPParameterName("p_tprog_tnive_clave", 1)]
        public string tprog_tnive_clave { get; set; }

        [SPParameterName("p_tprog_tcole_clave", 2)]
        public string tprog_tcole_clave { get; set; }
    }
    public class ModelModalidadporProgramaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }

    [SPName("P_QRY_TCAPR_TPROG")]
    public class ModelTcaprProgramaRequest : BaseModelRequest
    {
        [SPParameterName("p_tcapr_tcamp_clave", 0)]
        public string tcapr_tcamp_clave { get; set; }

        [SPParameterName("p_tprog_tnive_clave", 1)]
        public string tprog_tnive_clave { get; set; }

        [SPParameterName("p_tprog_tcole_clave", 2)]
        public string tprog_tcole_clave { get; set; }

        [SPParameterName("p_tprog_tmoda_clave", 3)]
        public string tprog_tmoda_clave { get; set; }
    }
    public class ModelTcaprProgramaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }


    [SPName("P_INS_TMAIL2")]//Actualiza Correo
    public class ModelInsertarEMail : BaseModelRequest
    {

        [SPParameterName("Clave_tmail", 0)]
        public string clave_tmail { get; set; }

        [SPParameterName("Desc_tmail", 1)]
        public string desc_tmail { get; set; }

        [SPParameterName("Usuario", 2)]
        public string usuario { get; set; }

        [SPParameterName("Estatus", 3)]
        public string estatus { get; set; }

    }
    public class ModelInsertarEMailResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TMAIL")]//Actualiza EMail
    public class ModelEditarEMail : BaseModelRequest
    {

        [SPParameterName("Clave_tmail", 0)]
        public string clave_tmail { get; set; }

        [SPParameterName("Desc_tmail", 1)]
        public string desc_tmail { get; set; }

        [SPParameterName("Usuario", 2)]
        public string usuario { get; set; }

        [SPParameterName("Estatus", 3)]
        public string estatus { get; set; }

    }

    [SPName("P_QRY_TCONT")]
    public class ModelContactosRequest : BaseModelRequest
    {
    }
    public class ModelContactosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }

    [SPName("P_INS_TCONT2")]//Insertar Contacto
    public class ModelInsertarContacto : BaseModelRequest
    {

        [SPParameterName("Clave_tcont", 0)]
        public string clave_tcont { get; set; }

        [SPParameterName("Desc_tcont", 1)]
        public string desc_tcont { get; set; }

        [SPParameterName("Usuario", 2)]
        public string usuario { get; set; }

        [SPParameterName("Estatus", 3)]
        public string estatus { get; set; }

    }
    public class ModelInsertarContactoResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TCONT")]//Actualiza_Pais
    public class ModelEditarContacto : BaseModelRequest
    {

        [SPParameterName("clave_tcont", 0)]
        public string clave_tcont { get; set; }

        [SPParameterName("desc_tcont", 1)]
        public string desc_tcont { get; set; }

        [SPParameterName("Usuario", 2)]
        public string usuario { get; set; }

        [SPParameterName("Estatus", 3)]
        public string estatus { get; set; }
    }

    [SPName("P_QRY_TDIRE")]
    public class ModeltdireRequest : BaseModelRequest
    {

    }

    public class ModeltdireResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
        [SPResponseColumnName("C_ESTATUS")]
        public string C_ESTATUS { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string ESTATUS { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }

    }


    [SPName("P_QRY_TDIRE_ACTIVOS")]
    public class ModelTdireActivosRequest : BaseModelRequest
    {

    }

    public class ModelTdireActivosResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
        [SPResponseColumnName("C_ESTATUS")]
        public string C_ESTATUS { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string ESTATUS { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }

    }


    [SPName("P_QRY_TCOPO")]
    public class ModeltpocoRequest : BaseModelRequest
    {
        [SPParameterName("P_Pais", 0)]
        public string p_pais { get; set; }

        [SPParameterName("P_Estado", 1)]
        public string p_estado { get; set; }

        [SPParameterName("P_Municipio", 2)]
        public string p_municipio { get; set; }
    }
    public class ModeltpocoResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }

        [SPResponseColumnName("COLONIA")]
        public string nombre { get; set; }

        [SPResponseColumnName("C_PAIS")]
        public string c_pais { get; set; }

        [SPResponseColumnName("PAIS")]
        public string pais { get; set; }

        [SPResponseColumnName("C_ESTADO")]
        public string c_estado { get; set; }

        [SPResponseColumnName("ESTADO")]
        public string estado { get; set; }

        [SPResponseColumnName("C_DELEGACION")]
        public string c_delegacion { get; set; }

        [SPResponseColumnName("DELEGACION")]
        public string delegacion { get; set; }

        [SPResponseColumnName("ESTATUS_CODE")]
        public string estatus_code { get; set; }

        [SPResponseColumnName("ESTATUS")]
        public string estatus { get; set; }

        [SPResponseColumnName("FECHA")]
        public string fecha { get; set; }
    }

    [SPName("P_UPD_TCOPO")]
    public class ModelUpdtpoco : BaseModelRequest
    {
        [SPParameterName("P_Usuario", 0)]
        public string usuario { get; set; }

        [SPParameterName("P_Clave", 1)]
        public string clave { get; set; }

        [SPParameterName("P_Estatus", 2)]
        public string estatus { get; set; }

        [SPParameterName("P_Pais", 3)]
        public string pais { get; set; }

        [SPParameterName("P_Estado", 4)]
        public string estado { get; set; }

        [SPParameterName("P_Municipio", 5)]
        public string municipio { get; set; }

        [SPParameterName("p_descripcion", 6)]
        public string descripcion { get; set; }

    }

    [SPName("P_INS_TCOPO")]
    public class ModelInstcopo : BaseModelRequest
    {

        [SPParameterName("P_Clave", 0)]
        public string clave { get; set; }

        [SPParameterName("P_DESCRIPCION", 1)]
        public string descripcion { get; set; }

        [SPParameterName("P_Estatus", 2)]
        public string estatus { get; set; }

        [SPParameterName("P_Pais", 3)]
        public string pais { get; set; }

        [SPParameterName("P_Estado", 4)]
        public string estado { get; set; }

        [SPParameterName("P_Municipio", 5)]
        public string municipio { get; set; }

        [SPParameterName("P_Usuario", 6)]
        public string usuario { get; set; }

    }
    public class ModelInstcopoResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }


    [SPName("P_QRY_TPAIS")]
    public class ModeltpaisRequest : BaseModelRequest
    {

    }
    public class ModeltpaisResponse : BaseModelResponse
    {
        [SPResponseColumnName("TPAIS_CLAVE")]
        public string clave { get; set; }

        [SPResponseColumnName("TPAIS_DESC")]
        public string nombre { get; set; }

        [SPResponseColumnName("TPAIS_GENTIL")]
        public string gentil { get; set; }

        [SPResponseColumnName("TPAIS_ESTATUS")]
        public string estatus_code { get; set; }

        [SPResponseColumnName("estatus")]
        public string estatus { get; set; }

        [SPResponseColumnName("TPAIS_DATE")]
        public string fecha { get; set; }
    }

    [SPName("P_QRY_ESTADOS")]
    public class ModelEstadosRequest : BaseModelRequest
    {
        [SPParameterName("P_Pais", 0)]
        public string pais { get; set; }

    }
    public class ModelEstadosResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }

        [SPResponseColumnName("DESCRIPCION")]
        public string descripcion { get; set; }
    }


    [SPName("P_QRY_MUNICIPIOS")]
    public class ModelMunicipiosRequest : BaseModelRequest
    {
        [SPParameterName("P_Pais", 0)]
        public string pais { get; set; }

        [SPParameterName("P_Estado", 1)]
        public string estado { get; set; }
    }
    public class ModelMunicipiosResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }

        [SPResponseColumnName("DESCRIPCION")]
        public string descripcion { get; set; }
    }

    [SPName("P_QRY_TESTA")]
    public class ModeltestaRequest : BaseModelRequest
    {

    }

    public class ModeltestaResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
        [SPResponseColumnName("C_PAIS")]
        public string C_pais { get; set; }
        [SPResponseColumnName("PAIS")]
        public string Pais { get; set; }
        [SPResponseColumnName("ESTATUS_CODE")]
        public string ESTATUS_CODE { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string ESTATUS { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }

    }



    [SPName("P_INS_TESTA2")]
    public class ModelInstesta : BaseModelRequest
    {

        [SPParameterName("Clave_testa", 0)]
        public string testa_clave { get; set; }

        [SPParameterName("Clave_tpais", 1)]
        public string testa_tpais_clave { get; set; }

        [SPParameterName("Desc_testa", 2)]
        public string testa_desc { get; set; }

        [SPParameterName("Usuario", 3)]
        public string testa_tuser_clave { get; set; }

        [SPParameterName("Estatus", 4)]
        public string testa_estatus { get; set; }

    }
    public class ModelInstestaResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TESTA")]
    public class ModelUpdtesta : BaseModelRequest
    {

        [SPParameterName("Clave_testa", 0)]
        public string testa_clave { get; set; }

        [SPParameterName("Clave_tpais", 1)]
        public string testa_tpais_clave { get; set; }

        [SPParameterName("Desc_testa", 2)]
        public string testa_desc { get; set; }

        [SPParameterName("Usuario", 3)]
        public string testa_tuser_clave { get; set; }

        [SPParameterName("Estatus", 4)]
        public string testa_estatus { get; set; }

    }

    [SPName("P_QRY_TCAMP_ACTIVOS")]
    public class ModelObtenCampusVigentesRequest : BaseModelRequest
    {

    }
    public class ModelObtenCampusVigentesResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("campus")]
        public string campus { get; set; }
    }


    [SPName("P_QRY_HORA")]
    public class ModelObtenHoraRequest : BaseModelRequest
    {

    }
    public class ModelObtenHoraResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("hora_ini")]
        public string hora_ini { get; set; }

        [SPResponseColumnName("hora_fin")]
        public string hora_fin { get; set; }
    }



    [SPName("P_QRY_CAT_DOCUMENTOS")]
    public class ModelCatDoctosRequest : BaseModelRequest
    {

    }
    public class ModelCatDoctosResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("c_tipo")]
        public string c_tipo { get; set; }

        [SPResponseColumnName("Tipo")]
        public string Tipo { get; set; }

        [SPResponseColumnName("c_formato")]
        public string c_formato { get; set; }

        [SPResponseColumnName("Formato")]
        public string Formato { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }

    [SPName("INS_TDOCU")]
    public class ModelInsertarCatDoctos : BaseModelRequest
    {

        [SPParameterName("p_tdocu_clave", 0)]
        public string tdocu_clave { get; set; }

        [SPParameterName("p_tdocu_desc", 1)]
        public string tdocu_desc { get; set; }

        [SPParameterName("p_tdocu_tipo", 2)]
        public string tdocu_tipo { get; set; }

        [SPParameterName("p_tdocu_fis_dig", 3)]
        public string tdocu_fis_dig { get; set; }

        [SPParameterName("p_tdocu_estatus", 4)]
        public string tdocu_estatus { get; set; }

        [SPParameterName("p_tdocu_tuser_clave", 5)]
        public string tdocu_tuser_clave { get; set; }
    }

    public class ModelInsertarCatDoctosResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_INS_TESPR")]//Insertar Tespr
    public class ModelInsertarTespr : BaseModelRequest
    {

        [SPParameterName("p_tespr_clave", 0)]
        public string tespr_clave { get; set; }

        [SPParameterName("p_tespr_desc", 1)]
        public string tespr_desc { get; set; }

        [SPParameterName("p_tespr_user", 2)]
        public string tespr_user { get; set; }

        [SPParameterName("p_tespr_estatus", 3)]
        public string tespr_estatus { get; set; }
    }

    public class ModelInsertarTesprResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }


    [SPName("P_UPD_TESPR")]
    public class ModelEditarTespr : BaseModelRequest
    {

        [SPParameterName("p_tespr_clave", 0)]
        public string tespr_clave { get; set; }

        [SPParameterName("p_tespr_desc", 1)]
        public string tespr_desc { get; set; }

        [SPParameterName("p_tespr_user", 2)]
        public string tespr_user { get; set; }

        [SPParameterName("p_tespr_estatus", 3)]
        public string tespr_estatus { get; set; }

    }


    [SPName("P_QRY_TTIBA")]//Tipo Bajas
    public class ModelObtenTipoBajasRequest : BaseModelRequest
    {
        [SPParameterName("p_tbaap_tpers_num", 0)]
        public string p_tbaap_tpers_num { get; set; }

        [SPParameterName("p_tbaap_tprog_clave", 1)]
        public string tbaap_tprog_clave { get; set; }

    }
    public class ModelObtenTipoBajasResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }

    [SPName("P_INS_TTIBA")]//Insertar tipo baja
    public class ModelInsertarTipoBaja : BaseModelRequest
    {

        [SPParameterName("p_ttiba_clave", 0)]
        public string ttiba_clave { get; set; }

        [SPParameterName("p_ttiba_desc", 1)]
        public string ttiba_desc { get; set; }

        [SPParameterName("p_ttiba_user", 2)]
        public string ttiba_user { get; set; }

        [SPParameterName("p_ttiba_estatus", 3)]
        public string ttiba_estatus { get; set; }
    }

    public class ModelInsertarTipoBajaResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TTIBA")]//Actualiza_Pais
    public class ModelEditarTipoBaja : BaseModelRequest
    {
        [SPParameterName("p_ttiba_clave", 0)]
        public string ttiba_clave { get; set; }

        [SPParameterName("p_ttiba_desc", 1)]
        public string ttiba_desc { get; set; }

        [SPParameterName("p_ttiba_user", 2)]
        public string ttiba_user { get; set; }

        [SPParameterName("p_ttiba_estatus", 3)]
        public string ttiba_estatus { get; set; }
    }




    [SPName("P_INS_TCALI")]
    public class ModelInsertaTCali : BaseModelRequest
    {
        [SPParameterName("p_tcali_tnive_clave", 0)]
        public string tcali_tnive_clave { get; set; }

        [SPParameterName("p_tcali_clave", 1)]
        public string tcali_clave { get; set; }

        [SPParameterName("p_tcali_puntos", 2)]
        public string tcali_puntos { get; set; }

        [SPParameterName("p_tcali_ind_aprob", 3)]
        public string tcali_ind_aprob { get; set; }

        [SPParameterName("p_tcali_ind_prom", 4)]
        public string tcali_ind_prom { get; set; }

        [SPParameterName("p_tcali_estatus", 5)]
        public string tcali_estatus { get; set; }

        [SPParameterName("p_tcali_user", 6)]
        public string tcali_user { get; set; }
    }
    public class ModelInsertaTCaliResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TCALI")]
    public class ModelEditarTCali : BaseModelRequest
    {
        [SPParameterName("p_tcali_tnive_clave", 0)]
        public string tcali_tnive_clave { get; set; }

        [SPParameterName("p_tcali_clave", 1)]
        public string tcali_clave { get; set; }

        [SPParameterName("p_tcali_puntos", 2)]
        public string tcali_puntos { get; set; }

        [SPParameterName("p_tcali_ind_aprob", 3)]
        public string tcali_ind_aprob { get; set; }

        [SPParameterName("p_tcali_ind_prom", 4)]
        public string tcali_ind_prom { get; set; }

        [SPParameterName("p_tcali_estatus", 5)]
        public string tcali_estatus { get; set; }

        [SPParameterName("p_tcali_user", 6)]
        public string tcali_user { get; set; }
    }



    [SPName("P_QRY_TSTAL")]
    public class ModelTstalsRequest : BaseModelRequest
    {

    }
    public class ModelTstalResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_tipo")]
        public string c_tipo { get; set; }

        [SPResponseColumnName("Tipo")]
        public string Tipo { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_INS_TSTAL")]//Insertar tipo baja
    public class ModelInsertarTstal : BaseModelRequest
    {

        [SPParameterName("p_tstal_clave", 0)]
        public string tstal_clave { get; set; }

        [SPParameterName("p_tstal_desc", 1)]
        public string tstal_desc { get; set; }

        [SPParameterName("p_tstal_user", 2)]
        public string tstal_user { get; set; }

        [SPParameterName("p_tstal_estatus", 3)]
        public string tstal_estatus { get; set; }

        [SPParameterName("p_tstal_tipo", 4)]
        public string tstal_tipo { get; set; }
    }

    public class ModelInsertarTstalResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TSTAL")]//Insertar tipo baja
    public class ModelEditarTstal : BaseModelRequest
    {

        [SPParameterName("p_tstal_clave", 0)]
        public string tstal_clave { get; set; }

        [SPParameterName("p_tstal_desc", 1)]
        public string tstal_desc { get; set; }

        [SPParameterName("p_tstal_user", 2)]
        public string tstal_user { get; set; }

        [SPParameterName("p_tstal_estatus", 3)]
        public string tstal_estatus { get; set; }

        [SPParameterName("p_tstal_tipo", 4)]
        public string tstal_tipo { get; set; }
    }

    [SPName("P_QRY_TCOMP")]
    public class ModelTcompRequest : BaseModelRequest
    {

    }
    public class ModelTcompResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }

    [SPName("P_INS_TCOMP")]//Insertar componentes de calificacion
    public class ModelInsertarTcomp : BaseModelRequest
    {

        [SPParameterName("p_tcomp_clave", 0)]
        public string tcomp_clave { get; set; }

        [SPParameterName("p_tcomp_desc", 1)]
        public string tcomp_desc { get; set; }

        [SPParameterName("p_tcomp_estatus", 2)]
        public string tcomp_estatus { get; set; }

        [SPParameterName("p_tcomp_user", 3)]
        public string tcomp_user { get; set; }
    }

    public class ModelInsertarTcompResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }



    [SPName("P_QRY_IMPORTE_CONCEPTO")]//Insertar componentes de calificacion
    public class ModelConcepto : BaseModelRequest
    {

        [SPParameterName("p_tprog_clave", 0)]
        public string tprog_clave { get; set; }

        [SPParameterName("p_tcamp_clave", 1)]
        public string tcamp_clave { get; set; }

        [SPParameterName("p_tcoco_clave", 2)]
        public string tcoco_clave { get; set; }
    }

    public class ModelConceptoResponse : BaseModelResponse
    {
        [SPResponseColumnName("Importe")]
        public string Importe { get; set; }
    }


    [SPName("P_UPD_TCOMP")]//Editar componentes de calificacion
    public class ModelEditarTcomp : BaseModelRequest
    {

        [SPParameterName("p_tcomp_clave", 0)]
        public string tcomp_clave { get; set; }

        [SPParameterName("p_tcomp_desc", 1)]
        public string tcomp_desc { get; set; }

        [SPParameterName("p_tcomp_estatus", 2)]
        public string tcomp_estatus { get; set; }

        [SPParameterName("p_tcomp_user", 3)]
        public string tcomp_user { get; set; }
    }

    [SPName("P_QRY_TPROG_TNIVE")]
    public class ModelTProgTNiveRequest : BaseModelRequest
    {

    }
    public class ModelTProgTNiveResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("Nivel")]
        public string Nivel { get; set; }
    }

    [SPName("P_QRY_TSERI_MATERIAS")]
    public class ModelTSeriRequest : BaseModelRequest
    {
        [SPParameterName("p_tseri_tprog_clave", 0)]
        public string tseri_tprog_clave { get; set; }

        [SPParameterName("p_tplan_tmate_clave1", 1)]
        public string tplan_tmate_clave1 { get; set; }

        [SPParameterName("p_tplan_tmate_clave2", 2)]
        public string tplan_tmate_clave2 { get; set; }

        [SPParameterName("p_tplan_tmate_clave3", 3)]
        public string tplan_tmate_clave3 { get; set; }

        [SPParameterName("p_tplan_tmate_clave4", 4)]
        public string tplan_tmate_clave4 { get; set; }
    }
    public class ModelTSeriResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("materia")]
        public string materia { get; set; }

        [SPResponseColumnName("cl_seriacion")]
        public string cl_seriacion { get; set; }

        [SPResponseColumnName("desc_seriacion")]
        public string desc_seriacion { get; set; }

        [SPResponseColumnName("clave_or2")]
        public string clave_or2 { get; set; }

        [SPResponseColumnName("or2")]
        public string or2 { get; set; }

        [SPResponseColumnName("clave_or3")]
        public string clave_or3 { get; set; }

        [SPResponseColumnName("or3")]
        public string or3 { get; set; }
    }


    [SPName("P_QRY_PLAN_MATERIAS")]
    public class ModelPlanMateriasRequest : BaseModelRequest
    {
        [SPParameterName("p_tplan_tprog_clave", 0)]
        public string tplan_tprog_clave { get; set; }
    }
    public class ModelPlanMateriasResponse : BaseModelResponse
    {
        [SPResponseColumnName("area")]
        public string area { get; set; }

        [SPResponseColumnName("cons")]
        public string cons { get; set; }

        [SPResponseColumnName("Clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("Nombre")]
        public string Nombre { get; set; }
    }

    [SPName("P_QRY_TNIVE")]
    public class ModelTNivelRequest : BaseModelRequest
    {
    }
    public class ModelTNivelResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }

    [SPName("P_UPD_TNIVE")]
    public class ModelEditarNivel : BaseModelRequest
    {

        [SPParameterName("p_tnive_clave", 0)]
        public string tnive_clave { get; set; }

        [SPParameterName("p_tnive_desc", 1)]
        public string tnive_desc { get; set; }

        [SPParameterName("p_tnive_user", 2)]
        public string tnive_user { get; set; }

        [SPParameterName("p_tnive_estatus", 3)]
        public string tnive_estatus { get; set; }
    }


    [SPName("P_QRY_TCOLE")]
    public class ModelTColeRequest : BaseModelRequest
    {
    }
    public class ModelTColeResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_QRY_TAREA")]
    public class ModelTAreaRequest : BaseModelRequest
    {
    }
    public class ModelTAreaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_QRY_TMODA")]
    public class ModelTModaRequest : BaseModelRequest
    {
    }
    public class ModelTModaResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }
    }


    [SPName("P_UPD_TMODA")]
    public class ModelEditarTModa : BaseModelRequest
    {
        [SPParameterName("p_tmoda_clave", 0)]
        public string tmoda_clave { get; set; }

        [SPParameterName("p_tmoda_desc", 1)]
        public string tmoda_desc { get; set; }

        [SPParameterName("p_tmoda_user", 2)]
        public string tmoda_user { get; set; }

        [SPParameterName("p_tmoda_estatus", 3)]
        public string tmoda_estatus { get; set; }
    }

    [SPName("P_QRY_TPROG")]
    public class ModelTProgRequest : BaseModelRequest
    {

    }
    public class ModelTProgResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }

        [SPResponseColumnName("Nivel")]
        public string Nivel { get; set; }

        [SPResponseColumnName("Colegio")]
        public string Colegio { get; set; }

        [SPResponseColumnName("Modalidad")]
        public string Modalidad { get; set; }

        [SPResponseColumnName("creditos")]
        public string creditos { get; set; }

        [SPResponseColumnName("cursos")]
        public string cursos { get; set; }

        [SPResponseColumnName("periodos")]
        public string periodos { get; set; }

        [SPResponseColumnName("incorporante")]
        public string incorporante { get; set; }

        [SPResponseColumnName("rvoe")]
        public string rvoe { get; set; }

        [SPResponseColumnName("fecha_rvoe")]
        public string fecha_rvoe { get; set; }
    }

    [SPName("P_QRY_TMATE")]//Obtener Lista Materias
    public class ModelObtenTMateRequest : BaseModelRequest
    {

    }
    public class ModelObtenTMateResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("c_estatus")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("Estatus")]
        public string Estatus { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }

        [SPResponseColumnName("creditos")]
        public string creditos { get; set; }

        [SPResponseColumnName("Teoria")]
        public string Teoria { get; set; }

        [SPResponseColumnName("Practica")]
        public string Practica { get; set; }

        [SPResponseColumnName("Campo")]
        public string Campo { get; set; }

        [SPResponseColumnName("Minimo")]
        public string Minimo { get; set; }

        [SPResponseColumnName("Maximo")]
        public string Maximo { get; set; }
    }


    [SPName("P_INS_TMATE")]
    public class ModelInsertarTMate : BaseModelRequest
    {
        [SPParameterName("p_tmate_clave", 0)]
        public string tmate_clave { get; set; }

        [SPParameterName("p_tmate_desc", 1)]
        public string tmate_desc { get; set; }

        [SPParameterName("p_tmate_creditos", 2)]
        public string tmate_creditos { get; set; }

        [SPParameterName("p_tmate_hr_teo", 3)]
        public string tmate_hr_teo { get; set; }

        [SPParameterName("p_tmate_hr_prac", 4)]
        public string tmate_hr_prac { get; set; }

        [SPParameterName("p_tmate_hr_campo", 5)]
        public string tmate_hr_campo { get; set; }

        [SPParameterName("p_tmate_min_cupo", 6)]
        public string tmate_min_cupo { get; set; }

        [SPParameterName("p_tmate_max_cupo", 7)]
        public string tmate_max_cupo { get; set; }

        [SPParameterName("p_tmate_clave_incorp", 8)]
        public string tmate_clave_incorp { get; set; }

        [SPParameterName("p_tmate_tipo", 9)]
        public string tmate_tipo { get; set; }

        [SPParameterName("p_tmate_estatus", 10)]
        public string tmate_estatus { get; set; }

        [SPParameterName("p_tmate_usuario", 11)]
        public string tmate_usuario { get; set; }
    }

    public class ModelInsertarTMateResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TMATE")]
    public class ModelEditarTMate : BaseModelRequest
    {
        [SPParameterName("p_tmate_clave", 0)]
        public string tmate_clave { get; set; }

        [SPParameterName("p_tmate_desc", 1)]
        public string tmate_desc { get; set; }

        [SPParameterName("p_tmate_creditos", 2)]
        public string tmate_creditos { get; set; }

        [SPParameterName("p_tmate_hr_teo", 3)]
        public string tmate_hr_teo { get; set; }

        [SPParameterName("p_tmate_hr_prac", 4)]
        public string tmate_hr_prac { get; set; }

        [SPParameterName("p_tmate_hr_campo", 5)]
        public string tmate_hr_campo { get; set; }

        [SPParameterName("p_tmate_min_cupo", 6)]
        public string tmate_min_cupo { get; set; }

        [SPParameterName("p_tmate_max_cupo", 7)]
        public string tmate_max_cupo { get; set; }

        [SPParameterName("p_tmate_clave_incorp", 8)]
        public string tmate_clave_incorp { get; set; }

        [SPParameterName("p_tmate_tipo", 9)]
        public string tmate_tipo { get; set; }

        [SPParameterName("p_tmate_estatus", 10)]
        public string tmate_estatus { get; set; }

        [SPParameterName("p_tmate_usuario", 11)]
        public string tmate_usuario { get; set; }
    }

}
