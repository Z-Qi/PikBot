using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using PikBot.Bot;
using PikBot.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PikBot
{
    public class Program
    {
        private class Credentials
        {
            public string Prefix { get; set; }
            public string Token { get; set; }
        }

        private Credentials credentials = JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(@"./cred.json"));
        private CommandFactory factory = CommandFactory.GetCommandFactory();
        private readonly string self = "<@377237438529273856>";
        private readonly string selfNick = "<@!377237438529273856>";

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var discordClient = new DiscordSocketClient();

            discordClient.Log += Log;
            discordClient.MessageReceived += MessageReceived;

            string token = credentials.Token;

            await discordClient.LoginAsync(TokenType.Bot, token);
            await discordClient.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot) return;
            if (!(
                message.Content.StartsWith(credentials.Prefix) ||
                message.Content.StartsWith(self) ||
                message.Content.StartsWith(selfNick)
                )) return;

            message.Content.Trim();
            ISocketMessageChannel channel = message.Channel;
            string[] args;

            if (message.Content.StartsWith(credentials.Prefix))
                args = message.Content.Substring(credentials.Prefix.Length).Split(' ');
            else
                args = message.Content.Substring(message.Content.StartsWith(self) ? self.Length : selfNick.Length).Trim().Split(' ');

            string commandName = args[0];

            try
            {
                Object obj = Enum.Parse(typeof(Commands.Commands), commandName, true);

                char[] commandArray = obj.ToString().ToCharArray();
                commandArray[0] = char.ToUpper(commandArray[0]);
                commandName = "PikBot.Commands." + new string(commandArray) + "Command";
            }
            catch
            {
                await Log(new LogMessage(LogSeverity.Info, "New Message", message.Author.Id + ": Invalid Command " + commandName));
                return;
            }

            await Log(new LogMessage(LogSeverity.Info, "New Message", message.Author.Id + ": " + commandName));

            args[0] = message.Id.ToString();
            Command command = factory.GetCommand(channel, new User(message.Author.Id.ToString()), args, commandName);
            await command.Run();
        }
    }
}