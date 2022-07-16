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

namespace CountDown_Day
{
    public class drop_down_list
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int IDS { get; set; }
    }
    public sealed partial class MainPage : Page
    {
        enum ForeDate
        {
            Past = 1,
            Future = -1,
            Now = 0
        }
        static ForeDate GetForeDate(DateTime dateTime)
        {
            int i = DateTime.Now.CompareTo(dateTime);
            if (i == 0)
                return ForeDate.Now;
            else if (i < 0)
                return ForeDate.Future;
            else if (i > 0)
                return ForeDate.Past;
            else
                throw new Exception();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(localfolder.Path + "\\config.ini"))
            {
                try
                {
                    localfolder.CreateFileAsync("config.ini", CreationCollisionOption.FailIfExists);
                }
                catch
                {
                    var dialog = new MessageDialog("Cannot Create File:" + localfolder.Path + "\\config.ini");
                    dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                    dialog.ShowAsync();
                }
            }

        }
    }
}