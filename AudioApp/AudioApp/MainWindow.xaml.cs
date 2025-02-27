using AudioApp.Models;
using NAudio.Wave.SampleProviders;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AudioApp
{
    public partial class MainWindow : Window
    {
        private HashSet<Key> keysHeld = new();
        private bool isKeyPressed = false;
        private SignalGeneratorType waveformType = SignalGeneratorType.Sin;
        private AudioEngine audioEngine;
        private float _gain = 0.5f;

        public MainWindow()
        {
            InitializeComponent();
            audioEngine = AudioEngine.GetInstance();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keysHeld.Contains(e.Key))
            {
                keysHeld.Add(e.Key);

                float tremoloDepth = TremoloDepthControl.Amount / 100.00f;
                float tremoloFrequency = TremoloFrequencyControl.Amount / 10.00f;
                var options = new SignalOptions()
                {
                    Key = e.Key,
                    Type = waveformType,
                    Gain = _gain,
                    TremoloDepth = tremoloDepth,
                    TremoloFrequency = tremoloFrequency,

                };

                audioEngine.AddSignalToMix(options);

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
