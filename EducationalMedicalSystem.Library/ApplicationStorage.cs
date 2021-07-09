using EducationalMedicalSystem.Library.Model;

namespace EducationalMedicalSystem.Library
{
    public class ApplicationStorage
    {
        private static ApplicationStorage _instance;

        public DataSet<LectureGroup> LectureGroups { get; }
        public DataSet<Lecture> Lectures { get; }
        public DataSet<ModelInfo> ModelsInfo { get; }
        public DataSet<ItemCoordinates> PartsOfBody { get; }
        public DataSet<ItemCoordinates> PartsOfDigestion { get; }
        public DataSet<SentenceWithBlank> SentencesWithBlank { get; }
        public DataSet<Match> Matches { get; }

        private ApplicationStorage()
        {
            LectureGroups = new DataSet<LectureGroup>("LectureGroups\\");
            Lectures = new DataSet<Lecture>("Lectures\\");
            ModelsInfo = new DataSet<ModelInfo>("ModelsInfo\\");
            PartsOfBody = new DataSet<ItemCoordinates>("PartsOfBody\\");
            PartsOfDigestion = new DataSet<ItemCoordinates>("PartsOfDigestion\\");
            SentencesWithBlank = new DataSet<SentenceWithBlank>("SentencesWithBlank\\");
            Matches = new DataSet<Match>("Matches\\");
        }

        public static ApplicationStorage GetInstance()
        {
            if (_instance == null)
                _instance = new ApplicationStorage();
            return _instance;
        }
    }
}
