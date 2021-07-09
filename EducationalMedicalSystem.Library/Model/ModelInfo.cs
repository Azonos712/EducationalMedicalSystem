using System;

namespace EducationalMedicalSystem.Library.Model
{
    [Serializable]
    public class ModelInfo
    {
        public string Description { get; }
        public string ModelPath { get; }

        public ModelInfo(string description, string modelpath)
        {
            Description = description;
            ModelPath = modelpath;
        }
    }
}
