using System.Collections.Generic;
using System.Linq;
using MVCConventions.Html;
using MVCConventions.Reflection;
using MVCConventions.Utility;

namespace MVCConventions.Registry
{
    public class ConventionProfile : IConventionProfile
    {
        private HtmlTagBuilder _defaultInputBuilder;
        private HtmlTagBuilder _defaultDisplayBuilder;
        private HtmlTagBuilder _defaultLabelBuilder;
        private readonly IList<HtmlTagBuilder> _inputBuilders;
        private readonly IList<HtmlTagBuilder> _displayBuilders;
        private readonly IList<HtmlTagBuilder> _labelBuilders;
        private readonly IList<HtmlTagModifier> _inputModifiers;
        private readonly IList<HtmlTagModifier> _displayModifiers;
        private readonly IList<HtmlTagModifier> _labelModifiers;

        public ConventionProfile()
        {
            _inputBuilders = new List<HtmlTagBuilder>();
            _displayBuilders = new List<HtmlTagBuilder>();
            _labelBuilders = new List<HtmlTagBuilder>();
            _inputModifiers = new List<HtmlTagModifier>();
            _displayModifiers = new List<HtmlTagModifier>();
            _labelModifiers = new List<HtmlTagModifier>();
        }

        public void RegisterDefaultInputBuilder(HtmlTagBuilder defaultInputBuilder)
        {
            _defaultInputBuilder = defaultInputBuilder;
        }

        public void RegisterInputBuilders(IEnumerable<HtmlTagBuilder> inputBuilders)
        {
            _inputBuilders.AddRange(inputBuilders);
        }

        public void RegisterInputModifiers(IEnumerable<HtmlTagModifier> inputModifiers)
        {
            _inputModifiers.AddRange(inputModifiers);
        }

        public IEnumerable<HtmlTagModifier> GetInputModifiers(UIComponentContext context)
        {
            return _inputModifiers.Where(x => x.ShouldModify(context));
        }

        public void RegisterDefaultDisplayBuilder(HtmlTagBuilder defaultDisplayBuilder)
        {
            _defaultDisplayBuilder = defaultDisplayBuilder;
        }

        public void RegisterDisplayBuilders(IEnumerable<HtmlTagBuilder> displayBuilders)
        {
            _displayBuilders.AddRange(displayBuilders);
        }

        public void RegisterDisplayModifiers(IEnumerable<HtmlTagModifier> displayModifiers)
        {
            _displayModifiers.AddRange(displayModifiers);
        }

        public IEnumerable<HtmlTagModifier> GetDisplayModifiers(UIComponentContext context)
        {
            return _displayModifiers.Where(x => x.ShouldModify(context));
        }

        public void RegisterDefaultLabelBuilder(HtmlTagBuilder defaultLabelBuilder)
        {
            _defaultLabelBuilder = defaultLabelBuilder;
        }

        public void RegisterLabelBuilders(IEnumerable<HtmlTagBuilder> labelBuilders)
        {
            _labelBuilders.AddRange(labelBuilders);
        }
        
        public void RegisterLabelModifiers(IEnumerable<HtmlTagModifier> labelModifiers)
        {
            _labelModifiers.AddRange(labelModifiers);
        }

        public HtmlTagBuilder GetInputBuilder(UIComponentContext context)
        {
            return _inputBuilders.FirstOrDefault(x => x.ShouldBuild(context)) ?? _defaultInputBuilder;
        }

        public HtmlTagBuilder GetDisplayBuilder(UIComponentContext context)
        {
            return _displayBuilders.FirstOrDefault(x => x.ShouldBuild(context)) ?? _defaultDisplayBuilder;
        }

        public HtmlTagBuilder GetLabelBuilder(UIComponentContext context)
        {
            return _labelBuilders.FirstOrDefault(x => x.ShouldBuild(context)) ?? _defaultLabelBuilder;
        }

        public IEnumerable<HtmlTagModifier> GetLabelModifiers(UIComponentContext context)
        {
            return _labelModifiers.Where(x => x.ShouldModify(context));
        }
    }
}