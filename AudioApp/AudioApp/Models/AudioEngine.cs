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

        //Key key, SignalGeneratorType type = SignalGeneratorType.Sin, float gain = 0.5f, float tremoloDepth = 0.5f, float tremoloFrequency = 5.0f)

        public void AddSignalToMix(SignalOptions options)
        {
            var signalBuilder = new SignalBuilder();
            signalBuilder.GenerateDefaultSignal(options.Key, options.Type, options.Gain, options.Octave);
            signalBuilder.AddTremolo(options.TremoloDepth, options.TremoloFrequency);
            var generatedSignal = signalBuilder.GetSignal();

            _mixer.AddMixerInput(generatedSignal);
        }

        public void RemoveSignalFromMix(Key key)
        {
            var inputToRemove = _mixer.MixerInputs
             .FirstOrDefault(i => i is MappedSignalGenerator mappedGen && mappedGen.Key == key);

            if (inputToRemove != null)
            {
                _mixer.RemoveMixerInput(inputToRemove);
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
