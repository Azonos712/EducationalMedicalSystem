using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalMedicalSystem.Library.Model
{
    [Serializable]
    public class LectureGroup
    {
        public string Title { get; }
        public int StartLectureIndex { get; }
        public int EndLectureIndex { get; }

        public LectureGroup(string title, int startLectureIndex, int endLectureIndex)
        {
            Title = title;
            StartLectureIndex = startLectureIndex;
            EndLectureIndex = endLectureIndex;
        }
    }
}
