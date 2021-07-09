using System;
using System.Collections.Generic;
using System.IO;

namespace EducationalMedicalSystem.Library
{
    public class DataSet<T> where T : class
    {
        private readonly List<T> _data = new();
        public IReadOnlyList<T> Data { get { return _data; } }
        public string PathToFiles { get; }

        public DataSet(string pathToFiles)
        {
            PathToFiles = pathToFiles;
            LoadStorageData();
        }

        private void LoadStorageData()
        {
            if (!Directory.Exists(PathToFiles))
                throw new Exception($"Отсутствует каталог по следующему пути: {PathToFiles}");

            _data.Clear();
            var files = Directory.GetFiles(PathToFiles);
            foreach (string filePath in files)
            {
                JsonReader<T> r = new(filePath);
                _data.Add(r.GetReadedData());
            }
        }
    }
}
