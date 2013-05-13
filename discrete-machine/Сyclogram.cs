using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class Сyclogram
    {
        public IEnumerable<IOperation>[] Operations { get; set; }

        public void Step()
        {
            throw new NotImplementedException();
        }
    }
}
