using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
            List<countdown_schedule> schedules = new List<countdown_schedule>();
            foreach (var v in MainPage.schedules)
            {
                schedules.Add(v);
            }
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            string[] ori = File.ReadAllLines(localfolder.Path + "\\config.ini");
            File.Delete(localfolder.Path + "\\config.ini");
            int i = 0;
            foreach (var v in schedules)
            {
                if (v.ID == argid)
                {
                    string filename = v.filename;
                    MainPage.schedules.Remove(v);
                    File.Delete(filename);
                }
                else
                {
                    File.AppendAllText(localfolder.Path + "\\config.ini", ori[i] + "\n");
                }
                i++;
            }
            for (i = 0; i < MainPage.schedules.Count; i++)
            {
                MainPage.schedules[i].ID = i;
            }
            App.Main?.ReLoadItems();
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
