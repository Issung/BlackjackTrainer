using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers;

public static class EnumHelper
{
    public static IEnumerable<T> GetValues<T>() where T : Enum
    {
        return (T[])Enum.GetValues(typeof(T));
    }
}
