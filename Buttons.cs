using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
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
    public sealed partial class MainPage : Page
    {
        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCountDay addCountDay = new AddCountDay();
            addCountDay.ShowAsync();
        }
        private void BSetting_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowAsync();
        }
        private void BDown_Click(object sender, RoutedEventArgs e)
        {
            App.Main?.ReLoadItems(nowid);
        }
        private void BUp_Click(object sender, RoutedEventArgs e)
        {
            App.Main?.ReLoadItems(nowid == 0 ? 0 : nowid - len);
        }
        public void Upd_Schedule(object sender, RoutedEventArgs e)
        {
            string til = (string)(((Button)sender).DataContext);
            int id = -1;
            foreach (var v in buttonmaps)
            {
                if (((string)(v.button.DataContext)) == til)
                {
                    id = v.ID;
                    break;
                }
            }
            if (id == -1)
                throw new Exception("Failed to query metadata");
            else
            {
                string title = schedules[id].Name;
                DateTime tdt = schedules[id].time;
                string ce;
                if (GetForeDate(tdt) == ForeDate.Future)
                    ce = "還有";
                else
                    ce = "已經";
                TimeSpan dif = tdt - DateTime.Now;
                int d = (int)(dif.TotalDays < 0 ? -dif.TotalDays : dif.TotalDays);
                this.TAction.Text = title + ce;
                this.TTime.Text = Convert.ToString(d);
            }
        }
        public void Change_Schedule(object sender, RoutedEventArgs e)
        {
            string til = (string)(((Button)sender).DataContext);
            int id = -1;
            foreach (var v in buttonmaps)
            {
                if (((string)(v.button.DataContext)) == til)
                {
                    id = v.ID;
                    break;
                }
            }
            if (id == -1)
                throw new Exception("Failed to query metadata");
            else
            {
                ChangeSchedule cgs = new ChangeSchedule();
                cgs.ShowAsyncN(id);
            }
        }
    }
}