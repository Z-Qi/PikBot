using Discord.Commands;
using Discord.Rest;
using System;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class PingCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Ping")]
        [Summary("Check the ping between you and the bot")]
        public async Task Ping()
        {
            RestUserMessage pingMessage = await Context.Channel.SendMessageAsync("pinging");
            TimeSpan ping = pingMessage.CreatedAt.Subtract(Context.Message.CreatedAt);

            await pingMessage.ModifyAsync(msg => msg.Content = ping.TotalMilliseconds.ToString() + "ms");
        }
    }
}
