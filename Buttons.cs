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

        }
        private void BDown_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BUp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}