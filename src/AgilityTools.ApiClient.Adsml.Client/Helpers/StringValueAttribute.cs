using System;

namespace AgilityTools.ApiClient.Adsml.Client.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }

        public StringValueAttribute(string value) {
            this.StringValue = value;
        }
    }
}