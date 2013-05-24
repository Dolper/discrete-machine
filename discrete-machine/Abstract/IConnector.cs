using System.ComponentModel;

namespace discrete_machine.Abstract
{
    public interface IConnector : INotifyPropertyChanged
    {
        string Name { get; set; }
        int Value { get; set; }
    }
}
