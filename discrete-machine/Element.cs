﻿using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public abstract class Element: IElement
    {
        public Element()
        {
            Input = new List<IConnector>();
            Output = new List<IConnector>();
            Operations = new List<IOperation>();
        }
        public Element(string name)
        {
            Name = name;
            Input = new List<IConnector>();
            Output = new List<IConnector>();
            Operations = new List<IOperation>();
        }

        private Guid id;
        public Guid Id
        {
            get
            {
                if (id == new Guid()) id = Guid.NewGuid();
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                OnPropertyChanged("Name");
                name = value;
            }
        }

        public virtual IEnumerable<IConnector> Input { get; protected set; }
        public virtual IEnumerable<IConnector> Output { get; protected set; }
        public virtual IEnumerable<IOperation> Operations { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            // TODO: make thread safe
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
