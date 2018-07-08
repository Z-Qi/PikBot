using Discord.Commands;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class CommandsCommand : ModuleBase<SocketCommandContext>
    {
        private CommandService _commands { get; }

        public CommandsCommand (CommandService commands)
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
    }
}
