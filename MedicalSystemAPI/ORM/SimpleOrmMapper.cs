using System.Reflection;

namespace MedicalSystemAPI.ORM
{
    public static class SimpleOrmMapper
    {
        public static string GetTableName<T>()
        {
            return typeof(T).Name + "s";
        }

        public static List<string> GetColumnNames<T>()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name)
                .ToList();
        }
    }
}