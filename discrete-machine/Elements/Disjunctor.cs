using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Disjunctor : Element
    {
        private readonly Connector _a = new ConnectorIn();
        private readonly Connector _b = new ConnectorIn();
        private readonly Connector _r = new ConnectorOut();

        public Exception Disjunct()
        {
            if (_a.Value > 1 || _b.Value > 1) return new Exception("Входные данные в дизъюнктор не валидны");
            _r.Value = _a.Value | _b.Value;
            return null;
        }

        public Disjunctor(String name) : base(name)
        {
            Input = new IConnector[] { _a, _b };
            Output = new IConnector[] { _r };
            Operations = new IOperation<Disjunctor>[] {
                new Operation<Disjunctor>(this) {
                    Name = "Запуск",
                    Exec = x => x.Disjunct()
                },
            };
        }
    }
}