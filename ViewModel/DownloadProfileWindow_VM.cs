using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using V2Grupp.Model;

namespace V2Grupp.ViewModel
{
    public class DownloadProfileWindow_VM : PropertyChangedBase
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

        ObservableCollection<PaintArea> _allAreas;
        /// <summary>
        /// Все сохраненные холсты
        /// </summary>
        public ObservableCollection<PaintArea> AllAreas
        {
            get { return _allAreas; }
            set
            {
                _allAreas = value;
                OnPropertyChanged(nameof(AllAreas));
            }
        }

        PaintArea _selectedArea;
        public PaintArea SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                _selectedArea = value;
                OnPropertyChanged(nameof(SelectedArea));
            }
        }

        /// <summary>
        /// Удалить выбранный профиль
        /// </summary>
        public ICommand DeleteCmd { get; }
        /// <summary>
        /// Сохранить выбранный профиль
        /// </summary>
        public ICommand SaveCmd { get; }

        #endregion

        public DownloadProfileWindow_VM()
        {
            AllAreas = GetAllSaveArea();
            DeleteCmd = new RelayCommand(DeleteItem);
            SaveCmd = new RelayCommand(SaveItem);
        }

        #region Обработка событий

        /// <summary>
        /// Получить список уже сохраненных областей для рисования 
        /// </summary>
        /// <returns>Список областей для рисования</returns>
        private ObservableCollection<PaintArea> GetAllSaveArea()
        {
            ObservableCollection<PaintArea> res = null;
            try
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<PaintArea>>(Properties.Settings.Default.Polygon_Json);
            }
            catch (Exception)
            {
                return new ObservableCollection<PaintArea>();
            }
            return res;
        }

        /// <summary>
        /// Удалить выбранный элемент
        /// </summary>
        private void DeleteItem(object parameter)
        {
            if (SelectedArea == null) return;
            AllAreas.Remove(SelectedArea);

            var json = JsonConvert.SerializeObject(AllAreas);

            Properties.Settings.Default.Polygon_Json = json;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Принять выбор пользователя и закрыть окно
        /// </summary>
        private void SaveItem(object parameter)
        {
            if (parameter == null) return;
            Window thisView = parameter as Window;
            if (SelectedArea == null) thisView.DialogResult = true;
            else thisView.DialogResult = true;

            thisView.Close();
        }

        #endregion
    }
}