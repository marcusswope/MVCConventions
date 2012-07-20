using HtmlTags;
using MVCConventions.Utility;

namespace MVCConventions.Registry
{
    public abstract class LabelRegistry : ConventionRegistry
    {
        protected LabelRegistry()
        {
            ByDefault(x => new HtmlTag("label").Text(x.Accessor.FieldName.ToPrettyString()));
            Always().ModifyWith((context, tag) => tag.Attr("for", context.MVC3TagName));
        }

        public override void Register(IConventionProfile profile)
        {
            profile.RegisterLabelBuilders(TagBuilders);
            profile.RegisterLabelModifiers(TagModifiers);
            profile.RegisterDefaultLabelBuilder(DefaultBuilder);
        }
    }
}