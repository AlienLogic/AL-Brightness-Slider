using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Screen = System.Windows.Forms.Screen;

namespace AL_Brightness_Slider.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private int offset = 0;

		public int WindowWidth { get; set; }
		public int WindowHeight { get; set; }
		public int WindowTop
		{
			get => Screen.PrimaryScreen.WorkingArea.Height - WindowHeight - offset;
			set { }
		}
		public int WindowLeft
		{
			get => Screen.PrimaryScreen.WorkingArea.Width - WindowWidth - offset;
			set { }
		}

		public SolidColorBrush BackgroundColor { get; }
		public ResizeMode ResizeMode { get; }
		public Orientation WindowOrientation { get; }
		public Orientation PanelOrientation => WindowOrientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;

		public MainWindowViewModel()
		{
			switch (Environment.OSVersion.Version.Major)
			{
				case 6:
					switch (Environment.OSVersion.Version.Minor)
					{
						case 1: // Windows 7
							WindowWidth = 68;
							WindowHeight = 289;
							WindowOrientation = Orientation.Vertical;
							ResizeMode = ResizeMode.CanResize;
							BackgroundColor = Brushes.White;
							offset = 8;
							break;

						case 2: // Windows 8
						case 3: // Windows 8.1
							break;
					}
					break;

				case 10: // Windows 10
					WindowWidth = 360;
					WindowHeight = 40;
					WindowOrientation = Orientation.Horizontal;
					ResizeMode = ResizeMode.NoResize;
					BackgroundColor = new SolidColorBrush(Color.FromRgb(31, 31, 31));
					break;
			}

			WindowTop = 0;
			WindowLeft = 0;
		}
	}
}
