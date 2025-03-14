using AudioApp.Models;
using System.Windows;
using System.Windows.Input;


namespace AudioApp
{
    public partial class MainWindow : Window
    {
        private HashSet<Key> keysHeld = new();
        private AudioEngine audioEngine;


        public static readonly DependencyProperty OscOneGainProperty =
            DependencyProperty.Register(nameof(OscOneGain), typeof(double), typeof(MainWindow),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => OnGainChanged(d, e, 0)));

        public double OscOneGain
        {
            get => (double)GetValue(OscOneGainProperty);
            set { SetValue(OscOneGainProperty, value); audioEngine.SetOscillatorGain(0, value); }
        }
        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
            audioEngine = AudioEngine.GetInstance();
            audioEngine.Play();
            audioEngine.SetOscillatorGain(0, OscOneGain);

        }

        private static void OnGainChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, int osc)
        {
            var window = d as MainWindow;
            window.audioEngine?.SetOscillatorGain(osc, (double)e.NewValue);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keysHeld.Contains(e.Key))
            {
                audioEngine.NoteDown(e.Key);
                keysHeld.Add(e.Key);
            }


        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            keysHeld.Remove(e.Key);

            audioEngine.NoteUp(e.Key);

        }




    }
}
