using EducationalMedicalSystem.Library;
using EducationalMedicalSystem.Library.Model;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EducationalMedicalSystem.UI
{
    public partial class TheoryForm : Window
    {
        private int _pageIndex = 0;
        private readonly PagingButtonsController _pbc;
        private readonly Lecture _lecture;

        private const string RESOURCES_PATH = @"pack://application:,,,/";

        public TheoryForm(int lectureNum)
        {
            InitializeComponent();
            _lecture = ApplicationStorage.GetInstance().Lectures.Data[lectureNum];
            _pbc = new PagingButtonsController(LeftBtn, LeftBtnImage, RightBtn, RightBtnImage, 0, _lecture.Paragraphs.Length - 1);
            DisplayContent();
        }

        private void DisplayContent()
        {
            _pbc.UpdateButtons(_pageIndex);

            Uri uri = new(RESOURCES_PATH + "Backgrounds/Theory/" + _lecture.BackgroundNumbers[_pageIndex] + ".png", UriKind.Absolute);
            BitmapImage bi = new(uri);
            ImageBrush brush = new(bi);
            TheoryWindow.Background = brush;

            ContentTextLeft.Text = "";
            ContentTextFull.Text = "";
            if (_lecture.BackgroundNumbers[_pageIndex] != 0)
                ContentTextLeft.Text = _lecture.Paragraphs[_pageIndex];
            else
                ContentTextFull.Text = _lecture.Paragraphs[_pageIndex];
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex--;
            DisplayContent();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex++;
            DisplayContent();
        }

        private void BackBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => this.Close();
    }
}