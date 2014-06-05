using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_ASA_ENTIDADESv1
{
    public class TipoDocumento
    {
        public string descripcion { get; set; }

        public TipoDocumento() { }

        public TipoDocumento(string descripcion)
        {
            this.descripcion = descripcion;
        }

    }

    
}
