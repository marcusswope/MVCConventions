using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MVCConventions.Reflection;
using MVCConventions.Utility;

namespace MVCConventions.Validation
{
    public class DateAfterTodayRule<T> : IValidationRule
    {

        private readonly PropertyChain _property;

        public DateAfterTodayRule(PropertyChain property)
        {
            _property = property;
        }

        public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context)
        {

            return validate(context);
        }

        public IEnumerable<ValidationFailure> Validate(ValidationContext context)
        {
             return validate(context);
        }

        public void ApplyCondition(Func<object, bool> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            // no op
        }

        private IEnumerable<ValidationFailure> validate(ValidationContext context)
        {
            var rawValue = _property.GetValue(context.InstanceToValidate);
            if (rawValue == null) yield break;

            var value = (DateTime)rawValue;
            var validationFailure = new ValidationFailure(_property.Name, 
                                                          string.Format("{0} cannot be in the future",
                                                          context.PropertyChain.BuildPropertyName(_property.Name).ToPrettyString()));
            if (value > DateTime.Now)
            {
                yield return validationFailure;
            }
        }

        public IEnumerable<IPropertyValidator> Validators
        {
            get { yield break; }
        }

        public string RuleSet { get; set; }
    }
}