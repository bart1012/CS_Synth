namespace AudioApp.Models
{
    public class PianoKey
    {
        public string Note { get; set; }
        public bool isBlack { get; set; }
        public int Index { get; set; }

        public override string? ToString()
        {
            return $"Note: {Note} | isBlack: {isBlack} | Index: {Index}";
        }
    }
}
