using Discord.WebSocket;
using PikBot.Bot;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    enum Commands
    {
        commands,
        echo,
        ping,
        help,
        test,
    }

    abstract class Command
    {
        protected ISocketMessageChannel channel;
        protected User user;
        protected string[] args;
        abstract protected string Desc { get; }

        protected Command(ISocketMessageChannel channel, User user, string[] args)
        {
            this.channel = channel;
            this.user = user;
            this.args = args;
        }

        virtual public async Task HelpInfo()
        {
            await channel.SendMessageAsync(Desc);
        }

        abstract public Task Run();
        abstract protected bool HasPermissions(User user);
    }
}
