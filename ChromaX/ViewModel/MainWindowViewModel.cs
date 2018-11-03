using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using ChromaX.Model;
using ChromaX.Service;
using log4net;
using log4net.Repository.Hierarchy;
using Xceed.Wpf.Toolkit;

namespace ChromaX.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ChromaService _chromaService;

        public MainWindowViewModel(ChromaService chromaService)
        {
            _chromaService = chromaService;
            _chromaService.SdkInit += (sender, eventArgs) => SdkInitialized = eventArgs.Initialized;

            AddAvailableColors();
            CreateKeyboardGrid();
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
                    _connectSdkCommand = new RelayCommand<object>(ConnectSdk);
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
                    _disconnectSdkCommand = new RelayCommand<object>(DisconnectSdk);
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

        #region Keyboard Preview

        private static readonly int CellSize = 50;

        private readonly ObservableCollection<PreviewCellViewModel> _previewCells = new ObservableCollection<PreviewCellViewModel>();

        public IEnumerable<PreviewCellViewModel> PreviewCells => _previewCells;

        public void CreateKeyboardGrid()
        {
            for (int i = 0; i < Constants.KbRows; i++)
            {
                for (int j = 0; j < Constants.KbColumns; j++)
                {
                    _previewCells.Add(new PreviewCellViewModel
                    {
                        Color = new SolidColorBrush(ChromaColor.Black.Color),
                        Row = i,
                        Column = j,
                        X = CellSize * j,
                        Y = CellSize * i,
                        Size = CellSize
                    });
                }
            }
        }


        private ICommand _setCellColorCommand;

        public ICommand SetCellColorCommand
        {
            get
            {
                if (_setCellColorCommand == null)
                {
                    _setCellColorCommand = new RelayCommand<PreviewCellViewModel>(SetCellColor);
                }

                return _setCellColorCommand;
            }
        }
        private void SetCellColor(PreviewCellViewModel cell)
        {
            cell.Color = new SolidColorBrush(_selectedColor);
        }

        #endregion
    }
}
