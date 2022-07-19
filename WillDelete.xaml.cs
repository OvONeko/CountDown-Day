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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace CountDown_Day
{
    public sealed partial class WillDelete : ContentDialog
    {
        int argid = -1;
        public WillDelete()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        public IAsyncOperation<ContentDialogResult> ShowAsyncN(int i)
        {
            argid = i;
            return this.ShowAsync();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            List<countdown_schedule> schedules = MainPage.schedules;
            if (argid == -1)
                throw new Exception();
            else
            {
                foreach (var v in schedules)
                {
                    if (v.ID == argid)
                    {
                        this.TTil.Text = "確定要刪除" + v.Name + "嗎?";
                    }
                }
            }
        }
    }
}
