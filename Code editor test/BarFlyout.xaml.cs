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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Code_editor_test
{
    public sealed partial class BarFlyout : UserControl
    {
        public event EventHandler BackdropTapped;
     
        public BarFlyout()
        {
            this.InitializeComponent();
        }
        private void Backdrop_Tapped(object sender, TappedRoutedEventArgs e) => BackdropTapped?.Invoke(this, new EventArgs());
        public void MyFancyPanel_BackdrpClicked(object sender, RoutedEventArgs e)
        {
            // MainPage m = new MainPage();
            // m.MyFancyPanel_BackdropClicked();
            UnloadObject(StackOverFlow);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if(webView.CanGoForward == true)
            {
                webView.GoForward();
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (webView.CanGoBack == true)
            {
                webView.GoBack();
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            webView.Navigate(new Uri("https://stackoverflow.com/search?q=" + args.QueryText));
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            webView.Refresh();
        }

        private async void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(webView.Source);
        }

        private async void webView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            await Windows.System.Launcher.LaunchUriAsync(args.Uri);
        }
    }
}
