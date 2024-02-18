using System.Data;

namespace ECommerceApi.API.Statics
{
    public static class ToType
    {
        public static Type SqlDbTypeToType(SqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlDbType.NVarChar:
                    return typeof(string);
                case SqlDbType.DateTime:
                    return typeof(DateTime);
                case SqlDbType.Int:
                    return typeof(int);
                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid);
                // Diğer veri türleri burada eklenir
                default:
                    return typeof(object); // Bilinmeyen veri türleri için varsayılan olarak object türünü döndür
            }
        }
    }
}

