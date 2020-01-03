using System;
using System.Collections.Generic;
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
            if (length <= 10) return Guid.NewGuid().ToString("X").Substring(0, length).ToUpper();
            return string.Empty;
        }
    }
}
