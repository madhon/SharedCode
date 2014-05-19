public static class ControlExtensions
{
  public static void Do<TControl>(this TControl control, Action<TControl> action)
    where TControl: Control
  {
    if (control.InvokeRequired)
      control.Invoke(action, control);
    else
      action(control);
  }
  
  
  public static void SafeThreadAction<T>(this T control, Action<T> call) where T : Control
		{
			if (control.IsHandleCreated &&control.InvokeRequired)
				control.Invoke(call, control);
			else
				call(control);
		}
  
  public static Y SafeThreadGet<Y, T>(this T control, Func<T, Y> call) where T : Control
		{
			IAsyncResult result = control.BeginInvoke(call, control);
			object result2 = control.EndInvoke(result);
			return (Y)result2;
		}
  
}