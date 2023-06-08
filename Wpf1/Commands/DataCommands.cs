using System;
using System.Windows.Input;

namespace Wpf1.Commands
{
    public interface ICommand1
    {
        void Execute(object par);
        bool CanExecute(object par);
        event EventHandler CanExecuteChanged;
    }

    public class DataCommands
    {
        public static RoutedCommand Delete { get; set; }
        public static RoutedCommand Edit { get; set; }
        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            Edit = new RoutedCommand("Edit", typeof(DataCommands), inputs);
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.D, ModifierKeys.Control, "Ctrl+D"));
            Delete = new RoutedCommand("Delete", typeof(DataCommands), inputs);
        }
    }
}
