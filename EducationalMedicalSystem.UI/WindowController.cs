using System.Windows;
using System.Windows.Controls;

namespace EducationalMedicalSystem.UI
{
    static class WindowController
    {
        public static void ShowNewWindow(Window originWindow, Window newWindow)
        {
            newWindow.Show();
            originWindow.Close();
        }

        public static void ShowPopUpWindow(Window popUpWindow, Grid mask)
        {
            mask.Visibility = Visibility.Visible;
            popUpWindow.ShowDialog();
            mask.Visibility = Visibility.Hidden;
        }
    }
}