using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_ASA_ENTIDADESv1
{
    public class Docente
    {
        public Persona docente { get; set; }
        public int legajo { get; set; }
        public Horario horarioTrabajo { get; set; }
        public float salario { get; set; }
    }
}
