using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace SlimShady.MonitorManagers
{
	public abstract class Monitor
	{
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>Name of the monitor rendered to the user</summary>
		public readonly string Name;

		public MonitorManager Manager;

		protected Monitor(string name, MonitorManager manager)
		{
			Name = name;
			Manager = manager;
		}

		public bool SupportsBrightness = false;
		public int MinBrightness = 0;
		public int MaxBrightness = 100;
		public abstract int Brightness { get; set; }

		public bool SupportsContrast = false;
		public int MinContrast = 0;
		public int MaxContrast = 100;
		public abstract int Contrast { get; set; }

		public bool SupportsTemperature = false;
		public int MinTemperature = 4000;
		public int MaxTemperature = 11500;
		public DoubleCollection SupportedColorTemperaturesList = null; // If not null --> use this instead of min/max
		public abstract int Temperature { get; set; }

		public override string ToString()
		{
			return Name;
		}

		protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected int Lerp(int from, int to, int value)
		{
			float fValue = (float)value / 100;
			return (int)((to - from) * fValue + from);
		}

		public string ToStringDbg()
		{
			string temperatures = SupportedColorTemperaturesList != null ? string.Join(",", SupportedColorTemperaturesList) : "";
			return Name + " { "
				+ (SupportsBrightness ? "T" : "F") + " " + MinBrightness + "-" + Brightness + "-" + MaxBrightness + "; "
				+ (SupportsContrast ? "T" : "F") + " " + MinContrast + "-" + Contrast + "-" + MaxContrast + "; "
				+ (SupportsTemperature ? "T" : "F") + " " + MinTemperature + "-" + Temperature + "-" + MaxTemperature + "(" + temperatures + ") "
				+ "}";
		}
	}
}
