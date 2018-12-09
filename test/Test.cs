using System;
using System.Linq;
using campaignmonitor;
using Xunit;

namespace test
{
    public class Test
    {
        [Fact]
        public void IsNullOrEmptyTest()
        {
            Assert.True(Utilities.IsNullOrEmpty(null));
            Assert.False(Utilities.IsNullOrEmpty("a"));
            Assert.True(Utilities.IsNullOrEmpty(""));
            Assert.False(Utilities.IsNullOrEmpty("null"));
        }

        [Fact]
        public void GetPositiveDivisorTest()
        {
            Assert.Equal(new[] {1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60}, Utilities.GetPositiveDivisor(60).ToArray());
            Assert.Equal(new[] {1, 2, 3, 6, 7, 14, 21, 42}, Utilities.GetPositiveDivisor(42).ToArray());
            Assert.Equal(new[] {1, 2, 3, 4, 6, 9, 12, 18, 36}, Utilities.GetPositiveDivisor(36).ToArray()); // square number

            Assert.Equal(new int[0], Utilities.GetPositiveDivisor(0).ToArray());
            Assert.Equal(new[] {1}, Utilities.GetPositiveDivisor(1).ToArray());

            var exception = Assert.Throws<ArgumentException>( () => Utilities.GetPositiveDivisor(-1).ToArray());
            Assert.Equal("number '-1' should be positive.", exception.Message);
        }


        [Fact]
        public void GetTriangleAreaTest()
        {
            Assert.Equal(6, Utilities.GetTriangleArea(3, 4, 5));

            var exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, 4, 5) );
            Assert.Equal("Length '-3' should be greater than 0.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(3, -4, 5) );
            Assert.Equal("Length '-4' should be greater than 0.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(3, 4, -5) );
            Assert.Equal("Length '-5' should be greater than 0.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, -4, 5) );
            Assert.Equal("Length '-3, -4' should be greater than 0.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(3, -4, -5) );
            Assert.Equal("Length '-4, -5' should be greater than 0.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, 4, -5) );
            Assert.Equal("Length '-3, -5' should be greater than 0.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, -4, -5) );
            Assert.Equal("Length '-3, -4, -5' should be greater than 0.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(1, 10, 12) );
            Assert.Equal("Invalid triangle because its lengths '1 + 10 <= 12'.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(1, 12, 10) );
            Assert.Equal("Invalid triangle because its lengths '1 + 10 <= 12'.", exception.Message);

            exception = Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(12, 1, 10) );
            Assert.Equal("Invalid triangle because its lengths '1 + 10 <= 12'.", exception.Message);
        }

        [Fact]
        public void GetMostCommonTest()
        {
            Assert.Equal(new[] {4, 5}, Utilities.GetMostCommon(new[] {5, 4, 3, 2, 4, 5, 1, 6, 1, 2, 5, 4}).OrderBy(x => x));
            Assert.Equal(new[] {1}, Utilities.GetMostCommon(new[] {1, 2, 3, 4, 5, 1, 6, 7}));
            Assert.Equal(new[] {1, 2, 3, 4, 5, 6, 7}, Utilities.GetMostCommon(new[] {1, 2, 3, 4, 5, 6, 7}));

            Assert.Equal(new[] {-5, -4}, Utilities.GetMostCommon(new[] {-5, -4, -3, 2, -4, -5}).OrderBy(x => x));
        }

        [Fact]
        public void LinkCheckerTest()
        {
            var linkStatus = LinkChecker.GetLinkStatus(linkCheckerTestHtml);

            var expected = new[] {
                new LinkStatus("https://www.bing.com", isValid: true),
                new LinkStatus("https://www.microsoft.com", isValid: true),
                new LinkStatus("https://www.Idontexist.com", isValid: false)
            };
            Assert.Equal(expected.OrderBy(a => a.Url), linkStatus.OrderBy(a => a.Url));
        }

        string linkCheckerTestHtml = @"<!DOCTYPE html> 
<html> 
<head> 
    <meta charset=""utf-8"" /> 
    <meta http-equiv=""content-type"" content=""text/html;charset=utf-8"">
    <title></title> 
</head> 
<body> 
    <h1>Hello world</h1> 
    <div> 
        <a href=""https://www.bing.com"">bing search</a> <br>
        <a href=""https://www.microsoft.com"">microsoft home page</a>  <br><hr /> 
        <a href=""https://www.Idontexist.com"">I don't exist website</a>  <br><hr /> 
 
        <input type=""text"" calss=""misshapenTag""> 
        <img class=""misshapenTag""> 
    </div> 
</body> 
</html>";

        [Fact]
        public void LinkCheckerNoLinksTest()
        {
            var linkStatus = LinkChecker.GetLinkStatus(string.Empty);
            Assert.Equal(Enumerable.Empty<LinkStatus>(), linkStatus);
        }

        [Fact]
        public void LinkCheckerInvalidLinksTest()
        {
            var linkStatus = LinkChecker.GetLinkStatus(LinkCheckerTestInvalidLinksHtml);

            var expected = new[] {
                new LinkStatus("ThisIsInvalidUrl", isValid: false),
                new LinkStatus("This Is Invalid Url", isValid: false),
                new LinkStatus("abcde://ThisIsInvalidUrl.com", isValid: false)
            };
            Assert.Equal(expected.OrderBy(a => a.Url), linkStatus.OrderBy(a => a.Url));
        }

        string LinkCheckerTestInvalidLinksHtml = @"<!DOCTYPE html> 
<html> 
<head> 
    <title></title> 
</head> 
<body> 
    <div> 
        <a href=""ThisIsInvalidUrl"">This is Invalid Url (without space)</a> <br>
        <a href=""This Is Invalid Url"">This is Invalid Url (with space)</a>  <br><hr /> 
        <a href=""abcde://ThisIsInvalidUrl.com"">This is Invalid Url (with symbols)</a>  <br><hr /> 
    </div> 
</body> 
</html>";

        [Fact]
        public void LinkCheckerCacheTest()
        {
            //jk-todo
        }
    }
}
