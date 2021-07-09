using EducationalMedicalSystem.Library;
using EducationalMedicalSystem.Library.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EducationalMedicalSystem.UI
{
    public partial class MatchForm : Window
    {
        private readonly Brush _defaultBrush;
        private int _currentScore;
        private int _matchNum;
        private readonly DataSet<Match> _matches;
        private Match _currentMatch;

        public MatchForm()
        {
            _matches = ApplicationStorage.GetInstance().Matches;
            InitializeComponent();
            _defaultBrush = Blank1.BorderBrush;
            SetMatch();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            string title = "СООТВЕТСТВИЕ";
            string text = "НЕОБХОДИМО УСТАНОВИТЬ СООТВЕТСТВИЕ.\nКАЖДОЕ ЗАДАНИЕ - 1 БАЛЛ.\nУДАЧИ!";
            WindowController.ShowPopUpWindow(new PopUpForm(title, text, false), GridBlackMask);
        }

        private void SetMatch()
        {
            if (_matchNum >= _matches.Data.Count)
            {
                string title = "СООТВЕТСТВИЕ";
                string text = "КОЛИЧЕСТВО ПРАВИЛЬНО ВЫПОЛНЕНЫХ ЗАДАНИЙ " + _currentScore + ".\nНА ДАННЫЙ МОМЕНТ";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text, true), GridBlackMask);
                GameProfile.GetInstance().IsLastTaskDone = true;
                WindowController.ShowNewWindow(this, new TaskSelectionForm());
                return;
            }

            _currentMatch = _matches.Data[_matchNum];

            Blank1.Text = Blank2.Text = Blank3.Text = Blank4.Text = string.Empty;
            Blank1.BorderBrush = Blank2.BorderBrush = Blank3.BorderBrush = Blank4.BorderBrush = _defaultBrush;
            NextBtn.IsEnabled = false;

            LeftTxtBlock.Text = string.Join("\n", _currentMatch.NumericPart);
            RightTxtBlock.Text = string.Join("\n", _currentMatch.LetterPart);

            if (string.IsNullOrWhiteSpace(LeftTxtBlock.Text))
                LeftImage.Visibility = Visibility.Visible;

        }

        private void Blank_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox current = sender as TextBox;
            current.Text = current.Text.ToUpper();
            current.SelectionStart = current.Text.Length;

            CheckNextBtn();
        }

        private void CheckNextBtn()
        {
            bool isEmptyBlank1 = string.IsNullOrWhiteSpace(Blank1.Text);
            bool isEmptyBlank2 = string.IsNullOrWhiteSpace(Blank2.Text);
            bool isEmptyBlank3 = string.IsNullOrWhiteSpace(Blank3.Text);
            bool isEmptyBlank4 = string.IsNullOrWhiteSpace(Blank4.Text);

            if (!isEmptyBlank1 && !isEmptyBlank2 && !isEmptyBlank3 && !isEmptyBlank4)
                NextBtn.IsEnabled = true;
            else
                NextBtn.IsEnabled = false;
        }

        private async void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            await CheckMatch();

            _matchNum++;
            SetMatch();
        }

        private async Task CheckMatch()
        {
            int answers = 0;

            answers += CheckSingleMatch(Blank1.Text, _currentMatch.RightAnswers[0], Blank1);
            answers += CheckSingleMatch(Blank2.Text, _currentMatch.RightAnswers[1], Blank2);
            answers += CheckSingleMatch(Blank3.Text, _currentMatch.RightAnswers[2], Blank3);
            answers += CheckSingleMatch(Blank4.Text, _currentMatch.RightAnswers[3], Blank4);

            if (answers == 4)
            {
                GameProfile.GetInstance().AddOnePointToScore();
                _currentScore++;
            }

            MainGrid.IsEnabled = false;
            await Task.Delay(1000);
            MainGrid.IsEnabled = true;
        }

        private static int CheckSingleMatch(string current, string answer, TextBox border)
        {
            if (current.ToLower().Trim() == answer)
            {
                border.BorderBrush = Brushes.Green;
                return 1;
            }
            else
            {
                border.BorderBrush = Brushes.Red;
                return 0;
            }
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new TaskSelectionForm());
        }
    }
}