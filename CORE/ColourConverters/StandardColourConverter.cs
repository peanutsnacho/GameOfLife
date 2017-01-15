using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CORE.ColourConverters
{
    public class StandardColourConverter : IValueConverter
    {
        private SolidColorBrush _liveColour;
        private SolidColorBrush _deadColour;

        public SolidColorBrush LiveColour
        {
            get { return _liveColour; }
            private set { _liveColour = value; }
        }

        public SolidColorBrush DeadColour
        {
            get { return _deadColour; }
            private set { _deadColour = value; }
        }

        public StandardColourConverter()
        {
            LiveColour = Brushes.Black;
            DeadColour = Brushes.White;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool live = false;

            if (value is bool)
                live = (bool)value;

            return live ? LiveColour : DeadColour;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool live = false;

            if (value is SolidColorBrush)
                if ((SolidColorBrush)value == LiveColour)
                    live = true;

            return live;
        }
    }
}
