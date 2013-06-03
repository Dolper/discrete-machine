using discrete_machine;
using discrete_machine.Abstract;
using discrete_machine.CyclogramElements;
using System.Collections.Generic;

namespace discrete_machine_app.Model
{
    class BindableCyclogramElement
    {
        private readonly Cyclogram _cyclogram;
        private readonly IOperation _operation;
        private readonly List<ObservableValue> _values;

        public BindableCyclogramElement(Cyclogram cyclogram, IOperation operation)
        {
            _cyclogram = cyclogram;
            _operation = operation;
            _values = new List<ObservableValue>();
        }
        public ObservableValue this[int index]
        {
            get
            {
                var result = _values.Find(x => x.Number == index);
                if (result == null)
                {
                    result = new ObservableValue(index, _cyclogram, _operation);
                    _values.Add(result);
                }
                return result;
            }
        }
        public string Name { get { return _operation.FullName; } }
    }
}
