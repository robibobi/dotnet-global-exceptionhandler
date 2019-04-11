using System;
using System.Runtime.ExceptionServices;
using System.Windows;
using Tcoc.ExceptionHandler.ViewModels;
using Tcoc.ExceptionHandler.Windows;

namespace Tcoc.ExceptionHandler.ExceptionHandling
{
    class WindowExceptionHandler : GlobalExceptionHandlerBase
    {
        public override void OnUnhandledException(Exception e)
        {
            // We might be on a worker thread.
            var operation = Application.Current.Dispatcher.BeginInvoke((Action)(() => {
                var exceptionWindow = new ExceptionWindow();
                exceptionWindow.DataContext = new ExceptionWindowVM(e);
                exceptionWindow.Show();
            }));
            operation.Wait();
        }

        private async void AsyncEventHandler()
        {
            try
            {
                //...
            } catch(Exception e)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ExceptionDispatchInfo.Capture(e).Throw();
                });
            }
        }
    }
}
