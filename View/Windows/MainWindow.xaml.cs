using System.Windows;

namespace V2Grupp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.MainWIndow_VM VM = new ViewModel.MainWIndow_VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
            MainFrame.Navigate(new Pages.PolygonWorkPage());
        }
    }
}