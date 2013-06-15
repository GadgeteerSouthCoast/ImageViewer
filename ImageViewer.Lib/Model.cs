
using System.Collections;
namespace ImageViewer.Lib
{
    public class Model : IModel
    {
        private string[] _fileNames;
        private int _fileIndex;

        public event FileNameChangedEventHandler FileNameChanged;

        public void SetFileNames(string[] fileNames)
        {
            _fileNames = fileNames;
            _fileIndex = -1;
        }

        public void ChangeFile(NavigationDirection navigationDirection)
        {
            if (navigationDirection == NavigationDirection.Forward)
            {
                MoveForward();
            }
            else
            {
                MoveBackward();
            }
            OnFileNameChanged();
        }

        private void OnFileNameChanged()
        {
            if (FileNameChanged != null)
            {
                FileNameChanged(this, new FileNameChangedEventArgs(CurrentFileName));
            }
        }

        private void MoveBackward()
        {
            _fileIndex--;
            if (_fileIndex < 0)
            {
                _fileIndex = _fileNames.Length - 1;
            }
        }

        private void MoveForward()
        {
            _fileIndex++;
            if (_fileIndex >= _fileNames.Length)
            {
                _fileIndex = 0;
            }
        }

        public string CurrentFileName
        {
            get
            {
                return _fileNames[_fileIndex];
            }
        }
    }
}
