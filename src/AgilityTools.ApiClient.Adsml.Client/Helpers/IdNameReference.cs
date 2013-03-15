using System.ComponentModel;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Internal conveniance class for use with AQL-queries. Used when the source data can contain either an attribute name or definition id.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IdNameReference 
    {
        private readonly string _name;
        private readonly int _defId;

        internal IdNameReference(int defId) {
            _defId = defId;
        }

        internal IdNameReference(string name) {
            _name = name;
        }

        public override string ToString() {
            return !string.IsNullOrEmpty(_name)
                       ? string.Format("\"{0}\"", _name)
                       : string.Format("#{0}", _defId);
        }
    }
}