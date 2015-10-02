using System;

namespace NZBDash.UI.Helpers
{
    public static class UrlHelper
    {
        public static Uri ReturnUri(string val)
        {
            try
            {
                var uri = new UriBuilder();
                if (val.StartsWith("http://", StringComparison.Ordinal))
                {
                    uri = new UriBuilder(val);
                }
                else if (val.StartsWith("https://", StringComparison.Ordinal))
                {
                    uri = new UriBuilder(val);
                }
                else if (val.Contains(":"))
                {
                    var split = val.Split(':');
                    int port;
                    int.TryParse(split[1], out port);

                    uri = new UriBuilder(Uri.UriSchemeHttp, split[0], port);
                }
                else
                {
                    uri = new UriBuilder(Uri.UriSchemeHttp, val);
                }

                return uri.Uri;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}