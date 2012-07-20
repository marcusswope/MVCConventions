using MVCConventions.Registry;
using MVCConventions.Utility;

namespace MVCConventions.Example.Conventions
{
    public class ExampleLabelConventions : LabelRegistry
    {
        public ExampleLabelConventions()
        {
            IfPropertyIs<bool>()
                .ModifyWith((context, tag) => tag.Format("{0}?"));
        }
    }
}