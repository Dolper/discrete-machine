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
        private int currentStep = 0;

        public void NextStep()
        {
            currentStep++;
        }
    }
}
