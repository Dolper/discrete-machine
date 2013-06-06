using discrete_machine;
using discrete_machine.Abstract;
using discrete_machine.CyclogramElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            foreach (var el in _machine.Wires)
                Wires.Add(new WireProxy(el));

            Steps = new ObservableCollection<CyclogramStep>();
            Steps.CollectionChanged += stepsCollectionChanged;
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
            var connectors = element.Input.Concat(element.Output);
            var wiresToDelete = Wires.Where(x =>
                    connectors.Contains(x.Wire.In) ||
                    connectors.Contains(x.Wire.Out)
                ).ToList();

            // TODO: replace with smth'n like removeAll
            foreach (var wire in wiresToDelete)
                RemoveWire(wire);

            foreach (var item in _machine.Cyclogram.Steps)
            {
                if (item is OperationsStep)
                    foreach (var operation in element.Operations)
                        (item as OperationsStep).RemoveOperation(operation);
                var conditionsToDelete = new List<ConditionalStep>();
                if (item is ConditionalStep)
                    foreach (var connector in connectors)
                        if (connectors.Contains((item as ConditionalStep).Connector))
                            conditionsToDelete.Add(item as ConditionalStep);
            }

            Elements.Remove(elementProxy);
            _machine.Elements.Remove(element);
        }

        internal ObservableCollection<CyclogramStep> Steps { get; set; }
        internal int NextStep { get { return _machine.Cyclogram.NextStep; } }
        internal void AddStep()
        {
            Steps.Add(new OperationsStep());
        }
        internal void AddCondition(IConnector connector, StepCondition condition, int operand)
        {
            Steps.Add(new ConditionalStep(connector, condition, operand));
        }
        private void stepsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                _machine.Cyclogram.Steps.AddRange(e.NewItems);
            if (e.OldItems != null)
                foreach (var el in e.OldItems)
                    _machine.Cyclogram.Steps.Remove(el);
        }

        public Cyclogram Cyclogram
        {
            get
            {
                return _machine.Cyclogram;
            }
        }
    }
}
