using discrete_machine;
using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace discrete_machine_app.Model
{
    public class WireProxy
    {
        public WireProxy(Wire wire)
        {
            Wire = wire;
        }
        public WireProxy(UIElementCoords coordsProperty, IConnector connector)
        {
            Out = coordsProperty;
            Wire = new Wire(connector);
        }

        public void ConnectTo(UIElementCoords coordsProperty, IConnector connector)
        {
            In = coordsProperty;
            Wire.ConnectTo(connector);
        }

        public string Name { get { return Wire.Name; } set { Wire.Name = value; } }

        public Wire Wire { get; protected set; }
        public UIElementCoords In { get; protected set; }
        public UIElementCoords Out { get; protected set; }
    }

    /// <summary>
    /// Caution: element in parent relative position must not change
    /// </summary>
    public class UIElementCoords: INotifyPropertyChanged
    {
        private UIElement parentElement;
        private UIElement element;

        private Point relativePosition;

        public UIElementCoords(UIElement element, UIElement parent)
        {
            this.element = element;
            this.parentElement = parent;

            relativePosition = element.TransformToAncestor(parentElement).Transform(new Point(0, 0));
            relativePosition = new Point(relativePosition.X + element.RenderSize.Width / 2, relativePosition.Y + element.RenderSize.Height / 2);

            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, parent.GetType())
                .AddValueChanged(parent, this.ProperyChanged);
            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, parent.GetType())
                .AddValueChanged(parent, this.ProperyChanged);

        }

        private double x;
        public double X
        {
            get
            {
                var canvasPosition = new Vector(Canvas.GetLeft(parentElement), Canvas.GetTop(parentElement));
                var result = relativePosition + canvasPosition;
                return result.X;
            }
        }

        private double y;
        public double Y
        {
            get
            {
                var canvasPosition = new Vector(Canvas.GetLeft(parentElement), Canvas.GetTop(parentElement));
                var result = relativePosition + canvasPosition;
                return result.Y;
            }
        }

        public void ProperyChanged(object sender, EventArgs e)
        {
            OnPropertiesChanged();
        }

        private void OnPropertiesChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("X"));
                PropertyChanged(this, new PropertyChangedEventArgs("Y"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
