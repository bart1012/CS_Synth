using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioApp.Service
{
    public class EffectsProcessor : ISampleProvider
    {
        private MixingSampleProvider _mixer { get; set; }
        private List<IAudioEffect> _effects { get; set; }

        private double phase = 0;

        public WaveFormat WaveFormat => _mixer.WaveFormat;

        public EffectsProcessor(MixingSampleProvider mixer)
        {
            _mixer = mixer;
            _effects = new();
        }

        public void AddToMix(ISampleProvider signal)
        {
            _mixer.AddMixerInput(signal);
        }

        public void RemoveFromMix(ISampleProvider signal)
        {
            _mixer.RemoveMixerInput(signal);
        }

        public void RemoveAllSignals()
        {
            _mixer.RemoveAllMixerInputs();
        }

        public void AddEffect(IAudioEffect effect)
        {
            _effects.Add(effect);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = _mixer.Read(buffer, offset, count);
            if (samplesRead == 0) return 0;
            else if (_effects.Count == 0) return samplesRead;
            _effects.ForEach(effect =>
            {
                effect.Process(buffer, samplesRead);
            });

            return samplesRead;

        }
    }
}
