using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_ASA_ENTIDADESv1
{
    public class AlumnoQuery
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaInsc { get; set; }
        public string Curso { get; set; }
        public DateTime FechaFalta { get; set; }
        public int Legajo { get; set; }
    }
}
