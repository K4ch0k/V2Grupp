using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using V2Grupp.Model;

namespace V2Grupp.ViewModel
{
    public class ShowTxtWIndow_VM : PropertyChangedBase
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
        /// Удалить выбранный профиль
        /// </summary>
        public ICommand DeleteCmd { get; }
        /// <summary>
        /// Сохранить выбранный профиль
        /// </summary>
        public ICommand SaveCmd { get; }

        FlowDocument _doc;
        public FlowDocument Document
        {
            get { return _doc; }
            set
            {
                _doc = value;
                OnPropertyChanged(nameof(Document));
            }
        }

        #endregion

        public ShowTxtWIndow_VM(string XmlTxt)
        {
            Document = ToFlowDocument(XmlTxt);
        }

        #region Обработка событий 

        public FlowDocument ToFlowDocument(string xmlString)
        {
            FlowDocument res = new FlowDocument();

            if (!string.IsNullOrEmpty(xmlString))
            {
                try
                {
                    res.Blocks.Add((Section)XamlReader.Parse(xmlString));
                }
                catch (Exception)
                {
                    res.Blocks.Add(new Paragraph());
                }
            }

            return res;
        }

        #endregion
    }
}