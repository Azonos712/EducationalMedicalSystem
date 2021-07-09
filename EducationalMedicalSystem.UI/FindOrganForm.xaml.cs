using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EducationalMedicalSystem.UI
{
    public partial class FindOrganForm : Window
    {
        private readonly HashSet<string> _partsOfBody = new();
        private readonly int _countOfBodyPartsToGuess = 5;
        private readonly Random _random = new();
        private int _currentScore = 0;

        public FindOrganForm()
        {
            InitializeComponent();
            SetCollectionOfBodyPartsToGuess();
            SetNewKeyWord();
            MixPartsOfBody();
        }

        public FindOrganForm(HashSet<string> partsOfBody)
        {
            _partsOfBody = partsOfBody;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            string title = "НАЙДИ ОРГАН";
            string text = "НЕОБХОДИМО НАЙТИ УКАЗАННЫЙ ОРГАН И НАЖАТЬ НА НЕГО.\nЗА ОДИН ПРАВИЛЬНЫЙ ОТВЕТ - 1 БАЛЛ.\nУДАЧИ!";
            WindowController.ShowPopUpWindow(new PopUpForm(title, text, false), GridBlackMask);
        }

        private void SetCollectionOfBodyPartsToGuess()
        {
            Image image;
            for (int i = 0; i < _countOfBodyPartsToGuess; i++)
            {
                do
                {
                    int index = _random.Next(0, BodyPartsGrid.Children.Count);
                    Border border = BodyPartsGrid.Children[index] as Border;
                    image = border.Child as Image;
                } while (!_partsOfBody.Add(image.Tag.ToString()));
            }
        }

        private void SetNewKeyWord()
        {
            if (_partsOfBody.Count == 0)
            {
                string title = "НАЙДИ ОРГАН";
                string declension = DeclensionGenerator.GetDeclension(_currentScore, "БАЛЛ", "БАЛЛА", "БАЛЛОВ");
                string text = $"ВЫ НАБРАЛИ {_currentScore} {declension}!\nНА ДАННЫЙ МОМЕНТ";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text, true), GridBlackMask);
                WindowController.ShowNewWindow(this, new TaskSelectionForm());
                return;
            }

            var index = _random.Next(0, _partsOfBody.Count);
            TxtKeyWord.Text = _partsOfBody.ElementAt(index);
            _partsOfBody.Remove(TxtKeyWord.Text);
        }

        private void MixPartsOfBody()
        {
            int[,] matrix = new int[3, 3];
            for (int i = 0; i < BodyPartsGrid.Children.Count; i++)
            {
                int row;
                int column;
                do
                {
                    row = _random.Next(0, 3);
                    column = _random.Next(0, 3);
                } while (matrix[row, column] == 1);
                matrix[row, column] = 1;
                BodyPartsGrid.Children[i].SetValue(Grid.RowProperty, row);
                BodyPartsGrid.Children[i].SetValue(Grid.ColumnProperty, column);
            }
        }

        private async void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BodyPartsGrid.IsEnabled = false;
            await CheckKeyWord(sender as Image);
            SetNewKeyWord();
            MixPartsOfBody();
            BodyPartsGrid.IsEnabled = true;
        }

        private async Task CheckKeyWord(Image img)
        {
            if (img.Tag.ToString() == TxtKeyWord.Text)
            {
                (img.Parent as Border).BorderBrush = Brushes.Green;
                GameProfile.GetInstance().AddOnePointToScore();
                _currentScore++;
            }
            else
            {
                (img.Parent as Border).BorderBrush = Brushes.Red;
            }

            await Task.Delay(750);
            (img.Parent as Border).BorderBrush = Brushes.Transparent;
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new TaskSelectionForm());
        }
    }
}