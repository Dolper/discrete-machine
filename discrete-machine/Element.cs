using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
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
            }
        }
        public string Name { get; set; }

        public virtual ICollection<IConnector> Input { get; protected set; }
        public virtual ICollection<IConnector> Output { get; protected set; }
        public virtual ICollection<IOperation> Operations { get; protected set; }
    }
}
