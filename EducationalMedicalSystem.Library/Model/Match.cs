using System;

namespace EducationalMedicalSystem.Library.Model
{
    [Serializable]
    public class Match
    {
        public string[] NumericPart { get; }
        public string[] LetterPart { get; }
        public string[] RightAnswers { get; }

        public Match(string[] numericPart, string[] letterPart, string[] rightAnswers)
        {
            NumericPart = numericPart;
            LetterPart = letterPart;
            RightAnswers = rightAnswers;
        }
    }
}