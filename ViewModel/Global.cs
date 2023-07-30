using Newtonsoft.Json;
using System;
using V2Grupp.Model;

namespace V2Grupp.ViewModel
{
    public static class Global
    {
        /// <summary>
        /// Базовое название окна (то, что видит пользователь)
        /// </summary>
        public static string AppName = "V2Grupp";

        /// <summary>
        /// Базовая иконка для окна
        /// </summary>
        public static string LogoIcon = "/Image/v2_Only.png";

        /// <summary>
        /// Сессия, с которой сейчас происходит работа
        /// </summary>
        public static PaintArea SelectedPaintArea { get; set; }

        /// <summary>
        /// Сохранить <see cref="SelectedPaintArea"/>
        /// </summary>
        public static void SavePaintArea()
        {
            var json = JsonConvert.SerializeObject(SelectedPaintArea);

            Properties.Settings.Default.Last_Polygon_Json = json;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Загрузить последний холст
        /// </summary>
        public static void LoadPaintArea()
        {
            PaintArea res = null;
            try
            {
                res = JsonConvert.DeserializeObject<PaintArea>(Properties.Settings.Default.Last_Polygon_Json);
                if (res.Description == null || res.Description.Trim() == "") res.Description = "Не сохранено в профиль";
            }
            catch (Exception)
            {
                SelectedPaintArea = new PaintArea();
            }
            SelectedPaintArea = res;
        }
    }
}