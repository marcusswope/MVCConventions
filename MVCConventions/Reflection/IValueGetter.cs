using System;
using System.Linq.Expressions;

//This class and almost everything in this namespace was taken from FubuMVC
//https://github.com/DarthFubuMVC/fubumvc
namespace MVCConventions.Reflection
{
    public interface IValueGetter
    {
        object GetValue(object target);
        string Name { get; }
        Type DeclaringType { get; }
        Expression ChainExpression(Expression body);
    }
}
