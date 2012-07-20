using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using MVCConventions.Registry;
using MVCConventions.Utility;
using StructureMap;
using System.Linq;

namespace MVCConventions.Infrastructure
{
    public static class MVCConventions
    {
        public static void Bootstrap(IContainer container, params Assembly[] assembliesToScan)
        {
            var registries = buildRegistries(assembliesToScan);
            var conventionProfile = buildProfile(container, registries);

            ViewPageExtensions.ProfileIs(conventionProfile);

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));
        }

        private static IConventionProfile buildProfile(IContainer container, IEnumerable<Type> registries)
        {
            container.Configure(x =>
                {
                    registries.Each(c => x.For(typeof (IConventionRegistry)).Add(c));
                    x.For<IConventionProfile>().Singleton().Use<ConventionProfile>();
                });

            var conventionProfile = container.GetInstance<IConventionProfile>();
            foreach (var conventionRegistry in container.GetAllInstances<IConventionRegistry>())
            {
                conventionRegistry.Register(conventionProfile);
            }
            return conventionProfile;
        }

        private static IEnumerable<Type> buildRegistries(IEnumerable<Assembly> assembliesToScan)
        {
            foreach (var assembly in assembliesToScan)
            {
                foreach (var registryType in assembly.GetTypes().Where(x => typeof(IConventionRegistry).IsAssignableFrom(x)))
                {
                    yield return registryType;
                }
            }
        }
    }
}