using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
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

        [Command("Purge")]
        [Summary("Removes past messages")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Purge(int numMessages)
        {
            var messages = await Context.Channel.GetMessagesAsync(numMessages).Flatten();
            Dictionary<string, int> msgsToDelete = new Dictionary<string, int>();

            await Context.Channel.DeleteMessagesAsync(messages);

            foreach (IMessage m in messages)
            {
                string author = m.Author.Username + "#" + m.Author.Discriminator;
                if (msgsToDelete.ContainsKey(author))
                    msgsToDelete[author]++;
                else
                    msgsToDelete.Add(author, 1);
            }

            string resultsMessage = "```\n" + "Purge Results\n".PadLeft(48, ' ');

            foreach (KeyValuePair<string, int> pair in msgsToDelete)
            {
                resultsMessage += pair.Key.PadLeft(40, ' ') + ":\t" + pair.Value + "\n";
            }
            resultsMessage += "Total".PadLeft(40, ' ') + ":\t" + numMessages + "```";

            await Context.Channel.SendMessageAsync(resultsMessage);

            //TODO: Add confirmation

            //RestUserMessage confirmation = await Context.Channel.SendMessageAsync(confirmationMessage + "```\nAre you sure you want to delete?");
            //await confirmation.AddReactionAsync(new Emoji("✅"));
            //await confirmation.AddReactionAsync(new Emoji("❎"));
        }

        [Command("Purge")]
        [Summary("Removes bot's own messages")]
        public async Task PurgeOwn(int numMessages)
        {
            var messages = await Context.Channel.GetMessagesAsync(numMessages).Flatten();
            foreach (var m in messages)
            {
                if (m.Author.Id == Context.Client.CurrentUser.Id)
                {
                    IMessage msg = await Context.Channel.GetMessageAsync(m.Id);
                    await msg.DeleteAsync();
                }
            }

            await Context.Channel.SendMessageAsync("Deleted " + numMessages + " messages from the bot");
        }

        [Command("Test")]
        [Summary("Command for testing things")]
        public async Task Test()
        {
            await ReplyAsync("Testing things");
        }
    }
}
