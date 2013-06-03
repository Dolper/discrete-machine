using discrete_machine;
using discrete_machine.Abstract;
using discrete_machine.Elements;
using discrete_machine_app.Model;
using discrete_machine_app.Stuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            if(element.Element is Summator)
                MainElementTemplate = (ControlTemplate)FindResource("SummatorTemplate");
            else if (element.Element is Register)
                MainElementTemplate = (ControlTemplate)FindResource("RegisterTemplate");
            else
                MainElementTemplate = (ControlTemplate)FindResource("DefaultTemplate");

            Binding templateBinding = new Binding();
            templateBinding.Source = this.MainElementTemplate;
            MainElement.SetBinding(Control.TemplateProperty, templateBinding);

            reinitMenu();
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(param => this.OnDelete());
                }
                return _deleteCommand;
            }
        }

        private void reinitMenu()
        {
            var menu = new ContextMenu();
            menu.Items.Add(new MenuItem { Header = "Удалить", Command = DeleteCommand });
            menu.Items.Add(new Separator());
            foreach (var item in Model.Operations.Select(x => new MenuItem { Header = x.Name, IsEnabled = false }))
                menu.Items.Add(item);

            MainElement.ContextMenu = menu;
        }
        private void OnDelete()
        {
            if (WantsToBeDeleted != null)
                WantsToBeDeleted(this, new EventArgs());
        }
         
        public static readonly DependencyProperty MainElementTemplateProperty =
            DependencyProperty.Register("MainElementTemplate", typeof(ControlTemplate), typeof(ElementTemplate));
        public ControlTemplate MainElementTemplate
        {
            get
            {
                return this.GetValue(MainElementTemplateProperty) as ControlTemplate;
            }
            set
            {
                this.SetValue(MainElementTemplateProperty, value);
            }
        }

        public ElementTemplate() :
            this(new ElementProxy(new Summator("Test Element")))
        { }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Model.Name);
        }
        
        public UIElementCoords ConnectorCoords(IConnector connector)
        {
            if (Model.Input.Contains(connector))
            {
                var itemUI = InputPanel.ItemContainerGenerator.ContainerFromItem(connector) as UIElement;
                var result = new UIElementCoords(itemUI, this);

                return result;
            }
            if (Model.Output.Contains(connector))
            {
                var itemUI = OutputPanel.ItemContainerGenerator.ContainerFromItem(connector) as UIElement;
                var result = new UIElementCoords(itemUI, this);

                return result;
            }
            return null;
        }

        private void OutputPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: remove check, make it two-way connectable
            if (sender != OutputPanel) return;

            var dataSource = sender as ListBox;
            var connector = GetDataFromListBox(dataSource, e.GetPosition(dataSource)) as IConnector;
            var connectorUI = ConnectorCoords(connector);
            var wireProxy = new WireProxy(connectorUI, connector);

            if (wireProxy != null)
            {
                DragDrop.DoDragDrop(OutputPanel, wireProxy, DragDropEffects.Move);
            }
        }

        private void InputPanel_Drop(object sender, DragEventArgs e)
        {
            // TODO: remove check, make it two-way connectable
            if (sender != InputPanel) return;

            var dataSource = sender as ListBox;
            var connector = GetDataFromListBox(dataSource, e.GetPosition(dataSource)) as IConnector;
            var connectorUI = ConnectorCoords(connector);

            var wire = e.Data.GetData(typeof(WireProxy)) as WireProxy;
            if (wire != null)
            {
                wire.ConnectTo(connectorUI, connector);
            }
            // TODO: Register wire
            var app = (App)Application.Current;
            app.Machine.AddWire(wire);
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

        public event EventHandler WantsToBeDeleted;

    }
}
