using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MVCConventions.Reflection;
using MVCConventions.Utility;

namespace MVCConventions.Validation
{
    public class MaximumLengthRequiredRule<T> : IValidationRule
    {
        private readonly PropertyChain _property;
        private readonly int _maxLength;

        public MaximumLengthRequiredRule(PropertyChain property, int maxLength)
        {
            _property = property;
            _maxLength = maxLength;
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
            var validationFailure = new ValidationFailure(_property.Name, string.Format("{0} must be less than {1} characters", context.PropertyChain.BuildPropertyName(_property.Name).ToPrettyString(), _maxLength));
            if (rawValue != null && rawValue.ToString().Length > _maxLength)
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