using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Summator : Element
    {
        private readonly Connector _a = new ConnectorIn();
        private readonly Connector _b = new ConnectorIn();
        private readonly Connector _r = new ConnectorOut();
        private readonly Connector _p = new ConnectorIntern();

        public Exception Sum()
        {
            if (_a.Value > 1 || _b.Value > 1) return new Exception("Входные данные в сумматор не валидны");
            var result = _a.Value + _b.Value + _p.Value;
            _p.Value = result & 2;
            _r.Value = result & 1;
            return null;
        }

        public Summator(string name) : base(name)
        {
            Input = new IConnector[] { _a, _b };
            Output = new IConnector[] { _r };
            Operations = new IOperation<Summator>[] { 
                new Operation<Summator>(this) { 
                    Exec = x => x.Sum() 
                }, 
            }.Select(x => x as IOperation);
        }
    }
}
