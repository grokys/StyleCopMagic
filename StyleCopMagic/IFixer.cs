// -----------------------------------------------------------------------
// <copyright file="IFixer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using Roslyn.Compilers.CSharp;

    public interface IFixer
    {
        SyntaxTree Repair();
    }
}
