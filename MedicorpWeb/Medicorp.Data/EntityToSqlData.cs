using System.Reflection;

namespace Medicorp.Data
{
    public static class EntityToSqlData
    {
        public static IEnumerable<string> GetIdentityProperties<T>()
        {
            List<string> props = new List<string>();

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                CustomColumn attributes = prop.GetCustomAttributes(typeof(CustomColumn), true).OfType<CustomColumn>().FirstOrDefault();
                if (attributes != null && attributes.Identity)
                {
                    props.Add(prop.Name);
                }
            }
            return props;
        }

        public static IEnumerable<string> GetKeyPropertyNames<T>()
        {
            List<string> props = new List<string>();

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                CustomColumn attributes = prop.GetCustomAttributes(typeof(CustomColumn), true).OfType<CustomColumn>().FirstOrDefault();
                if (attributes != null && attributes.Primary)
                {
                    props.Add(prop.Name);
                }
            }
            return props;
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>()
        {
            List<PropertyInfo> props = new List<PropertyInfo>();

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                CustomColumn attributes = prop.GetCustomAttributes(typeof(CustomColumn), true).OfType<CustomColumn>().FirstOrDefault();

                bool ignore = false;

                if (attributes != null && attributes.Ignore)
                {
                    ignore = true;
                }

                if (!ignore)
                {
                    props.Add(prop);
                }
            }
            return props;
        }
    }
}
