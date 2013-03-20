using System;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  public static class Helpers
  {
    public static int? ToNullableInt(this string s) {
      int i;
      return Int32.TryParse(s, out i) ? i : default(int?);
    }
  }
}

