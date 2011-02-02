using System.ComponentModel;

namespace AgilityTools.ApiClient.Adsml.Client
{
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