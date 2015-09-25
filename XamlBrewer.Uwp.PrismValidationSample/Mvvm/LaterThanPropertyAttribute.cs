using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mvvm
{
    /// <summary>
    /// Checks whether a DateTime is later than another Property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class LaterThanPropertyAttribute : ValidationAttribute
    {
        string propertyName;

        public LaterThanPropertyAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext obj)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var propertyInfo = obj.ObjectInstance.GetType().GetProperty(this.propertyName);

            if (propertyInfo == null)
            {
                // Should actually throw an exception.
                return ValidationResult.Success;
            }

            dynamic otherValue = propertyInfo.GetValue(obj.ObjectInstance);

            if (otherValue == null)
            {
                return ValidationResult.Success;
            }

            // Unfortunately we have to use the DateTime type here.
            //var compare = ((IComparable<DateTime>)otherValue).CompareTo((DateTime)value);
            var compare = otherValue.CompareTo((DateTime)value);

            if (compare >= 0)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
