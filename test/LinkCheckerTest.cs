using System;
using System.Linq;
using campaignmonitor;
using Moq;
using Moq.Protected;
using Xunit;

namespace test
{
    public class LinkCheckerTest
    {
        [Fact]
        public void ValidLinksTest()
        {
            var linkStatus = new LinkChecker().GetLinkStatus(validLinksHtml);

            var expected = new[] {
                new LinkStatus("https://www.bing.com", isValid: true),
                new LinkStatus("http://www.microsoft.com", isValid: true),
            };
            Assert.Equal(expected.OrderBy(a => a.Url), linkStatus.OrderBy(a => a.Url));
        }

        string validLinksHtml = @"<!DOCTYPE html> 
<html> 
<head> 
    <title></title> 
</head> 
<body> 
    <div> 
        <a href=""https://www.bing.com"">bing search</a> <br>
        <a href=""http://www.microsoft.com"">microsoft search</a> <br>
    </div> 
</body> 
</html>";

        [Fact]
        public void InvalidLinksTest()
        {
            var linkStatus = new LinkChecker().GetLinkStatus(InvalidLinksHtml);

            var expected = new[] {
                new LinkStatus("https://www.Idontexistasdfsdfsdf.com", isValid: false),
                new LinkStatus("http://www.Idontexistasdfsdfsdf123.com", isValid: false),
                new LinkStatus("ThisIsInvalidUrl", isValid: false),
                new LinkStatus("This Is Invalid Url", isValid: false),
                new LinkStatus("abcde://ThisIsInvalidUrl.com", isValid: false)
            };
            Assert.Equal(expected.OrderBy(a => a.Url), linkStatus.OrderBy(a => a.Url));
        }

        string InvalidLinksHtml = @"<!DOCTYPE html> 
<html> 
<head> 
    <title></title> 
</head> 
<body> 
    <div> 
        <a href=""https://www.Idontexistasdfsdfsdf.com"">I don't exist website 1</a>  <br><hr /> 
        <a href=""http://www.Idontexistasdfsdfsdf123.com"">I don't exist website 2</a>  <br><hr /> 
        <a href=""ThisIsInvalidUrl"">I don't exist website 2</a>  <br><hr /> 
        <a href=""This Is Invalid Url"">I don't exist website 2</a>  <br><hr /> 
        <a href=""abcde://ThisIsInvalidUrl.com"">I don't exist website 2</a>  <br><hr /> 
    </div> 
</body> 
</html>";

        [Fact]
        public void NoLinksTest()
        {
            var linkStatus = new LinkChecker().GetLinkStatus(string.Empty);
            Assert.Equal(Enumerable.Empty<LinkStatus>(), linkStatus);
        }

        [Fact]
        public void CacheTest()
        {
            var mock = new Mock<LinkChecker>();
            mock
                .Protected()
                .Setup<bool>("IsUrlValid", ItExpr.IsAny<string>())
                .Returns((string url) => url == "www.validurl.com" ? true: false);

            var expected = new[] {
                new LinkStatus("www.invalidurl.com", isValid: false),
                new LinkStatus("www.INVALIDURL.com", isValid: false),
                new LinkStatus("www.validurl.com", isValid: true),
                new LinkStatus("www.VALIDURL.com", isValid: false),
            };
            var linkStatus = mock.Object.GetLinkStatus(cacheHtml);

            mock.Protected().Verify("IsUrlValid", Times.Exactly(4), ItExpr.IsAny<string>());
            Assert.Equal(expected.OrderBy(a => a.Url), linkStatus.OrderBy(a => a.Url));
        }

        string cacheHtml = @"<!DOCTYPE html> 
<html> 
<head> 
    <title></title> 
</head> 
<body> 
    <div> 
        <a href=""www.validurl.com"">This is valid Url (lower cases)</a> <br>
        <a href=""www.VALIDURL.com"">This is valid  Url (capital cases)</a> <br>
        <a href=""www.invalidurl.com"">This is Invalid Url</a>  <br><hr /> 
        <a href=""www.invalidurl.com"">This is Invalid Url</a>  <br><hr /> 
        <a href=""www.invalidurl.com"">This is Invalid Url</a>  <br><hr /> 
        <a href=""www.INVALIDURL.com"">This is Invalid Url</a>  <br><hr /> 
    </div> 
</body> 
</html>";
    }
}
