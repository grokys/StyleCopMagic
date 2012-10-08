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

    /// <summary>
    /// Runtime builds <see cref="IFixer"/> classes.
    /// </summary>
    public static class FixerFactory
    {
        public static IEnumerable<Type> EnumerateFixers()
        {
            var types = from type in Assembly.GetAssembly(typeof(IFixer)).GetTypes()
                        where !type.IsInterface && typeof(IFixer).IsAssignableFrom(type)
                        select type;
            return types;
        }

        public static IFixer Create(Type type, SyntaxTree tree, Compilation compilation, ISettings settings)
        {
            Type[] ctorArgs = new[] { typeof(SyntaxTree), typeof(Compilation), typeof(ISettings) };
            ConstructorInfo constructor = type.GetConstructor(ctorArgs);
            return (IFixer)constructor.Invoke(new object[] { tree, compilation, settings });
        }
    }
}
