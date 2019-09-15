using System;
using System.Windows.Input;

namespace Oniqys.Wpf
{
    /// <summary>
    /// <see cref="ICommand"/>の一部を実装したコマンド用の基底クラスです。
    /// </summary>
    public abstract class CommandBase
    {
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// <see cref="CanExecuteChanged"/>を発射します。
        /// </summary>
        protected void RiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// パラメーターを使用しないコマンドです。
    /// パラメーターがある場合実行できません。
    /// </summary>
    public class Command : CommandBase, ICommand
    {
        private readonly Func<bool> _canExecute;

        private readonly Action _execute;

        /// <summary>
        /// コンストラクターです。
        /// </summary>
        public Command(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        public bool CanExecute(object _) => _canExecute?.Invoke() ?? true;

        /// <inheritdoc />
        public void Execute(object _)
        {
            if (CanExecute(_))
                _execute?.Invoke();
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

        /// <summary>
        /// コンストラクターです。
        /// </summary>
        public Command(Action<TParam> execute, Func<TParam, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// <see cref="ICommand.CanExecute(object)"/>を内部で実装します。
        /// </summary>
        public bool CanExecute(TParam parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// <see cref="ICommand.Execute(object)"/>を内部で実装します。
        /// </summary>
        public void Execute(TParam parameter)
        {
            if (CanExecute(parameter))
                _execute(parameter);
        }

        /// <inheritdoc />
        bool ICommand.CanExecute(object parameter) => CanExecute((TParam)parameter);

        /// <inheritdoc />
        void ICommand.Execute(object parameter) => Execute((TParam)parameter);
    }
}
