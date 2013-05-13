using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine.Abstract
{
    public interface IUnit
    {
        /// <summary>
        /// Id for serialization
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Name to display to user
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Operations available to unit
        /// </summary>
        IOperation[] Operations { get; }
    }
}
