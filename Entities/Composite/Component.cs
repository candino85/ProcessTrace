using Abstracciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Composite
{
    public abstract class Component:IEntidades
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Parent { get; set; }
        public abstract IList<Component> GetChild { get; }
        public abstract void AddChild(Component c);
        public abstract void RemoveChild(Component c);
        public string Permission { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
