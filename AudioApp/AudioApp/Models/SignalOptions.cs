using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    public class SignalOptions
    {
        public Key Key { get; set; }
        public SignalGeneratorType Type { get; set; } = SignalGeneratorType.Sin;
        public float Gain { get; set; } = 0.5f;
        public float TremoloDepth { get; set; } = 0.5f;
        public float TremoloFrequency { get; set; } = 5.0f;
        public int Octave { get; set; } = 2;
    }
}
