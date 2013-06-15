
namespace ImageViewer.Lib.Test
{
    public class Program
    {
        public static void Main()
        {
            ModelTests.MoveForwardExpectCurrentFileNameIsFirst();
            ModelTests.MoveForwardTwiceExpectCurrentFileNameIsSecond();
            ModelTests.MoveForwardFourTimesExpectCurrentFileNameIsFirst();

            ModelTests.MoveBackwardExpectCurrentFileNameIsThird();
            ModelTests.MoveBackwardTwiceExpectCurrentFileNameIsSecond();
            ModelTests.MoveBackwardFourTimesExpectCurrentFileNameIsThird();
        }
    }
}
