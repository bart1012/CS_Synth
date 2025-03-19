using NAudio.Dsp;
using NAudio.Wave;

namespace AudioApp.Models
{
    public class FilterWrapper : ISampleProvider
    {
        private ISampleProvider _source;
        private BiQuadFilter filter;
        public FilterWrapper(ISampleProvider src)
        {
            _source = src;
            filter = BiQuadFilter.HighPassFilter(44100, 185, 5);

        }

        public WaveFormat WaveFormat => _source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = _source.Read(buffer, offset, count);
            for (int i = 0; i < samplesRead; i++)
            {
                buffer[i + offset] = filter.Transform(buffer[i + offset]);
            }
            return samplesRead;
        }
    }
}
