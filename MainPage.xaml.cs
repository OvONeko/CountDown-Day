using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace CountDown_Day
{
    /// <summary>
    /// Can be used on itself or to navigate to a blank page inside the Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFolder localfolder;
        public MainPage()
        {
            this.InitializeComponent();
            localfolder = ApplicationData.Current.LocalFolder;
        }
        private void Grid_Num_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
