using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using renameit_v2_wpf.classes;
namespace renameit_v2_wpf
{

    public sealed class BackgroundConverter : IValueConverter
    {
        SolidColorBrush lightRedBrush =
        new SolidColorBrush(Color.FromArgb(255, 255, 240, 240));

        public object Convert(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {
            // Get the index of a ListViewItem
            return (bool)value ? lightRedBrush : Brushes.White;
            
        }

        public object ConvertBack(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
