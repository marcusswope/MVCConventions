using System;
using HtmlTags;
using MVCConventions.Reflection;

namespace MVCConventions.Html
{
    public class HtmlTagBuilder
    {
        private readonly Func<UIComponentContext, bool> _buildPredicate;
        private readonly Func<UIComponentContext, HtmlTag> _buildFunc;

        public HtmlTagBuilder(Func<UIComponentContext, bool> buildPredicate, Func<UIComponentContext, HtmlTag> buildFunc)
        {
            _buildPredicate = buildPredicate;
            _buildFunc = buildFunc;
        }

        public bool ShouldBuild(UIComponentContext context)
        {
            return _buildPredicate(context);
        }

        public HtmlTag Build(UIComponentContext context)
        {
            return _buildFunc(context);
        }
    }
}