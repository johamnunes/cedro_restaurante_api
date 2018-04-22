using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CedroRestaurante.ApplicationService.Helpers
{
    public static class DataValidation
    {
        public static IEnumerable<string> Validate(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Type attrType = typeof(ValidationAttribute);

            foreach (PropertyInfo propertyInfo in properties)
            {
                object[] customAttributes = propertyInfo.GetCustomAttributes(attrType, inherit: true);

                foreach (object customAttribute in customAttributes)
                {
                    ValidationAttribute validationAttribute = (ValidationAttribute)customAttribute;
                    bool isValid = validationAttribute.IsValid(propertyInfo.GetValue(obj, BindingFlags.GetProperty, null, null, null));
                    if (!isValid)
                        yield return validationAttribute.ErrorMessage;
                }
            }
        }
    }
}
