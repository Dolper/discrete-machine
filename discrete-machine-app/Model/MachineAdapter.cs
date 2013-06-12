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
            Elements = new ObservableCollection<ElementProxy>();
            Wires = new ObservableCollection<WireProxy>();
            _machine = machine;

            foreach (var el in _machine.Elements)
                Elements.Add(new ElementProxy(el));
            foreach (var el in _machine.Wires)
                Wires.Add(new WireProxy(el));

            _machine.Elements.CollectionChanged += Elements_CollectionChanged;
            _machine.Wires.CollectionChanged += Wires_CollectionChanged;
        }

        #region Elements

        public ObservableCollection<ElementProxy> Elements { get; set; }
        public void AddElement(ElementProxy elementProxy)
        {
            _machine.Elements.Add(elementProxy.Element);
            Elements.Add(elementProxy);
        }
        public void RemoveElement(ElementProxy elementProxy)
        {
            _machine.RemoveElement(elementProxy.Element);
        }
        void Elements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (IElement el in e.OldItems)
                    removeElementProxy(el);
        }
        void removeElementProxy(IElement element)
        {
            var target = Elements.FirstOrDefault(x => x.Element == element);
            if (target != null) Elements.Remove(target);
        }

        #endregion

        #region Wires

        public ObservableCollection<WireProxy> Wires { get; set; }
        public void AddWire(WireProxy wire)
        {
            _machine.Wires.Add(wire.Wire);
            Wires.Add(wire);
        }
        public void RemoveWire(WireProxy wire)
        {
            _machine.RemoveWire(wire.Wire);
        }
        void Wires_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (Wire wire in e.OldItems)
                    removeWireProxy(wire);
        }
        void removeWireProxy(Wire wire)
        {
            var target = Wires.FirstOrDefault(x => x.Wire == wire);
            if (target != null) Wires.Remove(target);
        }

        #endregion

        #region Cyclogram stuff

        public int NextStep { get { return _machine.Cyclogram.NextStep; } }
        public void AddEmptyOperationsStep()
        {
            _machine.Cyclogram.Steps.Add(new OperationsStep());
        }
        public void AddCondition(IConnector connector, StepCondition condition, int operand)
        {
            _machine.Cyclogram.Steps.Add(new ConditionalStep(connector, condition, operand));
        }
        public ObservableCollection<CyclogramStep> Steps
        {
            get { return _machine.Cyclogram.Steps; }
        }
        public Cyclogram Cyclogram
        {
            get { return _machine.Cyclogram; }
        }
        
        #endregion
    }
}
