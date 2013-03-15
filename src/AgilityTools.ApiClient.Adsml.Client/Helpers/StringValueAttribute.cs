using System;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Attribute used to store a string value in an enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }

        public StringValueAttribute(string value) {
            this.StringValue = value;
        }
    }
}