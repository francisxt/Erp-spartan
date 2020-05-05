using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Commons.Extensions
{
    public static class SharedExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        => enumValue.GetType()?.GetMember(enumValue.ToString())?.FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>(false)?.Name ?? enumValue.ToString();
    }
}
