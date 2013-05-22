using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace discrete_machine.Abstract
{
    public interface IElement: INotifyPropertyChanged
    {
        Guid Id { get; }

        String Name { get; set; }
        ICollection<IConnector> Input { get; }
        ICollection<IConnector> Output { get; }
        ICollection<IOperation> Operations { get; }
    }
}
