using System;
using System.Windows.Input;

namespace Oniqys.Wpf
{
    /// <summary>
    /// <see cref="ICommand"/>の一部を実装したコマンド用の基底クラスです。
    /// </summary>
    public abstract class CommandBase
    {
        public event EventHandler CanExecuteChanged;

        public void RiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// パラメーターを使用しないコマンドです。
    /// パラメーターがある場合実行できません。
    /// </summary>
    public class Command : CommandBase, ICommand
    {
        private readonly Func<bool> _canExecute;

        private readonly Action _execute;

        public Command(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute() => _canExecute?.Invoke() ?? true;

        public void Execute()
        {
            if (_canExecute?.Invoke() ?? true)
                _execute();
        }

        bool ICommand.CanExecute(object parameter) => CanExecute();

        void ICommand.Execute(object parameter) => Execute();
    }

    /// <summary>
    /// パラメーターを使用するコマンドです。
    /// </summary>
    /// <typeparam name="TParam">パラメターの型</typeparam>
    public class Command<TParam> : CommandBase, ICommand
    {
        private readonly Func<TParam, bool> _canExecute;

        private readonly Action<TParam> _execute;

        public Command(Action<TParam> execute, Func<TParam, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(TParam parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(TParam parameter)
        {
            if (_canExecute?.Invoke(parameter) ?? true)
                _execute(parameter);
        }

        bool ICommand.CanExecute(object parameter) => CanExecute((TParam)parameter);

        void ICommand.Execute(object parameter) => Execute((TParam)parameter);
    }
}
