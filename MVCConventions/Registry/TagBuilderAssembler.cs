using System;
using System.Linq.Expressions;
using HtmlTags;
using MVCConventions.Html;
using MVCConventions.Reflection;
using MVCConventions.Utility;

namespace MVCConventions.Registry
{
    public class TagAssembler
    {
        private Expression<Func<UIComponentContext, bool>> _predicate;
        private Func<UIComponentContext, HtmlTag> _buildFunc;
        private Func<UIComponentContext, HtmlTag, HtmlTag> _modifierFunc;

        public TagAssembler(Expression<Func<UIComponentContext, bool>> predicate)
        {
            _predicate = predicate;
        }

        public TagAssembler And(Expression<Func<UIComponentContext, bool>> predicate)
        {
            _predicate = _predicate.And(predicate);
            return this;
        }

        public TagAssembler Or(Expression<Func<UIComponentContext, bool>> predicate)
        {
            _predicate = _predicate.Or(predicate);
            return this;
        }

        public void BuildWith(Func<UIComponentContext, HtmlTag> builder)
        {
            _buildFunc = builder;
        }

        public void ModifyWith(Func<UIComponentContext, HtmlTag, HtmlTag> modifier)
        {
            _modifierFunc = modifier;
        }

        internal HtmlTagBuilder Builder
        {
            get { return _buildFunc == null ? null : new HtmlTagBuilder(_predicate.Compile(), _buildFunc); }
        }

        internal HtmlTagModifier Modifier
        {
            get { return _modifierFunc == null ? null : new HtmlTagModifier(_predicate.Compile(), _modifierFunc); }
        }
    }
}