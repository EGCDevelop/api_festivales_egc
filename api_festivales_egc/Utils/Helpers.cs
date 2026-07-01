using Microsoft.Data.SqlClient;

namespace api_festivales_egc.Utils
{
    public class Helpers
    {
        public static T GetValue<T>(SqlDataReader reader, string columnName)
        {
            var value = reader[columnName];

            if (value == DBNull.Value)
                return default!;

            if (typeof(T) == typeof(DateOnly))
            {
                return (T)(object)DateOnly.FromDateTime((DateTime)value);
            }

            if (typeof(T) == typeof(TimeOnly))
            {
                return (T)(object)TimeOnly.FromTimeSpan((TimeSpan)value);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T GetValueNull<T>(SqlDataReader reader, string columnName)
        {
            var value = reader[columnName];

            // Si el valor es DBNull, devolvemos `default(T)`
            if (value == DBNull.Value)
            {
                return default;
            }

            // Si es un tipo nullable, convertimos el valor a su tipo subyacente
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
            {
                return (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T)));
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
