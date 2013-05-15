using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine.Abstract
{
    public interface IElement
    {
        Guid Id { get; }

        String Name { get; set; }
        ICollection<IConnector> Input { get; }
        ICollection<IConnector> Output { get; }
        ICollection<IOperation> Operations { get; }
    }
}
