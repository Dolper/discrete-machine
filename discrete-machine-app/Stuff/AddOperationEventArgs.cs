using discrete_machine.Abstract;
using System;

namespace discrete_machine_app.Stuff
{
    public class AddOperationEventArgs : EventArgs
    {
        public AddOperationEventArgs(IOperation operation)
            : base()
        {
            Operation = operation;
        }

        public IOperation Operation;
    }
}
