using SlimShady.MonitorManagers;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace AL_Brightness_Slider.Views
{
	public partial class MainWindow : Window
	{
		private readonly List<Monitor> monitors = new List<Monitor>();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			switch (Environment.OSVersion.Version.Major)
			{
				case 6:
					switch (Environment.OSVersion.Version.Minor)
					{
						case 1: // Windows 7
							break;
						case 2: // Windows 8
						case 3: // Windows 8.1
							break;
					}
					break;
				case 10: // Windows 10
					ResizeMode = ResizeMode.NoResize;
					break;
			}

			loadMonitorsPanel();
		}

		private void loadMonitorsPanel(bool reload = false)
		{
			if (reload)
				monitors.Clear();

			monitors.AddRange(WmiMonitorManager.Instance.GetMonitorsList());
			monitors.AddRange(HardwareMonitorManager.Instance.GetMonitorsList());

			foreach (Monitor monitor in monitors)
				monitorsPanel.Children.Add(new MonitorPanelItem(monitor));
		}

		private void monitorsPanel_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Height = monitorsPanel.ActualHeight;

			Top = Screen.PrimaryScreen.WorkingArea.Height - Height;
			Left = Screen.PrimaryScreen.WorkingArea.Width - Width;
		}
	}
}
