using System;
using System.Windows.Data;
using System.Windows.Media;
namespace renameit_v2_wpf
{

    public sealed class BackgroundConverter : IValueConverter
    {
        SolidColorBrush lightRedBrush =
        new SolidColorBrush(Color.FromArgb(255, 255, 240, 240));

        public object Convert(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture) => (bool)value ? lightRedBrush : Brushes.White;

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
