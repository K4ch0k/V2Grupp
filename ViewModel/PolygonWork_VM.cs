using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using V2Grupp.Model;
using V2Grupp.View.Windows;

namespace V2Grupp.ViewModel
{
    public class PolygonWork_VM : PropertyChangedBase
    {
        #region Переменные

        List<Point> _coordinates;
        /// <summary>
        /// Список кординат
        /// </summary>
        public List<Point> Coord
        {
            get { return _coordinates; }
            set
            {
                _coordinates = value;
                OnPropertyChanged(nameof(Coord));
            }
        }

        Point _mousePos;
        /// <summary>
        /// Текущая позиция курсора на холсте
        /// </summary>
        public Point MousePos
        {
            get { return _mousePos; }
            set
            {
                _mousePos = value;
                OnPropertyChanged(nameof(MousePos));
            }
        }

        bool _posInside = false;
        /// <summary>
        /// Интересующая точка внутри фигуры или нет
        /// </summary>
        public bool PosInside
        {
            get { return _posInside; }
            set
            {
                _posInside = value;
                OnPropertyChanged(nameof(PosInside));
            }
        }

        bool _canCheckMousePos = false;
        /// <summary>
        /// Можно ли проверить - точка внутри или снаружи фигуры
        /// </summary>
        public bool CanCheckMousePos
        {
            get { return _canCheckMousePos; }
            set
            {
                _canCheckMousePos = value;
                OnPropertyChanged(nameof(CanCheckMousePos));
            }
        }

        bool _fillPolygon;
        /// <summary>
        /// Залить многоугольник цветом
        /// </summary>
        public bool FillPolygon
        {
            get { return _fillPolygon; }
            set
            {
                _fillPolygon = value;
                OnPropertyChanged(nameof(FillPolygon));
            }
        }

        string _descArea;
        /// <summary>
        /// Описание к <see cref="CurrentArea"/>
        /// </summary>
        public string DescArea
        {
            get { return _descArea; }
            set
            {
                _descArea = value;
                OnPropertyChanged(nameof(DescArea));
            }
        }

        PaintArea _currentArea;
        /// <summary>
        /// Область для рисования, с которой сейчас работает опльзователь
        /// </summary>
        public PaintArea CurrentArea
        {
            get { return _currentArea; }
            set
            {
                _currentArea = value;
                Global.SelectedPaintArea = value;
                OnPropertyChanged(nameof(CurrentArea));
            }
        }

        Canvas _mainCanvas;
        /// <summary>
        /// Область для рисований
        /// </summary>
        public Canvas MainCanvas
        {
            get { return _mainCanvas; }
            set
            {
                _mainCanvas = value;
                OnPropertyChanged(nameof(MainCanvas));
            }
        }

        #region Команды

        /// <summary>
        /// Добавить точку 
        /// </summary>
        public ICommand AddPointCmd { get; }

        /// <summary>
        /// Проверить координаты курсора
        /// </summary>
        public ICommand CheckMousePosCmd { get; }

        /// <summary>
        /// Приблизить холст
        /// </summary>
        public ICommand ZoomInCmd { get; }

        /// <summary>
        /// Отдалить холст
        /// </summary>
        public ICommand ZoomOutCmd { get; }

        /// <summary>
        /// Отменить последнее изменение
        /// </summary>
        public ICommand UndoLastChCmd { get; }

        /// <summary>
        /// Принять отмененные изменения
        /// </summary>
        public ICommand AcceptLastChCmd { get; }

        /// <summary>
        /// Отменить все изменения
        /// </summary>
        public ICommand RefreshCanvasCmd { get; }

        /// <summary>
        /// Отрисовать фигуру
        /// </summary>
        public ICommand DrawFigureCmd { get; }

        /// <summary>
        /// Обработка нажатий клавиатуры
        /// </summary>
        public ICommand KeyDownCmd { get; }

        /// <summary>
        /// Загрузить профиль
        /// </summary>
        public ICommand DownloadProfileCmd { get; }

        /// <summary>
        /// Сохранить текущий холст
        /// </summary>
        public ICommand SaveProfileCmd { get; }
        #endregion

        #endregion

        public PolygonWork_VM(Canvas PaintArea)
        {
            Coord = new List<Point>();

            AddPointCmd = new RelayCommand(AddPoint);
            CheckMousePosCmd = new RelayCommand(CheckMouseLocation);
            ZoomInCmd = new RelayCommand(ZoomIn);
            ZoomOutCmd = new RelayCommand(ZoomOut);
            UndoLastChCmd = new RelayCommand(UndoLastCh);
            AcceptLastChCmd = new RelayCommand(AcceptLastCh);
            RefreshCanvasCmd = new RelayCommand(RefreshCanvas);
            DrawFigureCmd = new RelayCommand(DrawFigure);
            KeyDownCmd = new RelayCommand(KeyDown);
            DownloadProfileCmd = new RelayCommand(DownloadProfile);
            SaveProfileCmd = new RelayCommand(SaveProfile);

            MainCanvas = PaintArea;
            DownloadProfile(true);
            DrawBySaveData(CurrentArea);
        }

        #region Обработка событий

        #region Отрисовка
        public void AddPoint(object parameter)
        {
            if (MainCanvas == null) return;
            Coord.Add(Mouse.GetPosition(MainCanvas));
            DrawFigure(MainCanvas);
            if (Coord.Count >= 3)
            {
                CanCheckMousePos = true;
            }
            else
            {
                CanCheckMousePos = false;
            }
        }

        /// <summary>
        /// Отрисовка линий
        /// </summary>
        /// <param name="paintRegion">Область, на которой происходит рисование</param>
        private void PaintLine(Canvas paintRegion)
        {
            Line line = new Line();
            for (int i = 1; i < Coord.Count; i++)
            {
                line = new Line();
                line.Stroke = SystemColors.WindowFrameBrush;
                line.X1 = Coord[i - 1].X;
                line.Y1 = Coord[i - 1].Y;
                line.X2 = Coord[i].X;
                line.Y2 = Coord[i].Y;
                paintRegion.Children.Add(line);
            }

            //  Последняя линия - к началу
            line = new Line();
            line.Stroke = SystemColors.WindowFrameBrush;
            line.X1 = Coord[Coord.Count - 1].X;
            line.Y1 = Coord[Coord.Count - 1].Y;
            line.X2 = Coord[0].X;
            line.Y2 = Coord[0].Y;
            paintRegion.Children.Add(line);
        }

        private void PaintPolygon(Canvas paintRegion)
        {
            Polygon poly = new Polygon();
            poly.Stroke = Brushes.Black;
            poly.Fill = Brushes.LightSeaGreen;
            poly.StrokeThickness = 1;
            poly.HorizontalAlignment = HorizontalAlignment.Center;
            poly.VerticalAlignment = VerticalAlignment.Center;

            PointCollection points = new PointCollection();
            for (int i = 0; i < Coord.Count; i++)
            {
                points.Add(Coord[i]);
            }
            poly.Points = points;
            paintRegion.Children.Add(poly);
        }

        private void PaintPoint(Canvas paintRegion)
        {
            Ellipse el = new Ellipse();
            for (int i = 0; i < Coord.Count; i++)
            {
                el.Stroke = new SolidColorBrush(Colors.Black);
                el.StrokeThickness = 3;

                el.Height = 3;
                el.Width = 3;
                el.Fill = new SolidColorBrush(Colors.Green);
                el.Margin = new Thickness(Coord[i].X, Coord[i].Y, 0, 0);
                paintRegion.Children.Add(el);
            }
        }

        private void DrawFigure(object parameter)
        {
            if (MainCanvas == null) return;
            MainCanvas.Children.Clear();
            if (Coord.Count >= 2)
            {
                if (FillPolygon)
                {
                    PaintPolygon(MainCanvas);
                }
                else
                {
                    PaintLine(MainCanvas);
                }
            }
            else
            {
                PaintPoint(MainCanvas);
            }

            CurrentArea = new PaintArea()
            {
                Id = -1,
                Description = "",
                LastCh = DateTime.Now,
                FillPoly = FillPolygon,
                Coordinates = Coord
            };
        }

        private void DrawBySaveData(PaintArea draw)
        {
            if (MainCanvas == null || draw == null || draw.Coordinates == null) return;
            DrawFigure(MainCanvas);
            if (Coord.Count >= 3)
            {
                CanCheckMousePos = true;
            }
            else
            {
                CanCheckMousePos = false;
            }
        }

        #endregion

        #region Проверка местонахождения точки

        /// <summary>
        /// Определить текущее положение курсора
        /// </summary>
        /// <param name="parameter">Холст, на котором проверяется положение курсора</param>
        private void CheckMouseLocation(object parameter)
        {
            if (MainCanvas == null) return;
            MousePos = Mouse.GetPosition(MainCanvas);
            PosInside = PointInFigure(Coord, MousePos.X, MousePos.Y);
        }

        /// <summary>
        /// Определить - лежит заданная точка внутри или снаружи списка точек
        /// </summary>
        /// <param name="coord">Список точек, которые образуют фигуру</param>
        /// <param name="x">Координата точки по X</param>
        /// <param name="y">Координата точки по Y</param>
        /// <returns>true - точка внутри заданных координат;
        /// false - снаружи</returns>
        bool PointInFigure(List<Point> coord, double x, double y)
        {
            int count = coord.Count;
            bool res = false;

            for (int i = 0, j = count - 1; i < count; j = i++)
            {
                if ((((coord[i].Y <= y) && (y < coord[j].Y)) ||
                    ((coord[j].Y <= y) && (y < coord[i].Y))) &&
                    (((coord[j].Y - coord[i].Y) != 0) &&
                    (x > ((coord[j].X - coord[i].X) * (y - coord[i].Y) / (coord[j].Y - coord[i].Y) + coord[i].X))))
                    res = !res;
            }

            return res;
        }

        #endregion

        private void ZoomIn(object parameter)
        {
            //if (MainCanvas == null) return;
            //if (MainCanvas.RenderTransform == null)
            //{
            //    ScaleTransform st = new ScaleTransform();
            //    MainCanvas.RenderTransform = st;
            //}

            //(MainCanvas.RenderTransform as ScaleTransform).ScaleX *= 2;
            //(MainCanvas.RenderTransform as ScaleTransform).ScaleY *= 2;
        }

        private void ZoomOut(object parameter)
        {
            //if (MainCanvas == null) return;
            //if (MainCanvas.RenderTransform == null)
            //{
            //    ScaleTransform st = new ScaleTransform();
            //    MainCanvas.RenderTransform = st;
            //}

            //(MainCanvas.RenderTransform as ScaleTransform).ScaleX /= 2;
            //(MainCanvas.RenderTransform as ScaleTransform).ScaleY /= 2;
        }

        private void UndoLastCh(object parameter)
        {
            if (CurrentArea != null && CurrentArea.Coordinates != null && CurrentArea.Coordinates.Count > 0)
            {
                CurrentArea.Coordinates.Remove(CurrentArea.Coordinates.Last());
                MainCanvas.Children.Clear();
                Coord = new List<Point>();
                CurrentArea.Coordinates.ForEach(x => Coord.Add(x));
                DrawBySaveData(CurrentArea);
            }
        }

        private void AcceptLastCh(object parameter)
        {

        }

        private void RefreshCanvas(object parameter)
        {
            if (MainCanvas == null) return;
            MainCanvas.Children.Clear();
            Coord = new List<Point>();
            CanCheckMousePos = false;
            CurrentArea = new PaintArea();
        }

        private void KeyDown(object parameter)
        {

        }

        private void DownloadProfile(object parameter)
        {
            if ((parameter as bool?) == true)
            {
                //  Загрузка фигуры, на которой закрылось приложение
                CurrentArea = Global.SelectedPaintArea;
                if (CurrentArea != new PaintArea())
                {
                    MainCanvas.Children.Clear();
                    Coord = new List<Point>();
                    CanCheckMousePos = false;
                    FillPolygon = CurrentArea.FillPoly;
                    if (CurrentArea.Coordinates != null)
                    {
                        CurrentArea.Coordinates.ForEach(x => Coord.Add(x));
                    }
                    else
                    {
                        CurrentArea.Coordinates = new List<Point>();
                    }
                }
            }
            else
            {
                //  Загрузка фигуры, которую выбрал пользователь
                Window loadWin = new DownloadProfileWindow();
                loadWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                if (loadWin.ShowDialog() == true)
                {
                    CurrentArea = loadWin.Tag as PaintArea;
                    MainCanvas.Children.Clear();
                    Coord = new List<Point>();
                    CanCheckMousePos = false;
                    if (CurrentArea == null) CurrentArea = new PaintArea();
                    FillPolygon = CurrentArea.FillPoly;
                    if (CurrentArea.Coordinates != null)
                    {
                        CurrentArea.Coordinates.ForEach(x => Coord.Add(x));
                    }
                    else
                    {
                        CurrentArea.Coordinates = new List<Point>();
                    }
                    DrawBySaveData(CurrentArea);
                }
            }
            DescArea = CurrentArea.Description;
        }

        /// <summary>
        /// Сохранить текущую область
        /// </summary>
        private void SaveProfile(object parameter)
        {
            int id = GetLastIDArea() + 1;
            Window saveWin = new GetStringWindow("Название профиля", $"Profile #{id}", "Сохранить", 250);
            saveWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (saveWin.ShowDialog() == true)
            {
                PaintArea saveArea = new PaintArea()
                {
                    Id = id,
                    Coordinates = Coord,
                    Description = saveWin.Tag.ToString(),
                    FillPoly = FillPolygon,
                    LastCh = DateTime.Now
                };
                List<PaintArea> allAreas = GetAllSaveArea();
                allAreas.Add(saveArea);
                var json = JsonConvert.SerializeObject(allAreas);

                Properties.Settings.Default.Polygon_Json = json;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Получить id последней записи
        /// </summary>
        /// <returns>id последней сохраненной записи</returns>
        private int GetLastIDArea()
        {
            List<PaintArea> allAreas = null;
            try
            {
                allAreas = JsonConvert.DeserializeObject<List<PaintArea>>(Properties.Settings.Default.Polygon_Json);
            }
            catch (Exception)
            {
                return 0;
            }
            if (allAreas == null || allAreas.Count() == 0) return 0;
            return allAreas.Last().Id;
        }

        /// <summary>
        /// Получить список уже сохраненных областей для рисования 
        /// </summary>
        /// <returns>Список областей для рисования</returns>
        private List<PaintArea> GetAllSaveArea()
        {
            List<PaintArea> res = null;
            try
            {
                res = JsonConvert.DeserializeObject<List<PaintArea>>(Properties.Settings.Default.Polygon_Json);
            }
            catch (Exception)
            {
                return new List<PaintArea>();
            }
            return res;
        }

        #endregion
    }
}