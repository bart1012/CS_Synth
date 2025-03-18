using NAudio.Dsp;
using NAudio.Wave;

namespace AudioApp.Models
{
    public class ADSREnvelopeProvider : ISampleProvider
    {
        private readonly ISampleProvider source;

        private readonly EnvelopeGenerator adsr;

        private float attackSeconds;

        private float releaseSeconds;

        public float AttackSeconds
        {
            get
            {
                return attackSeconds;
            }
            set
            {
                attackSeconds = value;
                adsr.AttackRate = attackSeconds * (float)WaveFormat.SampleRate;
            }
        }

        public float ReleaseSeconds
        {
            get
            {
                return releaseSeconds;
            }
            set
            {
                releaseSeconds = value;
                adsr.ReleaseRate = releaseSeconds * (float)WaveFormat.SampleRate;
            }
        }

        public float SustainSeconds
        {
            get => adsr.SustainLevel;
            set => adsr.SustainLevel = value;
        }

        public float DecaySeconds
        {
            get => adsr.DecayRate;
            set => adsr.DecayRate = value;
        }

        public WaveFormat WaveFormat => source.WaveFormat;

        public ADSREnvelopeProvider(ISampleProvider source)
        {
            if (source.WaveFormat.Channels > 1)
            {
                throw new ArgumentException("Currently only supports mono inputs");
            }

            this.source = source;
            adsr = new EnvelopeGenerator();
            AttackSeconds = 0.01f;
            adsr.SustainLevel = 1f;
            adsr.DecayRate = 0f * (float)WaveFormat.SampleRate;
            ReleaseSeconds = 0.3f;
        }

        public ADSREnvelopeProvider(ISampleProvider source, VolumeEnvelopeWrapper env)
        {
            if (source.WaveFormat.Channels > 1)
            {
                throw new ArgumentException("Currently only supports mono inputs");
            }

            this.source = source;
            adsr = new EnvelopeGenerator();
            AttackSeconds = env.Attack;
            adsr.SustainLevel = env.Sustain;
            adsr.DecayRate = env.Decay * (float)WaveFormat.SampleRate;
            ReleaseSeconds = env.Release;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (adsr.State == EnvelopeGenerator.EnvelopeState.Idle)
            {
                return 0;
            }

            int num = source.Read(buffer, offset, count);
            for (int i = 0; i < num; i++)
            {
                buffer[offset++] *= adsr.Process();
            }

            return num;
        }

        public void Start()
        {
            if (adsr.State != EnvelopeGenerator.EnvelopeState.Idle) return;
            adsr.Gate(gate: true);
        }
        public void Stop()
        {
            adsr.Gate(gate: false);
        }
    }
}
