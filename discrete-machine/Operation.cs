using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class Operation<T> : IOperation<T>
        where T : IUnit
    {
        private readonly T _reciever;
        public Operation(T reciever)
        {
            _reciever = reciever;
        }

        public string Name { get; set; }
        public T Reciever { get { return _reciever; } }

        public Func<T, Exception> Exec { get; set; }

        public Exception Execute()
        {
            return Exec(Reciever);
        }
    }

    public class Operation : Operation<IUnit>
    {
        public Operation(IUnit reciever) : base(reciever) { }
    }
}
