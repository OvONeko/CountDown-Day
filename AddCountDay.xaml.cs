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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace CountDown_Day
{
    public sealed partial class AddCountDay : ContentDialog
    {
        public AddCountDay()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            try
            {
                File.AppendAllText(localfolder.Path + "\\config.ini", this.CYear.SelectedValue.ToString() + " " + this.CMonth.SelectedValue.ToString() + " " + this.CDay.SelectedValue.ToString() + ":" + (this.TTitle.Text == "" ? "(未命名)" : this.TTitle.Text) + "\n");
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.ToString(), ex.Message);
                dialog.ShowAsync();
            }
            
        }
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            List<drop_down_list> lyear = new List<drop_down_list>();
            List<drop_down_list> lmonth = new List<drop_down_list>();
            List<drop_down_list> lday = new List<drop_down_list>();
            lyear.Add(new drop_down_list { Name = "每年", ID = 0, IDS = 0 });
            for (int i = 1970; i <= 2050; i++)
                lyear.Add(new drop_down_list { Name = (Convert.ToString(i) + "年"), ID = i - 1969, IDS = i });
            for (int i = 1; i <= 12; i++)
                lmonth.Add(new drop_down_list { Name = (Convert.ToString(i) + "月"), ID = i - 1, IDS = i });
            for (int i = 1; i <= 31; i++)
                lday.Add(new drop_down_list { Name = (Convert.ToString(i) + "日"), ID = i - 1, IDS = i });
            this.CYear.ItemsSource = lyear;
            this.CMonth.ItemsSource = lmonth;
            this.CDay.ItemsSource = lday;
            this.CYear.DisplayMemberPath = this.CMonth.DisplayMemberPath = this.CDay.DisplayMemberPath = "Name";
            this.CYear.SelectedValuePath = this.CMonth.SelectedValuePath = this.CDay.SelectedValuePath = "IDS";
            this.CYear.SelectedIndex = this.CMonth.SelectedIndex = this.CDay.SelectedIndex = 0;
        }
        private void CMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int mon = this.CMonth.SelectedIndex + 1;
            int td = this.CDay.SelectedIndex + 1;
            int[] adds = { 1, 3, 5, 7, 8, 10, 12 };
            bool isplus = false;
            if (mon == 2)
            {
                List<drop_down_list> lday = new List<drop_down_list>();
                int y = (int)this.CYear.SelectedValue;
                int j = y % 4 == 0 ? (y % 100 == 0 ? (y % 400 == 0 ? 29 : 28) : 29) : 28;
                for (int i = 1; i <= j; i++)
                    lday.Add(new drop_down_list { Name = (Convert.ToString(i) + "日"), ID = i - 1, IDS = i });
                this.CDay.ItemsSource = lday;
                this.CDay.DisplayMemberPath = "Name";
                this.CDay.SelectedValuePath = "IDS";
                if (td > j)
                    this.CDay.SelectedValue = j - 1;
                else
                    this.CDay.SelectedValue = td;
            }
            else
            {
                foreach (var v in adds)
                {
                    if (mon == v)
                    {
                        isplus = true;
                        break;
                    }
                }
                if (isplus)
                {
                    List<drop_down_list> lday = new List<drop_down_list>();
                    for (int i = 1; i <= 31; i++)
                        lday.Add(new drop_down_list { Name = (Convert.ToString(i) + "日"), ID = i - 1, IDS = i });
                    this.CDay.ItemsSource = lday;
                    this.CDay.DisplayMemberPath = "Name";
                    this.CDay.SelectedValuePath = "IDS";
                    this.CDay.SelectedIndex = td - 1;
                }
                else
                {
                    List<drop_down_list> lday = new List<drop_down_list>();
                    for (int i = 1; i <= 30; i++)
                        lday.Add(new drop_down_list { Name = (Convert.ToString(i) + "日"), ID = i - 1, IDS = i });
                    this.CDay.ItemsSource = lday;
                    this.CDay.DisplayMemberPath = "Name";
                    this.CDay.SelectedValuePath = "IDS";
                    this.CDay.SelectedIndex = td >= 30 ? 29 : td - 1;
                }
            }
        }
    }
}
