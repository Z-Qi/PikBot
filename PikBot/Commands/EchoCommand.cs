using Discord.Commands;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class EchoCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Echo")]
        [Summary("Echoes user's message")]
        public async Task Echo([Remainder] [Summary("The text to echo back")] string msg)
        {
            await ReplyAsync(msg);
        }
    }
}
