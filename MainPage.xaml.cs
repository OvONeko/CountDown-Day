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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace CountDown_Day
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Button> buttons = new List<Button>();
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void Grid_Num_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void BAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BSetting_Click(object sender, RoutedEventArgs e)
        {

        }
        private void IFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
