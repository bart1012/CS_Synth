using System.Windows;
using System.Windows.Controls;

namespace AudioApp.Controls
{
    /// <summary>
    /// Interaction logic for OctaveSelectorControl.xaml
    /// </summary>
    public partial class OctaveSelectorControl : UserControl
    {

        public static DependencyProperty OctaveProperty = DependencyProperty.Register(nameof(Octave), typeof(int), typeof(OctaveSelectorControl), new PropertyMetadata(1));

        public int Octave
        {
            get => (int)GetValue(OctaveProperty);
            set => SetValue(OctaveProperty, value);
        }

        public OctaveSelectorControl()
        {
            InitializeComponent();
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Octave < 7) Octave++;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Octave > 2) Octave--;

        }
    }
}
