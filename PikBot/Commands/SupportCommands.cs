using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class SupportCommands : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commands;

        public SupportCommands(CommandService commands)
        {
            _commands = commands;
        }

        [Command("Commands")]
        [Summary("See a list of available commands")]
        public async Task Commands()
        {
            string commandList = "```";
            foreach (CommandInfo command in _commands.Commands) commandList += command.Name + ", ";

            await Context.Channel.SendMessageAsync(commandList.Substring(0, commandList.Length - 2) + "```");
        }

        [Command("Help")]
        [Summary("Learn more about a command")]
        public async Task Help([Summary("Command name")] string commandName = "")
        {
            if (!string.IsNullOrEmpty(commandName))
            {
                commandName = commandName.ToLower();

                Dictionary<string, CommandInfo> commandsDict = new Dictionary<string, CommandInfo>();
                foreach (CommandInfo command in _commands.Commands) commandsDict.Add(command.Name.ToLower(), command);

                try
                {
                    await Context.Channel.SendMessageAsync(commandsDict.GetValueOrDefault(commandName).Summary);
                }
                catch
                {
                    await Context.Channel.SendMessageAsync("There is no such command :thinking:");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Please include the command name you need help with");
            }
        }
    }
}
