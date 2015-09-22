using System.ComponentModel.DataAnnotations;

namespace Mvvm
{
    /// <summary>
    /// Sample cumstom validation for Integer values.
    /// </summary>
    public class NumericAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int result;
            return int.TryParse(value.ToString(), out result);
        }
    }
}
