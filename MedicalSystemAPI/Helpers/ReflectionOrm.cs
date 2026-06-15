using System.Reflection;
using System.Text;

namespace MedicalSystemAPI.Helpers
{
    public static class ReflectionOrm
    {
        public static string GenerateInsertQuery<T>(T entity)
        {
            Type type = typeof(T);

            string tableName = type.Name + "s";

            PropertyInfo[] properties = type.GetProperties();

            var columns = new List<string>();
            var values = new List<string>();

            foreach (var property in properties)
            {
                if (property.Name == "Id")
                    continue;

                if (property.PropertyType.IsGenericType)
                    continue;

                if (!property.PropertyType.IsPrimitive &&
                    property.PropertyType != typeof(string) &&
                    property.PropertyType != typeof(DateTime) &&
                    property.PropertyType != typeof(decimal))
                    continue;

                columns.Add(property.Name);

                object? value = property.GetValue(entity);

                if (value == null)
                {
                    values.Add("NULL");
                }
                else if (value is string || value is DateTime)
                {
                    values.Add($"'{value}'");
                }
                else
                {
                    values.Add(value.ToString()!);
                }
            }

            StringBuilder sql = new StringBuilder();

            sql.Append($"INSERT INTO \"{tableName}\" (");

            sql.Append(string.Join(", ", columns));

            sql.Append(") VALUES (");

            sql.Append(string.Join(", ", values));

            sql.Append(");");

            return sql.ToString();
        }
    }
}