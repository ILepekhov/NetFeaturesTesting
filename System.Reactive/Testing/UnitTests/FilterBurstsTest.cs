using LogicToTest;
using Microsoft.Reactive.Testing;
using System.Reactive.Linq;
using Xunit;

namespace UnitTests
{
    public class FilterBurstsTest
    {
        [Fact]
        public void FilterBursts_SourceOf10AndBurstSize5_TwoEmissions()
        {
            var sequenceSize = 10;
            var burstSize = 5;
            var expected = new[] { 0, 5 };
            var xs = Observable.Range(0, sequenceSize);

            xs.FilterBursts(burstSize)
                .AssertEqual(expected.ToObservable());
        }

        [Theory]
        [InlineData(1, 1, new[] { 0 })]
        [InlineData(5, 1, new[] { 0 })]
        [InlineData(1, 5, new[] { 0, 1, 2, 3, 4 })]
        [InlineData(5, 5, new[] { 0 })]
        [InlineData(5, 8, new[] { 0, 5 })]
        public void FilterBursts(int bufferSize, int sequenceSize, int[] expected)
        {
            var xs = Observable.Range(0, sequenceSize);

            xs.FilterBursts(bufferSize)
                .AssertEqual(expected.ToObservable());
        }
    }
}