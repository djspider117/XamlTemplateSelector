using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XamlTemplateSelector.DemoApp.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XamlTemplateSelector.DemoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Device> _devices;

        public MainPage()
        {
            _devices = new ObservableCollection<Device>();
            InitializeComponent();
            lvDemo.ItemsSource = _devices;

            Loaded += MainPage_Loaded;

        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainPage_Loaded;

            _devices.Add(new DesktopDevice() { Guid = Guid.NewGuid().ToString(), ComputerName = "COMPUTER_1" });
            await Task.Delay(250);
            _devices.Add(new MobileDevice() { Guid = Guid.NewGuid().ToString(), Phone = "0125 458 14" });
            await Task.Delay(250);
            _devices.Add(new DesktopDevice() { Guid = Guid.NewGuid().ToString(), ComputerName = "COMPUTER_2" });
            await Task.Delay(250);
            _devices.Add(new MobileDevice() { Guid = Guid.NewGuid().ToString(), Phone = "03648 1987 18" });
            await Task.Delay(250);
            _devices.Add(new MobileDevice() { Guid = Guid.NewGuid().ToString(), Phone = "1585 589825 5" });
            await Task.Delay(250);
            _devices.Add(new DesktopDevice() { Guid = Guid.NewGuid().ToString(), ComputerName = "COMPUTER_3" });
        }
    }
}
