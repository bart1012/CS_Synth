using AudioApp.Models;
using NAudio.Wave.SampleProviders;
using System.Windows;
using System.Windows.Input;


namespace AudioApp
{
    public partial class MainWindow : Window
    {
        private HashSet<Key> keysHeld = new();
        private AudioEngine audioEngine;
        private SignalGeneratorType oscillatorOneType;
        private double oscillatorOneGain;
        private int oscillatorOneOctave;

        public MainWindow()
        {
            InitializeComponent();
            audioEngine = AudioEngine.GetInstance();
            audioEngine.Play();
            audioEngine.SetOscillatorGain(0, 0.0);
            OscOnePanel.GainChanged += (amount) =>
            {
                audioEngine.SetOscillatorGain(0, amount);
            };
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
