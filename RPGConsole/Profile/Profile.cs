namespace RPGConsole.Profile
{
    public class Profile
    {
        public string name;
        public int id;

        public Player playerInstance;

        public Vector2 position;

        public Profile(string name, ProfileSupport support)
        {
            //generate a id;
            if (playerInstance == null)
            {
                playerInstance = new Player();
            }
            support.maxIDS++;
            id = support.maxIDS + 1;
            this.name = name;
        }

        public void ChangeName(string name)
        {
            this.name = name;
        }

        public void ChangeID(int id)
        {
            this.id = id;
        }
    }
}
