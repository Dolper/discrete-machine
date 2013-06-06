using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Сonjunctor : Element
    {
        private readonly Connector _a;
        private readonly Connector _b;
        private readonly Connector _r;

        public Exception Сonjunct()
        {
            if (_a.Value > 1 || _b.Value > 1) return new Exception("Входные данные в конъюнкор не валидны");
            _r.Value = _a.Value & _b.Value;
            return null;
        }

        public Сonjunctor(String name):base(name)
        {
            _a = new ConnectorIn("X1", this);
            _b = new ConnectorIn("X2", this);
            _r = new ConnectorOut("R", this);

            Input = new IConnector[] { _a, _b };
            Output = new IConnector[] { _r };
            Operations = new IOperation<Сonjunctor>[] {
                new Operation<Сonjunctor>(this) {
                    Name = "Запуск",
                    Exec = x => x.Сonjunct()
                },
            };
        }
    }
}
