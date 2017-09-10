using System.Windows;
using System.Windows.Controls;

namespace Client
{
    public partial class GaugeControl : UserControl
    {

        public GaugeControl()
        {
            InitializeComponent();
        }
        
        public double InputValue
        {
            get { return (double)GetValue(InputValueProperty); }
            set { SetValue(InputValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for InputValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputValueProperty =
            DependencyProperty.Register("InputValue", typeof(double), typeof(GaugeControl));
    }
}