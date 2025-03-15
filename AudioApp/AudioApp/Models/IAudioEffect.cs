namespace AudioApp.Models
{
    public interface IAudioEffect
    {
        public void Process(float[] buffer, int samplesRead);
    }
}
