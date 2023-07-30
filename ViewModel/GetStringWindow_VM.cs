using System.Windows;
using System.Windows.Input;

namespace V2Grupp.ViewModel
{
    public class GetStringWindow_VM : PropertyChangedBase
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

        string _whatNeed = "Введите текст";
        /// <summary>
        /// Что требуется от пользователя
        /// </summary>
        public string WhatNeed
        {
            get { return _whatNeed; }
            set
            {
                _whatNeed = value;
                OnPropertyChanged(nameof(WhatNeed));
            }
        }

        string _inputString = "File #";
        /// <summary>
        /// Текст, который необходимо получить от пользователя
        /// </summary>
        public string InputString
        {
            get { return _inputString; }
            set
            {
                _inputString = value;
                OnPropertyChanged(nameof(InputString));
            }
        }

        string _okBtnTxt = "Сохранить";
        /// <summary>
        /// Текст на кнопке подтверждения введенного текста
        /// </summary>
        public string OkBtnTxt
        {
            get { return _okBtnTxt; }
            set
            {
                _okBtnTxt = value;
                OnPropertyChanged(nameof(OkBtnTxt));
            }
        }

        int _inputStrMaxLen;
        /// <summary>
        /// Максимальная длина вводимой строки
        /// </summary>
        public int InputStrMaxLen
        {
            get { return _inputStrMaxLen; }
            set
            {
                _inputStrMaxLen = value;
                OnPropertyChanged(nameof(InputStrMaxLen));
            }
        }

        int _winHeight;
        /// <summary>
        /// Высота окна
        /// </summary>
        public int WinHeight
        {
            get { return _winHeight; }
            set
            {
                _winHeight = value;
                OnPropertyChanged(nameof(WinHeight));
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
        /// Подтвердить введенное значение
        /// </summary>
        public ICommand SaveStrCmd { get; }

        #endregion

        public GetStringWindow_VM(string WhatNeedTxt, string InputStr, string OkBtn, int StrMaxLen)
        {
            Title = WhatNeedTxt;
            WhatNeed = WhatNeedTxt;
            InputString = InputStr;
            OkBtnTxt = OkBtn;
            InputStrMaxLen = StrMaxLen;
            WinHeight = 150;
            SaveStrCmd = new RelayCommand(SaveStr);
        }

        #region Обработка событий

        /// <summary>
        /// Закрыть View с DialogResult = true
        /// </summary>
        /// <param name="parameter">View, которое нужно закрыть</param>
        private void SaveStr(object parameter)
        {
            if (parameter == null) return;
            Window thisView = parameter as Window;
            thisView.DialogResult = true;
            thisView.Close();
        }

        #endregion
    }
}