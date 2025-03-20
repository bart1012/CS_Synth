using NAudio.Wave;

namespace AudioApp.Models
{
    public class SynthVoice : ISampleProvider
    {
        private WaveFormat _waveFormat;
        public List<Oscillator> Oscillators;
        public ADSREnvelope ADSREnvelope;
        public Filter Filter;

        public WaveFormat WaveFormat => _waveFormat;

        public SynthVoice(WaveFormat waveFormat)
        {
            _waveFormat = waveFormat;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            MixOscillators(buffer, offset, count);
            ApplyFilter(buffer, offset, count);
            ApplyEnvelope(buffer, offset, count);
            return count;
        }

        private void MixOscillators(float[] buffer, int offset, int count)
        {
            //mix oscillators and add to buffer
        }

        private void ApplyFilter(float[] buffer, int offset, int count)
        {
            //apply filter to buffer
        }

        private void ApplyEnvelope(float[] buffer, int offset, int count)
        {
            //apply envelope to buffer
        }
    }
}
