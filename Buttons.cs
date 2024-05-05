using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CountDown_Day {
    public sealed partial class MainPage : Page {
        private void ButtonAdapter() {
            if (Application.Current.RequestedTheme == ApplicationTheme.Light) {
                this.IBAdd.Source = new BitmapImage(new Uri("ms-appx:///Assets/button-add.png"));
                this.IBDown.Source = new BitmapImage(new Uri("ms-appx:///Assets/button-down.png"));
                this.IBSetting.Source = new BitmapImage(new Uri("ms-appx:///Assets/button-settings.png"));
                this.IBUp.Source = new BitmapImage(new Uri("ms-appx:///Assets/button-up.png"));
                this.IBRefresh.Source = new BitmapImage(new Uri("ms-appx:///Assets/refresh.png"));
            }
            else {
                this.IBAdd.Source = new BitmapImage(new Uri("ms-appx:///Assets/indark-button-add.png"));
                this.IBDown.Source = new BitmapImage(new Uri("ms-appx:///Assets/indark-button-down.png"));
                this.IBSetting.Source = new BitmapImage(new Uri("ms-appx:///Assets/indark-button-settings.png"));
                this.IBUp.Source = new BitmapImage(new Uri("ms-appx:///Assets/indark-button-up.png"));
                this.IBRefresh.Source = new BitmapImage(new Uri("ms-appx:///Assets/indark-refresh.png"));
            }
        }
        private void BAdd_Click(object sender, RoutedEventArgs e) {
            AddCountDay addCountDay = new AddCountDay();
            addCountDay.ShowAsync();
        }
        private void BSetting_Click(object sender, RoutedEventArgs e) {
            Settings settings = new Settings();
            settings.ShowAsync();
        }
        private void BDown_Click(object sender, RoutedEventArgs e) {
            App.Main?.ReLoadItems(nowid);
        }
        private void BUp_Click(object sender, RoutedEventArgs e) {
            App.Main?.ReLoadItems(nowid == 0 ? 0 : nowid - len);
        }
        public void Upd_Schedule(object sender, RoutedEventArgs e) {
            string til = (string)(((Button)sender).DataContext);
            int id = -1;
            foreach (var v in buttonmaps) {
                if (((string)(v.button.DataContext)) == til) {
                    id = v.ID;
                    break;
                }
            }
            if (id == -1)
                throw new Exception("Failed to query metadata");
            else {
                countdown_schedule handledSchedule = new countdown_schedule();
                foreach (var v in schedules) {
                    if (v.ID == id)
                        handledSchedule = v;
                }
                string title = handledSchedule.Name;
                DateTime tdt = handledSchedule.time;
                DateTime nowPlus = DateTime.Now.AddSeconds(10);
                tdt = new DateTime(tdt.Year, tdt.Month, tdt.Day, nowPlus.Hour, nowPlus.Minute, nowPlus.Second);
                string ce;
                if (GetForeDate(tdt) == ForeDate.Future)
                    ce = "还有";
                else
                    ce = "已经";
                TimeSpan dif = tdt - DateTime.Now;
                int d = (int)(dif.TotalDays < 0 ? -dif.TotalDays : dif.TotalDays);
                this.TAction.Text = title + ce;
                this.TTime.Text = Convert.ToString(d);
            }
        }
        public void Change_Schedule(object sender, RoutedEventArgs e) {
            string til = (string)(((Button)sender).DataContext);
            int id = -1;
            foreach (var v in buttonmaps) {
                if (((string)(v.button.DataContext)) == til) {
                    id = v.ID;
                    break;
                }
            }
            if (id == -1)
                throw new Exception("Failed to query metadata");
            else {
                ChangeSchedule cgs = new ChangeSchedule();
                cgs.ShowAsyncN(id);
            }
        }
        private void BRefresh_Click(object sender, RoutedEventArgs e) {
            scheduleBuffer.Clear();
            schedules.Clear();
            App.Main?.Page_Loaded(sender, e);
        }
    }
}