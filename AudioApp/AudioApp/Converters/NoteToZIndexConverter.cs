using System.Globalization;
using System.Windows.Data;

namespace AudioApp.Converters
{
    public class NoteToZIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string valueAsString)
            {
                return valueAsString.Contains('#') ? 1 : 0;
            }
            else
            {
                throw new ArgumentNullException(nameof(value));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
