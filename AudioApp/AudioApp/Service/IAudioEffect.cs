namespace AudioApp.Service
{
    public interface IAudioEffect
    {
        public void Process(float[] buffer, int samplesRead);
    }
}
