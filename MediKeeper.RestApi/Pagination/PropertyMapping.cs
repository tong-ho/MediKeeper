using System.Collections.Generic;

namespace MediKeeper.WebApi.Pagination
{
    public class PropertyMapping<T> : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            this.MappingDictionary = mappingDictionary;
        }
    }
}
