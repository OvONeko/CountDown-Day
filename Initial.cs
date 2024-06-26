﻿using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace CountDown_Day {
    public class drop_down_list {
        public string Name { get; set; }
        public int ID { get; set; }
        public int IDS { get; set; }
    }
    public class countdown_schedule {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime time { get; set; }
        public bool isshow { get; set; }
        public string filename { get; set; }
        public bool pinned { get; set; }
    }
    public class button_map {
        public int ID { get; set; }
        public Button button { get; set; }
        public Grid gridc { get; set; }
        public TextBlock texttime { get; set; }
        public TextBlock texttitle { get; set; }
    }
    public struct globalconfig {
        public string backgroundconfig { get; set; }
        public string foregroundconfig { get; set; }
    }
    public sealed partial class MainPage : Page {
        public static List<countdown_schedule> schedules = new List<countdown_schedule>();
        public static List<countdown_schedule> scheduleBuffer = new List<countdown_schedule>();
        public static List<button_map> buttonmaps = new List<button_map>();
        int nowid = 0;
        int len = (int)((Window.Current.Bounds.Height - 104) / 48);
        int pinnedCount = 0;

        public enum ForeDate {
            Past = 1,
            Future = -1,
            Now = 0
        }
        /// <summary>
        /// 用於獲取指定日期與當天的舌關係
        /// </summary>
        /// <param name="dateTime">指定日期</param>
        public static ForeDate GetForeDate(DateTime dateTime) {
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
        public static globalconfig glbc = new globalconfig { backgroundconfig = null, foregroundconfig = null };
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            UIElement uIElement = this.IEmpty;
            if (!File.Exists(localfolder.Path + "\\config.ini")) {
                //嘗試創建文件並顯示「空」選項
                try {
                    localfolder.CreateFileAsync("config.ini", CreationCollisionOption.FailIfExists);
                }
                catch (Exception ex) {
                    var dialog = new MessageDialog("Cannot Create File:" + localfolder.Path + "\\config.ini\n" + ex.ToString(), ex.Message);
                    dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                    dialog.ShowAsync();
                }
                uIElement.Visibility = Visibility.Visible;
            }
            else {
                FileInfo info = new FileInfo(localfolder.Path + "\\config.ini");
                if (info.Length == 0)
                    uIElement.Visibility = Visibility.Visible;
                else {
                    DirectoryInfo di = new DirectoryInfo(localfolder.Path);
                    int i6 = 0;
                    foreach (var v in di.GetFiles()) {
                        if (v.Name.Contains("config.ini"))
                            continue;
                        if (v.Name.Contains("global.ini"))
                            continue;
                        if (!v.Extension.Contains("ini"))
                            continue;
                        string fname = v.FullName;
                        string[] values;
                        try {
                            values = File.ReadAllLines(fname);
                        }
                        catch (Exception ex) {
                            MessageDialog dialog = new MessageDialog(ex.ToString(), ex.Message);
                            return;
                        }
                        int year = 0, month = 0, day = 0;
                        string tl = "";
                        int yst = 0;
                        bool isst = false;
                        bool pinned = false;
                        foreach (var u in values) {
                            string[] c = u.Split('=');
                            for (int i = 0; i < c.Length; i++) {
                                c[i] = c[i].Trim();
                            }
                            c[0] = c[0].ToLower();
                            if (c[0] == "month") {
                                month = Convert.ToInt32(c[1]);
                            }
                            else if (c[0] == "day") {
                                day = Convert.ToInt32(c[1]);
                            }
                            else if (c[0] == "title") {
                                tl = c[1];
                            }
                            else if (c[0] == "pinned") {
                                pinned = Convert.ToInt32(c[1]) != 0;
                            }
                            if (c[0] == "year") {
                                if (((month == 0) || (day == 0)) && (Convert.ToInt32(c[1]) == 0)) {
                                    isst = true;
                                    yst = Convert.ToInt32(c[1]);
                                }
                                else if (Convert.ToInt32(c[1]) != 0) {
                                    year = Convert.ToInt32(c[1]);
                                }
                                else {
                                    year = MainPage.GetForeDate(new DateTime(DateTime.Now.Year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(10)) == ForeDate.Past ? DateTime.Now.Year + 1 : DateTime.Now.Year;
                                }
                            }
                        }
                        if (isst) {
                            year = yst == 0 ? (MainPage.GetForeDate(new DateTime(DateTime.Now.Year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(10)) == ForeDate.Future ? DateTime.Now.Year : DateTime.Now.Year + 1) : yst;
                        }
                        DateTime targetdt = new DateTime(year, month, day);
                        scheduleBuffer.Add(new countdown_schedule { ID = i6, isshow = false, Name = tl, time = targetdt, filename = fname, pinned = pinned });
                        i6++;
                    }
                }
                foreach (var v in scheduleBuffer) {
                    if (v.pinned) {
                        pinnedCount++; 
                        schedules.Add(v);
                    }
                }
                foreach (var v in scheduleBuffer) {
                    if (!v.pinned)
                        schedules.Add(v);
                }
                scheduleBuffer.Clear();
                this.ReLoadItems();
            }
            this.GImInitial();
            this.ButtonAdapter();
        }
        public async void ReLoadItems(int status = 0) {
            buttonmaps.Clear();
            this.IFrame.Children.Clear();
            int i = status == 0 ? 0 : status;
            // MessageDialog dialog = new MessageDialog("ReloadItems Detached.");
            // dialog.ShowAsync();
            if (schedules.Count == 0) {
                this.IEmpty.Visibility = Visibility.Visible;
                return;
            }
            else
                this.IEmpty.Visibility = Visibility.Collapsed;
            double h = Window.Current.Bounds.Height;
            foreach (var v in schedules) {
                if (v.ID < status)
                    continue;
                if ((status > (schedules.Count / len)) && v.pinned)
                    continue;
                if (((i - status) * 48 + 8) < h) {
                    buttonmaps.Add(new button_map {
                        ID = v.ID,
                        button = new Button(),
                        gridc = new Grid(),
                        texttime = new TextBlock(),
                        texttitle = new TextBlock()
                    });
                    buttonmaps[i - status].button.Height = 32;
                    buttonmaps[i - status].button.Margin = new Thickness(8, (i - status) * 40 + 8, 8, 0);
                    buttonmaps[i - status].button.Style = (Application.Current.RequestedTheme == ApplicationTheme.Light) ? (Style)Resources["Button_Lable_Light"] : (Style)Resources["Button_Lable_Dark"];
                    buttonmaps[i - status].texttime.Text = v.time.ToShortDateString() + (v.pinned ? "    📌" : "");
                    buttonmaps[i - status].texttitle.Text = v.Name;
                    buttonmaps[i - status].texttime.Margin = new Thickness(0, 0, 0, 0);
                    buttonmaps[i - status].texttitle.Margin = new Thickness(0, 0, 0, 0);
                    buttonmaps[i - status].texttime.HorizontalAlignment = HorizontalAlignment.Left;
                    buttonmaps[i - status].texttitle.HorizontalAlignment = HorizontalAlignment.Right;
                    buttonmaps[i - status].texttime.HorizontalTextAlignment = TextAlignment.Right;
                    buttonmaps[i - status].texttitle.HorizontalTextAlignment = TextAlignment.Center;
                    buttonmaps[i - status].gridc.Margin = new Thickness(0, 0, 0, 0);
                    buttonmaps[i - status].gridc.Children.Add(buttonmaps[i - status].texttime);
                    buttonmaps[i - status].gridc.Children.Add(buttonmaps[i - status].texttitle);
                    buttonmaps[i - status].button.Content = buttonmaps[i - status].gridc;
                    buttonmaps[i - status].button.DataContext = v.time.ToShortDateString() + "\t\t" + v.Name + Convert.ToString(i - status);
                    buttonmaps[i - status].button.PointerEntered += Button_PointerEntered;
                    try {
                        buttonmaps[i - status].gridc.Width = double.IsNaN(this.IFrame.Width) ? (Window.Current.Bounds.Width - 612) : this.IFrame.Width - 32.0;
                        buttonmaps[i - status].button.Width = double.IsNaN(this.IFrame.Width) ? (Window.Current.Bounds.Width - 564) : this.IFrame.Width - 16.0;
                    }
                    catch (Exception ex) {
                        var dialog = new MessageDialog(ex.ToString(), ex.Message);
                        dialog.ShowAsync();
                    }
                    buttonmaps[i - status].button.Visibility = Visibility.Visible;
                    buttonmaps[i - status].button.VerticalAlignment = VerticalAlignment.Top;
                    buttonmaps[i - status].button.Click += Upd_Schedule;
                    buttonmaps[i - status].button.RightTapped += Change_Schedule;
                    IFrame.Children.Add(buttonmaps[i - status].button);
                }
                else if ((i - 1) > nowid) {
                    nowid = i;
                    break;
                }
                i++;
            }
            if (nowid != i)
                nowid = 0;
            this.TPage.Text = Convert.ToString((schedules.Count / len) - (nowid / len) + 1) + "/" + Convert.ToString(schedules.Count / len + 1);
        }

        public async void GImInitial(int cfg = 0) {
            if (!File.Exists(localfolder.Path + "\\global.ini")) {
                try {
                    localfolder.CreateFileAsync("global.ini", CreationCollisionOption.FailIfExists);
                }
                catch (Exception ex) {
                    var dialog = new MessageDialog("Cannot Create File:" + localfolder.Path + "\\global.ini\n" + ex.ToString(), ex.Message);
                    dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                    dialog.ShowAsync();
                }
                glbc.backgroundconfig = null;
                glbc.foregroundconfig = null;
                if (Application.Current.RequestedTheme == ApplicationTheme.Light) {
                    tcolor = true;
                    this.TTime.Foreground = this.TAction.Foreground = this.TD0.Foreground = new SolidColorBrush(Color.FromArgb(255, 10, 10, 10));
                    File.AppendAllText(localfolder.Path + "\\global.ini", "TColor=" + Convert.ToString(tcolor));
                }
                goto reset;
            }
            if (FileExtend.IsEmpty(localfolder.Path + "\\global.ini"))
                goto reset;
            string[] glb = File.ReadAllLines(localfolder.Path + "\\global.ini");
            foreach (var v in glb) {
                string[] u = v.Split('=');
                for (int i = 0; i < u.Length; i++)
                    u[i] = u[i].Trim();
                u[0] = u[0].ToLower();
                if (u[0] == "tcolor") {
                    try {
                        tcolor = ((u[1] == "1") || (u[1].ToLower() == "true")) ? true : false;
                        if (!tcolor)
                            this.TTime.Foreground = this.TAction.Foreground = this.TD0.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 248, 255));
                        else
                            this.TTime.Foreground = this.TAction.Foreground = this.TD0.Foreground = new SolidColorBrush(Color.FromArgb(255, 10, 10, 10));
                    }
                    catch (Exception ex) {
                        MessageDialog dialog = new MessageDialog(ex.ToString(), ex.Message);
                        dialog.ShowAsync();
                    }
                }
                else if ((u[0] == "background") || (u[0] == "bg")) {
                    int st = 0;
                    if (File.Exists(u[1]))
                        st = 1;
                    else if (File.Exists(localfolder.Path + "\\" + u[1]))
                        st = 2;
                    if (st == 0)
                        continue;
                    switch (st) {
                        case 1:
                            try {
                                this.IBg.Source = new BitmapImage(new Uri(u[1], UriKind.RelativeOrAbsolute));
                                glbc.backgroundconfig = u[1];
                            }
                            catch (Exception ex) {
                                MessageDialog dialog = new MessageDialog(ex.ToString(), ex.Message);
                                dialog.ShowAsync();
                            }
                            break;
                        case 2:
                            try {
                                this.IBg.Source = new BitmapImage(new Uri(localfolder.Path + "\\" + u[1], UriKind.RelativeOrAbsolute));
                                glbc.backgroundconfig = localfolder.Path + "\\" + u[1];
                            }
                            catch (Exception ex) {
                                MessageDialog dialog = new MessageDialog(ex.ToString(), ex.Message);
                                dialog.ShowAsync();
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if ((u[0] == "foreground") || (u[0] == "fg")) {
                    int st = 0;
                    if (File.Exists(u[1]))
                        st = 1;
                    else if (File.Exists(localfolder.Path + "\\" + u[1]))
                        st = 2;
                    if (st == 0)
                        continue;
                    switch (st) {
                        case 1:
                            try {
                                this.BForeImageBrush.ImageSource = new BitmapImage(new Uri(u[1], UriKind.RelativeOrAbsolute));
                                glbc.foregroundconfig = u[1];
                            }
                            catch (Exception ex) {
                                MessageDialog dialog = new MessageDialog(ex.ToString(), ex.Message);
                                dialog.ShowAsync();
                            }
                            break;
                        case 2:
                            try {
                                this.BForeImageBrush.ImageSource = new BitmapImage(new Uri(localfolder.Path + "\\" + u[1], UriKind.RelativeOrAbsolute));
                                glbc.foregroundconfig = localfolder.Path + "\\" + u[1];
                            }
                            catch (Exception ex) {
                                MessageDialog dialog = new MessageDialog(ex.ToString(), ex.Message);
                                dialog.ShowAsync();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        reset:
            if ((glbc.backgroundconfig == null) || (glbc.backgroundconfig == ""))
                this.IBg.Source = new BitmapImage(Application.Current.RequestedTheme == ApplicationTheme.Light ? (new Uri("ms-appx:///Assets/normal-bg.jpg")) : (new Uri("ms-appx:///Assets/dark-bg.jpg")));
            if ((glbc.foregroundconfig == null) || (glbc.foregroundconfig == ""))
                this.BForeImageBrush.ImageSource = new BitmapImage(Application.Current.RequestedTheme == ApplicationTheme.Light ? (new Uri("ms-appx:///Assets/note-light.png")) : (new Uri("ms-appx:///Assets/note-dark.png")));
            if ((DateTime.Now.Month == 3) && (DateTime.Now.Day == 31))
                this.BForeImageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Transfer.png"));
            if (DateTime.Now.Month == 6) {
                Random random = new Random();
                int i = (int)(DateTime.Now.Ticks % 20);
                int c = 0;
                while (i > 0) {
                    c = random.Next(0, 31);
                    i--;
                }
                if (c == 7)
                    this.BForeImageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/progress_pride_flag_4-3.png"));
            }
        }
    }
}