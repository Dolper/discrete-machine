using discrete_machine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace discrete_machine_app.Model
{
    public class MachineAdapter
    {
        private readonly Machine _machine;
        public MachineAdapter() : this(new Machine()) { }
        public MachineAdapter(Machine machine)
        {
            Elements = new List<ElementProxy>();
            Wires = new ObservableCollection<WireProxy>();
            _machine = machine;

            foreach (var el in _machine.Elements)
                Elements.Add(new ElementProxy(el));
        }

        public List<ElementProxy> Elements { get; set; }

        internal void AddElement(ElementProxy elementProxy)
        {
            if (!_machine.Elements.Contains(elementProxy.Element))
                _machine.AddElement(elementProxy.Element);
            Elements.Add(elementProxy);
        }

        public ObservableCollection<WireProxy> Wires { get; set; }
        internal void AddWire(WireProxy wire)
        {
            _machine.AddWire(wire.Wire);
            Wires.Add(wire);
        }
    }
}
