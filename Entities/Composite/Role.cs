using Abstracciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Composite
{
    public class Role : Component
    {
        private readonly IList<Component> _childList;

        public Role()
        {
            _childList = new List<Component>();
        }

        public override IList<Component> GetChild
        {
            get { return _childList; }
        }

        public override void AddChild(Component comp)
        {
            if (!_childList.Contains(comp))
                _childList.Add(comp);
        }

        public override void RemoveChild(Component comp)
        {
            if (_childList.Contains(comp))
                _childList.Remove(comp);
        }
    }
}
