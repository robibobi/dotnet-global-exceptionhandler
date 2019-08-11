using System;
using System.Windows;
using Tcoc.ExceptionHandler.ViewModels;
using Tcoc.ExceptionHandler.Windows;

namespace Tcoc.ExceptionHandler.ExceptionHandling
{
    /// <summary>
    /// This ExceptionHandler implementation opens a new
    /// error window for every unhandled exception that occurs.
    /// </summary>
    class WindowExceptionHandler : GlobalExceptionHandlerBase
    {
        /// <summary>
        /// This method opens a new ExceptionWindow with the
        /// passed exception object as datacontext.
        /// </summary>
        public override void OnUnhandledException(Exception e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                var exceptionWindow = new ExceptionWindow();
                exceptionWindow.DataContext = new ExceptionWindowVM(e);
                exceptionWindow.Show();
            }));
        } 
    }
}
