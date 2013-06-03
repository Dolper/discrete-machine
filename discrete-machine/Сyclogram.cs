using discrete_machine.Abstract;
using discrete_machine.CyclogramElements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class Сyclogram
    {
        public ArrayList Steps { get; set; }
        public int NextStep { get; set; }

        public Сyclogram()
        {
            Steps = new ArrayList();
            NextStep = 0;
        }

        public void Execute()
        {
            if (Steps[NextStep] is OperationsStep)
            {
                var step = Steps[NextStep] as OperationsStep;
                step.Execute();
            }
            else if (Steps[NextStep] is ConditionalStep)
            {
                var step = Steps[NextStep] as ConditionalStep;
                if (step.Comparer())
                {
                    var nextStep = step.transition;
                    NextStep = Steps.IndexOf(nextStep);
                }
            }
            NextStep++;
        }

        public void AddStep(CyclogramStep newStep)
        {
            Steps.Add(newStep);
        }
        public void RemoveStep(CyclogramStep step)
        {
            Steps.Remove(step);
        }
        public void ReorderSteps(int oldStepNum, int newStepNum)
        {
            var elem = Steps[oldStepNum];
            Steps.RemoveAt(oldStepNum);
            Steps.Insert(newStepNum, elem);
        }
    }
}
