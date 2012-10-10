// -----------------------------------------------------------------------
// <copyright file="IFixerExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Roslyn.Compilers.CSharp;

    public static class RuleRewriterFactory
    {
        public static IEnumerable<Type> EnumerateRewriters()
        {
            var types = from type in Assembly.GetAssembly(typeof(RuleRewriter)).GetTypes()
                        where typeof(RuleRewriter).IsAssignableFrom(type) && !type.IsAbstract
                        select type;
            return types;
        }

        public static RuleRewriter Create(Type type, ISettings settings, Func<SemanticModel> semanticModel)
        {
            // A dictionary of all recognised constructor parameters.
            Dictionary<Type, Func<object>> parameterTypes = new Dictionary<Type, Func<object>>
            {
                { typeof(ISettings), () => settings },
                { typeof(SemanticModel), semanticModel },
            };

            // Get a list of the type's constructors together with the constructor parameters types, 
            // ordered by number of parameters descending.
            var ctors = (from c in type.GetConstructors()
                         select new
                         {
                             ConstructorInfo = c,
                             Parameters = c.GetParameters()
                         }).OrderByDescending(x => x.Parameters.Length).ToArray();

            // Get the first constructor in which we recognise all parameter types.
            var ctor = ctors.FirstOrDefault(x => x.Parameters.All(p => parameterTypes.Keys.Contains(p.ParameterType)));

            object[] parameters = ctor.Parameters.Select(x => parameterTypes[x.ParameterType]()).ToArray();

            return (RuleRewriter)ctor.ConstructorInfo.Invoke(parameters);
        }
    }
}
