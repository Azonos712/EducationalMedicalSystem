using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EducationalMedicalSystem.Library;
using EducationalMedicalSystem.Library.Model;

namespace EducationalMedicalSystem.UI
{
    public partial class LectureSelectionForm : Window
    {
        private readonly DataSet<Lecture> _lectures;
        private readonly DataSet<LectureGroup> _lectureGroups;
        private bool _isSubLectures = true;

        public LectureSelectionForm()
        {
            _lectureGroups = ApplicationStorage.GetInstance().LectureGroups;
            _lectures = ApplicationStorage.GetInstance().Lectures;

            InitializeComponent();
            ShowLectures();
        }

        private void ShowLectures()
        {
            LecturesSP.Children.Clear();
            SubLecturesSP.Children.Clear();

            if (_lectures.Data.Count == 0 || _lectureGroups.Data.Count == 0)
            {
                MessageBox.Show("Ошибка загрузки лекций!");
                WindowController.ShowNewWindow(this, new MainWindow());
            }

            foreach (Lecture lecture in _lectures.Data)
            {
                Button btn = CreateButton(lecture.Title, OnLectureBtnClick);
                SubLecturesSP.Children.Add(btn);
            }

            foreach (LectureGroup group in _lectureGroups.Data)
            {
                int start = group.StartLectureIndex;
                int end = group.EndLectureIndex;

                RoutedEventHandler handler = start == end ?
                    (_, _) => OnLectureBtnClick(SubLecturesSP.Children[start], null) :
                    (_, _) => ShowNestedButtons(start, end);

                Button btn = CreateButton(group.Title, handler);
                LecturesSP.Children.Add(btn);
            }
        }

        private Button CreateButton(string title, RoutedEventHandler handler)
        {
            Button btn = new();
            btn.Content = new TextBlock()
            {
                TextAlignment = TextAlignment.Center,
                Text = title,
                TextWrapping = TextWrapping.Wrap
            };

            btn.Width = 550;
            btn.Style = (Style)FindResource("DefaultButtonStyle");
            btn.Click += handler;
            return btn;
        }

        private void OnLectureBtnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            int index = SubLecturesSP.Children.IndexOf(btn);
            new TheoryForm(index).ShowDialog();
        }

        private void ShowNestedButtons(int start, int end)
        {
            SwitchStackPanelsVisibility();

            for (int i = 0; i < SubLecturesSP.Children.Count; i++)
                if (i < start || i > end)
                    SubLecturesSP.Children[i].Visibility = Visibility.Collapsed;

            BackBtn.MouseDown -= BackBtn_MouseDown;
            BackBtn.MouseDown += InnerBackBtn_MouseDown;
        }

        private void InnerBackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SwitchStackPanelsVisibility();

            foreach (UIElement element in SubLecturesSP.Children)
                element.Visibility = Visibility.Visible;

            BackBtn.MouseDown -= InnerBackBtn_MouseDown;
            BackBtn.MouseDown += BackBtn_MouseDown;
        }

        private void SwitchStackPanelsVisibility()
        {
            LecturesSV.Visibility = (Visibility)Convert.ToByte(_isSubLectures); //Visibility.Hidden == 1; neew true
            SubLecturesSV.Visibility = (Visibility)Convert.ToByte(!_isSubLectures); //Visibility.Visible == 0; need false

            _isSubLectures = !_isSubLectures;
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new MainWindow());
        }
    }
}