using System;

namespace EducationalMedicalSystem.Library.Model
{
    [Serializable]
    public class SentenceWithBlank
    {
        public string FirstPart { get; }
        public string Blank { get; }
        public string SecondPart { get; }

        public SentenceWithBlank(string firstPart, string blank, string secondPart)
        {
            FirstPart = firstPart;
            Blank = blank;
            SecondPart = secondPart;
        }
    }
}