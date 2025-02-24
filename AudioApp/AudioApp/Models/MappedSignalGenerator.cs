

using System.Windows.Input;

namespace NAudio.Wave.SampleProviders;
public class MappedSignalGenerator : ISampleProvider
{
    private readonly WaveFormat waveFormat;

    private readonly Random random = new Random();

    private readonly double[] pinkNoiseBuffer = new double[7];

    private const double TwoPi = Math.PI * 2.0;

    private int nSample;

    private double phi;

    public Key Key { get; set; }

    public MappedSignalGenerator(Key e, SignalGeneratorType type, float gain)
    {
        phi = 0.0;
        waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
        PhaseReverse = new bool[2];
        SweepLengthSecs = 2.0;
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

    //
    // Summary:
    //     The waveformat of this WaveProvider (same as the source)
    public WaveFormat WaveFormat => waveFormat;

    //
    // Summary:
    //     Frequency for the Generator. (20.0 - 20000.0 Hz) Sin, Square, Triangle, SawTooth,
    //     Sweep (Start Frequency).
    public double Frequency { get; set; }

    //
    // Summary:
    //     Return Log of Frequency Start (Read only)
    public double FrequencyLog => Math.Log(Frequency);

    //
    // Summary:
    //     End Frequency for the Sweep Generator. (Start Frequency in Frequency)
    public double FrequencyEnd { get; set; }

    //
    // Summary:
    //     Return Log of Frequency End (Read only)
    public double FrequencyEndLog => Math.Log(FrequencyEnd);

    //
    // Summary:
    //     Gain for the Generator. (0.0 to 1.0)
    public double Gain { get; set; }

    //
    // Summary:
    //     Channel PhaseReverse
    public bool[] PhaseReverse { get; }

    //
    // Summary:
    //     Type of Generator.
    public SignalGeneratorType Type { get; set; }

    //
    // Summary:
    //     Length Seconds for the Sweep Generator.
    public double SweepLengthSecs { get; set; }

    //
    // Summary:
    //     Initializes a new instance for the Generator (Default :: 44.1Khz, 2 channels,
    //     Sinus, Frequency = 440, Gain = 1)
    public MappedSignalGenerator()
        : this(44100, 2)
    {
    }

    //
    // Summary:
    //     Initializes a new instance for the Generator (UserDef SampleRate & Channels)
    //
    //
    // Parameters:
    //   sampleRate:
    //     Desired sample rate
    //
    //   channel:
    //     Number of channels
    public MappedSignalGenerator(int sampleRate, int channel)
    {
        phi = 0.0;
        waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channel);
        Type = SignalGeneratorType.Sin;
        Frequency = 440.0;
        Gain = 1.0;
        PhaseReverse = new bool[channel];
        SweepLengthSecs = 2.0;
    }

    //
    // Summary:
    //     Reads from this provider.
    public int Read(float[] buffer, int offset, int count)
    {
        int num = offset;
        for (int i = 0; i < count / waveFormat.Channels; i++)
        {
            double num2;
            switch (Type)
            {
                case SignalGeneratorType.Sin:
                    {
                        double num4 = Math.PI * 2.0 * Frequency / (double)waveFormat.SampleRate;
                        num2 = Gain * Math.Sin((double)nSample * num4);
                        nSample++;
                        break;
                    }
                case SignalGeneratorType.Square:
                    {
                        double num4 = 2.0 * Frequency / (double)waveFormat.SampleRate;
                        double num7 = (double)nSample * num4 % 2.0 - 1.0;
                        num2 = ((num7 >= 0.0) ? Gain : (0.0 - Gain));
                        nSample++;
                        break;
                    }
                case SignalGeneratorType.Triangle:
                    {
                        double num4 = 2.0 * Frequency / (double)waveFormat.SampleRate;
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
                        double num4 = 2.0 * Frequency / (double)waveFormat.SampleRate;
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
                        double num3 = Math.Exp(FrequencyLog + (double)nSample * (FrequencyEndLog - FrequencyLog) / (SweepLengthSecs * (double)waveFormat.SampleRate));
                        double num4 = Math.PI * 2.0 * num3 / (double)waveFormat.SampleRate;
                        phi += num4;
                        num2 = Gain * Math.Sin(phi);
                        nSample++;
                        if ((double)nSample > SweepLengthSecs * (double)waveFormat.SampleRate)
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

            for (int j = 0; j < waveFormat.Channels; j++)
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

    //
    // Summary:
    //     Private :: Random for WhiteNoise & Pink Noise (Value form -1 to 1)
    //
    // Returns:
    //     Random value from -1 to +1
    private double NextRandomTwo()
    {
        return 2.0 * random.NextDouble() - 1.0;
    }
}

