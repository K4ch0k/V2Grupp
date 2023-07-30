using System.Windows;

namespace V2Grupp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfilesWindow.xaml
    /// </summary>
    public partial class ProfilesWindow : Window
    {
        ViewModel.ProfilesWindow_VM VM = new ViewModel.ProfilesWindow_VM();
        public ProfilesWindow()
        {
            InitializeComponent();
            DataContext = VM;
        }
    }
}