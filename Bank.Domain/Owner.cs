using System.Collections.Generic;

namespace Bank.Domain
{
    public class Owner
    {
        public Owner(int id, string title)
        {
            Id = id;
            Title = title;
        }

        private Owner()
        {

        }

        public int Id { get; }
        public string Title { get; }
        public IEnumerable<Account> Accounts { get; }
    }
}