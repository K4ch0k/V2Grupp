using System.Windows;

namespace V2Grupp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для DownloadProfileWindow.xaml
    /// </summary>
    public partial class DownloadProfileWindow : Window
    {
        public DownloadProfileWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.DownloadProfileWindow_VM();
        }
    }
}