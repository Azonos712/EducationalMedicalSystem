using System;

namespace EducationalMedicalSystem.Library.Model
{
    [Serializable]
    public class Lecture
    {
        public string Title { get; }
        public int[] BackgroundNumbers { get; }
        public string[] Paragraphs { get; }

        public Lecture(string title, string[] paragraphs, int[] backgroundNumbers)
        {
            Title = title;
            Paragraphs = paragraphs;
            BackgroundNumbers = backgroundNumbers;
        }
    }
}