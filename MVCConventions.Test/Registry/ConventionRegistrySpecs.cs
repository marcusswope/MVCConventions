using System;
using System.Linq;
using System.Linq.Expressions;
using HtmlTags;
using MVCConventions.Reflection;
using MVCConventions.Registry;
using MVCConventions.Test.Utility;
using NUnit.Framework;

namespace MVCConventions.Test.Registry
{
    public class TestModel
    {
        public bool BoolProp { get; set; }
        public string StringProp { get; set; }
    }

    public class TestConventionRegistry : InputRegistry
    {
        public TestConventionRegistry()
        {
            ByDefault(x => new HtmlTag("input"));
            
            IfPropertyIs<bool>()
                .BuildWith(x => new HtmlTag("a"));
            
            IfPropertyIs<bool>()
                .ModifyWith((context, tag) => tag.AddClass("test"));
        }
    }

    public class ConventionRegistrySpecs
    {
        [TestFixture]
        public class When_registering
        {
            private Expression<Func<TestModel, object>> _boolExpr;
            private Expression<Func<TestModel, object>> _stringExpr;

            [SetUp]
            public void SetUp()
            {
                _boolExpr = x => x.BoolProp;
                _stringExpr = x => x.StringProp;
            }

            [Test]
            public void Should_build_tag_builder_conditions()
            {
                var registry = new TestConventionRegistry();

                registry.TagBuilders.Count().ShouldEqual(1);

                registry.TagBuilders.ToList()[0].ShouldBuild(new UIComponentContext(_boolExpr.ToAccessor(), new TestModel(), null)).ShouldBeTrue();
                registry.TagBuilders.ToList()[0].ShouldBuild(new UIComponentContext(_stringExpr.ToAccessor(), new TestModel(), null)).ShouldBeFalse();
            }

            [Test]
            public void Should_build_tag_builder()
            {
                var registry = new TestConventionRegistry();

                registry.TagBuilders.Count().ShouldEqual(1);

                registry.TagBuilders.ToList()[0].Build(new UIComponentContext(_boolExpr.ToAccessor(), new TestModel(), null)).ToString().ShouldEqual(new HtmlTag("a").ToString());
            }

            [Test]
            public void Should_build_tag_modifier_conditions()
            {
                var registry = new TestConventionRegistry();

                registry.TagModifiers.Count().ShouldEqual(3);

                registry.TagModifiers.ToList()[2].ShouldModify(new UIComponentContext(_boolExpr.ToAccessor(), new TestModel(), null)).ShouldBeTrue();
                registry.TagModifiers.ToList()[2].ShouldModify(new UIComponentContext(_stringExpr.ToAccessor(), new TestModel(), null)).ShouldBeFalse();
            }

            [Test]
            public void Should_build_tag_modifier()
            {
                var registry = new TestConventionRegistry();

                registry.TagModifiers.Count().ShouldEqual(3);

                var htmlTag = new HtmlTag("a");
                registry.TagModifiers.ToList()[2].Modify(new UIComponentContext(_boolExpr.ToAccessor(), new TestModel(), null), htmlTag);
                htmlTag.ToString().ShouldEqual(new HtmlTag("a").AddClass("test").ToString());
            }

            [Test]
            public void Should_build_default_builder()
            {
                var registry = new TestConventionRegistry();

                registry.DefaultBuilder.Build(new UIComponentContext(_boolExpr.ToAccessor(), new TestModel(), null)).ToString()
                    .ShouldEqual(new HtmlTag("input").ToString());
            }
        }
    }
}