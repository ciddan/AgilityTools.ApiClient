namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Enum with the existing types of attributes. The string value is the adsml.xsd mandated name for each type.
    /// </summary>
    public enum AttributeTypes
    {
        [StringValue("text")]
        Text = 1,
        [StringValue("integer")]
        Integer = 2,
        [StringValue("decimal")]
        Decimal = 3,
        [StringValue("date")]
        Date = 4,
        [StringValue("binary")]
        Binary = 5
    }
}