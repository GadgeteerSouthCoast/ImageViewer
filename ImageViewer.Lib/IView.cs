
namespace ImageViewer.Lib
{
    public interface IView
    {
        event FileListReadyEventHandler FileListReady;
        event NavigationRequestedEventHandler NavigationRequested;
        void DisplayFile(string fileName);
    }
}
