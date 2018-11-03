using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChromaX.ViewModel
{
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName, ICommand command)
        {
            DisplayName = displayName;
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public ICommand Command { get; }
    }
}