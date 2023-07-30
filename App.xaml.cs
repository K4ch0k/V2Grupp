using System.Threading.Tasks;
using System.Windows;
using V2Grupp.ViewModel;

namespace V2Grupp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Task.Factory.StartNew(() =>
            {
                //  "Процесс загрузки"
                System.Threading.Thread.Sleep(1000);
                Global.LoadPaintArea();

                this.Dispatcher.Invoke(() =>
                {
                    var splashScreen = this.MainWindow;
                    var mainWindow = new View.Windows.MainWindow();
                    this.MainWindow = mainWindow;
                    mainWindow.Show();
                    splashScreen.Close();
                });
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (Global.SelectedPaintArea != null) Global.SavePaintArea();
        }
    }
}