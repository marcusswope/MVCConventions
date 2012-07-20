using System;
using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using System.Linq;
using MVCConventions.Attributes;
using MVCConventions.Reflection;

namespace MVCConventions.Validation
{
    public class ConventionBasedValidator<T> : AbstractValidator<T>
    {
        public ConventionBasedValidator()
        {
            validateProperties(typeof(T));
        }

        private void validateProperties(Type type, params PropertyInfo[] precedingProperties)
        {
            foreach (var property in type.GetProperties().Where(x => x.CanRead && x.CanWrite))
            {
                var propertyChain = buildPropertyChain(property, precedingProperties);
                if (property.HasAttribute<RequiredAttribute>())
                {
                    AddRule(new PropertyRequiredRule<T>(propertyChain));
                }
                if (property.HasAttribute<MinLengthAttribute>())
                {
                    AddRule(new MinimumLengthRequiredRule<T>(propertyChain, property.GetAttribute<MinLengthAttribute>().MinLength));
                }
                if (property.HasAttribute<MaxLengthAttribute>())
                {
                    AddRule(new MaximumLengthRequiredRule<T>(propertyChain, property.GetAttribute<MaxLengthAttribute>().MaxLength));
                }
                if (property.PropertyType.IsTypeOrNullableOf<DateTime>())
                {
                    AddRule(new DateAfterTodayRule<T>(propertyChain));
                }
                if (property.Name.Contains("Email") && property.PropertyType == typeof(string))
                {
                    AddRule(new EmailAddressRule<T>(propertyChain));
                }
                if ((property.Name.Contains("Zip") && property.PropertyType == typeof(string)))
                {
                    AddRule(new MinimumLengthRequiredRule<T>(propertyChain, 5));
                    AddRule(new MaximumLengthRequiredRule<T>(propertyChain, 9));
                }
                if ((property.Name.Contains("Phone") && property.PropertyType == typeof(string)))
                {
                    AddRule(new MinimumLengthRequiredRule<T>(propertyChain, 10));
                    AddRule(new MaximumLengthRequiredRule<T>(propertyChain, 10));
                }
                if ((property.Name.Contains("SocialSecurity") && property.PropertyType == typeof(string)))
                {
                    AddRule(new MinimumLengthRequiredRule<T>(propertyChain, 9));
                    AddRule(new MaximumLengthRequiredRule<T>(propertyChain, 9));
                }
                if (property.PropertyType.IsClass && property.PropertyType.Assembly == typeof(T).Assembly)
                {
                    var allProps = new List<PropertyInfo>(precedingProperties);
                    var propertyList = allProps.ToList();
                    propertyList.Add(property);
                    validateProperties(property.PropertyType, propertyList.ToArray());
                }
            }
        }

        private PropertyChain buildPropertyChain(PropertyInfo property, IEnumerable<PropertyInfo> precedingProperties)
        {
            var valueGetters = precedingProperties.Select(x => new PropertyValueGetter(x)).ToList();
            valueGetters.Add(new PropertyValueGetter(property));
            return new PropertyChain(valueGetters.ToArray());
        }
    }
}