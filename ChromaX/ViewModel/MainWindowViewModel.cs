using System.Windows.Input;
using ChromaX.Service;

namespace ChromaX.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ChromaService _chromaService;


        private bool _sdkInitialized;

        public bool SdkInitialized
        {
            get => _sdkInitialized;
            set
            {
                _sdkInitialized = value;
                OnPropertyChanged(nameof(SdkInitialized));
            }
        }

        private ICommand _connectSdkCommand;

        public ICommand ConnectSdkCommand
        {
            get
            {
                if (_connectSdkCommand == null)
                {
                    _connectSdkCommand = new RelayCommand(ConnectSdk);
                }

                return _connectSdkCommand;
            }
        }

        private ICommand _disconnectSdkCommand;

        public ICommand DisconnectSdkCommand
        {
            get
            {
                if (_disconnectSdkCommand == null)
                {
                    _disconnectSdkCommand = new RelayCommand(DisconnectSdk);
                }

                return _disconnectSdkCommand;
            }
        }

        public MainWindowViewModel(ChromaService chromaService)
        {
            _chromaService = chromaService;
            _chromaService.SdkInit += (sender, eventArgs) => SdkInitialized = eventArgs.Initialized;
        }

        private void ConnectSdk(object obj)
        {
            _chromaService.Initialize();
        }

        private void DisconnectSdk(object obj)
        {
            _chromaService.UnInitialize();
        }
    }
}