using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_ASA_ENTIDADESv1
{
    public class NivelEstudio
    {
        public string descripcion { get; set; }
        public int id { get; set; }
        public NivelEstudio(string descripcion)
        {
            this.descripcion = descripcion;
        }

        public NivelEstudio() { }
    }


}
