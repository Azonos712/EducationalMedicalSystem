using System.Windows;

namespace EducationalMedicalSystem.UI
{
    public partial class PopUpForm : Window
    {
        public PopUpForm(string title, string text, bool isShowedTotalScore)
        {
            InitializeComponent();
            TxtBlockTitle.Text = title;
            TxtBlockText.Text = text;

            if (isShowedTotalScore)// заходит если 1 - тру и показывает счёт
            {
                ScorePanel.Visibility = Visibility.Visible;
                TxtBlockScore.Text = GameProfile.GetInstance().Score.ToString();
            }
        }

        private void BtnForTxt_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
