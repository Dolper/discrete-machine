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
                //Operations = new IOperation[0][],
            };
            Elements = new List<IElement>();
            Wires = new List<Wire>();
        }

        public ICollection<IElement> Elements { get; set; }
        public ICollection<Wire> Wires { get; set; }

        public Сyclogram Cyclogram { get; set; }
    }
}
