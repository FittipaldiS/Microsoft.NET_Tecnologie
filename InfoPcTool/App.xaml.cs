using InfoPcTool.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InfoPcTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            PcInfoData pcInfoData = new();

            ViewModels.MainWindowViewModel mainWindowViewModel = new(pcInfoData);

            MainWindow mainWindwo = new(mainWindowViewModel);
            mainWindwo.Show();

        }
    }
}
