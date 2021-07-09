using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EducationalMedicalSystem.UI
{
    class PagingButtonsController
    {
        private const string RESOURCES_PATH = @"pack://application:,,,/";

        private readonly Button _leftBtn;
        private readonly Image _leftImg;
        private readonly Button _rightBtn;
        private readonly Image _rightImg;

        private readonly int _leftBorder;
        private readonly int _rightBorder;

        public PagingButtonsController(Button leftBtn, Image leftImg, Button rightBtn, Image rightImg, int leftBorder, int rightBorder)
        {
            _leftBtn = leftBtn;
            _leftImg = leftImg;
            _rightBtn = rightBtn;
            _rightImg = rightImg;
            _leftBorder = leftBorder;
            _rightBorder = rightBorder;
        }

        public void UpdateButtons(int pageIndex)
        {
            _leftBtn.IsEnabled = true;
            _leftImg.Source = new BitmapImage(new Uri(RESOURCES_PATH + @"Buttons/left.png", UriKind.Absolute));
            _rightBtn.IsEnabled = true;
            _rightImg.Source = new BitmapImage(new Uri(RESOURCES_PATH + @"Buttons/right.png", UriKind.Absolute));

            if (pageIndex == _leftBorder)
            {
                _leftBtn.IsEnabled = false;
                _leftImg.Source = new BitmapImage(new Uri(RESOURCES_PATH + @"Buttons/leftDis.png", UriKind.Absolute));
            }

            if (pageIndex == _rightBorder)
            {
                _rightBtn.IsEnabled = false;
                _rightImg.Source = new BitmapImage(new Uri(RESOURCES_PATH + @"Buttons/rightDis.png", UriKind.Absolute));
            }
        }
    }
}
