﻿using Abstracciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Evento_CE : IEntidades
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int Criticidad { get; set; }
        public string DVH { get; set;}
    }
}
