namespace EducationalMedicalSystem.UI
{
    class GameProfile
    {
        private static GameProfile _instance;

        public uint Score { get; private set; }
        public bool IsFirstLogIn { get; private set; }
        public bool IsLastTaskDone { get; set; }

        private GameProfile()
        {
            Score = 1;
            IsFirstLogIn = true;
            IsLastTaskDone = false;
        }

        public static GameProfile GetInstance()
        {
            if (_instance == null)
                _instance = new GameProfile();
            return _instance;
        }

        public void LogIn() => IsFirstLogIn = false;

        public void AddOnePointToScore() => Score++;
    }
}
