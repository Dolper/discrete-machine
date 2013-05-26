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

            var machine = app.Machine = new MachineAdapter();

            //machine.AddElement(new ElementProxy(new Summator("Summator1")));

            foreach (var element in machine.Elements)
            {
                AddElement(element);
            }

            machine.Wires.CollectionChanged += collectionChanged;
        }

        private void collectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("ADDED");
            foreach (var item in e.NewItems)
            {
                var wire = item as WireProxy;
                if (wire == null) continue;

                var c = new Control();
                c.DataContext = wire;
                c.Template = FindResource("WireTemplate") as ControlTemplate;
                SchemeCanvas.Children.Add(c);
            }
        }

        private ElementTemplate AddElement(ElementProxy el, int? top = null, int? left = null)
        {
            var position = new Point();
            if(top.HasValue) position.Y = top.Value;
            if(left.HasValue) position.X = left.Value;

            el.Position = position;

            var et = new ElementTemplate(el);
            et.MouseDown += Rectangle_MouseDown;
            et.MouseMove += Rectangle_MouseMove;
            et.MouseUp += Rectangle_MouseUp;
            SchemeCanvas.Children.Add(et);

            return et;
        }

        #region ElementCreation

        private void Label_MouseDown(object sender, EventArgs e)
        {

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
            }
            
            app.Machine.AddElement(ep);
            var et = AddElement(ep);
           // var point = e.GetPosition(SchemeCanvas);
            var position = new Point(250, 350);
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
                var upperlimit = canvPosToWindow.Y + (r.Height / 2);
                var lowerlimit = canvPosToWindow.Y + SchemeCanvas.ActualHeight - (r.Height / 2);

                var leftlimit = canvPosToWindow.X + (r.Width / 2);
                var rightlimit = canvPosToWindow.X + SchemeCanvas.ActualWidth - (r.Width / 2);


                var absmouseXpos = e.GetPosition(this).X;
                var absmouseYpos = e.GetPosition(this).Y;

                r.SetValue(Canvas.LeftProperty, e.GetPosition(SchemeCanvas).X - (r.Width / 2));
                r.SetValue(Canvas.TopProperty, e.GetPosition(SchemeCanvas).Y - (r.Height / 2));

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
    }
}
