using HtmlTags;

namespace MVCConventions.Registry
{
    public abstract class InputRegistry : ConventionRegistry
    {
        protected InputRegistry()
        {
            ByDefault(x => new HtmlTag("input").Attr("type", "text").Attr("value", x.RawValue));
            Always().ModifyWith((context, tag) => tag.Id(context.MVC3TagId));
            Always().ModifyWith((context, tag) => tag.Attr("name", context.MVC3TagName));
        }

        public override void Register(IConventionProfile profile)
        {
            profile.RegisterInputBuilders(TagBuilders);
            profile.RegisterInputModifiers(TagModifiers);
            profile.RegisterDefaultInputBuilder(DefaultBuilder);
        }
    }
}