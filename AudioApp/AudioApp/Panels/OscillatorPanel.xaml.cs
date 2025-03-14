using AudioApp.Models;

namespace AudioApp.Panels
{
    /// <summary>
    /// Interaction logic for OscillatorPanel.xaml
    /// </summary>
    public partial class OscillatorPanel : System.Windows.Controls.UserControl
    {
        public Oscillator Oscillator { get; private set; }

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
