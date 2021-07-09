using Newtonsoft.Json;
using System.IO;

namespace EducationalMedicalSystem.Library
{
    class JsonReader<T> where T : class
    {
        private readonly string _path;
        private T _data;

        public JsonReader(string path)
        {
            _path = path;
        }

        public T GetReadedData()
        {
            JsonSerializer serializer = new();
            using (StreamReader sr = new(_path))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                _data = serializer.Deserialize<T>(reader);
            }

            return _data;
        }
    }
}