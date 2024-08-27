using System;
using System.Windows.Input;

namespace Library.ViewModels.Helpers
{
    /// <summary>
    /// Command a kiválasztott keresési kritérium változásának kezelésére
    /// </summary>
    public class ComboBoxSelectionChangedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _execute;

        public ComboBoxSelectionChangedCommand(Action<object> execute)
        {
            _execute = execute;
        }
        /// <summary>
        /// végrehajtható-e a parancs
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// a parancs végrehajtása
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
