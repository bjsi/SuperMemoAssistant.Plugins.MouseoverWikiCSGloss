using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMemoAssistant.Plugins.MouseoverWikiCSGloss
{

  public static class WikiUrlUtils
  {
    // Example url: https://en.wikipedia.org/wiki/Hello_World
    public static bool IsDesktopWikipediaUrl(this string url)
    {

      if (string.IsNullOrEmpty(url))
        return false;

      if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        return false;

      Uri uri = new Uri(url);

      string[] splitUri = uri?.Host?.Split('.');
      if (splitUri == null || splitUri.Length != 3)
        return false;

      return splitUri[1] == "wikipedia" ? true : false;

    }

    // TODO: Return the segment after /wiki instead?
    public static string ParseArticleTitle(this string url)
    {
      return new Uri(url)?.Segments?.LastOrDefault();
    }

    public static string ParseArticleLanguage(this string url)
    {
      return url.IsDesktopWikipediaUrl()
        ? new Uri(url).Host.Split('.')[0]
        : null;
    }
  }



  public static class UrlUtils
  {
    public static string ConvRelToAbsLink(string baseUrl, string relUrl)
    {
      if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(relUrl))
      {
        // UriKind.Relative will be false for rel urls containing #
        if (Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute))
        {
          if (baseUrl.EndsWith("/"))
          {
            baseUrl = baseUrl.TrimEnd('/');
          }

          if (relUrl.StartsWith("/") && !relUrl.StartsWith("//"))
          {
            if (relUrl.StartsWith("/wiki") || relUrl.StartsWith("/w/"))
            {
              return $"{baseUrl}{relUrl}";
            }
            return $"{baseUrl}/wiki{relUrl}";
          }
          else if (relUrl.StartsWith("./"))
          {
            if (relUrl.StartsWith("./wiki") || relUrl.StartsWith("./w/"))
            {
              return $"{baseUrl}{relUrl.Substring(1)}";
            }
            return $"{baseUrl}/wiki{relUrl.Substring(1)}";
          }
          else if (relUrl.StartsWith("#"))
          {
            return $"{baseUrl}/wiki/{relUrl}";
          }
          else if (relUrl.StartsWith("//"))
          {
            return $"https:{relUrl}";
          }
        }
      }
      return relUrl;
    }

  }
}
