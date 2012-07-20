using System;
using HtmlTags;
using MVCConventions.Reflection;

namespace MVCConventions.Html
{
    public class HtmlTagModifier
    {
        private readonly Func<UIComponentContext, bool> _modifyPredicate;
        private readonly Func<UIComponentContext, HtmlTag, HtmlTag> _modifyFunc;

        public HtmlTagModifier(Func<UIComponentContext, bool> modifyPredicate, Func<UIComponentContext, HtmlTag, HtmlTag> modifyFunc)
        {
            _modifyPredicate = modifyPredicate;
            _modifyFunc = modifyFunc;
        }

        public bool ShouldModify(UIComponentContext context)
        {
            return _modifyPredicate(context);
        }

        public HtmlTag Modify(UIComponentContext context, HtmlTag htmlTag)
        {
            return _modifyFunc(context, htmlTag);
        }
    }
}