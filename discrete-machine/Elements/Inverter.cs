using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Inverter:Element
    {
        private readonly Connector _a = new ConnectorIn();
        private readonly Connector _r = new ConnectorOut();

        public Exception Invert()
        {
            if (_a.Value > 1) return new Exception("Входные данные в инвертор не валидны");
            _r.Value = (_a.Value == 1) ? 0 : 1;
            return null;
        }

        public Inverter()
        {
            _input = new IConnector[] { _a };
            _output = new IConnector[] { _r };
            _operations = new IOperation<Inverter>[] { 
                new Operation<Inverter>(this) { 
                    Exec = x => x.Invert() 
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
