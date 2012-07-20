using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using HtmlTags;
using MVCConventions.Html;
using MVCConventions.Reflection;

namespace MVCConventions.Registry
{
    public abstract class ConventionRegistry : IConventionRegistry
    {
        private readonly IList<TagAssembler> _assemblers;
        private Func<UIComponentContext, HtmlTag> _defaultBuilder;

        protected ConventionRegistry()
        {
            _assemblers = new List<TagAssembler>();
        }

        protected void ByDefault(Func<UIComponentContext, HtmlTag> defaultBuilder)
        {
            _defaultBuilder = defaultBuilder;
        }

        protected TagAssembler IfPropertyIs<T>()
        {
            return If(x => x.Accessor.PropertyType.IsTypeOrNullableOf<T>() || typeof(T).IsAssignableFrom(x.Accessor.PropertyType));
        }

        protected TagAssembler IfModelIs<T>()
        {
            return If(x => x.ModelType == typeof(T));
        }

        protected TagAssembler IfPropertyHas<T>()
            where T : Attribute
        {
            return If(x => x.Accessor.InnerProperty.GetCustomAttributes(typeof(T), true).Any());
        }

        protected TagAssembler IfModelHas<T>()
            where T : Attribute
        {
            return If(x => x.ModelType.GetCustomAttributes(typeof(T), true).Any());
        }

        protected TagAssembler Always()
        {
            return If(x => true);
        }

        protected TagAssembler If(Expression<Func<UIComponentContext, bool>> buildPredicate)
        {
            var assembler = new TagAssembler(buildPredicate);
            _assemblers.Add(assembler);
            return assembler;
        }

        public IEnumerable<HtmlTagBuilder> TagBuilders
        {
            get { return _assemblers.Where(x => x.Builder != null).Select(x => x.Builder).ToList().AsReadOnly(); }
        }

        public IEnumerable<HtmlTagModifier> TagModifiers
        {
            get { return _assemblers.Where(x => x.Modifier != null).Select(x => x.Modifier).ToList().AsReadOnly(); }
        }

        public HtmlTagBuilder DefaultBuilder
        {
            get { return new HtmlTagBuilder(x => true, _defaultBuilder); }
        }

        public abstract void Register(IConventionProfile profile);
    }
}