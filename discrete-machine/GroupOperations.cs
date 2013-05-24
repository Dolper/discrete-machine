using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine
{
    public class GroupOperations
    {
        private IOperation[][] Operations { get; set; }
        public ConditionalOperator[] ConditionalOperators { get; set; }

        public GroupOperations()
        { 

        }
    }
}
