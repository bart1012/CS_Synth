namespace AudioApp.Service
{
    public interface IAudioEffect
    {
        public int Process(float[] buffer, int offset, int count);
    }
}
