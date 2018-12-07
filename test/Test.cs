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
            Assert.Equal(true, Utilities.IsNullOrEmpty(null));
            Assert.Equal(false, Utilities.IsNullOrEmpty("a"));
            Assert.Equal(true, Utilities.IsNullOrEmpty(""));
            Assert.Equal(false, Utilities.IsNullOrEmpty("null"));
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
    }
}
