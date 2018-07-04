using Discord.WebSocket;
using PikBot.Bot;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    class HelpCommand : Command
    {
        protected override string Desc => "Learn more about a command";

        internal HelpCommand(ISocketMessageChannel channel, User user, string[] args) : base(channel, user, args) { }

        // Placeholder
        protected override bool HasPermissions(User user) { return user.permissions.Contains(Permission.ALL); }

        public override async Task Run()
        {
            if (HasPermissions(user))
            {
                if (args.Length > 0)
                {
                    char[] commandArray = args[1].ToLower().ToCharArray();
                    commandArray[0] = char.ToUpper(commandArray[0]);
                    string commandName = "PikBot.Commands." + new string(commandArray) + "Command";

                    Command command = CommandFactory.GetCommandFactory().GetCommand(channel, user, new string[0], commandName);
                    await command.HelpInfo();
                }
                else
                {
                    await channel.SendMessageAsync("Please include command name you need help with");
                }
            }
        }
    }
}
