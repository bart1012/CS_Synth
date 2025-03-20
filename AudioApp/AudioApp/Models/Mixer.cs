using NAudio.Utils;
using NAudio.Wave;

namespace AudioApp.Models
{
    public class Mixer : ISampleProvider
    {
        private List<SynthVoice> _voices;

        private const int _maxVoices = 1024;

        private float[] sourceBuffer;
        public WaveFormat WaveFormat { get; private set; }
        public IEnumerable<ISampleProvider> VoiceInputs => _voices;
        public Mixer(WaveFormat waveFormat)
        {
            WaveFormat = waveFormat;
            _voices = new(_maxVoices);
        }

        public void AddVoice(SynthVoice voice)
        {
            //ensure sufficient capacity, uniform waveformat samplerate, channels etc
            _voices.Add(voice);
        }

        public void RemoveVoice(SynthVoice voice)
        {
            _voices.Remove(voice);
        }

        public void RemoveAllVoices()
        {
            _voices.Clear();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int maxSamplesRead = 0;
            sourceBuffer = BufferHelpers.Ensure(sourceBuffer, count);
            for (int currentVoice = 0; currentVoice < _voices.Count; currentVoice++)
            {
                SynthVoice nVoice = _voices[currentVoice];
                int samplesRead = nVoice.Read(sourceBuffer, offset, count);
                int offsetTargetSample = offset;
                for (int nsample = 0; nsample < samplesRead; nsample++)
                {
                    if (nsample >= maxSamplesRead) buffer[offsetTargetSample++] = sourceBuffer[nsample];
                    else buffer[offsetTargetSample++] += sourceBuffer[nsample];
                }
                maxSamplesRead = Math.Max(samplesRead, maxSamplesRead);
                if (samplesRead < count)
                {
                    int lastSamplePosition = offset + samplesRead;
                    while (lastSamplePosition < offset + count)
                    {
                        buffer[lastSamplePosition++] = 0f;
                    }
                    maxSamplesRead = Math.Max(maxSamplesRead, samplesRead);
                }
            }
            return maxSamplesRead;

        }
    }
}
