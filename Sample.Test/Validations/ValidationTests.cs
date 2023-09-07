using System.ComponentModel.DataAnnotations;

namespace Sample.Test.Validations
{
    public abstract class ValidationTests
    {
        protected bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }
    }
}