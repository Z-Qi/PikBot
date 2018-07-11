using Discord.Commands;
using Discord.WebSocket;
using PikBot.Bot.ServiceManager;
using System.Threading.Tasks;

namespace PikBot.Commands
{
    public class GameCommands : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _client;
        private readonly GameManager _games;

        public GameCommands(DiscordSocketClient client, GameManager gameManager)
        {
            _client = client;
            _games = gameManager;
        }

        [Command("Countries")]
        [Summary("Country name game")]
        public async Task Countries()
        {
            if (_games.HasActiveGame(Context.Channel.Id))
            {
                await Context.Channel.SendMessageAsync("There is already an active game in this channel.");
                return;
            }
            else
            {
                _games.GetNewGame(Context.Channel.Id, "Countries");
                await ReplyAsync("IN PROD");
            }
        }

        [Command("Odd Ones Out")]
        [Alias("OddOnesOut", "OddOnes", "Odd Ones", "Odd", "OOO")]
        [Summary("Deduce who's not in the majority and vote them out")]
        public async Task OddOnesOut()
        {
            if (_games.HasActiveGame(Context.Channel.Id))
            {
                await Context.Channel.SendMessageAsync("There is already an active game in this channel.");
                return;
            }
            else
            {
                _games.GetNewGame(Context.Channel.Id, "OddOnesOut");
                await ReplyAsync("IN PROD");
            }
        }

        [Command("Abort")]
        [Alias("Stop", "End", "Quit")]
        [Summary("Ends any games active in the channel")]
        public async Task Abort()
        {
            if (_games.Abort(Context.Channel.Id))
                await Context.Channel.SendMessageAsync("All active games ended");
            else
                await Context.Channel.SendMessageAsync("There are no actives games to abort");
        }

        [Command("Debug")]
        public async Task Debug()
        {
            _games.Debug();
        }
    }
}
