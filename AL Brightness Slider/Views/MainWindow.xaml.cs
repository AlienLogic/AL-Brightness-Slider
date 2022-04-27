using SlimShady.MonitorManagers;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;

namespace AL_Brightness_Slider.Views
{
	public partial class MainWindow : Window
	{
		[DllImport("DwmApi.dll")]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		private const int WM_NCHITTEST = 0x0084;
		private const int HTBORDER = 18;
		private const int HTBOTTOM = 15;
		private const int HTBOTTOMLEFT = 16;
		private const int HTBOTTOMRIGHT = 17;
		private const int HTLEFT = 10;
		private const int HTRIGHT = 11;
		private const int HTTOP = 12;
		private const int HTTOPLEFT = 13;
		private const int HTTOPRIGHT = 14;

		private readonly NotifyIcon notifyIcon = new NotifyIcon();
		private readonly List<Monitor> monitors = new List<Monitor>();

		public MainWindow()
		{
			InitializeComponent();
		}

		#region Events
		private void mainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			loadMonitorsPanel();
			createNotifyIConContexMenu();
		}

		private void mainWindow_Activated(object sender, EventArgs e)
		{
			_ = Focus();
			Visibility = Visibility.Visible;
		}

		private void mainWindow_Deactivated(object sender, EventArgs e)
		{
			Visibility = Visibility.Hidden;
		}

		private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			notifyIcon.Dispose();
		}

		private void notifyIcon_Click(object sender, EventArgs e)
		{
			if (Visibility == Visibility.Hidden)
			{
				Visibility = Visibility.Visible;
				Activate();
			}
			else
				Visibility = Visibility.Hidden;
		}
		#endregion Events

		#region Private Methods
		private void createNotifyIConContexMenu()
		{
			ContextMenu contextMenu = new ContextMenu();

			_ = contextMenu.MenuItems.Add(new MenuItem("Exit", (snd, ev) =>
			{
				Close();
			}));

			/*_ = contextMenu.MenuItems.Add(new MenuItem("Start with Windows", (snd, ev) =>
			{
				// ...
			}));*/

			if (System.Diagnostics.Debugger.IsAttached)
			{
				_ = contextMenu.MenuItems.Add(new MenuItem("Reload monitors", (snd, ev) =>
				{
					loadMonitorsPanel(true);
				}));
			}

			notifyIcon.ContextMenu = contextMenu;
			notifyIcon.Icon = new System.Drawing.Icon(@"icon.ico");
			notifyIcon.Visible = true;
			notifyIcon.Text = "Brightness";
			notifyIcon.Click += notifyIcon_Click;
		}

		private void loadMonitorsPanel(bool reload = false)
		{
			if (reload)
			{
				monitors.Clear();
				monitorsPanelItems.Children.Clear();
			}

			monitors.AddRange(WmiMonitorManager.Instance.GetMonitorsList());
			monitors.AddRange(HardwareMonitorManager.Instance.GetMonitorsList());

			foreach (Monitor monitor in monitors)
				_ = monitorsPanelItems.Children.Add(new MonitorPanelItem(monitor)
				{
					DataContext = new ViewModels.MonitorPanelItemViewModel()
				});
		}

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			// Override the window hit test
			// and if the cursor is over a resize border,
			// return a standard border result instead.
			if (msg == WM_NCHITTEST)
			{
				handled = true;
				int htLocation = DefWindowProc(hwnd, msg, wParam, lParam).ToInt32();
				switch (htLocation)
				{
					case HTBOTTOM:
					case HTBOTTOMLEFT:
					case HTBOTTOMRIGHT:
					case HTLEFT:
					case HTRIGHT:
					case HTTOP:
					case HTTOPLEFT:
					case HTTOPRIGHT:
						htLocation = HTBORDER;
						break;
				}

				return new IntPtr(htLocation);
			}

			return IntPtr.Zero;
		}
		#endregion Private Methods

		#region Public Methods
		public void SetWindows7Borders()
		{
			try
			{
				// Obtain the window handle for WPF application
				IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
				HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
				mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);
				mainWindowSrc.AddHook(WndProc);

				// Set Margins
				MARGINS margins = new MARGINS
				{
					cxLeftWidth = 4,
					cxRightWidth = 4,
					cyBottomHeight = 4,
					cyTopHeight = 4
				};

				int hr = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
				if (hr < 0)
				{
					//DwmExtendFrameIntoClientArea Failed
				}
			}
			catch (DllNotFoundException) // If not Vista, paint background white.
			{
				Background = Brushes.White;
			}
		}
		#endregion Public Methods
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MARGINS
	{
		public int cxLeftWidth;    // width of left border that retains its size
		public int cxRightWidth;   // width of right border that retains its size
		public int cyTopHeight;    // height of top border that retains its size
		public int cyBottomHeight; // height of bottom border that retains its size
	};
}
