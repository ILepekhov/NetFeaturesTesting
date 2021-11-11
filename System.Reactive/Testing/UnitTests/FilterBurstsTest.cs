using LogicToTest;
using Microsoft.Reactive.Testing;
using System;
using System.Reactive.Linq;
using Xunit;

namespace UnitTests
{
    public class FilterBurstsTest : ReactiveTest
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

        [Fact]
        public void FilterBursts_TwoBurstsWithGapInEachBurstEmitted()
        {
            var scheduler = new TestScheduler();

            var xs = scheduler.CreateColdObservable(
                OnNext(250, 1),
                OnNext(275, 2),
                OnNext(300, 3),

                OnNext(400, -1),
                OnNext(401, -2),
                OnNext(405, -3),

                OnCompleted<int>(500));

            var testableObserver = scheduler.CreateObserver<int>();

            xs.FilterBursts(3)
                .Subscribe(testableObserver);

            scheduler.Start();

            testableObserver.Messages.AssertEqual(
                OnNext(250, 1),
                OnNext(400, -1),
                OnCompleted<int>(500));

            xs.Subscriptions.AssertEqual(Subscribe(0, 500));
        }

        [Fact]
        public void FilterBursts_InColdObservable()
        {
            var scheduler = new TestScheduler();

            var xs = scheduler.CreateColdObservable(
                OnNext(250, 1),
                OnNext(258, 2),
                OnNext(262, 3),

                OnNext(450, -1),
                OnNext(451, -2),
                OnNext(460, -3),

                OnCompleted<int>(500));

            var res = scheduler.Start(
                () => xs.FilterBursts(TimeSpan.FromTicks(10), scheduler));

            res.Messages.AssertEqual(
                OnNext(450, 1),
                OnNext(650, -1),
                OnCompleted<int>(700));

            xs.Subscriptions.AssertEqual(Subscribe(Subscribed, 700));
        }
    }
}