// -----------------------------------------------------------------------
// <copyright file="ISettings.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    /// <summary>
    /// Provides StyleCop settings.
    /// </summary>
    public interface ISettings
    {
        string CompanyName { get; }
        string Copyright { get; }
    }
}
