using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Counter : Element
    {
        private readonly Connector _p = new ConnectorIntern();

        public Exception Increment()
        {
            _p.Value++;
            return null;
        }

        public Exception Reset()
        {
            _p.Value=0;
            return null;
        }

        public Counter()
        {
            _input = new IConnector[] { };
            _output = new IConnector[] { };
            _operations = new IOperation<Counter>[] { 
                new Operation<Counter>(this) { 
                    Exec = x => x.Increment() 
                }, 
                new Operation<Counter>(this) { 
                    Exec = x => x.Reset() 
                }, 
            }.Select(x => x as IOperation);
        }

        private IEnumerable<IConnector> _input;
        private IEnumerable<IConnector> _output;
        private IEnumerable<IOperation> _operations;


        public override IEnumerable<IConnector> Input
        {
            get { return _input; }
        }

        public override IEnumerable<IConnector> Output
        {
            get { return _output; }
        }

        public override IEnumerable<Abstract.IOperation> Operations
        {
            get { return _operations; }
        }
    }
}
