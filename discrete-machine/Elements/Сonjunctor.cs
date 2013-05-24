using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Сonjunctor : Element
    {
        private readonly Connector _a = new ConnectorIn();
        private readonly Connector _b = new ConnectorIn();
        private readonly Connector _r = new ConnectorOut();

        public Exception Сonjunct()
        {
            if (_a.Value > 1 || _b.Value > 1) return new Exception("Входные данные в конъюнкор не валидны");
            _r.Value = _a.Value & _b.Value;
            return null;
        }

        public Сonjunctor()
        {
            Input = new IConnector[] { _a };
            Output = new IConnector[] { _r };
            Operations = new IOperation<Сonjunctor>[] { 
                new Operation<Сonjunctor>(this) { 
                    Exec = x => x.Сonjunct() 
                }, 
            }.Select(x => x as IOperation);
        }
    }
}
