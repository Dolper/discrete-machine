using discrete_machine.Abstract;
using discrete_machine.CyclogramElements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace discrete_machine_app
{
    /// <summary>
    /// Interaction logic for AddConditionStepDialog.xaml
    /// </summary>
    public partial class AddConditionStepDialog : Window
    {
        public IEnumerable<IConnector> AvailableConnectors { get; set; }

        public AddConditionStepDialog()
        {
            InitializeComponent();
            DataContext = this;

            ConditionCombobox.ItemsSource =
                Enum.GetValues(typeof(StepCondition)).Cast<StepCondition>();
        }

        public IConnector Connector { get; set; }
        public StepCondition StepCondition { get; set; }
        public int Operand { get; set; }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Connector == null)
            {
                ConnectorCombobox.IsDropDownOpen = true;
                return;
            }

            this.DialogResult = true;
        }
    }

}
