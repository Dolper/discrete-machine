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
            DataContext = Model;
        }

        public ElementTemplate() :
            this(new ElementProxy(new Summator("Test Element")))
        { }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Model.Name);
        }

        private void OutputPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: remove check, make it two-way connectable
            if (sender != OutputPanel) return;

            var dataSource = sender as ListBox;
            var connector = GetDataFromListBox(dataSource, e.GetPosition(dataSource)) as IConnector;
            var wire = new Wire(connector);

            if (wire != null)
            {
                DragDrop.DoDragDrop(OutputPanel, wire, DragDropEffects.Move);
            }
        }

        private void InputPanel_Drop(object sender, DragEventArgs e)
        {
            // TODO: remove check, make it two-way connectable
            if (sender != InputPanel) return;

            var dataSource = sender as ListBox;
            var connector = GetDataFromListBox(dataSource, e.GetPosition(dataSource)) as IConnector;

            var wire = e.Data.GetData(typeof(Wire)) as Wire;
            if (wire != null)
            {
                wire.ConnectTo(connector);
            }
            // TODO: Register wire
        }

        #region GetDataFromListBox(ListBox,Point)
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }

                    if (element == source)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }

            return null;
        }
        #endregion
    }
}
