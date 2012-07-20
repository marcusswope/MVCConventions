using MVCConventions.Attributes;
using MVCConventions.Test.Utility;
using MVCConventions.Validation;
using NUnit.Framework;

namespace MVCConventions.Test.Validation
{
    public class ConventionBasedValidatorTest
    {
        [TestFixture]
        public class When_asked_to_validate
        {
            [Test]
            public void Should_invalidate_basic_case()
            {
                var validator = new ConventionBasedValidator<BasicViewModel>();
                var viewModel = new BasicViewModel
                                    {
                                        Required = null,
                                        MinLength = "asdf",
                                        MaxLength = "asdfjkl",
                                        EmailAddress = "invalidEmail"
                                    };
                validator.Validate(viewModel).IsValid.ShouldBeFalse();
                validator.Validate(viewModel).Errors.Count.ShouldEqual(4);
            }

            [Test]
            public void Should_validate_basic_case()
            {
                var validator = new ConventionBasedValidator<BasicViewModel>();
                var viewModel = new BasicViewModel
                {
                    Required = "a",
                    MinLength = "asdfa",
                    MaxLength = "asdfkl",
                    EmailAddress = "valid@email.com"
                };
                validator.Validate(viewModel).IsValid.ShouldBeTrue();
                validator.Validate(viewModel).Errors.Count.ShouldEqual(0);
            }

            [Test]
            public void Should_invalidate_complex_case()
            {
                var validator = new ConventionBasedValidator<ComplexViewModel>();
                var basicViewModel = new BasicViewModel
                {
                    Required = null,
                    MinLength = "asdf",
                    MaxLength = "asdfjkl",
                    EmailAddress = "invalidEmail"
                };
                var complexViewModel = new ComplexViewModel
                                           {
                                               BasicViewModel = basicViewModel,
                                               Required = null,
                                               MinLength = "asdf",
                                               MaxLength = "asdfjkl",
                                               EmailAddress = "invalidEmail"
                                           };
                validator.Validate(complexViewModel).IsValid.ShouldBeFalse();
                validator.Validate(complexViewModel).Errors.Count.ShouldEqual(8);
            }

            [Test]
            public void Should_validate_complex_case()
            {
                var validator = new ConventionBasedValidator<ComplexViewModel>();
                var basicViewModel = new BasicViewModel
                {
                    Required = "a",
                    MinLength = "asdfa",
                    MaxLength = "asdfjl",
                    EmailAddress = "valid@email.com"
                };
                var complexViewModel = new ComplexViewModel
                {
                    BasicViewModel = basicViewModel,
                    Required = "a",
                    MinLength = "aasdf",
                    MaxLength = "asfjkl",
                    EmailAddress = "valid@email.com"
                };
                validator.Validate(complexViewModel).IsValid.ShouldBeTrue();
                validator.Validate(complexViewModel).Errors.Count.ShouldEqual(0);
            }
        }

        public class BasicViewModel
        {
            [Required]
            public string Required { get; set; }
            [MinLength(5)]
            public string MinLength { get; set; }
            [MaxLength(6)]
            public string MaxLength { get; set; }
            public string EmailAddress { get; set; }
        }

        public class ComplexViewModel
        {
            [Required]
            public string Required { get; set; }
            [MinLength(5)]
            public string MinLength { get; set; }
            [MaxLength(6)]
            public string MaxLength { get; set; }
            public string EmailAddress { get; set; }

            public BasicViewModel BasicViewModel { get; set; }

            public ComplexViewModel()
            {
                BasicViewModel = new BasicViewModel();
            }
        }
    }
}