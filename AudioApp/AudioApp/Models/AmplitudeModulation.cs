namespace AudioApp.Models
{
    public class AmplitudeModulation : IAudioEffect
    {
        private double _frequency;
        private double _depth;
        private readonly double _sampleRate = 44100.0;
        private double _phase = 0;

        public AmplitudeModulation(double frequency = 5.0, double depth = 0.5)
        {
            _frequency = frequency;
            _depth = depth;
        }

        public void Process(float[] buffer, int samplesRead)
        {


            double phaseIncrement = Math.PI * 2 * _frequency / _sampleRate;

            for (int i = 0; i < samplesRead; i++)
            {
                double amChange = 1 + _depth * Math.Sin(_phase);
                buffer[i] *= (float)amChange;
                _phase += phaseIncrement;
                if (_phase > Math.PI * 2) _phase -= Math.PI * 2;
            }

        }
    }
}
