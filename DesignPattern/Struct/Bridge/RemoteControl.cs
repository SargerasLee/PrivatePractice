using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Struct.Bridge
{
	public class RemoteControl
	{
		protected IDevice device;

		public RemoteControl(IDevice device)
		{
			this.device = device;
		}

		public virtual void TogglePower()
		{
			if (device.IsEnabled())
				device.Disable();
			else
				device.Enable();
		}

		public virtual void VolumeDown()
		{
			device.SetVolume(device.GetVolume() - 10);
		}

		public virtual void VolumeUp()
		{
			device.SetVolume(device.GetVolume() + 10);
		}

		public virtual void ChannelDown()
		{
			device.SetChannel(device.GetChannel() - 1);
		}

		public virtual void ChannelUp()
		{
			device.SetChannel(device.GetChannel() + 1);
		}
	}

	public class AdvancedRemoteControl : RemoteControl
	{
		public AdvancedRemoteControl(IDevice device) : base(device)
		{
			
		}

		public void Mute()
		{
			device.SetVolume(0);
		}
	}

	public interface IDevice
	{
		bool IsEnabled();
		void Enable();
		void Disable();
		int GetVolume();
		void SetVolume(int d);

		int GetChannel();
		void SetChannel(int channel);
	}

	public class Tv : IDevice
	{
		private int count = 100;
		public int ChannelCount
		{ 
			get
			{
				return count;
			}
		}
		public void Disable()
		{
			throw new NotImplementedException();
		}

		public void Enable()
		{
			throw new NotImplementedException();
		}

		public int GetChannel()
		{
			throw new NotImplementedException();
		}

		public int GetVolume()
		{
			throw new NotImplementedException();
		}

		public bool IsEnabled()
		{
			throw new NotImplementedException();
		}

		public void SetChannel(int channel)
		{
			throw new NotImplementedException();
		}

		public void SetVolume(int d)
		{
			throw new NotImplementedException();
		}
	}

	public class Radio : IDevice
	{
		public void Disable()
		{
			throw new NotImplementedException();
		}

		public void Enable()
		{
			throw new NotImplementedException();
		}

		public int GetChannel()
		{
			throw new NotImplementedException();
		}

		public int GetVolume()
		{
			throw new NotImplementedException();
		}

		public bool IsEnabled()
		{
			throw new NotImplementedException();
		}

		public void SetChannel(int channel)
		{
			throw new NotImplementedException();
		}

		public void SetVolume(int d)
		{
			throw new NotImplementedException();
		}
	}
}
