using System.Windows;
using System.Windows.Controls;

namespace AL_Brightness_Slider.ViewModels
{
	public class MonitorPanelItemViewModel : BaseViewModel
	{
		public int PanelWidth { get; set; }
		public int PanelHeight { get; set; }
		public int SquareIconText => PanelHeight < PanelWidth ? PanelHeight : PanelWidth;
		public string LabelBrightness { get; set; }
		public Orientation PanelOrientation { get; set; }
		public Dock ImageDock => PanelOrientation == Orientation.Horizontal ? Dock.Left : Dock.Top;
		public Dock LabelDock => PanelOrientation == Orientation.Horizontal ? Dock.Right : Dock.Bottom;
		public VerticalAlignment SliderVerticalAlignment => PanelOrientation == Orientation.Horizontal ? VerticalAlignment.Center : VerticalAlignment.Stretch;
		public HorizontalAlignment SliderHorizontalAlignment => PanelOrientation == Orientation.Horizontal ? HorizontalAlignment.Stretch : HorizontalAlignment.Center;

		public MonitorPanelItemViewModel()
		{
			PanelWidth = 360;
			PanelHeight = 66;
			PanelOrientation = PanelWidth > PanelHeight ? Orientation.Horizontal : Orientation.Vertical;

			LabelBrightness = "00";
		}
	}
}
