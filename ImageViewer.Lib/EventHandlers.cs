using Microsoft.SPOT;

namespace ImageViewer.Lib
{
    public delegate void FileListReadyEventHandler(object sender, FileListReadyEventArgs args);
    public delegate void FilesInitialisedEventHandler(object sender, EventArgs args);
    public delegate void NavigationRequestedEventHandler(object sender, NavigationRequestedEventArgs args);
    public delegate void FileNameChangedEventHandler(object sender, FileNameChangedEventArgs args);

    public class FileListReadyEventArgs : EventArgs
    {
        public string[] FileList { get; private set; }

        public FileListReadyEventArgs(string[] fileList)
        {
            FileList = fileList;
        }
    }

    public class FileNameChangedEventArgs : EventArgs
    {
        public string FileName { get; private set; }

        public FileNameChangedEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }

    public enum NavigationDirection
    {
        Forward,
        Backward
    }

    public class NavigationRequestedEventArgs : EventArgs
    {
        public NavigationDirection Direction { get; private set; }

        public NavigationRequestedEventArgs(NavigationDirection direction)
        {
            Direction = direction;
        }
    }
}
