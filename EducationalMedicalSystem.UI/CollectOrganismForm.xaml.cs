using EducationalMedicalSystem.Library;
using EducationalMedicalSystem.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace EducationalMedicalSystem.UI
{
    public partial class CollectOrganismForm : Window
    {
        private readonly byte _delta = 6;
        private readonly List<ItemCoordinates> _partsOfBody;

        public CollectOrganismForm()
        {
            _partsOfBody = new List<ItemCoordinates>(ApplicationStorage.GetInstance().PartsOfBody.Data);
            InitializeComponent();
        }

        private async void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (e.Source is not Thumb thumb)
                return;

            if (!_partsOfBody.Select(x => x.Name).Contains(thumb.Name))
                return;

            ItemCoordinates pof = _partsOfBody.First(x => x.Name == thumb.Name);

            var actualCoords = new Point(Canvas.GetLeft(thumb), Canvas.GetTop(thumb));
            var needCoords = new Point(pof.X, pof.Y);

            SetPosition(thumb, actualCoords.X + e.HorizontalChange, actualCoords.Y + e.VerticalChange);

            var upperXBound = needCoords.X + _delta;
            var lowerXBound = needCoords.X - _delta;
            var upperYBound = needCoords.Y + _delta;
            var lowerYBound = needCoords.Y - _delta;

            if (IsWithin(actualCoords.X, lowerXBound, upperXBound) && IsWithin(actualCoords.Y, lowerYBound, upperYBound))
            {
                SetPosition(thumb, needCoords.X, needCoords.Y);
                _partsOfBody.Remove(pof);
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
            if (_partsOfBody.Count == 0)
            {
                await Task.Delay(700);
                var title = "СОБЕРИ ЧЕЛОВЕКА";
                var text = "МОЛОДЕЦ, ВСЕ ОРГАНЫ УСТАНОВЛЕНЫ В СООТВЕТСТВУЮЩИХ МЕСТАХ";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text, false), GridBlackMask);
                WindowController.ShowNewWindow(this, new MainWindow());
            }
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new MainWindow());
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}