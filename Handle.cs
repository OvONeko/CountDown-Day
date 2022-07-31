using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CountDown_Day
{
    internal class Handle
    {
        public static async void WriteGlobalConfig()
        {
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
    public sealed partial class MainPage : Page
    {
        private void Listener_ThemeChanged(object sender)
        {
            this.ButtonAdapter();
            this.GImInitial();
        }
    }
}
