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
        public Element[] Elements { get; set; }
        public Wire[] Wires { get; set; }

        public Сyclogram Cyclogram { get; set; }
    }
}
