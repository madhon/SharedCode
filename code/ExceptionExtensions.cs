using System;
using System.Diagnostics;
using System.Text;

namespace System
{
    [DebuggerStepThrough]
    public static class ExceptionExtensions
    {
        public static string GetAllMessages(this Exception exception)
        {
            if (exception.InnerException == null)
                return string.Format("{0}: {1}", exception.GetType().FullName, exception.Message);

            var message = new StringBuilder();
            message.Append(exception.GetType().FullName);
            message.Append(": ");
            message.Append(exception.Message);

            exception = exception.InnerException;

            do
            {
                message.Append(" ---> ");
                message.Append(exception.GetType().FullName);
                message.Append(": ");
                message.Append(exception.Message);

                exception = exception.InnerException;
            } while (exception != null);

            return message.ToString();
        }

    }
}
