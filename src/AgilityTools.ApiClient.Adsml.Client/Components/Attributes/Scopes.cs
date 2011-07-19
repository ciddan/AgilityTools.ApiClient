namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Enum with the existing attribute scopes. The string value is the adsml.xsd mandated name for each scope.
    /// </summary>
    public enum Scopes
    {
        [StringValue("global")]
        Global,
        [StringValue("local")]
        Local,
        [StringValue("edgespecific")]
        EdgeSpecific
    }
}