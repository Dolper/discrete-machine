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
            Input = new IConnector[] { };
            Output = new IConnector[] { };
            Operations = new IOperation<Counter>[] {
                new Operation<Counter>(this) {
                    Name = "+1",
                    Exec = x => x.Increment(),
                },
                new Operation<Counter>(this) {
                    Name = "Сброс",
                    Exec = x => x.Reset()
                },
            };
        }
    }
}
