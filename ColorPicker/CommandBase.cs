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

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter)
        {
            if (_canExecute?.Invoke() ?? true)
                _execute();
        }
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

        public bool CanExecute(object parameter) => _canExecute?.Invoke((TParam)parameter) ?? true;

        public void Execute(object parameter)
        {
            var tparam = (TParam)parameter;
            if (_canExecute?.Invoke(tparam) ?? true)
                _execute(tparam);
        }
    }
}
