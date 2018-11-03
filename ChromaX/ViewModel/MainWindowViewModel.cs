using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using ChromaX.Model;
using ChromaX.Service;
using Xceed.Wpf.Toolkit;

namespace ChromaX.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ChromaService _chromaService;

        public MainWindowViewModel(ChromaService chromaService)
        {
            _chromaService = chromaService;
            _chromaService.SdkInit += (sender, eventArgs) => SdkInitialized = eventArgs.Initialized;

            AddAvailableColors();
        }

        #region Status Bar

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

        private void ConnectSdk(object obj)
        {
            _chromaService.Initialize();
        }

        private void DisconnectSdk(object obj)
        {
            _chromaService.UnInitialize();
        }

        #endregion

        #region Color Picker

        private Color _selectedColor = ChromaColor.White.Color;

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        private readonly ObservableCollection<ColorItem> _availableColors = new ObservableCollection<ColorItem>();

        public IEnumerable<ColorItem> AvailableColors => _availableColors;

        private void AddAvailableColors()
        {
            _availableColors.Add(new ColorItem(ChromaColor.White.Color, "White"));
            _availableColors.Add(new ColorItem(ChromaColor.Black.Color, "Black"));
            _availableColors.Add(new ColorItem(ChromaColor.Red.Color, "Red"));
            _availableColors.Add(new ColorItem(ChromaColor.Green.Color, "Green"));
            _availableColors.Add(new ColorItem(ChromaColor.Blue.Color, "Blue"));
            _availableColors.Add(new ColorItem(ChromaColor.Cyan.Color, "Cyan"));
            _availableColors.Add(new ColorItem(ChromaColor.Magenta.Color, "Magenta"));
            _availableColors.Add(new ColorItem(ChromaColor.Yellow.Color, "Yellow"));
            _availableColors.Add(new ColorItem(ChromaColor.HotPink.Color, "HotPink"));
            _availableColors.Add(new ColorItem(ChromaColor.Orange.Color, "Orange"));
            _availableColors.Add(new ColorItem(ChromaColor.Purple.Color, "Purple"));
        }

        #endregion
    }
}
