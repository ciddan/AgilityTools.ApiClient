using System;
using System.ComponentModel;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Interface that hides the auto-inherited methods from <see cref="object"/> from IntelliSense for any class which implements it.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterface
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}