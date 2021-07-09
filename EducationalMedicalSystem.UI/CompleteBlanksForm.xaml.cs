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
    public partial class CompleteBlanksForm : Window
    {
        private readonly Brush _defaultBrush;
        private int _currentScore;
        private int _sentenceNum;
        private readonly DataSet<SentenceWithBlank> _sentences;
        private SentenceWithBlank _currentSentence;

        public CompleteBlanksForm()
        {
            _sentences = ApplicationStorage.GetInstance().SentencesWithBlank;
            InitializeComponent();
            _defaultBrush = Blank1.BorderBrush;
            SetSentenceWithBlank();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            string title = "ДОПОЛНИ ПРОПУСКИ";
            string text = "НЕОБХОДИМО ДОПОЛНИТЬ ПРОПУСКИ В ПРЕДЛОЖЕНИИ.\nЗА ОДИН ПРАВИЛЬНО ЗАПОЛЕННЫЙ ПРОПУСК - 1 БАЛЛ.\nУДАЧИ!";
            WindowController.ShowPopUpWindow(new PopUpForm(title, text, false), GridBlackMask);
        }

        private void SetSentenceWithBlank()
        {
            if (_sentenceNum >= _sentences.Data.Count)
            {
                string title = "ДОПОЛНИ ПРОПУСКИ";
                string declension = DeclensionGenerator.GetDeclension(_currentScore, "ПРОПУСК", "ПРОПУСКА", "ПРОПУСКОВ");
                string text = $"ВЫ ПРАВИЛЬНО ДОПОЛНИЛИ {_currentScore}  {declension}.\nНА ДАННЫЙ МОМЕНТ";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text, true), GridBlackMask);
                WindowController.ShowNewWindow(this, new TaskSelectionForm());
                return;
            }

            _currentSentence = _sentences.Data[_sentenceNum];

            Blank1.Text = string.Empty;
            Blank1.BorderBrush = _defaultBrush;
            NextBtn.IsEnabled = false;

            TextBlock1.Text = _currentSentence.FirstPart;
            TextBlock2.Text = _currentSentence.SecondPart;
        }

        private void Blank_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox current = sender as TextBox;
            current.Text = current.Text.ToUpper();
            current.SelectionStart = current.Text.Length;

            if (!string.IsNullOrWhiteSpace(Blank1.Text))
                NextBtn.IsEnabled = true;
            else
                NextBtn.IsEnabled = false;
        }

        private async void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            await CheckBlank();
            _sentenceNum++;
            SetSentenceWithBlank();
        }

        private async Task CheckBlank()
        {
            if (_currentSentence.Blank == Blank1.Text.ToLower().Trim())
            {
                _currentScore++;
                GameProfile.GetInstance().AddOnePointToScore();
                Blank1.BorderBrush = Brushes.Green;
            }
            else
            {
                Blank1.BorderBrush = Brushes.Red;
            }

            MainGrid.IsEnabled = false;
            await Task.Delay(1000);
            MainGrid.IsEnabled = true;
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new TaskSelectionForm());
        }
    }
}