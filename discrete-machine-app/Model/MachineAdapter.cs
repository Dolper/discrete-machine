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
                _machine.Elements.Add(elementProxy.Element);
            Elements.Add(elementProxy);
        }

        public ObservableCollection<WireProxy> Wires { get; set; }
        internal void AddWire(WireProxy wire)
        {
            _machine.Wires.Add(wire.Wire);
            Wires.Add(wire);
        }
        internal void RemoveWire(WireProxy wire)
        {
            Wires.Remove(wire);
            _machine.Wires.Remove(wire.Wire);
        }

        internal void RemoveElement(ElementProxy elementProxy)
        {
            var element = elementProxy.Element;
            var wiresToDelete = Wires.Where(x =>
                    element.Input.Contains(x.Wire.In) ||
                    element.Input.Contains(x.Wire.Out) ||
                    element.Output.Contains(x.Wire.In) ||
                    element.Output.Contains(x.Wire.Out)
                ).ToList();

            // TODO: replace with smth'n like removeAll
            foreach (var wire in wiresToDelete)
                RemoveWire(wire);

            Elements.Remove(elementProxy);
            _machine.Elements.Remove(element);
        }
    }
}
