﻿using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace discrete_machine
{
    public class Wire: IOperation<Wire>, IElement
    {
        public Wire(IConnector connectorOut)
        {
            Out = connectorOut;
        }
        public void ConnectTo(IConnector connectorIn)
        {
            In = connectorIn;
        }

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

        public string Name
        {
            get
            {
                // TODO: Handle
                if (In != null && Out != null)
                    return string.Format("{0} -> {1}", Out.Name, In.Name);

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

        /// <summary>
        /// Not Implemented!
        /// </summary>
        public IEnumerable<IConnector> Input
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented!
        /// </summary>
        public IEnumerable<IConnector> Output
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented!
        /// </summary>
        public IEnumerable<IOperation> Operations
        {
            get { throw new NotImplementedException(); }
        }

        // TODO: implement
        public event PropertyChangedEventHandler PropertyChanged;

        public string FullName
        {
            get
            {
                // TODO: Handle
                if (In != null && Out != null)
                    return string.Format("{0} -> {1}", Out.Name, In.Name);

                return "???";
            }
        }
    }
}
