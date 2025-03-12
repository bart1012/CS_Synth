using System.Globalization;
using System.Windows.Data;

namespace AudioApp.Converters
{
    public class NoteToCanvasPositionConverter : IMultiValueConverter
    {

        private readonly Dictionary<string, int> _offsetDictionary = new()
            {
                {"C" , 0},
                {"C#", 25},
                { "D", 40 },
                { "D#", 65 },
                { "E", 80 },
                { "F", 120 },
                { "F#", 145 },
                { "G", 160 },
                { "G#", 185 },
                { "A", 200 },
                { "A#", 225 },
                { "B", 240 },
            };

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) return 0;

            if (values[0] is int index && values[1] is bool isBlackKey && values[2] is string note && values[3] is int octave)
            {

                double distance = _offsetDictionary[note] + ((octave - 1) * 280);
                Console.WriteLine(distance);
                //Console.WriteLine(distance);

                return distance;
            }

            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
