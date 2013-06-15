using GHI.Premium.USBHost;
using ImageViewer.Lib;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;


//TODO: centre on screen
//TODO: sort by filename


namespace ImageViewer
{
    public partial class Program : IView
    {
        private Presenter _presenter;
        private GT.StorageDevice _sdCardDevice;
        private USBH_Mouse _mouse;

        public event FileListReadyEventHandler FileListReady;
        public event NavigationRequestedEventHandler NavigationRequested;

        public void DisplayFile(string fileName)
        {
            var bitmap = new Bitmap(320, 240);
            var image = _sdCardDevice.LoadBitmap(fileName, Bitmap.BitmapImageType.Jpeg);
            bitmap.DrawImage((320 - image.Width) / 2, (240 - image.Height) / 2, image, 0, 0, image.Width, image.Height);
            bitmap.Flush();
        }

        private void ProgramStarted()
        {
            InitialisePresenter();
            AttachEventHandlers();
            HandleSdCardStatus();
        }

        private void HandleSdCardStatus()
        {
            if (!sdCardModule.IsCardInserted)
            {
                ShowSDCardMissingMessage();
            }
            else
            {
                InitialiseModel(sdCardModule.GetStorageDevice());
            }
        }

        private void InitialisePresenter()
        {
            _presenter = new Presenter(this, new Model());
        }

        private void AttachEventHandlers()
        {
            usbHost.MouseConnected += new GTM.GHIElectronics.UsbHost.MouseConnectedEventHandler(usbHost_MouseConnected);
            sdCardModule.SDCardMounted += new GTM.GHIElectronics.SDCard.SDCardMountedEventHandler(sdCard_SDCardMounted);
            sdCardModule.SDCardUnmounted += new GTM.GHIElectronics.SDCard.SDCardUnmountedEventHandler(sdCard_SDCardUnmounted);
            display_T35.WPFWindow.TouchDown += new Microsoft.SPOT.Input.TouchEventHandler(WPFWindow_TouchDown);
        }

        private void WPFWindow_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            if (e.Touches[0].X > 220)
            {
                OnNavigationRequested(NavigationDirection.Forward);
            }
            else if (e.Touches[0].X < 100)
            {
                OnNavigationRequested(NavigationDirection.Backward);
            }
        }

        private void sdCard_SDCardUnmounted(GTM.GHIElectronics.SDCard sender)
        {
            _sdCardDevice = null;
            ShowSDCardMissingMessage();
        }

        private void ShowSDCardMissingMessage()
        {
            var bitmap = new Bitmap(320, 240);
            bitmap.DrawText("SD Card is not mounted", Resources.GetFont(Resources.FontResources.NinaB), Colors.Yellow, 50, 100);
            bitmap.Flush();
        }

        private void sdCard_SDCardMounted(GTM.GHIElectronics.SDCard sender, GT.StorageDevice SDCard)
        {
            InitialiseModel(SDCard);
        }

        private void InitialiseModel(GT.StorageDevice SDCard)
        {
            _sdCardDevice = SDCard;
            OnFileListReady(_sdCardDevice.ListFiles("Thumbs"));
            OnNavigationRequested(NavigationDirection.Forward);
        }

        private void usbHost_MouseConnected(GTM.GHIElectronics.UsbHost sender, USBH_Mouse mouse)
        {
            _mouse = mouse;
            _mouse.MouseDown += new USBH_MouseEventHandler(_mouse_MouseDown);
            _mouse.MouseWheel += new USBH_MouseEventHandler(_mouse_MouseWheel);
        }

        private void _mouse_MouseWheel(USBH_Mouse sender, USBH_MouseEventArgs args)
        {
            var mwp = args.DeltaPosition.ScrollWheelValue;
            if (mwp > 0) // mwp is the number of pixels moved since last scrollinterrupt
            {
                OnNavigationRequested(NavigationDirection.Backward);
            }
            else
            {
                OnNavigationRequested(NavigationDirection.Forward);
            }
        }

        private void _mouse_MouseDown(USBH_Mouse sender, USBH_MouseEventArgs args)
        {
            if (_sdCardDevice != null)
            {
                OnNavigationRequested(args.ChangedButton == USBH_MouseButton.Left ? NavigationDirection.Backward : NavigationDirection.Forward);
            }
        }

        private void OnNavigationRequested(NavigationDirection navigationDirection)
        {
            if (NavigationRequested != null)
            {
                NavigationRequested(this, new NavigationRequestedEventArgs(navigationDirection));
            }
        }

        private void OnFileListReady(string[] fileNames)
        {
            if (FileListReady != null)
            {
                FileListReady(this, new FileListReadyEventArgs(fileNames));
            }
        }
    }
}
