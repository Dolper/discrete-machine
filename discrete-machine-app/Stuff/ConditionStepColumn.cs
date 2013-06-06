using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace discrete_machine_app.Stuff
{
    class ConditionStepColumn : DataGridTextColumn
    {
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {

            var textBox = (TextBox)base.GenerateEditingElement(cell, dataItem);

            return textBox;
        }
    }
}
