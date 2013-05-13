using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_machine
{
    public class Wire: IOperation<Wire>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Входной коннектор.
        /// Передача данных производится из выходного во входной, т.е. Out -> In.
        /// </summary>
        public IConnector In { get; set; }

        /// <summary>
        /// Выходной коннектор.
        /// Передача данных производится из выходного во входной, т.е. Out -> In.
        /// </summary>
        public IConnector Out { get; set; }

        public Wire Reciever
        {
            get { return this; }
        }

        string IOperation<Wire>.Name
        {
            get
            {
                // TODO: Handle
                if (In != null && Out != null)
                    return string.Format("{0} -> {1}", In.Name, Out.Name);

                return "???";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Exception Execute()
        {
            if (In == null)
                return new ArgumentException("Проблема c входом", "Out");
            In.Value = Out.Value;
            return null;
        }
    }
}
