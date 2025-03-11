using System.Windows;
using System.Windows.Controls;

namespace AudioApp.Panels
{
    /// <summary>
    /// Interaction logic for OscillatorPanel.xaml
    /// </summary>
    public partial class OscillatorPanel : UserControl
    {

        public static new readonly DependencyProperty HeightProperty = DependencyProperty.Register(nameof(Height), typeof(int), typeof(OscillatorPanel), new PropertyMetadata(default(int)));

        public new int Height
        {
            get => (int)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public static new readonly DependencyProperty WidthProperty = DependencyProperty.Register(nameof(Width), typeof(int), typeof(OscillatorPanel), new PropertyMetadata(default(int)));

        public new int Width
        {
            get => (int)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }
        public OscillatorPanel()
        {
            InitializeComponent();
        }
    }
}
