using System;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace CountDown_Day {
    internal class Handle {
        public static async void WriteGlobalConfig() {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            string filename = localfolder.Path + "\\global.ini";
            File.Delete(filename);
            globalconfig glbc = MainPage.glbc;
            if (MainPage.tcolor == true)
                File.AppendAllText(filename, "TColor=True\n");
            if ((glbc.backgroundconfig != null) && (glbc.backgroundconfig != ""))
                File.AppendAllText(filename, "Background=" + glbc.backgroundconfig + "\n");
            if ((glbc.foregroundconfig != null) && (glbc.foregroundconfig != ""))
                File.AppendAllText(filename, "Foreground=" + glbc.foregroundconfig + "\n");
        }
    }
    public sealed partial class MainPage : Page {
        private void Listener_ThemeChanged(object sender) {
            this.ButtonAdapter();
            this.GImInitial();
        }
        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e) {
            (sender as Button).Background = new ImageBrush();
            ((sender as Button).Background as ImageBrush).ImageSource = new BitmapImage(new Uri(this.BaseUri, (Application.Current.RequestedTheme == ApplicationTheme.Light) ? "Assets/button_lable_light.png" : "Assets/button_lable_dark.png"));
            ((sender as Button).Background as ImageBrush).Stretch = Stretch.None;
            ((sender as Button).Background as ImageBrush).AlignmentX = AlignmentX.Left;
            ((sender as Button).Background as ImageBrush).AlignmentY = AlignmentY.Center;
        }
    }
}
