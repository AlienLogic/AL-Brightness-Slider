using SlimShady.MonitorManagers;

using System.Windows;
using System.Windows.Controls;

namespace AL_Brightness_Slider.Views
{
	/// <summary>
	/// Logica di interazione per MonitorPanelItem.xaml
	/// </summary>
	public partial class MonitorPanelItem : UserControl
{
		Monitor monitor;

		public MonitorPanelItem(Monitor monitor)
		{
			InitializeComponent();
			this.monitor = monitor;
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			monitor.Brightness = (int)e.NewValue;
		}
	}
}
