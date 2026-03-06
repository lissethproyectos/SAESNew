
using ApisSysweb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApisSysweb.Interface
{
    public interface ICoinService
    {
        List<Grafica>ListPagosBanco(ref string Verificador);
        List<Grafica> ListPagosporEjercicio(ref string Verificador);
    }
}
