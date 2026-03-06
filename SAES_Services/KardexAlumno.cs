using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCalendarioEscolar;
using static SAES_DBO.Models.ModelGrafica;

namespace SAES_Services
{
    public class KardexAlumno : Methods
    {
        public KardexAlumno() : base() { }

        public DataTable ObtenKardexAlumno(int Num_Pers, string Clave_prog)
        {
            ModelKardexAlumnoRequest request = new ModelKardexAlumnoRequest() { NumeroPersona = Num_Pers, ClavePorgrama = Clave_prog };
            return DB.CallSPDataTableResult(request);
        }

        public DataTable ObtenGridStructureForInsert() => ToDataTable(new List<ModelKardexAlumnoForInsertResponse>());

        public string InsertKardexAlumno(int NumeroAlumno, string ClavePrograma, string ClavePeriodo, string ClaveMateria, string Grupo, string ClaveAcreditacion, string ClaveCalificacion, string Usuario)
        {
            ModelInsertKardexAlumnoResponse req = new ModelInsertKardexAlumnoResponse()
            {
                NumeroPersona = NumeroAlumno,
                ClavePrograma = ClavePrograma,
                ClavePeriodo = ClavePeriodo,
                ClaveMateria = ClaveMateria,
                ClaveGrupo = Grupo,
                ClaveAcreditacion = ClaveAcreditacion,
                ClaveCalificacion = ClaveCalificacion,
                User = Usuario
            };
            return DB.CallSPForInsertUpdate(req);
        }

        public string UpdateKardexAlumno(int NumeroAlumno, string ClavePrograma, string ClavePeriodo_old, string ClavePeriodo_new, string ClaveMateria_old, string ClaveMateria_new, string Grupo_old, string Grupo_new, string ClaveAcreditacion_OLD, string ClaveAcreditacion_NEW, string ClaveCalificacion_OLD, string ClaveCalificacion_NEW, string comentario, string Usuario)
        {
            ModelActualizaKardexAlumnoResponse req = new ModelActualizaKardexAlumnoResponse()
            {
                NumeroPersona = NumeroAlumno,
                ClavePrograma = ClavePrograma,
                ClavePeriodo_OLD = ClavePeriodo_old,
                ClavePeriodo_NEW = ClavePeriodo_new,
                ClaveMateria_OLD = ClaveMateria_old,
                ClaveMateria_NEW = ClaveMateria_new,
                ClaveGrupo_OLD = Grupo_old,
                ClaveGrupo_NEW = Grupo_new,
                ClaveAcreditacion_Old = ClaveAcreditacion_OLD,
                ClaveAcreditacion_New = ClaveAcreditacion_NEW,
                ClaveCalificacion_Old = ClaveCalificacion_OLD,
                ClaveCalificacion_New = ClaveCalificacion_NEW,
                Comentario = comentario,
                User = Usuario
            };
            return DB.CallSPForInsertUpdate(req);
        }

        public DataTable ObtenBitacoraKardex(int NumPers, string ClavePrograma, string ClavePeriodo, string ClaveMateria)
        {
            ModelBitacoraKardexAlumnoRequest request = new ModelBitacoraKardexAlumnoRequest() { NumeroPersona = NumPers, ClavePrograma = ClavePrograma, ClavePeriodo = ClavePeriodo, ClaveMateria = ClaveMateria };
            return DB.CallSPDataTableResult(request);
        }
        public DataTable ObtenerAlumnosProg(string Periodo, string Campus, string Nivel, string Programa)
        {
            ModelAlumnoProgRequest request = new ModelAlumnoProgRequest() { testu_periodo = Periodo, testu_campus = Campus, testu_nivel = Nivel, testu_programa = Programa };
            List<ModelAlumnoProgResponse> response = DB.CallSPListResult<ModelAlumnoProgResponse, ModelAlumnoProgRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerMateriasAlumno(string P_Matricula, string P_Programa, string P_Periodo)
        {
            ModelObtenTtiraRequest request = new ModelObtenTtiraRequest() { matricula = P_Matricula, programa = P_Programa, periodo = P_Periodo };
            List<ModelObtenTtiraResponse> response = DB.CallSPListResult<ModelObtenTtiraResponse, ModelObtenTtiraRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerGruposDisp(string P_Materia, string P_Grupo, 
            string P_Periodo, 
            string P_Campus,
            string P_Programa,
            string P_Matricula
            )
        {
            ModelObtenTgrupDispRequest request = new ModelObtenTgrupDispRequest()
            {
                materia = P_Materia,
                grupo = P_Grupo,
                periodo = P_Periodo,
                campus = P_Campus,
                programa= P_Programa,
                matricula= P_Matricula
            };
            List<ModelObtenTgrupDispResponse> response = DB.CallSPListResult<ModelObtenTgrupDispResponse, ModelObtenTgrupDispRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerGruposDisp2(string P_Materia, string P_Grupo, string P_Periodo, string P_Campus)
        {
            ModelObtenTgrupDispRequest request = new ModelObtenTgrupDispRequest()
            {
                materia = P_Materia,
                grupo = P_Grupo,
                periodo = P_Periodo,
                campus = P_Campus
            };
            List<ModelObtenTgrupDispResponse> response = DB.CallSPListResult<ModelObtenTgrupDispResponse, ModelObtenTgrupDispRequest>(request);
            return ToDataTableForDropDownList(response);
        }


        public DataTable ObtenerHorarioAlumno(string P_Matricula, string P_Periodo, string P_Campus, string P_Materia)
        {
            ModelObtenThoraRequest request = new ModelObtenThoraRequest()
            {
                matricula = P_Matricula,
                periodo = P_Periodo,
                campus = P_Campus,
                materia = P_Materia
            };
            List<ModelObtenThoraResponse> response = DB.CallSPListResult<ModelObtenThoraResponse, ModelObtenThoraRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerMateriasDisp(string P_Matricula, string P_Programa, string P_Campus, string P_Periodo)
        {
            ModelObtenTtiraDispRequest request = new ModelObtenTtiraDispRequest()
            {
                matricula = P_Matricula,
                programa = P_Programa,
                campus = P_Campus,
                periodo = P_Periodo
            };
            List<ModelObtenTtiraDispResponse> response = DB.CallSPListResult<ModelObtenTtiraDispResponse, ModelObtenTtiraDispRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerMateriasGrupo(string P_Periodo, string P_Campus, string P_Materia, string P_Grupo)
        {
            ModelObtenHorarioGrupoRequest request = new ModelObtenHorarioGrupoRequest() { periodo = P_Periodo, campus = P_Campus, materia = P_Materia, grupo = P_Grupo };
            List<ModelObtenHorarioGrupoResponse> response = DB.CallSPListResult<ModelObtenHorarioGrupoResponse, ModelObtenHorarioGrupoRequest>(request);
            return ToDataTable(response);
        }

        public ModelValidaTotMateriasResponse ValTotMaterias(string p_matricula, string p_campus, string p_programa, 
            string p_periodo, string p_tipo_materia)
        {
            ModelValidaTotMaterias valida = new ModelValidaTotMaterias()
            {
                matricula = p_matricula,
                campus = p_campus,
                programa = p_programa,
                periodo = p_periodo,
                tipo_materia= p_tipo_materia
            };
            ModelValidaTotMateriasResponse response = DB.CallSPResult<ModelValidaTotMateriasResponse, ModelValidaTotMaterias>(valida);
            return response;
        }

        public string InsertMateriaAlumno(string p_matricula, string p_ttir1_tpees_clave, string p_ttir1_tcamp_clave, string p_ttir1_tprog_clave,
            string p_ttir1_tmate_clave, string p_ttir1_tgrup_clave, string p_ttir1_tnive_clave, string p_ttir1_user)
        {
            ModelInsertMateriaAlumnoResponse req = new ModelInsertMateriaAlumnoResponse()
            {
                matricula = p_matricula,
                ttir1_tpees_clave = p_ttir1_tpees_clave,
                ttir1_tcamp_clave = p_ttir1_tcamp_clave,
                ttir1_tprog_clave = p_ttir1_tprog_clave,
                ttir1_tmate_clave = p_ttir1_tmate_clave,
                ttir1_tgrup_clave = p_ttir1_tgrup_clave,
                ttir1_tnive_clave = p_ttir1_tnive_clave,
                ttir1_user = p_ttir1_user
            };
            return DB.CallSPForInsertUpdate(req);
        }

        public ModelInsertarHoraMateriaResponse InsertHoraMateria(string p_matricula, string p_thora_thocl_inicio, string p_thora_thocl_fin,
          string p_thora_tpees_clave, string p_thora_tmate_clave, string p_thora_tgrup_clave, string p_thora_tdias_clave,
          string p_thora_tcamp_clave
          )
        {
            ModelInsertarHoraMateria Insert = new ModelInsertarHoraMateria()
            {
                matricula = p_matricula,
                thora_thocl_inicio = p_thora_thocl_inicio,
                thora_thocl_fin = p_thora_thocl_fin,
                thora_tpees_clave = p_thora_tpees_clave,
                thora_tmate_clave = p_thora_tmate_clave,
                thora_tgrup_clave = p_thora_tgrup_clave,
                thora_tdias_clave = p_thora_tdias_clave,
                thora_tcamp_clave = p_thora_tcamp_clave
            };
            ModelInsertarHoraMateriaResponse response = DB.CallSPResult<ModelInsertarHoraMateriaResponse, ModelInsertarHoraMateria>(Insert);
            return response;
        }

        public ModelInsertarHorarioAlumnoResponse InsertHorarioAlumno(string P_Matricula, string P_Periodo, string P_Campus,
         string P_Programa, string P_Materia, string P_Grupo, string P_Usuario)
        {

            ModelInsertarHorarioAlumno Insert = new ModelInsertarHorarioAlumno()
            {
                matricula = P_Matricula,
                periodo = P_Periodo,
                campus = P_Campus,
                programa = P_Programa,
                materia = P_Materia,
                grupo = P_Grupo,
                usuario = P_Usuario
            };
            ModelInsertarHorarioAlumnoResponse response = DB.CallSPResult<ModelInsertarHorarioAlumnoResponse, ModelInsertarHorarioAlumno>(Insert);
            return response;
        }

        public ModelInsertarHorarioMasivoAlumnoResponse InsertHorarioMasivoAlumno(string P_Matricula, string P_Periodo, string P_Campus,
         string P_Programa, string P_Materia, string P_Usuario)
        {
            ModelInsertarHorarioMasivo Insert = new ModelInsertarHorarioMasivo()
            {
                matricula = P_Matricula,
                periodo = P_Periodo,
                campus = P_Campus,
                programa = P_Programa,
                materia = P_Materia,
                usuario = P_Usuario
            };
            ModelInsertarHorarioMasivoAlumnoResponse response = DB.CallSPResult<ModelInsertarHorarioMasivoAlumnoResponse, ModelInsertarHorarioMasivo>(Insert);
            return response;
        }

        public string UpdEstatusMateria(string P_Matricula, string P_Periodo, string P_Programa,
          string P_Campus, string P_Materia, string P_Estatus, string P_Grupo,
          string P_Nivel, string P_Usuario
          )
        {
            ModelUpdMateriaAlumno Update = new ModelUpdMateriaAlumno()
            {
                matricula = P_Matricula,
                periodo = P_Periodo,
                programa = P_Programa,
                campus = P_Campus,
                materia = P_Materia,
                estatus = P_Estatus,
                grupo = P_Grupo,
                nivel = P_Nivel,
                usuario = P_Usuario
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public string UpdMateriasPropuestas(string P_Matricula, string P_Periodo, string P_Programa,
    string P_Campus, string P_Usuario
    )
        {
            ModelUpdMateriasPropuestas Update = new ModelUpdMateriasPropuestas()
            {
                matricula = P_Matricula,
                periodo = P_Periodo,
                programa = P_Programa,
                campus = P_Campus,
                usuario = P_Usuario
            };
            return DB.CallSPForInsertUpdate(Update);
        }


        //     public ModelInsTretiResponse Ins_ttir1(string p_matricula, string p_ttir1_tpees_clave, string p_ttir1_tcamp_clave, string p_ttir1_tnive_clave,
        //string p_ttir1_tprog_clave, string p_ttir1_tmate_clave, string p_ttir1_user)
        //     {
        //         ModelInsTtirlRequest Insert = new ModelInsTtirlRequest()
        //         {
        //             matricula = p_matricula,
        //             ttir1_tpees_clave = p_ttir1_tpees_clave,
        //             ttir1_tcamp_clave = p_ttir1_tcamp_clave,
        //             ttir1_tnive_clave = p_ttir1_tnive_clave,
        //             ttir1_tprog_clave = p_ttir1_tprog_clave,
        //             ttir1_tmate_clave = p_ttir1_tmate_clave,
        //             ttir1_user = p_ttir1_user
        //         };
        //         ModelInsTretiResponse response = DB.CallSPResult<ModelInsTretiResponse, ModelInsTreti>(Insert);
        //         return response;
        //     }

    }
}
