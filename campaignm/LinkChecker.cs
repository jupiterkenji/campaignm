using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace campaignmonitor
{
    public class LinkChecker
    {
        public static IEnumerable<LinkStatus> GetLinkStatus(string html)
        {
            var result = new ConcurrentBag<LinkStatus>();

            var urls = FindUrls(html);
            var cache = new ConcurrentDictionary<string, bool>();

            Parallel.ForEach(urls, (url) =>
            {
                var isFoundInCache = cache.TryGetValue(url, out _);
                if (!isFoundInCache)
                {
                    cache[url] = IsUrlValid(url);
                }

                result.Add(new LinkStatus(url, cache[url]));
            });

            return result;
        }

        static IEnumerable<string> FindUrls(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var links = htmlDoc.DocumentNode.SelectNodes("//a[@href]");

            if (links != null)
            {
                foreach (var link in links)
                {
                    yield return link.Attributes["href"].Value;
                }
            }
        }

        static bool IsUrlValid(string url)
        {
            var result = false;

            Uri uriResult;

            if (Uri.TryCreate(url, UriKind.Absolute, out uriResult))
            {
                result = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }

            return result;
        }
    }

    public class LinkStatus : IEquatable<LinkStatus> 
    {
        public LinkStatus(string url, bool isValid)
        {
            Url = url;
            IsValid = IsValid; 
        }

        public string Url {get; private set;}
        public bool IsValid {get; private set;}

        public bool Equals(LinkStatus obj)
        {
            var other = obj as LinkStatus;
    
            if (other == null)
                return false;
    
            if (Url != other.Url || IsValid != other.IsValid)
                return false;
    
            return true;
        }

        public override int GetHashCode()
        {
            int hashName = Url == null ? 0 : Url.GetHashCode();
            int hashIsValid = IsValid.GetHashCode();
    
            return hashName ^ hashIsValid;
        }
    }
}