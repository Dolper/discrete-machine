using discrete_machine;
using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace discrete_machine_app.Model
{
    public class WireProxy
    {
        public WireProxy(Wire wire)
        {
            Wire = wire;
        }
        public WireProxy(UIElement ConnectorUI, IConnector connector)
        {
            OutputConnector = ConnectorUI;
            Wire = new Wire(connector);
        }

        public void ConnectTo(UIElement ConnectorUI, IConnector connector)
        {
            InputConnector = ConnectorUI;
            Wire.ConnectTo(connector);
        }

        public Wire Wire { get; protected set; }
        public UIElement InputConnector { get; protected set; }
        public UIElement OutputConnector { get; protected set; }
    }
}
