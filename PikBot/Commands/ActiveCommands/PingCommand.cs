using Discord;
using Discord.Rest;
using Discord.WebSocket;
using PikBot.Bot;
using System;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    class PingCommand : Command
    {
        protected override string Desc => "Check the ping between you and the bot";

        internal PingCommand(ISocketMessageChannel channel, User user, string[] args) : base(channel, user, args) { }

        // Placeholder
        protected override bool HasPermissions(User user) { return user.permissions.Contains(Permission.ALL); }

        public override async Task Run()
        {
            IMessage message = await channel.GetMessageAsync(UInt64.Parse(args[0]));
            RestUserMessage pingMessage = await channel.SendMessageAsync("pinging");
            TimeSpan ping = pingMessage.CreatedAt.Subtract(message.CreatedAt);

            await pingMessage.ModifyAsync(msg => msg.Content = ping.TotalMilliseconds.ToString() + "ms");
        }
    }
}
