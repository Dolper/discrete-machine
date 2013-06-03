using discrete_machine.Abstract;
using System;
using System.Collections.Generic;

namespace discrete_machine.CyclogramElements
{
    class OperationsStep: CyclogramStep
    {
        private List<IOperation> stepOperations = new List<IOperation>();

        public List<Exception> Execute()
        {
            var errorList = new List<Exception>();
            foreach (var operation in stepOperations)
            {
                var error = operation.Execute();
                if (error != null) errorList.Add(error);
            }
            return errorList;
        }

        public void AddOperation(IOperation operation)
        {
            stepOperations.Add(operation);
        }
        public void RemoveOperation(IOperation operation)
        {
            stepOperations.Remove(operation);
        }
        public bool Contains(IOperation operation)
        {
            return stepOperations.Contains(operation);
        }
    }
}
