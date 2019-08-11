using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tcoc.ExceptionHandler.Exceptions;
using Tcoc.ExceptionHandler.Extensions;

namespace Tcoc.ExceptionHandler.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method raises an exception on the dispatcher thread.
        /// </summary>
        private void RaiseException(object sender, RoutedEventArgs e)
        {
            throw new SampleException("This is a sample exception");
        }

        /// <summary>
        /// This method raises an exception on another thread. This will
        /// terminate our application after the AppDomainUnhandledException
        /// event was raised. Our exception window will not work for this
        /// exception.
        /// </summary>
        private void RaiseExceptionOnThread(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() => { throw new SampleException("Non UI Thread Exception"); });
            t.Start();
        }

        /// <summary>
        /// To prevent an application crash without exception window,
        /// we need to catch all occuring exceptions on the worker thread
        /// and re-throw them on the dispatcher thread.
        /// </summary>
        private void RaiseExceptionOnThreadSafe(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    // Some business logic 
                    // ...
                    throw new SampleException("Sample");
                }
                catch (Exception exc)
                {
                    // Catch all exceptions on the worker thread and
                    // re-throw them on the dispatcher to prevent an
                    // application crash without exception window.
                    exc.ThrowOnDispatcher();
                }
            });
            t.Start();
        }

        /// <summary>
        /// This method creates a faulted Task object and forces
        /// a GC collection the raise the UnobservedTaskException
        /// event.
        /// </summary>
        private void RaiseExceptionOnUnobservedTask(object sender, RoutedEventArgs e)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            tcs.SetException(new Exception("Sample exception"));
            tcs = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
