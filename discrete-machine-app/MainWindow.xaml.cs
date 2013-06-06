using discrete_machine;
using discrete_machine.Abstract;
using discrete_machine.CyclogramElements;
using discrete_machine.Elements;
using discrete_machine_app.Model;
using discrete_machine_app.Stuff;
using discrete_machine_app.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            DataContext = this;
            var machine = app.Machine = new MachineAdapter();

            machine.Wires.CollectionChanged += wiresCollectionChanged;
            machine.Steps.CollectionChanged += stepsCollectionChanged;

            CyclogramGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "",
                Binding = new Binding("Name")
                {
                    Mode = BindingMode.OneWay
                },
            });
            CyclogramGrid.ItemsSource = cyclItems;
        }
        private ObservableCollection<BindableCyclogramElement> cyclItems = new ObservableCollection<BindableCyclogramElement>();

        private ICommand _addColumnCommand;
        private void AddEmptyColumn()
        {
            var machine = ((App)Application.Current).Machine;
            machine.AddStep(new OperationsStep());
        }
        private void stepsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < e.NewItems.Count; i++)
            {
                var index = e.NewStartingIndex + i;
                var newCol = new DataGridCheckBoxColumn()
                {
                    Header = index,
                    Binding = new Binding("[" + index + "].Value")
                    {
                        Mode = BindingMode.TwoWay,
                        Converter = new BoolToBoolConverter(),
                    }
                };
                CyclogramGrid.Columns.Add(newCol);
            }
        }
        public void AddNewOperation(object sender, AddOperationEventArgs e)
        {
            var machine = ((App)Application.Current).Machine;
            var elem = new BindableCyclogramElement(machine.Cyclogram, e.Operation);
            cyclItems.Add(elem);
            //CyclogramGrid.Items.Add(elem);
        }

        #region Wires
        private List<Control> _wiresControls = new List<Control>();
        private void wiresCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
                foreach (WireProxy wire in e.NewItems)
                {
                    var c = new Control();
                    c.DataContext = wire;
                    c.Template = FindResource("WireTemplate") as ControlTemplate;
                    _wiresControls.Add(c);
                    SchemeCanvas.Children.Add(c);
                }
            if(e.OldItems != null)
                foreach (WireProxy wire in e.OldItems)
                {
                    var uiElement = _wiresControls.First(x => x.DataContext == wire);
                    if (uiElement != null)
                    {
                        SchemeCanvas.Children.Remove(uiElement);
                        _wiresControls.Remove(uiElement);
                    }
                }
        }
        #endregion Wires


        #region ElementCreation
        private ElementTemplate AddElement(ElementProxy el)
        {
            var position = new Point();

            el.Position = position;

            var et = new ElementTemplate(el);
            et.MouseDown += Rectangle_MouseDown;
            et.MouseMove += Rectangle_MouseMove;
            et.MouseUp += Rectangle_MouseUp;
            SchemeCanvas.Children.Add(et);

            et.WantsToBeDeleted += DeleteElement;
            et.AddOperation += AddNewOperation;
            return et;
        }

        private void Label_MouseDown(object sender, EventArgs e)
        {
            if (!(sender is MenuItem)) return;

            var app = (App)Application.Current;
            ElementProxy ep = new ElementProxy(new Summator("Error"));

            switch((sender as MenuItem).Header.ToString())
            {
                case "Сумматор":
                    ep = new ElementProxy(new Summator("Сумматор ∑"));
                    break;
                case "Инвертор":
                    ep = new ElementProxy(new Inverter("Инвертор ⌐"));
                    break;
                case "Конъюнктор":
                    ep = new ElementProxy(new Сonjunctor("Конъюнктор &"));
                    break;
                case "Дизъюнктор":
                    ep = new ElementProxy(new Disjunctor("Дизъюнктор |"));
                    break;
                case "Регистр":
                    ep = new ElementProxy(new Register());
                    break;
            }
            
            app.Machine.AddElement(ep);
            var et = AddElement(ep);
            var position = new Point(50, 50);
            ep.Position = position;
        }
        #endregion

        #region ElementMoving

        private bool isDragging;
        private void Rectangle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var el = sender as ElementTemplate;
            if (el != null)
            {
                el.CaptureMouse();
                isDragging = true;
            }
        }

        private void Rectangle_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDragging)
            {
                Point canvPosToWindow = SchemeCanvas.TransformToAncestor(this).Transform(new Point(0, 0));

                var r = sender as ElementTemplate;
                var upperlimit = canvPosToWindow.Y + (r.DesiredSize.Height / 2);
                var lowerlimit = canvPosToWindow.Y + SchemeCanvas.ActualHeight - (r.DesiredSize.Height / 2);

                var leftlimit = canvPosToWindow.X + (r.DesiredSize.Width / 2);
                var rightlimit = canvPosToWindow.X + SchemeCanvas.ActualWidth - (r.DesiredSize.Width / 2);


                var absmouseXpos = e.GetPosition(this).X;
                var absmouseYpos = e.GetPosition(this).Y;

                r.SetValue(Canvas.LeftProperty, e.GetPosition(SchemeCanvas).X - (r.DesiredSize.Width / 2));
                r.SetValue(Canvas.TopProperty, e.GetPosition(SchemeCanvas).Y - (r.DesiredSize.Height / 2));

            }
        }

        private void Rectangle_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var el = sender as ElementTemplate;
            if (el != null)
            {
                el.ReleaseMouseCapture();
                isDragging = false;
            }
        }
        #endregion


        private void MenuItem_Click(object sender, EventArgs e)
        {
            Label_MouseDown(sender, e);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Label_MouseDown(sender, e);
        }

        public void DeleteElement(object sender, EventArgs e)
        {
            var et = sender as ElementTemplate;
            if (et == null) return;
            
            var machine = ((App)Application.Current).Machine;
            machine.RemoveElement(et.Model);

            var cyclogramRowsToDelete = cyclItems.Where(x => et.Model.Operations.Contains(x.Operation)).ToList();
            foreach (var row in cyclogramRowsToDelete)
                cyclItems.Remove(row);

            SchemeCanvas.Children.Remove(et);
        }

        private void AddOperationColumn_Click(object sender, RoutedEventArgs e)
        {
            AddEmptyColumn();
        }
        private void AddConditionColumn_Click(object sender, RoutedEventArgs e)
        {
            var machine = ((App)Application.Current).Machine;

            var operations = machine.Elements.SelectMany(x => x.Input.Concat(x.Output));
            var diag = new AddConditionStepDialog()
            {
                Owner = this,
                AvailableConnectors = operations
            };
            if (diag.ShowDialog() == true)
            {
                
            }
        }

    }
}
