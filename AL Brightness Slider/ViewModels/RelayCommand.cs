using System;
using System.Windows.Input;

namespace AL_Brightness_Slider.ViewModels
{
	class RelayCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private Action<object> executeMethod;
		private Predicate<object> canExecuteMethod;

		public RelayCommand(Action<object> executeMethod, Predicate<object> canExecuteMethod)
		{
			this.executeMethod = executeMethod;
			this.canExecuteMethod = canExecuteMethod;
		}

		public RelayCommand(Action<object> executeMethod) : this(executeMethod, null) { }

		public bool CanExecute(object parameter)
		{
			return (canExecuteMethod == null) || canExecuteMethod.Invoke(parameter);
		}

		public void Execute(object parameter)
		{
			executeMethod.Invoke(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
