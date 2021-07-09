using EducationalMedicalSystem.Library;
using System;
using System.Windows;
using System.Windows.Input;

namespace EducationalMedicalSystem.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitInstances();
            InitializeComponent();
        }

        private static void InitInstances()
        {
            try
            {
                ApplicationStorage.GetInstance();
                ModelStorage.GetInstance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
            }
        }

        private void TheoryBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowController.ShowNewWindow(this, new LectureSelectionForm());
        }

        private void SkeletonBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowController.ShowNewWindow(this, new ThreeDSelectionForm());
        }

        private void CollectTheManBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowController.ShowNewWindow(this, new CollectOrganismForm());
        }

        private void TestsBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowController.ShowNewWindow(this, new TaskSelectionForm());
        }

        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();
    }
}
