using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeShop
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        /* CanExecuteChanged 이벤트를 CommandManager의 RequerySuggested 이벤트에 연결 
        CanExecute method가 호출되어 CanExecute상태가 변경되면 CanExcuteChanged 이벤트가 발생 
        CommandManager가 명령 실행 기능이 변경되었다고 생각할 때마다(실제로 변경되지 않은 경우에도) CanExecuteChanged를 발생 */
        public event EventHandler CanExecuteChanged
        {
            // value : Delegate
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
    }
}

