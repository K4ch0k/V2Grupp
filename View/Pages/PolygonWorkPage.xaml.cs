using System.Windows.Controls;

namespace V2Grupp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PolygonWorkPage.xaml
    /// </summary>
    public partial class PolygonWorkPage : Page
    {
        public PolygonWorkPage()
        {
            InitializeComponent();
            DataContext = new ViewModel.PolygonWork_VM(PaintSurface);
        }
    }
}