using System.Collections.Generic;

namespace MediKeeper.WebApi.Pagination
{
    public interface IPropertyMapping
    {
        Dictionary<string, PropertyMappingValue> MappingDictionary { get; }
    }
}