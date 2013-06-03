using discrete_machine;
using discrete_machine.Abstract;
using discrete_machine.CyclogramElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace discrete_machine_app.Model
{
    class ObservableValue : INotifyPropertyChanged
    {
        private readonly OperationsStep _step;
        private readonly IOperation _operation;

        public ObservableValue(int number, Cyclogram cyclogram, IOperation operation)
        {
            _operation = operation;
            _step = cyclogram.Steps[number] as OperationsStep;
            Number = number;

            _step.CollectionChanged += _step_CollectionChanged;
        }


        public int Number { get; set; }

        public bool Value
        {
            get
            {
                return _step.Contains(_operation);
            }
            set
            {
                if (value)
                    _step.AddOperation(_operation);
                else
                    _step.RemoveOperation(_operation);
            }
        }

        void _step_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (var item in e.NewItems)
                    if (item == _operation) OnPropertyChanged("Value");
            if (e.OldItems != null)
                foreach (var item in e.OldItems)
                    if (item == _operation) OnPropertyChanged("Value");
        }

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
