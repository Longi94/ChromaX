﻿using System.Windows;
using ChromaX.Model;
using Xceed.Wpf.Toolkit;

namespace ChromaX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorPicker.AvailableColors.Clear();
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.White.Color, "White"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Black.Color, "Black"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Red.Color, "Red"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Green.Color, "Green"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Blue.Color, "Blue"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Cyan.Color, "Cyan"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Magenta.Color, "Magenta"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Yellow.Color, "Yellow"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.HotPink.Color, "HotPink"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Orange.Color, "Orange"));
            ColorPicker.AvailableColors.Add(new ColorItem(ChromaColor.Purple.Color, "Purple"));

            ColorPicker.SelectedColor = ChromaColor.White.Color;
        }
    }
}
