using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_ASA_ENTIDADESv1
{
    public class Alumno
    {
        public Persona alumno { get; set; }
        public Persona madre { get; set; }
        public Persona padre { get; set; }
        public Boolean conoceMusica { get; set; }
        public NivelEstudio nivelEstudio { get; set; }
        public int legajo { get; set; }

        public Alumno() { }

        public Alumno(Persona alumno, Persona madre, Persona padre, Boolean conoceMusica, NivelEstudio nivelEstudio)
        {
            this.alumno = alumno;
            this.madre = madre;
            this.padre = padre;
            this.conoceMusica = conoceMusica;
            this.nivelEstudio = nivelEstudio;
        }

      
    }
}
