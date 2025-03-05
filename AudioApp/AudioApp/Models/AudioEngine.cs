using AudioApp.Service;
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
        private Dictionary<Key, double> _frequencyMappings;
        private List<OscillatorSettings> _oscillators = new();
        private Dictionary<Key, ADSREnvelopeProvider> _activeNotes = new();
        private EffectsProcessor effectsProcessor;

        private AudioEngine()
        {
            _wasapiOut = new();
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
            {
                ReadFully = true
            };
            _oscillators.Add(new OscillatorSettings
            {
                Type = SignalGeneratorType.Sin,
                Gain = 0.5,
                Octave = 1,
                Pan = 0
            });
            _frequencyMappings = new Dictionary<Key, double>
            {
                { Key.A,  130.81 },  // C3
                { Key.W,  138.59 },  // C#3
                { Key.S,  146.83 },  // D3
                { Key.E,  155.56 },  // D#3
                { Key.D,  164.81 },  // E3
                { Key.F,  174.61 },  // F3
                { Key.T,  185.00 },  // F#3
                { Key.G,  196.00 },  // G3
                { Key.Y,  207.65 },  // G#3
                { Key.H,  220.00 },  // A3
                { Key.U,  233.08 },  // A#3
                { Key.J,  246.94 },  // B3
                { Key.K,  261.63 }   // C4
            };
            effectsProcessor = new(_mixer);
            effectsProcessor.AddEffect(new AmplitudeModulation(2.0, 0.5));
            _wasapiOut.Init(effectsProcessor);
        }

        public static AudioEngine GetInstance()
        {
            if (_instance is null)
            {
                _instance = new AudioEngine();
            }
            return _instance;
        }
        private SignalGenerator GenerateBaseSignal(Key key, OscillatorSettings options)
        {
            return new SignalGenerator()
            {
                Frequency = _frequencyMappings[key] * Math.Pow(2, options.Octave),
                Type = options.Type,
                Gain = options.Gain
            };

        }
        private ISampleProvider ApplyADSR(ISampleProvider source)
        {
            return new ADSREnvelopeProvider(source.ToMono())
            {
                AttackSeconds = 0.05f,
                ReleaseSeconds = 2.0f
            };
        }

        public void NoteDown(Key key)
        {

            if (!_frequencyMappings.ContainsKey(key)) return;
            SignalGenerator baseSignal = GenerateBaseSignal(key, _oscillators[0]);
            Console.WriteLine((float)baseSignal.Frequency);
            ADSREnvelopeProvider signal = (ADSREnvelopeProvider)ApplyADSR(baseSignal);
            _activeNotes[key] = signal;
            //_mixer.AddMixerInput(signal.ToStereo());
            effectsProcessor.AddToMix(signal.ToStereo());
            signal.Start();
        }

        public void NoteUp(Key key)
        {
            if (!_frequencyMappings.ContainsKey(key)) return;
            _activeNotes[key].Stop();
        }

        public void SetOscillatorType(int targetOscillator, SignalGeneratorType type)
        {
            if (targetOscillator < 0 || targetOscillator > _oscillators.Count() - 1) throw new InvalidOperationException("Oscillator not found.");
            _oscillators[targetOscillator].Type = type;
        }

        public void SetOscillatorGain(int targetOscillator, double gain)
        {
            if (targetOscillator < 0 || targetOscillator > _oscillators.Count() - 1) throw new InvalidOperationException("Oscillator not found.");
            _oscillators[targetOscillator].Gain = gain;
        }

        public void SetOscillatorOctave(int targetOscillator, int octave)
        {
            if (targetOscillator < 0 || targetOscillator > _oscillators.Count() - 1) throw new InvalidOperationException("Oscillator not found.");
            _oscillators[targetOscillator].Octave = octave;
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
