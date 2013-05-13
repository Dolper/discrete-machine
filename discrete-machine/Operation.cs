﻿using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class Operation<T> : IOperation<T>
        where T : IElement
    {
        private readonly T _reciever;
        public Operation(T reciever)
        {
            _reciever = reciever;
        }

        public string Name { get { return _reciever.Name; } set { throw new NotImplementedException(); } }
        public T Reciever { get { return _reciever; } }

        public Func<T, Exception> Exec { get; set; }

        public Exception Execute()
        {
            return Exec(Reciever);
        }
    }

    public class Operation : Operation<IElement>
    {
        public Operation(IElement reciever) : base(reciever) { }
    }
}
