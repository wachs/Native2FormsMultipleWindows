using Phoneword.UWP.Views;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms.Platform.UWP;

namespace Phoneword.UWP
{
    public sealed partial class MainPage : Page
    {
        [ThreadStatic]
        public static MainPage Instance;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            Instance = this;
            this.Content = new Phoneword.UWP.Views.PhonewordPage().CreateFrameworkElement();
        }

        public void NavigateToCallHistoryPage()
        {
            this.Frame.Navigate(new CallHistoryPage());
        }

        public async Task OpenNewWindow()
        {
            var windows = CoreApplication.Views;
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();


            await newAV.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            async () =>
                            {
                                var newWindow = Window.Current;
                                var newAppView = ApplicationView.GetForCurrentView();
                                Windows.UI.Xaml.Controls.Frame frameContent = new Windows.UI.Xaml.Controls.Frame();
                                frameContent.Navigate(typeof(MainPage), "");
                                newWindow.Content = frameContent;
                                newWindow.Activate();

                                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                                    newAppView.Id,
                                    ViewSizePreference.UseMinimum,
                                    currentAV.Id,
                                    ViewSizePreference.UseMinimum);

                            });
        }
    }
}
