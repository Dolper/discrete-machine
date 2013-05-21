using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class ConditionalOperator
    {
        private readonly Connector connector;
        private readonly char @operator;
        private readonly int operand;

        public ConditionalOperator(Connector connector, char @operator, int operand)
        {
            this.connector = connector;
            this.@operator = @operator;
            this.operand = operand;
        }

        public bool Comparer()
        {
            switch(@operator)
            {
                case '<':
                if(connector.Value < operand)
                    return true;
                    break;
                case '>':
                if(connector.Value > operand)
                    return true;
                    break;
            }
            return false;
        }
    }

    public class Сyclogram
    {
        public int CurrentStep = 0;
        public IOperation[][] Operations { get; set; }

        //public void Add(ConditionalOperator @operator, int row)
        //{
        //    if (@operator.Comparer()) CurrentStep = row;
        //}
        
        public void Add(IOperation operation, int row)
        {
            Operations[row].ToList().Add(operation);
        }

        public void Remove(IOperation operation, int row)
        {
            Operations[row].ToList().Remove(operation);
        }

        public void NextStep()
        {
            foreach (var operation in Operations[CurrentStep])
            {
                operation.Execute();
            }
            CurrentStep++;
        }
    }
}
