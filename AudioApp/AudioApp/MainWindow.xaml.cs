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
        private SignalGenerator sine = new SignalGenerator()
        {
            Gain = 0.2,
            Frequency = 500,
            Type = SignalGeneratorType.SawTooth
        };

        private FadeInOutSampleProvider fadeInOutSampleProvider;
        private bool isKeyPressed = false;

        public MainWindow()
        {
            InitializeComponent();
            fadeInOutSampleProvider = new FadeInOutSampleProvider(sine, true);
            wasapiOut.Init(fadeInOutSampleProvider);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case (Key.A): sine.Frequency = 261.63; break;
                case (Key.W): sine.Frequency = 277.18; break;
                case (Key.S): sine.Frequency = 293.66; break;
                case (Key.E): sine.Frequency = 311.13; break;
                case (Key.D): sine.Frequency = 329.63; break;
                case (Key.F): sine.Frequency = 349.23; break;
                case (Key.T): sine.Frequency = 369.99; break;
                case (Key.G): sine.Frequency = 392; break;
                case (Key.Y): sine.Frequency = 415.30; break;
                case (Key.H): sine.Frequency = 440; break;
                case (Key.U): sine.Frequency = 466.16; break;
                case (Key.J): sine.Frequency = 493.88; break;
                case (Key.K): sine.Frequency = 523.25; break;
                default: sine.Frequency = 440; break;
            }
            if (!isKeyPressed)
            {
                fadeInOutSampleProvider.BeginFadeIn(100);
                wasapiOut.Play();
                isKeyPressed = true;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (wasapiOut.PlaybackState == PlaybackState.Playing)
            {
                fadeInOutSampleProvider.BeginFadeOut(100);
            }

            isKeyPressed = false;
        }


        private void wasapiOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            wasapiOut.Stop();
        }



        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbox = sender as ComboBox;
            string selected = cbox.SelectedItem.ToString().Split(": ").Last();
            switch (selected)
            {

                case "Sin": sine.Type = SignalGeneratorType.Sin; break;
                case "Triangle": sine.Type = SignalGeneratorType.Triangle; break;
                case "SawTooth": sine.Type = SignalGeneratorType.SawTooth; break;
                case "Square":
                    sine.Type = SignalGeneratorType.Square; break;
                deafult: sine.Type = SignalGeneratorType.Sin; break;
            }

        }
    }
}
