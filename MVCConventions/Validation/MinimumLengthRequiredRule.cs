using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MVCConventions.Reflection;
using MVCConventions.Utility;

namespace MVCConventions.Validation
{
    public class MinimumLengthRequiredRule<T> : IValidationRule
    {
        private readonly PropertyChain _property;
        private readonly int _minLength;

        public MinimumLengthRequiredRule(PropertyChain property, int minLength)
        {
            _property = property;
            _minLength = minLength;
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
            //no op
        }

        private IEnumerable<ValidationFailure> validate(ValidationContext context)
        {
            var rawValue = _property.GetValue(context.InstanceToValidate);
            var validationFailure = new ValidationFailure(_property.Name, string.Format("{0} must be at least {1} characters", context.PropertyChain.BuildPropertyName(_property.Name).ToPrettyString(), _minLength));
            if (rawValue != null && !string.IsNullOrEmpty(rawValue.ToString()) && rawValue.ToString().Length < _minLength)
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