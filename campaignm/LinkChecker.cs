using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace campaignmonitor
{
    public class LinkChecker
    {
        public IEnumerable<LinkStatus> GetLinkStatus(string html)
        {
            var result = new ConcurrentBag<LinkStatus>();

            var urls = FindUrls(html).Distinct();

            Parallel.ForEach(urls, (url) =>
            {
                result.Add(new LinkStatus(url, IsUrlValid(url)));
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

        protected virtual bool IsUrlValid(string url)
        {
            try
            {
                using (var client = new HttpClient())
                using (var response = client.GetAsync(url).Result)
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch {}

            return false;
        }
    }


    public class LinkStatus : IEquatable<LinkStatus> 
    {
        public LinkStatus(string url, bool isValid)
        {
            Url = url;
            IsValid = isValid; 
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