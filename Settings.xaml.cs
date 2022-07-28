using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CountDown_Day
{
    public sealed partial class Settings : ContentDialog
    {
        public Settings()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void BOpen_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
            this.Hide();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void BGBg_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FGBg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
