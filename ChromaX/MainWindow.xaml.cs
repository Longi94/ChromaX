using System.Windows;
using System.Windows.Input;
using ChromaX.Model;
using ChromaX.Mouse;
using ChromaX.ViewModel;
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

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.MouseDownCommand.Execute(new MouseArgsWithPoint
            {
                Position = System.Windows.Input.Mouse.GetPosition(this)
            });
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.MouseUpCommand.Execute(new MouseArgsWithPoint
            {
                Position = System.Windows.Input.Mouse.GetPosition(this)
            });
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.MouseMoveCommand.Execute(new MouseArgsWithPoint
            {
                Position = System.Windows.Input.Mouse.GetPosition(this),
                EventArgs = e
            });
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.MouseWheelCommand.Execute(e);
        }
    }
}
