using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioApp.Models
{
    public class Oscillator : ISampleProvider, IAudioNode
    {
        private readonly WaveFormat _waveFormat;
        private int _nSample;

        public SignalGeneratorType Type { get; set; }
        public double Frequency { get; set; }
        public double Gain { get; set; }
        public double Detune { get; set; } = 0.0;
        public double Shape { get; set; } = 0.0;
        public double PulseWidth { get; set; } = 0.5;
        public int Unison { get; set; } = 1;
        public double UnisonSpread { get; set; } = 0.02;



        public WaveFormat WaveFormat => _waveFormat;

        public Oscillator(WaveFormat format)
        {
            _waveFormat = format;
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            _nSample = 0;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int startPosition = offset;

            for (int i = 0; i < count / _waveFormat.Channels; i++)
            {
                double nSampleValue;

                switch (Type)
                {
                    case SignalGeneratorType.Sin:
                        {
                            double angularFreq = Math.PI * 2.0 * Frequency / (double)_waveFormat.SampleRate;
                            nSampleValue = Gain * Math.Sin((double)_nSample * angularFreq);
                            _nSample++;
                            break;
                        }
                    case SignalGeneratorType.Square:
                        {
                            double angularFreq = 2.0 * Frequency / (double)_waveFormat.SampleRate;
                            double normalizedPhase = (double)_nSample * angularFreq % 2.0 - 1.0;
                            nSampleValue = ((normalizedPhase >= 0.0) ? Gain : (0.0 - Gain));
                            _nSample++;
                            break;
                        }
                    case SignalGeneratorType.Triangle:
                        {
                            double angularFreq = 2.0 * Frequency / (double)_waveFormat.SampleRate;
                            double normalizedPhase = (double)_nSample * angularFreq % 2.0;
                            nSampleValue = 2.0 * normalizedPhase;
                            if (nSampleValue > 1.0)
                            {
                                nSampleValue = 2.0 - nSampleValue;
                            }

                            if (nSampleValue < -1.0)
                            {
                                nSampleValue = -2.0 - nSampleValue;
                            }

                            nSampleValue *= Gain;
                            _nSample++;
                            break;
                        }
                    case SignalGeneratorType.SawTooth:
                        {
                            double angularFreq = 2.0 * Frequency / (double)_waveFormat.SampleRate;
                            double normalizedPhase = (double)_nSample * angularFreq % 2.0 - 1.0;
                            nSampleValue = Gain * normalizedPhase;
                            _nSample++;
                            break;
                        }

                    default:
                        nSampleValue = 0.0;
                        break;
                }

                for (int j = 0; j < _waveFormat.Channels; j++)
                {

                    buffer[startPosition++] = (float)nSampleValue;

                }
            }
            return count;
        }
    }
}
