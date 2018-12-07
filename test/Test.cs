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
            Assert.Equal(new[] {1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60}, Utilities.GetPositiveDivisor(60));
            Assert.Equal(new[] {1, 2, 3, 6, 7, 14, 21, 42}, Utilities.GetPositiveDivisor(42));

            Assert.Equal(new int[0], Utilities.GetPositiveDivisor(0));

            Assert.Throws<ArgumentException>( () => Utilities.GetPositiveDivisor(-1) );
        }


        [Fact]
        public void GetTriangleAreaTest()
        {
            Assert.Equal(6, Utilities.GetTriangleArea(3, 4, 5));

            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, 4, 5) );
            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(3, -4, 5) );
            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(3, 4, -5) );
            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, -4, 5) );
            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(3, -4, -5) );
            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, 4, -5) );
            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(-3, -4, -5) );

            Assert.Throws<InvalidTriangleException>( () => Utilities.GetTriangleArea(1, 10, 12) );
        }

        [Fact]
        public void GetMostCommonTest()
        {
            Assert.Equal(new[] {4, 5}, Utilities.GetMostCommon(new[] {5, 4, 3, 2, 4, 5, 1, 6, 1, 2, 5, 4}).OrderBy(x => x));
            Assert.Equal(new[] {1}, Utilities.GetMostCommon(new[] {1, 2, 3, 4, 5, 1, 6, 7}));
            Assert.Equal(new[] {1, 2, 3, 4, 5, 6, 7}, Utilities.GetMostCommon(new[] {1, 2, 3, 4, 5, 6, 7}));
        }

        [Fact]
        public void LinkCheckerTest()
        {
            var linkStatus = LinkChecker.GetLinkStatus(html);

            var expected = new[] {
                new LinkStatus("https://www.bing.com", isValid: true),
                new LinkStatus("https://www.microsoft.com", isValid: true),
                new LinkStatus("https://www.Idontexist.com", isValid: false)
            };
            Assert.Equal(expected.OrderBy(a => a.Url), linkStatus.OrderBy(a => a.Url));

            //check use cache //jk-todo
        }

        string html = @"<!DOCTYPE html> 
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
    }
}
