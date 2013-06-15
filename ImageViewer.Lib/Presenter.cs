
namespace ImageViewer.Lib
{
    public class Presenter
    {
        private IModel _model;
        private IView _view;

        public Presenter(IView view, IModel model)
        {
            _view = view;
            _model = model;
            _view.FileListReady += new FileListReadyEventHandler(_view_FileListReady);
            _view.NavigationRequested += new NavigationRequestedEventHandler(_view_NavigationRequested);
            _model.FileNameChanged += new FileNameChangedEventHandler(_model_FileNameChanged);
        }

        void _model_FileNameChanged(object sender, FileNameChangedEventArgs args)
        {
            _view.DisplayFile(args.FileName);
        }

        void _view_NavigationRequested(object sender, NavigationRequestedEventArgs args)
        {
            _model.ChangeFile(args.Direction);
        }

        void _view_FileListReady(object sender, FileListReadyEventArgs args)
        {
            _model.SetFileNames(args.FileList);
        }
    }
}
