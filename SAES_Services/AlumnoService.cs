using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelBanco;

namespace SAES_Services
{
    public class AlumnoService : Methods
    {
        public DataTable ObtenerAlumnos(string P_Matricula)
        {
            ModelObtenerAlumnosRequest request = new ModelObtenerAlumnosRequest() { Matricula = P_Matricula };
            List<ModelObtenerAlumnosResponse> response = DB.CallSPListResult<ModelObtenerAlumnosResponse, ModelObtenerAlumnosRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerTelefonos(string P_Matricula)
        {
            ModelObtenerTelefonosRequest request = new ModelObtenerTelefonosRequest() { Matricula = P_Matricula };
            List<ModelObtenerTelefonosResponse> response = DB.CallSPListResult<ModelObtenerTelefonosResponse, ModelObtenerTelefonosRequest>(request);
            return ToDataTable(response);
        }


        public List<ModelObtenerAlumnosResponse> ListaAlumnos(string P_Matricula)
        {
            ModelObtenerAlumnosRequest request = new ModelObtenerAlumnosRequest() { Matricula = P_Matricula };
            List<ModelObtenerAlumnosResponse> response = DB.CallSPListResult<ModelObtenerAlumnosResponse, ModelObtenerAlumnosRequest>(request);
            return response;
        }


        public ModelObtenerDatosAlumnoResponse ObtenerDatosAlumno(string p_matricula)
        {
            ModelObtenerDatosAlumnoRequest request = new ModelObtenerDatosAlumnoRequest() { Matricula = p_matricula };
            ModelObtenerDatosAlumnoResponse response = DB.CallSPResult<ModelObtenerDatosAlumnoResponse, ModelObtenerDatosAlumnoRequest>(request);
            return response;
        }

        public List<ModelObtenerProgsAlumnoResponse> ObtenerProgramaAlumno2(string p_clave)
        {
            ModelObtenerProgsAlumnoRequest request = new ModelObtenerProgsAlumnoRequest() { Clave = p_clave };
            List<ModelObtenerProgsAlumnoResponse> response = DB.CallSPListResult<ModelObtenerProgsAlumnoResponse, ModelObtenerProgsAlumnoRequest>(request);
            return response;
        }

        public DataTable ObtenerProgramaAlumno(string p_clave)
        {
            ModelObtenerProgsAlumnoRequest request = new ModelObtenerProgsAlumnoRequest() { Clave = p_clave };
            List<ModelObtenerProgsAlumnoResponse> response = DB.CallSPListResult<ModelObtenerProgsAlumnoResponse, ModelObtenerProgsAlumnoRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerBecasAlumno(string P_Matricula, string P_Programa)
        {
            ModelObtenerBecasAlumnoRequest request = new ModelObtenerBecasAlumnoRequest() { Matricula = P_Matricula, Programa= P_Programa };
            List<ModelObtenerBecasAlumnoResponse> response = DB.CallSPListResult<ModelObtenerBecasAlumnoResponse, ModelObtenerBecasAlumnoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerProgramaAlumnoMatricula(string p_clave)
        {
            ModelObtenerProgsAlumnoMatRequest request = new ModelObtenerProgsAlumnoMatRequest() { Clave = p_clave };
            List<ModelObtenerProgsAlumnoMatResponse> response = DB.CallSPListResult<ModelObtenerProgsAlumnoMatResponse, ModelObtenerProgsAlumnoMatRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerBecas(string p_matricula, string p_campus, string p_programa, string p_periodo)
        {
            ModelObtenerBecasRequest request = new ModelObtenerBecasRequest() { Matricula = p_matricula, Campus=p_campus, Programa=p_programa, Periodo=p_periodo };
            List<ModelObtenerBecasResponse> response = DB.CallSPListResult<ModelObtenerBecasResponse, ModelObtenerBecasRequest>(request);
            return ToDataTableForDropDownList(response);
        }
        public DataTable ObtenerSolicitudesIngreso(string p_matricula)
        {
            ModelObtenerSolicitudesIngresoRequest request = new ModelObtenerSolicitudesIngresoRequest() { Matricula = p_matricula };
            List<ModelObtenerSolicitudesIngresoResponse> response = DB.CallSPListResult<ModelObtenerSolicitudesIngresoResponse, ModelObtenerSolicitudesIngresoRequest>(request);
            return ToDataTable(response);
        }
        public DataTable ObtenerCorreos(string p_matricula)
        {
            ModelObtenerCorreosRequest request = new ModelObtenerCorreosRequest() { Matricula = p_matricula };
            List<ModelObtenerCorreosResponse> response = DB.CallSPListResult<ModelObtenerCorreosResponse, ModelObtenerCorreosRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerDocumentos(string p_matricula)
        {
            ModelObtenerDocumentosRequest request = new ModelObtenerDocumentosRequest() { Matricula = p_matricula };
            List<ModelObtenerDocumentosResponse> response = DB.CallSPListResult<ModelObtenerDocumentosResponse, ModelObtenerDocumentosRequest>(request);
            return ToDataTable(response);
        }

   

        public DataTable ObtenerDocumentosDisponibles(string p_matricula)
        {
            ModelObtenerDocumentosDisponiblesRequest request = new ModelObtenerDocumentosDisponiblesRequest() { Matricula = p_matricula };
            List<ModelObtenerDocumentosDisponiblesResponse> response = DB.CallSPListResult<ModelObtenerDocumentosDisponiblesResponse, ModelObtenerDocumentosDisponiblesRequest>(request);
            return ToDataTable(response);
        }


        public DataTable ObtenerDirecciones(string p_matricula)
        {
            ModelObtenDireccionesAluRequest request = new ModelObtenDireccionesAluRequest() { Matricula = p_matricula };
            List<ModelObtenDireccionesAluResponse> response = DB.CallSPListResult<ModelObtenDireccionesAluResponse, ModelObtenDireccionesAluRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerPeriodoAlumno(string p_clave)
        {
            ModelObtenerPeriodoAlumnoRequest request = new ModelObtenerPeriodoAlumnoRequest() { Clave = p_clave };
            List<ModelObtenerPeriodoAlumnoResponse> response = DB.CallSPListResult<ModelObtenerPeriodoAlumnoResponse, ModelObtenerPeriodoAlumnoRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public ModelInstbaapResponse InsertarBaja(string p_tbaap_tpers_num, string p_tbaap_tprog_clave, string p_tbaap_tpees_clave, string p_tbaap_ttiba_clave, string p_tbaap_fecha_baja
    , string tbaap_estima, string p_tbaap_date, string p_tbaap_user, string procesar_baja)
        {
            ModelInstbaap Insert = new ModelInstbaap()
            {
                tbaap_tpers_num = p_tbaap_tpers_num,
                tbaap_tprog_clave = p_tbaap_tprog_clave,
                tbaap_tpees_clave = p_tbaap_tpees_clave,
                tbaap_ttiba_clave = p_tbaap_ttiba_clave,
                tbaap_fecha_baja = p_tbaap_fecha_baja,
                tbaap_estima = tbaap_estima,
                tbaap_date = p_tbaap_date,
                tbaap_user = p_tbaap_user
            };
            ModelInstbaapResponse response = DB.CallSPResult<ModelInstbaapResponse, ModelInstbaap>(Insert);
            return response;
        }
        public string ModificarBaja(string p_tbaap_tpers_num, string p_tbaap_tprog_clave, string p_tbaap_tpees_clave, string p_tbaap_ttiba_clave, string p_tbaap_fecha_baja
    , string p_tbaap_estima, string p_tbaap_date, string p_tbaap_user)
        {
            ModelModificarBaja Update = new ModelModificarBaja()
            {
                tbaap_tpers_num = p_tbaap_tpers_num,
                tbaap_tprog_clave = p_tbaap_tprog_clave,
                tbaap_tpees_clave = p_tbaap_tpees_clave,
                tbaap_ttiba_clave = p_tbaap_ttiba_clave,
                tbaap_fecha_baja = p_tbaap_fecha_baja,
                tbaap_estima = p_tbaap_estima,
                tbaap_user= p_tbaap_user
            };
            return DB.CallSPForInsertUpdate(Update);
        }
        public DataTable ObtenerTestu(string p_testu_tpers_id)
        {
            ModelObtenerProgramaPeriodoAlumnoRequest request = new ModelObtenerProgramaPeriodoAlumnoRequest() { testu_tpers_id = p_testu_tpers_id };
            List<ModelObtenerProgramaPeriodoAlumnoResponse> response = DB.CallSPListResult<ModelObtenerProgramaPeriodoAlumnoResponse, ModelObtenerProgramaPeriodoAlumnoRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarTestuResponse InsertarTestu(string p_testu_tpers_num, string p_testu_tpees_clave, string p_testu_tcamp_clave, string p_testu_tprog_clave, string p_testu_ttasa_clave, 
            string p_testu_tstal_clave, string p_testu_periodo, string p_testu_tespe_clave, string p_testu_turno, string p_testu_user, string p_calculo_cuotas)
        {
            ModelInsertarTestu Insert = new ModelInsertarTestu()
            {
                testu_tpers_id = p_testu_tpers_num,
                testu_tpees_clave = p_testu_tpees_clave,
                testu_tcamp_clave = p_testu_tcamp_clave,
                testu_tprog_clave = p_testu_tprog_clave,
                testu_ttasa_clave = p_testu_ttasa_clave,
                testu_tstal_clave = p_testu_tstal_clave,
                testu_periodo = p_testu_periodo,
                testu_tespe_clave = p_testu_tespe_clave,
                testu_turno= p_testu_turno,
                testu_user= p_testu_user,
                calculo_cuotas = p_calculo_cuotas
            };
            //return DB.CallSPForInsertUpdate(Insert);
            ModelInsertarTestuResponse response = DB.CallSPResult<ModelInsertarTestuResponse, ModelInsertarTestu>(Insert);
            return response;
        }

        public ModelEditarTestuResponse EditarTestu(string p_testu_tpers_id, string p_testu_tpees_clave, string p_testu_tcamp_clave, string p_testu_tprog_clave, string p_testu_ttasa_clave,
            string p_testu_tstal_clave, int p_testu_periodo, string p_testu_tespe_clave, string p_testu_turno, string p_usuario, string p_calculo_cuotas)
        {
            ModelEditarTestu Editar = new ModelEditarTestu()
            {
                testu_tpers_id = p_testu_tpers_id,
                testu_tpees_clave = p_testu_tpees_clave,
                testu_tcamp_clave = p_testu_tcamp_clave,
                testu_tprog_clave = p_testu_tprog_clave,
                testu_ttasa_clave = p_testu_ttasa_clave,
                testu_tstal_clave = p_testu_tstal_clave,
                testu_periodo = p_testu_periodo,
                testu_tespe_clave = p_testu_tespe_clave,
                testu_turno = p_testu_turno,
                testu_user= p_usuario,
                calculo_cuotas = p_calculo_cuotas
            };

            ModelEditarTestuResponse response = DB.CallSPResult<ModelEditarTestuResponse, ModelEditarTestu>(Editar);
            return response;
            //return DB.CallSPForInsertUpdate(Editar);
        }

        public string EditarTcoda(string P_Matricula, string P_Tipo, string P_Consecutivo, string P_Nombre_Contacto, string P_Fecha_Nacimiento,
           string P_Rfc, string P_Usuario)
        {
            ModelEditarTcoda Editar = new ModelEditarTcoda()
            {
                Matricula = P_Matricula,
                Tipo = P_Tipo,
                Consecutivo = P_Consecutivo,
                Nombre_Contacto = P_Nombre_Contacto,
                Fecha_Nacimiento = P_Fecha_Nacimiento,
                Rfc = P_Rfc,
                Usuario = P_Usuario
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public string EditarTadmi(string P_Matricula, string p_tadmi_tpees_clave, string p_tadmi_consecutivo, string p_tadmi_turno, string p_tadmi_tprog_clave,
   string p_tadmi_ttiin_clave, string p_tadmi_tcamp_clave, string p_tadmi_tespr_clave, string p_tadmi_promedio,
   string p_tadmi_ttasa_clave, string p_tadmi_user)
        {
            ModelEditarTadmi Editar = new ModelEditarTadmi()
            {
                Matricula = P_Matricula,
                tadmi_tpees_clave = p_tadmi_tpees_clave,
                tadmi_consecutivo = p_tadmi_consecutivo,
                tadmi_turno = p_tadmi_turno,
                tadmi_tprog_clave = p_tadmi_tprog_clave,
                tadmi_ttiin_clave = p_tadmi_ttiin_clave,
                tadmi_tcamp_clave= p_tadmi_tcamp_clave,
                tadmi_tespr_clave= p_tadmi_tespr_clave,
                tadmi_promedio= p_tadmi_promedio,
                tadmi_ttasa_clave= p_tadmi_ttasa_clave,
                tadmi_user= p_tadmi_user
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public string EditarDireccion(string P_Matricula, string p_taldi_tdire_clave, string p_taldi_calle, string p_taldi_colonia, string p_taldi_testa_clave,
string p_taldi_tdele_clave, string p_taldi_tpais_clave, string p_taldi_tcopo_clave, string p_taldi_ciudad,
string p_taldi_estatus, string p_taldi_user, string p_taldi_consec)
        {
            ModelUpdTaldi Editar = new ModelUpdTaldi()
            {
                matricula = P_Matricula,
                taldi_tdire_clave = p_taldi_tdire_clave,
                taldi_calle = p_taldi_calle,
                taldi_colonia = p_taldi_colonia,
                taldi_testa_clave = p_taldi_testa_clave,
                taldi_tdele_clave = p_taldi_tdele_clave,
                taldi_tpais_clave = p_taldi_tpais_clave,
                taldi_tcopo_clave = p_taldi_tcopo_clave,
                taldi_ciudad = p_taldi_ciudad,
                taldi_estatus = p_taldi_estatus,
                taldi_user = p_taldi_user,
                taldi_consec= p_taldi_consec
            };
            return DB.CallSPForInsertUpdate(Editar);
        }


        public string CalculaCuotas(string p_testu_tpees_clave, string p_testu_tpers_id,
            string p_testu_ttasa_clave, string p_testu_tcamp_clave,
             string p_testu_tprog_clave, string p_usuario)
        {
            ModelCalculaCuotas calcular = new ModelCalculaCuotas()
            {
                testu_tpees_clave = p_testu_tpees_clave,
                testu_tpers_id = p_testu_tpers_id,
                testu_ttasa_clave = p_testu_ttasa_clave,
                testu_tcamp_clave = p_testu_tcamp_clave,
                testu_tprog_clave = p_testu_tprog_clave,
                testu_user = p_usuario
            };

            return DB.CallSPForInsertUpdate(calcular);
        }

        public string GeneraCuotas(string p_testu_tpees_clave, string p_testu_tpers_id,
         string p_testu_tcamp_clave,
           string p_testu_tprog_clave, string p_usuario)
        {
            ModelGeneraCuotas generar = new ModelGeneraCuotas()
            {
                testu_tpees_clave = p_testu_tpees_clave,
                testu_tpers_id = p_testu_tpers_id,
                testu_tcamp_clave = p_testu_tcamp_clave,
                testu_tprog_clave = p_testu_tprog_clave,
                testu_user = p_usuario
            };

            return DB.CallSPForInsertUpdate(generar);
        }


        public List<ModelObtenerProgActAlumnoResponse> ObtenerProgActAlumno(string Matricula)
        {
            ModelObtenerProgActAlumnoRequest request = new ModelObtenerProgActAlumnoRequest() { P_Matricula = Matricula };
            List<ModelObtenerProgActAlumnoResponse> response = DB.CallSPListResult<ModelObtenerProgActAlumnoResponse, ModelObtenerProgActAlumnoRequest>(request);
            return response;
        }



        public string ModificarDatosPersonales(string p_tpers_nombre, string p_tpers_paterno, string p_tpers_materno, 
            string p_tpers_genero, string p_tpers_fecha_nac
, string p_tpers_edo_civ, string p_tpers_curp, string p_tpers_usuario, string p_tpers_id)
        {
            ModelModificarTpers Update = new ModelModificarTpers()
            {
                tpers_nombre = p_tpers_nombre,
                tpers_paterno = p_tpers_paterno,
                tpers_materno = p_tpers_materno,
                tpers_genero = p_tpers_genero,
                tpers_fecha_nac = p_tpers_fecha_nac,
                tpers_edo_civ = p_tpers_edo_civ,
                tpers_curp = p_tpers_curp,
                tpers_usuario=p_tpers_usuario,
                tpers_id=p_tpers_id
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public string InsertarTelefono(string p_matricula, string p_talte_ttele_clave, 
            string p_talte_lada, 
            string p_talte_tel, string p_talte_ext, string p_talte_estatus, string p_talte_user)
        {
            ModelInsTalte Insert = new ModelInsTalte()
            {
                matricula = p_matricula,
                talte_ttele_clave = p_talte_ttele_clave,
                talte_lada = p_talte_lada,
                talte_tel = p_talte_tel,
                talte_ext = p_talte_ext,
                talte_estatus = p_talte_estatus,
                talte_user = p_talte_user
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public string EditarTelefono(string p_matricula, string p_talte_consec, string p_talte_ttele_clave,
    string p_talte_lada,
    string p_talte_tel, string p_talte_ext, string p_talte_estatus, string p_talte_user)
        {
            ModelEditarTalte Update = new ModelEditarTalte()
            {
                matricula = p_matricula,
                talte_consec = p_talte_consec,
                talte_ttele_clave = p_talte_ttele_clave,
                talte_lada = p_talte_lada,
                talte_tel = p_talte_tel,
                talte_ext = p_talte_ext,
                talte_estatus = p_talte_estatus,
                talte_user = p_talte_user
            };
            return DB.CallSPForInsertUpdate(Update);
        }


        public string InsertarCorreo(string p_matricula, string p_talco_tmail_clave, string p_talco_correo, 
            string p_talco_preferido, string p_talco_estatus, string p_talco_user)
        {
            ModelInstalco Insert = new ModelInstalco()
            {
                matricula = p_matricula,
                talco_tmail_clave = p_talco_tmail_clave,
                talco_correo = p_talco_correo,
                talco_preferido = p_talco_preferido,
                talco_estatus = p_talco_estatus,
                talco_user = p_talco_user
            };

            return DB.CallSPForInsertUpdate(Insert);
        }

        public string EditarCorreo(string p_matricula, string p_talco_tmail_clave, string p_talco_correo,
           string p_talco_preferido, string p_talco_estatus, string p_talco_user)
        {
            ModelUpdtalco Update = new ModelUpdtalco()
            {
                matricula = p_matricula,
                talco_tmail_clave = p_talco_tmail_clave,
                talco_correo = p_talco_correo,
                talco_preferido = p_talco_preferido,
                talco_estatus = p_talco_estatus,
                talco_user = p_talco_user
            };

            return DB.CallSPForInsertUpdate(Update);

        }

        public string EditarCorreoContacto(string p_matricula, string p_tcnco_tcoda_consec, string p_tcnco_consec,
        string p_tcnco_tmail_clave, string p_tcnco_correo, string p_tcnco_preferido, string p_tcnco_estatus, string p_tcnco_user)
        {
            ModelUpdtcnco Update = new ModelUpdtcnco()
            {
                matricula = p_matricula,
                tcnco_tcoda_consec = p_tcnco_tcoda_consec,
                tcnco_consec = p_tcnco_consec,
                tcnco_tmail_clave = p_tcnco_tmail_clave,
                tcnco_correo = p_tcnco_correo,
                tcnco_preferido = p_tcnco_preferido,
                tcnco_estatus= p_tcnco_estatus,
                tcnco_user= p_tcnco_user
            };

            return DB.CallSPForInsertUpdate(Update);

        }

        public DataTable ObtenerContactosGrid(string P_Matricula)
        {
            ModelObtenerContactosRequest request = new ModelObtenerContactosRequest() { Matricula = P_Matricula };
            List<ModelObtenerContactosResponse> response = DB.CallSPListResult<ModelObtenerContactosResponse, ModelObtenerContactosRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerContactos(string P_Matricula)
        {
            ModelObtenerContactosRequest request = new ModelObtenerContactosRequest() { Matricula = P_Matricula };
            List<ModelObtenerContactosResponse> response = DB.CallSPListResult<ModelObtenerContactosResponse, ModelObtenerContactosRequest>(request);
            return ToDataTableForDropDownList(response);
        }


        public string InsertarContacto(string p_matricula, string p_tcoda_tipo, string p_tcoda_nombre, 
            string p_tcoda_fecha_nac, string p_tcoda_rfc, string p_tcoda_usuario)
        {
            ModelInsTcoda Insert = new ModelInsTcoda()
            {
                matricula = p_matricula,
                tcoda_tipo = p_tcoda_tipo,
                tcoda_nombre = p_tcoda_nombre,
                tcoda_fecha_nac = p_tcoda_fecha_nac,
                tcoda_rfc = p_tcoda_rfc,
                tcoda_usuario = p_tcoda_usuario
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public string InsertarDireccion(string p_matricula, string p_taldi_tdire_clave, string p_taldi_calle,
        string p_taldi_colonia, string p_taldi_testa_clave, string p_taldi_tdele_clave, string p_taldi_tpais_clave,
        string p_taldi_tcopo_clave, string p_taldi_ciudad, string p_taldi_estatus, string p_taldi_user)
        {
            ModelInsTaldi Insert = new ModelInsTaldi()
            {
                matricula = p_matricula,
                taldi_tdire_clave= p_taldi_tdire_clave,
                taldi_calle = p_taldi_calle,
                taldi_colonia = p_taldi_colonia,
                taldi_testa_clave = p_taldi_testa_clave,
                taldi_tdele_clave = p_taldi_tdele_clave,
                taldi_tpais_clave= p_taldi_tpais_clave,
                taldi_tcopo_clave= p_taldi_tcopo_clave,
                taldi_ciudad= p_taldi_ciudad,
                taldi_estatus= p_taldi_estatus,
                taldi_user= p_taldi_user
            };
            return DB.CallSPForInsertUpdate(Insert);
        }


        public ModelInsertarTelContactoResponse InsertarTelContacto(string p_matricula, string p_tcote_tcoda_consec, 
            string p_tcote_ttele_clave, string p_tcote_lada, string p_tcote_tel, string p_tcote_ext,
            string p_tcote_estatus, string p_tcote_user
            )
        {
            ModelInsertarTelContacto Insert = new ModelInsertarTelContacto()
            {
                matricula = p_matricula,
                tcote_tcoda_consec = p_tcote_tcoda_consec,
                tcote_ttele_clave = p_tcote_ttele_clave,
                tcote_lada = p_tcote_lada,
                tcote_tel = p_tcote_tel,
                tcote_ext = p_tcote_ext,
                tcote_estatus = p_tcote_estatus,
                tcote_user = p_tcote_user
            };
            //return DB.CallSPForInsertUpdate(Insert);
            ModelInsertarTelContactoResponse response = DB.CallSPResult<ModelInsertarTelContactoResponse, ModelInsertarTelContacto>(Insert);
            return response;
        }

        public string EditarTelContacto(string p_matricula, string p_tcote_tcoda_consec,
    string p_tcote_ttele_clave, string p_tcote_lada, string p_tcote_tel, string p_tcote_ext,
    string p_tcote_estatus, string p_tcote_user, string p_tcote_consec
    )
        {
            ModelEditarTelContacto Insert = new ModelEditarTelContacto()
            {
                matricula = p_matricula,
                tcote_tcoda_consec = p_tcote_tcoda_consec,
                tcote_ttele_clave = p_tcote_ttele_clave,
                tcote_lada = p_tcote_lada,
                tcote_tel = p_tcote_tel,
                tcote_ext = p_tcote_ext,
                tcote_estatus = p_tcote_estatus,
                tcote_user = p_tcote_user,
                tcote_consec= p_tcote_consec
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public DataTable ObtenerTelContactos(string P_Matricula)
        {
            ModelObtenerTelContactosRequest request = new ModelObtenerTelContactosRequest() { Matricula = P_Matricula };
            List<ModelObtenerTelContactosResponse> response = DB.CallSPListResult<ModelObtenerTelContactosResponse, ModelObtenerTelContactosRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerEMailContactos(string P_Matricula)
        {
            ModelObtenerEmailContactosRequest request = new ModelObtenerEmailContactosRequest() { Matricula = P_Matricula };
            List<ModelObtenerEmailContactosResponse> response = DB.CallSPListResult<ModelObtenerEmailContactosResponse, ModelObtenerEmailContactosRequest>(request);
            return ToDataTable(response);
        }

    }
}
