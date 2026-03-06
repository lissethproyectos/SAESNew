using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApisSysweb.Model
{
    public class Grafica
    {
        public string Dato1 { get; set; }
        public string Dato2 { get; set; }
        public string Dato3 { get; set; }
        public string Dato4 { get; set; }
        public string Dato5 { get; set; }
        public string Dato6 { get; set; }

    }
    public class Resultado_Grafica
    {
        public bool Error { get; set; }
        public string Mensaje_Error { get; set; }
        public List<Grafica>Resultado { get; set; }

    }
}

