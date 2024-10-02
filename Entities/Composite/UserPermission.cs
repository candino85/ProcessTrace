using Abstracciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Composite
{
    public class UserPermission : IEntidades
    {
        List<Component> _permissions;

        public int Id { get; set; }
        public string Nombre { get; set; }

        public UserPermission()
        {
            _permissions = new List<Component>();
        }

        public List<Component> Permissions
        {
            get { return _permissions; }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
