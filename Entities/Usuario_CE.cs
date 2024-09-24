using Abstracciones;
using System;

namespace Entidades
{
    public class Usuario_CE : IEntidades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }                
        public bool Activo { get; set; }        
        public int IntentosAcceso { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime UltimoAcceso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string DVH { get; set; }        
    }
}
