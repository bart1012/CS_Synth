using NAudio.Wave.SampleProviders;

namespace AudioApp.Models
{
    public class OscillatorSettings
    {
        public SignalGeneratorType Type { get; set; }
        public double Gain { get; set; }
        public int Octave { get; set; }
    }
}
