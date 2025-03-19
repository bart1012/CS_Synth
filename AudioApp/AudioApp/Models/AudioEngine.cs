using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    public class AudioEngine
    {
        private static AudioEngine? _instance;
        private WasapiOut _wasapiOut;
        private VolumeEnvelopeWrapper _volumeEnvelope;
        private MixingSampleProvider _mixer;
        private Dictionary<Key, double> _frequencyMappings;
        private Oscillator[] _oscillators = new Oscillator[2];
        private Dictionary<Key, List<ADSREnvelopeProvider>> _activeNotes = new();
        private EffectsProcessor effectsProcessor;

        private AudioEngine()
        {
            _wasapiOut = new();
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
            {
                ReadFully = true
            };
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
        private SignalGenerator GenerateBaseSignal(Key key, Oscillator osc)
        {
            return new SignalGenerator()
            {
                Frequency = (_frequencyMappings[key] * Math.Pow(2, osc.Octave)) * Math.Pow(2, (osc.Detune * 100) / 1200),
                Type = osc.Waveform,
                Gain = osc.Gain
            };

        }
        private ISampleProvider ApplyADSR(ISampleProvider source)
        {
            return _volumeEnvelope is null ? new ADSREnvelopeProvider(source.ToMono()) : new ADSREnvelopeProvider(source.ToMono(), _volumeEnvelope);

        }
        public void NoteDown(Key key)
        {

            if (!_frequencyMappings.ContainsKey(key)) return;
            if (!_activeNotes.ContainsKey(key)) _activeNotes[key] = new(2);

            for (int i = 0; i < 2; i++)
            {
                SignalGenerator baseSignal = GenerateBaseSignal(key, _oscillators[i]);
                ADSREnvelopeProvider signal = (ADSREnvelopeProvider)ApplyADSR(baseSignal);

                if (_activeNotes[key].Count() < 2)
                {
                    _activeNotes[key].Add(signal);
                }
                else
                {
                    _activeNotes[key][i] = signal;
                }

                //_mixer.AddMixerInput(signal.ToStereo());
                effectsProcessor.AddToMix(signal.ToStereo());
                signal.Start();
            }



        }
        public void NoteUp(Key key)
        {
            if (!_frequencyMappings.ContainsKey(key)) return;
            foreach (var signal in _activeNotes[key])
            {
                signal.Stop();
            }
        }

        public void AddVolumeEnvelope(VolumeEnvelopeWrapper env)
        {
            _volumeEnvelope = env;
        }
        public void AddOscillator(Oscillator osc)
        {
            if (_oscillators[0] != null && _oscillators[1] != null) throw new InvalidOperationException("You cannot add more than 2 oscillators.");
            if (_oscillators[0] is null) _oscillators[0] = osc;
            else _oscillators[1] = osc;
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
