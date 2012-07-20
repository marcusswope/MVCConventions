using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using HtmlTags;
using MVCConventions.Html;
using MVCConventions.Reflection;
using MVCConventions.Registry;

namespace MVCConventions.Utility
{
    public static class ViewPageExtensions
    {
        private static IConventionProfile _profile;
        public static void ProfileIs(IConventionProfile profile)
        {
            _profile = profile;
        }

        public static HtmlTag InputFor<TViewModel>(this WebViewPage<TViewModel> view, Expression<Func<TViewModel, object>> property)
            where TViewModel : class
        {
            bootstrapGuard();
            var context = new UIComponentContext(property.ToAccessor(), getModel(view), view.ViewData.TemplateInfo.HtmlFieldPrefix);
            return assemble(_profile.GetInputBuilder(context), _profile.GetInputModifiers(context), context);
        }

        public static HtmlTag DisplayFor<TViewModel>(this WebViewPage<TViewModel> view, Expression<Func<TViewModel, object>> property)
            where TViewModel : class
        {
            bootstrapGuard();
            var context = new UIComponentContext(property.ToAccessor(), getModel(view), view.ViewData.TemplateInfo.HtmlFieldPrefix);
            return assemble(_profile.GetDisplayBuilder(context), _profile.GetDisplayModifiers(context), context);
        }

        public static HtmlTag LabelFor<TViewModel>(this WebViewPage<TViewModel> view, Expression<Func<TViewModel, object>> property)
            where TViewModel : class
        {
            bootstrapGuard();
            var context = new UIComponentContext(property.ToAccessor(), getModel(view), view.ViewData.TemplateInfo.HtmlFieldPrefix);
            return assemble(_profile.GetLabelBuilder(context), _profile.GetLabelModifiers(context), context);
        }

        public static void RenderPartial<TViewModel>(this WebViewPage<TViewModel> view, string viewName, Expression<Func<TViewModel, object>> property)
        {
            view.Html.RenderPartial(viewName, property.Compile().Invoke(view.Model), new ViewDataDictionary { TemplateInfo = new TemplateInfo { HtmlFieldPrefix = property.PropertyName() } });
        }

        private static HtmlTag assemble(HtmlTagBuilder builder, IEnumerable<HtmlTagModifier> modifiers, UIComponentContext context)
        {
            var tag = builder.Build(context);
            foreach (var modifier in modifiers)
            {
                tag = modifier.Modify(context, tag);
            }
            return tag;
        }

        private static TViewModel getModel<TViewModel>(WebViewPage<TViewModel> view) where TViewModel : class
        {
            if (view.Model == null)
            {
                return Activator.CreateInstance(typeof(TViewModel)) as TViewModel;
            }
            return view.Model;
        }

        private static void bootstrapGuard()
        {
            if (_profile == null) 
                throw new InvalidOperationException("You must call MVCConventions.Bootstrap(IContainer) first!");
        }
    }
}