using NAudio.Wave.SampleProviders;
using System.Windows;
using System.Windows.Controls;

namespace AudioApp.Controls
{
    /// <summary>
    /// Interaction logic for WaveformSelectorControl.xaml
    /// </summary>
    public partial class WaveformSelectorControl : UserControl
    {
        private static int _elementCounter = 0;

        public static readonly DependencyProperty WaveformProperty = DependencyProperty.Register(nameof(Waveform), typeof(SignalGeneratorType), typeof(WaveformSelectorControl), new FrameworkPropertyMetadata(SignalGeneratorType.Sin, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => Console.WriteLine("Wave changed")));

        public string GroupName;
        public SignalGeneratorType Waveform
        {
            get => (SignalGeneratorType)GetValue(WaveformProperty);
            set => SetValue(WaveformProperty, value);
        }


        public WaveformSelectorControl()
        {
            InitializeComponent();
            _elementCounter++;
            GroupName = "Osc" + _elementCounter;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton button)
            {
                var content = button.ToString().Split(':')[1].Split(' ')[0];
                Waveform = content switch
                {
                    "Si" => SignalGeneratorType.Sin,
                    "Sq" => SignalGeneratorType.Square,
                    "Tr" => SignalGeneratorType.Triangle,
                    "Sa" => SignalGeneratorType.SawTooth,
                    _ => throw new NotImplementedException()
                };
            }
        }
    }
}
