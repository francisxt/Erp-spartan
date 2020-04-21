using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Commons.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Generate a code string minor to 10
        /// </summary>
        /// <param name="length">Length of string generated</param>
        /// <returns>string</returns>
        public static string GetRandomCode(int length)
        {
            if (length <= 10) return Guid.NewGuid().ToString().Substring(0, length).ToUpper();
            return string.Empty;
        }
        public static string FormatDate(DateTime date)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            return date.ToString("dddd, dd MMMM yyyy", culture);
        }
        public static string FormatMoney(decimal money) => string.Format("{0:C}", money);
        
    }
}
