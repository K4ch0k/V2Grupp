using System.Windows;

namespace V2Grupp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для GetStringWindow.xaml
    /// </summary>
    public partial class GetStringWindow : Window
    {
        public GetStringWindow(string WhatNeed, string InputStr, string OkBtnTxt, int StrMaxLength)
        {
            DataContext = new ViewModel.GetStringWindow_VM(WhatNeed, InputStr, OkBtnTxt, StrMaxLength);
            InitializeComponent();
        }
    }
}