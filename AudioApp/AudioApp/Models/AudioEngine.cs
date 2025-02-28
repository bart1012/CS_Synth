using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    public class AudioEngine
    {
        private static AudioEngine? _instance;
        private WasapiOut _wasapiOut;
        private MixingSampleProvider _mixer;
        private List<SignalGenerator> _mixerSignals;
        private AudioEngine()
        {
            _wasapiOut = new();
            _mixerSignals = new();
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
            {
                ReadFully = true
            };
            _wasapiOut.Init(_mixer);
        }

        public static AudioEngine GetInstance()
        {
            if (_instance is null)
            {
                _instance = new AudioEngine();
            }
            return _instance;
        }


        public void AddSignalToMix(SignalOptions options)
        {
            var signalBuilder = new SignalBuilder();
            signalBuilder.GenerateDefaultSignal(options.Key, options.OscillatorOne_Type, options.OscillatorOne_Gain, options.OscillatorOne_Octave);
            signalBuilder.AddTremolo(options.TremoloDepth, options.TremoloFrequency);
            if (options.Filter != null)
            {
                signalBuilder.AddFilter(options.Filter);
            }
            var signalOne = signalBuilder.GetSignal();

            var signalBuilder2 = new SignalBuilder();
            signalBuilder2.GenerateDefaultSignal(options.Key, options.OscillatorTwo_Type, options.OscillatorTwo_Gain, options.OscillatorTwo_Octave);
            signalBuilder2.AddTremolo(options.TremoloDepth, options.TremoloFrequency);
            if (options.Filter != null)
            {
                signalBuilder2.AddFilter(options.Filter);
            }
            var signalTwo = signalBuilder2.GetSignal();

            _mixer.AddMixerInput(signalOne);
            _mixer.AddMixerInput(signalTwo);
        }

        public void RemoveSignalFromMix(Key key)
        {
            foreach (var signal in _mixer.MixerInputs.ToList())
            {
                if (signal is MappedSignalGenerator signalOsc && signalOsc.Key == key)
                {
                    _mixer.RemoveMixerInput(signal);
                }
            }

        }

        public void Play()
        {
            _wasapiOut.Play();
        }

        public void Stop()
        {
            if (_wasapiOut.PlaybackState == PlaybackState.Playing)
            {

                _wasapiOut.Stop();
            }
        }
    }
}
