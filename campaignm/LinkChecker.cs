using System.Collections.Generic;
using System.Linq;

namespace campaignmonitor
{
    public class LinkChecker
    {
        IEnumerable<LinkStatus> GetLinkStatus(string html)
        {
            return Enumerable.Empty<LinkStatus>();
        }
    }

    public class LinkStatus
    {
        public LinkStatus(string url, bool isValid)
        {
            Url = url;
            IsValid = IsValid; 
        }

        public string Url {get; private set;}
        public bool IsValid {get; private set;}
    }
}