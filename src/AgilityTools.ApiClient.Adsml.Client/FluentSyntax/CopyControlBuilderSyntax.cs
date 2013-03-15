using System.ComponentModel;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Defines available commands and command order for the <see cref="ICopyControlBuilder"/>. 
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ICopyControlBuilder : IFluentInterface, ICopyControlConfigLookupControls
    {
        ICopyControlConfigLookupControls CopyLocalAttributesFromSource();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ICopyControlConfigLookupControls : IFluentInterface
    {
        ILookupControlBuilder ConfigureLookupControls();
    }
}