using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace discrete_machine.CyclogramElements
{
    public interface CyclogramStep
    {
        List<Exception> Execute();
    }
}
