using System.Windows;

namespace V2Grupp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShowTxtWIndow.xaml
    /// </summary>
    public partial class ShowTxtWIndow : Window
    {
        public ShowTxtWIndow(string Txt)
        {
            InitializeComponent();
            DataContext = new ViewModel.ShowTxtWIndow_VM(Txt);
        }
    }
}