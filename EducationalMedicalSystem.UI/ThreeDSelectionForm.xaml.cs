using System.Windows;
using System.Windows.Input;

namespace EducationalMedicalSystem.UI
{
    public partial class ThreeDSelectionForm : Window
    {
        public ThreeDSelectionForm()
        {
            InitializeComponent();
        }

        private void ModelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowController.ShowNewWindow(this, new SkeletonForm());
        }

        private void BonesBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowController.ShowNewWindow(this, new BonesForm());
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new MainWindow());
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) //moving a window without a system frame
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
