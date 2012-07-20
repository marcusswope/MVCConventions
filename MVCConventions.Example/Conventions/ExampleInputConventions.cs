using System;
using HtmlTags;
using MVCConventions.Attributes;
using MVCConventions.Reflection;
using MVCConventions.Registry;

namespace MVCConventions.Example.Conventions
{
    public class ExampleInputConventions : InputRegistry
    {
        public ExampleInputConventions()
        {
            ByDefault(x => new HtmlTag("input").Attr("type", "text").Attr("value", x.RawValue));

            IfPropertyIs<bool>().BuildWith(Checkbox);

            If(x => x.Accessor.FieldName.Contains("Password"))
                .Or(x => x.Accessor.FieldName.Contains("SocialSecurity"))
                .ModifyWith((context, tag) => tag.Attr("type", "password"));

            If(x => x.Accessor.FieldName.Contains("Email"))
                .ModifyWith((context, tag) => tag.AddClass("email"));

            IfPropertyIs<DateTime>()
                .ModifyWith((context, tag) => tag.AddClass("date"));

            IfPropertyIs<DateTime>()
                .ModifyWith((context, tag) => tag.Attr("placeholder", "mm/dd/yyyy"));

            IfPropertyHas<RequiredAttribute>()
                .ModifyWith((context, tag) => tag.AddClass("required"));

            IfPropertyHas<MaxLengthAttribute>()
                .ModifyWith((context, tag) => tag.Attr("maxLength", context.PropertyAttribute<MaxLengthAttribute>().MaxLength));

            IfPropertyHas<MinLengthAttribute>()
                .ModifyWith((context, tag) => tag.Attr("minLength", context.PropertyAttribute<MinLengthAttribute>().MinLength));

        }

        private static HtmlTag Checkbox(UIComponentContext context)
        {
            var checkbox = new CheckboxTag(context.Value<bool>()).Attr("value", "true");
            var hidden = new HtmlTag("input").Attr("type", "hidden").Attr("value", "false");
            checkbox.Next = hidden;
            return checkbox;
        }
    }
}