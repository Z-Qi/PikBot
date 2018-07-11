using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class UtilityCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Ping")]
        [Summary("Check the ping between you and the bot")]
        public async Task Ping()
        {
            RestUserMessage pingMessage = await Context.Channel.SendMessageAsync("pinging");
            TimeSpan ping = pingMessage.CreatedAt.Subtract(Context.Message.CreatedAt);

            await pingMessage.ModifyAsync(msg => msg.Content = ping.TotalMilliseconds.ToString() + "ms");
        }

        [Command("User")]
        [Alias("WhoIs", "Info")]
        [Summary("Get more information about a user")]
        public async Task UserInfo(string content = "")
        {
            ulong userId = Context.Message.MentionedUsers.Count > 0 ?
                Context.Message.MentionedUsers.First().Id :
                Context.User.Id;

            SocketGuildUser user = Context.Guild.GetUser(userId);

            EmbedBuilder builder = new EmbedBuilder
            {
                Color = new Color(255, 240, 220),
                Title = user.Username + "#" + user.Discriminator,
                ThumbnailUrl = user.GetAvatarUrl(),
                Footer = new EmbedFooterBuilder { Text = "ID: " + user.Id.ToString() },
                Timestamp = DateTime.UtcNow,
            };
            builder.AddInlineField("Nickname", user.Nickname ?? user.Username);
            builder.AddInlineField("Joined", user.JoinedAt.Value.Date.ToShortDateString());
            builder.AddInlineField("Current Status", user.Status);
            builder.AddInlineField("Registered", user.CreatedAt.Date.ToShortDateString());

            await Context.Channel.SendMessageAsync("", false, builder);
        }

        [Command("Test")]
        [Summary("Command for testing things")]
        public async Task Test()
        {
            await ReplyAsync("Testing things");
        }
    }
}
