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
using Windows.Security.Authentication.Web.Core;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.Data.Json;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModelSense
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContentFrame : Page
    {
        public ContentFrame()
        {
            this.InitializeComponent();
        }

        public void SearchOrURL_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (SearchOrURL.Text.Contains("https://"))
                {
                    CurrentWebsite.Source = new Uri(SearchOrURL.Text.ToString());
                }
                else if (SearchOrURL.Text.Contains("http://"))
                {
                    CurrentWebsite.Source = new Uri(SearchOrURL.Text.ToString());
                }
                else if (SearchOrURL.Text.Contains(""))
                {
                    CurrentWebsite.Source = new Uri("https://www.google.com/search?q=" + SearchOrURL.Text.ToString());
                }
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchOrURL_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            {
                if (SearchOrURL.Text.Contains("https://"))
                {
                    CurrentWebsite.Source = new Uri(SearchOrURL.Text.ToString());
                    SearchOrURL.Text = CurrentWebsite.Source.ToString();
                }
                else if (SearchOrURL.Text.Contains("http://"))
                {
                    CurrentWebsite.Source = new Uri(SearchOrURL.Text.ToString());
                    SearchOrURL.Text = CurrentWebsite.Source.ToString();
                }
                else if (SearchOrURL.Text.Contains(""))
                {
                    CurrentWebsite.Source = new Uri("https://www.google.com/search?q=" + SearchOrURL.Text.ToString());
                    SearchOrURL.Text = CurrentWebsite.Source.ToString();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccountsSettingsPane.Show();
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested += BuildPaneAsync;
        }

        private async void BuildPaneAsync(AccountsSettingsPane s,
AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();

            e.HeaderText = "ModelSense will import your browsing data if you're signed in.";

            var msaProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync(
                "https://login.microsoft.com", "consumers");

            var command = new WebAccountProviderCommand(msaProvider, GetMsaTokenAsync);

            e.WebAccountProviderCommands.Add(command);

            deferral.Complete();
        }

        private async void GetMsaTokenAsync(WebAccountProviderCommand command)
        {
            WebTokenRequest request = new WebTokenRequest(command.WebAccountProvider, "wl.basic");
            WebTokenRequestResult result = await WebAuthenticationCoreManager.RequestTokenAsync(request);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CurrentWebsite.Reload();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CurrentWebsite.CanGoBack)
                CurrentWebsite.GoBack();
                SearchOrURL.Text = CurrentWebsite.Source.ToString();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (CurrentWebsite.CanGoForward)
                CurrentWebsite.GoForward();
                SearchOrURL.Text = CurrentWebsite.Source.ToString();
        }
    }
}
