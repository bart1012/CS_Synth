using AudioApp.Models;
using System.Windows;

namespace AudioApp.Panels
{
    /// <summary>
    /// Interaction logic for OscillatorPanel.xaml
    /// </summary>
    public partial class OscillatorPanel : System.Windows.Controls.UserControl
    {
        public Oscillator Oscillator { get; private set; }

        public static readonly DependencyProperty FrameTitleProperty = DependencyProperty.Register(nameof(FrameTitle), typeof(String), typeof(OscillatorPanel));

        public string FrameTitle
        {
            get => (String)GetValue(FrameTitleProperty);
            set => SetValue(FrameTitleProperty, value);
        }

        public OscillatorPanel()
        {
            Oscillator = new Oscillator()
            {
                Gain = 0.0,
                Waveform = NAudio.Wave.SampleProviders.SignalGeneratorType.Sin,
                Octave = 2
            };
            InitializeComponent();
            DataContext = Oscillator;
        }


    }
}
