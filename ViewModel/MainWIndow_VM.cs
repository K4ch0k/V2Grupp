using System.Windows;
using System.Windows.Input;
using V2Grupp.View.Windows;

namespace V2Grupp.ViewModel
{
    public class MainWIndow_VM : PropertyChangedBase
    {
        #region Переменные

        string _title = Global.AppName;
        /// <summary>
        /// Заголовок формы
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        string _logoIcon = Global.LogoIcon;
        /// <summary>
        /// Иконка окна
        /// </summary>
        public string LogoIcon
        {
            get { return _logoIcon; }
            set
            {
                _logoIcon = value;
                OnPropertyChanged(nameof(LogoIcon));
            }
        }

        /// <summary>
        /// Справка
        /// </summary>
        public ICommand HelpCmd { get; }

        #endregion
        public MainWIndow_VM()
        {
            HelpCmd = new RelayCommand(ShowHelp);
        }

        #region Обработка событий

        /// <summary>
        /// Отобразить окно справки
        /// </summary>
        private void ShowHelp(object parameter)
        {
            string xaml = "<Section xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                "<Paragraph>" +
                "<Run>Для создания многоугольника кликайте по рабочей поверхности (белая область главного окна).</Run> " +
                "<Run>В правой части окна расположена панель инструментов, отображающая координаты курсора на холсте, " +
                "позицию курсора относительно многоугольника, параметр заливки фигуры, описание профиля, " +
                "с которым сейчас производится работа.</Run> " +
                "<Run>В нижней части панели инструментов находятся кнопки сохранения и загрузки многоугольников, " +
                "удаление последней отмеченой точки и очистка холста.</Run> " +
                "<Run>После закрытия приложения многоугольник автоматически сохраняется и " +
                "будет отображен при следующем запуске программы.</Run> " +
                "</Paragraph></Section>";

            Window helpWin = new ShowTxtWIndow(xaml);
            helpWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWin.Show();
        }

        #endregion
    }
}