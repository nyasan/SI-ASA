using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_ASA_ENTIDADESv1
{
    public class Persona
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int numDoc { get; set; }
        public TipoDocumento tipoDoc { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string mail { get; set; }
        public DateTime fechaNacimiento { get; set; }
    }


}
