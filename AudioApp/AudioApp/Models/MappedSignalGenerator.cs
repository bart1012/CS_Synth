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



        public MappedSignalGenerator(Key e, SignalGeneratorType type, float gain)
        {
            Key = e;
            Frequency = e switch
            {
                Key.A => 261.63f,  // Middle C (C4)
                Key.W => 277.18f,  // C#4
                Key.S => 293.66f,  // D4
                Key.E => 311.13f,  // D#4
                Key.D => 329.63f,  // E4
                Key.F => 349.23f,  // F4
                Key.T => 369.99f,  // F#4
                Key.G => 392.00f,  // G4
                Key.Y => 415.30f,  // G#4
                Key.H => 440.00f,  // A4 (A4 is often used as the tuning standard)
                Key.U => 466.16f,  // A#4
                Key.J => 493.88f,  // B4
                Key.K => 523.25f,  // C5
                _ => 261.63f // C4
            };
            Type = type;
            Gain = gain;
        }

        public new int Read(float[] buffer, int offset, int count)
        {

            int num = offset;
            for (int i = 0; i < count / 2; i++)
            {
                double num2;
                switch (Type)
                {
                    case SignalGeneratorType.Sin:
                        {
                            double num4 = Math.PI * 2.0 * Frequency / (double)44100;
                            num2 = Gain * Math.Sin((double)nSample * num4);
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.Square:
                        {
                            double num4 = 2.0 * Frequency / (double)44100;
                            double num7 = (double)nSample * num4 % 2.0 - 1.0;
                            num2 = ((num7 >= 0.0) ? Gain : (0.0 - Gain));
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.Triangle:
                        {
                            double num4 = 2.0 * Frequency / (double)44100;
                            double num7 = (double)nSample * num4 % 2.0;
                            num2 = 2.0 * num7;
                            if (num2 > 1.0)
                            {
                                num2 = 2.0 - num2;
                            }

                            if (num2 < -1.0)
                            {
                                num2 = -2.0 - num2;
                            }

                            num2 *= Gain;
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.SawTooth:
                        {
                            double num4 = 2.0 * Frequency / (double)44100;
                            double num7 = (double)nSample * num4 % 2.0 - 1.0;
                            num2 = Gain * num7;
                            nSample++;
                            break;
                        }
                    case SignalGeneratorType.White:
                        num2 = Gain * NextRandomTwo();
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
                            num2 = Gain * (num6 / 5.0);
                            break;
                        }
                    case SignalGeneratorType.Sweep:
                        {
                            double num3 = Math.Exp(FrequencyLog + (double)nSample * (FrequencyEndLog - FrequencyLog) / (SweepLengthSecs * (double)44100));
                            double num4 = Math.PI * 2.0 * num3 / (double)44100;
                            phi += num4;
                            num2 = Gain * Math.Sin(phi);
                            nSample++;
                            if ((double)nSample > SweepLengthSecs * (double)44100)
                            {
                                nSample = 0;
                                phi = 0.0;
                            }

                            break;
                        }
                    default:
                        num2 = 0.0;
                        break;
                }

                for (int j = 0; j < 2; j++)
                {
                    if (PhaseReverse[j])
                    {
                        buffer[num++] = (float)(0.0 - num2);
                    }
                    else
                    {
                        buffer[num++] = (float)num2;
                    }
                }
            }
            return count;
        }

        private double NextRandomTwo()
        {
            return 2.0 * random.NextDouble() - 1.0;
        }



    }
}
