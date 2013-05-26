using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace discrete_machine_app.Model
{
    public class ElementProxy : IElement
    {
        private readonly IElement _host;
        public ElementProxy(IElement host)
        {
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;
        }

        public IElement Element { get { return _host; } }

        private Point position;
        public Point Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        private string template;
        public string Template
        {
            get { return template; }
            set
            {
                template = value;
                OnPropertyChanged("Template");
            }
        }

        #region IElement implementation
        public Guid Id
        {
            get { return Element.Id; }
            set { Element.Id = value; }
        }

        public string Name
        {
            get { return Element.Name; }
            set { Element.Name = value; }
        }

        public IEnumerable<IConnector> Input
        {
            get { return Element.Input; }
        }

        public IEnumerable<IConnector> Output
        {
            get { return Element.Output; }
        }

        public IEnumerable<IOperation> Operations
        {
            get { return Element.Operations; }
        }


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnHostPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
