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

        public abstract IEnumerable<IConnector> Input { get; }
        public abstract IEnumerable<IConnector> Output { get; }
        public abstract IEnumerable<IOperation> Operations { get; }
    }
}
