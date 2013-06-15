
namespace ImageViewer.Lib
{
    public interface IModel
    {
        event FileNameChangedEventHandler FileNameChanged;
        
        void SetFileNames(string[] fileNames);
        void ChangeFile(NavigationDirection navigationDirection);
        string CurrentFileName { get; }
    }
}
