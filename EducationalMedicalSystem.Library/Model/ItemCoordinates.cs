using System;

namespace EducationalMedicalSystem.Library.Model
{
    [Serializable]
    public class ItemCoordinates
    {
        public string Name { get; }
        public int X { get; }
        public int Y { get; }

        public ItemCoordinates(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }
    }
}
