using System.Collections.Generic;

namespace MediKeeper.WebApi.Pagination
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<T>();
        bool IsValidMapping<T>(string fields);
    }
}