using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public enum QueryTypes
    {
        [StringValue("ABOVE")]
        Above,
        [StringValue("BELOW")]
        Below,
        [StringValue("CHILD")]
        Child,
        [StringValue("PARENT")]
        Parent,
        [StringValue("LINK-TO")]
        LinkTo,
        [StringValue("LINK-FROM")]
        LinkFrom
    }
}