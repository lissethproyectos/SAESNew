using System.ComponentModel.DataAnnotations;

namespace SAES_DBO.Models
{
    [SPName("P_OBTEN_TTIAC")]
    public class ModelTipoAcreditacionRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_NADA", 0)]
        public string Nada { get; set; }
    }
    public class ModelTipoAcreditacionResponse : BaseModelResponse
    {
        [SPResponseColumnName("ttiac_clave")]
        public string Clave { get; set; }
        [SPResponseColumnName("ttiac_desc")]
        public string Descripcion { get; set; }
        [SPResponseColumnName("ttiac_user")]
        public string Usuario { get; set; }
        [SPResponseColumnName("ttiac_clave_cert")]
        public string ClaveCert { get; set; }
        [SPResponseColumnName("ttiac_siglas")]
        public string SiglasCert { get; set; }
        [SPResponseColumnName("ttiac_date")]
        public string FechaRegistro { get; set; }
        [SPResponseColumnName("ttiac_estatus")]
        public string Estatus { get; set; }
    }

    [SPName("P_INSERTA_TTIAC")]
    public class ModeltTipoAcreditacionForInsertRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CLAVE", 0)]
        public string Clave { get; set; }
        [SPParameterName("P_DESC", 1)]
        public string Descripcion { get; set; }
        [SPParameterName("P_USER", 2)]
        public string Usuario { get; set; }
        [SPParameterName("P_CLAVE_CERT", 3)]
        public string ClaveCert { get; set; }
        [SPParameterName("P_SIGLAS", 4)]
        public string Siglas { get; set; }
        [SPParameterName("P_ESTATUS", 5)]
        public string Estatus { get; set; }
    }

    [SPName("P_ACTU_TTIAC")]
    public class ModeltTipoAcreditacionForUpdateRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CLAVE_OLD", 0)]
        public string ClaveOld { get; set; }
        [Required]
        [SPParameterName("P_CLAVE", 1)]
        public string Clave { get; set; }
        [SPParameterName("P_DESC", 2)]
        public string Descripcion { get; set; }
        [SPParameterName("P_USER", 3)]
        public string Usuario { get; set; }
        [SPParameterName("P_CLAVE_CERT", 4)]
        public string ClaveCert { get; set; }
        [SPParameterName("P_SIGLAS", 5)]
        public string Siglas { get; set; }
        [SPParameterName("P_ESTATUS", 6)]
        public string Estatus { get; set; }
    }
}
