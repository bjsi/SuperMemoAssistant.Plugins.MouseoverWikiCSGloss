﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMemoAssistant.Plugins.MouseoverWikiCSGloss
{
  public static class HtmlEx
  {
    public static HtmlDocument ConvRelToAbsLinks(this HtmlDocument doc, string baseUrl)
    {
      if (doc != null)
      {
        // Can't directly check if relative, fails if rel url contains #
        var linkNodes = doc.DocumentNode.SelectNodes("//a[@href]")?
                                        .Where(a => !Uri.IsWellFormedUriString(a.GetAttributeValue("href", null),
                                                                               UriKind.Absolute));
        if (linkNodes != null)
        {
          foreach (var linkNode in linkNodes)
          {
            string absHref = UrlUtils.ConvRelToAbsLink(baseUrl, linkNode.GetAttributeValue("href", null));
            linkNode.Attributes["href"].Value = absHref;
          }
        }
      }
      return doc;
    }

  }
}
