using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine.Abstract
{
    public interface IElement : IUnit
    {
        Guid Id { get; }

        IEnumerable<IConnector> Input { get; }
        IEnumerable<IConnector> Output { get; }
    }
}
