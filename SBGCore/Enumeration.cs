using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBGCore
{
    /// <summary>
    /// Enumeration Class so we can do a switch case on enums
    /// </summary>
    public static class Enumeration
    {
        /// <summary>
        /// Go through a list of enums and return them as a Dictionary
        /// </summary>
        /// <typeparam name="TEnum">type enum</typeparam>
        /// <returns>Dictionary of enum values</returns>
        public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);
            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration Type is expected");
            var dictionary = new Dictionary<int, string>();
            foreach (int value in Enum.GetValues(enumerationType))
            {
                var name = Enum.GetName(enumerationType, value);
                dictionary.Add(value, name);
            }
            return dictionary;
        }
    }
}
