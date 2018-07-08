using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class HelpCommand : ModuleBase<SocketCommandContext>
    {
        private CommandService _commands { get; }

        public HelpCommand(CommandService commands)
        {
            _commands = commands;
        }

        [Command("Help")]
        [Summary("Learn more about a command")]
        public async Task Help([Summary("Command name")] string commandName = "")
        {
            if (!string.IsNullOrEmpty(commandName))
            {
                commandName = commandName.ToLower();

                //Stopwatch sw = new Stopwatch();

                //sw.Start();

                Dictionary<string, CommandInfo> commandsDict = new Dictionary<string, CommandInfo>();
                foreach (CommandInfo command in _commands.Commands)
                {
                    commandsDict.Add(command.Name.ToLower(), command);
                }

                try
                {
                    await Context.Channel.SendMessageAsync(commandsDict.GetValueOrDefault(commandName).Summary);
                }
                catch
                {
                    await Context.Channel.SendMessageAsync("There is no such command :thinking:");
                }

                //sw.Stop();

                //Console.WriteLine("Elapsed={0}", sw.Elapsed);

                //sw.Restart();

                //foreach (CommandInfo command in _commands.Commands)
                //{
                //    if (command.Name.ToLower().Equals(commandName)) await Context.Channel.SendMessageAsync(command.Summary);
                //}

                //sw.Stop();

                //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            }
            else
            {
                await Context.Channel.SendMessageAsync("Please include the command name you need help with");
            }
        }
    }
}
