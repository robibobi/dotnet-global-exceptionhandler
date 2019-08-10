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

        private void RaiseException(object sender, RoutedEventArgs e)
        {
            throw new SampleException("This is a sample exception");
        }

        private void RaiseExceptionOnThread(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() => { throw new SampleException("Non UI Thread Exception"); });
            t.Start();
        }

        private void RaiseExceptionOnThreadSafe(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    throw new SampleException("Sample");
                }
                catch (Exception exc)
                {
                    exc.ThrowOnDispatcher();
                }
            });
            t.Start();
        }

        private void RaiseExceptionOnUnobservedTask(object sender, RoutedEventArgs e)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            tcs.SetException(new Exception("Sample exception"));
            tcs = null;

            Console.WriteLine("GC: Collecting");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("GC: Collected");
        }
    }
}
