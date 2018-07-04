using Discord.WebSocket;
using PikBot.Bot;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace PikBot.Commands
{
    class EchoCommand : Command
    {
        protected override string Desc { get { return "Bot will echo what you said"; } }

        internal EchoCommand(ISocketMessageChannel channel, User user, string[] args) : base(channel, user, args) { }

        // Placeholder
        protected override bool HasPermissions(User user) { return user.permissions.Contains(Permission.ALL); }

        public override async Task Run()
        {
            args = args.Skip(1).ToArray();
            string[] echo = new string[args.Length];
            Array.Copy(args, echo, args.Length);

            await channel.SendMessageAsync(string.Join(" ", echo));
        }
    }
}
