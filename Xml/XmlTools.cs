using System;
using System.Globalization;

namespace Diese.Xml
{
    static public class XmlTools
    {
        public static string ToString<T>(this T value)
        {
            string text;

            try
            {
                text = (string)Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture);
            }
            catch
            {
                text = "";
            }

            return text;
        }

        public static T Parse<T>(this string text)
        {
            T value;

            try
            {
                value = (T)Convert.ChangeType(text, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                value = default(T);
            }

            return value;
        }

        static public T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}