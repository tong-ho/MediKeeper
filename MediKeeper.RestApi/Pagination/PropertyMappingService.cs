using MediKeeper.RestApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediKeeper.WebApi.Pagination
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> itemPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.InvariantCultureIgnoreCase)
        {
            { nameof(Item.Id), new PropertyMappingValue(new List<string>() { nameof(Item.Id) })},
            { nameof(Item.Name), new PropertyMappingValue(new List<string>() { nameof(Item.Name) })},
            { nameof(Item.Cost), new PropertyMappingValue(new List<string>() { nameof(Item.Cost) })}
        };

        private readonly IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<Item>(itemPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<T>()
        {
            var matchingMapping = propertyMappings.OfType<PropertyMapping<T>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(T)}>");
        }

        public bool IsValidMapping<T>(string fields)
        {
            var propertyMapping = GetPropertyMapping<T>();

            if (string.IsNullOrEmpty(fields))
            {
                return true;
            }

            //the orderBy string is separated by commas
            string[] fieldsAfterSplit = fields.Split(',');

            //apply each orderBy clause in reverse order - otherwise, the IQueryable will be ordered wrong
            foreach (string field in fieldsAfterSplit)
            {
                string trimmedField = field.Trim();

                int indexOfFirstSpace = trimmedField.IndexOf(" ");
                string propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                //Find property
                if (!propertyMapping.TryGetValue(propertyName, out PropertyMappingValue propertyMappingValue))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
