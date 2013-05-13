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
        public IOperation[][] Operations { get; set; }

        //public void Add()
        //{ 
        
        //}

        public void Step()
        {
            Operations[0][0].Execute();
        }
    }
}
