using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace discrete_machine
{
    public class ConditionalOperator
    {
        private readonly Connector connector;
        private readonly string @operator;
        private readonly int operand;
        public readonly int transition;

        public ConditionalOperator(Connector connector, string @operator, int operand, int transition)
        {
            this.connector = connector;
            this.@operator = @operator;
            this.operand = operand;
            this.transition = transition;
        }

        public bool Comparer()
        {
            switch (@operator)
            {
                case "<":
                    if (connector.Value < operand)
                        return true;
                    break;
                case ">":
                    if (connector.Value > operand)
                        return true;
                    break;
                case "=":
                    if (connector.Value == operand)
                        return true;
                    break;
                case "<>":
                    if (connector.Value != operand)
                        return true;
                    break;
                case "<=":
                    if (connector.Value <= operand)
                        return true;
                    break;
                case ">=":
                    if (connector.Value >= operand)
                        return true;
                    break;
            }
            return false;
        }
    }
}
