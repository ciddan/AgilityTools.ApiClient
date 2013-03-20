using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Internal conveniance class for use with AQL-queries. Used when the source data can contain either an attribute name or definition id.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class IdNameReference 
  {
    private readonly string _name;
    private readonly IList<int> _defIds = new List<int>();

    internal IdNameReference(IEnumerable<int> definitionIds) {
      _defIds = new List<int>(definitionIds);
    }

    internal IdNameReference(params int[] definitionIds) {
      _defIds = new List<int>(definitionIds);
    }
    
    internal IdNameReference(string name) {
      _name = name;
    }
    
    public override string ToString() {
      if (_defIds.Count > 0) {
        return _defIds.Select(d => d.ToString()).Aggregate(
          string.Empty,
          (aggr, next) => {
            if (string.IsNullOrEmpty(aggr)) {
              return string.Format("#{0}", next);
            } else {
              return string.Format("{0}, #{1}", aggr, next);
            }
          }
        );
      }

      return string.Format("\"{0}\"", _name);
    }
  }
}