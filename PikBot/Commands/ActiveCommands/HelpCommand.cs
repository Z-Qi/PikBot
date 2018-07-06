using Discord.WebSocket;
using PikBot.Bot;
using System;
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
                if (args.Length > 1)
                {
                    String commandName = args[1];

                    try
                    {
                        Object obj = Enum.Parse(typeof(Commands), commandName, true);

                        commandName = obj.ToString();

                        Command command = CommandFactory.GetCommandFactory().GetCommand(channel, user, new string[0], commandName);
                        await command.HelpInfo();
                    }
                    catch
                    {
                        await channel.SendMessageAsync(":thinking:");
                    }
                }
                else
                {
                    await channel.SendMessageAsync("Please include command name you need help with");
                }
            }
        }
    }
}
