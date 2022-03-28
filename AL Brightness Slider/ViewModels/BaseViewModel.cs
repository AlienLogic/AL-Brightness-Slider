using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AL_Brightness_Slider.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool IsDesignMode => DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
	}
}
