using EducationalMedicalSystem.Library;
using EducationalMedicalSystem.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace EducationalMedicalSystem.UI
{
    public partial class CreateChainForm : Window
    {
        private readonly byte _delta = 10;
        private readonly List<ItemCoordinates> _partsOfDigestion;
        private readonly List<int> _positions = new() { 0, 1, 2, 3, 4 };
        private int _currentScore;

        public CreateChainForm()
        {
            _partsOfDigestion = new List<ItemCoordinates>(ApplicationStorage.GetInstance().PartsOfDigestion.Data);
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            string title = "СОСТАВЬ ЦЕПОЧКУ";
            string text = "УСТАНОВИ В ПРАВИЛЬНОЙ ПОСЛЕДОВАТЕЛЬНОСТИ ОРГАНЫ УЧАВСТВУЮЩИЕ В ПИЩЕВАРИТЕЛЬНОМ ПРОЦЕССЕ ЧЕЛОВЕКА.";
            WindowController.ShowPopUpWindow(new PopUpForm(title, text, false), GridBlackMask);
        }

        private async void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (e.Source is not Thumb thumb)
                return;

            if (!_partsOfDigestion.Select(x => x.Name).Contains(thumb.Name))
                return;

            ItemCoordinates pod = _partsOfDigestion.First(x => x.Name == thumb.Name);
            var actualCoords = new Point(Canvas.GetLeft(thumb), Canvas.GetTop(thumb));
            var needCoords = new Point(pod.X, pod.Y);
            SetPosition(thumb, actualCoords.X + e.HorizontalChange, actualCoords.Y + e.VerticalChange);

            for (int i = 0; i < _positions.Count; i++)
            {
                var upperXBound = 85 + (_positions[i] * 200) + _delta;
                var lowerXBound = 85 + (_positions[i] * 200) - _delta;
                var upperYBound = 118 + _delta;
                var lowerYBound = 118 - _delta;

                if (IsWithin(actualCoords.X, lowerXBound, upperXBound) && IsWithin(actualCoords.Y, lowerYBound, upperYBound))
                {
                    var installedX = 85 + (_positions[i] * 200);
                    var installedY = 118;

                    SetPosition(thumb, installedX, installedY);

                    Border b = FindName("Border" + _positions[i]) as Border;
                    if (needCoords.X == installedX)
                    {
                        b.BorderBrush = Brushes.Green;
                        GameProfile.GetInstance().AddOnePointToScore();
                        _currentScore++;
                    }
                    else
                    {
                        b.BorderBrush = Brushes.Red;
                    }

                    _partsOfDigestion.Remove(pod);
                    _positions.Remove(_positions[i]);
                    break;
                }
            }

            await CheckEndOfGame();
        }

        private static void SetPosition(Thumb t, double x, double y)
        {
            Canvas.SetLeft(t, x);
            Canvas.SetTop(t, y);
        }

        private static bool IsWithin(double numberToCheck, double bottom, double top)
        {
            return bottom <= numberToCheck && numberToCheck <= top;
        }

        private async Task CheckEndOfGame()
        {
            if (_positions.Count == 0 && _partsOfDigestion.Count == 0)
            {
                await Task.Delay(650);
                string title = "СОСТАВЬ ЦЕПОЧКУ";
                string declension = DeclensionGenerator.GetDeclension(_currentScore, "ОРГАН", "ОРГАНА", "ОРГАНОВ");
                string text = $"ВЫ УСТАНОВИЛИ ПРАВИЛЬНО { _currentScore} {declension}.\nНА ДАННЫЙ МОМЕНТ";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text, true), GridBlackMask);
                WindowController.ShowNewWindow(this, new TaskSelectionForm());
            }
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new TaskSelectionForm());
        }
    }
}