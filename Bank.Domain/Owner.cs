namespace Bank.Domain
{
    public class Owner
    {
        public Owner(string title)
        {   
            Title = title;
        }

        private Owner()
        {

        }

        public int Id { get; }
        public string Title { get; }
    }
}