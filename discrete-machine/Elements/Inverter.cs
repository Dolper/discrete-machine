using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Inverter:Element
    {
        private readonly Connector _a;
        private readonly Connector _r;

        public Exception Invert()
        {
            if (_a.Value > 1) return new Exception("Входные данные в инвертор не валидны");
            _r.Value = (_a.Value == 1) ? 0 : 1;
            return null;
        }

        public Inverter(String name):base(name)
        {
            _a = new ConnectorIn("X", this);
            _r = new ConnectorOut("R", this);

            Input = new IConnector[] { _a };
            Output = new IConnector[] { _r };
            Operations = new IOperation<Inverter>[] {
                new Operation<Inverter>(this) {
                    Name = "Запуск",
                    Exec = x => x.Invert()
                },
            };
        }
    }
}
