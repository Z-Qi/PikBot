using Discord.Commands;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class TestCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Test")]
        [Summary("Command for testing things")]
        public async Task Test()
        {
            await ReplyAsync("Testing things");
        }
    }
}
