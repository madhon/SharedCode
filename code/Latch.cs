using System;

namespace System
{
	public class Latch
	{
		private int count = 0;

		public void Increment()
		{
			count++;
		}

		public void Decrement()
		{
			count–;
		}

		public bool IsLatched
		{
			get { return count > 0; }
		}

		public void RunInsideLatch(Action handler)
		{
			Increment();
			handler();
			Decrement();
		}

		public void RunLatchedOperation(Action handler)
		{
			if (IsLatched)
			{
				return;
			}
				handler();
		}
	}
}