using Discord.WebSocket;
using PikBot.Bot;
using System;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    class CommandsCommand : Command
    {
        protected override string Desc => "See a list of available commands";

        internal CommandsCommand(ISocketMessageChannel channel, User user, string[] args) : base(channel, user, args) { }

        // Placeholder
        protected override bool HasPermissions(User user) { return user.permissions.Contains(Permission.ALL); }

        public override async Task Run()
        {
            if (HasPermissions(user))
            {
                string commandList = "```";
                foreach (string commandName in Enum.GetNames(typeof(Commands))) commandList += commandName + ", ";

                await channel.SendMessageAsync(commandList + "```");
            }
        }
    }
}
