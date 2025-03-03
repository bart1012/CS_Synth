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
        private AudioEngine audioEngine;
        private SignalGeneratorType oscillatorOneType;
        private float oscillatorOneGain;
        private int oscillatorOneOctave;

        public MainWindow()
        {
            InitializeComponent();
            audioEngine = AudioEngine.GetInstance();
            audioEngine.Play();
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

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (audioEngine is null) return;
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

                audioEngine.SetOscillatorType(0, waveformMap[selected]);
            }

        }

        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (audioEngine is null) return;

            var slider = sender as Slider;
            float value = (float)slider.Value;
            audioEngine.SetOscillatorGain(0, value);
        }

        private void OctaveSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (audioEngine is null) return;

            var slider = sender as Slider;
            int value = (int)slider.Value;
            audioEngine.SetOscillatorOctave(0, value);
        }


    }
}
