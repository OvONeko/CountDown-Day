using Microsoft.Toolkit.Uwp.UI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
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
        public static bool tcolor = false;
        ThemeListener Listener = new ThemeListener();
        public MainPage()
        {
            this.InitializeComponent();
            Listener.ThemeChanged += Listener_ThemeChanged;
            localfolder = ApplicationData.Current.LocalFolder;
        }
        private void Grid_Num_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ReLoadItems(nowid);
        }
        private void TTime_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (tcolor)
            {
                this.TextDarkToLight.Begin();
            }
            else
            {
                this.TextLightToDark.Begin();
            }
            tcolor = !tcolor;
            if (!File.Exists(localfolder.Path + "\\global.ini"))
            {
                try
                {
                    localfolder.CreateFileAsync("global.ini", CreationCollisionOption.FailIfExists);
                }
                catch (Exception ex)
                {
                    var dialog = new MessageDialog("Cannot Create File:" + localfolder.Path + "\\global.ini\n" + ex.ToString(), ex.Message);
                    dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                    dialog.ShowAsync();
                }
                return;
            }
            string[] glb = File.ReadAllLines(localfolder.Path + "\\global.ini");
            File.Delete(localfolder.Path + "\\global.ini");
            foreach (var v in glb)
            {
                if (v.ToLower().Contains("tcolor"))
                    continue;
                else
                    File.AppendAllText(localfolder.Path + "\\global.ini", v);
            }
            File.AppendAllText(localfolder.Path + "\\global.ini", "TColor=" + Convert.ToString(tcolor));
        }
    }
}
