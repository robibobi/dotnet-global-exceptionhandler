using System;
using System.Runtime.ExceptionServices;
using System.Windows;

namespace Tcoc.ExceptionHandler.Extensions
{
    static class ExceptionExtensions
    {
        public static void ThrowOnDispatcher(this Exception exc)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                // preserve the callstack of the exception
                ExceptionDispatchInfo.Capture(exc).Throw();
            }));
        }
    }
}
