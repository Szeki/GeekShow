using GeekShow.Component;
using GeekShow.Core.Component;
using GeekShow.Shared.Component;
using GeekShow.Shared.Service;
using GeekShow.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace GeekShow
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        #region Members

        private TransitionCollection transitions;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            RegisterTypes();
            ResolveDefaultTypes();

            if (!Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("AppVersion"))
            {
                string appVersion = string.Format("{0}.{1}.{2}.{3}",
                    Package.Current.Id.Version.Build,
                    Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Revision);

                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Add("AppVersion", appVersion);
            }

            RegisterBackgroundTasks();

            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
            this.Resuming += OnResuming;
        }

        #endregion

        #region Private Methods

        private void RegisterTypes()
        {
            IoC.BuildContainer();
        }

        private void ResolveDefaultTypes()
        {
            ViewModelBase.PersistManager = IoC.Resolve<ITvShowPersistManager>();
        }

        private void RegisterBackgroundTasks()
        {
            BackgroundTaskAccessHelper.CheckAppVersion();

            BackgroundTaskRegistrationHelper.RegisterBackgroundTask("GeekShow.Tasks.UpdateShowsTask", "UpdateShows",
                new TimeTrigger(120, false),
                new SystemCondition(SystemConditionType.UserNotPresent | SystemConditionType.InternetAvailable));

            BackgroundTaskRegistrationHelper.RegisterBackgroundTask("GeekShow.Tasks.NotifyShowsTask", "NotifyShows",
                new TimeTrigger(120, false),
                new SystemCondition(SystemConditionType.UserNotPresent));
        }

        private void UpdateTvShowsIfNecessary()
        {
            if (!Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("UpdateSynched"))
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Add("UpdateSynched", true);
            }
            else if (!(bool)Windows.Storage.ApplicationData.Current.LocalSettings.Values["UpdateSynched"])
            {
                ViewModelBase.UpdateTvShows();

                Windows.Storage.ApplicationData.Current.LocalSettings.Values["UpdateSynched"] = true;
            }
        }

        #endregion

        #region Eventhandlers

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            //Loads TvShows from storage if there was an update
            UpdateTvShowsIfNecessary();

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            
            deferral.Complete();
        }

        private void OnResuming(object sender, object e)
        {
            //Loads TvShows from storage if there was an update
            UpdateTvShowsIfNecessary();
        }

        #endregion
    }
}