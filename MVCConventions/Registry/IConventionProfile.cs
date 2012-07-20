using System.Collections.Generic;
using MVCConventions.Html;
using MVCConventions.Reflection;

namespace MVCConventions.Registry
{
    public interface IConventionProfile
    {
        void RegisterDefaultInputBuilder(HtmlTagBuilder defaultInputBuilder);
        void RegisterInputBuilders(IEnumerable<HtmlTagBuilder> inputBuilders);
        void RegisterInputModifiers(IEnumerable<HtmlTagModifier> inputModifiers);
        HtmlTagBuilder GetInputBuilder(UIComponentContext context);
        IEnumerable<HtmlTagModifier> GetInputModifiers(UIComponentContext context);

        void RegisterDefaultDisplayBuilder(HtmlTagBuilder defaultDisplayBuilder);
        void RegisterDisplayBuilders(IEnumerable<HtmlTagBuilder> displayBuilders);
        void RegisterDisplayModifiers(IEnumerable<HtmlTagModifier> displayModifiers);
        HtmlTagBuilder GetDisplayBuilder(UIComponentContext context);
        IEnumerable<HtmlTagModifier> GetDisplayModifiers(UIComponentContext context);
        

        void RegisterDefaultLabelBuilder(HtmlTagBuilder defaultLabelBuilder);
        void RegisterLabelBuilders(IEnumerable<HtmlTagBuilder> labelBuilders);
        void RegisterLabelModifiers(IEnumerable<HtmlTagModifier> labelModifiers);
        HtmlTagBuilder GetLabelBuilder(UIComponentContext context);
        IEnumerable<HtmlTagModifier> GetLabelModifiers(UIComponentContext context);
    }
}