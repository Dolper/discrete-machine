using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace discrete_machine.Abstract
{
    public interface IElement: INotifyPropertyChanged
    {
        Guid Id { get; set; }

        String Name { get; set; }
        IEnumerable<IConnector> Input { get; }
        IEnumerable<IConnector> Output { get; }
        IEnumerable<IOperation> Operations { get; }
    }
}
