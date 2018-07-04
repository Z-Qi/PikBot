using Discord.WebSocket;
using PikBot.Bot;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    class TestCommand : Command
    {
        protected override string Desc => "Command for testing things";

        internal TestCommand(ISocketMessageChannel channel, User user, string[] args) : base(channel, user, args) { }

        // Placeholder
        protected override bool HasPermissions(User user) { return user.permissions.Contains(Permission.ALL); }

        public override async Task Run()
        {
            if (HasPermissions(user)) await channel.SendMessageAsync("Nothing here");
        }
    }
}
