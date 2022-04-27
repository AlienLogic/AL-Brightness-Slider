using System;
using System.Windows;

namespace AL_Brightness_Slider
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			MainWindow = new Views.MainWindow()
			{
				DataContext = new ViewModels.MainWindowViewModel()
			};

			MainWindow.Show();
		}
	}
}
