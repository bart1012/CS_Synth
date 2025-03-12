namespace AudioApp.Models
{
    public class PianoKey
    {
        public string Note { get; set; }
        public bool IsBlack { get; set; }
        public int Index { get; set; }
        public int Octave { get; set; }
        public override string? ToString()
        {
            return $"Note: {Note} | isBlack: {IsBlack} | Index: {Index} | Octave: {Octave}";
        }
    }
}
