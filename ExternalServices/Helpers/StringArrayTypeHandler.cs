using Dapper;
using System;
using System.Data;

namespace ExternalServices.Helpers
{
    public class StringArrayTypeHandler : SqlMapper.TypeHandler<string[]>
    {

        public override string[] Parse(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            string stringValue = value.ToString();

            if (string.IsNullOrEmpty(stringValue))
            {
                return null;
            }

            return stringValue.Split(',');
        }

        public override void SetValue(IDbDataParameter parameter, string[] value)
        {
            throw new NotImplementedException();
        }

    }
}
