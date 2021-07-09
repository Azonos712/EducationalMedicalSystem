using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace EducationalMedicalSystem.UI
{
    public partial class SkeletonForm : Window
    {
        private readonly Model3D _skeletonModel;

        public SkeletonForm()
        {
            _skeletonModel = ModelStorage.GetInstance().Models[0];
            InitializeComponent();
            Display3DModel();
        }

        private void Display3DModel()
        {
            if (_skeletonModel == null)
            {
                MessageBox.Show("Ошибка загрузки модели скелета!");
                return;
            }

            ModelVisual3D device3D = new();
            device3D.Content = _skeletonModel;
            viewPort3d.Children.Add(device3D);
            SetViewerSettings();
        }

        private void SetViewerSettings()
        {
            viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            viewPort3d.Camera.Position = new Point3D(0, 4, 1.5);
            viewPort3d.Camera.LookDirection = new Vector3D(0, -1, 0);
            viewPort3d.Camera.UpDirection = new Vector3D(0, 0, 1);
        }

        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowController.ShowNewWindow(this, new ThreeDSelectionForm());
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
