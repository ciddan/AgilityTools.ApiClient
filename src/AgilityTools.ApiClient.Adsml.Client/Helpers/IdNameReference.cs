using System.ComponentModel;

namespace AgilityTools.ApiClient.Adsml.Client.Helpers
{
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