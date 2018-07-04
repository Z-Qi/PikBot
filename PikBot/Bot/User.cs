using System.Collections.Generic;

namespace PikBot.Bot
{
    class User
    {
        private string id;
        public HashSet<Permission> permissions = new HashSet<Permission>();
        public bool InGame { get; set; }

        public User(string id)
        {
            // Placeholders
            this.id = id;
            permissions.Add(Permission.ALL);
            InGame = false;
        }
    }
}
