using NAudio.Dsp;
using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    public class SignalOptions
    {
        public Key Key { get; set; }
        public SignalGeneratorType OscillatorOne_Type { get; set; } = SignalGeneratorType.Sin;
        public float OscillatorOne_Gain { get; set; } = 0.5f;
        public int OscillatorOne_Octave { get; set; } = 2;
        public int OscillatorOne_DetuneAmount { get; set; }

        public SignalGeneratorType OscillatorTwo_Type { get; set; } = SignalGeneratorType.Sin;
        public float OscillatorTwo_Gain { get; set; } = 0.5f;
        public int OscillatorTwo_Octave { get; set; } = 2;
        public int OscillatorTwo_DetuneAmount { get; set; }

        public BiQuadFilter Filter;
        public float TremoloDepth { get; set; } = 0.5f;
        public float TremoloFrequency { get; set; } = 5.0f;
    }
}
