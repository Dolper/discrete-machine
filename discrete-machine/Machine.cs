using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class Machine
    {
        public Machine()
        {
            Cyclogram = new Сyclogram()
            {
                Operations = new IOperation[0][],
            };
            Elements = new IElement[0];
            Wires = new Wire[0];
        }

        public IElement[] Elements { get; set; }
        public Wire[] Wires { get; set; }

        public Сyclogram Cyclogram { get; set; }

        public void AddElement(IElement el)
        {
            Elements = Elements.Concat(new IElement[] { el }).ToArray();
        }
        public void AddWire(Wire wire)
        {
            Wires = Wires.Concat(new Wire[] { wire }).ToArray();
        }
    }
}
