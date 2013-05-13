using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine.Abstract
{
    public interface IOperation<out T>
        //where T: IUnit
    {
        T Reciever { get; }
        string Name { get; set; }
        Exception Execute();
    }

    public interface IOperation : IOperation<IUnit>
    {
    }
}
