using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace discrete_machine.CyclogramElements
{
    public class OperationsStep: CyclogramStep
    {
        private ObservableCollection<IOperation> stepOperations = new ObservableCollection<IOperation>();

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { stepOperations.CollectionChanged += value; }
            remove { stepOperations.CollectionChanged -= value; }
        }

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
