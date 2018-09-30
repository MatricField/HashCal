using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Windows.Data;

namespace HashCal
{
    [ValueConversion(typeof(byte[]), typeof(string))]
    public class ByteArrayToStringConvertor :
        IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                var bytes = value as byte[];
                if (null != bytes)
                {
                    var integer = new BigInteger(bytes);
                    return integer.ToString("X", culture.NumberFormat);
                } 
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
