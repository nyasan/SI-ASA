﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_ASA_ENTIDADESv1
{
    public class Curso
    {
        public int id_curso { get; set; }
        public String nombre { get; set; }
        public Horario horario { get; set; }
        //public String hora_desde { get; set; }
        //public String hora_hasta { get; set; }
        public String descripcion { get; set; }

        public Curso()
        {
        }

    }
}
