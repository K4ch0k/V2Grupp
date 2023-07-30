using System;
using System.Collections.Generic;
using System.Windows;

namespace V2Grupp.Model
{
    /// <summary>
    /// Информация о нарисованных объектах
    /// </summary>
    public class PaintArea
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Обозначение записи, введенное пользователем
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Последнее изменение
        /// </summary>
        public DateTime LastCh { get; set; }
        /// <summary>
        /// Список координат, которые образуют фигуру
        /// </summary>
        public List<Point> Coordinates { get; set; }
        /// <summary>
        /// Нужно ли залить фигуру цветом
        /// </summary>
        public bool FillPoly { get; set; }
    }
}