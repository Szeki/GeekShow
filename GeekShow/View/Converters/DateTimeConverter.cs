using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GeekShow.View.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        #region Public Properties

        public string DateTimeFormat
        {
            get;
            set;
        }

        #endregion

        #region IValueConverter

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
            {
                return string.Empty;
            }

            if(value is DateTime || value is DateTime?)
            {
                var dateValue = value as DateTime?;

                return dateValue.Value.ToString(DateTimeFormat);
            }

            if(value is DateTimeOffset || value is DateTimeOffset?)
            {
                var dateValue = value as DateTimeOffset?;

                return dateValue.Value.ToString(DateTimeFormat);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
