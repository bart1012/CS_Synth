using AudioApp.Models;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AudioApp
{
    public partial class MainWindow : Window
    {
        private WasapiOut wasapiOut = new WasapiOut();
        private HashSet<Key> keysHeld = new();
        private bool isKeyPressed = false;
        private MixingSampleProvider mixer;
        private List<SignalGenerator> mixerSignals = new();
        private SignalGeneratorType waveformType = SignalGeneratorType.Sin;
        private AudioEngine audioEngine;

        private float _gain = 0.5f;

        public MainWindow()
        {
            InitializeComponent();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
            {
                ReadFully = true
            };
            wasapiOut.Init(mixer);
            audioEngine = AudioEngine.GetInstance();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keysHeld.Contains(e.Key))
            {
                keysHeld.Add(e.Key);

                float tremoloDepth = TremoloDepthControl.Amount / 100.00f;
                float tremoloFrequency = TremoloFrequencyControl.Amount / 10.00f;
                audioEngine.AddSignalToMix(e.Key, waveformType, _gain, tremoloDepth, tremoloFrequency);

            }


            if (!isKeyPressed)
            {
                audioEngine.Play();
                isKeyPressed = true;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            keysHeld.Remove(e.Key);

            audioEngine.RemoveSignalFromMix(e.Key);


            audioEngine.Stop();

            isKeyPressed = false;
        }

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbox = sender as ComboBox;
            string selected = cbox.SelectedItem.ToString().Split(": ").Last();

            switch (selected)
            {
                case "Sin": waveformType = SignalGeneratorType.Sin; break;
                case "Triangle": waveformType = SignalGeneratorType.Triangle; break;
                case "SawTooth": waveformType = SignalGeneratorType.SawTooth; break;
                case "Square": waveformType = SignalGeneratorType.Square; break;
            }



        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            _gain = (float)slider.Value;
        }







    }
}
