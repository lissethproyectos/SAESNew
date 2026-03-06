using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelAlumno;

namespace SAES_Services
{
    public class Catalogos : Methods
    {
        public DataTable obtenPeriodo()
        {
            ModelPeriodoRequest request = new ModelPeriodoRequest() { Nada = "" };
            List<ModelPeriodoResponse> response = DB.CallSPListResult<ModelPeriodoResponse, ModelPeriodoRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenCampus()
        {
            ModelCampusRequest request = new ModelCampusRequest() { Nada = "" };
            List<ModelCampusResponse> response = DB.CallSPListResult<ModelCampusResponse, ModelCampusRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenOpcTitulacion()
        {
            ModelTtiopRequest request = new ModelTtiopRequest() { };
            List<ModelTtiopResponse> response = DB.CallSPListResult<ModelTtiopResponse, ModelTtiopRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenOpcTitulacionNivel(string P_Nivel)
        {
            ModelTtiopNivelRequest request = new ModelTtiopNivelRequest() { Nivel=P_Nivel };
            List<ModelTtiopNivelpResponse> response = DB.CallSPListResult<ModelTtiopNivelpResponse, ModelTtiopNivelRequest>(request);
            return ToDataTableForDropDownList(response);
        }
     
        public DataTable obtenModalidadActivos()
        {
            ModelTModaActivosRequest request = new ModelTModaActivosRequest() { };
            List<ModelTModaActivosResponse> response = DB.CallSPListResult<ModelTModaActivosResponse, ModelTModaActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }


        public DataTable obtenEMail()
        {
            ModelEMailRequest request = new ModelEMailRequest() { };
            List<ModelEMailResponse> response = DB.CallSPListResult<ModelEMailResponse, ModelEMailRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenEMailActivos()
        {
            ModelEMailActivosRequest request = new ModelEMailActivosRequest() { };
            List<ModelEMailActivosResponse> response = DB.CallSPListResult<ModelEMailActivosResponse, ModelEMailActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenNivelesActivos()
        {
            ModelNivelesActivosRequest request = new ModelNivelesActivosRequest() { };
            List<ModelNivelesActivosResponse> response = DB.CallSPListResult<ModelNivelesActivosResponse, ModelNivelesActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenColegiosActivos()
        {
            ModelColegiosActivosRequest request = new ModelColegiosActivosRequest() { };
            List<ModelColegiosActivosResponse> response = DB.CallSPListResult<ModelColegiosActivosResponse, ModelColegiosActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenColegiosporPrograma(string p_tcapr_tcamp_clave, string p_tprog_tnive_clave)
        {
            ModelColegiosporProgramaRequest request = new ModelColegiosporProgramaRequest() { 
                tcapr_tcamp_clave= p_tcapr_tcamp_clave,
                tprog_tnive_clave= p_tprog_tnive_clave
            };
            List<ModelColegiosporProgramaResponse> response = DB.CallSPListResult<ModelColegiosporProgramaResponse, ModelColegiosporProgramaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenModalidadporPrograma(string p_tcapr_tcamp_clave, string p_tprog_tnive_clave, string p_tprog_tcole_clave)
        {
            ModelModalidadporProgramaRequest request = new ModelModalidadporProgramaRequest()
            {
                tcapr_tcamp_clave = p_tcapr_tcamp_clave,
                tprog_tnive_clave = p_tprog_tnive_clave,
                tprog_tcole_clave= p_tprog_tcole_clave
            };
            List<ModelModalidadporProgramaResponse> response = DB.CallSPListResult<ModelModalidadporProgramaResponse, ModelModalidadporProgramaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenTcaprPrograma(string p_tcapr_tcamp_clave, string p_tprog_tnive_clave, string p_tprog_tcole_clave, string p_tprog_tmoda_clave)
        {
            ModelTcaprProgramaRequest request = new ModelTcaprProgramaRequest()
            {
                tcapr_tcamp_clave = p_tcapr_tcamp_clave,
                tprog_tnive_clave = p_tprog_tnive_clave,
                tprog_tcole_clave = p_tprog_tcole_clave,
                tprog_tmoda_clave= p_tprog_tmoda_clave
            };
            List<ModelTcaprProgramaResponse> response = DB.CallSPListResult<ModelTcaprProgramaResponse, ModelTcaprProgramaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenCampusDocente(string usuario, string periodo)
        {
            ModelCampusDocenteRequest request = new ModelCampusDocenteRequest() { Usuario = usuario, Periodo = periodo };
            List<ModelCampusDocenteResponse> response = DB.CallSPListResult<ModelCampusDocenteResponse, ModelCampusDocenteRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenNivel(string campus)
        {
            ModelNivelRequest request = new ModelNivelRequest() { Campus = campus };
            List<ModelNivelResponse> response = DB.CallSPListResult<ModelNivelResponse, ModelNivelRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenTipoPeriodo()
        {
            ModelTipoPeriodoRequest request = new ModelTipoPeriodoRequest() { };
            List<ModelTipoPeriodoResponse> response = DB.CallSPListResult<ModelTipoPeriodoResponse, ModelTipoPeriodoRequest>(request);
            return ToDataTableForDropDownList(response);
        }


        public DataTable obtenPrograma(string campus, string nivel)
        {
            ModelProgramaRequest request = new ModelProgramaRequest() { Campus = campus, Nivel = nivel };
            List<ModelProgramaResponse> response = DB.CallSPListResult<ModelProgramaResponse, ModelProgramaRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenProgramaCampus(string campus)
        {
            ModelProgramaCampusRequest request = new ModelProgramaCampusRequest() { Campus = campus };
            List<ModelProgramaCampusResponse> response = DB.CallSPListResult<ModelProgramaCampusResponse, ModelProgramaCampusRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenProgramaVigente(string campus, string nivel)
        {
            ModelProgramaRequest request = new ModelProgramaRequest() { Campus = campus, Nivel = nivel };
            List<ModelProgramaResponse> response = DB.CallSPListResult<ModelProgramaResponse, ModelProgramaRequest>(request);
            return ToDataTableForDropDownList(response);
        }


        public DataTable obtenProgramas()
        {
            ModelCatProgramaRequest request = new ModelCatProgramaRequest() { };
            List<ModelCatProgramaResponse> response = DB.CallSPListResult<ModelCatProgramaResponse, ModelCatProgramaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenSecuencias(string p_campus)
        {
            ModelSecuenciasRequest request = new ModelSecuenciasRequest() { Campus = p_campus };
            List<ModelSecuenciasResponse> response = DB.CallSPListResult<ModelSecuenciasResponse, ModelSecuenciasRequest>(request);
            return ToDataTable(response);
        }
        public DataTable obtenMateria(string campus, string nivel, string programa)
        {
            ModelMateriaRequest request = new ModelMateriaRequest() { Campus = campus, Nivel = nivel, Programa = programa };
            List<ModelMateriaResponse> response = DB.CallSPListResult<ModelMateriaResponse, ModelMateriaRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenMateriaDocente(string usuario, string campus, string periodo)
        {
            ModelMateriaDocenteRequest request = new ModelMateriaDocenteRequest() { Usuario = usuario, Campus = campus, Periodo = periodo };
            List<ModelMateriaDocenteResponse> response = DB.CallSPListResult<ModelMateriaDocenteResponse, ModelMateriaDocenteRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenGrupo(string campus, string nivel, string programa, string materia, string periodo)
        {
            ModelGrupoRequest request = new ModelGrupoRequest() { Campus = campus, Nivel = nivel, Programa = programa, Materia = materia, Periodo = periodo };
            List<ModelGrupoResponse> response = DB.CallSPListResult<ModelGrupoResponse, ModelGrupoRequest>(request);
            return ToDataTableForDropDownList(response);
        }
        public DataTable obtenGrupoDocente(string usuario, string campus, string materia, string periodo)
        {
            ModelGrupoDocenteRequest request = new ModelGrupoDocenteRequest() { Usuario = usuario, Campus = campus, Materia = materia, Periodo = periodo };
            List<ModelGrupoDocenteResponse> response = DB.CallSPListResult<ModelGrupoDocenteResponse, ModelGrupoDocenteRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenComponente(string materia)
        {
            ModelComponenteRequest request = new ModelComponenteRequest() { Materia = materia };
            List<ModelComponenteResponse> response = DB.CallSPListResult<ModelComponenteResponse, ModelComponenteRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenCalificaciones(string programa)
        {
            ModelCalificacionesRequest request = new ModelCalificacionesRequest() { Programa = programa };
            List<ModelCalificacionesResponse> response = DB.CallSPListResult<ModelCalificacionesResponse, ModelCalificacionesRequest>(request);
            return ToDataTableForDropDownList(response, false);
        }

        public DataTable obtenMatricula(string matricula)
        {
            ModelMatriculaRequest request = new ModelMatriculaRequest() { Matricula = matricula };
            List<ModelMatriculaResponse> response = DB.CallSPListResult<ModelMatriculaResponse, ModelMatriculaRequest>(request);
            return ToDataTableForDropDownList(response, false);
        }

        public DataTable obtenAlumnoPrograma(string NumPers)
        {
            ModelAlumnoProgramaRequest request = new ModelAlumnoProgramaRequest() { PERS_NUM = NumPers };
            List<ModelAlumnoProgramaResponse> response = DB.CallSPListResult<ModelAlumnoProgramaResponse, ModelAlumnoProgramaRequest>(request);
            return ToDataTableForDropDownList(response);
        }




        public DataTable obtenAlumnoProgramaTCEEL(string Matricula, string Programa)
        {
            ModelAlumnoProgramaTCEELRequest request = new ModelAlumnoProgramaTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelAlumnoProgramaTCEELResponse> response = DB.CallSPListResult<ModelAlumnoProgramaTCEELResponse, ModelAlumnoProgramaTCEELRequest>(request);
            return ToDataTable(response);
        }
        public DataTable obtenTipoCertificadoTCEEL(string Matricula, string Programa)
        {
            ModelTipoCertificadoTCEELRequest request = new ModelTipoCertificadoTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelTipoCertificadoTCEELResponse> response = DB.CallSPListResult<ModelTipoCertificadoTCEELResponse, ModelTipoCertificadoTCEELRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenDatosTotalesTCEEL(string Matricula, string Programa)
        {
            ModelDatosTotalesTCEELRequest request = new ModelDatosTotalesTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelDatosTotalesTCEELResponse> response = DB.CallSPListResult<ModelDatosTotalesTCEELResponse, ModelDatosTotalesTCEELRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenDatosFuncionarioTCEEL(string Matricula, string Programa)
        {
            ModelObtenDatosFuncionarioTCEELRequest request = new ModelObtenDatosFuncionarioTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenDatosFuncionarioTCEELResponse> response = DB.CallSPListResult<ModelObtenDatosFuncionarioTCEELResponse, ModelObtenDatosFuncionarioTCEELRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenDatosMateriaTCEEL(string Matricula, string Programa)
        {
            ModelObtenDatosMateriaTCEELRequest request = new ModelObtenDatosMateriaTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenDatosMateriaTCEELResponse> response = DB.CallSPListResult<ModelObtenDatosMateriaTCEELResponse, ModelObtenDatosMateriaTCEELRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenCampusTCEEL(string Matricula, string Programa)
        {
            ModelObtenCampusTCEELRequest request = new ModelObtenCampusTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenCampusTCEELResponse> response = DB.CallSPListResult<ModelObtenCampusTCEELResponse, ModelObtenCampusTCEELRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenAlumnoTCEEL(string Matricula)
        {
            ModelObtenAlumnoTCEELRequest request = new ModelObtenAlumnoTCEELRequest() { Matricula = Matricula };
            List<ModelObtenAlumnoTCEELResponse> response = DB.CallSPListResult<ModelObtenAlumnoTCEELResponse, ModelObtenAlumnoTCEELRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenMateriasNoAcreditadas(string ClavePrograma)
        {
            ModelMateriaNoAcreditadaRequest request = new ModelMateriaNoAcreditadaRequest() { ClavePrograma = ClavePrograma };
            List<ModelMateriaNoAcreditadaResponse> response = DB.CallSPListResult<ModelMateriaNoAcreditadaResponse, ModelMateriaNoAcreditadaRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenAcreditaciones()
        {
            ModelAcreditacionRequest request = new ModelAcreditacionRequest() { Nada = "" };
            List<ModelAcreditacionResponse> response = DB.CallSPListResult<ModelAcreditacionResponse, ModelAcreditacionRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenTodoPeriodos()
        {
            ModelTodoPeriodoRequest request = new ModelTodoPeriodoRequest() { Nada = "" };
            List<ModelTodoPeriodoResponse> response = DB.CallSPListResult<ModelTodoPeriodoResponse, ModelTodoPeriodoRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenEstatusCatOpcionesTitulacion()
        {
            return ToDataTableForDropDownList(new List<RowDropDownList>() {
                new RowDropDownList(){ Clave = "A", Descripcion = "Activo"},
                new RowDropDownList(){ Clave = "B", Descripcion = "Baja"}
            }, false);
        }

        public DataTable obtenAutorizacion_ttiel()
        {
            return ToDataTableForDropDownList(new List<RowDropDownList>() {
                new RowDropDownList(){ Clave = "1", Descripcion = "RVOE FEDERAL"},
                new RowDropDownList(){ Clave = "2", Descripcion = "RVOE ESTATAL"},
                new RowDropDownList(){ Clave = "3", Descripcion = "AUTORIZACIÓN FEDERAL"},
                new RowDropDownList(){ Clave = "4", Descripcion = "AUTORIZACIÓN ESTATAL"},
                new RowDropDownList(){ Clave = "5", Descripcion = "ACTA DE SESIÓN"},
                new RowDropDownList(){ Clave = "6", Descripcion = "ACUERDO DE INCORPORACIÓN"},
                new RowDropDownList(){ Clave = "7", Descripcion = "ACUERDO SECRETARIAL SEP"},
                new RowDropDownList(){ Clave = "8", Descripcion = "DECRETO DE CREACIÓN"},
                new RowDropDownList(){ Clave = "9", Descripcion = "OTRO"}
            }, false);
        }

        public DataTable obtenModalidadTitulacion_ttiel()
        {
            return ToDataTableForDropDownList(new List<RowDropDownList>() {
                new RowDropDownList(){ Clave = "1", Descripcion = "POR TESIS"},
                new RowDropDownList(){ Clave = "2", Descripcion = "POR PROMEDIO"},
                new RowDropDownList(){ Clave = "3", Descripcion = "POR ESTUDIOS DE POSGRADOS"},
                new RowDropDownList(){ Clave = "4", Descripcion = "POR EXPERIENCIA LABORAL"},
                new RowDropDownList(){ Clave = "5", Descripcion = "POR CENEVAL"},
                new RowDropDownList(){ Clave = "6", Descripcion = "OTRO"}
            }, false);
        }

        public DataTable obtenServicioSocial_ttiel()
        {
            return ToDataTableForDropDownList(new List<RowDropDownList>() {
                new RowDropDownList(){ Clave = "1", Descripcion = "ART. 52 LRART. 5 CONST"},
                new RowDropDownList(){ Clave = "2", Descripcion = "ART. 55 LRART. 5 CONST"},
                new RowDropDownList(){ Clave = "3", Descripcion = "ART. 91 RLRART. 5 CONST"},
                new RowDropDownList(){ Clave = "4", Descripcion = "ART. 10 REGLAMENTO PARA LA PRESTACIÓN DEL SERVICIO SOCIAL DE LOS ESTUDIANTES DE LAS INSTITUCIONES DE EDUCACIÓN SUPERIOR EN LA REPÚBLICA MEXICANA"},
                new RowDropDownList(){ Clave = "5", Descripcion = "NO APLICA"}
            }, false);
        }
        public DataTable obtenEstudiosAntecedentes_ttiel()
        {
            return ToDataTableForDropDownList(new List<RowDropDownList>() {
                new RowDropDownList(){ Clave = "1", Descripcion = "MAESTRÍA"},
                new RowDropDownList(){ Clave = "2", Descripcion = "LICENCIATURA"},
                new RowDropDownList(){ Clave = "3", Descripcion = "TÉCNICO SUPERIOR UNIVERSITARIO"},
                new RowDropDownList(){ Clave = "4", Descripcion = "BACHILLERATO"},
                new RowDropDownList(){ Clave = "5", Descripcion = "EQUIVALENTE A BACHILLERATO"},
                new RowDropDownList(){ Clave = "6", Descripcion = "SECUNDARIA"}
            }, false);
        }
        public DataTable obtenRoles()
        {
            ModelObtenRolesRequest request = new ModelObtenRolesRequest() { };
            List<ModelObtenRolesResponse> response = DB.CallSPListResult<ModelObtenRolesResponse, ModelObtenRolesRequest>(request);
            return ToDataTable(response);
        }

        public List<ModelObtenRolesResponse> obtenListRoles()
        {
            ModelObtenRolesRequest request = new ModelObtenRolesRequest() { };
            List<ModelObtenRolesResponse> response = DB.CallSPListResult<ModelObtenRolesResponse, ModelObtenRolesRequest>(request);
            return response;
        }
        public List<ModelObtenMenusResponse> obtenListMenu()
        {
            ModelObtenMenusRequest request = new ModelObtenMenusRequest() { };
            List<ModelObtenMenusResponse> response = DB.CallSPListResult<ModelObtenMenusResponse, ModelObtenMenusRequest>(request);
            return response;
        }

        public List<ModelObtenDatosCPResponse> obtenDatosCP(string p_clave, string p_estado, string p_municipio)
        {
            ModelObtenDatosCPRequest request = new ModelObtenDatosCPRequest() { clave= p_clave, tcopo_testa_clave= p_estado, tcopo_tdele_clave= p_municipio };
            List<ModelObtenDatosCPResponse> response = DB.CallSPListResult<ModelObtenDatosCPResponse, ModelObtenDatosCPRequest>(request);
            return response;
        }

        public List<ModelObtenColoniasCPResponse> obtenColoniasCP(string p_clave)
        {
            ModelObtenColoniasCPRequest request = new ModelObtenColoniasCPRequest() { clave = p_clave };
            List<ModelObtenColoniasCPResponse> response = DB.CallSPListResult<ModelObtenColoniasCPResponse, ModelObtenColoniasCPRequest>(request);
            return response;
        }

        public string InsertarRol(string clave, string descripcion, string usu_alta, string estatus)
        {
            ModelInsertarRol Insert = new ModelInsertarRol()
            {
                trole_clave = clave,
                trole_desc = descripcion,
                trole_usuario = usu_alta,
                trole_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public ModelInsertarCampusResponse InsertarCampus(string tcamp_clave, string tcamp_desc, string tcamp_calle, string tcamp_colonia,
            string tcamp_tpais_clave, string tcamp_testa_clave, string tcamp_tdele_clave, string tcamp_tcopo_clave, string tcamp_user,
            string tcamp_estatus, string tcamp_abr, string tcamp_rfc, string tcamp_incorp)
        {
            ModelInsertarCampus Insert = new ModelInsertarCampus()
            {
                p_tcamp_clave = tcamp_clave,
                p_tcamp_desc = tcamp_desc,
                p_tcamp_calle = tcamp_calle,
                p_tcamp_colonia = tcamp_colonia,
                p_tcamp_tpais_clave = tcamp_tpais_clave,
                p_tcamp_testa_clave = tcamp_testa_clave,
                p_tcamp_tdele_clave = tcamp_tdele_clave,
                p_tcamp_tcopo_clave = tcamp_tcopo_clave,
                p_tcamp_user = tcamp_user,
                p_tcamp_estatus = tcamp_estatus,
                p_tcamp_abr = tcamp_abr,
                p_tcamp_rfc = tcamp_rfc,
                p_tcamp_incorp = tcamp_incorp
            };
            ModelInsertarCampusResponse response = DB.CallSPResult<ModelInsertarCampusResponse, ModelInsertarCampus>(Insert);
            return response;
        }

        public string EditarCampus(string tcamp_clave, string tcamp_desc, string tcamp_calle, string tcamp_colonia,
     string tcamp_tpais_clave, string tcamp_testa_clave, string tcamp_tdele_clave, string tcamp_tcopo_clave, string tcamp_user,
     string tcamp_estatus, string tcamp_abr, string tcamp_rfc, string tcamp_incorp)
        {
            ModelEditarCampus Update = new ModelEditarCampus()
            {
                p_tcamp_clave = tcamp_clave,
                p_tcamp_desc = tcamp_desc,
                p_tcamp_calle = tcamp_calle,
                p_tcamp_colonia = tcamp_colonia,
                p_tcamp_tpais_clave = tcamp_tpais_clave,
                p_tcamp_testa_clave = tcamp_testa_clave,
                p_tcamp_tdele_clave = tcamp_tdele_clave,
                p_tcamp_tcopo_clave = tcamp_tcopo_clave,
                p_tcamp_user = tcamp_user,
                p_tcamp_estatus = tcamp_estatus,
                p_tcamp_abr = tcamp_abr,
                p_tcamp_rfc = tcamp_rfc,
                p_tcamp_incorp = tcamp_incorp
            };

            return DB.CallSPForInsertUpdate(Update);

        }



        public ModelInsertarNivelResponse InsertarNivel(string p_tnive_clave, string p_tnive_desc, string p_tnie_user, string p_tnive_estatus)
        {
            ModelInsertarNivel Insert = new ModelInsertarNivel()
            {
                tnive_clave = p_tnive_clave,
                tnive_desc = p_tnive_desc,
                tnie_user = p_tnie_user,
                tnive_estatus = p_tnive_estatus
            };

            ModelInsertarNivelResponse response = DB.CallSPResult<ModelInsertarNivelResponse, ModelInsertarNivel>(Insert);
            return response;
        }

        public string EditarNivel(string p_tnive_clave, string p_tnive_desc, string p_tnive_user, string p_tnive_estatus)
        {
            ModelEditarNivel Editar = new ModelEditarNivel()
            {
                tnive_clave = p_tnive_clave,
                tnive_desc = p_tnive_desc,
                tnive_user = p_tnive_user,
                tnive_estatus = p_tnive_estatus
            };

            return DB.CallSPForInsertUpdate(Editar);

        }

        public ModelInsertarTdeleResponse InsertarDelegaciones(string p_tdele_clave, string p_tdele_desc, string p_tdele_testa_clave, string p_tdele_tpais_clave, string p_tdele_user, string p_tdele_estatus)
        {
            ModelInsertarTdele Insert = new ModelInsertarTdele()
            {
                tdele_clave = p_tdele_clave,
                tdele_desc = p_tdele_desc,
                tdele_testa_clave = p_tdele_testa_clave,
                tdele_tpais_clave = p_tdele_tpais_clave,
                tdele_user = p_tdele_user,
                tdele_estatus = p_tdele_estatus
            };
            ModelInsertarTdeleResponse response = DB.CallSPResult<ModelInsertarTdeleResponse, ModelInsertarTdele>(Insert);
            return response;

            //return DB.CallSPForInsertUpdate(Insert);
        }

        public string EditarrDelegaciones(string p_tdele_clave, string p_tdele_desc, string p_tdele_testa_clave, string p_tdele_tpais_clave, string p_tdele_user, string p_tdele_estatus)
        {
            ModelEditarTdele Editar = new ModelEditarTdele()
            {
                tdele_clave = p_tdele_clave,
                tdele_desc = p_tdele_desc,
                tdele_testa_clave = p_tdele_testa_clave,
                tdele_tpais_clave = p_tdele_tpais_clave,
                tdele_user = p_tdele_user,
                tdele_estatus = p_tdele_estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public string EditarRol(string clave, string descripcion, string estatus)
        {
            ModelEditarRol Editar = new ModelEditarRol()
            {
                trole_clave = clave,
                trole_desc = descripcion,
                trole_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public DataTable ObtenerPreguntas()
        {
            ModelObtenPreguntaRequest request = new ModelObtenPreguntaRequest() { };
            List<ModelObtenPreguntaResponse> response = DB.CallSPListResult<ModelObtenPreguntaResponse, ModelObtenPreguntaRequest>(request);
            return ToDataTable(response);
        }
        public string InsertarPregunta(int clave, string descripcion, string estatus)
        {
            ModelInsertarPregunta Insert = new ModelInsertarPregunta()
            {
                preg_clave = clave,
                preg_desc = descripcion,
                preg_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Insert);
        }
        public string EditarPregunta(int clave, string descripcion, string estatus)
        {
            ModelEditarPregunta Insert = new ModelEditarPregunta()
            {
                preg_clave = clave,
                preg_desc = descripcion,
                preg_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Insert);
        }
        //public DataTable ObtenerPeriodosEscolares()
        //{
        //    ModelObtenPeriodosEscolaresRequest request = new ModelObtenPeriodosEscolaresRequest() { };
        //    List<ModelObtenPeriodosEscolaresResponse> response = DB.CallSPListResult<ModelObtenPeriodosEscolaresResponse, ModelObtenPeriodosEscolaresRequest>(request);
        //    return ToDataTable(response);
        //}
        public DataTable ObtenerPeriodosEscolares()
        {
            ModelObtenPeriodosEscolaresRequest request = new ModelObtenPeriodosEscolaresRequest() { };
            List<ModelObtenPeriodosEscolaresResponse> response = DB.CallSPListResult<ModelObtenPeriodosEscolaresResponse, ModelObtenPeriodosEscolaresRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerPeriodosPrograma()
        {
            ModelObtenPeriodosProgramaRequest request = new ModelObtenPeriodosProgramaRequest() { };
            List<ModelObtenPeriodosProgramaResponse> response = DB.CallSPListResult<ModelObtenPeriodosProgramaResponse, ModelObtenPeriodosProgramaRequest>(request);
            return ToDataTable(response);
        }

    
        public DataTable ObtenerConceptosDescuento()
        {
            ModelObtenConceptosDescuentoRequest request = new ModelObtenConceptosDescuentoRequest() { };
            List<ModelObtenConceptosDescuentoResponse> response = DB.CallSPListResult<ModelObtenConceptosDescuentoResponse, ModelObtenConceptosDescuentoRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerEscuelasProcedencia()
        {
            ModelObtenEscuelasProcedenciaRequest request = new ModelObtenEscuelasProcedenciaRequest() { };
            List<ModelObtenEscuelasProcedenciaResponse> response = DB.CallSPListResult<ModelObtenEscuelasProcedenciaResponse, ModelObtenEscuelasProcedenciaRequest>(request);
            return ToDataTableForDropDownList(response);
        }


        public DataTable ObtenerGridEscuelasProcedencia()
        {
            ModelObtenEscuelasProcedenciaRequest request = new ModelObtenEscuelasProcedenciaRequest() { };
            List<ModelObtenEscuelasProcedenciaResponse> response = DB.CallSPListResult<ModelObtenEscuelasProcedenciaResponse, ModelObtenEscuelasProcedenciaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerPeriodosEscolaresVigentes()
        {
            ModelObtenPeriodosEscolaresVigentesRequest request = new ModelObtenPeriodosEscolaresVigentesRequest() { };
            List<ModelObtenPeriodosEscolaresVigentesResponse> response = DB.CallSPListResult<ModelObtenPeriodosEscolaresVigentesResponse, ModelObtenPeriodosEscolaresVigentesRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerPeriodosEscolaresActivos()
        {
            ModelObtenPeriodosEscolaresActivosRequest request = new ModelObtenPeriodosEscolaresActivosRequest() { };
            List<ModelObtenPeriodosEscolaresActivosResponse> response = DB.CallSPListResult<ModelObtenPeriodosEscolaresActivosResponse, ModelObtenPeriodosEscolaresActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }
        public string InsertarPeriodoEscolar(int p_clave, string p_descripcion, string p_oficio, string p_fecha_inicio, string p_fecha_fin, string p_usuario, string p_estatus)
        {
            ModelInsertarPeriodoEscolar Insert = new ModelInsertarPeriodoEscolar()
            {
                clave = p_clave,
                descripcion = p_descripcion,
                oficio = p_oficio,
                fecha_inicio = p_fecha_inicio,
                fecha_fin = p_fecha_fin,
                usuario = p_usuario,
                estatus = p_estatus

            };
            return DB.CallSPForInsertUpdate(Insert);
        }


        public string EditarPeriodoEscolar(int p_clave, string p_descripcion, string p_oficio, string p_fecha_inicio, string p_fecha_fin, string p_usuario, string p_estatus)
        {
            ModelEditarPeriodoEscolar Insert = new ModelEditarPeriodoEscolar()
            {
                clave = p_clave,
                descripcion = p_descripcion,
                oficio = p_oficio,
                fecha_inicio = p_fecha_inicio,
                fecha_fin = p_fecha_fin,
                usuario = p_usuario,
                estatus = p_estatus

            };
            return DB.CallSPForInsertUpdate(Insert);
        }
        public string InsertarPais(string p_clave, string p_descripcion, string p_gentil, string p_usuario, string p_estatus)
        {
            ModelInsertarPais Insert = new ModelInsertarPais()
            {
                clave = p_clave,
                nombre = p_descripcion,
                gentil = p_gentil,
                usuario = p_usuario,
                estatus = p_estatus
            };
            return DB.CallSPForInsertUpdate(Insert);
        }






        public DataTable ObtenerBajas(string matricula, string programa)
        {
            ModelObtenBajasRequest request = new ModelObtenBajasRequest() { p_tbaap_tpers_num = matricula, tbaap_tprog_clave = programa };
            List<ModelObtenBajasResponse> response = DB.CallSPListResult<ModelObtenBajasResponse, ModelObtenBajasRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerTipoBajas()
        {
            ModelObtenTipoBajasRequest request = new ModelObtenTipoBajasRequest() { };
            List<ModelObtenTipoBajasResponse> response = DB.CallSPListResult<ModelObtenTipoBajasResponse, ModelObtenTipoBajasRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarTipoBajaResponse InsertarTipoBaja(string p_ttiba_clave, string p_ttiba_desc, string p_ttiba_user, string p_ttiba_estatus)
        {
            ModelInsertarTipoBaja Insert = new ModelInsertarTipoBaja()
            {
                ttiba_clave = p_ttiba_clave,
                ttiba_desc = p_ttiba_desc,
                ttiba_user = p_ttiba_user,
                ttiba_estatus = p_ttiba_estatus
            };
            ModelInsertarTipoBajaResponse response = DB.CallSPResult<ModelInsertarTipoBajaResponse, ModelInsertarTipoBaja>(Insert);
            return response;
        }

        public string EditarTipoBaja(string p_ttiba_clave, string p_ttiba_desc, string p_ttiba_user, string p_ttiba_estatus)
        {
            ModelEditarTipoBaja Editar = new ModelEditarTipoBaja()
            {
                ttiba_clave = p_ttiba_clave,
                ttiba_desc = p_ttiba_desc,
                ttiba_user = p_ttiba_user,
                ttiba_estatus = p_ttiba_estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public string EditarPais(string p_clave, string p_descripcion, string p_gentil, string p_usuario, string p_estatus)
        {
            ModelEditarPais Editar = new ModelEditarPais()
            {
                clave = p_clave,
                nombre = p_descripcion,
                gentil = p_gentil,
                usuario = p_usuario,
                estatus = p_estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public List<ModelObtenPaisesResponse> ObtenerPaises()
        {
            ModelObtenPaisesRequest request = new ModelObtenPaisesRequest() { };
            List<ModelObtenPaisesResponse> response = DB.CallSPListResult<ModelObtenPaisesResponse, ModelObtenPaisesRequest>(request);
            return response;
        }

        public List<ModelObtenPaisesActivosResponse> ObtenerPaisesActivos()
        {
            ModelObtenPaisesActivosRequest request = new ModelObtenPaisesActivosRequest() { };
            List<ModelObtenPaisesActivosResponse> response = DB.CallSPListResult<ModelObtenPaisesActivosResponse, ModelObtenPaisesActivosRequest>(request);
            return response;
        }

        public List<ModelObtenDelegacionesResponse> ObtenerDelegaciones(string P_Pais, string P_Municipio, string P_Estatus)
        {
            ModelObtenDelegacionesRequest request = new ModelObtenDelegacionesRequest() { pais = P_Pais, municipio = P_Municipio, estatus = P_Estatus };
            List<ModelObtenDelegacionesResponse> response = DB.CallSPListResult<ModelObtenDelegacionesResponse, ModelObtenDelegacionesRequest>(request);
            return response;
        }

        public List<ModelObtenMunicipiosResponse> ObtenerMunicipios(string P_Pais, string P_Estado)
        {
            ModelObtenMunicipiosRequest request = new ModelObtenMunicipiosRequest() { pais = P_Pais, estado = P_Estado };
            List<ModelObtenMunicipiosResponse> response = DB.CallSPListResult<ModelObtenMunicipiosResponse, ModelObtenMunicipiosRequest>(request);
            return response;
        }

        public List<ModelObtenDelegacionesResponse> ObtenerColonias(string P_Pais, string P_Municipio, string P_Estatus)
        {
            ModelObtenDelegacionesRequest request = new ModelObtenDelegacionesRequest() { pais = P_Pais, municipio = P_Municipio, estatus = P_Estatus };
            List<ModelObtenDelegacionesResponse> response = DB.CallSPListResult<ModelObtenDelegacionesResponse, ModelObtenDelegacionesRequest>(request);
            return response;
        }

        public DataTable ObtenerCatCampus()
        {
            ModelObtenCatCampusRequest request = new ModelObtenCatCampusRequest() { };
            List<ModelObtenCatCampusResponse> response = DB.CallSPListResult<ModelObtenCatCampusResponse, ModelObtenCatCampusRequest>(request);
            return ToDataTable(response);
        }

        public List<ModelObtenEstadosResponse> ObtenerEstados(string P_Pais)
        {
            ModelObtenEstadosRequest request = new ModelObtenEstadosRequest() { pais = P_Pais };
            List<ModelObtenEstadosResponse> response = DB.CallSPListResult<ModelObtenEstadosResponse, ModelObtenEstadosRequest>(request);
            return response;
        }


        public DataTable ObtenerCampus()
        {
            ModelObtenCampusRequest request = new ModelObtenCampusRequest() { };
            List<ModelObtenCampusResponse> response = DB.CallSPListResult<ModelObtenCampusResponse, ModelObtenCampusRequest>(request);
            return ToDataTableForDropDownList(response);
        }

     
        public DataTable ObtenerCampus2()
        {
            ModelObtenCampusRequest request = new ModelObtenCampusRequest() { };
            List<ModelObtenCampusResponse> response = DB.CallSPListResult<ModelObtenCampusResponse, ModelObtenCampusRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerCampusVigentes()
        {
            ModelObtenCampusVigentesRequest request = new ModelObtenCampusVigentesRequest() { };
            List<ModelObtenCampusVigentesResponse> response = DB.CallSPListResult<ModelObtenCampusVigentesResponse, ModelObtenCampusVigentesRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerHoras()
        {
            ModelObtenHoraRequest request = new ModelObtenHoraRequest() { };
            List<ModelObtenHoraResponse> response = DB.CallSPListResult<ModelObtenHoraResponse, ModelObtenHoraRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public List<ModelObtenTurnosResponse> ObtenerTurnos()
        {
            ModelObtenTurnosRequest request = new ModelObtenTurnosRequest() { };
            List<ModelObtenTurnosResponse> response = DB.CallSPListResult<ModelObtenTurnosResponse, ModelObtenTurnosRequest>(request);
            return response;
        }

        public DataTable ObtenerPeriodosEscolares(string tipo, string parametro1)
        {
            ModelObtenComboRequest request = new ModelObtenComboRequest() { Tipo = tipo, Parametro1 = parametro1 };
            List<ModelObtenComboResponse> response = DB.CallSPListResult<ModelObtenComboResponse, ModelObtenComboRequest>(request);
            return ToDataTableForDropDownList(response);
            //return response;
        }

        public DataTable ObtenerDias()
        {
            ModelObtenComboRequest request = new ModelObtenComboRequest() { Tipo = "Dias", Parametro1 = "" };
            List<ModelObtenComboResponse> response = DB.CallSPListResult<ModelObtenComboResponse, ModelObtenComboRequest>(request);
            return ToDataTableForDropDownList(response);
            //return response;
        }

        public DataTable ObtenerHoraInicial()
        {
            ModelObtenComboRequest request = new ModelObtenComboRequest() { Tipo = "Hora_Inicial", Parametro1 = "" };
            List <ModelObtenComboResponse> response = DB.CallSPListResult<ModelObtenComboResponse, ModelObtenComboRequest>(request);
            return ToDataTableForDropDownList(response);
            //return response;
        }

        public DataTable ObtenerHoraFinal(string parametro1)
        {
            ModelObtenComboRequest request = new ModelObtenComboRequest() { Tipo = "Hora_Final", Parametro1 = parametro1 };
            List<ModelObtenComboResponse> response = DB.CallSPListResult<ModelObtenComboResponse, ModelObtenComboRequest>(request);
            return ToDataTableForDropDownList(response);
            //return response;
        }


        public DataTable ObtenerComboComun(string tipo, string parametro1)
        {
            ModelObtenComboRequest request = new ModelObtenComboRequest() { Tipo = tipo, Parametro1 = parametro1 };
            List<ModelObtenComboResponse> response = DB.CallSPListResult<ModelObtenComboResponse, ModelObtenComboRequest>(request);
            return ToDataTableForDropDownList(response);
            //return response;
        }

        public DataTable ObtenerTcoco(string tipo, string parametro1)
        {
            ModelObtenComboRequest request = new ModelObtenComboRequest() { Tipo = tipo, Parametro1 = parametro1 };
            List<ModelObtenComboResponse> response = DB.CallSPListResult<ModelObtenComboResponse, ModelObtenComboRequest>(request);
            return ToDataTableForDropDownList(response);
            //return response;
        }

        public DataTable obtenGridFuncionarios()
        {
            ModelTorcaRequest request = new ModelTorcaRequest() { };
            List<ModelTorcaResponse> response = DB.CallSPListResult<ModelTorcaResponse, ModelTorcaRequest>(request);
            return ToDataTable(response);
        }
        public DataTable obtenFuncionarios()
        {
            ModelTorcaRequest request = new ModelTorcaRequest() { };
            List<ModelTorcaResponse> response = DB.CallSPListResult<ModelTorcaResponse, ModelTorcaRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenFuncionariosActivos()
        {
            ModelTorcaActivosRequest request = new ModelTorcaActivosRequest() { };
            List<ModelTorcaActivosResponse> response = DB.CallSPListResult<ModelTorcaActivosResponse, ModelTorcaActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }


        public DataTable obtenFuncionariosporCampus(string p_campus)
        {
            ModelTfucaRequest request = new ModelTfucaRequest() { Campus = p_campus };
            List<ModelTfucaResponse> response = DB.CallSPListResult<ModelTfucaResponse, ModelTfucaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenTelefonos()
        {
            ModelTelefonoRequest request = new ModelTelefonoRequest() { };
            List<ModelTelefonoResponse> response = DB.CallSPListResult<ModelTelefonoResponse, ModelTelefonoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenTelefonosActivos()
        {
            ModelTelefonoActivosRequest request = new ModelTelefonoActivosRequest() { };
            List<ModelTelefonoActivosResponse> response = DB.CallSPListResult<ModelTelefonoActivosResponse, ModelTelefonoActivosRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenTtasaActivos()
        {
            ModelTasaActivosRequest request = new ModelTasaActivosRequest() { };
            List<ModelTasaActivosResponse> response = DB.CallSPListResult<ModelTasaActivosResponse, ModelTasaActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenTtiinActivos()
        {
            ModelTtiinActivosRequest request = new ModelTtiinActivosRequest() { };
            List<ModelTtiinActivosResponse> response = DB.CallSPListResult<ModelTtiinActivosResponse, ModelTtiinActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public string EditarTelefono(string p_clave_ttele, string p_desc_ttele, string p_usuario, string p_estatus)
        {
            ModelEditarTelefono Editar = new ModelEditarTelefono()
            {
                clave_ttele = p_clave_ttele,
                desc_ttele = p_desc_ttele,
                usuario = p_usuario,
                estatus = p_estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }


        public ModelInsertarTelefonoResponse InsertarTelefono(string p_clave_ttele, string p_desc_ttele, string p_usuario, string p_estatus)
        {
            ModelInsertarTelefono Insert = new ModelInsertarTelefono()
            {
                clave_ttele = p_clave_ttele,
                desc_ttele = p_desc_ttele,
                usuario = p_usuario,
                estatus = p_estatus
            };
            ModelInsertarTelefonoResponse response = DB.CallSPResult<ModelInsertarTelefonoResponse, ModelInsertarTelefono>(Insert);
            return response;
        }

        public ModelInsertarEMailResponse InsertarCorreo(string p_clave_tmail, string p_desc_tmail,
            string p_usuario, string p_estatus)
        {
            ModelInsertarEMail Insert = new ModelInsertarEMail()
            {
                clave_tmail = p_clave_tmail,
                desc_tmail = p_desc_tmail,
                usuario = p_usuario,
                estatus = p_estatus
            };
            ModelInsertarEMailResponse response = DB.CallSPResult<ModelInsertarEMailResponse, ModelInsertarEMail>(Insert);
            return response;
        }

        public string EditarEMail(string p_clave_tmail, string p_desc_tmail, string p_usuario, string p_estatus)
        {
            ModelEditarEMail Editar = new ModelEditarEMail()
            {
                clave_tmail = p_clave_tmail,
                desc_tmail = p_desc_tmail,
                usuario = p_usuario,
                estatus = p_estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public DataTable obtenContactos()
        {
            ModelContactosRequest request = new ModelContactosRequest() { };
            List<ModelContactosResponse> response = DB.CallSPListResult<ModelContactosResponse, ModelContactosRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public ModelInsertarContactoResponse InsertarContacto(string p_clave_tcont,
            string p_desc_tcont, string p_usuario, string p_estatus)
        {
            ModelInsertarContacto Insert = new ModelInsertarContacto()
            {
                clave_tcont = p_clave_tcont,
                desc_tcont = p_desc_tcont,
                usuario = p_usuario,
                estatus = p_estatus
            };
            ModelInsertarContactoResponse response = DB.CallSPResult<ModelInsertarContactoResponse, ModelInsertarContacto>(Insert);
            return response;
        }


        public string EditarContacto(string p_clave_tcont,
            string p_desc_tcont, string p_usuario, string p_estatus)
        {
            ModelEditarContacto Editar = new ModelEditarContacto()
            {
                clave_tcont = p_clave_tcont,
                desc_tcont = p_desc_tcont,
                usuario = p_usuario,
                estatus = p_estatus
            };
            return DB.CallSPForInsertUpdate(Editar);
        }




        public List<ModeltdireResponse> QRY_TDIRE()
        {
            ModeltdireRequest request = new ModeltdireRequest() { };
            List<ModeltdireResponse> response = DB.CallSPListResult<ModeltdireResponse, ModeltdireRequest>(request);
            return response;
        }

        public DataTable obtenTDireActivos()
        {
            ModelTdireActivosRequest request = new ModelTdireActivosRequest() { };
            List<ModelTdireActivosResponse> response = DB.CallSPListResult<ModelTdireActivosResponse, ModelTdireActivosRequest>(request);
            return ToDataTableForDropDownList(response);
        }



        public ModelInstdireResponse Ins_tdire(string clave_dire, string descripcion, string usu_alta, string estatus)
        {
            ModelInstdire Insert = new ModelInstdire()
            {
                tdire_clave = clave_dire,
                tdire_desc = descripcion,
                tdire_user = usu_alta,
                tdire_estatus = estatus
            };
            ModelInstdireResponse response = DB.CallSPResult<ModelInstdireResponse, ModelInstdire>(Insert);
            return response;
        }

        public ModelInstorcaResponse Ins_torca(string p_torca_clave, string p_torca_desc, string p_torca_user, string p_torca_estatus)
        {
            ModelInstorca Insert = new ModelInstorca()
            {
                torca_clave = p_torca_clave,
                torca_desc = p_torca_desc,
                torca_user = p_torca_user,
                torca_estatus = p_torca_estatus
            };
            ModelInstorcaResponse response = DB.CallSPResult<ModelInstorcaResponse, ModelInstorca>(Insert);
            return response;
        }

        public string Upd_torca(string p_torca_clave, string p_torca_desc, string p_torca_user, string p_torca_estatus)
        {
            ModelUpdtorca Update = new ModelUpdtorca()
            {
                torca_clave = p_torca_clave,
                torca_desc = p_torca_desc,
                torca_user = p_torca_user,
                torca_estatus = p_torca_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public ModelInstfucaResponse Ins_tfuca(string p_tfuca_tcamp_clave, string p_tfuca_clave, string p_tfuca_desc, string p_tfuca_nombre,
            string p_tfuca_paterno, string p_tfuca_materno, string p_tfuca_curp, string p_tfuca_tuser_clave, string p_tfuca_estatus)
        {
            ModelInstfuca Insert = new ModelInstfuca()
            {
                tfuca_tcamp_clave = p_tfuca_tcamp_clave,
                tfuca_clave = p_tfuca_clave,
                tfuca_desc = p_tfuca_desc,
                tfuca_nombre = p_tfuca_nombre,
                tfuca_paterno= p_tfuca_paterno,
                tfuca_materno= p_tfuca_materno,
                tfuca_curp= p_tfuca_curp,
                tfuca_tuser_clave= p_tfuca_tuser_clave,
                tfuca_estatus= p_tfuca_estatus
            };
            ModelInstfucaResponse response = DB.CallSPResult<ModelInstfucaResponse, ModelInstfuca>(Insert);
            return response;
        }

        public string Upd_tfuca(string p_tfuca_tcamp_clave, string p_tfuca_clave, string p_tfuca_desc, string p_tfuca_nombre,
            string p_tfuca_paterno, string p_tfuca_materno, string p_tfuca_curp, string p_tfuca_tuser_clave, string p_tfuca_estatus)
        {
            ModelUpdtfuca Update = new ModelUpdtfuca()
            {
                tfuca_tcamp_clave = p_tfuca_tcamp_clave,
                tfuca_clave = p_tfuca_clave,
                tfuca_desc = p_tfuca_desc,
                tfuca_nombre = p_tfuca_nombre,
                tfuca_paterno = p_tfuca_paterno,
                tfuca_materno = p_tfuca_materno,
                tfuca_curp = p_tfuca_curp,
                tfuca_tuser_clave = p_tfuca_tuser_clave,
                tfuca_estatus = p_tfuca_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public ModelInsTtiniResponse Ins_ttini(string p_ttini_ttiop_clave, string p_ttini_tnive_clave, string p_ttini_creditos, string p_ttini_promedio,
    string p_ttini_tcoco_clave, string p_ttini_tuser_clave, string p_ttini_tseso)
        {
            ModelInsTtini Insert = new ModelInsTtini()
            {
                ttini_ttiop_clave = p_ttini_ttiop_clave,
                ttini_tnive_clave = p_ttini_tnive_clave,
                ttini_creditos = p_ttini_creditos,
                ttini_promedio = p_ttini_promedio,
                ttini_tcoco_clave = p_ttini_tcoco_clave,
                ttini_tuser_clave = p_ttini_tuser_clave,
                ttini_tseso= p_ttini_tseso
            };
            ModelInsTtiniResponse response = DB.CallSPResult<ModelInsTtiniResponse, ModelInsTtini>(Insert);
            return response;
        }

        public string Upd_ttini(string p_ttini_ttiop_clave, string p_ttini_tnive_clave, string p_ttini_creditos, string p_ttini_promedio,
    string p_ttini_tcoco_clave, string p_ttini_tuser_clave, string p_ttini_tseso)
        {
            ModelUpdttini Update = new ModelUpdttini()
            {
                ttini_ttiop_clave = p_ttini_ttiop_clave,
                ttini_tnive_clave = p_ttini_tnive_clave,
                ttini_creditos = p_ttini_creditos,
                ttini_promedio = p_ttini_promedio,
                ttini_tcoco_clave = p_ttini_tcoco_clave,
                ttini_tuser_clave = p_ttini_tuser_clave,
                ttini_tseso = p_ttini_tseso
            };
            return DB.CallSPForInsertUpdate(Update);
        }


        public string Upd_tdire(string clave_dire, string descripcion, string usu_alta, string estatus)
        {
            ModelUpdtdire Update = new ModelUpdtdire()
            {
                tdire_clave = clave_dire,
                tdire_desc = descripcion,
                tdire_user = usu_alta,
                tdire_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

     

        public DataTable QRY_TCOPO(string pais, string estado, string municipio)
        {
            ModeltpocoRequest request = new ModeltpocoRequest() { p_pais = pais, p_estado = estado, p_municipio = municipio };
            List<ModeltpocoResponse> response = DB.CallSPListResult<ModeltpocoResponse, ModeltpocoRequest>(request);
            return ToDataTable(response);
        }

        public string UPD_TCOPO(string p_usuario, string p_clave, string p_estatus, string p_pais,
            string p_estado, string p_municipio, string p_tcopo_desc)
        {
            ModelUpdtpoco Update = new ModelUpdtpoco()
            {
                usuario = p_usuario,
                clave = p_clave,
                estatus = p_estatus,
                pais = p_pais,
                estado = p_estado,
                municipio = p_municipio,
                descripcion = p_tcopo_desc
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public ModelInstcopoResponse Ins_tcopo(string p_clave, string p_descripcion, string p_estatus, string p_pais,
           string p_estado, string p_municipio, string p_usuario)
        {
            ModelInstcopo Insert = new ModelInstcopo()
            {
                clave = p_clave,
                descripcion = p_descripcion,
                estatus = p_estatus,
                pais = p_pais,
                estado = p_estado,
                municipio = p_municipio,
                usuario = p_usuario
            };
            ModelInstcopoResponse response = DB.CallSPResult<ModelInstcopoResponse, ModelInstcopo>(Insert);
            return response;
        }

        public List<ModeltpaisResponse> QRY_TPAIS()
        {
            ModeltpaisRequest request = new ModeltpaisRequest() { };
            List<ModeltpaisResponse> response = DB.CallSPListResult<ModeltpaisResponse, ModeltpaisRequest>(request);
            return response;
        }

        public DataTable QRY_ESTADOS(string p_pais)
        {
            ModelEstadosRequest request = new ModelEstadosRequest() { pais = p_pais };
            List<ModelEstadosResponse> response = DB.CallSPListResult<ModelEstadosResponse, ModelEstadosRequest>(request);
            return ToDataTableForDropDownList(response);
        }
        public DataTable Qry_Estados_Grid(string p_pais)
        {
            ModelEstadosRequest request = new ModelEstadosRequest() { pais = p_pais };
            List<ModelEstadosResponse> response = DB.CallSPListResult<ModelEstadosResponse, ModelEstadosRequest>(request);
            return ToDataTable(response);
        }
        public DataTable QRY_MUNICIPIOS(string p_pais, string p_estado)
        {
            ModelMunicipiosRequest request = new ModelMunicipiosRequest() { pais = p_pais, estado = p_estado };
            List<ModelMunicipiosResponse> response = DB.CallSPListResult<ModelMunicipiosResponse, ModelMunicipiosRequest>(request);
            return ToDataTable(response);
        }

        public DataTable QRY_TESTA()
        {
            ModeltestaRequest request = new ModeltestaRequest() { };
            List<ModeltestaResponse> response = DB.CallSPListResult<ModeltestaResponse, ModeltestaRequest>(request);
            return ToDataTable(response);
        }


        public ModelInstestaResponse Ins_testa(string clave_esta, string clave_pais, string descripcion, string usu_alta, string estatus)
        {
            ModelInstesta Insert = new ModelInstesta()
            {
                testa_clave = clave_esta,
                testa_tpais_clave = clave_pais,
                testa_desc = descripcion,
                testa_tuser_clave = usu_alta,
                testa_estatus = estatus
            };
            ModelInstestaResponse response = DB.CallSPResult<ModelInstestaResponse, ModelInstesta>(Insert);
            return response;
        }

        public string Upd_testa(string clave_esta, string clave_pais, string descripcion, string usu_alta, string estatus)
        {
            ModelUpdtesta Update = new ModelUpdtesta()
            {
                testa_clave = clave_esta,
                testa_tpais_clave = clave_pais,
                testa_desc = descripcion,
                testa_tuser_clave = usu_alta,
                testa_estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public DataTable obtenCatDocumentos()
        {
            ModelCatDoctosRequest request = new ModelCatDoctosRequest() { };
            List<ModelCatDoctosResponse> response = DB.CallSPListResult<ModelCatDoctosResponse, ModelCatDoctosRequest>(request);
            return ToDataTable(response);
        }


        public ModelConceptoResponse obtenImporteConcepto(string p_tprog_clave, string p_tcamp_clave, string p_tcoco_clave)
        {
            ModelConcepto model = new ModelConcepto() { tprog_clave= p_tprog_clave, tcamp_clave= p_tcamp_clave, tcoco_clave= p_tcoco_clave };
            ModelConceptoResponse response = DB.CallSPResult<ModelConceptoResponse, ModelConcepto>(model);
            return response;
        }

        public ModelInsertarCatDoctosResponse InsertarCatCampus(string p_tdocu_clave, string p_tdocu_desc,
            string p_tdocu_tipo, string p_tdocu_fis_dig, string p_tdocu_estatus, string p_tdocu_tuser_clave
            )
        {
            ModelInsertarCatDoctos Insert = new ModelInsertarCatDoctos()
            {
                tdocu_clave = p_tdocu_clave,
                tdocu_desc = p_tdocu_desc,
                tdocu_tipo = p_tdocu_tipo,
                tdocu_fis_dig = p_tdocu_fis_dig,
                tdocu_estatus = p_tdocu_estatus,
                tdocu_tuser_clave = p_tdocu_tuser_clave
            };
            ModelInsertarCatDoctosResponse response = DB.CallSPResult<ModelInsertarCatDoctosResponse, ModelInsertarCatDoctos>(Insert);
            return response;
        }

        public ModelInsertarTesprResponse InsertarTespr(string p_tespr_clave, string p_tespr_desc, string p_tespr_user, string p_tespr_estatus)
        {
            ModelInsertarTespr Insert = new ModelInsertarTespr()
            {
                tespr_clave = p_tespr_clave,
                tespr_desc = p_tespr_desc,
                tespr_user = p_tespr_user,
                tespr_estatus = p_tespr_estatus
            };
            ModelInsertarTesprResponse response = DB.CallSPResult<ModelInsertarTesprResponse, ModelInsertarTespr>(Insert);
            return response;
        }

        public ModelInsertarTstalResponse InsertarTstal(string p_tstal_clave, string p_tstal_desc, string p_tstal_user, string p_tstal_estatus, string p_tstal_tipo)
        {
            ModelInsertarTstal Insert = new ModelInsertarTstal()
            {
                tstal_clave = p_tstal_clave,
                tstal_desc = p_tstal_desc,
                tstal_user = p_tstal_user,
                tstal_estatus = p_tstal_estatus,
                tstal_tipo = p_tstal_tipo
            };
            ModelInsertarTstalResponse response = DB.CallSPResult<ModelInsertarTstalResponse, ModelInsertarTstal>(Insert);
            return response;
        }

        public string EditarTstal(string p_tstal_clave, string p_tstal_desc, string p_tstal_user, string p_tstal_estatus, string p_tstal_tipo)
        {
            ModelEditarTstal Editar = new ModelEditarTstal()
            {
                tstal_clave = p_tstal_clave,
                tstal_desc = p_tstal_desc,
                tstal_user = p_tstal_user,
                tstal_estatus = p_tstal_estatus,
                tstal_tipo = p_tstal_tipo
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public string EditarTespr(string p_tespr_clave, string p_tespr_desc, string p_tespr_user, string p_tespr_estatus)
        {
            ModelEditarTespr Update = new ModelEditarTespr()
            {
                tespr_clave = p_tespr_clave,
                tespr_desc = p_tespr_desc,
                tespr_user = p_tespr_user,
                tespr_estatus = p_tespr_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public DataTable QRY_TSTAL()
        {
            ModelTstalsRequest request = new ModelTstalsRequest() { };
            List<ModelTstalResponse> response = DB.CallSPListResult<ModelTstalResponse, ModelTstalsRequest>(request);
            return ToDataTable(response);
        }


        public DataTable obtenComponentesCalificacion()
        {
            ModelTcompRequest request = new ModelTcompRequest() { };
            List<ModelTcompResponse> response = DB.CallSPListResult<ModelTcompResponse, ModelTcompRequest>(request);
            return ToDataTable(response);
        }
        public ModelInsertarTcompResponse InsertarTcomp(string p_tcomp_clave, string p_tcomp_desc, string p_tcomp_estatus, string p_tcomp_user)
        {
            ModelInsertarTcomp Insert = new ModelInsertarTcomp()
            {
                tcomp_clave = p_tcomp_clave,
                tcomp_desc = p_tcomp_desc,
                tcomp_estatus = p_tcomp_estatus,
                tcomp_user = p_tcomp_user
            };
            ModelInsertarTcompResponse response = DB.CallSPResult<ModelInsertarTcompResponse, ModelInsertarTcomp>(Insert);
            return response;
        }

        public string EditarTcomp(string p_tcomp_clave, string p_tcomp_desc, string p_tcomp_estatus, string p_tcomp_user)
        {
            ModelEditarTcomp Editar = new ModelEditarTcomp()
            {
                tcomp_clave = p_tcomp_clave,
                tcomp_desc = p_tcomp_desc,
                tcomp_estatus = p_tcomp_estatus,
                tcomp_user = p_tcomp_user
            };
            return DB.CallSPForInsertUpdate(Editar);
        }
        public DataTable obtenProgramaNivel()
        {
            ModelTProgTNiveRequest request = new ModelTProgTNiveRequest() { };
            List<ModelTProgTNiveResponse> response = DB.CallSPListResult<ModelTProgTNiveResponse, ModelTProgTNiveRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenSeriacionMaterias(string p_tseri_tprog_clave, string p_tplan_tmate_clave1, string p_tplan_tmate_clave2, string p_tplan_tmate_clave3, string p_tplan_tmate_clave4)
        {
            ModelTSeriRequest request = new ModelTSeriRequest()
            {
                tseri_tprog_clave = p_tseri_tprog_clave,
                tplan_tmate_clave1 = p_tplan_tmate_clave1,
                tplan_tmate_clave2 = p_tplan_tmate_clave2,
                tplan_tmate_clave3 = p_tplan_tmate_clave3,
                tplan_tmate_clave4 = p_tplan_tmate_clave4
            };
            List<ModelTSeriResponse> response = DB.CallSPListResult<ModelTSeriResponse, ModelTSeriRequest>(request);
            return ToDataTable(response);
        }
        public DataTable obtenPlanMaterias(string p_tplan_tprog_clave)
        {
            ModelPlanMateriasRequest request = new ModelPlanMateriasRequest() { tplan_tprog_clave = p_tplan_tprog_clave };
            List<ModelPlanMateriasResponse> response = DB.CallSPListResult<ModelPlanMateriasResponse, ModelPlanMateriasRequest>(request);
            return ToDataTable(response);
        }

        //public DataTable obtenMaterias()
        //{
        //    ModelTSeriRequest request = new ModelTSeriRequest() { };
        //    List<ModelTSeriResponse> response = DB.CallSPListResult<ModelTSeriResponse, ModelTSeriRequest>(request);
        //    return ToDataTable(response);
        //}

        public DataTable obtenTNive()
        {
            ModelTNivelRequest request = new ModelTNivelRequest() { };
            List<ModelTNivelResponse> response = DB.CallSPListResult<ModelTNivelResponse, ModelTNivelRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenGridTNive()
        {
            ModelTNivelRequest request = new ModelTNivelRequest() { };
            List<ModelTNivelResponse> response = DB.CallSPListResult<ModelTNivelResponse, ModelTNivelRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenTCole()
        {
            ModelTColeRequest request = new ModelTColeRequest() { };
            List<ModelTColeResponse> response = DB.CallSPListResult<ModelTColeResponse, ModelTColeRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenGridTCole()
        {
            ModelTColeRequest request = new ModelTColeRequest() { };
            List<ModelTColeResponse> response = DB.CallSPListResult<ModelTColeResponse, ModelTColeRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenTArea()
        {
            ModelTAreaRequest request = new ModelTAreaRequest() { };
            List<ModelTAreaResponse> response = DB.CallSPListResult<ModelTAreaResponse, ModelTAreaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenTModa()
        {
            ModelTModaRequest request = new ModelTModaRequest() { };
            List<ModelTModaResponse> response = DB.CallSPListResult<ModelTModaResponse, ModelTModaRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarTcoleResponse InsertarColegio(string p_tcole_clave, string p_tcole_desc, string p_tcole_user, string p_tcole_estatus
            )
        {
            ModelInsertarTcole Insert = new ModelInsertarTcole()
            {
                tcole_clave = p_tcole_clave,
                tcole_desc = p_tcole_desc,
                tcole_user = p_tcole_user,
                tcole_estatus = p_tcole_estatus
            };
            ModelInsertarTcoleResponse response = DB.CallSPResult<ModelInsertarTcoleResponse, ModelInsertarTcole>(Insert);
            return response;

            //return DB.CallSPForInsertUpdate(Insert);
        }

        public ModelInsertarTareaResponse InsertarTarea(string p_tarea_clave, string p_tarea_desc, string p_tarea_user, string p_tarea_ind_espec,
        string p_tarea_estatus)
        {
            ModelInsertarTarea Insert = new ModelInsertarTarea()
            {
                tarea_clave = p_tarea_clave,
                tarea_desc = p_tarea_desc,
                tarea_user = p_tarea_user,
                tarea_ind_espec = p_tarea_ind_espec,
                tarea_estatus = p_tarea_estatus
            };
            ModelInsertarTareaResponse response = DB.CallSPResult<ModelInsertarTareaResponse, ModelInsertarTarea>(Insert);
            return response;
        }

        public string EditarTarea(string p_tarea_clave, string p_tarea_desc, string p_tarea_user, string p_tarea_ind_espec, string p_tarea_estatus)
        {
            ModelEditarTarea Update = new ModelEditarTarea()
            {
                tarea_clave = p_tarea_clave,
                tarea_desc = p_tarea_desc,
                tarea_user = p_tarea_user,
                tarea_ind_espec = p_tarea_ind_espec,
                tarea_estatus = p_tarea_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public string EditarColegio(string p_tcole_clave, string p_tcole_desc, string p_tcole_user, string p_tcole_estatus)
        {
            ModelEditarTcole Update = new ModelEditarTcole()
            {
                tcole_clave = p_tcole_clave,
                tcole_desc = p_tcole_desc,
                tcole_user = p_tcole_user,
                tcole_estatus = p_tcole_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public ModelInsertarTModaResponse InsertarTModa(string p_tmoda_clave, string p_tmoda_desc, string p_tmoda_user, string p_tmoda_estatus)
        {
            ModelInsertarTModa Insert = new ModelInsertarTModa()
            {
                tmoda_clave = p_tmoda_clave,
                tmoda_desc = p_tmoda_desc,
                tmoda_user = p_tmoda_user,
                tmoda_estatus = p_tmoda_estatus
            };
            ModelInsertarTModaResponse response = DB.CallSPResult<ModelInsertarTModaResponse, ModelInsertarTModa>(Insert);
            return response;
        }

        public string EditarTModa(string p_tmoda_clave, string p_tmoda_desc, string p_tmoda_user, string p_tmoda_estatus)
        {
            ModelEditarTModa Update = new ModelEditarTModa()
            {
                tmoda_clave = p_tmoda_clave,
                tmoda_desc = p_tmoda_desc,
                tmoda_user = p_tmoda_user,
                tmoda_estatus = p_tmoda_estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public DataTable obtenPrograma()
        {
            ModelTProgRequest request = new ModelTProgRequest() { };
            List<ModelTProgResponse> response = DB.CallSPListResult<ModelTProgResponse, ModelTProgRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerMaterias()
        {
            ModelObtenTMateRequest request = new ModelObtenTMateRequest() { };
            List<ModelObtenTMateResponse> response = DB.CallSPListResult<ModelObtenTMateResponse, ModelObtenTMateRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarTMateResponse InsertarTMate(string p_tmate_clave, string p_tmate_desc, string p_tmate_creditos, string p_tmate_hr_teo,
            string p_tmate_hr_prac, string p_tmate_hr_campo, string p_tmate_min_cupo, string p_tmate_max_cupo, string p_tmate_clave_incorp, 
            string p_tmate_tipo, string p_tmate_estatus, string p_tmate_usuario
            )
        {
            ModelInsertarTMate Insert = new ModelInsertarTMate()
            {
                tmate_clave = p_tmate_clave,
                tmate_desc = p_tmate_desc,
                tmate_creditos = p_tmate_creditos,
                tmate_hr_teo = p_tmate_hr_teo,
                tmate_hr_prac = p_tmate_hr_prac,
                tmate_hr_campo = p_tmate_hr_campo,
                tmate_min_cupo = p_tmate_min_cupo,
                tmate_max_cupo= p_tmate_max_cupo,
                tmate_clave_incorp= p_tmate_clave_incorp,
                tmate_tipo= p_tmate_tipo,
                tmate_estatus= p_tmate_estatus,
                tmate_usuario= p_tmate_usuario
            };
            ModelInsertarTMateResponse response = DB.CallSPResult<ModelInsertarTMateResponse, ModelInsertarTMate>(Insert);
            return response;
        }

        public string EditarTMate(string p_tmate_clave, string p_tmate_desc, string p_tmate_creditos, string p_tmate_hr_teo,
       string p_tmate_hr_prac, string p_tmate_hr_campo, string p_tmate_min_cupo, string p_tmate_max_cupo, string p_tmate_clave_incorp,
       string p_tmate_tipo, string p_tmate_estatus, string p_tmate_usuario
       )
        {
            ModelEditarTMate Update = new ModelEditarTMate()
            {
                tmate_clave = p_tmate_clave,
                tmate_desc = p_tmate_desc,
                tmate_creditos = p_tmate_creditos,
                tmate_hr_teo = p_tmate_hr_teo,
                tmate_hr_prac = p_tmate_hr_prac,
                tmate_hr_campo = p_tmate_hr_campo,
                tmate_min_cupo = p_tmate_min_cupo,
                tmate_max_cupo = p_tmate_max_cupo,
                tmate_clave_incorp = p_tmate_clave_incorp,
                tmate_tipo = p_tmate_tipo,
                tmate_estatus = p_tmate_estatus,
                tmate_usuario = p_tmate_usuario
            };
            return DB.CallSPForInsertUpdate(Update);
        }
    }


    public class RowDropDownList
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
