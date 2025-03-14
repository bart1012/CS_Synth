using System.Windows;

namespace AudioApp.Panels
{
    /// <summary>
    /// Interaction logic for OscillatorPanel.xaml
    /// </summary>
    public partial class OscillatorPanel : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty GainProperty =
     DependencyProperty.Register(nameof(Gain), typeof(double), typeof(OscillatorPanel),
         new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGainChanged));

        public double Gain
        {
            get => (double)GetValue(GainProperty);
            set => SetValue(GainProperty, value);
        }

        private static void OnGainChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as OscillatorPanel;
            Console.WriteLine($"Gain Updated in OscillatorPanel: {e.NewValue}");  // Debugging output
        }

        public OscillatorPanel()
        {
            InitializeComponent();
        }


    }
}
