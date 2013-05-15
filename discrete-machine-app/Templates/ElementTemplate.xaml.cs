using discrete_machine.Abstract;
using discrete_machine_app.Model;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace discrete_machine_app.Templates
{
    /// <summary>
    /// Interaction logic for ElementTemplate.xaml
    /// </summary>
    public partial class ElementTemplate : UserControl
    {
        public ElementTemplate()
        {
            // test one
            InitializeComponent();
            Inputs = new List<string> {
                "1",
                "2",
            };
            Outputs = new List<string> {
                "1",
            };
            Internals = new List<string> {
                "1",
            };

            InputPanel.ItemsSource = Inputs;
            OutputPanel.ItemsSource = Outputs;
            InternalsPanel.ItemsSource = Internals;
        }

        private readonly ElementProxy _element;
        public ElementTemplate(ElementProxy element)
        {
            InitializeComponent();
            _element = element;

            InputPanel.ItemsSource = _element.Input;
            OutputPanel.ItemsSource = _element.Output;
            //InternalsPanel.ItemsSource =  Internals;
        }

        public IEnumerable<string> Inputs { get; set; }
        public IEnumerable<string> Outputs { get; set; }
        public IEnumerable<string> Internals { get; set; }
    }
}
