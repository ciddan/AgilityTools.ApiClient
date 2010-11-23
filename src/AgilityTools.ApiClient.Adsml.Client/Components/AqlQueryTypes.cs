using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public enum AqlQueryTypes
    {
        [StringValue("")]
        None = 0,
        [StringValue("ABOVE")]
        Above = 1,
        [StringValue("BELOW")]
        Below = 2,
        [StringValue("CHILD")]
        Child = 3,
        [StringValue("PARENT")]
        Parent = 4,
        [StringValue("LINK-TO")]
        LinkTo = 5,
        [StringValue("LINK-FROM")]
        LinkFrom = 6
    }
}