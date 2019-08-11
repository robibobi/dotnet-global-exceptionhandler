using System.Windows;
using Tcoc.ExceptionHandler.ExceptionHandling;

namespace Tcoc.ExceptionHandler
{
    public partial class App : Application
    {
        private readonly WindowExceptionHandler _exceptionHandler;

        public App()
        {
            _exceptionHandler = new WindowExceptionHandler();
        }
    }
}
