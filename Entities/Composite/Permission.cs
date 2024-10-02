using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Composite
{
    public class Permission : Component
    {
        public Permission()
        {

        }
        public override void AddChild(Component c)
        {
            throw new NotImplementedException();
        }
        public override void RemoveChild(Component c)
        {
            throw new NotImplementedException();
        }
        public override IList<Component> GetChild
        {
            get { return new List<Component>(); }
        }
    }
}
