using HelixToolkit.Wpf;
using EducationalMedicalSystem.Library;
using EducationalMedicalSystem.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace EducationalMedicalSystem.UI
{
    public class ModelStorage
    {
        private readonly List<Model3D> _models = new();
        public IReadOnlyList<Model3D> Models { get { return _models; } }
        public IReadOnlyList<ModelInfo> ModelsInfo { get; }

        private static ModelStorage _instance;

        private ModelStorage()
        {
            ModelsInfo = ApplicationStorage.GetInstance().ModelsInfo.Data;
            LoadAllModels();
        }

        public static ModelStorage GetInstance()
        {
            if (_instance == null)
                _instance = new ModelStorage();
            return _instance;
        }

        private async void LoadAllModels()
        {
            _models.Clear();
            foreach (var modelInfo in ModelsInfo)
            {
                var temp = await Task.Run(() => LoadModel(modelInfo.ModelPath));
                _models.Add(temp);
            }
        }

        private Model3DGroup LoadModel(string model3DPath)
        {
            var mi = new ModelImporter
            {
                DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Beige))
            };
            return mi.Load(model3DPath, null, true);
        }
    }
}
