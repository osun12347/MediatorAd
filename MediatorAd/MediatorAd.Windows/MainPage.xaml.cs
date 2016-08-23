using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.AdMediator.Windows81;
using System.Diagnostics;
using Microsoft.AdMediator.Core.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MediatorAd
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public event UnhandledExceptionEventHandler UnhandledException;
        public MainPage()
        {
            this.InitializeComponent();
            AdMediator.AdSdkError += AdMediator_AdSdkError;
            AdMediator.AdMediatorFilled += AdMediator_AdMediatorFilled;
            AdMediator.AdMediatorError += AdMediator_AdMediatorError;
            AdMediator.AdSdkEvent += AdMediator_AdSdkEvent;
            UnhandledException += App_UnhandledException;
            AdMediator.AdSdkTimeouts[AdSdkNames.MicrosoftAdvertising] = TimeSpan.FromSeconds(10);
            AdMediator.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Width"] = 300;
            AdMediator.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Height"] = 250;

        }

        private void AdMediator_AdMediatorError(object sender, Microsoft.AdMediator.Core.Events.AdMediatorFailedEventArgs e)
        {
            Debug.WriteLine("AdMediatorError:" +e.Error+""+e.ErrorCode);
        }

        private void AdMediator_AdSdkEvent(object sender, Microsoft.AdMediator.Core.Events.AdSdkEventArgs e)
        {
            Debug.WriteLine("ADSDK event {0} by {1}",e.EventName,e.Name);
        }

        private void AdMediator_AdMediatorFilled(object sender, Microsoft.AdMediator.Core.Events.AdSdkEventArgs e)
        {
            Debug.WriteLine("AdFilled:" + e.Name);
        }

        private void AdMediator_AdSdkError(object sender, Microsoft.AdMediator.Core.Events.AdFailedEventArgs e)
        {
            Debug.WriteLine("Adsdkerror by {0} errorcode: {1} errordescription:{2} error:{3}",e.Name,e.ErrorCode,e.ErrorDescription,e.Error);
        }
       private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e != null)
            {
                Exception exception = e.Exception;
                if (exception is NullReferenceException && exception.ToString().ToUpper().Contains("SOMA"))
                {
                    Debug.WriteLine("Handled Smaato null reference exception {0}", exception);
                    e.Handled = true;
                    return;
                }
            }
            // APP SPECIFIC HANDLING HERE

            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

    }
}
