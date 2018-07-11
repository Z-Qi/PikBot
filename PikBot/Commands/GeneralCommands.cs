using Discord.Commands;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class GeneralCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Echo")]
        [Summary("Echoes user's message")]
        public async Task Echo([Remainder] [Summary("The text to echo back")] string msg)
        {
            await ReplyAsync(msg);
        }

        [Command("RemindMe")]
        [Alias("Remind", "RM")]
        [Summary("Set up reminders")]
        public async Task RemindMe([Remainder] string time)
        {
            await ReplyAsync("TODO");
        }
    }
}
