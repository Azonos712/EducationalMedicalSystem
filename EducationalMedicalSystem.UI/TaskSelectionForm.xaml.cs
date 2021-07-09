using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EducationalMedicalSystem.UI
{
    public partial class TaskSelectionForm : Window
    {
        private readonly GameProfile _gameProfile;
        private readonly uint _scoreForTask1 = 1;
        private readonly uint _scoreForTask2 = 4;
        private readonly uint _scoreForTask3 = 10;
        private readonly uint _scoreForTask4 = 15;

        public TaskSelectionForm()
        {
            _gameProfile = GameProfile.GetInstance();
            InitializeComponent();
            CheckScore();
        }

        private void CheckScore()
        {
            TxtBlockScore.Text = _gameProfile.Score.ToString();

            CheckButtonAvailability(Task1Btn, _scoreForTask1);
            CheckButtonAvailability(Task2Btn, _scoreForTask2);
            CheckButtonAvailability(Task3Btn, _scoreForTask3);
            CheckButtonAvailability(Task4Btn, _scoreForTask4);
        }

        private void CheckButtonAvailability(Button btn, uint neededScore)
        {
            if (_gameProfile.Score >= neededScore)
                btn.Style = (Style)FindResource("DefaultButtonStyle");
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            if (_gameProfile.IsFirstLogIn)
            {
                string title = "ВВЕДЕНИЕ";
                string text1 = "ДОБРО ПОЖАЛОВАТЬ В ИНТЕРАКТИВНОЕ ОБУЧЕНИЕ.\n";
                string text2 = "ВЫПОЛНЯЙ ДОСТУПНЫЕ ЗАДАНИЯ, ПОЛУЧАЙ ЗА НИХ БАЛЛЫ И ОТКРЫВАЙ НОВЫЕ.\nУСПЕХОВ!";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text1 + text2, false), GridBlackMask);
                _gameProfile.LogIn();
            }

            if (_gameProfile.IsLastTaskDone)
            {
                await Task.Delay(350);
                string title = "ПОЗДРАВЛЯЕМ!";
                string declension = DeclensionGenerator.GetDeclension((int)_gameProfile.Score, "БАЛЛ", "БАЛЛА", "БАЛЛОВ");
                string text = $"ВЫ ПРОШЛИ ВСЕ ЗАДАНИЯ И НАБРАЛИ {_gameProfile.Score} {declension}.\nДО НОВЫХ ВСТРЕЧ!";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text, false), GridBlackMask);
                _gameProfile.IsLastTaskDone = false;
            }
        }

        private bool CheckTaskAvailability(uint neededScore)
        {
            if (_gameProfile.Score < neededScore)
            {
                string title = "ОШИБКА";
                string declension = DeclensionGenerator.GetDeclension((int)neededScore, "БАЛЛ", "БАЛЛА", "БАЛЛОВ");
                string text = $"ДЛЯ ОТКРЫТИЯ ДАННОГО ЗАДАНИЯ ТРЕБУЕТСЯ МИНИМУМ {neededScore} {declension}";
                WindowController.ShowPopUpWindow(new PopUpForm(title, text, false), GridBlackMask);
                return false;
            }
            return true;
        }

        private void Task1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTaskAvailability(_scoreForTask1))
                WindowController.ShowNewWindow(this, new FindOrganForm());
        }

        private void Task2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTaskAvailability(_scoreForTask2))
                WindowController.ShowNewWindow(this, new CreateChainForm());
        }

        private void Task3Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTaskAvailability(_scoreForTask3))
                WindowController.ShowNewWindow(this, new CompleteBlanksForm());
        }

        private void Task4Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTaskAvailability(_scoreForTask4))
                WindowController.ShowNewWindow(this, new MatchForm());
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