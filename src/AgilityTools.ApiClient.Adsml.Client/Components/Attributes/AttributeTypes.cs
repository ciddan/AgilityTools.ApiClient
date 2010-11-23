using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
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