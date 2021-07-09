namespace EducationalMedicalSystem.UI
{
    public static class DeclensionGenerator
    {
        /// <summary>
        /// Возвращает слова в падеже, зависимом от заданного числа 
        /// </summary>
        /// <param name="number">Число от которого зависит выбранное слово</param>
        /// <param name="nominativ">Именительный падеж слова. Например "день"</param>
        /// <param name="genetiv">Родительный падеж слова. Например "дня"</param>
        /// <param name="plural">Множественное число слова. Например "дней"</param>
        /// <returns></returns>
        public static string GetDeclension(int number, string nominativ, string genetiv, string plural)//переписать кое-шо
        {
            number %= 100;
            if (11 <= number && number <= 19)
                return plural;

            number %= 10;

            return number switch
            {
                1 => nominativ,
                2 or 3 or 4 => genetiv,
                _ => plural,
            };
        }
    }
}