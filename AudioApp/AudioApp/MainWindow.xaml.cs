using AudioApp.Models;
using System.Windows;
using System.Windows.Input;


namespace AudioApp
{
    public partial class MainWindow : Window
    {
        private HashSet<Key> keysHeld = new();
        private AudioEngine audioEngine;

        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
            audioEngine = AudioEngine.GetInstance();
            audioEngine.AddOscillator(OscOnePanel.Oscillator);
            audioEngine.AddOscillator(OscTwoPanel.Oscillator);
            audioEngine.AddVolumeEnvelope(VolumeEnvelopePanel.VolumeEnvelope);
            audioEngine.Play();


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //prevent continous firing of key-down event
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
