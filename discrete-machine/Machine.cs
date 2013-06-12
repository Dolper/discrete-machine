using discrete_machine.Abstract;
using discrete_machine.CyclogramElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class Machine
    {
        public Machine()
        {
            Cyclogram = new Cyclogram();
            Elements = new ObservableCollection<IElement>();
            Wires = new ObservableCollection<Wire>();
        }

        public ObservableCollection<IElement> Elements { get; set; }
        public ObservableCollection<Wire> Wires { get; set; }

        public Cyclogram Cyclogram { get; set; }


        public void RemoveElement(IElement element)
        {
            var connectors = element.Input.Concat(element.Output);
            var wiresToDelete = Wires.Where(x =>
                    connectors.Contains(x.In) ||
                    connectors.Contains(x.Out)
                ).ToList();

            foreach (var wire in wiresToDelete)
                RemoveWire(wire);

            foreach (var item in Cyclogram.Steps)
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

            Elements.Remove(element);
        }

        public void RemoveWire(Wire wire)
        {
            foreach (var item in Cyclogram.Steps)
            {
                var step = item as OperationsStep;
                if (step != null) step.RemoveOperation(wire);
            }

            Wires.Remove(wire);
        }
    }
}
