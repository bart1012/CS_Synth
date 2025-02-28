using NAudio.Dsp;
using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    interface IBuilder
    {
        void AddTremolo(float depth, float frequency);
        MappedSignalGenerator GetSignal();

        void GenerateDefaultSignal(Key key, SignalGeneratorType type, float gain, int octave);
    }

    class SignalBuilder : IBuilder
    {
        private MappedSignalGenerator _signal;
        public void AddTremolo(float depth, float frequency)
        {
            if (_signal is null) throw new NullReferenceException();
            else _signal.AddTremolo(depth, frequency);
        }

        public void AddFilter(BiQuadFilter filter)
        {
            _signal.AddFilter(filter);
        }

        public void GenerateDefaultSignal(Key key, SignalGeneratorType type, float gain, int octave)
        {
            _signal = new MappedSignalGenerator(key, type, gain, octave);
        }

        public MappedSignalGenerator GetSignal()
        {
            return _signal;
        }


    }
}
