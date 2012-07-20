using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MVCConventions.Reflection;

namespace MVCConventions.Validation
{
    public class EmailAddressRule<T> : IValidationRule
    {
        private readonly PropertyChain _property;
        private readonly EmailValidator _validator;

        public EmailAddressRule(PropertyChain property)
        {
            _property = property;
            _validator = new EmailValidator();
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
            if (rawValue == null) yield break;

            var regex = new Regex(_validator.Expression);
            if (!regex.IsMatch(rawValue.ToString()))
            {
                yield return new ValidationFailure(_property.Name, string.Format("{0} is not a valid email address", context.PropertyChain.BuildPropertyName(_property.Name)));
            }
        }

        public IEnumerable<IPropertyValidator> Validators
        {
            get { yield break; }
        }

        public string RuleSet { get; set; }
    }
}