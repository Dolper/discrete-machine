using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public abstract class Connector : IConnector
    {
        private IElement host;
        public Connector(string name, IElement elementForName = null)
        {
            this.name = name;
            host = elementForName;
            if (host != null)
                host.PropertyChanged += host_PropertyChanged;
        }

        private string name;
        public string Name
        {
            get
            {
                if (host != null)
                    return host.Name + " " + name;
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        protected int value = 0;
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        void host_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
                OnPropertyChanged("Name");
        }

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ConnectorIn : Connector
    {
        public ConnectorIn(string name, IElement host = null) : base(name, host) { }
        public ConnectorIn(IElement host = null) : base("_x1", host) { }
    }
    public class ConnectorOut : Connector
    {
        public ConnectorOut(string name, IElement host = null) : base(name, host) { }
        public ConnectorOut(IElement host = null) : base("_r", host) { }
    }
    public class ConnectorIntern : Connector
    {
        public ConnectorIntern(string name, IElement host = null) : base(name, host) { }
        public ConnectorIntern(IElement host = null) : base("_p", host) { }
    }
}
