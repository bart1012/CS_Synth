using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    public class MappedSignalGenerator : SignalGenerator, ISampleProvider
    {
        public Key Key { get; set; }
        private int nSample;
        private readonly Random random = new Random();
        private readonly double[] pinkNoiseBuffer = new double[7];
        private double phi = 0.0;
        private double tremoloDepth;
        private double tremoloFrequency;




        public MappedSignalGenerator(Key e, SignalGeneratorType type, float gain, int octave)
        {
            if (octave < 1 || octave > 3) throw new ArgumentException("Octave must be between 1 and 3");
            Key = e;
            float baseFrequency = e switch
            {
                Key.A => 130.81f,  // C3
                Key.W => 138.59f,  // C#3
                Key.S => 146.83f,  // D3
                Key.E => 155.56f,  // D#3
                Key.D => 164.81f,  // E3
                Key.F => 174.61f,  // F3
                Key.T => 185.00f,  // F#3
                Key.G => 196.00f,  // G3
                Key.Y => 207.65f,  // G#3
                Key.H => 220.00f,  // A3
                Key.U => 233.08f,  // A#3
                Key.J => 246.94f,  // B3
                Key.K => 261.63f,  // C4
                _ => 130.81f       // Default to C3
            };
            Frequency = baseFrequency * MathF.Pow(2, octave - 1);
            Type = type;
            Gain = 1.0;


        }

        public new int Read(float[] buffer, int offset, int count)
        {


            int num = offset;
            for (int i = 0; i < count / 2; i++)
            {
                double amplitude;
                switch (Type)
                {
                    case SignalGeneratorType.Sin:
                        {
                            double angularFrequency = Math.PI * 2 * Frequency / 44100;
                            amplitude = Gain * Math.Sin(angularFrequency * nSample);
                            amplitude *= CalculateTremoloSignal();
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.Square:
                        {
                            double angularFrequency = 2.0 * Frequency / (double)44100;
                            double num7 = (double)nSample * angularFrequency % 2.0 - 1.0;
                            amplitude = ((num7 >= 0.0) ? Gain : (0.0 - Gain));
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.Triangle:
                        {
                            double angularFrequency = 2.0 * Frequency / (double)44100;
                            double num7 = (double)nSample * angularFrequency % 2.0;
                            amplitude = 2.0 * num7;
                            if (amplitude > 1.0)
                            {
                                amplitude = 2.0 - amplitude;
                            }

                            if (amplitude < -1.0)
                            {
                                amplitude = -2.0 - amplitude;
                            }

                            amplitude *= Gain;
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.SawTooth:
                        {
                            double angularFrequency = 2.0 * Frequency / (double)44100;
                            double num7 = (double)nSample * angularFrequency % 2.0 - 1.0;
                            amplitude = Gain * num7;
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.White:
                        amplitude = Gain * NextRandomTwo();
                        break;
                    case SignalGeneratorType.Pink:
                        {
                            double num5 = NextRandomTwo();
                            pinkNoiseBuffer[0] = 0.99886 * pinkNoiseBuffer[0] + num5 * 0.0555179;
                            pinkNoiseBuffer[1] = 0.99332 * pinkNoiseBuffer[1] + num5 * 0.0750759;
                            pinkNoiseBuffer[2] = 0.969 * pinkNoiseBuffer[2] + num5 * 0.153852;
                            pinkNoiseBuffer[3] = 0.8665 * pinkNoiseBuffer[3] + num5 * 0.3104856;
                            pinkNoiseBuffer[4] = 0.55 * pinkNoiseBuffer[4] + num5 * 0.5329522;
                            pinkNoiseBuffer[5] = -0.7616 * pinkNoiseBuffer[5] - num5 * 0.016898;
                            double num6 = pinkNoiseBuffer[0] + pinkNoiseBuffer[1] + pinkNoiseBuffer[2] + pinkNoiseBuffer[3] + pinkNoiseBuffer[4] + pinkNoiseBuffer[5] + pinkNoiseBuffer[6] + num5 * 0.5362;
                            pinkNoiseBuffer[6] = num5 * 0.115926;
                            amplitude = Gain * (num6 / 5.0);
                            break;
                        }
                    case SignalGeneratorType.Sweep:
                        {
                            double num3 = Math.Exp(FrequencyLog + (double)nSample * (FrequencyEndLog - FrequencyLog) / (SweepLengthSecs * (double)44100));
                            double angularFrequency = Math.PI * 2.0 * num3 / (double)44100;
                            phi += angularFrequency;
                            amplitude = Gain * Math.Sin(phi);
                            nSample++;
                            if ((double)nSample > SweepLengthSecs * (double)44100)
                            {
                                nSample = 0;
                                phi = 0.0;
                            }

                            break;
                        }
                    default:
                        amplitude = 0.0;
                        break;
                }

                for (int j = 0; j < 2; j++)
                {
                    if (PhaseReverse[j])
                    {
                        buffer[num++] = (float)(0.0 - amplitude);
                    }
                    else
                    {
                        buffer[num++] = (float)amplitude;
                    }
                }
            }
            return count;
        }

        private double NextRandomTwo()
        {
            return 2.0 * random.NextDouble() - 1.0;
        }

        public void AddTremolo(double depth, double frequency)
        {
            tremoloDepth = depth;
            tremoloFrequency = frequency;
        }
        private double CalculateTremoloSignal()
        {
            double tremoloSignal = 1 + tremoloDepth * Math.Sin((Math.PI * 2 / 44100) * tremoloFrequency * nSample);
            return tremoloSignal;
        }


    }
}
