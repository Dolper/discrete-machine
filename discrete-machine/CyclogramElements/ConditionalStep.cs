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
        public static string[] ConditionsSymbols = 
            { ">", "<", "=", "<>", "<=", "=>" };
        public static string Symbol(StepCondition condition)
        {
            return ConditionsSymbols[(int)condition];
        }


        private readonly Connector connector;
        private readonly StepCondition @operator;
        private readonly int operand;
        public CyclogramStep transition { get; set; }

        public ConditionalStep(Connector connector, StepCondition @operator, int operand)
        {
            this.connector = connector;
            this.@operator = @operator;
            this.operand = operand;
        }

        public bool Comparer()
        {
            switch (@operator)
            {
                case StepCondition.Less:
                    if (connector.Value < operand)
                        return true;
                    break;
                case StepCondition.More:
                    if (connector.Value > operand)
                        return true;
                    break;
                case StepCondition.Equals:
                    if (connector.Value == operand)
                        return true;
                    break;
                case StepCondition.NotEquals:
                    if (connector.Value != operand)
                        return true;
                    break;
                case StepCondition.LessEquals:
                    if (connector.Value <= operand)
                        return true;
                    break;
                case StepCondition.MoreEquals:
                    if (connector.Value >= operand)
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
