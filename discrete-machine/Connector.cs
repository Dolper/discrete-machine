using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public abstract class Connector : IConnector
    {
        public string Name { get; set; }

        protected int value = 0;
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                if (ValueChanged != null) ValueChanged(this, new EventArgs());
            }
        }

        public event EventHandler ValueChanged;
    }

    public class ConnectorIn : Connector { }
    public class ConnectorOut : Connector { }
}
