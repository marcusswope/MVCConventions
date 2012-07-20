using MVCConventions.Utility;
using NUnit.Framework;

namespace MVCConventions.Test.Utility
{
    public class ExtensionsSpecs
    {
        [TestFixture]
        public class When_asked_for_pretty_string
        {
            [Test]
            public void Should_display_easy_case()
            {
                "BoolProperty".ToPrettyString().ShouldEqual("Bool Property");
            }

            [Test]
            public void Should_display_multiple_uppercase_in_a_row()
            {
                "XMLFile".ToPrettyString().ShouldEqual("XML File");
            }

            [Test]
            public void Should_display_easy_number_case()
            {
                "10Times".ToPrettyString().ShouldEqual("10 Times");
            }

            [Test]
            public void Should_display_numbers_and_uppercase()
            {
                "10XMLFiles".ToPrettyString().ShouldEqual("10 XML Files");
            }

            [Test]
            public void Should_display_numbers_and_lower_case()
            {
                "10xmlFiles".ToPrettyString().ShouldEqual("10 xml Files");
            }

            [Test]
            public void Should_split_numbers_after_letters()
            {
                "Address1".ToPrettyString().ShouldEqual("Address 1");
            }

            [Test]
            public void Should_return_all_caps_as_all_caps()
            {
                "ALLCAPS".ToPrettyString().ShouldEqual("ALLCAPS");
            }

            [Test]
            public void Should_return_description_attribute_on_enums()
            {
                Currency.OneThousand.ToPrettyString().ShouldEqual("$1,000");
                Currency.TenThousand.ToPrettyString().ShouldEqual("$10,000");
            }
        }

        public enum Currency
        {
            [System.ComponentModel.Description("$1,000")]
            OneThousand,
            [System.ComponentModel.Description("$10,000")]
            TenThousand
        }
    }
}