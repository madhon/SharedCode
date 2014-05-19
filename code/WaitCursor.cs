using System;
using System.Windows.Forms;

namespace System
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor cursor;

        public WaitCursor()
        {
            cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = cursor;
        }
    }
}
