using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace EducationalMedicalSystem.UI
{
    public partial class BonesForm : Window
    {
        private int _pageIndex = 1;
        private readonly PagingButtonsController _pbc;
        private readonly ModelStorage _modelsStorage;
        private ModelVisual3D _currentDevice3D;

        public BonesForm()
        {
            _modelsStorage = ModelStorage.GetInstance();
            InitializeComponent();
            _pbc = new PagingButtonsController(LeftBtn, LeftBtnImage, RightBtn, RightBtnImage, 1, _modelsStorage.Models.Count - 1);
            Display3DModel();
        }

        private void Display3DModel()
        {
            if (_modelsStorage.Models.Count <= 1 || _modelsStorage.Models[_pageIndex] == null)
            {
                MessageBox.Show("Ошибка загрузки моделей!");
                return;
            }

            _pbc.UpdateButtons(_pageIndex);

            TxtInfo.Text = _modelsStorage.ModelsInfo[_pageIndex].Description;

            SetViewerSettings();
            UpdateScene();
        }

        private void SetViewerSettings()
        {
            viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            viewPort3d.ZoomExtentsWhenLoaded = true;
            if (_currentDevice3D == null)
            {
                viewPort3d.Camera.Position = new Point3D(0.5, 2, 3);
                viewPort3d.Camera.LookDirection = new Vector3D(0, -1, 0);
                viewPort3d.Camera.UpDirection = new Vector3D(0, 0, 1);
            }
        }

        private void UpdateScene()
        {
            ModelVisual3D device3D = new()
            {
                Content = _modelsStorage.Models[_pageIndex]
            };

            if (_currentDevice3D != null)
                viewPort3d.Children.Remove(_currentDevice3D);

            _currentDevice3D = device3D;

            viewPort3d.Children.Add(_currentDevice3D);
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex--;
            Display3DModel();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex++;
            Display3DModel();
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