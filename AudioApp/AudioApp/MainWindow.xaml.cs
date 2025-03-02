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
        private AudioEngine audioEngine;
        private SignalOptions waveOptions;
        private SignalGeneratorType oscillatorOneType;
        private SignalGeneratorType oscillatorTwoType;
        private float oscillatorOneGain;
        private float oscillatorTwoGain;
        private int oscillatorOneOctave;
        private int oscillatorTwoOctave;

        public MainWindow()
        {
            InitializeComponent();
            audioEngine = AudioEngine.GetInstance();
            oscillatorOneType = SignalGeneratorType.Sin;
            oscillatorTwoType = SignalGeneratorType.SawTooth;
            oscillatorOneGain = 0.5f;
            oscillatorTwoGain = 0.5f;
            oscillatorOneOctave = 2;
            oscillatorTwoOctave = 2;
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
                    OscillatorOne_Type = oscillatorOneType,
                    OscillatorOne_Gain = oscillatorOneGain,
                    OscillatorTwo_Type = oscillatorTwoType,
                    OscillatorTwo_Gain = oscillatorTwoGain,
                    TremoloDepth = tremoloDepth,
                    TremoloFrequency = tremoloFrequency,
                    OscillatorOne_Octave = oscillatorOneOctave,
                    OscillatorTwo_Octave = oscillatorTwoOctave
                    //Filter = BiQuadFilter.LowPassFilter(44100, 110, 1.0f)
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

            //audioEngine.Stop();

            isKeyPressed = false;
        }

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cbox && cbox.SelectedItem != null)
            {
                string selected = cbox.SelectedItem.ToString().Split(": ").Last();

                var waveformMap = new Dictionary<string, SignalGeneratorType>
        {
            { "Sin", SignalGeneratorType.Sin },
            { "Triangle", SignalGeneratorType.Triangle },
            { "SawTooth", SignalGeneratorType.SawTooth },
            { "Square", SignalGeneratorType.Square }
        };

                if (waveformMap.TryGetValue(selected, out SignalGeneratorType waveformType))
                {
                    if (cbox.Name == "WaveformTypeOsc1")
                        oscillatorOneType = waveformType;
                    else if (cbox.Name == "WaveformTypeOsc2")
                        oscillatorTwoType = waveformType;
                }
            }

        }

        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            var slider = sender as Slider;
            float value = (float)slider.Value;
            if (slider.Name == "VolumeSliderOsc1") oscillatorOneGain = value;
            else oscillatorTwoGain = value;
        }

        private void OctaveSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            var slider = sender as Slider;
            int value = (int)slider.Value;
            if (slider.Name == "OctaveSliderOsc1") oscillatorOneOctave = value;
            else oscillatorTwoOctave = value;
        }

        private void TremoloFrequencyControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
