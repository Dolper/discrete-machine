using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Summator : Element
    {
        private readonly Connector _a;
        private readonly Connector _b;
        private readonly Connector _r;
        private readonly Connector _p = new ConnectorIntern();

        public Connector P { get { return _p; } }

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
            _a = new ConnectorIn("X1", this);
            _b = new ConnectorIn("X2", this);
            _r = new ConnectorOut("R", this);

            Input = new IConnector[] { _a, _b };
            Output = new IConnector[] { _r };
            Operations = new Operation<Summator>[] { 
                new Operation<Summator>(this) {
                    Name = "Запуск",
                    Exec = x => x.Sum() 
                },
            };
        }
    }
}
