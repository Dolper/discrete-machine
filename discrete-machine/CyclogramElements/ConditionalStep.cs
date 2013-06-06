using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace discrete_machine.CyclogramElements
{
    public enum StepCondition
    {
        More,
        Less,
        Equals,
        NotEquals,
        LessEquals,
        MoreEquals
    }

    public class ConditionalStep : CyclogramStep
    {
        private readonly StepCondition @operator;
        private readonly int operand;
        public CyclogramStep transition { get; set; }
        public IConnector Connector { get; set; }

        public ConditionalStep(IConnector connector, StepCondition @operator, int operand)
        {
            this.Connector = connector;
            this.@operator = @operator;
            this.operand = operand;
        }

        public bool Comparer()
        {
            switch (@operator)
            {
                case StepCondition.Less:
                    if (Connector.Value < operand)
                        return true;
                    break;
                case StepCondition.More:
                    if (Connector.Value > operand)
                        return true;
                    break;
                case StepCondition.Equals:
                    if (Connector.Value == operand)
                        return true;
                    break;
                case StepCondition.NotEquals:
                    if (Connector.Value != operand)
                        return true;
                    break;
                case StepCondition.LessEquals:
                    if (Connector.Value <= operand)
                        return true;
                    break;
                case StepCondition.MoreEquals:
                    if (Connector.Value >= operand)
                        return true;
                    break;
            }
            return false;
        }

        public List<Exception> Execute()
        {
            return new List<Exception>();
        }
    }
}
