using System.ComponentModel;

namespace AgilityTools.ApiClient.Adsml.Client.Helpers
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IdNameReference 
    {
        private readonly string _name;
        private readonly int _defId;

        private readonly bool _initialized;

        internal IdNameReference(int defId) {
            _defId = defId;
            _initialized = true;
        }

        internal IdNameReference(string name) {
            _name = name;
            _initialized = true;
        }

        public override string ToString()
        {
            if (_initialized) {
                return !string.IsNullOrEmpty(_name) 
                    ? string.Format("\"{0}\"", _name) 
                    : string.Format("#{0}", _defId);
            }

            return base.ToString();
        }
    }
}