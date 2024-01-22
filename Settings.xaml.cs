using System;
using System.IO;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CountDown_Day {
    public sealed partial class Settings : ContentDialog {
        public Settings() {
            this.InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e) {
            this.TGBg.Text = MainPage.glbc.backgroundconfig == null ? "" : MainPage.glbc.backgroundconfig;
            this.TGFg.Text = MainPage.glbc.foregroundconfig == null ? "" : MainPage.glbc.foregroundconfig;
        }
        private void BOpen_Click(object sender, RoutedEventArgs e) {
            Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
            this.Hide();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            MainPage.glbc.backgroundconfig = this.TGBg.Text;
            MainPage.glbc.foregroundconfig = this.TGFg.Text;
            Handle.WriteGlobalConfig();
            App.Main?.GImInitial();
        }
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {

        }
        private async void BGBg_Click(object sender, RoutedEventArgs e) {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            var localfolder = ApplicationData.Current.LocalFolder;
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file == null)
                return;
            //File.Copy(file.Path, localfolder.Path + file.DisplayName + "." + file.FileType);
            byte[] result;
            using (Stream stream = await file.OpenStreamForReadAsync()) {
                using (var memoryStream = new MemoryStream()) {
                    stream.CopyTo(memoryStream);
                    result = memoryStream.ToArray();
                }
            }
            File.WriteAllBytes(localfolder.Path + "\\" + file.DisplayName + "." + file.FileType, result);
            this.TGBg.Text = localfolder.Path + "\\" + file.DisplayName + "." + file.FileType;
        }
        private async void FGBg_Click(object sender, RoutedEventArgs e) {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            var localfolder = ApplicationData.Current.LocalFolder;
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file == null)
                return;
            //File.Copy(file.Path, localfolder.Path + file.DisplayName + "." + file.FileType);
            byte[] result;
            using (Stream stream = await file.OpenStreamForReadAsync()) {
                using (var memoryStream = new MemoryStream()) {
                    stream.CopyTo(memoryStream);
                    result = memoryStream.ToArray();
                }
            }
            File.WriteAllBytes(localfolder.Path + "\\" + file.DisplayName + "." + file.FileType, result);
            this.TGFg.Text = localfolder.Path + "\\" + file.DisplayName + "." + file.FileType;
        }
    }
}
