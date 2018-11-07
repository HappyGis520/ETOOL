using System;
using System.Windows.Input;

namespace EllaMakerTool.WPF.Helper.Command
{
    public class ShowLoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
