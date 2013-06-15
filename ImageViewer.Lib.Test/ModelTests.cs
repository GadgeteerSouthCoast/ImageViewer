using MJTest;

namespace ImageViewer.Lib.Test
{
    static class ModelTests
    {
        private static IModel _target;

        public static void MoveForwardExpectCurrentFileNameIsFirst()
        {
            GivenIHaveInitialisedAModelWith3FileNames();
            WhenIMoveForward(1);
            ThenTheCurrentFileNameIs("One");
        }

        public static void MoveForwardTwiceExpectCurrentFileNameIsSecond()
        {
            GivenIHaveInitialisedAModelWith3FileNames();
            WhenIMoveForward(2);
            ThenTheCurrentFileNameIs("Two");
        }

        public static void MoveForwardFourTimesExpectCurrentFileNameIsFirst()
        {
            GivenIHaveInitialisedAModelWith3FileNames();
            WhenIMoveForward(4);
            ThenTheCurrentFileNameIs("One");
        }

        public static void MoveBackwardExpectCurrentFileNameIsThird()
        {
            GivenIHaveInitialisedAModelWith3FileNames();
            WhenIMoveBackward(1);
            ThenTheCurrentFileNameIs("Three");
        }

        public static void MoveBackwardTwiceExpectCurrentFileNameIsSecond()
        {
            GivenIHaveInitialisedAModelWith3FileNames();
            WhenIMoveBackward(2);
            ThenTheCurrentFileNameIs("Two");
        }

        public static void MoveBackwardFourTimesExpectCurrentFileNameIsThird()
        {
            GivenIHaveInitialisedAModelWith3FileNames();
            WhenIMoveBackward(4);
            ThenTheCurrentFileNameIs("Three");
        }

        private static void GivenIHaveInitialisedAModelWith3FileNames()
        {
            _target = new Model();
            _target.SetFileNames(new[] { "One", "Two", "Three" });
        }

        private static void WhenIMoveBackward(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                _target.ChangeFile(NavigationDirection.Backward);
            }
        }

        private static void WhenIMoveForward(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                _target.ChangeFile(NavigationDirection.Forward);
            }
        }

        private static void ThenTheCurrentFileNameIs(string expected)
        {
            Assert.That("The filename isn't " + expected, _target.CurrentFileName == expected);
        }
    }
}
