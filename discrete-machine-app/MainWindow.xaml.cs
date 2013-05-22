using discrete_machine;
using discrete_machine.Abstract;
using discrete_machine.Elements;
using discrete_machine_app.Model;
using discrete_machine_app.Templates;
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

namespace discrete_machine_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var app = (App)Application.Current;
            InitializeComponent();

            var machine = app.Machine = new MachineAdapter(new Machine());

            machine.AddElement(new ElementProxy(new Summator("Summator1"))
            {
                Top = 20,
                Left = 100,
            });

            foreach (var element in app.Machine.Elements)
            {
                var elementTemplate = new ElementTemplate(element);
                SchemeCanvas.Children.Add(elementTemplate);
            }
        }
    }
}
