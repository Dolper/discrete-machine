using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using discrete_machine.Abstract;

namespace discrete_machine.Elements
{
    public class Register : Element
    {
        private readonly Connector _a = new ConnectorIn();
        private readonly Connector _word = new ConnectorIn();
        private readonly Connector _p = new ConnectorIntern();
        private readonly Connector _r = new ConnectorOut();
        private readonly Int16 _count = 4;

        public Exception Init()
        {
            _p.Value = _word.Value;
            return null;
        }

        public Exception Step()
        {
            if (_a.Value > 1) return new Exception("Входные данные в регистр не валидны");
            
            _r.Value = _p.Value & 1;
            _p.Value = _p.Value >> 1;
            
            return null;
        }

        public Exception CyclicStep()
        {
            if (_a.Value > 1) return new Exception("Входные данные в регистр не валидны");

            _r.Value = _p.Value & 1;
            _p.Value = _p.Value >> 1 + _p.Value << _count-1;
                     
            return null;
        }
        

        public Register()
        {
            _input = new IConnector[] { _a };
            _output = new IConnector[] { _r };
            _operations = new IOperation<Register>[] { 
                new Operation<Register>(this) { 
                    Exec = x => x.Step() 
                }, 
                new Operation<Register>(this) { 
                    Exec = x => x.CyclicStep() 
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

        public override IEnumerable<IOperation> Operations
        {
            get { return _operations; }
        }
    }
}
