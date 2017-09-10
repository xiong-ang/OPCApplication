using System.Windows.Controls;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace Client
{
    public partial class ChartControl : UserControl
    {
        public ChartControl()
        {
            InitializeComponent();
        }

        public SimplingPoint InputSimplingPoint
        {
            get { return (SimplingPoint)GetValue(InputSimplingPointProperty); }
            set { SetValue(InputSimplingPointProperty, value); }
        }
        // Using a DependencyProperty as the backing store for InputSimplingPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputSimplingPointProperty =
            DependencyProperty.Register("InputSimplingPoint", typeof(SimplingPoint), typeof(ChartControl), new PropertyMetadata(OnSignalChanged));
             

        private static void OnSignalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChartControl;
            Messenger.Default.Send<SimplingPoint>(control.InputSimplingPoint, ChartMessageTokens.SimplingPointChangedFromView);            
        }      
        
    }
}