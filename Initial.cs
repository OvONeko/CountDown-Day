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

namespace CountDown_Day
{
    public sealed partial class MainPage : Page
    {
        enum ForeDate
        {
            Past = 1,
            Future = -1,
            Now = 0
        }
        static ForeDate GetForeDate(DateTime dateTime)
        {
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
    }
}