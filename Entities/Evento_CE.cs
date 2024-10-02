using Abstracciones;
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
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public DateTime FechaHora { get; set; }
        public string Modulo { get; set; }
        public string Operacion { get; set; }
        public int Criticidad { get; set; }
        public string Mensaje { get; set; }
        public string DVH { get; set; }
    }
    public class Filtro
    {
        public int IdUsuario { get; set; }
        public DateTime? FechaHoraDesde { get; set; }
        public DateTime? FechaHoraHasta { get; set; }
        public string Modulo { get; set; }
        public string Operacion { get; set; }
        public Dictionary<string,bool> Criticidad { get; set; }
        public string Mensaje { get; set; }
    }
}
