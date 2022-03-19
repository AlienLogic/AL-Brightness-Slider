using SlimShady.MonitorManagers;

using System.Windows;
using System.Windows.Controls;

namespace AL_Brightness_Slider.Views
{
	// TODO: for HardwareMonitorManager change value only on drag ends
	// https://stackoverflow.com/questions/723502/wpf-slider-with-an-event-that-triggers-after-a-user-drags

	public partial class MonitorPanelItem : UserControl
	{
		private readonly Monitor monitor;

		public MonitorPanelItem(Monitor monitor)
		{
			InitializeComponent();
			this.monitor = monitor;
		}

		private void MonitorPanelItem_Loaded(object sender, RoutedEventArgs e)
		{
			slider.Value = monitor.Brightness;
			text.Content = monitor.Brightness;
		}

		private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			monitor.Brightness = (int)e.NewValue;
			text.Content = monitor.Brightness;
		}

		private void slider_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
		{
			if (e.Delta > 0)
				slider.Value += 5;
			else
				slider.Value -= 5;
		}
	}
}
