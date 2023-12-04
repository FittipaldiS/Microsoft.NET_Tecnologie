using InfoPcTool.Models;
using System.Windows;

namespace InfoPcTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModels.MainWindowViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
        }
    }
}
