using RealTimeGraphX;
using System.Windows;
using System.Windows.Controls;

namespace Synchronisiereinrichtung
{
    public class WpfGraphControl : Control
    {
        public IGraphController Controller
        {
            get => (IGraphController)GetValue(ControllerProperty);
            set => SetValue(ControllerProperty, value);
        }

        public static readonly DependencyProperty ControllerProperty = DependencyProperty.Register("Controller", typeof(IGraphController), typeof(WpfGraphControl), new PropertyMetadata(null));

        static WpfGraphControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WpfGraphControl), new FrameworkPropertyMetadata(typeof(WpfGraphControl)));
        }
    }
}