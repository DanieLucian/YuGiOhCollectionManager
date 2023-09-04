using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExternalServices.Helpers;

public sealed class GenericComparer<T> : IEqualityComparer<T> where T : class
{

    private readonly string[] _props;

    public GenericComparer(params string[] props)
    {
        _props = props;
    }

    public bool Equals(T x, T y)
    {
        bool result = true;
        Type type = x.GetType();
        PropertyInfo[] propertyInfos = type.GetProperties();
        foreach (var item in _props)
        {
            var check = propertyInfos.SingleOrDefault(t => t.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

            if (check.GetValue(x) != null &&
                check.GetValue(y) != null &&
                !string.Equals(
                 check.GetValue(x).ToString(),
                 check.GetValue(y).ToString(),
                 StringComparison.OrdinalIgnoreCase))
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public int GetHashCode(T obj)
    {
        string code = string.Empty;
        Type type = obj.GetType();
        PropertyInfo[] propertyInfos = type.GetProperties();
        foreach (var item in _props)
        {
            var check = propertyInfos.SingleOrDefault(t => t.Name.Equals(item, StringComparison.OrdinalIgnoreCase));
            code += check.GetValue(obj)?.ToString();
        }
        return code.ToLower().GetHashCode();
    }

}
