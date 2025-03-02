using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    public class ADSRWrapper : ISampleProvider
    {
        public Key Key;
        private MappedSignalGenerator _source;
        private AdsrSampleProvider _envelope;


        public ADSRWrapper(MappedSignalGenerator source)
        {
            _source = source;
            Key = source.Key;
            _envelope = new AdsrSampleProvider(source.ToMono())
            {
                AttackSeconds = 2.0f,
                ReleaseSeconds = 2.0f
            };
        }

        public WaveFormat WaveFormat => _source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            return _envelope.Read(buffer, offset, count);
        }

        public void Stop()
        {
            _envelope.Stop();
        }
    }
}
