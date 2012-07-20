namespace MVCConventions.Registry
{
    public abstract class DisplayRegistry : ConventionRegistry
    {
        protected DisplayRegistry()
        {
            Always().ModifyWith((context, tag) => tag.Attr("name", context.MVC3TagName));
            Always().ModifyWith((context, tag) => tag.Id(context.MVC3TagId));
        }

        public override void Register(IConventionProfile profile)
        {
            profile.RegisterDisplayBuilders(TagBuilders);
            profile.RegisterDisplayModifiers(TagModifiers);
            profile.RegisterDefaultDisplayBuilder(DefaultBuilder);
        }
    }
}