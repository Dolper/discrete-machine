using discrete_machine;
using discrete_machine.Abstract;
using discrete_machine.Elements;
using discrete_machine_app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public ElementProxy Model { get; set; }

        public ElementTemplate(ElementProxy element)
        {
            InitializeComponent();
            Model = element;
            Initialize();
        }

        public ElementTemplate()
        {
            InitializeComponent();
            Model = new ElementProxy(new Summator("Test Element"));
            Initialize();
        }

        private void Initialize()
        {
            this.DataContext = Model;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Model.Name);
        }
    }
}
