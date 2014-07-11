using System;
using System.Windows.Forms;

namespace System
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor cursor;

        public WaitCursor()
        {
            IsWaitCursor = true;
        }

        public void Dispose()
        {
            IsWaitCursor = false;
        }
        
        public bool IsWaitCursor
        {
            get { return Application.UseWaitCursor; }
            set
            {
                if (Application.UseWaitCursor ! = value)
                {
                        Application.UseWaitCursor = value;
                        if (value)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                        }
                }
            }
        }
    }
}
