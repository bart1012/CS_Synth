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

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = _mixer.Read(buffer, offset, count);
            if (samplesRead == 0) return 0;

            double frequency = 5.0;
            double sampleRate = 44100.0;
            double phaseIncrement = (Math.PI * 2 * frequency) / sampleRate;

            for (int i = 0; i < samplesRead; i++)
            {
                double tremoloValue = 1 + 0.5 * Math.Sin(phase);
                buffer[offset + i] *= (float)tremoloValue;

                phase += phaseIncrement;
                if (phase > Math.PI * 2) phase -= Math.PI * 2;
            }

            return samplesRead;

        }
    }
}
