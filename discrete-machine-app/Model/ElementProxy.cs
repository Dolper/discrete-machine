using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace discrete_machine_app.Model
{
    public class ElementProxy : IElement
    {
        private readonly IElement _host;
        public ElementProxy(IElement host)
        {
            _host = host;
        }

        public IElement Element { get { return _host; } }

        public int Top { get; set; }
        public int Left { get; set; }
        public string Template { get; set; }

        #region IElement implementation
        public Guid Id
        {
            get { return Element.Id; }
        }

        public string Name
        {
            get { return Element.Name; }
            set { Element.Name = value; }
        }

        public ICollection<IConnector> Input
        {
            get { return Element.Input; }
        }

        public ICollection<IConnector> Output
        {
            get { return Element.Output; }
        }

        public ICollection<IOperation> Operations
        {
            get { return Element.Operations; }
        }

        // TODO: make proxy properties changes noticable
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add { _host.PropertyChanged += value; }
            remove { _host.PropertyChanged -= value; }
        }
        #endregion
    }
}
